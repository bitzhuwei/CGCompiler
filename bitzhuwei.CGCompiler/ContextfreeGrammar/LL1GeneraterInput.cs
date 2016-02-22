using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// LL1文法自动生成器的输入数据
    /// </summary>
    public class LL1GeneraterInput
    {
        /// <summary>
        /// 已重写
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}"
                , this.m_TerminalList
                , this.m_NonTerminalList
                , this.m_firstCollectionInput
                , this.m_followCollectionInput
                , this.m_ll1ParserMapInput);
            //return base.ToString();
        }
        /// <summary>
        /// 用于生成编译器的必要输入数据
        /// <para>使用此类型可避免重复计算，以节省时间</para>
        /// </summary>
        /// <param name="grammar">要生成编译器的文法</param>
        public LL1GeneraterInput(ContextfreeGrammar grammar)
        {
            this.m_TerminalList = grammar.GetTerminalList();
            this.m_NonTerminalList = grammar.GetNonterminalList();
            this.m_firstCollectionInput = grammar.GetFirstCollection();
            this.m_followCollectionInput = grammar.GetFollowCollection();
            this.m_ll1ParserMapInput = grammar.GetLL1ParserMap();
        }

        private List<ProductionNode> m_TerminalList = new List<ProductionNode>();
        /// <summary>
        /// 终结点列表
        /// </summary>
        public List<ProductionNode> TerminalList
        {
            get
            {
                ProductionNode[] terminals = new ProductionNode[this.m_TerminalList.Count];
                this.m_TerminalList.CopyTo(terminals);
                var terminalList = new List<ProductionNode>(terminals);
                return terminalList;
            }
            //set { m_TerminalList = value; }
        }

        private List<ProductionNode> m_NonTerminalList = new List<ProductionNode>();

        /// <summary>
        /// 非终结点列表
        /// </summary>
        public List<ProductionNode> NonTerminalList
        {
            get
            {
                ProductionNode[] nonTerminals = new ProductionNode[this.m_NonTerminalList.Count];
                this.m_NonTerminalList.CopyTo(nonTerminals);
                var nonTerminalList = new List<ProductionNode>(nonTerminals);
                return nonTerminalList;
            }
            //set { m_NonTerminalList = value; }
        }

        private FIRSTCollection m_firstCollectionInput;
        /// <summary>
        /// FIRST集
        /// </summary>
        public FIRSTCollection FirstCollectionInput
        {
            get
            {
                FIRSTCollectionItem[] items = new FIRSTCollectionItem[this.m_firstCollectionInput.Count];
                this.m_firstCollectionInput.CopyTo(items);
                var result = new FIRSTCollection(this.m_firstCollectionInput.GetGrammar());
                result.AddRange(items);
                return result;
            }
            //set { m_firstCollectionInput = value; }
        }
        private FOLLOWCollection m_followCollectionInput;
        /// <summary>
        /// FOLLOW集
        /// </summary>
        public FOLLOWCollection FollowCollectionInput
        {
            get
            {
                FOLLOWCollectionItem[] items = new FOLLOWCollectionItem[this.m_followCollectionInput.Count];
                this.m_followCollectionInput.CopyTo(items);
                var result = new FOLLOWCollection(this.m_followCollectionInput.GetGrammar());
                result.AddRange(items);
                return result;
            }
            //set { m_followCollectionInput = value; }
        }
        private LL1ParserMap m_ll1ParserMapInput;
        /// <summary>
        /// LL1分析表
        /// </summary>
        public LL1ParserMap LL1ParserMapInput
        {
            get { return m_ll1ParserMapInput; }
            //set { m_ll1ParserMapInput = value; }
        }
    }
}
