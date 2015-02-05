using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 2型文法（上下文无关文法）
    /// </summary>
    public partial class ContextfreeGrammar : ICloneable
    {
        #region 生成整个词法分析器的源代码
        
        /// <summary>
        /// 生成整个词法分析器的源代码
        /// </summary>
        /// <returns></returns>
        public string GenerateLexicalAnalyzer()
        {
            int preSpace = 0;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateLexicalAnalyzer(ref preSpace, input);
        }
        /// <summary>
        /// 生成整个词法分析器的源代码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateLexicalAnalyzer(LL1GeneraterInput input)
        {
            int preSpace = 0;

            return GenerateLexicalAnalyzer(ref preSpace, input);
        }
        /// <summary>
        /// 生成整个词法分析器的源代码
        /// </summary>
        /// <param name="grammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateLexicalAnalyzer(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetSpaces(preSpace) + "using System;");
            builder.AppendLine(GetSpaces(preSpace) + "using System.Text;");
            builder.AppendLine(GetSpaces(preSpace) + "using System.Text.RegularExpressions;");
            builder.AppendLine(GetSpaces(preSpace) + "using System.Collections.Generic;");
            builder.AppendLine(GetSpaces(preSpace) + "using bitzhuwei.CompilerBase;");
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("namespace {0}", this.Namespace));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GenerateLexicalAnalyzerClass(ref preSpace, input));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace));
            return builder.ToString();
        }
        #endregion 生成整个词法分析器的源代码
        
        #region 生成词法分析器类的源代码
        /// <summary>
        /// 生成LL1生成词法分析器类的源代码（只包含类）
        /// </summary>
        /// <returns></returns>
        public string GenerateLexicalAnalyzerClass()
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateLexicalAnalyzer(ref preSpace, input);
        }
        /// <summary>
        /// 生成LL1生成词法分析器类的源代码（只包含类）
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateLexicalAnalyzerClass(LL1GeneraterInput input)
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;

            return GenerateLexicalAnalyzerClass(ref preSpace, input);
        }
        /// <summary>
        /// 生成词法分析器类的源代码（只包含类）
        /// </summary>
        /// <param name="grammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateLexicalAnalyzerClass(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("/// {0}的词法分析器", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            //LexicalAnalyzerCG : LexicalAnalyzerBase<EnumTokenTypeCG>

            builder.AppendLine(GetSpaces(preSpace) + string.Format("public partial class {0} : {1}<{2}>"
                ,GetLexicalAnalyzerName()
                , GetLexicalAnalyzerBaseName()
                , GetEnumTokenTypeSG()));

            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            GenerateLexicalAnalyzerConstructor(builder, ref preSpace, input);
            GenerateLexicalAnalyzerMethodNextToken(builder, ref preSpace, input);
            GenerateLexicalAnalyzerTokenFactory(builder, ref preSpace, input);
            GenerateLexicalAnalyzerMethodGetCharType(builder, ref preSpace, input);
            GenerateLexicalAnalyzerFieldregChineseLetter(builder, ref preSpace, input);
            
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            return builder.ToString();
        }
        /// <summary>
        /// 字段：汉字
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="grammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLexicalAnalyzerFieldregChineseLetter(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            //private static readonly Regex regChineseLetter = new Regex("^[^\x00-\xFF]");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 汉字 new Regex(\"^[^\\x00-\\xFF]\")");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + 
                "private static readonly Regex regChineseLetter = new Regex(\"^[^\\x00-\\xFF]\");");
        }

        private void GenerateLexicalAnalyzerMethodGetCharType(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 获取字符类型");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"c\">要归类的字符</param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("protected virtual {0} GetCharType(char c)"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')) return {0}.Letter;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if ('0' <= c && c <= '9') return {0}.Number;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '_') return {0}.UnderLine;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '.') return {0}.Dot;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == ',') return {0}.Comma;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '+') return {0}.Plus;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '-') return {0}.Minus;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '*') return {0}.Multiply;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '/') return {0}.Divide;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '%') return {0}.Percent;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '^') return {0}.Xor;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '&') return {0}.And;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '|') return {0}.Or;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '~') return {0}.Reverse;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '$') return {0}.Dollar;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '<') return {0}.LessThan;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '>') return {0}.GreaterThan;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '(') return {0}.LeftParentheses;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == ')') return {0}.RightParentheses;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '[') return {0}.LeftBracket;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == ']') return {0}.RightBracket;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '{1}') return {0}.LeftBrace;"
                , GetEnumCharTypeSG(), "{"));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '{1}') return {0}.RightBrace;"
                , GetEnumCharTypeSG(), "}"));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '!') return {0}.Not;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '#') return {0}.Pound;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '\\\\') return {0}.Slash;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '?') return {0}.Question;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '\\\'') return {0}.Quotation;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '\"') return {0}.DoubleQuotation;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == ':') return {0}.Colon;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == ';') return {0}.Semicolon;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == '=') return {0}.Equality;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if ({1}.IsMatch(Convert.ToString(c))) return {0}.ChineseLetter;"
                , GetEnumCharTypeSG()
                , GetFieldregChineseLetter()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (c == ' ' || c == '\\t' || c == '\\r' || c == '\\n') return {0}.Space;"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("return {0}.Unknown;"
                , GetEnumCharTypeSG()));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        public string GetFieldregChineseLetter()
        {
            return "regChineseLetter";
        }

        public string GetEnumCharTypeSG()
        {
            return string.Format("EnumCharType{0}", this.GrammarName);
        }

        private void GenerateLexicalAnalyzerTokenFactory(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region 获取某类型的单词");

            var map = GetTokenMap(input);
            //map.Map.Add(CharProductionNodeListList.letter);
            //map.Map.Add(CharProductionNodeListList.chineseLetter);
            //map.Map.Add(CharProductionNodeListList.underline);
            //map.Map.Add(CharProductionNodeListList.constentNumber);
            foreach (var item in map.Map)
            {
                var charType = item.CharType;
                if (charType == EnumCharType.Letter
                    || charType == EnumCharType.ChineseLetter
                    || charType == EnumCharType.UnderLine
                    || charType == EnumCharType.Number
                    || charType == EnumCharType.Quotation
                    || charType == EnumCharType.DoubleQuotation)
                    continue;
                int conditions = item.MappedNodes.Count;
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) + "/// )");
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
                builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("protected virtual bool Get{0}{1}(Token<{2}> result)"
                    , item.CharType
                    , conditions > 1 ? "Opt" : ""
                    , GetEnumTokenTypeSG()));
                builder.AppendLine(GetSpaces(preSpace) + "{");
                IncreasepreSpace(ref preSpace);
                builder.AppendLine(GetSpaces(preSpace) + "var count = this.GetSourceCode().Length;");
                builder.AppendLine(GetSpaces(preSpace) + 
                    string.Format("//item.CharType: {0}"
                    , item.CharType));
                if (item.MappedNodes.Count > 1)
                {
                    builder.AppendLine("//todo: maybe you need to set TokenType to their right position.");
                }
                builder.AppendLine(GetSpaces(preSpace) + "//Mapped nodes:");
                foreach (var node in item.MappedNodes)
                {
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("//    {0}", node));
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("//result.TokenType = {0}.{1};"
                        , GetEnumTokenTypeSG()
                        , GetEnumTokenTypeSGItem(node)));
                }
                //
                var tokens = from node in item.MappedNodes
                             group node by (node.NodeName.Count() - 2) into teams
                             orderby teams.Key descending
                             select teams;
                foreach (var token in tokens)
                {
                    var key = token.Key;
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("if (PtNextLetter + {0} <= count)", key));
                    builder.AppendLine(GetSpaces(preSpace) + "{");
                    IncreasepreSpace(ref preSpace);
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("var str = this.GetSourceCode().Substring(PtNextLetter, {0});"
                        , key));
                    foreach (var sign in token)
                    {
                        var start = sign.NodeName[0] == '"' ? 1 : 0;
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("if ({0} == str)"
                            , sign.NodeName
                            //.Substring(start, sign.NodeName.Length - start - start)
                            ));
                        builder.AppendLine(GetSpaces(preSpace) + "{");
                        IncreasepreSpace(ref preSpace);
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("result.TokenType = {0}.{1};"
                            , GetEnumTokenTypeSG()
                            , GetEnumTokenTypeSGItem(sign)));
                        builder.AppendLine(GetSpaces(preSpace) + "result.Detail = str;");
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("PtNextLetter += {0};", key));
                        builder.AppendLine(GetSpaces(preSpace) + "return true;");
                        DecreasepreSpace(ref preSpace);
                        builder.AppendLine(GetSpaces(preSpace) + "}");
                    }
                    DecreasepreSpace(ref preSpace);
                    builder.AppendLine(GetSpaces(preSpace) + "}");
                }

                builder.AppendLine(GetSpaces(preSpace));
                builder.AppendLine(GetSpaces(preSpace) + "return false;");
                DecreasepreSpace(ref preSpace);
                builder.AppendLine(GetSpaces(preSpace) + "}");
            }
            if (map.Contains(EnumCharType.Letter)
                || map.Contains(EnumCharType.ChineseLetter)
                || map.Contains(EnumCharType.UnderLine)
                )
                GenerateLexicalAnalyzerTokenFactoryIdentifier(builder, ref preSpace, input);
            if (map.Contains(EnumCharType.Number))
                GenerateLexicalAnalyzerTokenFactoryConstentNumber(builder, ref preSpace, input);
            if (map.Contains(EnumCharType.Quotation))
                GenerateLexicalAnalyzerTokenFactoryConstentChar(builder, ref preSpace, input);
            if(map.Contains(EnumCharType.DoubleQuotation))
                GenerateLexicalAnalyzerTokenFactoryConstentString(builder, ref preSpace, input);
            GenerateLexicalAnalyzerTokenFactoryUnknown(builder, ref preSpace, input);
            GenerateLexicalAnalyzerTokenFactorySpace(builder, ref preSpace, input);
            GenerateLexicalAnalyzerTokenFactoryMultilineNote(builder, ref preSpace, input);
            GenerateLexicalAnalyzerTokenFactorySingleLineNote(builder, ref preSpace, input);


            builder.AppendLine(GetSpaces(preSpace) + "#endregion 获取某类型的单词");
        }

        private void GenerateLexicalAnalyzerTokenFactorySingleLineNote(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 跳过单行注释");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + "protected virtual void SkipSingleLineNote()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "int count = this.GetSourceCode().Length;");
            builder.AppendLine(GetSpaces(preSpace) + "char cNext;");
            builder.AppendLine(GetSpaces(preSpace) + "while (PtNextLetter < count)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "cNext = GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (cNext == '\\r' || cNext == '\\n')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryMultilineNote(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 跳过多行注释");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + "protected virtual void SkipMultilineNote()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "int count = this.GetSourceCode().Length;");
            builder.AppendLine(GetSpaces(preSpace) + "while (PtNextLetter < count)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "if (GetSourceCode()[PtNextLetter] == '*')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "if (PtNextLetter < count)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "if (GetSourceCode()[PtNextLetter] == '/')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        private void GenerateLexicalAnalyzerTokenFactorySpace(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// space tab \\r \\n");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("protected virtual bool GetSpace(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "char c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == '\\n')// || c == '\\r') //换行：Windows：\\r\\n Linux：\\n");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "this.m_CurrentLine++;");
            builder.AppendLine(GetSpaces(preSpace) + "this.m_CurrentColumn = 0;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return false;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryUnknown(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 未知符号");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("protected virtual bool GetUnknown(Token<{0}> result)"
                ,GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("result.TokenType = {0}.unknown;"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "result.Detail = this.GetSourceCode()[PtNextLetter].ToString();");
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = true;");
            builder.AppendLine(GetSpaces(preSpace) + "result.Tag = string.Format(\"发现未知字符[{0}]。\", result.Detail);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "return true;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryConstentString(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 字符串常量 \"XXX\"");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("protected virtual bool GetConstentString(Token<{0}> result)"
                , GetEnumTokenTypeSG()));//GetMethodGetSomeTokenWithCharType
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("result.TokenType = {0}.constString;"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "int count = this.GetSourceCode().Length;");
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder constString = new StringBuilder(\"\\\"\");");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "bool notMatched = true;");
            builder.AppendLine(GetSpaces(preSpace) + "char c;");
            builder.AppendLine(GetSpaces(preSpace) + "while ((PtNextLetter < count) && notMatched)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == '\"')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "constString.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "notMatched = false;");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else if (c == '\\r' || c == '\\n')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "constString.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "result.Detail = constString.ToString();");
            builder.AppendLine(GetSpaces(preSpace) + "return true;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryConstentChar(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 字符常量 'X'");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("protected virtual bool GetQuotation(Token<{0}> result)"
                ,GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("result.TokenType = {0}.ConstentChar;"
                ,GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "int count = this.GetSourceCode().Length;");
            builder.AppendLine(GetSpaces(preSpace) + "int ptCharHead = PtNextLetter;");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "char cNext;");
            builder.AppendLine(GetSpaces(preSpace) + "while (PtNextLetter < count && PtNextLetter - ptCharHead < 4)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "cNext = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (cNext == '\\'')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else if (cNext == '\\\\')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter += 2;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "int length = PtNextLetter - ptCharHead;");
            builder.AppendLine(GetSpaces(preSpace) + "result.Detail = this.GetSourceCode().Substring(ptCharHead, length);");
            builder.AppendLine(GetSpaces(preSpace) + "if (length < 3)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = true;");
            builder.AppendLine(GetSpaces(preSpace) + "result.Tag = string.Format(\"字符标识[{0}]错误。\", result.Detail);");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else if (length == 3)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "var ect = GetCharType(this.GetSourceCode()[ptCharHead + 1]);");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("if (ect == {0}.{1})"
                , GetEnumCharTypeSG()
                , GetEnumCharTypeSGItemDefaultName()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = true;");
            builder.AppendLine(GetSpaces(preSpace) + "result.Tag = string.Format(\"字符标识[{0}]错误。\", result.Detail);");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else //if (length == 4)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "if (this.GetSourceCode()[ptCharHead] != '\\''");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "|| this.GetSourceCode()[ptCharHead + 1] != '\\\\'");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("|| GetCharType(this.GetSourceCode()[ptCharHead + 2]) == {0}.{1}"
                ,GetEnumCharTypeSG()
                ,GetEnumCharTypeSGItemDefaultName()));
            builder.AppendLine(GetSpaces(preSpace) + "|| this.GetSourceCode()[ptCharHead + 3] != '\\'')");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = true;");
            builder.AppendLine(GetSpaces(preSpace) + "result.Tag = string.Format(\"字符标识[{0}]错误。\", result.Detail);");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return true;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryConstentNumber(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region GetConstentNumber");
            //GetConstentNumber
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 数值");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("protected virtual bool GetConstentNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("result.TokenType = {0}.{1};"
                , GetEnumTokenTypeSG()
                , GetEnumTokenTypeSGItem( ProductionNode.tail_number)));
            builder.AppendLine(GetSpaces(preSpace) + "if (this.GetSourceCode()[PtNextLetter] == '0')//可能是八进制或十六进制数");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "if (PtNextLetter + 1 < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "char c = this.GetSourceCode()[PtNextLetter + 1];");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == 'x' || c == 'X')");
            builder.AppendLine(GetSpaces(preSpace) + "{//十六进制数");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "return GetConstentHexadecimalNumber(result);");
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("else if (GetCharType(c) == {0}.Number)"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{//八进制数");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "return GetConstentOctonaryNumber(result);");
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else//十进制数");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "return GetConstentDecimalNumber(result);");
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            builder.AppendLine(GetSpaces(preSpace) + "{//源代码最后一个字符 0");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "result.Detail = \"0\";//0");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "return true;");
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else//十进制数");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "return GetConstentDecimalNumber(result);");
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 十进制数");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("protected virtual bool GetConstentDecimalNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "char c;");
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder tag = new StringBuilder();");
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "string numberSerial1, numberSerial2, numberSerial3;");
            builder.AppendLine(GetSpaces(preSpace) + "numberSerial1 = GetNumberSerial(this.GetSourceCode(), 10);");
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(numberSerial1);");
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = string.IsNullOrEmpty(numberSerial1);");
            builder.AppendLine(GetSpaces(preSpace) + "if (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == 'l' || c == 'L')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == '.')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "numberSerial2 = GetNumberSerial(this.GetSourceCode(), 10);");
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(numberSerial2);");
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = result.LexicalError || string.IsNullOrEmpty(numberSerial2);");
            builder.AppendLine(GetSpaces(preSpace) + "if (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == 'e' || c == 'E')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "if (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == '+' || c == '-')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "numberSerial3 = GetNumberSerial(this.GetSourceCode(), 10);");
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(numberSerial3);");
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = result.LexicalError || string.IsNullOrEmpty(numberSerial3);");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "result.Detail = tag.ToString();");
            builder.AppendLine(GetSpaces(preSpace) + "if (result.LexicalError)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.Tag = string.Format(\"十进制数[{0}]格式错误，无法解析。\", tag.ToString());");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return true;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 八进制数");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("protected virtual bool GetConstentOctonaryNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "char c;");
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder tag = new StringBuilder(this.GetSourceCode().Substring(PtNextLetter, 1));");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            builder.AppendLine(GetSpaces(preSpace) + "string numberSerial = GetNumberSerial(this.GetSourceCode(), 8);");
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(numberSerial);");
            builder.AppendLine(GetSpaces(preSpace) + "if (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == 'l' || c == 'L')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "result.Detail = tag.ToString();");
            builder.AppendLine(GetSpaces(preSpace) + "if (string.IsNullOrEmpty(numberSerial))");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = true;");
            builder.AppendLine(GetSpaces(preSpace) + "result.Tag = string.Format(\"八进制数[{0}]格式错误，无法解析。\", tag.ToString());");
            builder.AppendLine(GetSpaces(preSpace) + "return false;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return true;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 十六进制数");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("protected virtual bool GetConstentHexadecimalNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "char c;");
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder tag = new StringBuilder(this.GetSourceCode().Substring(PtNextLetter, 2));");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter += 2;");
            builder.AppendLine(GetSpaces(preSpace) + "string numberSerial = GetNumberSerial(this.GetSourceCode(), 16);");
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(numberSerial);");
            builder.AppendLine(GetSpaces(preSpace) + "if (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (c == 'l' || c == 'L')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "tag.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "result.Detail = tag.ToString();");
            builder.AppendLine(GetSpaces(preSpace) + "if (string.IsNullOrEmpty(numberSerial))");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.LexicalError = true;");
            builder.AppendLine(GetSpaces(preSpace) + "result.Tag = string.Format(\"十六进制数[{0}]格式错误。\", tag.ToString());");
            builder.AppendLine(GetSpaces(preSpace) + "return false;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return true;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 数字序列");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"sourceCode\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"scale\">进制</param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + "protected virtual string GetNumberSerial(string sourceCode, int scale)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "if (scale == 10)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "return GetNumberSerialDecimal(this.GetSourceCode());");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "if (scale == 16)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "return GetNumberSerialHexadecimal(this.GetSourceCode());");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "if (scale == 8)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "return GetNumberSerialOctonary(this.GetSourceCode());");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return string.Empty;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 十进制数序列");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"sourceCode\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + "protected virtual string GetNumberSerialDecimal(string sourceCode)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder result = new StringBuilder(String.Empty);");
            builder.AppendLine(GetSpaces(preSpace) + "char c;");
            builder.AppendLine(GetSpaces(preSpace) + "while (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if ('0' <= c && c <= '9')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return result.ToString();");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 八进制数序列");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"sourceCode\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + "protected virtual string GetNumberSerialOctonary(string sourceCode)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder result = new StringBuilder(String.Empty);");
            builder.AppendLine(GetSpaces(preSpace) + "char c;");
            builder.AppendLine(GetSpaces(preSpace) + "while (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if ('0' <= c && c <= '7')");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return result.ToString();");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 十六进制数序列（不包括0x前缀）");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"sourceCode\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + "protected virtual string GetNumberSerialHexadecimal(string sourceCode)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder result = new StringBuilder(String.Empty);");
            builder.AppendLine(GetSpaces(preSpace) + "char c;");
            builder.AppendLine(GetSpaces(preSpace) + "while (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "c = this.GetSourceCode()[PtNextLetter];");
            builder.AppendLine(GetSpaces(preSpace) + "if (('0' <= c && c <= '9')");
            builder.AppendLine(GetSpaces(preSpace) + "|| ('a' <= c && c <= 'f')");
            builder.AppendLine(GetSpaces(preSpace) + "|| ('A' <= c && c <= 'F'))");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.Append(c);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "return result.ToString();");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            builder.AppendLine(GetSpaces(preSpace) + "#endregion GetConstentNumber");
        }

        private void GenerateLexicalAnalyzerTokenFactoryIdentifier(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region GetIdentifier");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 获取标识符（函数名，变量名，等）");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("protected virtual bool GetIdentifier(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("result.TokenType = {0}.identifier;"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "StringBuilder builder = new StringBuilder();");
            builder.AppendLine(GetSpaces(preSpace) + "while (PtNextLetter < this.GetSourceCode().Length)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("if (ct == {0}.Letter"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + 
                string.Format("|| ct == {0}.Number"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + 
                string.Format("|| ct == {0}.UnderLine"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + 
                string.Format("|| ct == {0}.ChineseLetter)"
                , GetEnumCharTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "builder.Append(this.GetSourceCode()[PtNextLetter]);");
            builder.AppendLine(GetSpaces(preSpace) + "PtNextLetter++;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "break;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "result.Detail = builder.ToString();");

            builder.AppendLine(GetSpaces(preSpace) + "// specify if this string is a keyword");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("foreach (var item in {0}.keywords)", GetLexicalAnalyzerName()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if (item.ToString().Substring({0}) == result.Detail)",
                    GetKeywordPrefix4Token().Length));
                    //GetKeywordPrefix4SyntaxTreeNodeLeave(EnumProductionNodePosition.Leave).Length));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "result.TokenType = item;");
            builder.AppendLine(GetSpaces(preSpace) + "break;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            builder.AppendLine(GetSpaces(preSpace) + "return true;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "");

            builder.AppendLine(GetSpaces(preSpace) + string.Format(
                "public static readonly IEnumerable<{0}> keywords = new List<{0}>()", GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            // add keywords of the language
            foreach (var item in input.TerminalList)
            {
                if (IsIdentifier(item.NodeName))
                {
                    builder.AppendLine(GetSpaces(preSpace) + string.Format("{0}.{1},",
                        GetEnumTokenTypeSG(),
                        GetEnumTokenTypeSGItem(item)));
                }
            }
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "};");
            builder.AppendLine(GetSpaces(preSpace) + "");
            builder.AppendLine(GetSpaces(preSpace) + "#endregion GetIdentifier");
        }

        /// <summary>
        /// 判定标识符是否能够作为关键字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsIdentifier(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if ((!value.StartsWith("\"")) || (!value.EndsWith("\""))) return false;
            for (int i = 1; i < value.Length - 1; i++)
            {
                if (!(
                        ('a' <= value[i] && value[i] <= 'z')
                        || ('A' <= value[i] && value[i] <= 'Z')
                      )
                    )
                {
                    return false;   
                }
            }
            return true;
        }

        private void GenerateLexicalAnalyzerMethodNextToken(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// 从<code>{0}</code>开始获取下一个<code>Token</code>"
                , GetPropertyPtNextLetter()));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("protected override Token<{0}> NextToken()", GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("var result = new Token<{0}>();", GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("result.Line = {0};"
                , GetFieldm_CurrentLine()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("result.Column = {0};"
                , GetFieldm_CurrentColumn()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("result.IndexOfSourceCode = {0};"
                , GetPropertyPtNextLetter()));
            builder.AppendLine(GetSpaces(preSpace) + "var count = this.GetSourceCode().Length;");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if ({0} < 0 || {0} >= count) return result;"
                , GetPropertyPtNextLetter()));
            builder.AppendLine(GetSpaces(preSpace) + "var gotToken = false;");
            builder.AppendLine(GetSpaces(preSpace) + "var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);");
            builder.AppendLine(GetSpaces(preSpace) + "switch (ct)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            var map = GetTokenMap(input);
            map.Add(EnumCharType.Space, null);
            //map.Map.Add(CharProductionNodeListList.letter);
            //map.Map.Add(CharProductionNodeListList.chineseLetter);
            //map.Map.Add(CharProductionNodeListList.underline);
            //map.Map.Add(CharProductionNodeListList.constentNumber);
            foreach (var enumValue in map.Map)
            {
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("case {0}.{1}:"
                    , GetEnumCharTypeSG()
                    , enumValue.CharType));
                builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                    string.Format("gotToken = {0}(result);"
                    , GetMethodGetSomeTokenWithCharType(enumValue)));
                builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "break;");
            }

            builder.AppendLine(GetSpaces(preSpace) + "default:");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "gotToken = GetUnknown(result);");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "break;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "if (gotToken)");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("result.Length = {0} - result.IndexOfSourceCode;"
                , GetPropertyPtNextLetter()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("return result;"
                , GetPropertyPtNextLetter()));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "return null;");

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        private CharProductionNodeListList GetTokenMap(LL1GeneraterInput input)
        {
            var terminalList = input.TerminalList;
            //terminalList.Remove(ProductionNode.epsilonLeave);
            var map = new CharProductionNodeListList();
            foreach (var terminal in terminalList)
            {
                EnumCharType ct;
                var name = terminal.NodeName;
                if (name.Count() == 0)
                {
                    Console.WriteLine("{0}的节点名为空", terminal);
                    continue;
                }
                else
                {
                    if (terminal == ProductionNode.tail_identifier)
                    {
                        map.Add(EnumCharType.Letter, terminal);
                    }
                    else if (terminal == ProductionNode.tail_number)
                    {
                        map.Add(EnumCharType.Number, terminal);
                    }
                    else if (terminal == ProductionNode.tail_constString)
                    {
                        map.Add(EnumCharType.DoubleQuotation, terminal);
                    }
                    else if (terminal == ProductionNode.tail_null)
                    {
                        // nothing to do
                    }
                    else if (name[0] == '"' && name.Count() > 1)
                    {
                        ct = GetCharType(name[1]);
                        if (ct != EnumCharType.Letter
                            && ct != EnumCharType.ChineseLetter
                            && ct != EnumCharType.UnderLine)
                            map.Add(ct, terminal);
                        else
                        {
                            map.Add(ct, terminal);
                            //map.Add(EnumCharTypeCG.DoubleQuotation, terminal);
                        }
                    }
                    //else
                    //{
                    //    Console.WriteLine("错误的叶结点名：{0}", terminal.NodeName);
                    //    //ct = GetCharType(name[0]);
                    //    //map.Add(ct, terminal);
                    //}
                }
            }
            return map;
        }
        /// <summary>
        /// GetIdentifier
        /// <para>GetUnknown</para>
        /// <para>GetPlusOpt</para>
        /// <para>GetPlus</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetMethodGetSomeTokenWithCharType(CharProductionNodeList value)
        {
            var ct = value.CharType;
            var result = string.Empty;
            if (ct == EnumCharType.Letter
                || ct == EnumCharType.ChineseLetter
                || ct == EnumCharType.UnderLine)
                result = "GetIdentifier";
            else if (ct == EnumCharType.Number)
                result = "GetConstentNumber";
            else if (ct == EnumCharType.Quotation)
                result = "GetConstentChar";
            else if (ct == EnumCharType.DoubleQuotation)
                result = "GetConstentString";
            else if (ct == EnumCharType.Space)
                result = "GetSpace";
            else
            {
                result = string.Format("Get{0}{1}"
                    , ct
                    , value.MappedNodes.Count > 1 ? "Opt" : "");
            }
            return result; 
        }

        //private static string GetMethodGetSomeTokenWithCharType(EnumCharType ct)
        //{
        //    var result = string.Empty;
        //    if (ct == EnumCharType.Letter
        //        || ct == EnumCharType.ChineseLetter
        //        || ct == EnumCharType.UnderLine)
        //        result = "GetIdentifier";
        //    else if (ct == EnumCharType.Number)
        //        result = "GetConstentNumber";
        //    else if (ct == EnumCharType.Quotation)
        //        result = "GetConstentChar";
        //    else if (ct == EnumCharType.DoubleQuotation)
        //        result = "GetConstentString";
        //    else if (ct == EnumCharType.Space)
        //        result = "GetSpace";
        //    return result;
        //}

        /// <summary>
        /// 用于生成词法分析器
        /// </summary>
        class CharProductionNodeListList
        {
            //public static readonly CharProductionNodeList letter = new CharProductionNodeList(
            //    EnumCharType.Letter, new ProductionNode("BIT", EnumProductionNodePosition.Leave));
            //public static readonly CharProductionNodeList chineseLetter = new CharProductionNodeList(
            //    EnumCharType.Letter, new ProductionNode("祝威", EnumProductionNodePosition.Leave));
            //public static readonly CharProductionNodeList underline = new CharProductionNodeList(
            //    EnumCharType.Letter, new ProductionNode("_", EnumProductionNodePosition.Leave));
            //public static readonly CharProductionNodeList constentNumber = new CharProductionNodeList(
            //    EnumCharType.Letter, new ProductionNode("0123456789", EnumProductionNodePosition.Leave));

            List<CharProductionNodeList> m_map = new List<CharProductionNodeList>();

            public bool Contains(EnumCharType charType)
            {
                foreach (var item in this.Map)
                {
                    if (item.CharType==charType)
                    {
                        return true;
                    }
                }
                return false;
            }
            public List<CharProductionNodeList> Map
            {
                get { return m_map; }
            }
            public void Add(EnumCharType ct, ProductionNode node)
            {
                foreach (var item in this.m_map)
                {
                    if (item.CharType == ct)
                    {
                        foreach (var n in item.MappedNodes)
                        {
                            if (n == node)
                            {
                                return;
                            }
                        }
                        item.MappedNodes.Add(node);
                        return;
                    }
                }
                var newItem = new CharProductionNodeList(ct, node);
                this.m_map.Add(newItem);
            }
        }
        /// <summary>
        /// 用于生成词法分析器
        /// </summary>
        class CharProductionNodeList
        {
            public CharProductionNodeList(EnumCharType charType,params ProductionNode[] mappedNodes)
            {
                m_charType = charType;
                if (mappedNodes!=null)
                    foreach (var item in mappedNodes)
                    {
                        m_MappedNodes.Add(item);
                    }
            }
            public override string ToString()
            {
                //return string.Format("{0}:{1}");
                return base.ToString();
            }
            private EnumCharType m_charType;

            public EnumCharType CharType
            {
                get { return m_charType; }
                set { m_charType = value; }
            }
            private ProductionNodeList m_MappedNodes = new ProductionNodeList();

            public ProductionNodeList MappedNodes
            {
                get { return m_MappedNodes; }
                set { m_MappedNodes = value; }
            }

        }
        /// <summary>
        /// 生成词法分析器的构造函数
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="grammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLexicalAnalyzerConstructor(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// {0}的词法分析器", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + 
                string.Format("public {0}()"
                , GetLexicalAnalyzerName()));
            builder.AppendLine(GetSpaces(preSpace) + "{ }");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// {0}的词法分析器", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"sourceCode\">要分析的源代码</param>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("public {0}(string sourceCode)"
                , GetLexicalAnalyzerName()));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + ": base(sourceCode)");
            builder.AppendLine(GetSpaces(preSpace) + "{ }");
        }

        #endregion 生成词法分析器类的源代码
        

    }
}
