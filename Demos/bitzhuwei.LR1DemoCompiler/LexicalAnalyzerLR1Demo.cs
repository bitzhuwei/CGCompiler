using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.LR1DemoCompiler
{
    /// <summary>
    /// LR1Demo的词法分析器
    /// </summary>
    public partial class LexicalAnalyzerLR1Demo : LexicalAnalyzerBase<EnumTokenTypeLR1Demo>
    {
        /// <summary>
        /// LR1Demo的词法分析器
        /// </summary>
        public LexicalAnalyzerLR1Demo()
        { }
        /// <summary>
        /// LR1Demo的词法分析器
        /// </summary>
        /// <param name="sourceCode">要分析的源代码</param>
        public LexicalAnalyzerLR1Demo(string sourceCode)
            : base(sourceCode)
        { }
        /// <summary>
        /// 从<code>PtNextLetter</code>开始获取下一个<code>Token</code>
        /// </summary>
        /// <returns></returns>
        protected override Token<EnumTokenTypeLR1Demo> NextToken()
        {
            var result = new Token<EnumTokenTypeLR1Demo>();
            result.Line = m_CurrentLine;
            result.Column = m_CurrentColumn;
            result.IndexOfSourceCode = PtNextLetter;
            var count = this.GetSourceCode().Length;
            if (PtNextLetter < 0 || PtNextLetter >= count) return result;
            var gotToken = false;
            var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);
            switch (ct)
            {
                case EnumCharTypeLR1Demo.Semicolon:
                    gotToken = GetSemicolon(result);
                    break;
                case EnumCharTypeLR1Demo.Letter:
                    gotToken = GetIdentifier(result);
                    break;
                case EnumCharTypeLR1Demo.Colon:
                    gotToken = GetColon(result);
                    break;
                case EnumCharTypeLR1Demo.LeftParentheses:
                    gotToken = GetLeftParentheses(result);
                    break;
                case EnumCharTypeLR1Demo.RightParentheses:
                    gotToken = GetRightParentheses(result);
                    break;
                case EnumCharTypeLR1Demo.Number:
                    gotToken = GetConstentNumber(result);
                    break;
                case EnumCharTypeLR1Demo.Plus:
                    gotToken = GetPlus(result);
                    break;
                case EnumCharTypeLR1Demo.Comma:
                    gotToken = GetComma(result);
                    break;
                case EnumCharTypeLR1Demo.Space:
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
        protected virtual bool GetSemicolon(Token<EnumTokenTypeLR1Demo> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Semicolon
            //Mapped nodes:
            //    ";"
            //result.TokenType = EnumTokenTypeLR1Demo.token_Semicolon_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (";" == str)
                {
                    result.TokenType = EnumTokenTypeLR1Demo.token_Semicolon_;
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
        protected virtual bool GetColon(Token<EnumTokenTypeLR1Demo> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Colon
            //Mapped nodes:
            //    ":="
            //result.TokenType = EnumTokenTypeLR1Demo.token_Colon_Equality_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if (":=" == str)
                {
                    result.TokenType = EnumTokenTypeLR1Demo.token_Colon_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
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
        protected virtual bool GetLeftParentheses(Token<EnumTokenTypeLR1Demo> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: LeftParentheses
            //Mapped nodes:
            //    "("
            //result.TokenType = EnumTokenTypeLR1Demo.token_LeftParentheses_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("(" == str)
                {
                    result.TokenType = EnumTokenTypeLR1Demo.token_LeftParentheses_;
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
        protected virtual bool GetRightParentheses(Token<EnumTokenTypeLR1Demo> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: RightParentheses
            //Mapped nodes:
            //    ")"
            //result.TokenType = EnumTokenTypeLR1Demo.token_RightParentheses_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (")" == str)
                {
                    result.TokenType = EnumTokenTypeLR1Demo.token_RightParentheses_;
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
        protected virtual bool GetPlus(Token<EnumTokenTypeLR1Demo> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Plus
            //Mapped nodes:
            //    "+"
            //result.TokenType = EnumTokenTypeLR1Demo.token_Plus_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("+" == str)
                {
                    result.TokenType = EnumTokenTypeLR1Demo.token_Plus_;
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
        protected virtual bool GetComma(Token<EnumTokenTypeLR1Demo> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Comma
            //Mapped nodes:
            //    ","
            //result.TokenType = EnumTokenTypeLR1Demo.token_Comma_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("," == str)
                {
                    result.TokenType = EnumTokenTypeLR1Demo.token_Comma_;
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
        protected virtual bool GetIdentifier(Token<EnumTokenTypeLR1Demo> result)
        {
            result.TokenType = EnumTokenTypeLR1Demo.identifier;
            StringBuilder builder = new StringBuilder();
            while (PtNextLetter < this.GetSourceCode().Length)
            {
                var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);
                if (ct == EnumCharTypeLR1Demo.Letter
                    || ct == EnumCharTypeLR1Demo.Number
                    || ct == EnumCharTypeLR1Demo.UnderLine
                    || ct == EnumCharTypeLR1Demo.ChineseLetter)
                {
                    builder.Append(this.GetSourceCode()[PtNextLetter]);
                    PtNextLetter++;
                }
                else
                { break; }
            }
            result.Detail = builder.ToString();
            // specify if this string is a keyword
            foreach (var item in LexicalAnalyzerLR1Demo.keywords)
            {
                if (item.ToString().Substring(6) == result.Detail)
                {
                    result.TokenType = item;
                    break;
                }
            }
            return true;
        }
        
        public static readonly IEnumerable<EnumTokenTypeLR1Demo> keywords = new List<EnumTokenTypeLR1Demo>()
        {
            EnumTokenTypeLR1Demo.token_print,
        };
        
        #endregion GetIdentifier
        #region GetConstentNumber
        /// <summary>
        /// 数值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentNumber(Token<EnumTokenTypeLR1Demo> result)
        {
            result.TokenType = EnumTokenTypeLR1Demo.number;
            if (this.GetSourceCode()[PtNextLetter] == '0')//可能是八进制或十六进制数
            {
                if (PtNextLetter + 1 < this.GetSourceCode().Length)
                {
                    char c = this.GetSourceCode()[PtNextLetter + 1];
                    if (c == 'x' || c == 'X')
                    {//十六进制数
                        return GetConstentHexadecimalNumber(result);
                    }
                    else if (GetCharType(c) == EnumCharTypeLR1Demo.Number)
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
        protected virtual bool GetConstentDecimalNumber(Token<EnumTokenTypeLR1Demo> result)
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
        protected virtual bool GetConstentOctonaryNumber(Token<EnumTokenTypeLR1Demo> result)
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
        protected virtual bool GetConstentHexadecimalNumber(Token<EnumTokenTypeLR1Demo> result)
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
        /// 未知符号
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetUnknown(Token<EnumTokenTypeLR1Demo> result)
        {
            result.TokenType = EnumTokenTypeLR1Demo.unknown;
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
        protected virtual bool GetSpace(Token<EnumTokenTypeLR1Demo> result)
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
        protected virtual EnumCharTypeLR1Demo GetCharType(char c)
        {
            if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')) return EnumCharTypeLR1Demo.Letter;
            if ('0' <= c && c <= '9') return EnumCharTypeLR1Demo.Number;
            if (c == '_') return EnumCharTypeLR1Demo.UnderLine;
            if (c == '.') return EnumCharTypeLR1Demo.Dot;
            if (c == ',') return EnumCharTypeLR1Demo.Comma;
            if (c == '+') return EnumCharTypeLR1Demo.Plus;
            if (c == '-') return EnumCharTypeLR1Demo.Minus;
            if (c == '*') return EnumCharTypeLR1Demo.Multiply;
            if (c == '/') return EnumCharTypeLR1Demo.Divide;
            if (c == '%') return EnumCharTypeLR1Demo.Percent;
            if (c == '^') return EnumCharTypeLR1Demo.Xor;
            if (c == '&') return EnumCharTypeLR1Demo.And;
            if (c == '|') return EnumCharTypeLR1Demo.Or;
            if (c == '~') return EnumCharTypeLR1Demo.Reverse;
            if (c == '$') return EnumCharTypeLR1Demo.Dollar;
            if (c == '<') return EnumCharTypeLR1Demo.LessThan;
            if (c == '>') return EnumCharTypeLR1Demo.GreaterThan;
            if (c == '(') return EnumCharTypeLR1Demo.LeftParentheses;
            if (c == ')') return EnumCharTypeLR1Demo.RightParentheses;
            if (c == '[') return EnumCharTypeLR1Demo.LeftBracket;
            if (c == ']') return EnumCharTypeLR1Demo.RightBracket;
            if (c == '{') return EnumCharTypeLR1Demo.LeftBrace;
            if (c == '}') return EnumCharTypeLR1Demo.RightBrace;
            if (c == '!') return EnumCharTypeLR1Demo.Not;
            if (c == '#') return EnumCharTypeLR1Demo.Pound;
            if (c == '\\') return EnumCharTypeLR1Demo.Slash;
            if (c == '?') return EnumCharTypeLR1Demo.Question;
            if (c == '\'') return EnumCharTypeLR1Demo.Quotation;
            if (c == '"') return EnumCharTypeLR1Demo.DoubleQuotation;
            if (c == ':') return EnumCharTypeLR1Demo.Colon;
            if (c == ';') return EnumCharTypeLR1Demo.Semicolon;
            if (c == '=') return EnumCharTypeLR1Demo.Equality;
            if (c == '@') return EnumCharTypeLR1Demo.At;
            if (regChineseLetter.IsMatch(Convert.ToString(c))) return EnumCharTypeLR1Demo.ChineseLetter;
            if (c == ' ' || c == '\t' || c == '\r' || c == '\n') return EnumCharTypeLR1Demo.Space;
            return EnumCharTypeLR1Demo.Unknown;
        }
        /// <summary>
        /// 汉字 new Regex("^[^\x00-\xFF]")
        /// </summary>
        private static readonly Regex regChineseLetter = new Regex("^[^\x00-\xFF]");
    }
}

