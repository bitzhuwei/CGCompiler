namespace bitzhuwei.LR1DemoCompiler
{
    /// <summary>
    /// 文法LR1Demo的语法树结点枚举类型
    /// </summary>
    public enum EnumVTypeLR1Demo
    {
        /// <summary>
        /// 未知的语法结点符号
        /// </summary>
        Unknown,
        /// <summary>
        /// &lt;Start&gt; ::= &lt;Start&gt; &quot;;&quot; &lt;Start&gt; | identifier &quot;:=&quot; &lt;Expression&gt; | &quot;print&quot; &quot;(&quot; &lt;Line&gt; &quot;)&quot;;
        /// </summary>
        case_Start,
        /// <summary>
        /// &lt;Expression&gt; ::= identifier | number | &lt;Expression&gt; &quot;+&quot; &lt;Expression&gt; | &quot;(&quot; &lt;Start&gt; &quot;,&quot; &lt;Expression&gt; &quot;)&quot;;
        /// </summary>
        case_Expression,
        /// <summary>
        /// &lt;Line&gt; ::= &lt;Expression&gt; | &lt;Line&gt; &quot;,&quot; &lt;Expression&gt;;
        /// </summary>
        case_Line,
        /// <summary>
        /// &quot;;&quot;
        /// </summary>
        tail_semicolon_Leave,
        /// <summary>
        /// identifier
        /// </summary>
        identifierLeave,
        /// <summary>
        /// &quot;:=&quot;
        /// </summary>
        tail_colon_Equality_Leave,
        /// <summary>
        /// &quot;print&quot;
        /// </summary>
        tail_printLeave,
        /// <summary>
        /// &quot;(&quot;
        /// </summary>
        tail_leftParentheses_Leave,
        /// <summary>
        /// &quot;)&quot;
        /// </summary>
        tail_rightParentheses_Leave,
        /// <summary>
        /// number
        /// </summary>
        numberLeave,
        /// <summary>
        /// &quot;+&quot;
        /// </summary>
        tail_plus_Leave,
        /// <summary>
        /// &quot;,&quot;
        /// </summary>
        tail_comma_Leave,
        /// <summary>
        /// #
        /// </summary>
        tail_startEndLeave,
    }

}

