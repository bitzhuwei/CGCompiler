using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.ExpressionCompiler
{
    /// <summary>
    /// Expression的词法分析器
    /// </summary>
    public partial class LexicalAnalyzerExpression : LexicalAnalyzerBase<EnumTokenTypeExpression>
    {
        /// <summary>
        /// Expression的词法分析器
        /// </summary>
        public LexicalAnalyzerExpression()
        { }
        /// <summary>
        /// Expression的词法分析器
        /// </summary>
        /// <param name="sourceCode">要分析的源代码</param>
        public LexicalAnalyzerExpression(string sourceCode)
            : base(sourceCode)
        { }
        /// <summary>
        /// 从<code>PtNextLetter</code>开始获取下一个<code>Token</code>
        /// </summary>
        /// <returns></returns>
        protected override Token<EnumTokenTypeExpression> NextToken()
        {
            var result = new Token<EnumTokenTypeExpression>();
            result.Line = m_CurrentLine;
            result.Column = m_CurrentColumn;
            result.IndexOfSourceCode = PtNextLetter;
            var count = this.GetSourceCode().Length;
            if (PtNextLetter < 0 || PtNextLetter >= count) return result;
            var gotToken = false;
            var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);
            switch (ct)
            {
                case EnumCharTypeExpression.Plus:
                    gotToken = GetPlus(result);
                    break;
                case EnumCharTypeExpression.Minus:
                    gotToken = GetMinus(result);
                    break;
                case EnumCharTypeExpression.Multiply:
                    gotToken = GetMultiply(result);
                    break;
                case EnumCharTypeExpression.Divide:
                    gotToken = GetDivide(result);
                    break;
                case EnumCharTypeExpression.Number:
                    gotToken = GetConstentNumber(result);
                    break;
                case EnumCharTypeExpression.LeftParentheses:
                    gotToken = GetLeftParentheses(result);
                    break;
                case EnumCharTypeExpression.RightParentheses:
                    gotToken = GetRightParentheses(result);
                    break;
                case EnumCharTypeExpression.Space:
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
        protected virtual bool GetPlus(Token<EnumTokenTypeExpression> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Plus
            //Mapped nodes:
            //    "+"
            //result.TokenType = EnumTokenTypeExpression.token_Plus_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("+" == str)
                {
                    result.TokenType = EnumTokenTypeExpression.token_Plus_;
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
        protected virtual bool GetMinus(Token<EnumTokenTypeExpression> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Minus
            //Mapped nodes:
            //    "-"
            //result.TokenType = EnumTokenTypeExpression.token_Minus_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("-" == str)
                {
                    result.TokenType = EnumTokenTypeExpression.token_Minus_;
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
        protected virtual bool GetMultiply(Token<EnumTokenTypeExpression> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Multiply
            //Mapped nodes:
            //    "*"
            //result.TokenType = EnumTokenTypeExpression.token_Multiply_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("*" == str)
                {
                    result.TokenType = EnumTokenTypeExpression.token_Multiply_;
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
        protected virtual bool GetDivide(Token<EnumTokenTypeExpression> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Divide
            //Mapped nodes:
            //    "/"
            //result.TokenType = EnumTokenTypeExpression.token_Divide_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("/" == str)
                {
                    result.TokenType = EnumTokenTypeExpression.token_Divide_;
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
        protected virtual bool GetLeftParentheses(Token<EnumTokenTypeExpression> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: LeftParentheses
            //Mapped nodes:
            //    "("
            //result.TokenType = EnumTokenTypeExpression.token_LeftParentheses_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("(" == str)
                {
                    result.TokenType = EnumTokenTypeExpression.token_LeftParentheses_;
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
        protected virtual bool GetRightParentheses(Token<EnumTokenTypeExpression> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: RightParentheses
            //Mapped nodes:
            //    ")"
            //result.TokenType = EnumTokenTypeExpression.token_RightParentheses_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (")" == str)
                {
                    result.TokenType = EnumTokenTypeExpression.token_RightParentheses_;
                    result.Detail = str;
                    PtNextLetter += 1;
                    return true;
                }
            }
            
            return false;
        }
        #region GetConstentNumber
        /// <summary>
        /// 数值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentNumber(Token<EnumTokenTypeExpression> result)
        {
            result.TokenType = EnumTokenTypeExpression.number;
            if (this.GetSourceCode()[PtNextLetter] == '0')//可能是八进制或十六进制数
            {
                if (PtNextLetter + 1 < this.GetSourceCode().Length)
                {
                    char c = this.GetSourceCode()[PtNextLetter + 1];
                    if (c == 'x' || c == 'X')
                    {//十六进制数
                        return GetConstentHexadecimalNumber(result);
                    }
                    else if (GetCharType(c) == EnumCharTypeExpression.Number)
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
        protected virtual bool GetConstentDecimalNumber(Token<EnumTokenTypeExpression> result)
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
        protected virtual bool GetConstentOctonaryNumber(Token<EnumTokenTypeExpression> result)
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
        protected virtual bool GetConstentHexadecimalNumber(Token<EnumTokenTypeExpression> result)
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
        protected virtual bool GetUnknown(Token<EnumTokenTypeExpression> result)
        {
            result.TokenType = EnumTokenTypeExpression.unknown;
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
        protected virtual bool GetSpace(Token<EnumTokenTypeExpression> result)
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
        protected virtual EnumCharTypeExpression GetCharType(char c)
        {
            if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')) return EnumCharTypeExpression.Letter;
            if ('0' <= c && c <= '9') return EnumCharTypeExpression.Number;
            if (c == '_') return EnumCharTypeExpression.UnderLine;
            if (c == '.') return EnumCharTypeExpression.Dot;
            if (c == ',') return EnumCharTypeExpression.Comma;
            if (c == '+') return EnumCharTypeExpression.Plus;
            if (c == '-') return EnumCharTypeExpression.Minus;
            if (c == '*') return EnumCharTypeExpression.Multiply;
            if (c == '/') return EnumCharTypeExpression.Divide;
            if (c == '%') return EnumCharTypeExpression.Percent;
            if (c == '^') return EnumCharTypeExpression.Xor;
            if (c == '&') return EnumCharTypeExpression.And;
            if (c == '|') return EnumCharTypeExpression.Or;
            if (c == '~') return EnumCharTypeExpression.Reverse;
            if (c == '$') return EnumCharTypeExpression.Dollar;
            if (c == '<') return EnumCharTypeExpression.LessThan;
            if (c == '>') return EnumCharTypeExpression.GreaterThan;
            if (c == '(') return EnumCharTypeExpression.LeftParentheses;
            if (c == ')') return EnumCharTypeExpression.RightParentheses;
            if (c == '[') return EnumCharTypeExpression.LeftBracket;
            if (c == ']') return EnumCharTypeExpression.RightBracket;
            if (c == '{') return EnumCharTypeExpression.LeftBrace;
            if (c == '}') return EnumCharTypeExpression.RightBrace;
            if (c == '!') return EnumCharTypeExpression.Not;
            if (c == '#') return EnumCharTypeExpression.Pound;
            if (c == '\\') return EnumCharTypeExpression.Slash;
            if (c == '?') return EnumCharTypeExpression.Question;
            if (c == '\'') return EnumCharTypeExpression.Quotation;
            if (c == '"') return EnumCharTypeExpression.DoubleQuotation;
            if (c == ':') return EnumCharTypeExpression.Colon;
            if (c == ';') return EnumCharTypeExpression.Semicolon;
            if (c == '=') return EnumCharTypeExpression.Equality;
            if (regChineseLetter.IsMatch(Convert.ToString(c))) return EnumCharTypeExpression.ChineseLetter;
            if (c == ' ' || c == '\t' || c == '\r' || c == '\n') return EnumCharTypeExpression.Space;
            return EnumCharTypeExpression.Unknown;
        }
        /// <summary>
        /// 汉字 new Regex("^[^\x00-\xFF]")
        /// </summary>
        private static readonly Regex regChineseLetter = new Regex("^[^\x00-\xFF]");
    }

}

