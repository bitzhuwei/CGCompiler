using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using bitzhuwei.CompilerBase;

namespace CSharpGL.GLSL430Compiler
{
    /// <summary>
    /// GLSL430的词法分析器
    /// </summary>
    public partial class LexicalAnalyzerGLSL430 : LexicalAnalyzerBase<EnumTokenTypeGLSL430>
    {
        /// <summary>
        /// GLSL430的词法分析器
        /// </summary>
        public LexicalAnalyzerGLSL430()
        { }
        /// <summary>
        /// GLSL430的词法分析器
        /// </summary>
        /// <param name="sourceCode">要分析的源代码</param>
        public LexicalAnalyzerGLSL430(string sourceCode)
            : base(sourceCode)
        { }
        /// <summary>
        /// 从<code>PtNextLetter</code>开始获取下一个<code>Token</code>
        /// </summary>
        /// <returns></returns>
        protected override Token<EnumTokenTypeGLSL430> NextToken()
        {
            var result = new Token<EnumTokenTypeGLSL430>();
            result.Line = m_CurrentLine;
            result.Column = m_CurrentColumn;
            result.IndexOfSourceCode = PtNextLetter;
            var count = this.GetSourceCode().Length;
            if (PtNextLetter < 0 || PtNextLetter >= count) return result;
            var gotToken = false;
            var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);
            switch (ct)
            {
                case EnumCharTypeGLSL430.Letter:
                    gotToken = GetIdentifier(result);
                    break;
                case EnumCharTypeGLSL430.Semicolon:
                    gotToken = GetSemicolon(result);
                    break;
                case EnumCharTypeGLSL430.LeftBracket:
                    gotToken = GetLeftBracket(result);
                    break;
                case EnumCharTypeGLSL430.RightBracket:
                    gotToken = GetRightBracket(result);
                    break;
                case EnumCharTypeGLSL430.LeftBrace:
                    gotToken = GetLeftBrace(result);
                    break;
                case EnumCharTypeGLSL430.RightBrace:
                    gotToken = GetRightBrace(result);
                    break;
                case EnumCharTypeGLSL430.LeftParentheses:
                    gotToken = GetLeftParentheses(result);
                    break;
                case EnumCharTypeGLSL430.RightParentheses:
                    gotToken = GetRightParentheses(result);
                    break;
                case EnumCharTypeGLSL430.Colon:
                    gotToken = GetColon(result);
                    break;
                case EnumCharTypeGLSL430.Comma:
                    gotToken = GetComma(result);
                    break;
                case EnumCharTypeGLSL430.Equality:
                    gotToken = GetEqualityOpt(result);
                    break;
                case EnumCharTypeGLSL430.Plus:
                    gotToken = GetPlusOpt(result);
                    break;
                case EnumCharTypeGLSL430.Minus:
                    gotToken = GetMinusOpt(result);
                    break;
                case EnumCharTypeGLSL430.Dot:
                    gotToken = GetDot(result);
                    break;
                case EnumCharTypeGLSL430.Number:
                    gotToken = GetConstentNumber(result);
                    break;
                case EnumCharTypeGLSL430.Not:
                    gotToken = GetNotOpt(result);
                    break;
                case EnumCharTypeGLSL430.Reverse:
                    gotToken = GetReverse(result);
                    break;
                case EnumCharTypeGLSL430.Multiply:
                    gotToken = GetMultiplyOpt(result);
                    break;
                case EnumCharTypeGLSL430.Divide:
                    gotToken = GetDivideOpt(result);
                    break;
                case EnumCharTypeGLSL430.Percent:
                    gotToken = GetPercentOpt(result);
                    break;
                case EnumCharTypeGLSL430.LessThan:
                    gotToken = GetLessThanOpt(result);
                    break;
                case EnumCharTypeGLSL430.GreaterThan:
                    gotToken = GetGreaterThanOpt(result);
                    break;
                case EnumCharTypeGLSL430.And:
                    gotToken = GetAndOpt(result);
                    break;
                case EnumCharTypeGLSL430.Xor:
                    gotToken = GetXorOpt(result);
                    break;
                case EnumCharTypeGLSL430.Or:
                    gotToken = GetOrOpt(result);
                    break;
                case EnumCharTypeGLSL430.Question:
                    gotToken = GetQuestion(result);
                    break;
                case EnumCharTypeGLSL430.Space:
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
        protected virtual bool GetSemicolon(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Semicolon
            //Mapped nodes:
            //    ";"
            //result.TokenType = EnumTokenTypeGLSL430.token_Semicolon_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (";" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Semicolon_;
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
        protected virtual bool GetLeftBracket(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: LeftBracket
            //Mapped nodes:
            //    "["
            //result.TokenType = EnumTokenTypeGLSL430.token_LeftBracket_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("[" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_LeftBracket_;
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
        protected virtual bool GetRightBracket(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: RightBracket
            //Mapped nodes:
            //    "]"
            //result.TokenType = EnumTokenTypeGLSL430.token_RightBracket_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("]" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_RightBracket_;
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
        protected virtual bool GetLeftBrace(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: LeftBrace
            //Mapped nodes:
            //    "{"
            //result.TokenType = EnumTokenTypeGLSL430.token_LeftBrace_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("{" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_LeftBrace_;
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
        protected virtual bool GetRightBrace(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: RightBrace
            //Mapped nodes:
            //    "}"
            //result.TokenType = EnumTokenTypeGLSL430.token_RightBrace_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("}" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_RightBrace_;
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
        protected virtual bool GetLeftParentheses(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: LeftParentheses
            //Mapped nodes:
            //    "("
            //result.TokenType = EnumTokenTypeGLSL430.token_LeftParentheses_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("(" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_LeftParentheses_;
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
        protected virtual bool GetRightParentheses(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: RightParentheses
            //Mapped nodes:
            //    ")"
            //result.TokenType = EnumTokenTypeGLSL430.token_RightParentheses_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (")" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_RightParentheses_;
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
        protected virtual bool GetColon(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Colon
            //Mapped nodes:
            //    ":"
            //result.TokenType = EnumTokenTypeGLSL430.token_Colon_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (":" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Colon_;
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
        protected virtual bool GetComma(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Comma
            //Mapped nodes:
            //    ","
            //result.TokenType = EnumTokenTypeGLSL430.token_Comma_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("," == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Comma_;
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
        protected virtual bool GetEqualityOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Equality
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "="
            //result.TokenType = EnumTokenTypeGLSL430.token_Equality_;
            //    "=="
            //result.TokenType = EnumTokenTypeGLSL430.token_Equality_Equality_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("==" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Equality_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Equality_;
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
        protected virtual bool GetPlusOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Plus
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "++"
            //result.TokenType = EnumTokenTypeGLSL430.token_Plus_Plus_;
            //    "+"
            //result.TokenType = EnumTokenTypeGLSL430.token_Plus_;
            //    "+="
            //result.TokenType = EnumTokenTypeGLSL430.token_Plus_Equality_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("++" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Plus_Plus_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
                if ("+=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Plus_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("+" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Plus_;
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
        protected virtual bool GetMinusOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Minus
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "--"
            //result.TokenType = EnumTokenTypeGLSL430.token_Minus_Minus_;
            //    "-"
            //result.TokenType = EnumTokenTypeGLSL430.token_Minus_;
            //    "-="
            //result.TokenType = EnumTokenTypeGLSL430.token_Minus_Equality_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("--" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Minus_Minus_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
                if ("-=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Minus_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("-" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Minus_;
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
        protected virtual bool GetDot(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Dot
            //Mapped nodes:
            //    "."
            //result.TokenType = EnumTokenTypeGLSL430.token_Dot_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("." == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Dot_;
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
        protected virtual bool GetNotOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Not
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "!"
            //result.TokenType = EnumTokenTypeGLSL430.token_Not_;
            //    "!="
            //result.TokenType = EnumTokenTypeGLSL430.token_Not_Equality_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("!=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Not_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("!" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Not_;
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
        protected virtual bool GetReverse(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Reverse
            //Mapped nodes:
            //    "~"
            //result.TokenType = EnumTokenTypeGLSL430.token_Reverse_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("~" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Reverse_;
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
        protected virtual bool GetMultiplyOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Multiply
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "*="
            //result.TokenType = EnumTokenTypeGLSL430.token_Multiply_Equality_;
            //    "*"
            //result.TokenType = EnumTokenTypeGLSL430.token_Multiply_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("*=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Multiply_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("*" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Multiply_;
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
        protected virtual bool GetDivideOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Divide
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "/="
            //result.TokenType = EnumTokenTypeGLSL430.token_Divide_Equality_;
            //    "/"
            //result.TokenType = EnumTokenTypeGLSL430.token_Divide_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("/*" == str) { SkipSingleLineNote(); return false; }
                if ("//" == str) { SkipSingleLineNote(); return false; }
                if ("/=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Divide_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("/" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Divide_;
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
        protected virtual bool GetPercentOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Percent
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "%="
            //result.TokenType = EnumTokenTypeGLSL430.token_Percent_Equality_;
            //    "%"
            //result.TokenType = EnumTokenTypeGLSL430.token_Percent_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("%=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Percent_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("%" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Percent_;
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
        protected virtual bool GetLessThanOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: LessThan
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "<<="
            //result.TokenType = EnumTokenTypeGLSL430.token_LessThan_LessThan_Equality_;
            //    "<"
            //result.TokenType = EnumTokenTypeGLSL430.token_LessThan_;
            //    "<="
            //result.TokenType = EnumTokenTypeGLSL430.token_LessThan_Equality_;
            //    "<<"
            //result.TokenType = EnumTokenTypeGLSL430.token_LessThan_LessThan_;
            if (PtNextLetter + 3 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 3);
                if ("<<=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_LessThan_LessThan_Equality_;
                    result.Detail = str;
                    PtNextLetter += 3;
                    return true;
                }
            }
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("<=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_LessThan_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
                if ("<<" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_LessThan_LessThan_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("<" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_LessThan_;
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
        protected virtual bool GetGreaterThanOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: GreaterThan
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    ">>="
            //result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_GreaterThan_Equality_;
            //    ">"
            //result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_;
            //    ">="
            //result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_Equality_;
            //    ">>"
            //result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_GreaterThan_;
            if (PtNextLetter + 3 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 3);
                if (">>=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_GreaterThan_Equality_;
                    result.Detail = str;
                    PtNextLetter += 3;
                    return true;
                }
            }
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if (">=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
                if (">>" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_GreaterThan_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if (">" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_GreaterThan_;
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
        protected virtual bool GetAndOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: And
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "&="
            //result.TokenType = EnumTokenTypeGLSL430.token_And_Equality_;
            //    "&&"
            //result.TokenType = EnumTokenTypeGLSL430.token_And_And_;
            //    "&"
            //result.TokenType = EnumTokenTypeGLSL430.token_And_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("&=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_And_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
                if ("&&" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_And_And_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("&" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_And_;
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
        protected virtual bool GetXorOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Xor
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "^="
            //result.TokenType = EnumTokenTypeGLSL430.token_Xor_Equality_;
            //    "^^"
            //result.TokenType = EnumTokenTypeGLSL430.token_Xor_Xor_;
            //    "^"
            //result.TokenType = EnumTokenTypeGLSL430.token_Xor_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("^=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Xor_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
                if ("^^" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Xor_Xor_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("^" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Xor_;
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
        protected virtual bool GetOrOpt(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Or
            //todo: maybe you need to set TokenType to their right position.
            //Mapped nodes:
            //    "|="
            //result.TokenType = EnumTokenTypeGLSL430.token_Or_Equality_;
            //    "||"
            //result.TokenType = EnumTokenTypeGLSL430.token_Or_Or_;
            //    "|"
            //result.TokenType = EnumTokenTypeGLSL430.token_Or_;
            if (PtNextLetter + 2 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 2);
                if ("|=" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Or_Equality_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
                if ("||" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Or_Or_;
                    result.Detail = str;
                    PtNextLetter += 2;
                    return true;
                }
            }
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("|" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Or_;
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
        protected virtual bool GetQuestion(Token<EnumTokenTypeGLSL430> result)
        {
            var count = this.GetSourceCode().Length;
            //item.CharType: Question
            //Mapped nodes:
            //    "?"
            //result.TokenType = EnumTokenTypeGLSL430.token_Question_;
            if (PtNextLetter + 1 <= count)
            {
                var str = this.GetSourceCode().Substring(PtNextLetter, 1);
                if ("?" == str)
                {
                    result.TokenType = EnumTokenTypeGLSL430.token_Question_;
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
        protected virtual bool GetIdentifier(Token<EnumTokenTypeGLSL430> result)
        {
            result.TokenType = EnumTokenTypeGLSL430.identifier;
            StringBuilder builder = new StringBuilder();
            while (PtNextLetter < this.GetSourceCode().Length)
            {
                var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);
                if (ct == EnumCharTypeGLSL430.Letter
                    || ct == EnumCharTypeGLSL430.Number
                    || ct == EnumCharTypeGLSL430.UnderLine
                    || ct == EnumCharTypeGLSL430.ChineseLetter)
                {
                    builder.Append(this.GetSourceCode()[PtNextLetter]);
                    PtNextLetter++;
                }
                else
                { break; }
            }
            result.Detail = builder.ToString();
            // specify if this string is a keyword
            foreach (var item in LexicalAnalyzerGLSL430.keywords)
            {
                if (item.ToString().Substring(6) == result.Detail)
                {
                    result.TokenType = item;
                    break;
                }
            }
            return true;
        }
        
        public static readonly IEnumerable<EnumTokenTypeGLSL430> keywords = new List<EnumTokenTypeGLSL430>()
        {
            EnumTokenTypeGLSL430.token_precision,
            EnumTokenTypeGLSL430.token_void,
            EnumTokenTypeGLSL430.token_if,
            EnumTokenTypeGLSL430.token_else,
            EnumTokenTypeGLSL430.token_switch,
            EnumTokenTypeGLSL430.token_case,
            EnumTokenTypeGLSL430.token_default,
            EnumTokenTypeGLSL430.token_while,
            EnumTokenTypeGLSL430.token_do,
            EnumTokenTypeGLSL430.token_for,
            EnumTokenTypeGLSL430.token_continue,
            EnumTokenTypeGLSL430.token_break,
            EnumTokenTypeGLSL430.token_return,
            EnumTokenTypeGLSL430.token_discard,
            EnumTokenTypeGLSL430.token_true,
            EnumTokenTypeGLSL430.token_false,
            EnumTokenTypeGLSL430.token_float,
            EnumTokenTypeGLSL430.token_double,
            EnumTokenTypeGLSL430.token_int,
            EnumTokenTypeGLSL430.token_uint,
            EnumTokenTypeGLSL430.token_bool,
            EnumTokenTypeGLSL430.token_samplerCube,
            EnumTokenTypeGLSL430.token_samplerCubeShadow,
            EnumTokenTypeGLSL430.token_samplerCubeArray,
            EnumTokenTypeGLSL430.token_samplerCubeArrayShadow,
            EnumTokenTypeGLSL430.token_isamplerCube,
            EnumTokenTypeGLSL430.token_isamplerCubeArray,
            EnumTokenTypeGLSL430.token_usamplerCube,
            EnumTokenTypeGLSL430.token_usamplerCubeArray,
            EnumTokenTypeGLSL430.token_samplerBuffer,
            EnumTokenTypeGLSL430.token_isamplerBuffer,
            EnumTokenTypeGLSL430.token_usamplerBuffer,
            EnumTokenTypeGLSL430.token_imageCube,
            EnumTokenTypeGLSL430.token_iimageCube,
            EnumTokenTypeGLSL430.token_uimageCube,
            EnumTokenTypeGLSL430.token_imageBuffer,
            EnumTokenTypeGLSL430.token_iimageBuffer,
            EnumTokenTypeGLSL430.token_uimageBuffer,
            EnumTokenTypeGLSL430.token_imageCubeArray,
            EnumTokenTypeGLSL430.token_iimageCubeArray,
            EnumTokenTypeGLSL430.token_uimageCubeArray,
            EnumTokenTypeGLSL430.token_struct,
            EnumTokenTypeGLSL430.token_const,
            EnumTokenTypeGLSL430.token_inout,
            EnumTokenTypeGLSL430.token_in,
            EnumTokenTypeGLSL430.token_out,
            EnumTokenTypeGLSL430.token_centroid,
            EnumTokenTypeGLSL430.token_patch,
            EnumTokenTypeGLSL430.token_sample,
            EnumTokenTypeGLSL430.token_uniform,
            EnumTokenTypeGLSL430.token_buffer,
            EnumTokenTypeGLSL430.token_shared,
            EnumTokenTypeGLSL430.token_coherent,
            EnumTokenTypeGLSL430.token_volatile,
            EnumTokenTypeGLSL430.token_restrict,
            EnumTokenTypeGLSL430.token_readonly,
            EnumTokenTypeGLSL430.token_writeonly,
            EnumTokenTypeGLSL430.token_subroutine,
            EnumTokenTypeGLSL430.token_layout,
            EnumTokenTypeGLSL430.token_smooth,
            EnumTokenTypeGLSL430.token_flat,
            EnumTokenTypeGLSL430.token_noperspective,
            EnumTokenTypeGLSL430.token_invariant,
            EnumTokenTypeGLSL430.token_precise,
        };
        
        #endregion GetIdentifier
        #region GetConstentNumber
        /// <summary>
        /// 数值
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual bool GetConstentNumber(Token<EnumTokenTypeGLSL430> result)
        {
            result.TokenType = EnumTokenTypeGLSL430.number;
            if (this.GetSourceCode()[PtNextLetter] == '0')//可能是八进制或十六进制数
            {
                if (PtNextLetter + 1 < this.GetSourceCode().Length)
                {
                    char c = this.GetSourceCode()[PtNextLetter + 1];
                    if (c == 'x' || c == 'X')
                    {//十六进制数
                        return GetConstentHexadecimalNumber(result);
                    }
                    else if (GetCharType(c) == EnumCharTypeGLSL430.Number)
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
        protected virtual bool GetConstentDecimalNumber(Token<EnumTokenTypeGLSL430> result)
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
        protected virtual bool GetConstentOctonaryNumber(Token<EnumTokenTypeGLSL430> result)
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
        protected virtual bool GetConstentHexadecimalNumber(Token<EnumTokenTypeGLSL430> result)
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
        protected virtual bool GetUnknown(Token<EnumTokenTypeGLSL430> result)
        {
            result.TokenType = EnumTokenTypeGLSL430.unknown;
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
        protected virtual bool GetSpace(Token<EnumTokenTypeGLSL430> result)
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
        protected virtual EnumCharTypeGLSL430 GetCharType(char c)
        {
            if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')) return EnumCharTypeGLSL430.Letter;
            if ('0' <= c && c <= '9') return EnumCharTypeGLSL430.Number;
            if (c == '_') return EnumCharTypeGLSL430.UnderLine;
            if (c == '.') return EnumCharTypeGLSL430.Dot;
            if (c == ',') return EnumCharTypeGLSL430.Comma;
            if (c == '+') return EnumCharTypeGLSL430.Plus;
            if (c == '-') return EnumCharTypeGLSL430.Minus;
            if (c == '*') return EnumCharTypeGLSL430.Multiply;
            if (c == '/') return EnumCharTypeGLSL430.Divide;
            if (c == '%') return EnumCharTypeGLSL430.Percent;
            if (c == '^') return EnumCharTypeGLSL430.Xor;
            if (c == '&') return EnumCharTypeGLSL430.And;
            if (c == '|') return EnumCharTypeGLSL430.Or;
            if (c == '~') return EnumCharTypeGLSL430.Reverse;
            if (c == '$') return EnumCharTypeGLSL430.Dollar;
            if (c == '<') return EnumCharTypeGLSL430.LessThan;
            if (c == '>') return EnumCharTypeGLSL430.GreaterThan;
            if (c == '(') return EnumCharTypeGLSL430.LeftParentheses;
            if (c == ')') return EnumCharTypeGLSL430.RightParentheses;
            if (c == '[') return EnumCharTypeGLSL430.LeftBracket;
            if (c == ']') return EnumCharTypeGLSL430.RightBracket;
            if (c == '{') return EnumCharTypeGLSL430.LeftBrace;
            if (c == '}') return EnumCharTypeGLSL430.RightBrace;
            if (c == '!') return EnumCharTypeGLSL430.Not;
            if (c == '#') return EnumCharTypeGLSL430.Pound;
            if (c == '\\') return EnumCharTypeGLSL430.Slash;
            if (c == '?') return EnumCharTypeGLSL430.Question;
            if (c == '\'') return EnumCharTypeGLSL430.Quotation;
            if (c == '"') return EnumCharTypeGLSL430.DoubleQuotation;
            if (c == ':') return EnumCharTypeGLSL430.Colon;
            if (c == ';') return EnumCharTypeGLSL430.Semicolon;
            if (c == '=') return EnumCharTypeGLSL430.Equality;
            if (c == '@') return EnumCharTypeGLSL430.At;
            if (regChineseLetter.IsMatch(Convert.ToString(c))) return EnumCharTypeGLSL430.ChineseLetter;
            if (c == ' ' || c == '\t' || c == '\r' || c == '\n') return EnumCharTypeGLSL430.Space;
            return EnumCharTypeGLSL430.Unknown;
        }
        /// <summary>
        /// 汉字 new Regex("^[^\x00-\xFF]")
        /// </summary>
        private static readonly Regex regChineseLetter = new Regex("^[^\x00-\xFF]");
    }
}

