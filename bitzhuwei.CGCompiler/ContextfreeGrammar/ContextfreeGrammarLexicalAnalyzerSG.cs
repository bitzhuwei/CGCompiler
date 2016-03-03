using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        ///// <summary>
        ///// 生成整个词法分析器的源代码
        ///// </summary>
        ///// <returns></returns>
        //public string GenerateLexicalAnalyzer()
        //{
        //    int preSpace = 0;
        //    LL1GeneraterInput input = new LL1GeneraterInput(this);
        //    return GenerateLexicalAnalyzer(ref preSpace, input);
        //}
        ///// <summary>
        ///// 生成整个词法分析器的源代码
        ///// </summary>
        ///// <param name="input">输入</param>
        ///// <returns></returns>
        //public string GenerateLexicalAnalyzer(LL1GeneraterInput input)
        //{
        //    int preSpace = 0;

        //    return GenerateLexicalAnalyzer(ref preSpace, input);
        //}
        public void GenerateLexicalAnalyzer(string fullname, LL1GeneraterInput input)
        {
            //TextWriterTraceListener listener = new TextWriterTraceListener(fullname);
            StreamWriter writer = new StreamWriter(fullname, false);
            TextWriterTraceListener listener = new TextWriterTraceListener(writer);
            Debug.Listeners.Add(listener);

            Debug.IndentSize = 4;
            Debug.IndentLevel = 0;
            GenerateLexicalAnalyzer(input);

            Debug.Flush();
            Debug.Close();
            Debug.Listeners.Remove(listener);
        }
        /// <summary>
        /// 生成整个词法分析器的源代码
        /// </summary>
        /// <param name="grammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public void GenerateLexicalAnalyzer(LL1GeneraterInput input)
        {
            Debug.WriteLine("using System;");
            Debug.WriteLine("using System.Text;");
            Debug.WriteLine("using System.Text.RegularExpressions;");
            Debug.WriteLine("using System.Collections.Generic;");
            Debug.WriteLine("using bitzhuwei.CompilerBase;");
            Debug.WriteLine("");
            Debug.WriteLine(
                string.Format("namespace {0}", this.Namespace));
            Debug.WriteLine("{");
            Debug.Indent();
            GenerateLexicalAnalyzerClass(input);
            Debug.Unindent(); 
            Debug.WriteLine("}");
            Debug.WriteLine("");
        }
        #endregion 生成整个词法分析器的源代码

        #region 生成词法分析器类的源代码
        /// <summary>
        /// 生成词法分析器类的源代码（只包含类）
        /// </summary>
        /// <param name="grammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public void GenerateLexicalAnalyzerClass(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine(string.Format("/// {0}的词法分析器", this.GrammarName));
            Debug.WriteLine("/// </summary>");
            //LexicalAnalyzerCG : LexicalAnalyzerBase<EnumTokenTypeCG>

            Debug.WriteLine(string.Format("public partial class {0} : {1}<{2}>"
                , GetLexicalAnalyzerName()
                , GetLexicalAnalyzerBaseName()
                , GetEnumTokenTypeSG()));

            Debug.WriteLine("{");
            Debug.Indent();

            GenerateLexicalAnalyzerConstructor(input);
            GenerateLexicalAnalyzerMethodNextToken(input);
            GenerateLexicalAnalyzerTokenFactory(input);
            GenerateLexicalAnalyzerMethodGetCharType(input);
            GenerateLexicalAnalyzerFieldregChineseLetter(input);

            Debug.Unindent();
            Debug.WriteLine("}");
        }
        /// <summary>
        /// 字段：汉字
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="grammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLexicalAnalyzerFieldregChineseLetter(LL1GeneraterInput input)
        {
            //private static readonly Regex regChineseLetter = new Regex("^[^\x00-\xFF]");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 汉字 new Regex(\"^[^\\x00-\\xFF]\")");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine(
                "private static readonly Regex regChineseLetter = new Regex(\"^[^\\x00-\\xFF]\");");
        }

        private void GenerateLexicalAnalyzerMethodGetCharType(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 获取字符类型");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"c\">要归类的字符</param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual {0} GetCharType(char c)"
                , GetEnumCharTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z')) return {0}.Letter;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if ('0' <= c && c <= '9') return {0}.Number;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '_') return {0}.UnderLine;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '.') return {0}.Dot;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == ',') return {0}.Comma;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '+') return {0}.Plus;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '-') return {0}.Minus;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '*') return {0}.Multiply;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '/') return {0}.Divide;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '%') return {0}.Percent;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '^') return {0}.Xor;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '&') return {0}.And;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '|') return {0}.Or;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '~') return {0}.Reverse;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '$') return {0}.Dollar;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '<') return {0}.LessThan;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '>') return {0}.GreaterThan;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '(') return {0}.LeftParentheses;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == ')') return {0}.RightParentheses;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '[') return {0}.LeftBracket;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == ']') return {0}.RightBracket;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '{1}') return {0}.LeftBrace;"
                , GetEnumCharTypeSG(), "{"));
            Debug.WriteLine(
                string.Format("if (c == '{1}') return {0}.RightBrace;"
                , GetEnumCharTypeSG(), "}"));
            Debug.WriteLine(
                string.Format("if (c == '!') return {0}.Not;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '#') return {0}.Pound;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '\\\\') return {0}.Slash;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '?') return {0}.Question;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '\\\'') return {0}.Quotation;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '\"') return {0}.DoubleQuotation;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == ':') return {0}.Colon;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == ';') return {0}.Semicolon;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '=') return {0}.Equality;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if (c == '@') return {0}.At;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("if ({1}.IsMatch(Convert.ToString(c))) return {0}.ChineseLetter;"
                , GetEnumCharTypeSG()
                , GetFieldregChineseLetter()));
            Debug.WriteLine(
                string.Format("if (c == ' ' || c == '\\t' || c == '\\r' || c == '\\n') return {0}.Space;"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(
                string.Format("return {0}.Unknown;"
                , GetEnumCharTypeSG()));
            Debug.Unindent();
            Debug.WriteLine("}");
        }

        public string GetFieldregChineseLetter()
        {
            return "regChineseLetter";
        }

        public string GetEnumCharTypeSG()
        {
            return string.Format("EnumCharType{0}", this.GrammarName);
        }

        private void GenerateLexicalAnalyzerTokenFactory(LL1GeneraterInput input)
        {
            Debug.WriteLine("#region 获取某类型的单词");

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
                Debug.WriteLine("/// <summary>");
                Debug.WriteLine("/// )");
                Debug.WriteLine("/// </summary>");
                Debug.WriteLine("/// <param name=\"result\"></param>");
                Debug.WriteLine("/// <returns></returns>");
                Debug.WriteLine(
                    string.Format("protected virtual bool Get{0}{1}(Token<{2}> result)"
                    , item.CharType
                    , conditions > 1 ? "Opt" : ""
                    , GetEnumTokenTypeSG()));
                Debug.WriteLine("{");
                Debug.Indent();
                Debug.WriteLine("var count = this.GetSourceCode().Length;");
                Debug.WriteLine(
                    string.Format("//item.CharType: {0}"
                    , item.CharType));
                if (item.MappedNodes.Count > 1)
                {
                    Debug.WriteLine("//todo: maybe you need to set TokenType to their right position.");
                }
                Debug.WriteLine("//Mapped nodes:");
                foreach (var node in item.MappedNodes)
                {
                    Debug.WriteLine(
                        string.Format("//    {0}", node));
                    Debug.WriteLine(
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
                    Debug.WriteLine(
                        string.Format("if (PtNextLetter + {0} <= count)", key));
                    Debug.WriteLine("{");
                    Debug.Indent();
                    Debug.WriteLine(
                        string.Format("var str = this.GetSourceCode().Substring(PtNextLetter, {0});"
                        , key));
                    foreach (var sign in token)
                    {
                        if (key == 2 && sign.NodeName[1] == '/')
                        {
                            Debug.WriteLine(
                                "if (\"//\" == str) { SkipSingleLineNote(); return false; }");
                        }
                        //var start = sign.NodeName[0] == '"' ? 1 : 0;
                        Debug.WriteLine(
                            string.Format("if ({0} == str)"
                            , sign.NodeName
                            //.Substring(start, sign.NodeName.Length - start - start)
                            ));
                        Debug.WriteLine("{");
                        Debug.Indent();
                        Debug.WriteLine(
                            string.Format("result.TokenType = {0}.{1};"
                            , GetEnumTokenTypeSG()
                            , GetEnumTokenTypeSGItem(sign)));
                        Debug.WriteLine("result.Detail = str;");
                        Debug.WriteLine(
                            string.Format("PtNextLetter += {0};", key));
                        Debug.WriteLine("return true;");
                        Debug.Unindent();
                        Debug.WriteLine("}");

                    }
                    Debug.Unindent();
                    Debug.WriteLine("}");
                }

                Debug.WriteLine("");
                Debug.WriteLine("return false;");
                Debug.Unindent();
                Debug.WriteLine("}");
            }
            if (map.Contains(EnumCharType.Letter)
                || map.Contains(EnumCharType.ChineseLetter)
                || map.Contains(EnumCharType.UnderLine)
                )
                GenerateLexicalAnalyzerTokenFactoryIdentifier(input);
            if (map.Contains(EnumCharType.Number))
                GenerateLexicalAnalyzerTokenFactoryConstentNumber(input);
            if (map.Contains(EnumCharType.Quotation))
                GenerateLexicalAnalyzerTokenFactoryConstentChar(input);
            if (map.Contains(EnumCharType.DoubleQuotation))
                GenerateLexicalAnalyzerTokenFactoryConstentString(input);
            GenerateLexicalAnalyzerTokenFactoryUnknown(input);
            GenerateLexicalAnalyzerTokenFactorySpace(input);
            GenerateLexicalAnalyzerTokenFactoryMultilineNote(input);
            GenerateLexicalAnalyzerTokenFactorySingleLineNote(input);


            Debug.WriteLine("#endregion 获取某类型的单词");
        }

        private void GenerateLexicalAnalyzerTokenFactorySingleLineNote(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 跳过单行注释");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine("protected virtual void SkipSingleLineNote()");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("int count = this.GetSourceCode().Length;");
            Debug.WriteLine("char cNext;");
            Debug.WriteLine("while (PtNextLetter < count)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("cNext = GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (cNext == '\\r' || cNext == '\\n')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryMultilineNote(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 跳过多行注释");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine("protected virtual void SkipMultilineNote()");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("int count = this.GetSourceCode().Length;");
            Debug.WriteLine("while (PtNextLetter < count)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("if (GetSourceCode()[PtNextLetter] == '*')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("if (PtNextLetter < count)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("if (GetSourceCode()[PtNextLetter] == '/')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.Indent();
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.Indent();
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
        }

        private void GenerateLexicalAnalyzerTokenFactorySpace(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// space tab \\r \\n");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetSpace(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("char c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("if (c == '\\n')// || c == '\\r') //换行：Windows：\\r\\n Linux：\\n");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("this.m_CurrentLine++;");
            Debug.WriteLine("this.m_CurrentColumn = 0;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return false;");
            Debug.Unindent();
            Debug.WriteLine("}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryUnknown(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 未知符号");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetUnknown(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("result.TokenType = {0}.unknown;"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("result.Detail = this.GetSourceCode()[PtNextLetter].ToString();");
            Debug.WriteLine("result.LexicalError = true;");
            Debug.WriteLine("result.Tag = string.Format(\"发现未知字符[{0}]。\", result.Detail);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryConstentString(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 字符串常量 \"XXX\"");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetConstentString(Token<{0}> result)"
                , GetEnumTokenTypeSG()));//GetMethodGetSomeTokenWithCharType
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("result.TokenType = {0}.constString;"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("int count = this.GetSourceCode().Length;");
            Debug.WriteLine("StringBuilder constString = new StringBuilder(\"\\\"\");");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("bool notMatched = true;");
            Debug.WriteLine("char c;");
            Debug.WriteLine("while ((PtNextLetter < count) && notMatched)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (c == '\"')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("constString.Append(c);");
            Debug.WriteLine("notMatched = false;");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else if (c == '\\r' || c == '\\n')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("constString.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("result.Detail = constString.ToString();");
            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryConstentChar(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 字符常量 'X'");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetQuotation(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("result.TokenType = {0}.ConstentChar;"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("int count = this.GetSourceCode().Length;");
            Debug.WriteLine("int ptCharHead = PtNextLetter;");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("char cNext;");
            Debug.WriteLine("while (PtNextLetter < count && PtNextLetter - ptCharHead < 4)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("cNext = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (cNext == '\\'')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else if (cNext == '\\\\')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("PtNextLetter += 2;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("int length = PtNextLetter - ptCharHead;");
            Debug.WriteLine("result.Detail = this.GetSourceCode().Substring(ptCharHead, length);");
            Debug.WriteLine("if (length < 3)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.LexicalError = true;");
            Debug.WriteLine("result.Tag = string.Format(\"字符标识[{0}]错误。\", result.Detail);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else if (length == 3)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("var ect = GetCharType(this.GetSourceCode()[ptCharHead + 1]);");
            Debug.WriteLine(
                string.Format("if (ect == {0}.{1})"
                , GetEnumCharTypeSG()
                , GetEnumCharTypeSGItemDefaultName()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.LexicalError = true;");
            Debug.WriteLine("result.Tag = string.Format(\"字符标识[{0}]错误。\", result.Detail);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else //if (length == 4)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("if (this.GetSourceCode()[ptCharHead] != '\\''");
            Debug.Indent();
            Debug.WriteLine("|| this.GetSourceCode()[ptCharHead + 1] != '\\\\'");
            Debug.WriteLine(
                string.Format("|| GetCharType(this.GetSourceCode()[ptCharHead + 2]) == {0}.{1}"
                , GetEnumCharTypeSG()
                , GetEnumCharTypeSGItemDefaultName()));
            Debug.WriteLine("|| this.GetSourceCode()[ptCharHead + 3] != '\\'')");
            Debug.Unindent();
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.LexicalError = true;");
            Debug.WriteLine("result.Tag = string.Format(\"字符标识[{0}]错误。\", result.Detail);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
        }

        private void GenerateLexicalAnalyzerTokenFactoryConstentNumber(LL1GeneraterInput input)
        {
            Debug.WriteLine("#region GetConstentNumber");
            //GetConstentNumber
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 数值");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetConstentNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("result.TokenType = {0}.{1};"
                , GetEnumTokenTypeSG()
                , GetEnumTokenTypeSGItem(ProductionNode.tail_number)));
            Debug.WriteLine("if (this.GetSourceCode()[PtNextLetter] == '0')//可能是八进制或十六进制数");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("if (PtNextLetter + 1 < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("char c = this.GetSourceCode()[PtNextLetter + 1];");
            Debug.WriteLine("if (c == 'x' || c == 'X')");
            Debug.WriteLine("{//十六进制数");
            Debug.Indent();
            Debug.WriteLine("return GetConstentHexadecimalNumber(result);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine(
                string.Format("else if (GetCharType(c) == {0}.Number)"
                , GetEnumCharTypeSG()));
            Debug.WriteLine("{//八进制数");
            Debug.Indent();
            Debug.WriteLine("return GetConstentOctonaryNumber(result);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else//十进制数");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("return GetConstentDecimalNumber(result);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.WriteLine("{//源代码最后一个字符 0");
            Debug.Indent();
            Debug.WriteLine("result.Detail = \"0\";//0");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else//十进制数");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("return GetConstentDecimalNumber(result);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 十进制数");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetConstentDecimalNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("char c;");
            Debug.WriteLine("StringBuilder tag = new StringBuilder();");
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("string numberSerial1, numberSerial2, numberSerial3;");
            Debug.WriteLine("numberSerial1 = GetNumberSerial(this.GetSourceCode(), 10);");
            Debug.WriteLine("tag.Append(numberSerial1);");
            Debug.WriteLine("result.LexicalError = string.IsNullOrEmpty(numberSerial1);");
            Debug.WriteLine("if (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (c == 'l' || c == 'L')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("tag.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("if (c == '.')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("tag.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("numberSerial2 = GetNumberSerial(this.GetSourceCode(), 10);");
            Debug.WriteLine("tag.Append(numberSerial2);");
            Debug.WriteLine("result.LexicalError = result.LexicalError || string.IsNullOrEmpty(numberSerial2);");
            Debug.WriteLine("if (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("if (c == 'e' || c == 'E')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("tag.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("if (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (c == '+' || c == '-')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("tag.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("numberSerial3 = GetNumberSerial(this.GetSourceCode(), 10);");
            Debug.WriteLine("tag.Append(numberSerial3);");
            Debug.WriteLine("result.LexicalError = result.LexicalError || string.IsNullOrEmpty(numberSerial3);");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("result.Detail = tag.ToString();");
            Debug.WriteLine("if (result.LexicalError)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.Tag = string.Format(\"十进制数[{0}]格式错误，无法解析。\", tag.ToString());");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 八进制数");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetConstentOctonaryNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("char c;");
            Debug.WriteLine("StringBuilder tag = new StringBuilder(this.GetSourceCode().Substring(PtNextLetter, 1));");
            Debug.WriteLine("PtNextLetter++;");
            Debug.WriteLine("string numberSerial = GetNumberSerial(this.GetSourceCode(), 8);");
            Debug.WriteLine("tag.Append(numberSerial);");
            Debug.WriteLine("if (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (c == 'l' || c == 'L')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("tag.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("result.Detail = tag.ToString();");
            Debug.WriteLine("if (string.IsNullOrEmpty(numberSerial))");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.LexicalError = true;");
            Debug.WriteLine("result.Tag = string.Format(\"八进制数[{0}]格式错误，无法解析。\", tag.ToString());");
            Debug.WriteLine("return false;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 十六进制数");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetConstentHexadecimalNumber(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("char c;");
            Debug.WriteLine("StringBuilder tag = new StringBuilder(this.GetSourceCode().Substring(PtNextLetter, 2));");
            Debug.WriteLine("PtNextLetter += 2;");
            Debug.WriteLine("string numberSerial = GetNumberSerial(this.GetSourceCode(), 16);");
            Debug.WriteLine("tag.Append(numberSerial);");
            Debug.WriteLine("if (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (c == 'l' || c == 'L')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("tag.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("result.Detail = tag.ToString();");
            Debug.WriteLine("if (string.IsNullOrEmpty(numberSerial))");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.LexicalError = true;");
            Debug.WriteLine("result.Tag = string.Format(\"十六进制数[{0}]格式错误。\", tag.ToString());");
            Debug.WriteLine("return false;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 数字序列");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"sourceCode\"></param>");
            Debug.WriteLine("/// <param name=\"scale\">进制</param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine("protected virtual string GetNumberSerial(string sourceCode, int scale)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("if (scale == 10)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("return GetNumberSerialDecimal(this.GetSourceCode());");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("if (scale == 16)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("return GetNumberSerialHexadecimal(this.GetSourceCode());");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("if (scale == 8)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("return GetNumberSerialOctonary(this.GetSourceCode());");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return string.Empty;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 十进制数序列");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"sourceCode\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine("protected virtual string GetNumberSerialDecimal(string sourceCode)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("StringBuilder result = new StringBuilder(String.Empty);");
            Debug.WriteLine("char c;");
            Debug.WriteLine("while (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if ('0' <= c && c <= '9')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.Indent();
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return result.ToString();");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 八进制数序列");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"sourceCode\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine("protected virtual string GetNumberSerialOctonary(string sourceCode)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("StringBuilder result = new StringBuilder(String.Empty);");
            Debug.WriteLine("char c;");
            Debug.WriteLine("while (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if ('0' <= c && c <= '7')");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.Indent();
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return result.ToString();");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 十六进制数序列（不包括0x前缀）");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"sourceCode\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine("protected virtual string GetNumberSerialHexadecimal(string sourceCode)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("StringBuilder result = new StringBuilder(String.Empty);");
            Debug.WriteLine("char c;");
            Debug.WriteLine("while (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("c = this.GetSourceCode()[PtNextLetter];");
            Debug.WriteLine("if (('0' <= c && c <= '9')");
            Debug.WriteLine("|| ('a' <= c && c <= 'f')");
            Debug.WriteLine("|| ('A' <= c && c <= 'F'))");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.Append(c);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.Indent();
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("return result.ToString();");
            Debug.Unindent();
            Debug.WriteLine("}");

            Debug.WriteLine("#endregion GetConstentNumber");
        }

        private void GenerateLexicalAnalyzerTokenFactoryIdentifier(LL1GeneraterInput input)
        {
            Debug.WriteLine("#region GetIdentifier");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine("/// 获取标识符（函数名，变量名，等）");
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"result\"></param>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected virtual bool GetIdentifier(Token<{0}> result)"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("result.TokenType = {0}.identifier;"
                , GetEnumTokenTypeSG()));
            Debug.WriteLine("StringBuilder builder = new StringBuilder();");
            Debug.WriteLine("while (PtNextLetter < this.GetSourceCode().Length)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);");
            Debug.WriteLine(
                string.Format("if (ct == {0}.Letter"
                , GetEnumCharTypeSG()));
            Debug.Indent();
            Debug.WriteLine(string.Format("|| ct == {0}.Number"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(string.Format("|| ct == {0}.UnderLine"
                , GetEnumCharTypeSG()));
            Debug.WriteLine(string.Format("|| ct == {0}.ChineseLetter)"
                , GetEnumCharTypeSG()));
            Debug.Unindent();
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("builder.Append(this.GetSourceCode()[PtNextLetter]);");
            Debug.WriteLine("PtNextLetter++;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.WriteLine("{ break; }");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("result.Detail = builder.ToString();");

            Debug.WriteLine("// specify if this string is a keyword");
            Debug.WriteLine(
                string.Format("foreach (var item in {0}.keywords)", GetLexicalAnalyzerName()));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("if (item.ToString().Substring({0}) == result.Detail)",
                    GetKeywordPrefix4Token().Length));
            //GetKeywordPrefix4SyntaxTreeNodeLeave(EnumProductionNodePosition.Leave).Length));
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine("result.TokenType = item;");
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.Unindent();
            Debug.WriteLine("}");

            Debug.WriteLine("return true;");
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("");

            Debug.WriteLine(string.Format(
                "public static readonly IEnumerable<{0}> keywords = new List<{0}>()", GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();
            // add keywords of the language
            foreach (var item in input.TerminalList)
            {
                if (IsIdentifier(item.NodeName))
                {
                    Debug.WriteLine(string.Format("{0}.{1},",
                        GetEnumTokenTypeSG(),
                        GetEnumTokenTypeSGItem(item)));
                }
            }
            Debug.Unindent();
            Debug.WriteLine("};");
            Debug.WriteLine("");
            Debug.WriteLine("#endregion GetIdentifier");
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

        private void GenerateLexicalAnalyzerMethodNextToken(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine(
                string.Format("/// 从<code>{0}</code>开始获取下一个<code>Token</code>"
                , GetPropertyPtNextLetter()));
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <returns></returns>");
            Debug.WriteLine(
                string.Format("protected override Token<{0}> NextToken()", GetEnumTokenTypeSG()));
            Debug.WriteLine("{");
            Debug.Indent();

            Debug.WriteLine(
                string.Format("var result = new Token<{0}>();", GetEnumTokenTypeSG()));
            Debug.WriteLine(
                string.Format("result.Line = {0};"
                , GetFieldm_CurrentLine()));
            Debug.WriteLine(
                string.Format("result.Column = {0};"
                , GetFieldm_CurrentColumn()));
            Debug.WriteLine(
                string.Format("result.IndexOfSourceCode = {0};"
                , GetPropertyPtNextLetter()));
            Debug.WriteLine("var count = this.GetSourceCode().Length;");
            Debug.WriteLine(
                string.Format("if ({0} < 0 || {0} >= count) return result;"
                , GetPropertyPtNextLetter()));
            Debug.WriteLine("var gotToken = false;");
            Debug.WriteLine("var ct = GetCharType(this.GetSourceCode()[PtNextLetter]);");
            Debug.WriteLine("switch (ct)");
            Debug.WriteLine("{");
            Debug.Indent();

            var map = GetTokenMap(input);
            map.Add(EnumCharType.Space, null);
            //map.Map.Add(CharProductionNodeListList.letter);
            //map.Map.Add(CharProductionNodeListList.chineseLetter);
            //map.Map.Add(CharProductionNodeListList.underline);
            //map.Map.Add(CharProductionNodeListList.constentNumber);
            foreach (var enumValue in map.Map)
            {
                Debug.WriteLine(
                    string.Format("case {0}.{1}:"
                    , GetEnumCharTypeSG()
                    , enumValue.CharType));
                Debug.Indent();
                Debug.WriteLine(string.Format("gotToken = {0}(result);"
                    , GetMethodGetSomeTokenWithCharType(enumValue)));
                Debug.WriteLine("break;");
                Debug.Unindent();
            }

            Debug.WriteLine("default:");
            Debug.Indent();
            Debug.WriteLine("gotToken = GetUnknown(result);");
            Debug.WriteLine("break;");
            Debug.Unindent();
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("if (gotToken)");
            Debug.WriteLine("{");
            Debug.Indent();
            Debug.WriteLine(
                string.Format("result.Length = {0} - result.IndexOfSourceCode;"
                , GetPropertyPtNextLetter()));
            Debug.WriteLine(
                string.Format("return result;"
                , GetPropertyPtNextLetter()));
            Debug.Unindent();
            Debug.WriteLine("}");
            Debug.WriteLine("else");
            Debug.Indent();
            Debug.WriteLine("return null;");
            Debug.Unindent();

            Debug.Unindent();
            Debug.WriteLine("}");
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
                    if (item.CharType == charType)
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
            public CharProductionNodeList(EnumCharType charType, params ProductionNode[] mappedNodes)
            {
                m_charType = charType;
                if (mappedNodes != null)
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
        private void GenerateLexicalAnalyzerConstructor(LL1GeneraterInput input)
        {
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine(
                string.Format("/// {0}的词法分析器", this.GrammarName));
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine(
                string.Format("public {0}()"
                , GetLexicalAnalyzerName()));
            Debug.WriteLine("{ }");
            Debug.WriteLine("/// <summary>");
            Debug.WriteLine(
                string.Format("/// {0}的词法分析器", this.GrammarName));
            Debug.WriteLine("/// </summary>");
            Debug.WriteLine("/// <param name=\"sourceCode\">要分析的源代码</param>");
            Debug.WriteLine(
                string.Format("public {0}(string sourceCode)"
                , GetLexicalAnalyzerName()));
            Debug.Indent();
            Debug.WriteLine(": base(sourceCode)");
            Debug.Unindent();
            Debug.WriteLine("{ }");
        }

        #endregion 生成词法分析器类的源代码


    }
}
