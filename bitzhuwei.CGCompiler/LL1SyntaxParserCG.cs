using System;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// CG的LL1语法分析器
    /// </summary>
    public partial class LL1SyntaxParserCG : LL1SyntaxParserBase<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
    {
        #region 分析表中的元素
        
        /// <summary>
        /// 对 &lt;Start&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;Start&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Start___tail_lessThan_Leave;
        /// <summary>
        /// 对 &lt;PList&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;PList&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_PList___tail_lessThan_Leave;
        /// <summary>
        /// 对 &lt;PList&gt; ::= #... 进行分析
        /// <para>&lt;PList&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_PList___tail_startEndLeave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___tail_lessThan_Leave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= identifier... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___identifierLeave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___tail_nullLeave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___tail_identifierLeave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___tail_numberLeave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___tail_constStringLeave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= number... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___numberLeave;
        /// <summary>
        /// 对 &lt;VList&gt; ::= constString... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VList___constStringLeave;
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vn&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___tail_lessThan_Leave;
        /// <summary>
        /// 对 &lt;V&gt; ::= identifier... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___identifierLeave;
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___tail_nullLeave;
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___tail_identifierLeave;
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___tail_numberLeave;
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___tail_constStringLeave;
        /// <summary>
        /// 对 &lt;V&gt; ::= number... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___numberLeave;
        /// <summary>
        /// 对 &lt;V&gt; ::= constString... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_V___constStringLeave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;;&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= null;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___tail_semicolon_Leave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;|&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &quot;|&quot; &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___tail_or_Leave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___tail_lessThan_Leave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= identifier... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___identifierLeave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___tail_nullLeave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___tail_identifierLeave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___tail_numberLeave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___tail_constStringLeave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= number... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___numberLeave;
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= constString... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_VOpt___constStringLeave;
        /// <summary>
        /// 对 &lt;Vn&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;Vn&gt; ::= &quot;&lt;&quot; identifier &quot;&gt;&quot;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vn___tail_lessThan_Leave;
        /// <summary>
        /// 对 &lt;Vt&gt; ::= identifier... 进行分析
        /// <para>&lt;Vt&gt; ::= identifier;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vt___identifierLeave;
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;null&quot;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vt___tail_nullLeave;
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;identifier&quot;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vt___tail_identifierLeave;
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;number&quot;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vt___tail_numberLeave;
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;constString&quot;;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vt___tail_constStringLeave;
        /// <summary>
        /// 对 &lt;Vt&gt; ::= number... 进行分析
        /// <para>&lt;Vt&gt; ::= number;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vt___numberLeave;
        /// <summary>
        /// 对 &lt;Vt&gt; ::= constString... 进行分析
        /// <para>&lt;Vt&gt; ::= constString;</para>
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsecase_Vt___constStringLeave;
        
        /// <summary>
        /// 对 叶结点&quot;::=&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_colon_Colon_Equality_Leave_;
        /// <summary>
        /// 对 叶结点&quot;;&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_semicolon_Leave_;
        /// <summary>
        /// 对 叶结点null 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParseepsilonLeave_;
        /// <summary>
        /// 对 叶结点&quot;|&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_or_Leave_;
        /// <summary>
        /// 对 叶结点&quot;&lt;&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_lessThan_Leave_;
        /// <summary>
        /// 对 叶结点identifier 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParseidentifierLeave_;
        /// <summary>
        /// 对 叶结点&quot;&gt;&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_greaterThan_Leave_;
        /// <summary>
        /// 对 叶结点&quot;null&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_nullLeave_;
        /// <summary>
        /// 对 叶结点&quot;identifier&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_identifierLeave_;
        /// <summary>
        /// 对 叶结点&quot;number&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_numberLeave_;
        /// <summary>
        /// 对 叶结点&quot;constString&quot; 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_constStringLeave_;
        /// <summary>
        /// 对 叶结点number 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsenumberLeave_;
        /// <summary>
        /// 对 叶结点constString 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParseconstStringLeave_;
        /// <summary>
        /// 对 叶结点# 进行分析
        /// </summary>
        private static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            FuncParsetail_startEndLeave_;
        
        #endregion 分析表中的元素
        #region 用于分析栈操作的字段-终结点
        
        private static readonly EnumVTypeCG m_tail_colon_Colon_Equality_Leave = EnumVTypeCG.tail_colon_Colon_Equality_Leave;
        private static readonly EnumVTypeCG m_tail_semicolon_Leave = EnumVTypeCG.tail_semicolon_Leave;
        private static readonly EnumVTypeCG m_epsilonLeave = EnumVTypeCG.epsilonLeave;
        private static readonly EnumVTypeCG m_tail_or_Leave = EnumVTypeCG.tail_or_Leave;
        private static readonly EnumVTypeCG m_tail_lessThan_Leave = EnumVTypeCG.tail_lessThan_Leave;
        private static readonly EnumVTypeCG m_identifierLeave = EnumVTypeCG.identifierLeave;
        private static readonly EnumVTypeCG m_tail_greaterThan_Leave = EnumVTypeCG.tail_greaterThan_Leave;
        private static readonly EnumVTypeCG m_tail_nullLeave = EnumVTypeCG.tail_nullLeave;
        private static readonly EnumVTypeCG m_tail_identifierLeave = EnumVTypeCG.tail_identifierLeave;
        private static readonly EnumVTypeCG m_tail_numberLeave = EnumVTypeCG.tail_numberLeave;
        private static readonly EnumVTypeCG m_tail_constStringLeave = EnumVTypeCG.tail_constStringLeave;
        private static readonly EnumVTypeCG m_numberLeave = EnumVTypeCG.numberLeave;
        private static readonly EnumVTypeCG m_constStringLeave = EnumVTypeCG.constStringLeave;
        private static readonly EnumVTypeCG m_tail_startEndLeave = EnumVTypeCG.tail_startEndLeave;
        
        #endregion 用于分析栈操作的字段-终结点
        #region 用于分析栈操作的字段-非终结点
        
        private static readonly EnumVTypeCG m_case_Start = EnumVTypeCG.case_Start;
        private static readonly EnumVTypeCG m_case_PList = EnumVTypeCG.case_PList;
        private static readonly EnumVTypeCG m_case_VList = EnumVTypeCG.case_VList;
        private static readonly EnumVTypeCG m_case_V = EnumVTypeCG.case_V;
        private static readonly EnumVTypeCG m_case_VOpt = EnumVTypeCG.case_VOpt;
        private static readonly EnumVTypeCG m_case_Vn = EnumVTypeCG.case_Vn;
        private static readonly EnumVTypeCG m_case_Vt = EnumVTypeCG.case_Vt;
        
        #endregion 用于分析栈操作的字段-非终结点
        #region 获取分析表中的元素
        
        /// <summary>
        /// 对 &lt;Start&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;Start&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Start___tail_lessThan_Leave()
        {
            return FuncParsecase_Start___tail_lessThan_Leave;
        }
        /// <summary>
        /// 对 &lt;PList&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;PList&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_PList___tail_lessThan_Leave()
        {
            return FuncParsecase_PList___tail_lessThan_Leave;
        }
        /// <summary>
        /// 对 &lt;PList&gt; ::= #... 进行分析
        /// <para>&lt;PList&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_PList___tail_startEndLeave()
        {
            return FuncParsecase_PList___tail_startEndLeave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___tail_lessThan_Leave()
        {
            return FuncParsecase_VList___tail_lessThan_Leave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= identifier... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___identifierLeave()
        {
            return FuncParsecase_VList___identifierLeave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___tail_nullLeave()
        {
            return FuncParsecase_VList___tail_nullLeave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___tail_identifierLeave()
        {
            return FuncParsecase_VList___tail_identifierLeave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___tail_numberLeave()
        {
            return FuncParsecase_VList___tail_numberLeave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___tail_constStringLeave()
        {
            return FuncParsecase_VList___tail_constStringLeave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= number... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___numberLeave()
        {
            return FuncParsecase_VList___numberLeave;
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= constString... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VList___constStringLeave()
        {
            return FuncParsecase_VList___constStringLeave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vn&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___tail_lessThan_Leave()
        {
            return FuncParsecase_V___tail_lessThan_Leave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= identifier... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___identifierLeave()
        {
            return FuncParsecase_V___identifierLeave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___tail_nullLeave()
        {
            return FuncParsecase_V___tail_nullLeave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___tail_identifierLeave()
        {
            return FuncParsecase_V___tail_identifierLeave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___tail_numberLeave()
        {
            return FuncParsecase_V___tail_numberLeave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___tail_constStringLeave()
        {
            return FuncParsecase_V___tail_constStringLeave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= number... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___numberLeave()
        {
            return FuncParsecase_V___numberLeave;
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= constString... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_V___constStringLeave()
        {
            return FuncParsecase_V___constStringLeave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;;&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= null;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___tail_semicolon_Leave()
        {
            return FuncParsecase_VOpt___tail_semicolon_Leave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;|&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &quot;|&quot; &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___tail_or_Leave()
        {
            return FuncParsecase_VOpt___tail_or_Leave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___tail_lessThan_Leave()
        {
            return FuncParsecase_VOpt___tail_lessThan_Leave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= identifier... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___identifierLeave()
        {
            return FuncParsecase_VOpt___identifierLeave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___tail_nullLeave()
        {
            return FuncParsecase_VOpt___tail_nullLeave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___tail_identifierLeave()
        {
            return FuncParsecase_VOpt___tail_identifierLeave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___tail_numberLeave()
        {
            return FuncParsecase_VOpt___tail_numberLeave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___tail_constStringLeave()
        {
            return FuncParsecase_VOpt___tail_constStringLeave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= number... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___numberLeave()
        {
            return FuncParsecase_VOpt___numberLeave;
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= constString... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_VOpt___constStringLeave()
        {
            return FuncParsecase_VOpt___constStringLeave;
        }
        /// <summary>
        /// 对 &lt;Vn&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;Vn&gt; ::= &quot;&lt;&quot; identifier &quot;&gt;&quot;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vn___tail_lessThan_Leave()
        {
            return FuncParsecase_Vn___tail_lessThan_Leave;
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= identifier... 进行分析
        /// <para>&lt;Vt&gt; ::= identifier;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vt___identifierLeave()
        {
            return FuncParsecase_Vt___identifierLeave;
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;null&quot;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vt___tail_nullLeave()
        {
            return FuncParsecase_Vt___tail_nullLeave;
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;identifier&quot;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vt___tail_identifierLeave()
        {
            return FuncParsecase_Vt___tail_identifierLeave;
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;number&quot;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vt___tail_numberLeave()
        {
            return FuncParsecase_Vt___tail_numberLeave;
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;constString&quot;;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vt___tail_constStringLeave()
        {
            return FuncParsecase_Vt___tail_constStringLeave;
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= number... 进行分析
        /// <para>&lt;Vt&gt; ::= number;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vt___numberLeave()
        {
            return FuncParsecase_Vt___numberLeave;
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= constString... 进行分析
        /// <para>&lt;Vt&gt; ::= constString;</para>
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsecase_Vt___constStringLeave()
        {
            return FuncParsecase_Vt___constStringLeave;
        }
        
        /// <summary>
        /// 对 叶结点&quot;::=&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_colon_Colon_Equality_Leave_()
        {
            return FuncParsetail_colon_Colon_Equality_Leave_;
        }
        /// <summary>
        /// 对 叶结点&quot;;&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_semicolon_Leave_()
        {
            return FuncParsetail_semicolon_Leave_;
        }
        /// <summary>
        /// 对 叶结点null 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParseepsilonLeave_()
        {
            return FuncParseepsilonLeave_;
        }
        /// <summary>
        /// 对 叶结点&quot;|&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_or_Leave_()
        {
            return FuncParsetail_or_Leave_;
        }
        /// <summary>
        /// 对 叶结点&quot;&lt;&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_lessThan_Leave_()
        {
            return FuncParsetail_lessThan_Leave_;
        }
        /// <summary>
        /// 对 叶结点identifier 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParseidentifierLeave_()
        {
            return FuncParseidentifierLeave_;
        }
        /// <summary>
        /// 对 叶结点&quot;&gt;&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_greaterThan_Leave_()
        {
            return FuncParsetail_greaterThan_Leave_;
        }
        /// <summary>
        /// 对 叶结点&quot;null&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_nullLeave_()
        {
            return FuncParsetail_nullLeave_;
        }
        /// <summary>
        /// 对 叶结点&quot;identifier&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_identifierLeave_()
        {
            return FuncParsetail_identifierLeave_;
        }
        /// <summary>
        /// 对 叶结点&quot;number&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_numberLeave_()
        {
            return FuncParsetail_numberLeave_;
        }
        /// <summary>
        /// 对 叶结点&quot;constString&quot; 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_constStringLeave_()
        {
            return FuncParsetail_constStringLeave_;
        }
        /// <summary>
        /// 对 叶结点number 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsenumberLeave_()
        {
            return FuncParsenumberLeave_;
        }
        /// <summary>
        /// 对 叶结点constString 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParseconstStringLeave_()
        {
            return FuncParseconstStringLeave_;
        }
        /// <summary>
        /// 对 叶结点# 进行分析
        /// </summary>
        public static CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            GetFuncParsetail_startEndLeave_()
        {
            return FuncParsetail_startEndLeave_;
        }
        
        #endregion 获取分析表中的元素
        #region 分析表中的元素指向的分析函数
        
        /// <summary>
        /// 对 &lt;Start&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;Start&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Start___tail_lessThan_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Start> ::= <Vn> "::=" <VList> ";" <PList>;
            return Derivationcase_Start___case_Vntail_colon_Colon_Equality_Leavecase_VListtail_semicolon_Leavecase_PList(result, parser);
        }
        /// <summary>
        /// 对 &lt;PList&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;PList&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_PList___tail_lessThan_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <PList> ::= <Vn> "::=" <VList> ";" <PList>;
            return Derivationcase_PList___case_Vntail_colon_Colon_Equality_Leavecase_VListtail_semicolon_Leavecase_PList(result, parser);
        }
        /// <summary>
        /// 对 &lt;PList&gt; ::= #... 进行分析
        /// <para>&lt;PList&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_PList___tail_startEndLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <PList> ::= null;
            return Derivationcase_PList___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___tail_lessThan_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= identifier... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___tail_nullLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___tail_identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___tail_numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___tail_constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= number... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VList&gt; ::= constString... 进行分析
        /// <para>&lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VList___constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VList> ::= <V> <VOpt>;
            return Derivationcase_VList___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vn&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___tail_lessThan_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vn>;
            return Derivationcase_V___case_Vn(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= identifier... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vt>;
            return Derivationcase_V___case_Vt(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___tail_nullLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vt>;
            return Derivationcase_V___case_Vt(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___tail_identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vt>;
            return Derivationcase_V___case_Vt(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___tail_numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vt>;
            return Derivationcase_V___case_Vt(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___tail_constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vt>;
            return Derivationcase_V___case_Vt(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= number... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vt>;
            return Derivationcase_V___case_Vt(result, parser);
        }
        /// <summary>
        /// 对 &lt;V&gt; ::= constString... 进行分析
        /// <para>&lt;V&gt; ::= &lt;Vt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_V___constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <V> ::= <Vt>;
            return Derivationcase_V___case_Vt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;;&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= null;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___tail_semicolon_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= null;
            return Derivationcase_VOpt___epsilonLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;|&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &quot;|&quot; &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___tail_or_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= "|" <V> <VOpt>;
            return Derivationcase_VOpt___tail_or_Leavecase_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___tail_lessThan_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= identifier... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___tail_nullLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___tail_identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___tail_numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___tail_constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= number... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;VOpt&gt; ::= constString... 进行分析
        /// <para>&lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_VOpt___constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <VOpt> ::= <V> <VOpt>;
            return Derivationcase_VOpt___case_Vcase_VOpt(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vn&gt; ::= &quot;&lt;&quot;... 进行分析
        /// <para>&lt;Vn&gt; ::= &quot;&lt;&quot; identifier &quot;&gt;&quot;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vn___tail_lessThan_Leave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vn> ::= "<" identifier ">";
            return Derivationcase_Vn___tail_lessThan_LeaveidentifierLeavetail_greaterThan_Leave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= identifier... 进行分析
        /// <para>&lt;Vt&gt; ::= identifier;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vt___identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vt> ::= identifier;
            return Derivationcase_Vt___identifierLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;null&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;null&quot;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vt___tail_nullLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vt> ::= "null";
            return Derivationcase_Vt___tail_nullLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;identifier&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;identifier&quot;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vt___tail_identifierLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vt> ::= "identifier";
            return Derivationcase_Vt___tail_identifierLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;number&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;number&quot;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vt___tail_numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vt> ::= "number";
            return Derivationcase_Vt___tail_numberLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= &quot;constString&quot;... 进行分析
        /// <para>&lt;Vt&gt; ::= &quot;constString&quot;;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vt___tail_constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vt> ::= "constString";
            return Derivationcase_Vt___tail_constStringLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= number... 进行分析
        /// <para>&lt;Vt&gt; ::= number;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vt___numberLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vt> ::= number;
            return Derivationcase_Vt___numberLeave(result, parser);
        }
        /// <summary>
        /// 对 &lt;Vt&gt; ::= constString... 进行分析
        /// <para>&lt;Vt&gt; ::= constString;</para>
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsecase_Vt___constStringLeave(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            // <Vt> ::= constString;
            return Derivationcase_Vt___constStringLeave(result, parser);
        }
        
        /// <summary>
        /// 对 叶结点&quot;::=&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_colon_Colon_Equality_Leave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_colon_Colon_Equality_Leave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;;&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_semicolon_Leave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_semicolon_Leave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点null 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            ParseepsilonLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.epsilonLeave;
            result.NodeValue.NodeName = @"null";
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            result.MappedTokenLength = 0;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;|&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_or_Leave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_or_Leave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;&lt;&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_lessThan_Leave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_lessThan_Leave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点identifier 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            ParseidentifierLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.identifierLeave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;&gt;&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_greaterThan_Leave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_greaterThan_Leave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;null&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_nullLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_nullLeave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;identifier&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_identifierLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_identifierLeave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;number&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_numberLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_numberLeave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点&quot;constString&quot; 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_constStringLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.tail_constStringLeave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点number 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            ParsenumberLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.numberLeave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点constString 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            ParseconstStringLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.constStringLeave;
            result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ptNextToken++;
            result.MappedTokenLength = 1;
            parserCG.m_ParserStack.Pop();
            return Next(result, parserCG);
        }
        /// <summary>
        /// 对 叶结点# 进行分析
        /// <param name="result"></param>
        /// <param name="parser"></param>
        /// </summary>
        /// <returns></returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Parsetail_startEndLeave_(SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result, ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {
            var parserCG = parser as LL1SyntaxParserCG;
            if (result != null)
            {
                result.NodeValue.NodeType = EnumVTypeCG.tail_startEndLeave;
                result.NodeValue.NodeName = parserCG.m_TokenListSource[parserCG.m_ptNextToken].Detail;
                result.MappedTotalTokenList = parserCG.m_TokenListSource;
                result.MappedTokenStartIndex = parserCG.m_ptNextToken;
                result.MappedTokenLength = 1;
            }
            parserCG.m_ParserStack.Pop();
            parserCG.m_ptNextToken++;
            return Next(result, parserCG);
        }
        
        #endregion 分析表中的元素指向的分析函数
        #region 所有推导式的推导动作函数
        
        /// <summary>
        /// &lt;Start&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Start___case_Vntail_colon_Colon_Equality_Leavecase_VListtail_semicolon_Leavecase_PList(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Start> ::= <Vn> "::=" <VList> ";" <PList>;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Start;
            result.NodeValue.NodeName = EnumVTypeCG.case_Start.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_case_PList);
            parserCG.m_ParserStack.Push(m_tail_semicolon_Leave);
            parserCG.m_ParserStack.Push(m_case_VList);
            parserCG.m_ParserStack.Push(m_tail_colon_Colon_Equality_Leave);
            parserCG.m_ParserStack.Push(m_case_Vn);
            // generate syntax tree
            var case_VnTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VnTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VnTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VnTree0.Parent = result;
            //case_VnTree0.Value = new ProductionNode(EnumVTypeCG.case_Vn);
            result.Children.Add(case_VnTree0);
            var tail_colon_Colon_Equality_LeaveTree1 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_colon_Colon_Equality_LeaveTree1.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_colon_Colon_Equality_LeaveTree1.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_colon_Colon_Equality_LeaveTree1.Parent = result;
            //tail_colon_Colon_Equality_LeaveTree1.Value = new ProductionNode(EnumVTypeCG.tail_colon_Colon_Equality_Leave);
            result.Children.Add(tail_colon_Colon_Equality_LeaveTree1);
            var case_VListTree2 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VListTree2.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VListTree2.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VListTree2.Parent = result;
            //case_VListTree2.Value = new ProductionNode(EnumVTypeCG.case_VList);
            result.Children.Add(case_VListTree2);
            var tail_semicolon_LeaveTree3 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_semicolon_LeaveTree3.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_semicolon_LeaveTree3.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_semicolon_LeaveTree3.Parent = result;
            //tail_semicolon_LeaveTree3.Value = new ProductionNode(EnumVTypeCG.tail_semicolon_Leave);
            result.Children.Add(tail_semicolon_LeaveTree3);
            var case_PListTree4 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_PListTree4.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_PListTree4.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_PListTree4.Parent = result;
            //case_PListTree4.Value = new ProductionNode(EnumVTypeCG.case_PList);
            result.Children.Add(case_PListTree4);
            return case_VnTree0;
        }//<Start> ::= <Vn> "::=" <VList> ";" <PList>;
        /// <summary>
        /// &lt;PList&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_PList___case_Vntail_colon_Colon_Equality_Leavecase_VListtail_semicolon_Leavecase_PList(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<PList> ::= <Vn> "::=" <VList> ";" <PList>;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_PList;
            result.NodeValue.NodeName = EnumVTypeCG.case_PList.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_case_PList);
            parserCG.m_ParserStack.Push(m_tail_semicolon_Leave);
            parserCG.m_ParserStack.Push(m_case_VList);
            parserCG.m_ParserStack.Push(m_tail_colon_Colon_Equality_Leave);
            parserCG.m_ParserStack.Push(m_case_Vn);
            // generate syntax tree
            var case_VnTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VnTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VnTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VnTree0.Parent = result;
            //case_VnTree0.Value = new ProductionNode(EnumVTypeCG.case_Vn);
            result.Children.Add(case_VnTree0);
            var tail_colon_Colon_Equality_LeaveTree1 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_colon_Colon_Equality_LeaveTree1.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_colon_Colon_Equality_LeaveTree1.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_colon_Colon_Equality_LeaveTree1.Parent = result;
            //tail_colon_Colon_Equality_LeaveTree1.Value = new ProductionNode(EnumVTypeCG.tail_colon_Colon_Equality_Leave);
            result.Children.Add(tail_colon_Colon_Equality_LeaveTree1);
            var case_VListTree2 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VListTree2.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VListTree2.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VListTree2.Parent = result;
            //case_VListTree2.Value = new ProductionNode(EnumVTypeCG.case_VList);
            result.Children.Add(case_VListTree2);
            var tail_semicolon_LeaveTree3 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_semicolon_LeaveTree3.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_semicolon_LeaveTree3.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_semicolon_LeaveTree3.Parent = result;
            //tail_semicolon_LeaveTree3.Value = new ProductionNode(EnumVTypeCG.tail_semicolon_Leave);
            result.Children.Add(tail_semicolon_LeaveTree3);
            var case_PListTree4 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_PListTree4.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_PListTree4.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_PListTree4.Parent = result;
            //case_PListTree4.Value = new ProductionNode(EnumVTypeCG.case_PList);
            result.Children.Add(case_PListTree4);
            return case_VnTree0;
        }//<PList> ::= <Vn> "::=" <VList> ";" <PList>;
        /// <summary>
        /// &lt;PList&gt; ::= null;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_PList___epsilonLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<PList> ::= null;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_PList;
            result.NodeValue.NodeName = EnumVTypeCG.case_PList.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_epsilonLeave);
            // generate syntax tree
            var epsilonLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            epsilonLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            epsilonLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            epsilonLeaveTree0.Parent = result;
            //epsilonLeaveTree0.Value = new ProductionNode(EnumVTypeCG.epsilonLeave);
            result.Children.Add(epsilonLeaveTree0);
            return epsilonLeaveTree0;
        }//<PList> ::= null;
        /// <summary>
        /// &lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_VList___case_Vcase_VOpt(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<VList> ::= <V> <VOpt>;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_VList;
            result.NodeValue.NodeName = EnumVTypeCG.case_VList.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_case_VOpt);
            parserCG.m_ParserStack.Push(m_case_V);
            // generate syntax tree
            var case_VTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VTree0.Parent = result;
            //case_VTree0.Value = new ProductionNode(EnumVTypeCG.case_V);
            result.Children.Add(case_VTree0);
            var case_VOptTree1 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VOptTree1.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VOptTree1.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VOptTree1.Parent = result;
            //case_VOptTree1.Value = new ProductionNode(EnumVTypeCG.case_VOpt);
            result.Children.Add(case_VOptTree1);
            return case_VTree0;
        }//<VList> ::= <V> <VOpt>;
        /// <summary>
        /// &lt;V&gt; ::= &lt;Vn&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_V___case_Vn(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<V> ::= <Vn>;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_V;
            result.NodeValue.NodeName = EnumVTypeCG.case_V.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_case_Vn);
            // generate syntax tree
            var case_VnTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VnTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VnTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VnTree0.Parent = result;
            //case_VnTree0.Value = new ProductionNode(EnumVTypeCG.case_Vn);
            result.Children.Add(case_VnTree0);
            return case_VnTree0;
        }//<V> ::= <Vn>;
        /// <summary>
        /// &lt;V&gt; ::= &lt;Vt&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_V___case_Vt(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<V> ::= <Vt>;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_V;
            result.NodeValue.NodeName = EnumVTypeCG.case_V.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_case_Vt);
            // generate syntax tree
            var case_VtTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VtTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VtTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VtTree0.Parent = result;
            //case_VtTree0.Value = new ProductionNode(EnumVTypeCG.case_Vt);
            result.Children.Add(case_VtTree0);
            return case_VtTree0;
        }//<V> ::= <Vt>;
        /// <summary>
        /// &lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_VOpt___case_Vcase_VOpt(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<VOpt> ::= <V> <VOpt>;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_VOpt;
            result.NodeValue.NodeName = EnumVTypeCG.case_VOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_case_VOpt);
            parserCG.m_ParserStack.Push(m_case_V);
            // generate syntax tree
            var case_VTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VTree0.Parent = result;
            //case_VTree0.Value = new ProductionNode(EnumVTypeCG.case_V);
            result.Children.Add(case_VTree0);
            var case_VOptTree1 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VOptTree1.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VOptTree1.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VOptTree1.Parent = result;
            //case_VOptTree1.Value = new ProductionNode(EnumVTypeCG.case_VOpt);
            result.Children.Add(case_VOptTree1);
            return case_VTree0;
        }//<VOpt> ::= <V> <VOpt>;
        /// <summary>
        /// &lt;VOpt&gt; ::= &quot;|&quot; &lt;V&gt; &lt;VOpt&gt;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_VOpt___tail_or_Leavecase_Vcase_VOpt(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<VOpt> ::= "|" <V> <VOpt>;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_VOpt;
            result.NodeValue.NodeName = EnumVTypeCG.case_VOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_case_VOpt);
            parserCG.m_ParserStack.Push(m_case_V);
            parserCG.m_ParserStack.Push(m_tail_or_Leave);
            // generate syntax tree
            var tail_or_LeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_or_LeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_or_LeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_or_LeaveTree0.Parent = result;
            //tail_or_LeaveTree0.Value = new ProductionNode(EnumVTypeCG.tail_or_Leave);
            result.Children.Add(tail_or_LeaveTree0);
            var case_VTree1 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VTree1.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VTree1.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VTree1.Parent = result;
            //case_VTree1.Value = new ProductionNode(EnumVTypeCG.case_V);
            result.Children.Add(case_VTree1);
            var case_VOptTree2 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            case_VOptTree2.MappedTotalTokenList = parserCG.m_TokenListSource;
            case_VOptTree2.MappedTokenStartIndex = parserCG.m_ptNextToken;
            case_VOptTree2.Parent = result;
            //case_VOptTree2.Value = new ProductionNode(EnumVTypeCG.case_VOpt);
            result.Children.Add(case_VOptTree2);
            return tail_or_LeaveTree0;
        }//<VOpt> ::= "|" <V> <VOpt>;
        /// <summary>
        /// &lt;VOpt&gt; ::= null;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_VOpt___epsilonLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<VOpt> ::= null;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_VOpt;
            result.NodeValue.NodeName = EnumVTypeCG.case_VOpt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_epsilonLeave);
            // generate syntax tree
            var epsilonLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            epsilonLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            epsilonLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            epsilonLeaveTree0.Parent = result;
            //epsilonLeaveTree0.Value = new ProductionNode(EnumVTypeCG.epsilonLeave);
            result.Children.Add(epsilonLeaveTree0);
            return epsilonLeaveTree0;
        }//<VOpt> ::= null;
        /// <summary>
        /// &lt;Vn&gt; ::= &quot;&lt;&quot; identifier &quot;&gt;&quot;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vn___tail_lessThan_LeaveidentifierLeavetail_greaterThan_Leave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vn> ::= "<" identifier ">";
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vn;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vn.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_tail_greaterThan_Leave);
            parserCG.m_ParserStack.Push(m_identifierLeave);
            parserCG.m_ParserStack.Push(m_tail_lessThan_Leave);
            // generate syntax tree
            var tail_lessThan_LeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_lessThan_LeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_lessThan_LeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_lessThan_LeaveTree0.Parent = result;
            //tail_lessThan_LeaveTree0.Value = new ProductionNode(EnumVTypeCG.tail_lessThan_Leave);
            result.Children.Add(tail_lessThan_LeaveTree0);
            var identifierLeaveTree1 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            identifierLeaveTree1.MappedTotalTokenList = parserCG.m_TokenListSource;
            identifierLeaveTree1.MappedTokenStartIndex = parserCG.m_ptNextToken;
            identifierLeaveTree1.Parent = result;
            //identifierLeaveTree1.Value = new ProductionNode(EnumVTypeCG.identifierLeave);
            result.Children.Add(identifierLeaveTree1);
            var tail_greaterThan_LeaveTree2 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_greaterThan_LeaveTree2.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_greaterThan_LeaveTree2.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_greaterThan_LeaveTree2.Parent = result;
            //tail_greaterThan_LeaveTree2.Value = new ProductionNode(EnumVTypeCG.tail_greaterThan_Leave);
            result.Children.Add(tail_greaterThan_LeaveTree2);
            return tail_lessThan_LeaveTree0;
        }//<Vn> ::= "<" identifier ">";
        /// <summary>
        /// &lt;Vt&gt; ::= &quot;null&quot;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vt___tail_nullLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vt> ::= "null";
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vt;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_tail_nullLeave);
            // generate syntax tree
            var tail_nullLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_nullLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_nullLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_nullLeaveTree0.Parent = result;
            //tail_nullLeaveTree0.Value = new ProductionNode(EnumVTypeCG.tail_nullLeave);
            result.Children.Add(tail_nullLeaveTree0);
            return tail_nullLeaveTree0;
        }//<Vt> ::= "null";
        /// <summary>
        /// &lt;Vt&gt; ::= &quot;identifier&quot;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vt___tail_identifierLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vt> ::= "identifier";
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vt;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_tail_identifierLeave);
            // generate syntax tree
            var tail_identifierLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_identifierLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_identifierLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_identifierLeaveTree0.Parent = result;
            //tail_identifierLeaveTree0.Value = new ProductionNode(EnumVTypeCG.tail_identifierLeave);
            result.Children.Add(tail_identifierLeaveTree0);
            return tail_identifierLeaveTree0;
        }//<Vt> ::= "identifier";
        /// <summary>
        /// &lt;Vt&gt; ::= &quot;number&quot;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vt___tail_numberLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vt> ::= "number";
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vt;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_tail_numberLeave);
            // generate syntax tree
            var tail_numberLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_numberLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_numberLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_numberLeaveTree0.Parent = result;
            //tail_numberLeaveTree0.Value = new ProductionNode(EnumVTypeCG.tail_numberLeave);
            result.Children.Add(tail_numberLeaveTree0);
            return tail_numberLeaveTree0;
        }//<Vt> ::= "number";
        /// <summary>
        /// &lt;Vt&gt; ::= &quot;constString&quot;;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vt___tail_constStringLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vt> ::= "constString";
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vt;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_tail_constStringLeave);
            // generate syntax tree
            var tail_constStringLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            tail_constStringLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            tail_constStringLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            tail_constStringLeaveTree0.Parent = result;
            //tail_constStringLeaveTree0.Value = new ProductionNode(EnumVTypeCG.tail_constStringLeave);
            result.Children.Add(tail_constStringLeaveTree0);
            return tail_constStringLeaveTree0;
        }//<Vt> ::= "constString";
        /// <summary>
        /// &lt;Vt&gt; ::= identifier;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vt___identifierLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vt> ::= identifier;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vt;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_identifierLeave);
            // generate syntax tree
            var identifierLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            identifierLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            identifierLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            identifierLeaveTree0.Parent = result;
            //identifierLeaveTree0.Value = new ProductionNode(EnumVTypeCG.identifierLeave);
            result.Children.Add(identifierLeaveTree0);
            return identifierLeaveTree0;
        }//<Vt> ::= identifier;
        /// <summary>
        /// &lt;Vt&gt; ::= number;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vt___numberLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vt> ::= number;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vt;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_numberLeave);
            // generate syntax tree
            var numberLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            numberLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            numberLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            numberLeaveTree0.Parent = result;
            //numberLeaveTree0.Value = new ProductionNode(EnumVTypeCG.numberLeave);
            result.Children.Add(numberLeaveTree0);
            return numberLeaveTree0;
        }//<Vt> ::= number;
        /// <summary>
        /// &lt;Vt&gt; ::= constString;
        /// <summary>
        /// <param name="result">需要扩展的结点</param>
        /// <param name="parser">使用的分析器对象</param>
        /// <returns>下一个要扩展的结点</returns>
        private static SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            Derivationcase_Vt___constStringLeave(
            SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> result,
            ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> parser)
        {//<Vt> ::= constString;
            var parserCG = parser as LL1SyntaxParserCG;
            result.NodeValue.NodeType = EnumVTypeCG.case_Vt;
            result.NodeValue.NodeName = EnumVTypeCG.case_Vt.ToString();
            //result.NodeValue.Position = EnumProductionNodePosition.NonLeave;
            result.MappedTotalTokenList = parserCG.m_TokenListSource;
            result.MappedTokenStartIndex = parserCG.m_ptNextToken;
            parserCG.m_ParserStack.Pop();
            // right-to-left push
            parserCG.m_ParserStack.Push(m_constStringLeave);
            // generate syntax tree
            var constStringLeaveTree0 = new SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>();
            constStringLeaveTree0.MappedTotalTokenList = parserCG.m_TokenListSource;
            constStringLeaveTree0.MappedTokenStartIndex = parserCG.m_ptNextToken;
            constStringLeaveTree0.Parent = result;
            //constStringLeaveTree0.Value = new ProductionNode(EnumVTypeCG.constStringLeave);
            result.Children.Add(constStringLeaveTree0);
            return constStringLeaveTree0;
        }//<Vt> ::= constString;
        
        #endregion 所有推导式的推导动作函数
        #region FillMapCells()
        
        private void FillMapCells()
        {
            m_Map.SetCell(EnumVTypeCG.case_Start, EnumTokenTypeCG.token_LessThan_, FuncParsecase_Start___tail_lessThan_Leave);
            m_Map.SetCell(EnumVTypeCG.case_PList, EnumTokenTypeCG.token_LessThan_, FuncParsecase_PList___tail_lessThan_Leave);
            m_Map.SetCell(EnumVTypeCG.case_PList, EnumTokenTypeCG.token_startEnd, FuncParsecase_PList___tail_startEndLeave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.token_LessThan_, FuncParsecase_VList___tail_lessThan_Leave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.identifier, FuncParsecase_VList___identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.token_null, FuncParsecase_VList___tail_nullLeave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.token_identifier, FuncParsecase_VList___tail_identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.token_number, FuncParsecase_VList___tail_numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.token_constString, FuncParsecase_VList___tail_constStringLeave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.number, FuncParsecase_VList___numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_VList, EnumTokenTypeCG.constString, FuncParsecase_VList___constStringLeave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.token_LessThan_, FuncParsecase_V___tail_lessThan_Leave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.identifier, FuncParsecase_V___identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.token_null, FuncParsecase_V___tail_nullLeave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.token_identifier, FuncParsecase_V___tail_identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.token_number, FuncParsecase_V___tail_numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.token_constString, FuncParsecase_V___tail_constStringLeave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.number, FuncParsecase_V___numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_V, EnumTokenTypeCG.constString, FuncParsecase_V___constStringLeave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.token_Semicolon_, FuncParsecase_VOpt___tail_semicolon_Leave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.token_Or_, FuncParsecase_VOpt___tail_or_Leave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.token_LessThan_, FuncParsecase_VOpt___tail_lessThan_Leave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.identifier, FuncParsecase_VOpt___identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.token_null, FuncParsecase_VOpt___tail_nullLeave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.token_identifier, FuncParsecase_VOpt___tail_identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.token_number, FuncParsecase_VOpt___tail_numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.token_constString, FuncParsecase_VOpt___tail_constStringLeave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.number, FuncParsecase_VOpt___numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_VOpt, EnumTokenTypeCG.constString, FuncParsecase_VOpt___constStringLeave);
            m_Map.SetCell(EnumVTypeCG.case_Vn, EnumTokenTypeCG.token_LessThan_, FuncParsecase_Vn___tail_lessThan_Leave);
            m_Map.SetCell(EnumVTypeCG.case_Vt, EnumTokenTypeCG.identifier, FuncParsecase_Vt___identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_Vt, EnumTokenTypeCG.token_null, FuncParsecase_Vt___tail_nullLeave);
            m_Map.SetCell(EnumVTypeCG.case_Vt, EnumTokenTypeCG.token_identifier, FuncParsecase_Vt___tail_identifierLeave);
            m_Map.SetCell(EnumVTypeCG.case_Vt, EnumTokenTypeCG.token_number, FuncParsecase_Vt___tail_numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_Vt, EnumTokenTypeCG.token_constString, FuncParsecase_Vt___tail_constStringLeave);
            m_Map.SetCell(EnumVTypeCG.case_Vt, EnumTokenTypeCG.number, FuncParsecase_Vt___numberLeave);
            m_Map.SetCell(EnumVTypeCG.case_Vt, EnumTokenTypeCG.constString, FuncParsecase_Vt___constStringLeave);
            
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.epsilon, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_Or_, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.identifier, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_null, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_identifier, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_number, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_constString, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.number, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.constString, FuncParsetail_colon_Colon_Equality_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_colon_Colon_Equality_Leave, EnumTokenTypeCG.token_startEnd, FuncParsetail_colon_Colon_Equality_Leave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.epsilon, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_Or_, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.identifier, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_null, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_identifier, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_number, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_constString, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.number, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.constString, FuncParsetail_semicolon_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_semicolon_Leave, EnumTokenTypeCG.token_startEnd, FuncParsetail_semicolon_Leave_);
            
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_Semicolon_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.epsilon, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_Or_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_LessThan_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.identifier, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_null, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_identifier, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_number, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_constString, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.number, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.constString, FuncParseepsilonLeave_);
            m_Map.SetCell(EnumVTypeCG.epsilonLeave, EnumTokenTypeCG.token_startEnd, FuncParseepsilonLeave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.epsilon, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_Or_, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.identifier, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_null, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_identifier, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_number, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_constString, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.number, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.constString, FuncParsetail_or_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_or_Leave, EnumTokenTypeCG.token_startEnd, FuncParsetail_or_Leave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.epsilon, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_Or_, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.identifier, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_null, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_identifier, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_number, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_constString, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.number, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.constString, FuncParsetail_lessThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_lessThan_Leave, EnumTokenTypeCG.token_startEnd, FuncParsetail_lessThan_Leave_);
            
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_Semicolon_, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.epsilon, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_Or_, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_LessThan_, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.identifier, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_null, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_identifier, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_number, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_constString, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.number, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.constString, FuncParseidentifierLeave_);
            m_Map.SetCell(EnumVTypeCG.identifierLeave, EnumTokenTypeCG.token_startEnd, FuncParseidentifierLeave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.epsilon, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_Or_, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.identifier, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_null, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_identifier, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_number, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_constString, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.number, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.constString, FuncParsetail_greaterThan_Leave_);
            m_Map.SetCell(EnumVTypeCG.tail_greaterThan_Leave, EnumTokenTypeCG.token_startEnd, FuncParsetail_greaterThan_Leave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.epsilon, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_Or_, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.identifier, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_null, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_identifier, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_number, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_constString, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.number, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.constString, FuncParsetail_nullLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_nullLeave, EnumTokenTypeCG.token_startEnd, FuncParsetail_nullLeave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.epsilon, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_Or_, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.identifier, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_null, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_identifier, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_number, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_constString, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.number, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.constString, FuncParsetail_identifierLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_identifierLeave, EnumTokenTypeCG.token_startEnd, FuncParsetail_identifierLeave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.epsilon, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_Or_, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.identifier, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_null, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_identifier, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_number, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_constString, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.number, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.constString, FuncParsetail_numberLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_numberLeave, EnumTokenTypeCG.token_startEnd, FuncParsetail_numberLeave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.epsilon, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_Or_, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.identifier, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_null, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_identifier, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_number, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_constString, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.number, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.constString, FuncParsetail_constStringLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_constStringLeave, EnumTokenTypeCG.token_startEnd, FuncParsetail_constStringLeave_);
            
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_Semicolon_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.epsilon, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_Or_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_LessThan_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.identifier, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_null, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_identifier, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_number, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_constString, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.number, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.constString, FuncParsenumberLeave_);
            m_Map.SetCell(EnumVTypeCG.numberLeave, EnumTokenTypeCG.token_startEnd, FuncParsenumberLeave_);
            
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_Semicolon_, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.epsilon, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_Or_, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_LessThan_, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.identifier, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_null, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_identifier, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_number, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_constString, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.number, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.constString, FuncParseconstStringLeave_);
            m_Map.SetCell(EnumVTypeCG.constStringLeave, EnumTokenTypeCG.token_startEnd, FuncParseconstStringLeave_);
            
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_Colon_Colon_Equality_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_Semicolon_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.epsilon, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_Or_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_LessThan_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.identifier, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_GreaterThan_, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_null, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_identifier, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_number, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_constString, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.number, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.constString, FuncParsetail_startEndLeave_);
            m_Map.SetCell(EnumVTypeCG.tail_startEndLeave, EnumTokenTypeCG.token_startEnd, FuncParsetail_startEndLeave_);
        }
        
        #endregion FillMapCells()
        #region 为分析表中的元素配置分析函数
        
        private void InitFunc()
        {
            FuncParsecase_Start___tail_lessThan_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Start___tail_lessThan_Leave);
            FuncParsecase_PList___tail_lessThan_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_PList___tail_lessThan_Leave);
            FuncParsecase_PList___tail_startEndLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_PList___tail_startEndLeave);
            FuncParsecase_VList___tail_lessThan_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___tail_lessThan_Leave);
            FuncParsecase_VList___identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___identifierLeave);
            FuncParsecase_VList___tail_nullLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___tail_nullLeave);
            FuncParsecase_VList___tail_identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___tail_identifierLeave);
            FuncParsecase_VList___tail_numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___tail_numberLeave);
            FuncParsecase_VList___tail_constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___tail_constStringLeave);
            FuncParsecase_VList___numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___numberLeave);
            FuncParsecase_VList___constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VList___constStringLeave);
            FuncParsecase_V___tail_lessThan_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___tail_lessThan_Leave);
            FuncParsecase_V___identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___identifierLeave);
            FuncParsecase_V___tail_nullLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___tail_nullLeave);
            FuncParsecase_V___tail_identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___tail_identifierLeave);
            FuncParsecase_V___tail_numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___tail_numberLeave);
            FuncParsecase_V___tail_constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___tail_constStringLeave);
            FuncParsecase_V___numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___numberLeave);
            FuncParsecase_V___constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_V___constStringLeave);
            FuncParsecase_VOpt___tail_semicolon_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___tail_semicolon_Leave);
            FuncParsecase_VOpt___tail_or_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___tail_or_Leave);
            FuncParsecase_VOpt___tail_lessThan_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___tail_lessThan_Leave);
            FuncParsecase_VOpt___identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___identifierLeave);
            FuncParsecase_VOpt___tail_nullLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___tail_nullLeave);
            FuncParsecase_VOpt___tail_identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___tail_identifierLeave);
            FuncParsecase_VOpt___tail_numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___tail_numberLeave);
            FuncParsecase_VOpt___tail_constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___tail_constStringLeave);
            FuncParsecase_VOpt___numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___numberLeave);
            FuncParsecase_VOpt___constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_VOpt___constStringLeave);
            FuncParsecase_Vn___tail_lessThan_Leave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vn___tail_lessThan_Leave);
            FuncParsecase_Vt___identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vt___identifierLeave);
            FuncParsecase_Vt___tail_nullLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vt___tail_nullLeave);
            FuncParsecase_Vt___tail_identifierLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vt___tail_identifierLeave);
            FuncParsecase_Vt___tail_numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vt___tail_numberLeave);
            FuncParsecase_Vt___tail_constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vt___tail_constStringLeave);
            FuncParsecase_Vt___numberLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vt___numberLeave);
            FuncParsecase_Vt___constStringLeave = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsecase_Vt___constStringLeave);
            
            FuncParsetail_colon_Colon_Equality_Leave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_colon_Colon_Equality_Leave_);
            FuncParsetail_semicolon_Leave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_semicolon_Leave_);
            FuncParseepsilonLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(ParseepsilonLeave_);
            FuncParsetail_or_Leave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_or_Leave_);
            FuncParsetail_lessThan_Leave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_lessThan_Leave_);
            FuncParseidentifierLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(ParseidentifierLeave_);
            FuncParsetail_greaterThan_Leave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_greaterThan_Leave_);
            FuncParsetail_nullLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_nullLeave_);
            FuncParsetail_identifierLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_identifierLeave_);
            FuncParsetail_numberLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_numberLeave_);
            FuncParsetail_constStringLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_constStringLeave_);
            FuncParsenumberLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(ParsenumberLeave_);
            FuncParseconstStringLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(ParseconstStringLeave_);
            FuncParsetail_startEndLeave_ = 
                new CandidateFunction<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>(Parsetail_startEndLeave_);
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
        /// LL1SyntaxParserCG的语法分析器
        /// </summary>
        public LL1SyntaxParserCG()
            : base(21, 14) { }
        /// LL1SyntaxParserCG的语法分析器
        /// </summary>
        /// <param name="tokens">要分析的单词列表</param>
        public LL1SyntaxParserCG(TokenList<EnumTokenTypeCG> tokens)
            : base(21, 14)
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
            m_ParserStack.Push(m_case_Start);
            if (m_TokenListSource.Count == 0)
            {
                var newToken = new Token<EnumTokenTypeCG>()
                {
                    Detail = "#",
                    Line = 0,
                    Column = 0,
                    IndexOfSourceCode = 0,
                    Length = 1,
                    LexicalError = false,
                    TokenType = EnumTokenTypeCG.token_startEnd
                };
                m_TokenListSource.Add(newToken);
            }
            else
            {
                var token = m_TokenListSource[m_TokenListSource.Count - 1];
                {
                    var newToken = new Token<EnumTokenTypeCG>()
                    {
                        Detail = "#",
                        Line = token.Line,
                        Column = token.Column + token.Length + 1,
                        IndexOfSourceCode = token.IndexOfSourceCode + token.Length + 1,
                        Length = 1,
                        LexicalError = false,
                        TokenType = EnumTokenTypeCG.token_startEnd
                    };
                    m_TokenListSource.Add(newToken);
                }
            }
        }
        
        #endregion 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析
        #region SetMapLinesAndColumns()
        
        private void SetMapLinesAndColumns()
        {
            m_Map.SetLine(0, EnumVTypeCG.case_Start);
            m_Map.SetLine(1, EnumVTypeCG.case_PList);
            m_Map.SetLine(2, EnumVTypeCG.case_VList);
            m_Map.SetLine(3, EnumVTypeCG.case_V);
            m_Map.SetLine(4, EnumVTypeCG.case_VOpt);
            m_Map.SetLine(5, EnumVTypeCG.case_Vn);
            m_Map.SetLine(6, EnumVTypeCG.case_Vt);
            
            m_Map.SetLine(7, EnumVTypeCG.tail_colon_Colon_Equality_Leave);
            m_Map.SetLine(8, EnumVTypeCG.tail_semicolon_Leave);
            m_Map.SetLine(9, EnumVTypeCG.epsilonLeave);
            m_Map.SetLine(10, EnumVTypeCG.tail_or_Leave);
            m_Map.SetLine(11, EnumVTypeCG.tail_lessThan_Leave);
            m_Map.SetLine(12, EnumVTypeCG.identifierLeave);
            m_Map.SetLine(13, EnumVTypeCG.tail_greaterThan_Leave);
            m_Map.SetLine(14, EnumVTypeCG.tail_nullLeave);
            m_Map.SetLine(15, EnumVTypeCG.tail_identifierLeave);
            m_Map.SetLine(16, EnumVTypeCG.tail_numberLeave);
            m_Map.SetLine(17, EnumVTypeCG.tail_constStringLeave);
            m_Map.SetLine(18, EnumVTypeCG.numberLeave);
            m_Map.SetLine(19, EnumVTypeCG.constStringLeave);
            m_Map.SetLine(20, EnumVTypeCG.tail_startEndLeave);
            
            
            m_Map.SetColumn(0, EnumTokenTypeCG.token_Colon_Colon_Equality_);
            m_Map.SetColumn(1, EnumTokenTypeCG.token_Semicolon_);
            m_Map.SetColumn(2, EnumTokenTypeCG.epsilon);
            m_Map.SetColumn(3, EnumTokenTypeCG.token_Or_);
            m_Map.SetColumn(4, EnumTokenTypeCG.token_LessThan_);
            m_Map.SetColumn(5, EnumTokenTypeCG.identifier);
            m_Map.SetColumn(6, EnumTokenTypeCG.token_GreaterThan_);
            m_Map.SetColumn(7, EnumTokenTypeCG.token_null);
            m_Map.SetColumn(8, EnumTokenTypeCG.token_identifier);
            m_Map.SetColumn(9, EnumTokenTypeCG.token_number);
            m_Map.SetColumn(10, EnumTokenTypeCG.token_constString);
            m_Map.SetColumn(11, EnumTokenTypeCG.number);
            m_Map.SetColumn(12, EnumTokenTypeCG.constString);
            m_Map.SetColumn(13, EnumTokenTypeCG.token_startEnd);
        }
        
        #endregion SetMapLinesAndColumns()
    }

}

