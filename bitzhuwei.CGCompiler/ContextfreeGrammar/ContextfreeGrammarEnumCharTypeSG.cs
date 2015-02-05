using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 2型文法（上下文无关文法）
    /// </summary>
    public partial class ContextfreeGrammar : ICloneable
    {
        #region 生成整个字符枚举类型文件的源代码
        /// <summary>
        /// 生成整个<code>EnumCharTypeSG</code>枚举类型文件的源代码
        /// </summary>
        /// <returns></returns>
        public string GenerateEnumCharType()
        {
            int preSpace = 0;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateEnumCharType(ref preSpace, input);
        }
        /// <summary>
        /// 生成整个<code>EnumCharTypeSG</code>枚举类型文件的源代码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateEnumCharType(LL1GeneraterInput input)
        {
            int preSpace = 0;

            return GenerateEnumCharType(ref preSpace, input);
        }

        /// <summary>
        /// 生成整个<code>EnumCharTypeSG</code>枚举类型文件的源代码
        /// </summary>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateEnumCharType(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("namespace {0}", this.Namespace));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GenerateEnumCharTypeClass(ref preSpace, input));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace));
            return builder.ToString();
        }
        #endregion 生成整个字符枚举类型文件的源代码

        #region 生成字符枚举类型的源代码

        /// <summary>
        /// 生成<code>EnumCharTypeSG</code>的源码
        /// </summary>
        /// <returns></returns>
        public string GenerateEnumCharTypeClass()
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateEnumCharTypeClass(ref preSpace, input);
        }
        /// <summary>
        /// 生成<code>EnumCharTypeSG</code>的源码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateEnumCharTypeClass(LL1GeneraterInput input)
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;

            return GenerateEnumCharTypeClass(ref preSpace, input);
        }

        /// <summary>
        /// 生成<code>EnumCharTypeSG</code>的源码
        /// </summary>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateEnumCharTypeClass(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("/// 文法{0}的字符枚举类型", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("public enum {0}"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            GenerateEnumCharTypeItemUnknown(builder, ref preSpace, input);
            GenerateEnumCharTypeWithoutItemUnknown(builder, ref preSpace, input);

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            return builder.ToString();
        }
        #endregion 生成字符枚举类型的源代码

        /// <summary>
        /// 获取<code>EnumCharTypeSG</code>除默认项以外的选项
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateEnumCharTypeWithoutItemUnknown(
            StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// a-z A-Z");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Letter,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 汉字");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "ChineseLetter,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 0 1 2 3 4 5 6 7 8 9");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Number,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// _");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "UnderLine,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// .");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Dot,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// ,");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Comma,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// +");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Plus,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// -");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Minus,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// *");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Multiply,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// /");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Divide,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// %");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Percent,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// ^");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Xor,//10");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// &amp;");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "And,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// |");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Or,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// ~");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Reverse,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// $");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Dollar,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// &lt;");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "LessThan,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// &gt;");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "GreaterThan,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// (");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "LeftParentheses,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// )");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "RightParentheses,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// [");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "LeftBracket,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// ]");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "RightBracket,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// {");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "LeftBrace,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// }");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "RightBrace,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// !");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Not,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// #");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Pound,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// \\");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Slash,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// ?");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Question,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// '");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Quotation,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// \"");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "DoubleQuotation,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// :");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Colon,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// ;");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Semicolon,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// =");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Equality,");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// space Tab \\r\\n");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "Space,");
        }
        /// <summary>
        /// 获取<code>EnumCharTypeSG</code>的默认项
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateEnumCharTypeItemUnknown(
            StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// {0}", GetEnumCharTypeSGItemDefaultNote()));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0},"
                , GetEnumCharTypeSGItemDefaultName()));
        }

        private string GetEnumCharTypeSGItemDefaultName()
        {
            return "Unknown";
        }

        private string GetEnumCharTypeSGItemDefaultNote()
        {
            return "未知字符";            
        }

        
        /// <summary>
        /// 获取字符类型
        /// </summary>
        /// <param name="c">要归类的字符</param>
        /// <returns></returns>
        static EnumCharType GetCharType(char c)
        {
            if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')) return EnumCharType.Letter;
            if ('0' <= c && c <= '9') return EnumCharType.Number;
            if (c == '_') return EnumCharType.UnderLine;
            if (c == '.') return EnumCharType.Dot;
            if (c == ',') return EnumCharType.Comma;
            if (c == '+') return EnumCharType.Plus;
            if (c == '-') return EnumCharType.Minus;
            if (c == '*') return EnumCharType.Multiply;
            if (c == '/') return EnumCharType.Divide;
            if (c == '%') return EnumCharType.Percent;
            if (c == '^') return EnumCharType.Xor;
            if (c == '&') return EnumCharType.And;
            if (c == '|') return EnumCharType.Or;
            if (c == '~') return EnumCharType.Reverse;
            if (c == '$') return EnumCharType.Dollar;
            if (c == '<') return EnumCharType.LessThan;
            if (c == '>') return EnumCharType.GreaterThan;
            if (c == '(') return EnumCharType.LeftParentheses;
            if (c == ')') return EnumCharType.RightParentheses;
            if (c == '[') return EnumCharType.LeftBracket;
            if (c == ']') return EnumCharType.RightBracket;
            if (c == '{') return EnumCharType.LeftBrace;
            if (c == '}') return EnumCharType.RightBrace;
            if (c == '!') return EnumCharType.Not;
            if (c == '#') return EnumCharType.Pound;
            if (c == '\\') return EnumCharType.Slash;
            if (c == '?') return EnumCharType.Question;
            if (c == '\'') return EnumCharType.Quotation;
            if (c == '\"') return EnumCharType.DoubleQuotation;
            if (c == ':') return EnumCharType.Colon;
            if (c == ';') return EnumCharType.Semicolon;
            if (c == '=') return EnumCharType.Equality;
            if (regChineseLetter.IsMatch(Convert.ToString(c))) return EnumCharType.ChineseLetter;
            if (c == ' ' || c == '\t' || c == '\r' || c == '\n') return EnumCharType.Space;
            return EnumCharType.Unknown;

        }
        private static readonly Regex regChineseLetter = new Regex("^[^\x00-\xFF]");
    }
}
