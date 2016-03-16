using System;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.ExpressionCompiler
{
    /// <summary>
    /// Expression的LL1语法分析器
    /// </summary>
    public partial class LL1SyntaxParserExpression : LL1SyntaxParserBase<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
    {
        #region 分析表中的元素
        
        /// <summary>
        /// 对 &lt;Expression&gt; ::= number... 进行分析
        /// <para>&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_Expression___numberLeave;
        /// <summary>
        /// 对 &lt;Expression&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_Expression___tail_leftParentheses_Leave;
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;+&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= &quot;+&quot; &lt;Multiply&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_PlusOpt___tail_plus_Leave;
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;-&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= &quot;-&quot; &lt;Multiply&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_PlusOpt___tail_minus_Leave;
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;)&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_PlusOpt___tail_rightParentheses_Leave;
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= #... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_PlusOpt___tail_startEndLeave;
        /// <summary>
        /// 对 &lt;Multiply&gt; ::= number... 进行分析
        /// <para>&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_Multiply___numberLeave;
        /// <summary>
        /// 对 &lt;Multiply&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_Multiply___tail_leftParentheses_Leave;
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;+&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_MultiplyOpt___tail_plus_Leave;
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;-&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_MultiplyOpt___tail_minus_Leave;
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;*&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= &quot;*&quot; &lt;Unit&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_MultiplyOpt___tail_multiply_Leave;
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;/&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= &quot;/&quot; &lt;Unit&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_MultiplyOpt___tail_divide_Leave;
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;)&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_MultiplyOpt___tail_rightParentheses_Leave;
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= #... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_MultiplyOpt___tail_startEndLeave;
        /// <summary>
        /// 对 &lt;Unit&gt; ::= number... 进行分析
        /// <para>&lt;Unit&gt; ::= number;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_Unit___numberLeave;
        /// <summary>
        /// 对 &lt;Unit&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Unit&gt; ::= &quot;(&quot; &lt;Expression&gt; &quot;)&quot;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsecase_Unit___tail_leftParentheses_Leave;
        
        /// <summary>
        /// 对 叶结点&quot;+&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsetail_plus_Leave_;
        /// <summary>
        /// 对 叶结点&quot;-&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsetail_minus_Leave_;
        /// <summary>
        /// 对 叶结点null 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParseepsilonLeave_;
        /// <summary>
        /// 对 叶结点&quot;*&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsetail_multiply_Leave_;
        /// <summary>
        /// 对 叶结点&quot;/&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsetail_divide_Leave_;
        /// <summary>
        /// 对 叶结点number 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsenumberLeave_;
        /// <summary>
        /// 对 叶结点&quot;(&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsetail_leftParentheses_Leave_;
        /// <summary>
        /// 对 叶结点&quot;)&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsetail_rightParentheses_Leave_;
        /// <summary>
        /// 对 叶结点# 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            FuncParsetail_startEndLeave_;
        
        #endregion 分析表中的元素
        #region 用于分析栈操作的字段-终结点
        
        private static readonly EnumVTypeExpression m_tail_plus_Leave = EnumVTypeExpression.tail_plus_Leave;
        private static readonly EnumVTypeExpression m_tail_minus_Leave = EnumVTypeExpression.tail_minus_Leave;
        private static readonly EnumVTypeExpression m_epsilonLeave = EnumVTypeExpression.epsilonLeave;
        private static readonly EnumVTypeExpression m_tail_multiply_Leave = EnumVTypeExpression.tail_multiply_Leave;
        private static readonly EnumVTypeExpression m_tail_divide_Leave = EnumVTypeExpression.tail_divide_Leave;
        private static readonly EnumVTypeExpression m_numberLeave = EnumVTypeExpression.numberLeave;
        private static readonly EnumVTypeExpression m_tail_leftParentheses_Leave = EnumVTypeExpression.tail_leftParentheses_Leave;
        private static readonly EnumVTypeExpression m_tail_rightParentheses_Leave = EnumVTypeExpression.tail_rightParentheses_Leave;
        private static readonly EnumVTypeExpression m_tail_startEndLeave = EnumVTypeExpression.tail_startEndLeave;
        
        #endregion 用于分析栈操作的字段-终结点
        #region 用于分析栈操作的字段-非终结点
        
        private static readonly EnumVTypeExpression m_case_Expression = EnumVTypeExpression.case_Expression;
        private static readonly EnumVTypeExpression m_case_PlusOpt = EnumVTypeExpression.case_PlusOpt;
        private static readonly EnumVTypeExpression m_case_Multiply = EnumVTypeExpression.case_Multiply;
        private static readonly EnumVTypeExpression m_case_MultiplyOpt = EnumVTypeExpression.case_MultiplyOpt;
        private static readonly EnumVTypeExpression m_case_Unit = EnumVTypeExpression.case_Unit;
        
        #endregion 用于分析栈操作的字段-非终结点
        #region 获取分析表中的元素
        
        /// <summary>
        /// 对 &lt;Expression&gt; ::= number... 进行分析
        /// <para>&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_Expression___numberLeave()
        {
            return FuncParsecase_Expression___numberLeave;
        }
        /// <summary>
        /// 对 &lt;Expression&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_Expression___tail_leftParentheses_Leave()
        {
            return FuncParsecase_Expression___tail_leftParentheses_Leave;
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;+&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= &quot;+&quot; &lt;Multiply&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_PlusOpt___tail_plus_Leave()
        {
            return FuncParsecase_PlusOpt___tail_plus_Leave;
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;-&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= &quot;-&quot; &lt;Multiply&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_PlusOpt___tail_minus_Leave()
        {
            return FuncParsecase_PlusOpt___tail_minus_Leave;
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;)&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_PlusOpt___tail_rightParentheses_Leave()
        {
            return FuncParsecase_PlusOpt___tail_rightParentheses_Leave;
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= #... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_PlusOpt___tail_startEndLeave()
        {
            return FuncParsecase_PlusOpt___tail_startEndLeave;
        }
        /// <summary>
        /// 对 &lt;Multiply&gt; ::= number... 进行分析
        /// <para>&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_Multiply___numberLeave()
        {
            return FuncParsecase_Multiply___numberLeave;
        }
        /// <summary>
        /// 对 &lt;Multiply&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_Multiply___tail_leftParentheses_Leave()
        {
            return FuncParsecase_Multiply___tail_leftParentheses_Leave;
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;+&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_MultiplyOpt___tail_plus_Leave()
        {
            return FuncParsecase_MultiplyOpt___tail_plus_Leave;
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;-&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_MultiplyOpt___tail_minus_Leave()
        {
            return FuncParsecase_MultiplyOpt___tail_minus_Leave;
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;*&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= &quot;*&quot; &lt;Unit&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_MultiplyOpt___tail_multiply_Leave()
        {
            return FuncParsecase_MultiplyOpt___tail_multiply_Leave;
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;/&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= &quot;/&quot; &lt;Unit&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_MultiplyOpt___tail_divide_Leave()
        {
            return FuncParsecase_MultiplyOpt___tail_divide_Leave;
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;)&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_MultiplyOpt___tail_rightParentheses_Leave()
        {
            return FuncParsecase_MultiplyOpt___tail_rightParentheses_Leave;
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= #... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_MultiplyOpt___tail_startEndLeave()
        {
            return FuncParsecase_MultiplyOpt___tail_startEndLeave;
        }
        /// <summary>
        /// 对 &lt;Unit&gt; ::= number... 进行分析
        /// <para>&lt;Unit&gt; ::= number;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_Unit___numberLeave()
        {
            return FuncParsecase_Unit___numberLeave;
        }
        /// <summary>
        /// 对 &lt;Unit&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Unit&gt; ::= &quot;(&quot; &lt;Expression&gt; &quot;)&quot;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsecase_Unit___tail_leftParentheses_Leave()
        {
            return FuncParsecase_Unit___tail_leftParentheses_Leave;
        }
        
        /// <summary>
        /// 对 叶结点&quot;+&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsetail_plus_Leave_()
        {
            return FuncParsetail_plus_Leave_;
        }
        /// <summary>
        /// 对 叶结点&quot;-&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsetail_minus_Leave_()
        {
            return FuncParsetail_minus_Leave_;
        }
        /// <summary>
        /// 对 叶结点null 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParseepsilonLeave_()
        {
            return FuncParseepsilonLeave_;
        }
        /// <summary>
        /// 对 叶结点&quot;*&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsetail_multiply_Leave_()
        {
            return FuncParsetail_multiply_Leave_;
        }
        /// <summary>
        /// 对 叶结点&quot;/&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsetail_divide_Leave_()
        {
            return FuncParsetail_divide_Leave_;
        }
        /// <summary>
        /// 对 叶结点number 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsenumberLeave_()
        {
            return FuncParsenumberLeave_;
        }
        /// <summary>
        /// 对 叶结点&quot;(&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsetail_leftParentheses_Leave_()
        {
            return FuncParsetail_leftParentheses_Leave_;
        }
        /// <summary>
        /// 对 叶结点&quot;)&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsetail_rightParentheses_Leave_()
        {
            return FuncParsetail_rightParentheses_Leave_;
        }
        /// <summary>
        /// 对 叶结点# 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            GetFuncParsetail_startEndLeave_()
        {
            return FuncParsetail_startEndLeave_;
        }
        
        #endregion 获取分析表中的元素
        #region 分析表中的元素指向的分析函数
        
        /// <summary>
        /// 对 &lt;Expression&gt; ::= number... 进行分析
        /// <para>&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_Expression___numberLeave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <Expression> ::= <Multiply> <PlusOpt>;
            return Derivationcase_Expression___case_Multiplycase_PlusOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;Expression&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_Expression___tail_leftParentheses_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <Expression> ::= <Multiply> <PlusOpt>;
            return Derivationcase_Expression___case_Multiplycase_PlusOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;+&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= &quot;+&quot; &lt;Multiply&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_PlusOpt___tail_plus_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <PlusOpt> ::= "+" <Multiply>;
            return Derivationcase_PlusOpt___tail_plus_Leavecase_Multiply(result, parser);
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;-&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= &quot;-&quot; &lt;Multiply&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_PlusOpt___tail_minus_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <PlusOpt> ::= "-" <Multiply>;
            return Derivationcase_PlusOpt___tail_minus_Leavecase_Multiply(result, parser);
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= &quot;)&quot;... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_PlusOpt___tail_rightParentheses_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <PlusOpt> ::= null;
            return Derivationcase_PlusOpt___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;PlusOpt&gt; ::= #... 进行分析
        /// <para>&lt;PlusOpt&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_PlusOpt___tail_startEndLeave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <PlusOpt> ::= null;
            return Derivationcase_PlusOpt___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Multiply&gt; ::= number... 进行分析
        /// <para>&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_Multiply___numberLeave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <Multiply> ::= <Unit> <MultiplyOpt>;
            return Derivationcase_Multiply___case_Unitcase_MultiplyOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;Multiply&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_Multiply___tail_leftParentheses_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <Multiply> ::= <Unit> <MultiplyOpt>;
            return Derivationcase_Multiply___case_Unitcase_MultiplyOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;+&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_MultiplyOpt___tail_plus_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <MultiplyOpt> ::= null;
            return Derivationcase_MultiplyOpt___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;-&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_MultiplyOpt___tail_minus_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <MultiplyOpt> ::= null;
            return Derivationcase_MultiplyOpt___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;*&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= &quot;*&quot; &lt;Unit&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_MultiplyOpt___tail_multiply_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <MultiplyOpt> ::= "*" <Unit>;
            return Derivationcase_MultiplyOpt___tail_multiply_Leavecase_Unit(result, parser);
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;/&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= &quot;/&quot; &lt;Unit&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_MultiplyOpt___tail_divide_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <MultiplyOpt> ::= "/" <Unit>;
            return Derivationcase_MultiplyOpt___tail_divide_Leavecase_Unit(result, parser);
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= &quot;)&quot;... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_MultiplyOpt___tail_rightParentheses_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <MultiplyOpt> ::= null;
            return Derivationcase_MultiplyOpt___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;MultiplyOpt&gt; ::= #... 进行分析
        /// <para>&lt;MultiplyOpt&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_MultiplyOpt___tail_startEndLeave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <MultiplyOpt> ::= null;
            return Derivationcase_MultiplyOpt___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Unit&gt; ::= number... 进行分析
        /// <para>&lt;Unit&gt; ::= number;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_Unit___numberLeave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <Unit> ::= number;
            return Derivationcase_Unit___numberLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Unit&gt; ::= &quot;(&quot;... 进行分析
        /// <para>&lt;Unit&gt; ::= &quot;(&quot; &lt;Expression&gt; &quot;)&quot;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsecase_Unit___tail_leftParentheses_Leave(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            // <Unit> ::= "(" <Expression> ")";
            return Derivationcase_Unit___tail_leftParentheses_Leavecase_Expressiontail_rightParentheses_Leave(result, parser);
        }
        
        /// <summary>
        /// 对 叶结点&quot;+&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsetail_plus_Leave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.tail_plus_Leave;
            result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点&quot;-&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsetail_minus_Leave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.tail_minus_Leave;
            result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点null 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            ParseepsilonLeave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.epsilonLeave;
            result.NodeValue.NodeName = @"null";
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            result.MappedTokenLength = 0;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点&quot;*&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsetail_multiply_Leave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.tail_multiply_Leave;
            result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点&quot;/&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsetail_divide_Leave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.tail_divide_Leave;
            result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点number 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            ParsenumberLeave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.numberLeave;
            result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点&quot;(&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsetail_leftParentheses_Leave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.tail_leftParentheses_Leave;
            result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点&quot;)&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsetail_rightParentheses_Leave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.tail_rightParentheses_Leave;
            result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserExpression.m_ParserStack.Pop();
            return Next(result, parserExpression);
        }
        /// <summary>
        /// 对 叶结点# 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Parsetail_startEndLeave_(SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result, ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {
            var parserExpression = parser as LL1SyntaxParserExpression;
            if (result != null)
            {
                result.NodeValue.NodeType = EnumVTypeExpression.tail_startEndLeave;
                result.NodeValue.NodeName = parserExpression.m_TokenListSource[parserExpression.m_ptNextToken].Detail;
                result.MappedTotalTokenList = parserExpression.m_TokenListSource;
                result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
                result.MappedTokenLength = 1;
            }
            parserExpression.m_ParserStack.Pop();
            parserExpression.m_ptNextToken++;
            return Next(result, parserExpression);
        }
        
        #endregion 分析表中的元素指向的分析函数
        #region 所有推导式的推导动作函数
        
        /// <summary>
        /// &lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_Expression___case_Multiplycase_PlusOpt(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<Expression> ::= <Multiply> <PlusOpt>;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_Expression;
            result.NodeValue.NodeName = EnumVTypeExpression.case_Expression.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_case_PlusOpt);
            parserExpression.m_ParserStack.Push(m_case_Multiply);
            // generate syntax tree
            var case_MultiplyTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_MultiplyTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_MultiplyTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_MultiplyTree0.Parent = result;
            //case_MultiplyTree0.Value = new ProductionNode(EnumVTypeExpression.case_Multiply);
            result.Children.Add(case_MultiplyTree0);
            var case_PlusOptTree1 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_PlusOptTree1.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_PlusOptTree1.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_PlusOptTree1.Parent = result;
            //case_PlusOptTree1.Value = new ProductionNode(EnumVTypeExpression.case_PlusOpt);
            result.Children.Add(case_PlusOptTree1);
            return case_MultiplyTree0;
        }//<Expression> ::= <Multiply> <PlusOpt>;
        /// <summary>
        /// &lt;PlusOpt&gt; ::= &quot;+&quot; &lt;Multiply&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_PlusOpt___tail_plus_Leavecase_Multiply(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<PlusOpt> ::= "+" <Multiply>;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_PlusOpt;
            result.NodeValue.NodeName = EnumVTypeExpression.case_PlusOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_case_Multiply);
            parserExpression.m_ParserStack.Push(m_tail_plus_Leave);
            // generate syntax tree
            var tail_plus_LeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            tail_plus_LeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            tail_plus_LeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            tail_plus_LeaveTree0.Parent = result;
            //tail_plus_LeaveTree0.Value = new ProductionNode(EnumVTypeExpression.tail_plus_Leave);
            result.Children.Add(tail_plus_LeaveTree0);
            var case_MultiplyTree1 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_MultiplyTree1.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_MultiplyTree1.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_MultiplyTree1.Parent = result;
            //case_MultiplyTree1.Value = new ProductionNode(EnumVTypeExpression.case_Multiply);
            result.Children.Add(case_MultiplyTree1);
            return tail_plus_LeaveTree0;
        }//<PlusOpt> ::= "+" <Multiply>;
        /// <summary>
        /// &lt;PlusOpt&gt; ::= &quot;-&quot; &lt;Multiply&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_PlusOpt___tail_minus_Leavecase_Multiply(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<PlusOpt> ::= "-" <Multiply>;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_PlusOpt;
            result.NodeValue.NodeName = EnumVTypeExpression.case_PlusOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_case_Multiply);
            parserExpression.m_ParserStack.Push(m_tail_minus_Leave);
            // generate syntax tree
            var tail_minus_LeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            tail_minus_LeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            tail_minus_LeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            tail_minus_LeaveTree0.Parent = result;
            //tail_minus_LeaveTree0.Value = new ProductionNode(EnumVTypeExpression.tail_minus_Leave);
            result.Children.Add(tail_minus_LeaveTree0);
            var case_MultiplyTree1 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_MultiplyTree1.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_MultiplyTree1.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_MultiplyTree1.Parent = result;
            //case_MultiplyTree1.Value = new ProductionNode(EnumVTypeExpression.case_Multiply);
            result.Children.Add(case_MultiplyTree1);
            return tail_minus_LeaveTree0;
        }//<PlusOpt> ::= "-" <Multiply>;
        /// <summary>
        /// &lt;PlusOpt&gt; ::= null;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_PlusOpt___epsilonLeave(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<PlusOpt> ::= null;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_PlusOpt;
            result.NodeValue.NodeName = EnumVTypeExpression.case_PlusOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_epsilonLeave);
            // generate syntax tree
            var epsilonLeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            epsilonLeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            epsilonLeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            epsilonLeaveTree0.Parent = result;
            //epsilonLeaveTree0.Value = new ProductionNode(EnumVTypeExpression.epsilonLeave);
            result.Children.Add(epsilonLeaveTree0);
            return epsilonLeaveTree0;
        }//<PlusOpt> ::= null;
        /// <summary>
        /// &lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_Multiply___case_Unitcase_MultiplyOpt(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<Multiply> ::= <Unit> <MultiplyOpt>;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_Multiply;
            result.NodeValue.NodeName = EnumVTypeExpression.case_Multiply.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_case_MultiplyOpt);
            parserExpression.m_ParserStack.Push(m_case_Unit);
            // generate syntax tree
            var case_UnitTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_UnitTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_UnitTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_UnitTree0.Parent = result;
            //case_UnitTree0.Value = new ProductionNode(EnumVTypeExpression.case_Unit);
            result.Children.Add(case_UnitTree0);
            var case_MultiplyOptTree1 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_MultiplyOptTree1.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_MultiplyOptTree1.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_MultiplyOptTree1.Parent = result;
            //case_MultiplyOptTree1.Value = new ProductionNode(EnumVTypeExpression.case_MultiplyOpt);
            result.Children.Add(case_MultiplyOptTree1);
            return case_UnitTree0;
        }//<Multiply> ::= <Unit> <MultiplyOpt>;
        /// <summary>
        /// &lt;MultiplyOpt&gt; ::= &quot;*&quot; &lt;Unit&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_MultiplyOpt___tail_multiply_Leavecase_Unit(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<MultiplyOpt> ::= "*" <Unit>;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_MultiplyOpt;
            result.NodeValue.NodeName = EnumVTypeExpression.case_MultiplyOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_case_Unit);
            parserExpression.m_ParserStack.Push(m_tail_multiply_Leave);
            // generate syntax tree
            var tail_multiply_LeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            tail_multiply_LeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            tail_multiply_LeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            tail_multiply_LeaveTree0.Parent = result;
            //tail_multiply_LeaveTree0.Value = new ProductionNode(EnumVTypeExpression.tail_multiply_Leave);
            result.Children.Add(tail_multiply_LeaveTree0);
            var case_UnitTree1 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_UnitTree1.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_UnitTree1.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_UnitTree1.Parent = result;
            //case_UnitTree1.Value = new ProductionNode(EnumVTypeExpression.case_Unit);
            result.Children.Add(case_UnitTree1);
            return tail_multiply_LeaveTree0;
        }//<MultiplyOpt> ::= "*" <Unit>;
        /// <summary>
        /// &lt;MultiplyOpt&gt; ::= &quot;/&quot; &lt;Unit&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_MultiplyOpt___tail_divide_Leavecase_Unit(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<MultiplyOpt> ::= "/" <Unit>;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_MultiplyOpt;
            result.NodeValue.NodeName = EnumVTypeExpression.case_MultiplyOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_case_Unit);
            parserExpression.m_ParserStack.Push(m_tail_divide_Leave);
            // generate syntax tree
            var tail_divide_LeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            tail_divide_LeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            tail_divide_LeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            tail_divide_LeaveTree0.Parent = result;
            //tail_divide_LeaveTree0.Value = new ProductionNode(EnumVTypeExpression.tail_divide_Leave);
            result.Children.Add(tail_divide_LeaveTree0);
            var case_UnitTree1 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_UnitTree1.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_UnitTree1.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_UnitTree1.Parent = result;
            //case_UnitTree1.Value = new ProductionNode(EnumVTypeExpression.case_Unit);
            result.Children.Add(case_UnitTree1);
            return tail_divide_LeaveTree0;
        }//<MultiplyOpt> ::= "/" <Unit>;
        /// <summary>
        /// &lt;MultiplyOpt&gt; ::= null;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_MultiplyOpt___epsilonLeave(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<MultiplyOpt> ::= null;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_MultiplyOpt;
            result.NodeValue.NodeName = EnumVTypeExpression.case_MultiplyOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_epsilonLeave);
            // generate syntax tree
            var epsilonLeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            epsilonLeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            epsilonLeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            epsilonLeaveTree0.Parent = result;
            //epsilonLeaveTree0.Value = new ProductionNode(EnumVTypeExpression.epsilonLeave);
            result.Children.Add(epsilonLeaveTree0);
            return epsilonLeaveTree0;
        }//<MultiplyOpt> ::= null;
        /// <summary>
        /// &lt;Unit&gt; ::= number;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_Unit___numberLeave(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<Unit> ::= number;
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_Unit;
            result.NodeValue.NodeName = EnumVTypeExpression.case_Unit.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_numberLeave);
            // generate syntax tree
            var numberLeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            numberLeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            numberLeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            numberLeaveTree0.Parent = result;
            //numberLeaveTree0.Value = new ProductionNode(EnumVTypeExpression.numberLeave);
            result.Children.Add(numberLeaveTree0);
            return numberLeaveTree0;
        }//<Unit> ::= number;
        /// <summary>
        /// &lt;Unit&gt; ::= &quot;(&quot; &lt;Expression&gt; &quot;)&quot;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>
            Derivationcase_Unit___tail_leftParentheses_Leavecase_Expressiontail_rightParentheses_Leave(
            SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> result,
            ISyntaxParser<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression> parser)
        {//<Unit> ::= "(" <Expression> ")";
            var parserExpression = parser as LL1SyntaxParserExpression;
            result.NodeValue.NodeType = EnumVTypeExpression.case_Unit;
            result.NodeValue.NodeName = EnumVTypeExpression.case_Unit.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserExpression.m_TokenListSource;
            result.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            parserExpression.m_ParserStack.Pop();
            // right-to-left push
            parserExpression.m_ParserStack.Push(m_tail_rightParentheses_Leave);
            parserExpression.m_ParserStack.Push(m_case_Expression);
            parserExpression.m_ParserStack.Push(m_tail_leftParentheses_Leave);
            // generate syntax tree
            var tail_leftParentheses_LeaveTree0 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            tail_leftParentheses_LeaveTree0.MappedTotalTokenList = parserExpression.m_TokenListSource;
            tail_leftParentheses_LeaveTree0.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            tail_leftParentheses_LeaveTree0.Parent = result;
            //tail_leftParentheses_LeaveTree0.Value = new ProductionNode(EnumVTypeExpression.tail_leftParentheses_Leave);
            result.Children.Add(tail_leftParentheses_LeaveTree0);
            var case_ExpressionTree1 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            case_ExpressionTree1.MappedTotalTokenList = parserExpression.m_TokenListSource;
            case_ExpressionTree1.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            case_ExpressionTree1.Parent = result;
            //case_ExpressionTree1.Value = new ProductionNode(EnumVTypeExpression.case_Expression);
            result.Children.Add(case_ExpressionTree1);
            var tail_rightParentheses_LeaveTree2 = new SyntaxTree<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>();
            tail_rightParentheses_LeaveTree2.MappedTotalTokenList = parserExpression.m_TokenListSource;
            tail_rightParentheses_LeaveTree2.MappedTokenStartIndex = parserExpression.m_ptNextToken;
            tail_rightParentheses_LeaveTree2.Parent = result;
            //tail_rightParentheses_LeaveTree2.Value = new ProductionNode(EnumVTypeExpression.tail_rightParentheses_Leave);
            result.Children.Add(tail_rightParentheses_LeaveTree2);
            return tail_leftParentheses_LeaveTree0;
        }//<Unit> ::= "(" <Expression> ")";
        
        #endregion 所有推导式的推导动作函数
        #region FillMapCells()
        
        private void FillMapCells()
        {
            m_Map.SetCell(EnumVTypeExpression.case_Expression, EnumTokenTypeExpression.number, FuncParsecase_Expression___numberLeave);
            m_Map.SetCell(EnumVTypeExpression.case_Expression, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsecase_Expression___tail_leftParentheses_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_PlusOpt, EnumTokenTypeExpression.token_Plus_, FuncParsecase_PlusOpt___tail_plus_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_PlusOpt, EnumTokenTypeExpression.token_Minus_, FuncParsecase_PlusOpt___tail_minus_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_PlusOpt, EnumTokenTypeExpression.token_RightParentheses_, FuncParsecase_PlusOpt___tail_rightParentheses_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_PlusOpt, EnumTokenTypeExpression.token_startEnd, FuncParsecase_PlusOpt___tail_startEndLeave);
            m_Map.SetCell(EnumVTypeExpression.case_Multiply, EnumTokenTypeExpression.number, FuncParsecase_Multiply___numberLeave);
            m_Map.SetCell(EnumVTypeExpression.case_Multiply, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsecase_Multiply___tail_leftParentheses_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_MultiplyOpt, EnumTokenTypeExpression.token_Plus_, FuncParsecase_MultiplyOpt___tail_plus_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_MultiplyOpt, EnumTokenTypeExpression.token_Minus_, FuncParsecase_MultiplyOpt___tail_minus_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_MultiplyOpt, EnumTokenTypeExpression.token_Multiply_, FuncParsecase_MultiplyOpt___tail_multiply_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_MultiplyOpt, EnumTokenTypeExpression.token_Divide_, FuncParsecase_MultiplyOpt___tail_divide_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_MultiplyOpt, EnumTokenTypeExpression.token_RightParentheses_, FuncParsecase_MultiplyOpt___tail_rightParentheses_Leave);
            m_Map.SetCell(EnumVTypeExpression.case_MultiplyOpt, EnumTokenTypeExpression.token_startEnd, FuncParsecase_MultiplyOpt___tail_startEndLeave);
            m_Map.SetCell(EnumVTypeExpression.case_Unit, EnumTokenTypeExpression.number, FuncParsecase_Unit___numberLeave);
            m_Map.SetCell(EnumVTypeExpression.case_Unit, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsecase_Unit___tail_leftParentheses_Leave);
            
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.token_Plus_, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.token_Minus_, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.epsilon, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.token_Multiply_, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.token_Divide_, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.number, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsetail_plus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_plus_Leave, EnumTokenTypeExpression.token_startEnd, FuncParsetail_plus_Leave_);
            
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.token_Plus_, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.token_Minus_, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.epsilon, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.token_Multiply_, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.token_Divide_, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.number, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsetail_minus_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_minus_Leave, EnumTokenTypeExpression.token_startEnd, FuncParsetail_minus_Leave_);
            
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.token_Plus_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.token_Minus_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.epsilon, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.token_Multiply_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.token_Divide_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.number, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.token_RightParentheses_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeExpression.epsilonLeave, EnumTokenTypeExpression.token_startEnd, FuncParseepsilonLeave_);
            
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.token_Plus_, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.token_Minus_, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.epsilon, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.token_Multiply_, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.token_Divide_, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.number, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsetail_multiply_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_multiply_Leave, EnumTokenTypeExpression.token_startEnd, FuncParsetail_multiply_Leave_);
            
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.token_Plus_, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.token_Minus_, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.epsilon, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.token_Multiply_, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.token_Divide_, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.number, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsetail_divide_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_divide_Leave, EnumTokenTypeExpression.token_startEnd, FuncParsetail_divide_Leave_);
            
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.token_Plus_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.token_Minus_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.epsilon, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.token_Multiply_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.token_Divide_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.number, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeExpression.numberLeave, EnumTokenTypeExpression.token_startEnd, FuncParsenumberLeave_);
            
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.token_Plus_, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.token_Minus_, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.epsilon, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.token_Multiply_, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.token_Divide_, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.number, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsetail_leftParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_leftParentheses_Leave, EnumTokenTypeExpression.token_startEnd, FuncParsetail_leftParentheses_Leave_);
            
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.token_Plus_, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.token_Minus_, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.epsilon, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.token_Multiply_, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.token_Divide_, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.number, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsetail_rightParentheses_Leave_);
            m_Map.SetCell(EnumVTypeExpression.tail_rightParentheses_Leave, EnumTokenTypeExpression.token_startEnd, FuncParsetail_rightParentheses_Leave_);
            
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.token_Plus_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.token_Minus_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.epsilon, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.token_Multiply_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.token_Divide_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.number, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.token_LeftParentheses_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.token_RightParentheses_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeExpression.tail_startEndLeave, EnumTokenTypeExpression.token_startEnd, FuncParsetail_startEndLeave_);
        }
        
        #endregion FillMapCells()
        #region 为分析表中的元素配置分析函数
        
        private void InitFunc()
        {
            FuncParsecase_Expression___numberLeave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_Expression___numberLeave);
            FuncParsecase_Expression___tail_leftParentheses_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_Expression___tail_leftParentheses_Leave);
            FuncParsecase_PlusOpt___tail_plus_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_PlusOpt___tail_plus_Leave);
            FuncParsecase_PlusOpt___tail_minus_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_PlusOpt___tail_minus_Leave);
            FuncParsecase_PlusOpt___tail_rightParentheses_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_PlusOpt___tail_rightParentheses_Leave);
            FuncParsecase_PlusOpt___tail_startEndLeave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_PlusOpt___tail_startEndLeave);
            FuncParsecase_Multiply___numberLeave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_Multiply___numberLeave);
            FuncParsecase_Multiply___tail_leftParentheses_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_Multiply___tail_leftParentheses_Leave);
            FuncParsecase_MultiplyOpt___tail_plus_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_MultiplyOpt___tail_plus_Leave);
            FuncParsecase_MultiplyOpt___tail_minus_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_MultiplyOpt___tail_minus_Leave);
            FuncParsecase_MultiplyOpt___tail_multiply_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_MultiplyOpt___tail_multiply_Leave);
            FuncParsecase_MultiplyOpt___tail_divide_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_MultiplyOpt___tail_divide_Leave);
            FuncParsecase_MultiplyOpt___tail_rightParentheses_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_MultiplyOpt___tail_rightParentheses_Leave);
            FuncParsecase_MultiplyOpt___tail_startEndLeave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_MultiplyOpt___tail_startEndLeave);
            FuncParsecase_Unit___numberLeave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_Unit___numberLeave);
            FuncParsecase_Unit___tail_leftParentheses_Leave = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsecase_Unit___tail_leftParentheses_Leave);
            
            FuncParsetail_plus_Leave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsetail_plus_Leave_);
            FuncParsetail_minus_Leave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsetail_minus_Leave_);
            FuncParseepsilonLeave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(ParseepsilonLeave_);
            FuncParsetail_multiply_Leave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsetail_multiply_Leave_);
            FuncParsetail_divide_Leave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsetail_divide_Leave_);
            FuncParsenumberLeave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(ParsenumberLeave_);
            FuncParsetail_leftParentheses_Leave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsetail_leftParentheses_Leave_);
            FuncParsetail_rightParentheses_Leave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsetail_rightParentheses_Leave_);
            FuncParsetail_startEndLeave_ = 
                new CandidateFunction<EnumTokenTypeExpression, EnumVTypeExpression, TreeNodeValueExpression>(Parsetail_startEndLeave_);
        }
        
        #endregion 为分析表中的元素配置分析函数
        /// <summary>
        /// 初始化LL(1)分析表
        /// </summary>
        public override void InitMap()
        {
            InitFunc();
            // init parser map
            SetMapLinesAndColumns();
            FillMapCells();
        }
        /// <summary>
        /// LL1SyntaxParserExpression的语法分析器
        /// </summary>
        public LL1SyntaxParserExpression()
            : base(14, 9) { }
        /// LL1SyntaxParserExpression的语法分析器
        /// </summary>
        /// <param name="tokens">要分析的单词列表</param>
        public LL1SyntaxParserExpression(TokenList<EnumTokenTypeExpression> tokens)
            : base(14, 9)
        {
            m_TokenListSource = tokens;
        }
        #region 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析
        
        /// <summary>
        /// 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析
        /// </summary>
        public override void Reset()
        {
            m_ptNextToken = 0;
            m_ParserStack.Clear();
            m_ParserStack.Push(m_tail_startEndLeave);
            m_ParserStack.Push(m_case_Expression);
            if (m_TokenListSource.Count == 0)
            {
                var newToken = new Token<EnumTokenTypeExpression>()
                {
                    Detail = "#",
                    Line = 0,
                    Column = 0,
                    IndexOfSourceCode = 0,
                    Length = 1,
                    LexicalError = false,
                    TokenType = EnumTokenTypeExpression.token_startEnd
                };
                m_TokenListSource.Add(newToken);
            }
            else
            {
                var token = m_TokenListSource[m_TokenListSource.Count - 1];
                {
                    var newToken = new Token<EnumTokenTypeExpression>()
                    {
                        Detail = "#",
                        Line = token.Line,
                        Column = token.Column + token.Length + 1,
                        IndexOfSourceCode = token.IndexOfSourceCode + token.Length + 1,
                        Length = 1,
                        LexicalError = false,
                        TokenType = EnumTokenTypeExpression.token_startEnd
                    };
                    m_TokenListSource.Add(newToken);
                }
            }
        }
        
        #endregion 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析
        #region SetMapLinesAndColumns()
        
        private void SetMapLinesAndColumns()
        {
            m_Map.SetLine(0, EnumVTypeExpression.case_Expression);
            m_Map.SetLine(1, EnumVTypeExpression.case_PlusOpt);
            m_Map.SetLine(2, EnumVTypeExpression.case_Multiply);
            m_Map.SetLine(3, EnumVTypeExpression.case_MultiplyOpt);
            m_Map.SetLine(4, EnumVTypeExpression.case_Unit);
            
            m_Map.SetLine(5, EnumVTypeExpression.tail_plus_Leave);
            m_Map.SetLine(6, EnumVTypeExpression.tail_minus_Leave);
            m_Map.SetLine(7, EnumVTypeExpression.epsilonLeave);
            m_Map.SetLine(8, EnumVTypeExpression.tail_multiply_Leave);
            m_Map.SetLine(9, EnumVTypeExpression.tail_divide_Leave);
            m_Map.SetLine(10, EnumVTypeExpression.numberLeave);
            m_Map.SetLine(11, EnumVTypeExpression.tail_leftParentheses_Leave);
            m_Map.SetLine(12, EnumVTypeExpression.tail_rightParentheses_Leave);
            m_Map.SetLine(13, EnumVTypeExpression.tail_startEndLeave);
            
            
            m_Map.SetColumn(0, EnumTokenTypeExpression.token_Plus_);
            m_Map.SetColumn(1, EnumTokenTypeExpression.token_Minus_);
            m_Map.SetColumn(2, EnumTokenTypeExpression.epsilon);
            m_Map.SetColumn(3, EnumTokenTypeExpression.token_Multiply_);
            m_Map.SetColumn(4, EnumTokenTypeExpression.token_Divide_);
            m_Map.SetColumn(5, EnumTokenTypeExpression.number);
            m_Map.SetColumn(6, EnumTokenTypeExpression.token_LeftParentheses_);
            m_Map.SetColumn(7, EnumTokenTypeExpression.token_RightParentheses_);
            m_Map.SetColumn(8, EnumTokenTypeExpression.token_startEnd);
        }
        
        #endregion SetMapLinesAndColumns()
    }

}

