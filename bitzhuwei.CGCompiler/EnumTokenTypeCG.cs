namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 文法CG的单词枚举类型
    /// </summary>
    public enum EnumTokenTypeCG
    {
        /// <summary>
        /// 未知的单词符号
        /// </summary>
        unknown,
        /// <summary>
        /// &quot;::=&quot;
        /// </summary>
        token_Colon_Colon_Equality_,
        /// <summary>
        /// &quot;;&quot;
        /// </summary>
        token_Semicolon_,
        /// <summary>
        /// null
        /// </summary>
        epsilon,
        /// <summary>
        /// &quot;|&quot;
        /// </summary>
        token_Or_,
        /// <summary>
        /// &quot;&lt;&quot;
        /// </summary>
        token_LessThan_,
        /// <summary>
        /// identifier
        /// </summary>
        identifier,
        /// <summary>
        /// &quot;&gt;&quot;
        /// </summary>
        token_GreaterThan_,
        /// <summary>
        /// &quot;null&quot;
        /// </summary>
        token_null,
        /// <summary>
        /// &quot;identifier&quot;
        /// </summary>
        token_identifier,
        /// <summary>
        /// &quot;number&quot;
        /// </summary>
        token_number,
        /// <summary>
        /// &quot;constString&quot;
        /// </summary>
        token_constString,
        /// <summary>
        /// constString
        /// </summary>
        constString,
        /// <summary>
        /// #
        /// </summary>
        token_startEnd,
    }

}

