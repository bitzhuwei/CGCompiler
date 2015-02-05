namespace bitzhuwei.ExpressionCompiler
{
    /// <summary>
    /// 文法Expression的单词枚举类型
    /// </summary>
    public enum EnumTokenTypeExpression
    {
        /// <summary>
        /// 未知的单词符号
        /// </summary>
        unknown,
        /// <summary>
        /// &quot;+&quot;
        /// </summary>
        token_Plus_,
        /// <summary>
        /// &quot;-&quot;
        /// </summary>
        token_Minus_,
        /// <summary>
        /// null
        /// </summary>
        epsilon,
        /// <summary>
        /// &quot;*&quot;
        /// </summary>
        token_Multiply_,
        /// <summary>
        /// &quot;/&quot;
        /// </summary>
        token_Divide_,
        /// <summary>
        /// number
        /// </summary>
        number,
        /// <summary>
        /// &quot;(&quot;
        /// </summary>
        token_LeftParentheses_,
        /// <summary>
        /// &quot;)&quot;
        /// </summary>
        token_RightParentheses_,
        /// <summary>
        /// #
        /// </summary>
        token_startEnd,
    }

}

