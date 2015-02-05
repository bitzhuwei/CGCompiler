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
    public partial class SyntaxTreeTextBox<TEnumTokenType, TEnumVType, TTreeNodeValue> 
        : TextBox, ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>// Control
        where TEnumTokenType : struct, IComparable, IFormattable, IConvertible
        where TEnumVType : struct, IComparable, IFormattable, IConvertible
        where TTreeNodeValue : class, ICloneable, new()
    {
        /// <summary>
        /// 语法树显示器控件
        /// </summary>
        public SyntaxTreeTextBox()
        {
            InitializeComponent();
            this.m_DelUpdateTreeNode = new Action<int>(UpdateTreeNode);
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
            var sourceCodeViewer = this.GetSourceCodeViewer();
            if (sourceCodeViewer != null)
                sourceCodeViewer.RemoveSyntaxTreeViewer(this);
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
                //string result = string.Format("{0}"
                //    , m_UpdatingNodeLine);
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
            int notUpdatedCount = m_SyntaxTreeWatchdog.NotDealtCount();
            if (notUpdatedCount > 0)
            {
                this.UpdateTreeNode(notUpdatedCount);
            }
            notUpdatedCount = m_SyntaxTreeWatchdog.NotDealtCount();
            if (notUpdatedCount == 0)
            {
                if (this.Focused)
                {
                    int newIndex = this.SelectionStart;
                    if (newIndex != m_UpdatedNodeLine)
                    {
                        MapToSourceCodeViewer(newIndex);
                    }
                }
                else if (m_UpdatingNodeLine != m_UpdatedNodeLine
                    || m_UpdatingNodeLineAtSpace != m_UpdatedNodeLineAtSpace)
                {
                    Highlight();
                }
            }
        }

        /// <summary>
        /// 更新控件上显示的语法树
        /// </summary>
        /// <param name="notUpdatedCount"></param>
        private void UpdateTreeNode(int notUpdatedCount)
        {
            if (this.IsDisposed) return;
            if (this.InvokeRequired)
            {
                this.Invoke(m_DelUpdateTreeNode, new object[] { notUpdatedCount });
            }
            else
            {
                if (this.m_SourceCodeViewer != null)
                {
                    var root = this.m_SourceCodeViewer.GetOutputSyntaxTree();
                    string strTree = root.ToString();
                    int notUpdatedCount2 = this.m_SyntaxTreeWatchdog.NotDealtCount();
                    if (notUpdatedCount == notUpdatedCount2)
                    {
                        this.Text = strTree;
                        this.m_SyntaxTreeWatchdog.Decrease(notUpdatedCount);
                    }
                }
            }
        }

        /// <summary>
        /// 光标所在位置改变，应该映射到对应的源代码文本框（SourceCodeTextBox）中
        /// </summary>
        /// <param name="newIndex">当前光标位置</param>
        private void MapToSourceCodeViewer(int newIndex)
        {
            if (this.m_SourceCodeViewer == null) return;
            var tokenListSource = this.m_SourceCodeViewer.GetOutputTokenList();
            if (tokenListSource.Count == 0) return;

            int line = this.GetLineFromCharIndex(newIndex);
            var tree = this.GetSyntaxTreeFromLine(line);
            if (tree == null) return;
            var tokens = tree.MappedTotalTokenList;
            if (tokens.Count == 0) return;
            if (tree.MappedTokenStartIndex >= tokens.Count) return;
            int first = tokens[tree.MappedTokenStartIndex].IndexOfSourceCode;
            int length;
            if (tree.MappedTokenLength == 0)
            {
                length = 0;
            }
            else
            {
                var last = tokens[tree.MappedTokenStartIndex + tree.MappedTokenLength - 1];
                length = last.IndexOfSourceCode + last.Length - first;
            }
         
            this.GetSourceCodeViewer().NotifyToHighlight(first, length);
        }

        private SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue>
            GetSyntaxTreeFromLine(int line)
        {
            SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> result = null;
            var tree = this.m_SourceCodeViewer.GetOutputSyntaxTree();
            if (tree == null) return null;
            int currLine = 0;
            result = _GetSyntaxTreeFromLine(tree, line, ref currLine);
            return result;
        }

        private SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue>
            _GetSyntaxTreeFromLine(
            SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> tree, int line, ref int currLine)
        {
            if (line == currLine)
            {
                return tree;
            }

            SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> result = null;
            foreach (var item in tree.Children)
            {
                currLine++;
                result = _GetSyntaxTreeFromLine(item, line, ref currLine);
                if (result != null)
                    break;
            }
            return result;
        }
        /// <summary>
        /// 高亮指定索引的单词信息
        /// </summary>
        /// <param name="tokenIndex"></param>
        /// <param name="indexAtSpace"></param>
        public void NotifyToHighlight(int tokenIndex, bool indexAtSpace)
        {
            m_UpdatingNodeLine = GetSyntaxTreeNodeLineByTokenIndex(
                this.m_SourceCodeViewer.GetOutputSyntaxTree(), tokenIndex);
            m_UpdatingNodeLineAtSpace = indexAtSpace;
        }
        /// <summary>
        /// 根据单词索引获取对应的树节点
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="tokenIndex"></param>
        /// <returns></returns>
        public int GetSyntaxTreeNodeLineByTokenIndex(
            SyntaxTree<TEnumTokenType,TEnumVType,TTreeNodeValue> tree, int tokenIndex)
        {
            int result = 0;
            _GetSyntaxTreeNodeLineByTokenIndex(tree, tokenIndex, ref result);
            return result;
        }

        private bool _GetSyntaxTreeNodeLineByTokenIndex(
            SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> tree, 
            int tokenIndex,
            ref int result)
        {
            if (tree.Children.Count == 0)
            {
                return (tree.MappedTokenStartIndex == tokenIndex
                    && tree.MappedTokenLength > 0);
            }
            else
            {
                foreach (var item in tree.Children)
                {
                    result++;
                    if (_GetSyntaxTreeNodeLineByTokenIndex(item, tokenIndex, ref result))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        /// <summary>
        /// 高亮源代码指定索引位置的单词分析结果
        /// <para>成功返回true，否则返回false</para>
        /// </summary>
        /// <returns></returns>
        private void Highlight(/*int notHighlightedCount*/)
        {
            if (this.IsDisposed) return;
            int line = this.GetFirstCharIndexFromLine(m_UpdatingNodeLine);
            int line2 = this.GetFirstCharIndexFromLine(m_UpdatingNodeLine + 1);
            if (line >= 0)//Token列表已经显示到文本框
            {
                int length = line2 >= 0 ? (m_UpdatingNodeLineAtSpace ? 0 : line2 - line - 1) : 0;
                this.Select(line, length);
                this.ScrollToCaret();
                m_UpdatedNodeLine = m_UpdatingNodeLine;
                m_UpdatedNodeLineAtSpace = m_UpdatingNodeLineAtSpace;
            }

        }

        #endregion 单词定位


        /// <summary>
        /// 通知此控件SyntaxTree已更新，需重新显示到控件
        /// </summary>
        public void SyntaxTreeUpdated()
        {
            this.m_SyntaxTreeWatchdog.Increase();
        }

        #region 属性和字段
        /// <summary>
        /// 要更新高亮行而未完成更新时此文本框中的光标位置
        /// </summary>
        private int m_UpdatingNodeLine = 0;
        /// <summary>
        /// 本文本框中的光标上次的位置
        /// </summary>
        private int m_UpdatedNodeLine = -1;
        /// <summary>
        /// true表示要更新高亮行时应选中源代码中的空白处
        /// </summary>
        private bool m_UpdatingNodeLineAtSpace = false;
        /// <summary>
        /// true表示选中了源代码中的空白处
        /// </summary>
        private bool m_UpdatedNodeLineAtSpace = false;

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
        private ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> m_SourceCodeViewer;

        private ChangeWatchdog m_SyntaxTreeWatchdog = new ChangeWatchdog();
        /// <summary>
        /// 源代码监视器，记录源代码的更改、分析次数
        /// </summary>
        private ChangeWatchdog m_SourceCodeWatchdog = new ChangeWatchdog();
        private Delegate m_DelUpdateTreeNode;
        private Func<string> m_DelToString;
        ///// <summary>
        ///// 运行标记
        ///// </summary>
        //private RunningSign m_Sign = new RunningSign();

        private EventHandler appIdleEvent;// = new EventHandler(this.Application_Idle);

        //private TokenTextBoxUpdateBean m_TokenTextBoxUpdateWatchdog = new TokenTextBoxUpdateBean();
        #endregion 属性和字段        
        
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
                value.AddSyntaxTreeViewer(this);
        }

    }
}
