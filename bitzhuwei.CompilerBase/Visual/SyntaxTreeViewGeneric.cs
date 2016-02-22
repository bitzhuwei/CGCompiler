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
    /// 语法树显示器控件
    /// </summary>
    /// <typeparam name="TEnumTokenType">单词的枚举类型</typeparam>
    /// <typeparam name="TEnumVType">语法分析中的结点类型(某Vn or 某Vt)，建议使用枚举类型</typeparam>
    /// <typeparam name="TTreeNodeValue">语法树结点值，根据语音特性自定义类型进行填充</typeparam>
    public partial class SyntaxTreeView<TEnumTokenType, TEnumVType, TTreeNodeValue>
        : TreeView, ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>//Control
        where TEnumTokenType : struct, IComparable, IFormattable, IConvertible
        where TEnumVType : struct, IComparable, IFormattable, IConvertible
        where TTreeNodeValue : class, ICloneable, new()
    {
        /// <summary>
        /// 语法树显示器控件
        /// </summary>
        public SyntaxTreeView()
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
        /// 此信息可显示在状态栏中
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.IsDisposed) return "This SyntaxTreeView instance is disposed.";
            if (this.InvokeRequired)
            {
                return this.Invoke(m_DelToString) as string;
            }
            else
            {
                string name = string.Empty;
                if (this.SelectedNode != null)
                {
                    name = this.SelectedNode.Name;
                }
                return string.Format("Nodes: {0}, Current Node Name: {1}", this.Nodes.Count.ToString(), name);
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
            //todo:not finished
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
                    TreeNode newNode = this.SelectedNode;
                    if (newNode != m_UpdatedNode)
                    {
                        MapToSourceCodeViewer(newNode);//, this.SelectionLength);
                    }
                }
                else if (m_UpdatingNode != m_UpdatedNode
                    || m_UpdatingNodeAtSpace != m_UpdatedNodeAtSpace)
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
                    TreeNode rootNode = new TreeNode();
                    Bind(rootNode, root);
                    int notUpdatedCount2 = this.m_SyntaxTreeWatchdog.NotDealtCount();
                    if (notUpdatedCount == notUpdatedCount2)
                    {
                        this.Nodes.Clear();
                        this.Nodes.Add(rootNode);
                        this.ExpandAll();
                        this.m_SyntaxTreeWatchdog.Decrease(notUpdatedCount);
                    }
                }
            }
        }

        /// <summary>
        /// 将得到的语法树绑定显示到控件
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="root"></param>
        private void Bind(TreeNode rootNode, SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> root)
        {
            rootNode.Tag = root;
            rootNode.Checked = !root.SyntaxError;
            rootNode.Name = root.NodeValue.ToString();/*.NodeName;*/
            int length = root.MappedTokenLength;
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("{0},{1}_{2} {{ ", rootNode.Name, root.MappedTokenStartIndex, root.MappedTokenLength));
            if (root.MappedTotalTokenList != null && root.MappedTotalTokenList.Count >= length)
                for (int i = 0; i < length; i++)
                {
                    builder.Append(root.MappedTotalTokenList[root.MappedTokenStartIndex + i].Detail + " ");
                }
            builder.Append("}");
            if (root.SyntaxError)
                builder.Append(" error:" + root.ErrorInfo);
            rootNode.Text = builder.ToString();
            foreach (var v in root.Children)
            {
                TreeNode childNode = new TreeNode();
                Bind(childNode, v);
                rootNode.Nodes.Add(childNode);
            }
        }

        /// <summary>
        /// 光标所在位置改变，应该映射到对应的源代码文本框（SourceCodeTextBox）中
        /// </summary>
        /// <param name="newNode">新选中的结点</param>
        private void MapToSourceCodeViewer(TreeNode newNode)//, int length)
        {
            m_UpdatedNode = newNode;
            m_UpdatingNode = newNode;
            //m_UpdatedNodeAtSpace = length == 0;
            //m_UpdatingNodeAtSpace = length == 0; 
            if (newNode == null) return;
            var sourceCodeViewer = this.GetSourceCodeViewer();
            if (sourceCodeViewer == null) return;
            var tree = newNode.Tag as SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue>;
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

        /// <summary>
        /// 高亮指定索引的单词信息
        /// </summary>
        /// <param name="tokenIndex"></param>
        /// <param name="indexAtSpace"></param>
        public void NotifyToHighlight(int tokenIndex, bool indexAtSpace)
        {
            m_UpdatingNode = GetTreeNodeByTokenIndex(this.Nodes, tokenIndex);
            m_UpdatingNodeAtSpace = indexAtSpace;
        }
        /// <summary>
        /// 根据单词索引获取对应的树节点
        /// </summary>
        /// <param name="nodeCollection"></param>
        /// <param name="tokenIndex"></param>
        /// <returns></returns>
        public TreeNode GetTreeNodeByTokenIndex(TreeNodeCollection nodeCollection, int tokenIndex)
        {
            TreeNode node = null;
            foreach (var v in nodeCollection)
            {
                node = v as TreeNode;
                var tree = node.Tag as SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue>;
                if (tree != null && tree.Children.Count == 0)
                {
                    if (tree.MappedTokenStartIndex == tokenIndex
                        && tree.MappedTokenLength > 0)
                    {
                        return (v as TreeNode);
                    }
                }
                else
                {
                    TreeNode result = GetTreeNodeByTokenIndex(node.Nodes, tokenIndex);
                    if (result != null) return result;
                }
            }
            return null;
        }
        /// <summary>
        /// 高亮源代码指定索引位置的单词分析结果
        /// <para>成功返回true，否则返回false</para>
        /// </summary>
        /// <returns></returns>
        private void Highlight(/*int notHighlightedCount*/)
        {
            if (this.IsDisposed) return;
            this.SelectedNode = m_UpdatingNode;
            m_UpdatedNode = m_UpdatingNode;
            m_UpdatedNodeAtSpace = m_UpdatingNodeAtSpace;
        }

        #endregion 单词定位

        /// <summary>
        /// 通知此控件，SyntaxTree的数据已经更新，要重新绘制
        /// </summary>
        public void SyntaxTreeUpdated()
        {
            this.m_SyntaxTreeWatchdog.Increase();
        }

        #region 属性和字段
        /// <summary>
        /// 要更新高亮行而未完成更新时此文本框中的光标位置
        /// </summary>
        private TreeNode m_UpdatingNode = null;
        /// <summary>
        /// 本文本框中的光标上次的位置
        /// </summary>
        private TreeNode m_UpdatedNode = null;
        /// <summary>
        /// true表示要更新高亮行时应选中源代码中的空白处
        /// </summary>
        private bool m_UpdatingNodeAtSpace = false;
        /// <summary>
        /// true表示选中了源代码中的空白处
        /// </summary>
        private bool m_UpdatedNodeAtSpace = false;
        /// <summary>
        /// 获取本控件绑定的源代码控件
        /// </summary>
        /// <returns></returns>
        public ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> GetSourceCodeViewer()
        {
            return m_SourceCodeViewer;
        }
        /// <summary>
        /// 设置此语法树显示器控件要监控的源代码控件
        /// </summary>
        /// <param name="sourceCodeViewer"></param>
        public void SetSourceCodeViewer(ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> sourceCodeViewer)
        {
            m_SourceCodeViewer = sourceCodeViewer;
            if (sourceCodeViewer != null && (!sourceCodeViewer.Contains(this)))
                sourceCodeViewer.AddSyntaxTreeViewer(this);
        }

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

        private EventHandler appIdleEvent;

        #endregion 属性和字段

    }
}
