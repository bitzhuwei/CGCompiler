namespace bitzhuwei.LR1DemoCompiler
{
    /// <summary>
    /// 文法LR1Demo的单词枚举类型
    /// </summary>
    public enum EnumTokenTypeLR1Demo
    {
        /// <summary>
        /// 未知的单词符号
        /// </summary>
        unknown,
        /// <summary>
        /// &quot;;&quot;
        /// </summary>
        token_Semicolon_,
        /// <summary>
        /// identifier
        /// </summary>
        identifier,
        /// <summary>
        /// &quot;:=&quot;
        /// </summary>
        token_Colon_Equality_,
        /// <summary>
        /// &quot;print&quot;
        /// </summary>
        token_print,
        /// <summary>
        /// &quot;(&quot;
        /// </summary>
        token_LeftParentheses_,
        /// <summary>
        /// &quot;)&quot;
        /// </summary>
        token_RightParentheses_,
        /// <summary>
        /// number
        /// </summary>
        number,
        /// <summary>
        /// &quot;+&quot;
        /// </summary>
        token_Plus_,
        /// <summary>
        /// &quot;,&quot;
        /// </summary>
        token_Comma_,
        /// <summary>
        /// #
        /// </summary>
        token_startEnd,
    }

}

