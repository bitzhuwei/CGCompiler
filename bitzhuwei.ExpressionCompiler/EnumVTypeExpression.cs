namespace bitzhuwei.ExpressionCompiler
{
    /// <summary>
    /// 文法Expression的语法树结点枚举类型
    /// </summary>
    public enum EnumVTypeExpression
    {
        /// <summary>
        /// 未知的语法结点符号
        /// </summary>
        Unknown,
        /// <summary>
        /// &lt;Expression&gt; ::= &lt;Multiply&gt; &lt;PlusOpt&gt;;
        /// </summary>
        case_Expression,
        /// <summary>
        /// &lt;PlusOpt&gt; ::= &quot;+&quot; &lt;Multiply&gt; | &quot;-&quot; &lt;Multiply&gt; | null;
        /// </summary>
        case_PlusOpt,
        /// <summary>
        /// &lt;Multiply&gt; ::= &lt;Unit&gt; &lt;MultiplyOpt&gt;;
        /// </summary>
        case_Multiply,
        /// <summary>
        /// &lt;MultiplyOpt&gt; ::= &quot;*&quot; &lt;Unit&gt; | &quot;/&quot; &lt;Unit&gt; | null;
        /// </summary>
        case_MultiplyOpt,
        /// <summary>
        /// &lt;Unit&gt; ::= number | &quot;(&quot; &lt;Expression&gt; &quot;)&quot;;
        /// </summary>
        case_Unit,
        /// <summary>
        /// &quot;+&quot;
        /// </summary>
        tail_plus_Leave,
        /// <summary>
        /// &quot;-&quot;
        /// </summary>
        tail_minus_Leave,
        /// <summary>
        /// null
        /// </summary>
        epsilonLeave,
        /// <summary>
        /// &quot;*&quot;
        /// </summary>
        tail_multiply_Leave,
        /// <summary>
        /// &quot;/&quot;
        /// </summary>
        tail_divide_Leave,
        /// <summary>
        /// number
        /// </summary>
        numberLeave,
        /// <summary>
        /// &quot;(&quot;
        /// </summary>
        tail_leftParentheses_Leave,
        /// <summary>
        /// &quot;)&quot;
        /// </summary>
        tail_rightParentheses_Leave,
        /// <summary>
        /// #
        /// </summary>
        tail_startEndLeave,
    }

}

