namespace bitzhuwei.LevelCompiler
{
    /// <summary>
    /// 文法LevelCompiler的语法树结点枚举类型
    /// </summary>
    public enum EnumVTypeLevelCompiler
    {
        /// <summary>
        /// 未知的语法结点符号
        /// </summary>
        Unknown,
        /// <summary>
        /// &lt;Level&gt; ::= &quot;level&quot; &quot;{&quot; &lt;StepList&gt; &quot;}&quot;;
        /// </summary>
        case_Level,
        /// <summary>
        /// &lt;StepList&gt; ::= &lt;Step&gt; &lt;StepList&gt; | null;
        /// </summary>
        case_StepList,
        /// <summary>
        /// &lt;Step&gt; ::= &quot;step&quot; &quot;{&quot; &lt;TankList&gt; &quot;}&quot;;
        /// </summary>
        case_Step,
        /// <summary>
        /// &lt;TankList&gt; ::= &lt;Tank&gt; &lt;TankList&gt; | null;
        /// </summary>
        case_TankList,
        /// <summary>
        /// &lt;Tank&gt; ::= &quot;tank&quot; &quot;{&quot; &lt;TankPrefab&gt; &lt;BornPoint&gt; &quot;}&quot;;
        /// </summary>
        case_Tank,
        /// <summary>
        /// &lt;TankPrefab&gt; ::= number;
        /// </summary>
        case_TankPrefab,
        /// <summary>
        /// &lt;BornPoint&gt; ::= number;
        /// </summary>
        case_BornPoint,
        /// <summary>
        /// &quot;level&quot;
        /// </summary>
        tail_levelLeave,
        /// <summary>
        /// &quot;{&quot;
        /// </summary>
        tail_leftBrace_Leave,
        /// <summary>
        /// &quot;}&quot;
        /// </summary>
        tail_rightBrace_Leave,
        /// <summary>
        /// null
        /// </summary>
        epsilonLeave,
        /// <summary>
        /// &quot;step&quot;
        /// </summary>
        tail_stepLeave,
        /// <summary>
        /// &quot;tank&quot;
        /// </summary>
        tail_tankLeave,
        /// <summary>
        /// number
        /// </summary>
        numberLeave,
        /// <summary>
        /// #
        /// </summary>
        tail_startEndLeave,
    }

}

