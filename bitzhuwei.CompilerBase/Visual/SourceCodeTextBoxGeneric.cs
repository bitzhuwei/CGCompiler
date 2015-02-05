using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading;

namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// 源代码文本框，集成了词法分析、语法分析功能
    /// </summary>
    /// <typeparam name="TEnumTokenType">单词的枚举类型</typeparam>
    /// <typeparam name="TEnumVType">语法分析中的结点类型(某Vn or 某Vt)，建议使用枚举类型</typeparam>
    /// <typeparam name="TTreeNodeValue">语法树结点值，根据语音特性自定义类型进行填充</typeparam>
    public partial class SourceCodeTextBox<TEnumTokenType, TEnumVType, TTreeNodeValue>
        : TextBox, ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>
        where TEnumTokenType : struct, IComparable, IFormattable, IConvertible
        where TEnumVType : struct, IComparable, IFormattable, IConvertible
        where TTreeNodeValue : class, ICloneable, new()
    {
        /// <summary>
        /// 源代码文本框，集成了词法分析、语法分析功能
        /// </summary>
        public SourceCodeTextBox()
        {
            InitializeComponent();
            this.m_DelToString = new Func<string>(this.ToString);
            this.appIdleEvent = new EventHandler(this.Application_Idle);
            Application.Idle += this.appIdleEvent;
        }
        /// <summary>
        /// 源代码改变，进行词法分析和语法分析
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            Console.WriteLine("{0}: SourceCodeTextBox.OnTextChanged, {1}", DateTime.Now, this.ToString());
            base.OnTextChanged(e);
            if (this.m_CompilerThread == null)
            {
                this.m_CompilerThread = new Thread(new ThreadStart(Do_Compile));
                this.m_CompilerThread.IsBackground = true;
                //this.m_CompilerThread.Priority = ThreadPriority.Lowest;
                this.m_CompilerThread.Start();
            }
            this.SetSourceCode(this.Text);
            this.m_SourceCodeWatchdog.Increase();
        }
        /// <summary>
        /// 实现了全选的功能
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
        //protected override void OnPaint(PaintEventArgs pe)
        //{
        //    Graphics g = pe.Graphics;
        //    g.DrawString("请在此处输入源代码或通过拖拽、点击菜单栏工具栏的打开按钮加载源文件"
        //        , m_Font, m_Brush, 10, 10);
        //    base.OnPaint(pe);
        //}
        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    Graphics g = pevent.Graphics;
        //    g.DrawString("请在此处输入源代码或通过拖拽、点击菜单栏工具栏的打开按钮加载源文件"
        //        , m_Font, m_Brush, 10, 10);
        //    base.OnPaintBackground(pevent);
        //}
        /// <summary>
        /// 销毁此对象之前要去掉Application.Idle事件并通知关联的源代码显示控件移除对此对象的引用
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            Application.Idle -= this.appIdleEvent;
            foreach (var v in this.m_TokenListViewerCollection)
            {
                v.SetSourceCodeViewer(null);
            }
            base.OnHandleDestroyed(e);
        }
        /// <summary>
        /// 此信息可显示在状态栏中
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.InvokeRequired)
            {
                return this.Invoke(m_DelToString) as string;
            }
            else
            {
                int index = this.SelectionStart;
                int Ln = this.GetLineFromCharIndex(index);
                int Col = index - this.GetFirstCharIndexFromLine(Ln);
                int notAnalyzedCount = m_SourceCodeWatchdog.NotDealtCount();
                string result = string.Format("Ln {0}/{1}, Col {2}, Ind ({3}, {4})/{5}, Ana {6}/{7}({8}) {9}, Tk {10}/{11}"
                    , Ln, this.Lines.Count(), Col, index, this.SelectionLength, this.Text.Length
                    , m_SourceCodeWatchdog.DealtCount()
                    , m_SourceCodeWatchdog.TotalChangedCount()
                    , notAnalyzedCount > 0 ? notAnalyzedCount.ToString() : "√"
                    , m_CompilerThread != null ? (m_CompilerThread.IsAlive ? "Alive" : "Dead") : "null"
                    , m_SelectedTokenIndex >= 0 ?
                        ((m_SelectedTokenIndexAtSpace ? "<-" : "") + m_SelectedTokenIndex.ToString())
                        : "NaN"
                    , this.GetOutputTokenList().Count
                    );
                return result;
            }
        }

        #region 单词定位
        /// <summary>
        /// 源代码文本框的光标位置改变
        /// <para>要通知单词列表控件高亮相应的单词</para>
        /// </summary>
        /// <param name="newIndex">源代码中的光标位置</param>
        /// <param name="selectedLength">选中的字符长度</param>
        void MapToViewerCollection(int newIndex, int selectedLength)
        {
            if (this.m_SourceCodeWatchdog.NotDealtCount() > 0) return;
            int sourceCodeIndex;
            int tokenIndex = -1; bool indexAtSpace = false;
            if (this.SelectionLength > 0)
                sourceCodeIndex = newIndex;
            else if (newIndex > 0)
                sourceCodeIndex = newIndex - 1;
            else
                sourceCodeIndex = newIndex;

            int length = this.GetOutputTokenList().Count;
            if (length == 0)
            {
                tokenIndex = 0; indexAtSpace = true;
            }
            if (tokenIndex == -1 && sourceCodeIndex < this.GetOutputTokenList()[0].IndexOfSourceCode)
            {
                tokenIndex = 0; indexAtSpace = true;
            }
            if (tokenIndex == -1)
            {
                int lastCharIndexAtSourceCode = this.GetOutputTokenList()[length - 1].IndexOfSourceCode
                    + this.GetOutputTokenList()[length - 1].Length;
                if (sourceCodeIndex >= lastCharIndexAtSourceCode)
                {
                    tokenIndex = length; indexAtSpace = true;
                }
            }
            if (tokenIndex == -1)
                for (int i = 0; i < length; i++)
                {
                    if (this.GetOutputTokenList()[i].IndexOfSourceCode <= sourceCodeIndex
                        && sourceCodeIndex < this.GetOutputTokenList()[i].IndexOfSourceCode + this.GetOutputTokenList()[i].Length)
                    {//应高亮索引为i的单词
                        tokenIndex = i; indexAtSpace = false;
                        break;
                    }
                }
            if (tokenIndex == -1)
                for (int i = 1; i < length; i++)
                {
                    if (this.GetOutputTokenList()[i - 1].IndexOfSourceCode + this.GetOutputTokenList()[i - 1].Length <= sourceCodeIndex
                        && sourceCodeIndex < this.GetOutputTokenList()[i].IndexOfSourceCode)
                    {//源代码显示控件中的光标在空白处
                        tokenIndex = i; indexAtSpace = true;
                        break;
                    }
                }

            foreach (var v in this.m_TokenListViewerCollection)
            {
                v.NotifyToHighlight(tokenIndex, indexAtSpace);
            }
            foreach (var v in this.m_SyntaxTreeViewerCollection)
            {
                v.NotifyToHighlight(tokenIndex, indexAtSpace);
            }
            this.m_UpdatedIndex = newIndex;
            this.m_UpdatingIndex = newIndex;
            this.m_UpdatedLength = selectedLength;
            this.m_UpadatingLength = selectedLength;
            this.m_SelectedTokenIndex = tokenIndex;
            this.m_SelectedTokenIndexAtSpace = indexAtSpace;
        }

        /// <summary>
        /// 加载到Application.Idle事件上，即可实现实时监测更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Application_Idle(object sender, EventArgs e)
        {
            if (this.Focused)
            {
                int index = this.SelectionStart;
                int length = this.SelectionLength;
                if (index != m_UpdatedIndex || length != m_UpdatedLength)
                {//映射到TokenList文本框
                    MapToViewerCollection(index, length);
                }
            }
            else if (this.m_UpdatingIndex != this.m_UpdatedIndex
                || this.m_UpadatingLength != this.m_UpdatedLength)
            {
                HighlightTokenInSourceCode();
            }
        }

        /// <summary>
        /// 源代码高亮
        /// </summary>
        private void HighlightTokenInSourceCode()
        {
            this.Select(this.m_UpdatingIndex, this.m_UpadatingLength);
            this.ScrollToCaret();
            this.m_UpdatedIndex = this.m_UpdatingIndex;
            this.m_UpdatedLength = this.m_UpadatingLength;
        }

        /// <summary>
        /// 源代码高亮
        /// </summary>
        /// <param name="start">单词第一个字符的索引</param>
        /// <param name="length">单词字符个数</param>
        public void NotifyToHighlight(int start, int length)
        {
            this.m_UpdatingIndex = start;
            this.m_UpadatingLength = length;
        }


        #endregion 单词定位

        /// <summary>
        /// m_CompilerThread的函数体
        /// </summary>
        private void Do_Compile()
        {
            int notAnalyzedCount = 0;
            int notAnalyzedCount2 = 0;
            bool notChangedAgain = true;
            var tokens = new TokenList<TEnumTokenType>();
            var tmp = new TokenList<TEnumTokenType>();
            while (true)
            {
                notAnalyzedCount = this.m_SourceCodeWatchdog.NotDealtCount();
                notChangedAgain = true;
                if (notAnalyzedCount > 0)
                {
                    Console.WriteLine("{0}: SourceCodeTextBox, Do_Compile, {1}", DateTime.Now, this.ToString());
                    tokens.Clear();
                    m_LexicalAnalyzer.SetSourceCode(this.GetSourceCode());
                    while (true)
                    {
                        tmp = m_LexicalAnalyzer.Analyze(10000);
                        notAnalyzedCount2 = this.m_SourceCodeWatchdog.NotDealtCount();
                        if (notAnalyzedCount == notAnalyzedCount2)//源代码没有发生新的改变
                        {
                            if (tmp.Count > 0)
                            {
                                tokens.AddRange(tmp);
                                Thread.Sleep(0);//给UI线程执行的机会
                            }
                            else
                                break;//词法分析完毕
                        }
                        else
                        {
                            Console.WriteLine("changedagain {0}/{1}", notAnalyzedCount, notAnalyzedCount2);
                            notChangedAgain = false;
                            //m_LexicalAnalyzer.Reset();//有m_LexicalAnalyzer.SourceCode = m_SourceCode;就不用这句了
                            break;
                        }
                    }
                    if (notChangedAgain)
                    {
                        notAnalyzedCount2 = this.m_SourceCodeWatchdog.NotDealtCount();
                        if (notAnalyzedCount == notAnalyzedCount2)//源代码没有发生新的改变
                        {
                            this.SetOutputTokenList(tokens);
                            var syntaxTree = this.GetSyntaxParser().Parse();
                            this.SetOutputSyntaxTree(syntaxTree);
                            m_SourceCodeWatchdog.Decrease(notAnalyzedCount);
#if DEBUG
                            Console.WriteLine("{0}: SourceCodeTextBox, Do_Compile analyzation done , {1}", DateTime.Now, this.ToString());
#endif
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 给此源代码文本框添加一个词法分析文本框
        /// </summary>
        /// <param name="tokenListViewer"></param>
        public void AddTokenListViewer(ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer)
        {
            this.m_TokenListViewerCollection.Add(tokenListViewer);
            tokenListViewer.SetSourceCodeViewer(this);
        }
        /// <summary>
        /// 给此源代码文本框去掉一个词法分析文本框
        /// </summary>
        /// <param name="tokenListViewer"></param>
        public void RemoveTokenListViewer(ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer)
        {
            if (this.m_TokenListViewerCollection.Contains(tokenListViewer))
            {
                tokenListViewer.SetSourceCodeViewer(null);
                this.m_TokenListViewerCollection.Remove(tokenListViewer);
            }
        }
        /// <summary>
        /// 判定此源代码文本框是否绑定了给定的单词列表控件
        /// </summary>
        /// <param name="tokenListViewer"></param>
        /// <returns></returns>
        public bool Contains(ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer)
        {
            return this.m_TokenListViewerCollection.Contains(tokenListViewer);
        }
        /// <summary>
        /// 给此源代码文本框添加一个词法分析文本框
        /// </summary>
        /// <param name="syntaxTreeViewer"></param>
        public void AddSyntaxTreeViewer(ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> syntaxTreeViewer)
        {
            this.m_SyntaxTreeViewerCollection.Add(syntaxTreeViewer);
            syntaxTreeViewer.SetSourceCodeViewer(this);
        }
        /// <summary>
        /// 给此源代码文本框去掉一个词法分析文本框
        /// </summary>
        /// <param name="SyntaxTreeViewer"></param>
        public void RemoveSyntaxTreeViewer(ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> SyntaxTreeViewer)
        {
            if (this.m_SyntaxTreeViewerCollection.Contains(SyntaxTreeViewer))
            {
                SyntaxTreeViewer.SetSourceCodeViewer(null);
                this.m_SyntaxTreeViewerCollection.Remove(SyntaxTreeViewer);
            }
        }
        /// <summary>
        /// 判定此源代码控件是否绑定了给定的语法树控件
        /// </summary>
        /// <param name="SyntaxTreeViewer"></param>
        /// <returns></returns>
        public bool Contains(ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> SyntaxTreeViewer)
        {
            return this.m_SyntaxTreeViewerCollection.Contains(SyntaxTreeViewer);
        }

        #region 属性和字段

        /// <summary>
        /// 要更新而未完成更新的选中长度
        /// <para>此数值与SelectionLength属性表示同一物理意义</para>
        /// </summary>
        private int m_UpadatingLength = 0;
        /// <summary>
        /// 要更新而未完成更新的光标所在位置
        /// <para>此数值与SelectionStart属性表示同一物理意义</para>
        /// </summary>
        private int m_UpdatingIndex = -1;

        /// <summary>
        /// 上次选中长度
        /// <para>此数值与SelectionLength属性表示同一物理意义</para>
        /// </summary>
        private int m_UpdatedLength = 0;
        /// <summary>
        /// 上次光标所在位置
        /// <para>此数值与SelectionStart属性表示同一物理意义</para>
        /// </summary>
        private int m_UpdatedIndex = -1;

        /// <summary>
        /// 需要对本源代码文本框进行分析和单词定位的TokenList控件集合
        /// </summary>
        private Collection<ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>> m_TokenListViewerCollection
            = new Collection<ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>>();
        /// <summary>
        /// 需要对本源代码文本框进行分析和单词定位的TokenList控件集合
        /// </summary>
        private Collection<ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>> m_SyntaxTreeViewerCollection
            = new Collection<ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>>();

        private int m_SelectedTokenIndex = -1;
        private bool m_SelectedTokenIndexAtSpace = true;

        private Func<string> m_DelToString;

        /// <summary>
        /// 获取最新的源代码
        /// </summary>
        /// <returns></returns>
        public string GetSourceCode()
        {
            return m_SourceCode;
        }
        /// <summary>
        /// 设置源代码
        /// </summary>
        /// <param name="sourceCode"></param>
        public void SetSourceCode(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode))
                m_SourceCode = string.Empty;
            else
                m_SourceCode = sourceCode;
        }
        /// <summary>
        /// 获取最近一次完整的分析得到的单词列表
        /// </summary>
        /// <returns></returns>
        public TokenList<TEnumTokenType> GetOutputTokenList()
        {
            return m_OutputTokenList;
        }
        /// <summary>
        /// 设置（更新）单词列表
        /// </summary>
        /// <param name="tokenList"></param>
        public void SetOutputTokenList(TokenList<TEnumTokenType> tokenList)
        {
            if (tokenList == null) return;
            m_OutputTokenList = tokenList;
            this.GetSyntaxParser().SetTokenListSource(tokenList);
            if (this.m_TokenListViewerCollection != null)
                foreach (var v in this.m_TokenListViewerCollection)
                {
                    v.TokenListUpdated();
                }
        }
        /// <summary>
        /// 获取最近一次分析得到的语法树
        /// </summary>
        /// <returns></returns>
        public SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> GetOutputSyntaxTree()
        {
            return m_OutputSyntaxTree;
        }
        /// <summary>
        /// 设置（更新）语法树
        /// </summary>
        /// <param name="syntaxTree"></param>
        public void SetOutputSyntaxTree(SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> syntaxTree)
        {
            if (syntaxTree == null) return;
            m_OutputSyntaxTree = syntaxTree;
            if (this.m_SyntaxTreeViewerCollection != null)
                foreach (var v in this.m_SyntaxTreeViewerCollection)
                {
                    v.SyntaxTreeUpdated();
                }
        }


        /// <summary>
        /// 获取当前使用的词法分析器
        /// </summary>
        /// <returns></returns>
        public ILexicalAnalyzer<TEnumTokenType> GetLexicalAnalyzer()
        {
            //if (m_LexicalAnalyzer == null)
            //    m_LexicalAnalyzer = new LexicalAnalyzerBase();
            return m_LexicalAnalyzer;
        }
        /// <summary>
        /// 设置词法分析器
        /// <para>只有在分析线程完成分析工作且要赋的值不为null时才会更改此属性</para>
        /// </summary>
        /// <param name="lexicalAnalyzer"></param>
        public void SetLexicalAnalyzer(ILexicalAnalyzer<TEnumTokenType> lexicalAnalyzer)
        {
            if (lexicalAnalyzer != null && this.m_SourceCodeWatchdog.NotDealtCount() == 0)
                m_LexicalAnalyzer = lexicalAnalyzer;
        }

        /// <summary>
        /// 词法分析器
        /// </summary>
        private ILexicalAnalyzer<TEnumTokenType> m_LexicalAnalyzer =
            null;
        //new SmileWei.Compiler.LexicalAnalyzer.LexicalAnalyzerBase("");
        //new SmileWei.Compiler.LexicalAnalyzer.LexicalAnalyzerK();

        /// <summary>
        /// 获取当前使用的语法分析器
        /// </summary>
        /// <returns></returns>
        public ISyntaxParser<TEnumTokenType, TEnumVType, TTreeNodeValue> GetSyntaxParser()
        {
            //if (m_SyntaxParser == null)
            //    m_SyntaxParser = /*new SyntaxParserCG();*/
            return m_SyntaxParser;
        }
        /// <summary>
        /// 设置语法分析器
        /// <para>只有在分析线程完成分析工作且要赋的值不为null时才会更改此属性</para>
        /// </summary>
        /// <param name="syntaxParser"></param>
        public void SetSyntaxParser(ISyntaxParser<TEnumTokenType, TEnumVType, TTreeNodeValue> syntaxParser)
        {
            if (syntaxParser != null && this.m_SourceCodeWatchdog.NotDealtCount() == 0)
                m_SyntaxParser = syntaxParser;
        }

        private ISyntaxParser<TEnumTokenType, TEnumVType, TTreeNodeValue> m_SyntaxParser = null;
        //new SyntaxParserBase();
        /// <summary>
        /// 源代码监视器，记录源代码的更改、分析次数
        /// </summary>
        private ChangeWatchdog m_SourceCodeWatchdog = new ChangeWatchdog();

        /// <summary>
        /// 最新的源代码在此处
        /// </summary>
        private string m_SourceCode;

        private TokenList<TEnumTokenType> m_OutputTokenList = new TokenList<TEnumTokenType>();

        private SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> m_OutputSyntaxTree = new SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue>();

        /// <summary>
        /// 词法分析、语法分析所在的线程
        /// </summary>
        private Thread m_CompilerThread;


        //private static Font m_Font = new Font("Lucida Console", 12);
        //private static Brush m_Brush = new SolidBrush(Color.Gray);
        private EventHandler appIdleEvent;

        #endregion 属性和字段

    }
}
