using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 字符类型
    /// </summary>
    public enum EnumCharType
    {
        /// <summary>
        /// 未知字符
        /// </summary>
        Unknown,
        /// <summary>
        /// a-z A-Z
        /// </summary>
        Letter,//0
        /// <summary>
        /// 汉字
        /// </summary>
        ChineseLetter,
        /// <summary>
        /// 0 1 2 3 4 5 6 7 8 9
        /// </summary>
        Number,
        /// <summary>
        /// _
        /// </summary>
        UnderLine,
        /// <summary>
        /// .
        /// </summary>
        Dot,
        /// <summary>
        /// ,
        /// </summary>
        Comma,
        /// <summary>
        /// +
        /// </summary>
        Plus,//5
        /// <summary>
        /// -
        /// </summary>
        Minus,
        /// <summary>
        /// *
        /// </summary>
        Multiply,
        /// <summary>
        /// /
        /// </summary>
        Divide,
        /// <summary>
        /// %
        /// </summary>
        Percent,
        /// <summary>
        /// ^
        /// </summary>
        Xor,//10
        /// <summary>
        /// &amp;
        /// </summary>
        And,
        /// <summary>
        /// |
        /// </summary>
        Or,
        /// <summary>
        /// ~
        /// </summary>
        Reverse,
        /// <summary>
        /// $
        /// </summary>
        Dollar,
        /// <summary>
        /// &lt;
        /// </summary>
        LessThan,
        /// <summary>
        /// &gt;
        /// </summary>
        GreaterThan,
        /// <summary>
        /// (
        /// </summary>
        LeftParentheses,
        /// <summary>
        /// )
        /// </summary>
        RightParentheses,//15
        /// <summary>
        /// [
        /// </summary>
        LeftBracket,
        /// <summary>
        /// ]
        /// </summary>
        RightBracket,
        /// <summary>
        /// {
        /// </summary>
        LeftBrace,
        /// <summary>
        /// }
        /// </summary>
        RightBrace,
        /// <summary>
        /// !
        /// </summary>
        Not,//20
        /// <summary>
        /// #
        /// </summary>
        Pound,
        /// <summary>
        /// \
        /// </summary>
        Slash,
        /// <summary>
        /// ?
        /// </summary>
        Question,
        /// <summary>
        /// '
        /// </summary>
        Quotation,
        /// <summary>
        /// "
        /// </summary>
        DoubleQuotation,//25
        /// <summary>
        /// :
        /// </summary>
        Colon,
        /// <summary>
        /// ;
        /// </summary>
        Semicolon,
        /// <summary>
        /// =
        /// </summary>
        Equality,
        /// <summary>
        /// @
        /// </summary>
        At,
        /// <summary>
        /// space Tab \r\n
        /// </summary>
        Space,

    }
}
