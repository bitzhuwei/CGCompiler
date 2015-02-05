using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// 单词列表文本框，用于显示单词列表
    /// </summary>
    /// <typeparam name="TEnumTokenType">单词的枚举类型</typeparam>
    /// <typeparam name="TEnumVType">语法分析中的结点类型(某Vn or 某Vt)，建议使用枚举类型</typeparam>
    /// <typeparam name="TTreeNodeValue">语法树结点值，根据语音特性自定义类型进行填充</typeparam>
    public partial class TokenListTextBox<TEnumTokenType, TEnumVType, TTreeNodeValue> 
        : TextBox, ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>// Control
        where TEnumTokenType : struct, IComparable, IFormattable, IConvertible
        where TEnumVType : struct, IComparable, IFormattable, IConvertible
        where TTreeNodeValue : class, ICloneable, new()
    {
        /// <summary>
        /// 单词列表文本框，用于显示单词列表
        /// </summary>
        public TokenListTextBox()
        {
            InitializeComponent();
            this.m_DelUpdatetxtTokens = new Action<int>(UpdateText);
            this.m_DelToString = new Func<string>(ToString);
            this.appIdleEvent = new EventHandler(this.Application_Idle);
            Application.Idle += this.appIdleEvent;
        }
        /// <summary>
        /// 销毁此对象之前要去掉Application.Idle事件并通知关联的源代码显示控件移除对此对象的引用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            Application.Idle -= this.appIdleEvent;
            if (this.m_SourceCodeViewer != null)
                this.m_SourceCodeViewer.RemoveTokenListViewer(this);
            base.OnHandleDestroyed(e);
        }

        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    base.OnPaint(pe);
        //}
        /// <summary>
        /// 实现了全选功能
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                this.SelectAll();
            }
        }
        /// <summary>
        /// 文本改变，通知高亮源代码指定索引位置的单词分析结果
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Highlight();
        }

        /// <summary>
        /// 此信息可显示在状态栏中
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.IsDisposed) return "This TokenListBox instance is disposed.";
            if (this.InvokeRequired)
            {
                return this.Invoke(m_DelToString) as string;
            }
            else
            {
                int index = this.SelectionStart;
                int Ln = this.GetLineFromCharIndex(index);
                int Col = index - this.GetFirstCharIndexFromLine(Ln);
                string result = string.Format("Ln {0}/{1}, Col {2}, Ind ({3}, {4})/{5}"
                    , Ln, this.Lines.Count(), Col, index, this.SelectionLength, this.Text.Length);
                return result;
            }
        }

        #region 单词定位

        /// <summary>
        /// 加载到Application.Idle事件上，即可实现实时监测更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Application_Idle(object sender, EventArgs e)
        {
            int notUpdatedCount = m_TokenListWatchdog.NotDealtCount();
            if (notUpdatedCount > 0)
            {
                this.UpdateText(notUpdatedCount);
            }
            notUpdatedCount = m_TokenListWatchdog.NotDealtCount();
            if (notUpdatedCount == 0)
            {
                if (this.Focused)
                {
                    int newIndex = this.SelectionStart;
                    if (newIndex != m_UpdatedIndex)
                    {
                        MapToSourceCodeViewer(newIndex, this.SelectionLength);
                    }
                }
                else if (m_UpdatingIndex != m_UpdatedIndex
                    || m_UpdatingIndexAtSpace != m_UpdatedIndexAtSpace)
                {
                    Highlight();
                }
            }
        }
        /// <summary>
        /// 更新控件上显示的TokenList
        /// </summary>
        /// <param name="notUpdatedCount"></param>
        private void UpdateText(int notUpdatedCount)
        {
            if (this.IsDisposed) return;
            if (this.InvokeRequired)
            {
                this.Invoke(m_DelUpdatetxtTokens, new object[] { notUpdatedCount });
            }
            else
            {
                if (this.m_SourceCodeViewer != null)
                {
                    try
                    {
                        StringBuilder builder = new StringBuilder();
                        //var outputTokenList = this.m_SourceCodeViewer.GetOutputTokenList();
                        foreach (var v in this.m_SourceCodeViewer.GetOutputTokenList())
                        {
                            builder.AppendLine(v.ToString());
                        }
                        int notUpdatedCount2 = this.m_TokenListWatchdog.NotDealtCount();
                        if (notUpdatedCount == notUpdatedCount2)
                        {
                            this.Text = builder.ToString();
                            this.m_TokenListWatchdog.Decrease(notUpdatedCount);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 光标所在位置改变，应该映射到对应的源代码文本框（SourceCodeTextBox）中
        /// </summary>
        /// <param name="newIndex">当前光标位置</param>
        /// <param name="length">当前选中的字符串长度</param>
        private void MapToSourceCodeViewer(int newIndex, int length)
        {
            if (this.m_SourceCodeViewer == null) return;
            var tokenListSource = this.m_SourceCodeViewer.GetOutputTokenList();
            if (tokenListSource.Count == 0) return;
            int line = this.GetLineFromCharIndex(newIndex);
            int tokenIndex;
            if (line >= tokenListSource.Count)
                tokenIndex = tokenListSource.Count - 1;
            else
                tokenIndex = line;
            var tk = tokenListSource[tokenIndex];
            this.m_SourceCodeViewer.NotifyToHighlight(tk.IndexOfSourceCode, tk.Length);
            m_UpdatedIndex = newIndex;
            m_UpdatingIndex = newIndex;
            m_UpdatedIndexAtSpace = true;
            m_UpdatingIndexAtSpace = true;
        }

        /// <summary>
        /// 高亮指定索引的单词信息
        /// </summary>
        /// <param name="tokenIndex"></param>
        /// <param name="indexAtSpace"></param>
        public void NotifyToHighlight(int tokenIndex, bool indexAtSpace)
        {
            m_UpdatingIndex = tokenIndex;
            m_UpdatingIndexAtSpace = indexAtSpace;
        }
        /// <summary>
        /// 高亮源代码指定索引位置的单词分析结果
        /// <para>成功返回true，否则返回false</para>
        /// </summary>
        /// <returns></returns>
        private void Highlight(/*int notHighlightedCount*/)
        {
            if (this.IsDisposed) return;
            int line = this.GetFirstCharIndexFromLine(m_UpdatingIndex);
            int line2 = this.GetFirstCharIndexFromLine(m_UpdatingIndex + 1);
            if (line >= 0)//Token列表已经显示到文本框
            {
                int length = line2 >= 0 ? (m_UpdatingIndexAtSpace ? 0 : line2 - line - 1) : 0;
                this.Select(line, length);
                this.ScrollToCaret();
                m_UpdatedIndex = m_UpdatingIndex;
                m_UpdatedIndexAtSpace = m_UpdatingIndexAtSpace;
            }

        }

        #endregion 单词定位


        /// <summary>
        /// 通知此控件TokenList已更新，需重新显示到控件
        /// </summary>
        public void TokenListUpdated()
        {
            this.m_TokenListWatchdog.Increase();
        }

        #region 属性和字段
        /// <summary>
        /// 要更新高亮行而未完成更新时此文本框中的光标位置
        /// </summary>
        private int m_UpdatingIndex = 0;
        /// <summary>
        /// 本文本框中的光标上次的位置
        /// </summary>
        private int m_UpdatedIndex = -1;
        /// <summary>
        /// true表示要更新高亮行时应选中源代码中的空白处
        /// </summary>
        private bool m_UpdatingIndexAtSpace = false;
        /// <summary>
        /// true表示选中了源代码中的空白处
        /// </summary>
        private bool m_UpdatedIndexAtSpace = false;

        ///// <summary>
        ///// 分析得到的单词列表
        ///// <para>文本框根据此列表显示单词列表</para>
        ///// </summary>
        //public TokenList TokenListSource
        //{
        //    get { return m_TokenListSource; }
        //    set { if (value != null) m_TokenListSource = value; }
        //}
        ///// <summary>
        ///// 分析得到的单词列表
        ///// <para>文本框根据此列表显示单词列表</para>
        ///// </summary>
        //private TokenList m_TokenListSource = new TokenList();


        ///// <summary>
        ///// 要更新高亮行而未完成更新时源代码文本框中的光标位置
        ///// </summary>
        //private int m_CurrentIndexOfSourceCode;
        ///// <summary>
        ///// 上次在源代码文本框中的光标位置
        ///// </summary>
        //private int m_LastIndexOfSourceCode;
        /// <summary>
        /// 获取所监视的源代码显示控件
        /// </summary>
        public ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> GetSourceCodeViewer()
        {
            return m_SourceCodeViewer;
        }

        /// <summary>
        /// 设置所监视的源代码显示控件
        /// </summary>
        public void SetSourceCodeViewer(
            ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> value)
        {
            m_SourceCodeViewer = value;
            if (value != null && (!value.Contains(this)))
                value.AddTokenListViewer(this);
        }
        private ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> m_SourceCodeViewer;

        private ChangeWatchdog m_TokenListWatchdog = new ChangeWatchdog();
        /// <summary>
        /// 源代码监视器，记录源代码的更改、分析次数
        /// </summary>
        private ChangeWatchdog m_SourceCodeWatchdog = new ChangeWatchdog();
        private Delegate m_DelUpdatetxtTokens;
        private Func<string> m_DelToString;
        ///// <summary>
        ///// 运行标记
        ///// </summary>
        //private RunningSign m_Sign = new RunningSign();

        private EventHandler appIdleEvent;// = new EventHandler(this.Application_Idle);

        //private TokenTextBoxUpdateBean m_TokenTextBoxUpdateWatchdog = new TokenTextBoxUpdateBean();
        #endregion 属性和字段

    }
}
