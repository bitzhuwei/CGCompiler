namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 文法CG的语法树结点枚举类型
    /// </summary>
    public enum EnumVTypeCG
    {
        /// <summary>
        /// 未知的语法结点符号
        /// </summary>
        Unknown,
        /// <summary>
        /// &lt;Start&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt;;
        /// </summary>
        case_Start,
        /// <summary>
        /// &lt;PList&gt; ::= &lt;Vn&gt; &quot;::=&quot; &lt;VList&gt; &quot;;&quot; &lt;PList&gt; | null;
        /// </summary>
        case_PList,
        /// <summary>
        /// &lt;VList&gt; ::= &lt;V&gt; &lt;VOpt&gt;;
        /// </summary>
        case_VList,
        /// <summary>
        /// &lt;V&gt; ::= &lt;Vn&gt; | &lt;Vt&gt;;
        /// </summary>
        case_V,
        /// <summary>
        /// &lt;VOpt&gt; ::= &lt;V&gt; &lt;VOpt&gt; | &quot;|&quot; &lt;V&gt; &lt;VOpt&gt; | null;
        /// </summary>
        case_VOpt,
        /// <summary>
        /// &lt;Vn&gt; ::= &quot;&lt;&quot; identifier &quot;&gt;&quot;;
        /// </summary>
        case_Vn,
        /// <summary>
        /// &lt;Vt&gt; ::= &quot;null&quot; | &quot;identifier&quot; | &quot;number&quot; | &quot;constString&quot; | constString;
        /// </summary>
        case_Vt,
        /// <summary>
        /// &quot;::=&quot;
        /// </summary>
        tail_colon_Colon_Equality_Leave,
        /// <summary>
        /// &quot;;&quot;
        /// </summary>
        tail_semicolon_Leave,
        /// <summary>
        /// null
        /// </summary>
        epsilonLeave,
        /// <summary>
        /// &quot;|&quot;
        /// </summary>
        tail_or_Leave,
        /// <summary>
        /// &quot;&lt;&quot;
        /// </summary>
        tail_lessThan_Leave,
        /// <summary>
        /// identifier
        /// </summary>
        identifierLeave,
        /// <summary>
        /// &quot;&gt;&quot;
        /// </summary>
        tail_greaterThan_Leave,
        /// <summary>
        /// &quot;null&quot;
        /// </summary>
        tail_nullLeave,
        /// <summary>
        /// &quot;identifier&quot;
        /// </summary>
        tail_identifierLeave,
        /// <summary>
        /// &quot;number&quot;
        /// </summary>
        tail_numberLeave,
        /// <summary>
        /// &quot;constString&quot;
        /// </summary>
        tail_constStringLeave,
        /// <summary>
        /// constString
        /// </summary>
        constStringLeave,
        /// <summary>
        /// #
        /// </summary>
        tail_startEndLeave,
    }

}

