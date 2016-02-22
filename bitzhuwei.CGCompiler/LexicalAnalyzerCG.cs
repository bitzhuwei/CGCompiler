using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// CG的词法分析器
    /// </summary>
    public partial class LexicalAnalyzerCG : LexicalAnalyzerBase<EnumTokenTypeCG>
    {
        /// <summary>
        /// CG的词法分析器
        /// </summary>
        public LexicalAnalyzerCG()
        { }
        /// <summary>
        /// CG的词法分析器
        /// </summary>
        /// <param name="sourceCode">要分析的源代码</param>
        public LexicalAnalyzerCG(string sourceCode)
            : base(sourceCode)
        { }
        /// <summary>
        /// 从<code>PtNextLetter</code>开始获取下一个<code>Token</code>
        /// </summary>
        /// <returns></returns>
        protected override Token<EnumTokenTypeCG> NextToken()
        {
            var result = new Token<EnumTokenTypeCG>();
            result.Line = m_CurrentLine;
            result.Column = m_CurrentColumn;
            result.IndexOfSourceCode = PtNextLetter;
            var count = this.GetSourceCode().Length;
            if (PtNextLetter < 0 || PtNextLetter >= count) return result;
            var gotToken = false;
            var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);
            switch (ct)
            {
                case EnumCharTypeCG.Colon:
                    gotToken = GetColon(result);
                    break;
                case EnumCharTypeCG.Semicolon:
                    gotToken = GetSemicolon(result);
                    break;
                case EnumCharTypeCG.Or:
                    gotToken = GetOr(result);
                    break;
                case EnumCharTypeCG.LessThan:
                    gotToken = GetLessThan(result);
                    break;
                case EnumCharTypeCG.Letter:
                    gotToken = GetIdentifier(result);
                    break;
                case EnumCharTypeCG.GreaterThan:
                    gotToken = GetGreaterThan(result);
                    break;
                case EnumCharTypeCG.Number:
                    gotToken = GetConstentNumber(result);
                    break;
                case EnumCharTypeCG.DoubleQuotation:
                    gotToken = GetConstentString(result);
                    break;
                case EnumCharTypeCG.Space:
                    gotToken = GetSpace(result);
                    break;
                default:
                    gotToken = GetUnknown(result);
                    break;
            }
            if (gotToken)
            {
                result.Length = PtNextLetter - result.IndexOfSourceCode;
                return result;
            }
            else
                return null;
        }
        #region 获取某类型的单词
        /// <summary>
        /// )
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetColon(Token<EnumTokenTypeCG> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Colon
            //Mapped nodes:
            //    "::="
            //result.TokenType = EnumTokenTypeCG.token_Colon_Colon_Equality_;
            if (PtNextLetter + 3 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 3);
                if ("::=" == str)
                {
                    result.TokenType = EnumTokenTypeCG.token_Colon_Colon_Equality_;
                    result.Detail = str;
                    PtNextLetter += 3;
                    return true;
                }
            }
            
            return false;
        }
        /// <summary>
        /// )
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetSemicolon(Token<EnumTokenTypeCG> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Semicolon
            //Mapped nodes:
            //    ";"
            //result.TokenType = EnumTokenTypeCG.token_Semicolon_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (";" == str)
                {
                    result.TokenType = EnumTokenTypeCG.token_Semicolon_;
                    result.Detail = str;
                    PtNextLetter += 1;
                    return true;
                }
            }
            
            return false;
        }
        /// <summary>
        /// )
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetOr(Token<EnumTokenTypeCG> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Or
            //Mapped nodes:
            //    "|"
            //result.TokenType = EnumTokenTypeCG.token_Or_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("|" == str)
                {
                    result.TokenType = EnumTokenTypeCG.token_Or_;
                    result.Detail = str;
                    PtNextLetter += 1;
                    return true;
                }
            }
            
            return false;
        }
        /// <summary>
        /// )
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetLessThan(Token<EnumTokenTypeCG> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: LessThan
            //Mapped nodes:
            //    "<"
            //result.TokenType = EnumTokenTypeCG.token_LessThan_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("<" == str)
                {
                    result.TokenType = EnumTokenTypeCG.token_LessThan_;
                    result.Detail = str;
                    PtNextLetter += 1;
                    return true;
                }
            }
            
            return false;
        }
        /// <summary>
        /// )
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetGreaterThan(Token<EnumTokenTypeCG> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: GreaterThan
            //Mapped nodes:
            //    ">"
            //result.TokenType = EnumTokenTypeCG.token_GreaterThan_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (">" == str)
                {
                    result.TokenType = EnumTokenTypeCG.token_GreaterThan_;
                    result.Detail = str;
                    PtNextLetter += 1;
                    return true;
                }
            }
            
            return false;
        }
        #region GetIdentifier
        /// <summary>
        /// 获取标识符（函数名，变量名，等）
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetIdentifier(Token<EnumTokenTypeCG> result)
        {
            result.TokenType = EnumTokenTypeCG.identifier;
            StringBuilder builder = new StringBuilder();
            while (PtNextLetter < this.GetSourceCode().Length)
            {
                var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);
                if (ct == EnumCharTypeCG.Letter
                    || ct == EnumCharTypeCG.Number
                    || ct == EnumCharTypeCG.UnderLine
                    || ct == EnumCharTypeCG.ChineseLetter)
                {
                    builder.Append(this.GetSourceCode()[PtNextLetter]);
                    PtNextLetter++;
                }
                else
                    break;
            }
            result.Detail = builder.ToString();
            // specify if this string is a keyword
            foreach (var item in LexicalAnalyzerCG.keywords)
            {
                if (item.ToString().Substring(5) == result.Detail)
                {
                    result.TokenType = item;
                    break;
                }
            }
            return true;
        }
        
        public static readonly List<EnumTokenTypeCG> keywords = new List<EnumTokenTypeCG>()
        {
            EnumTokenTypeCG.token_null,
            EnumTokenTypeCG.token_identifier,
            EnumTokenTypeCG.token_number,
            EnumTokenTypeCG.token_constString,
        };
        
        #endregion GetIdentifier
        #region GetConstentNumber
        /// <summary>
        /// 数值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentNumber(Token<EnumTokenTypeCG> result)
        {
            result.TokenType = EnumTokenTypeCG.number;
            if (this.GetSourceCode()[PtNextLetter] == '0')//可能是八进制或十六进制数
            {
                if (PtNextLetter + 1 < this.GetSourceCode().Length)
                {
                    char c = this.GetSourceCode()[PtNextLetter + 1];
                    if (c == 'x' || c == 'X')
                    {//十六进制数
                        return GetConstentHexadecimalNumber(result);
                    }
                    else if (GetCharType(c) == EnumCharTypeCG.Number)
                    {//八进制数
                        return GetConstentOctonaryNumber(result);
                    }
                    else//十进制数
                    {
                        return GetConstentDecimalNumber(result);
                    }
                }
                else
                {//源代码最后一个字符 0
                    result.Detail = "0";//0
                    PtNextLetter++;
                    return true;
                }
            }
            else//十进制数
            {
                return GetConstentDecimalNumber(result);
            }
        }
        /// <summary>
        /// 十进制数
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentDecimalNumber(Token<EnumTokenTypeCG> result)
        {
            char c;
            StringBuilder tag = new StringBuilder();
            c = this.GetSourceCode()[PtNextLetter];
            string numberSerial1, numberSerial2, numberSerial3;
            numberSerial1 = GetNumberSerial(this.GetSourceCode(), 10);
            tag.Append(numberSerial1);
            result.LexicalError = string.IsNullOrEmpty(numberSerial1);
            if (PtNextLetter < this.GetSourceCode().Length)
            {
                c = this.GetSourceCode()[PtNextLetter];
                if (c == 'l' || c == 'L')
                {
                    tag.Append(c);
                    PtNextLetter++;
                }
                if (c == '.')
                {
                    tag.Append(c);
                    PtNextLetter++;
                    numberSerial2 = GetNumberSerial(this.GetSourceCode(), 10);
                    tag.Append(numberSerial2);
                    result.LexicalError = result.LexicalError || string.IsNullOrEmpty(numberSerial2);
                    if (PtNextLetter < this.GetSourceCode().Length)
                    {
                        c = this.GetSourceCode()[PtNextLetter];
                    }
                }
                if (c == 'e' || c == 'E')
                {
                    tag.Append(c);
                    PtNextLetter++;
                    if (PtNextLetter < this.GetSourceCode().Length)
                    {
                        c = this.GetSourceCode()[PtNextLetter];
                        if (c == '+' || c == '-')
                        {
                            tag.Append(c);
                            PtNextLetter++;
                        }
                    }
                    numberSerial3 = GetNumberSerial(this.GetSourceCode(), 10);
                    tag.Append(numberSerial3);
                    result.LexicalError = result.LexicalError || string.IsNullOrEmpty(numberSerial3);
                }
            }
            result.Detail = tag.ToString();
            if (result.LexicalError)
            {
                result.Tag = string.Format("十进制数[{0}]格式错误，无法解析。", tag.ToString());
            }
            return true;
        }
        /// <summary>
        /// 八进制数
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentOctonaryNumber(Token<EnumTokenTypeCG> result)
        {
            char c;
            StringBuilder tag = new StringBuilder(this.GetSourceCode().Substring(PtNextLetter, 1));
            PtNextLetter++;
            string numberSerial = GetNumberSerial(this.GetSourceCode(), 8);
            tag.Append(numberSerial);
            if (PtNextLetter < this.GetSourceCode().Length)
            {
                c = this.GetSourceCode()[PtNextLetter];
                if (c == 'l' || c == 'L')
                {
                    tag.Append(c);
                    PtNextLetter++;
                }
            }
            result.Detail = tag.ToString();
            if (string.IsNullOrEmpty(numberSerial))
            {
                result.LexicalError = true;
                result.Tag = string.Format("八进制数[{0}]格式错误，无法解析。", tag.ToString());
                return false;
            }
            return true;
        }
        /// <summary>
        /// 十六进制数
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentHexadecimalNumber(Token<EnumTokenTypeCG> result)
        {
            char c;
            StringBuilder tag = new StringBuilder(this.GetSourceCode().Substring(PtNextLetter, 2));
            PtNextLetter += 2;
            string numberSerial = GetNumberSerial(this.GetSourceCode(), 16);
            tag.Append(numberSerial);
            if (PtNextLetter < this.GetSourceCode().Length)
            {
                c = this.GetSourceCode()[PtNextLetter];
                if (c == 'l' || c == 'L')
                {
                    tag.Append(c);
                    PtNextLetter++;
                }
            }
            result.Detail = tag.ToString();
            if (string.IsNullOrEmpty(numberSerial))
            {
                result.LexicalError = true;
                result.Tag = string.Format("十六进制数[{0}]格式错误。", tag.ToString());
                return false;
            }
            return true;
        }
        /// <summary>
        /// 数字序列
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <param name="scale">进制</param>
        /// <returns></returns>
        protected virtual string GetNumberSerial(string sourceCode, int scale)
        {
            if (scale == 10)
            {
                return GetNumberSerialDecimal(this.GetSourceCode());
            }
            if (scale == 16)
            {
                return GetNumberSerialHexadecimal(this.GetSourceCode());
            }
            if (scale == 8)
            {
                return GetNumberSerialOctonary(this.GetSourceCode());
            }
            return string.Empty;
        }
        /// <summary>
        /// 十进制数序列
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        protected virtual string GetNumberSerialDecimal(string sourceCode)
        {
            StringBuilder result = new StringBuilder(String.Empty);
            char c;
            while (PtNextLetter < this.GetSourceCode().Length)
            {
                c = this.GetSourceCode()[PtNextLetter];
                if ('0' <= c && c <= '9')
                {
                    result.Append(c);
                    PtNextLetter++;
                }
                else
                    break;
            }
            return result.ToString();
        }
        /// <summary>
        /// 八进制数序列
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        protected virtual string GetNumberSerialOctonary(string sourceCode)
        {
            StringBuilder result = new StringBuilder(String.Empty);
            char c;
            while (PtNextLetter < this.GetSourceCode().Length)
            {
                c = this.GetSourceCode()[PtNextLetter];
                if ('0' <= c && c <= '7')
                {
                    result.Append(c);
                    PtNextLetter++;
                }
                else
                    break;
            }
            return result.ToString();
        }
        /// <summary>
        /// 十六进制数序列（不包括0x前缀）
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <returns></returns>
        protected virtual string GetNumberSerialHexadecimal(string sourceCode)
        {
            StringBuilder result = new StringBuilder(String.Empty);
            char c;
            while (PtNextLetter < this.GetSourceCode().Length)
            {
                c = this.GetSourceCode()[PtNextLetter];
                if (('0' <= c && c <= '9')
                || ('a' <= c && c <= 'f')
                || ('A' <= c && c <= 'F'))
                {
                    result.Append(c);
                    PtNextLetter++;
                }
                else
                    break;
            }
            return result.ToString();
        }
        #endregion GetConstentNumber
        /// <summary>
        /// 字符串常量 "XXX"
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentString(Token<EnumTokenTypeCG> result)
        {
            result.TokenType = EnumTokenTypeCG.constString;
            int count = this.GetSourceCode().Length;
            StringBuilder constString = new StringBuilder("\"");
            PtNextLetter++;
            bool notMatched = true;
            char c;
            while ((PtNextLetter < count) && notMatched)
            {
                c = this.GetSourceCode()[PtNextLetter];
                if (c == '"')
                {
                    constString.Append(c);
                    notMatched = false;
                    PtNextLetter++;
                }
                else if (c == '\r' || c == '\n')
                {
                    break;
                }
                else
                {
                    constString.Append(c);
                    PtNextLetter++;
                }
            }
            result.Detail = constString.ToString();
            return true;
        }
        /// <summary>
        /// 未知符号
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetUnknown(Token<EnumTokenTypeCG> result)
        {
            result.TokenType = EnumTokenTypeCG.unknown;
            result.Detail = this.GetSourceCode()[PtNextLetter].ToString();
            result.LexicalError = true;
            result.Tag = string.Format("发现未知字符[{0}]。", result.Detail);
            PtNextLetter++;
            return true;
        }
        /// <summary>
        /// space tab \r \n
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetSpace(Token<EnumTokenTypeCG> result)
        {
            char c = this.GetSourceCode()[PtNextLetter];
            PtNextLetter++;
            if (c == '\n')// || c == '\r') //换行：Windows：\r\n Linux：\n
            {
                this.m_CurrentLine++;
                this.m_CurrentColumn = 0;
            }
            return false;
        }
        /// <summary>
        /// 跳过多行注释
        /// </summary>
        /// <returns></returns>
        protected virtual void SkipMultilineNote()
        {
            int count = this.GetSourceCode().Length;
            while (PtNextLetter < count)
            {
                if (GetSourceCode()[PtNextLetter] == '*')
                {
                    PtNextLetter++;
                    if (PtNextLetter < count)
                    {
                        if (GetSourceCode()[PtNextLetter] == '/')
                        {
                            PtNextLetter++;
                            break;
                        }
                        else
                            PtNextLetter++;
                    }
                }
                else
                    PtNextLetter++;
            }
        }
        /// <summary>
        /// 跳过单行注释
        /// </summary>
        /// <returns></returns>
        protected virtual void SkipSingleLineNote()
        {
            int count = this.GetSourceCode().Length;
            char cNext;
            while (PtNextLetter < count)
            {
                cNext = GetSourceCode()[PtNextLetter];
                if (cNext == '\r' || cNext == '\n')
                {
                    break;
                }
                PtNextLetter++;
            }
        }
        #endregion 获取某类型的单词
        /// <summary>
        /// 获取字符类型
        /// </summary>
        /// <param name="c">要归类的字符</param>
        /// <returns></returns>
        protected virtual EnumCharTypeCG GetCharType(char c)
        {
            if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')) return EnumCharTypeCG.Letter;
            if ('0' <= c && c <= '9') return EnumCharTypeCG.Number;
            if (c == '_') return EnumCharTypeCG.UnderLine;
            if (c == '.') return EnumCharTypeCG.Dot;
            if (c == ',') return EnumCharTypeCG.Comma;
            if (c == '+') return EnumCharTypeCG.Plus;
            if (c == '-') return EnumCharTypeCG.Minus;
            if (c == '*') return EnumCharTypeCG.Multiply;
            if (c == '/') return EnumCharTypeCG.Divide;
            if (c == '%') return EnumCharTypeCG.Percent;
            if (c == '^') return EnumCharTypeCG.Xor;
            if (c == '&') return EnumCharTypeCG.And;
            if (c == '|') return EnumCharTypeCG.Or;
            if (c == '~') return EnumCharTypeCG.Reverse;
            if (c == '$') return EnumCharTypeCG.Dollar;
            if (c == '<') return EnumCharTypeCG.LessThan;
            if (c == '>') return EnumCharTypeCG.GreaterThan;
            if (c == '(') return EnumCharTypeCG.LeftParentheses;
            if (c == ')') return EnumCharTypeCG.RightParentheses;
            if (c == '[') return EnumCharTypeCG.LeftBracket;
            if (c == ']') return EnumCharTypeCG.RightBracket;
            if (c == '{') return EnumCharTypeCG.LeftBrace;
            if (c == '}') return EnumCharTypeCG.RightBrace;
            if (c == '!') return EnumCharTypeCG.Not;
            if (c == '#') return EnumCharTypeCG.Pound;
            if (c == '\\') return EnumCharTypeCG.Slash;
            if (c == '?') return EnumCharTypeCG.Question;
            if (c == '\'') return EnumCharTypeCG.Quotation;
            if (c == '"') return EnumCharTypeCG.DoubleQuotation;
            if (c == ':') return EnumCharTypeCG.Colon;
            if (c == ';') return EnumCharTypeCG.Semicolon;
            if (c == '=') return EnumCharTypeCG.Equality;
            if (regChineseLetter.IsMatch(Convert.ToString(c))) return EnumCharTypeCG.ChineseLetter;
            if (c == ' ' || c == '\t' || c == '\r' || c == '\n') return EnumCharTypeCG.Space;
            return EnumCharTypeCG.Unknown;
        }
        /// <summary>
        /// 汉字 new Regex("^[^\x00-\xFF]")
        /// </summary>
        private static readonly Regex regChineseLetter = new Regex("^[^\x00-\xFF]");
    }

}

