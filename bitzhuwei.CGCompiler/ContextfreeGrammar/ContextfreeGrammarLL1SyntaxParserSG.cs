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
        #region 生成整个语法分析器的源代码
        
        /// <summary>
        /// 生成LL1语法分析器的源代码
        /// </summary>
        /// <returns></returns>
        public string GenerateLL1SyntaxParser()
        {
            int preSpace = 0;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateLL1SyntaxParser(ref preSpace, input);
        }
        /// <summary>
        /// 生成LL1语法分析器的源代码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateLL1SyntaxParser(LL1GeneraterInput input)
        {
            int preSpace = 0;

            return GenerateLL1SyntaxParser(ref preSpace, input);
        }

        /// <summary>
        /// 生成LL1语法分析器的源代码
        /// </summary>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateLL1SyntaxParser(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetSpaces(preSpace) + "using System;");
            builder.AppendLine(GetSpaces(preSpace) + "using bitzhuwei.CompilerBase;");
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("namespace {0}", this.Namespace));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GenerateLL1SyntaxParserClass(ref preSpace, input));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace));
            return builder.ToString();
        }
        #endregion 生成整个语法分析器的源代码
        
        #region 生成语法分析器类的源代码
        /// <summary>
        /// 生成LL1语法分析器的源代码（只包含类）
        /// </summary>
        /// <returns></returns>
        public string GenerateLL1SyntaxParserClass()
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateLL1SyntaxParser(ref preSpace, input);
        }
        /// <summary>
        /// 生成LL1语法分析器的源代码（只包含类）
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateLL1SyntaxParserClass(LL1GeneraterInput input)
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;

            return GenerateLL1SyntaxParserClass(ref preSpace, input);
        }

        /// <summary>
        /// 生成LL1语法分析器的源代码（只包含类）
        /// </summary>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateLL1SyntaxParserClass(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("/// {0}的LL1语法分析器", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("public partial class {0} : {1}<{2}, {3}, {4}>"
                ,GetLL1SyntaxParserName()
                ,GetLL1SyntaxParserBaseName()
                , GetEnumTokenTypeSG()
                , GetEnumVTypeSG()
                , GetTreeNodeValueSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            GenerateLL1SyntaxParserFieldFuncParseDerivation(builder, ref preSpace, input);
            GenerateLL1SyntaxParserFieldm_Terminal(builder, ref preSpace, input);
            GenerateLL1SyntaxParserFieldm_NonTerminal(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodGetFuncParseDerivation(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodParseDerivation(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodDerivation(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodFillMapCells(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodInitFunc(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodInitMap(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodConstructor(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodReset(builder, ref preSpace, input);
            GenerateLL1SyntaxParserMethodSetMapLinesAndColumns(builder, ref preSpace, input);

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            return builder.ToString();
        }
        #endregion 生成语法分析器类的源代码
        /// <summary>
        /// private void SetMapLinesAndColumns()
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="grammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodSetMapLinesAndColumns(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region SetMapLinesAndColumns()");
            builder.AppendLine(GetSpaces(preSpace));

            builder.AppendLine(GetSpaces(preSpace) + "private void SetMapLinesAndColumns()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            var nonTerminals = input.NonTerminalList;
            for (int i = 0; i < nonTerminals.Count; i++)
            {
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("{0}.SetLine({1}, {2}.{3});"
                        , GetFieldm_Map()
                        , i
                        , GetEnumVTypeSG()
                        , GetEnumVTypeSGItem(nonTerminals[i])));
            }
            builder.AppendLine(GetSpaces(preSpace));
            var terminals = input.TerminalList;
            //terminals.Remove(ProductionNode.epsilonLeave);
            //terminals.Add(ProductionNode.epsilonLeave);
            terminals.Add(ProductionNode.startEndLeave);
            for (int i = 0; i < terminals.Count; i++)
            {
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("{0}.SetLine({1}, {2}.{3});"
                        , GetFieldm_Map()
                        , i + nonTerminals.Count
                        , GetEnumVTypeSG()
                        , GetEnumVTypeSGItem(terminals[i])));
            }

            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace));
            //terminals.Remove(ProductionNode.epsilonLeave);
            for (int i = 0; i < terminals.Count; i++)
            {
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("{0}.SetColumn({1}, {2}.{3});"
                        , GetFieldm_Map()
                        , i
                        , GetEnumTokenTypeSG()
                        , GetEnumTokenTypeSGItem(terminals[i])));
            }

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion SetMapLinesAndColumns()");
        }
        /// <summary>
        /// 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodReset(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析");
            builder.AppendLine(GetSpaces(preSpace));

            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "public override void Reset()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0} = 0;"
                    , GetFieldm_ptNextToken()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0}.Clear();"
                    , GetFieldm_ParserStack()));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0}.Push({1});"
                    , GetFieldm_ParserStack()
                    , GetFieldStackItem(ProductionNode.startEndLeave)));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0}.Push({1});"
                    , GetFieldm_ParserStack()
                    , GetFieldStackItem(input.NonTerminalList[0])));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("if ({0}.Count == 0)"
                    , GetFieldm_TokenListSource()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("var newToken = new Token<{0}>()"
                    , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "Detail = \"#\",");
            builder.AppendLine(GetSpaces(preSpace) + "Line = 0,");
            builder.AppendLine(GetSpaces(preSpace) + "Column = 0,");
            builder.AppendLine(GetSpaces(preSpace) + "IndexOfSourceCode = 0,");
            builder.AppendLine(GetSpaces(preSpace) + "Length = 1,");
            builder.AppendLine(GetSpaces(preSpace) + "LexicalError = false,");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("TokenType = {0}.{1}"
                    , GetEnumTokenTypeSG()
                    , GetEnumTokenTypeSGItem(ProductionNode.startEndLeave)));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "};");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0}.Add(newToken);"
                    , GetFieldm_TokenListSource()));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace) + "else");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("var token = {0}[{0}.Count - 1];"
                    , GetFieldm_TokenListSource()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("var newToken = new Token<{0}>()"
                    , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            //todo
            builder.AppendLine(GetSpaces(preSpace) + "Detail = \"#\",");
            builder.AppendLine(GetSpaces(preSpace) + "Line = token.Line,");
            builder.AppendLine(GetSpaces(preSpace) + "Column = token.Column + token.Length + 1,");
            builder.AppendLine(GetSpaces(preSpace) + "IndexOfSourceCode = token.IndexOfSourceCode + token.Length + 1,");
            builder.AppendLine(GetSpaces(preSpace) + "Length = 1,");
            builder.AppendLine(GetSpaces(preSpace) + "LexicalError = false,");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("TokenType = {0}.{1}"
                    , GetEnumTokenTypeSG()
                    , GetEnumTokenTypeSGItem(ProductionNode.startEndLeave)));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "};");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0}.Add(newToken);"
                    , GetFieldm_TokenListSource()));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 重置语法分析器到初始状态，这样就可以重新对上次分析过的单词列表进行分析");
        }
        /// <summary>
        /// public SyntaxParserCG() (+1 overloads)
        /// <para></para>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodConstructor(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            var parserMap = input.LL1ParserMapInput;
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// {0}的语法分析器"
                    , GetLL1SyntaxParserName()));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("public {0}()"
                    , GetLL1SyntaxParserName()));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                string.Format(": base({0}, {1}) {2}"
                , parserMap.GetLineCount() + parserMap.GetColumnCount()
                , parserMap.GetColumnCount()
                , "{ }"));
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// {0}的语法分析器"
                    , GetLL1SyntaxParserName()));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"tokens\">要分析的单词列表</param>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("public {0}(TokenList<{1}> tokens)"
                    , GetLL1SyntaxParserName()
                    , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                string.Format(": base({0}, {1})"
                , parserMap.GetLineCount() + parserMap.GetColumnCount()
                , parserMap.GetColumnCount()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "m_TokenListSource = tokens;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }
        /// <summary>
        /// public override void InitMap()
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodInitMap(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 初始化LL(1)分析表");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "public override void InitMap()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "InitFunc();");
            builder.AppendLine(GetSpaces(preSpace) + "// init parser map");
            builder.AppendLine(GetSpaces(preSpace) + "SetMapLinesAndColumns();");
            builder.AppendLine(GetSpaces(preSpace) + "FillMapCells();");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }
        /// <summary>
        /// 为分析表中的元素配置分析函数
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodInitFunc(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            //FuncParseStart_lessThan = 
            //    new Func<
            //        SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>,
            //        ISyntaxParser<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>, 
            //        SyntaxTree<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
            //        >(ParseStart_lessThan);
            builder.AppendLine(GetSpaces(preSpace) + "#region 为分析表中的元素配置分析函数");
            builder.AppendLine(GetSpaces(preSpace));

            builder.AppendLine(GetSpaces(preSpace) + "private void InitFunc()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            var terminalList = input.TerminalList;
            //terminalList.Remove(ProductionNode.epsilonLeave);
            terminalList.Add(ProductionNode.startEndLeave);
            var parserMap = input.LL1ParserMapInput;
            var nonTerminalList = input.NonTerminalList;
            foreach (var left in nonTerminalList)
            {
                foreach (var next in terminalList)
                {
                    var derivationList = parserMap.GetDerivationList(left, next);
                    if (derivationList.Count > 0)
                    {
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("{0} = "
                            , GetFieldFuncParseName(left, next)));
                        IncreasepreSpace(ref preSpace);

                        builder.AppendLine(GetSpaces(preSpace) + string.Format("new CandidateFunction<{0}, {1}, {2}>({3});",
                            GetEnumTokenTypeSG(),
                            GetEnumVTypeSG(),
                            GetTreeNodeValueSG(),
                            GetMethodParseName(left,next)));

                        DecreasepreSpace(ref preSpace);
                    }
                }
            }
            builder.AppendLine(GetSpaces(preSpace));
            //terminalList.Remove(ProductionNode.startEndLeave);
            foreach (var leave in terminalList)
            {
                builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("{0} = "
                            , GetFieldFuncParseName(leave)));
                IncreasepreSpace(ref preSpace);

                builder.AppendLine(GetSpaces(preSpace) + string.Format("new CandidateFunction<{0}, {1}, {2}>({3});",
                           GetEnumTokenTypeSG(),
                           GetEnumVTypeSG(),
                           GetTreeNodeValueSG(),
                           GetMethodParseName(leave)));

                DecreasepreSpace(ref preSpace);
            }

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 为分析表中的元素配置分析函数");
        }
        /// <summary>
        /// private void FillMapCells()
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodFillMapCells(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            //private void FillMapCells()
            builder.AppendLine(GetSpaces(preSpace) + "#region FillMapCells()");
            builder.AppendLine(GetSpaces(preSpace));

            builder.AppendLine(GetSpaces(preSpace) + "private void FillMapCells()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            var nonTerminals = input.NonTerminalList;
            //nonTerminals.Remove(ProductionNode.epsilonLeave);
            //nonTerminals.Remove(ProductionNode.startEndLeave);
            var terminals = input.TerminalList;
            //terminals.Remove(ProductionNode.epsilonLeave);
            terminals.Add(ProductionNode.startEndLeave);
            var parserMap = input.LL1ParserMapInput;
            foreach (var left in nonTerminals)
            {
                foreach (var next in terminals)
                {
                    var derivationList = parserMap.GetDerivationList(left, next);
                    if (derivationList.Count > 0)
                    {
                        if(derivationList.Count>1)
                        { Console.WriteLine("asf"); }
                        //foreach (var derivation in derivationList)
                        {
                            builder.AppendLine(GetSpaces(preSpace) +
                                string.Format("{0}.SetCell({1}.{2}, {3}.{4}, {5});"
                                    , GetFieldm_Map()
                                    , GetEnumVTypeSG()
                                    , GetEnumVTypeSGItem(left)
                                    , GetEnumTokenTypeSG()
                                    , GetEnumTokenTypeSGItem(next)
                                    , GetFieldFuncParseName(left, next)));
                        }
                    }
                }
            }
            foreach (var terminal in terminals)
            {
                builder.AppendLine(GetSpaces(preSpace));
                foreach (var terminal2 in terminals)
                {
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("{0}.SetCell({1}.{2}, {3}.{4}, {5});"
                            , GetFieldm_Map()
                            , GetEnumVTypeSG()
                            , GetEnumVTypeSGItem(terminal)
                            , GetEnumTokenTypeSG()
                            , GetEnumTokenTypeSGItem(terminal2)
                            , GetFieldFuncParseName(terminal)));
                }
            }
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion FillMapCells()");
        }
        /// <summary>
        /// 所有推导式的推导动作函数
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodDerivation(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region 所有推导式的推导动作函数");
            builder.AppendLine(GetSpaces(preSpace));

            var parserMap = input.LL1ParserMapInput;
            var lineCount = parserMap.GetLineCount();
            var columnCount = parserMap.GetColumnCount();
            var derivationList = new List<Derivation>();
            foreach (var production in this.ProductionCollection)
            {
                var left = production.Left;
                foreach (var candidate in production.RightCollection)
                {
                    var derivation = new Derivation(left, candidate);
                    derivationList.Add(derivation);
                }
            }
            derivationList = derivationList.Distinct().ToList();
            foreach (var derivation in derivationList)
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// {0}", derivation.ToString().ToHtml()));
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\">需要扩展的结点</param>");
                builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"parser\">使用的分析器对象</param>");
                builder.AppendLine(GetSpaces(preSpace) + "/// <returns>下一个要扩展的结点</returns>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("private static SyntaxTree<{0}, {1}, {2}>"
                        , GetEnumTokenTypeSG()
                        , GetEnumVTypeSG()
                        , GetTreeNodeValueSG()));
                builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                    string.Format("{0}(", GetMethodDerivationName(derivation)));
                builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                    string.Format("SyntaxTree<{0}, {1}, {2}> result,"
                        , GetEnumTokenTypeSG()
                        , GetEnumVTypeSG()
                        , GetTreeNodeValueSG()));
                builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                    string.Format("ISyntaxParser<{0}, {1}, {2}> parser)"
                        , GetEnumTokenTypeSG()
                        , GetEnumVTypeSG()
                        , GetTreeNodeValueSG()));
                builder.Append(GetSpaces(preSpace) + "{");
                builder.AppendLine(string.Format("//{0}", derivation.ToString()));
                IncreasepreSpace(ref preSpace);
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("var parser{0} = parser as {1};"
                    , this.GrammarName
                    , GetLL1SyntaxParserName()));
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("result.NodeValue.NodeType = {0}.{1};"
                        , GetEnumVTypeSG()
                        , GetEnumVTypeSGItem(derivation.Left)));
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("result.NodeValue.NodeName = {0}.{1}.ToString();"
                        , GetEnumVTypeSG()
                        , GetEnumVTypeSGItem(derivation.Left)));
                builder.AppendLine(GetSpaces(preSpace) +
                    "//result.NodeValue.Position = EnumProductionNodePosition.NonLeave;");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("result.{1} = parser{0}.m_TokenListSource;"
                        , this.GrammarName
                        ,GetPropertyMappedTotalTokenList()));
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("result.{2} = parser{0}.{1};"
                        , this.GrammarName
                        , GetFieldm_ptNextToken()
                        , GetPropertyMappedTokenStartIndex()));
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("parser{0}.{1}.Pop();"
                        , this.GrammarName
                        , GetFieldm_ParserStack()));
                builder.AppendLine(GetSpaces(preSpace) + "// right-to-left push");
                var candidate=derivation.Right;
                for (int i = candidate.Count - 1; i >= 0; i--)
                {
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("parser{0}.{1}.Push({2});"
                            , this.GrammarName
                            , GetFieldm_ParserStack()
                            , GetFieldStackItem(candidate[i])));
                }
                builder.AppendLine(GetSpaces(preSpace) + "// generate syntax tree");
                ProductionNode nextResult = null;
                int index = 0;
                for (int i = 0; i < candidate.Count; i++)
                {
                    ProductionNode currNode = candidate[i];
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("var {3}Tree{4} = new SyntaxTree<{0}, {1}, {2}>();"
                            , GetEnumTokenTypeSG()
                            , GetEnumVTypeSG()
                            , GetTreeNodeValueSG()
                            , GetEnumVTypeSGItem(currNode)
                            , i));
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("{0}Tree{2}.{3} = parser{1}.{4};"
                            , GetEnumVTypeSGItem(currNode)
                            , this.GrammarName
                            , i
                            , GetPropertyMappedTotalTokenList()
                            , GetFieldm_TokenListSource()));
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("{0}Tree{2}.{4} = parser{1}.{3};"
                            , GetEnumVTypeSGItem(currNode)
                            , this.GrammarName
                            , i
                            , GetFieldm_ptNextToken()
                            , GetPropertyMappedTokenStartIndex()));
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("{0}Tree{1}.Parent = result;"
                            , GetEnumVTypeSGItem(currNode)
                            , i));
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("//{0}Tree{2}.Value = new ProductionNode({1}.{0});"
                            ,GetEnumVTypeSGItem(currNode)
                            , GetEnumVTypeSG()
                            , i));
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("result.Children.Add({0}Tree{1});"
                            ,GetEnumVTypeSGItem(currNode)
                            , i));
                    if (nextResult == null && currNode.Position == EnumProductionNodePosition.NonLeave)
                    {
                        nextResult = currNode; index = i;
                    }
                }
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("return {0}Tree{1};"
                    , GetEnumVTypeSGItem(candidate[0])
                    , 0
                    , this.GrammarName));

                //if (nextResult == null)
                //{
                //    nextResult = candidate[0];
                //}
                //builder.AppendLine(GetSpaces(preSpace) +
                //    string.Format("return Next({0}Tree{1}, parser{2});"
                //    , GetEnumVTypeSGItem(nextResult)
                //    , index
                //    , this.GrammarName));

                DecreasepreSpace(ref preSpace);
                builder.Append(GetSpaces(preSpace) + "}");
                builder.AppendLine(string.Format("//{0}", derivation.ToString()));
            }

            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 所有推导式的推导动作函数");
        }
        /// <summary>
        /// 分析表中的元素指向的分析函数
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodParseDerivation(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region 分析表中的元素指向的分析函数");
            builder.AppendLine(GetSpaces(preSpace));

            var terminalList = input.TerminalList;
            //terminalList.Remove(ProductionNode.epsilonLeave);
            terminalList.Add(ProductionNode.startEndLeave);
            var nonTerminalList = input.NonTerminalList;
            var parserMap = input.LL1ParserMapInput;
            foreach (var left in nonTerminalList)
            {
                foreach (var next in terminalList)
                {
                    var derivationList = parserMap.GetDerivationList(left, next);
                    if (derivationList.Count > 0)
                    {
                        builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("/// 对 {0} ::= {1}... 进行分析", left.ToString().ToHtml(), next.ToString().ToHtml()));
                        foreach (var derivation in derivationList)
                        {
                            builder.AppendLine(GetSpaces(preSpace) +
                                string.Format("/// <para>{0}</para>", derivation.ToString().ToHtml()));
                        }
                        builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
                        builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"parser\"></param>");
                        builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                        builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("private static SyntaxTree<{0}, {1}, {2}>{3}{4}{5}(SyntaxTree<{0}, {1}, {2}> result, ISyntaxParser<{0}, {1}, {2}> parser)"
                            , GetEnumTokenTypeSG()
                            , GetEnumVTypeSG()
                            , GetTreeNodeValueSG()
                            , Environment.NewLine
                            , GetSpaces(preSpace + m_preSpaceStep)
                            , GetMethodParseName(left, next)));
                        builder.AppendLine(GetSpaces(preSpace) + "{");
                        IncreasepreSpace(ref preSpace);
                        foreach (var derivation in derivationList)
                        {
                            builder.AppendLine(GetSpaces(preSpace) +
                                string.Format("// {0}", derivation.ToString()));
                            builder.AppendLine(GetSpaces(preSpace) +
                                string.Format("return {0}(result, parser);"
                                , GetMethodDerivationName(derivation)));
                        }
                        DecreasepreSpace(ref preSpace);
                        builder.AppendLine(GetSpaces(preSpace) + "}");
                    }
                }
            }
            builder.AppendLine(GetSpaces(preSpace));
            //terminalList.Remove(ProductionNode.startEndLeave);
            foreach (var leave in terminalList)
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// 对 叶结点{0} 进行分析", leave.ToString().ToHtml()));
                builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"result\"></param>");
                builder.AppendLine(GetSpaces(preSpace) + "/// <param name=\"parser\"></param>");
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("private static SyntaxTree<{0}, {1}, {2}>{3}{4}{5}(SyntaxTree<{0}, {1}, {2}> result, ISyntaxParser<{0}, {1}, {2}> parser)"
                    , GetEnumTokenTypeSG()
                    , GetEnumVTypeSG()
                    , GetTreeNodeValueSG()
                    , Environment.NewLine
                    , GetSpaces(preSpace + m_preSpaceStep)
                    , GetMethodParseName(leave)));
                builder.AppendLine(GetSpaces(preSpace) + "{");

                IncreasepreSpace(ref preSpace);
                if (leave == ProductionNode.startEndLeave)
                {
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("var parser{0} = parser as {1};"
                        , this.GrammarName
                        , GetLL1SyntaxParserName()));
                    builder.AppendLine(GetSpaces(preSpace) + "if (result != null)");
                    builder.AppendLine(GetSpaces(preSpace) + "{");
                    IncreasepreSpace(ref preSpace);
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.NodeValue.NodeType = {0}.{1};"
                         , GetEnumVTypeSG()
                         , GetEnumVTypeSGItem(leave)));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.NodeValue.NodeName = parser{0}.m_TokenListSource[parser{0}.m_ptNextToken].Detail;"
                         , this.GrammarName));
                    //builder.AppendLine(GetSpaces(preSpace) +
                    //     string.Format("//result.NodeValue.Position = EnumProductionNodePosition.Leave;"));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.MappedTotalTokenList = parser{0}.m_TokenListSource;", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.MappedTokenStartIndex = parser{0}.m_ptNextToken;", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) + "result.MappedTokenLength = 1;");
                    DecreasepreSpace(ref preSpace);
                    builder.AppendLine(GetSpaces(preSpace) + "}");
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("parser{0}.m_ParserStack.Pop();", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("parser{0}.m_ptNextToken++;", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("return Next(result, parser{0});", this.GrammarName));
                }
                else if (leave == ProductionNode.tail_null)
                {
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("var parser{0} = parser as {1};"
                        , this.GrammarName
                        , GetLL1SyntaxParserName()));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.NodeValue.NodeType = {0}.{1};"
                         , GetEnumVTypeSG()
                         , GetEnumVTypeSGItem(leave)));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.NodeValue.NodeName = {0};"
                    //parser{0}.m_TokenListSource[parser{0}.m_ptNextToken].Detail;"
                         , "@\"" + leave.NodeName + "\""));
                    //builder.AppendLine(GetSpaces(preSpace) +
                    //     string.Format("//result.NodeValue.Position = EnumProductionNodePosition.Leave;"));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.MappedTotalTokenList = parser{0}.m_TokenListSource;", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.MappedTokenStartIndex = parser{0}.m_ptNextToken;", this.GrammarName));
                    var notEpsilon = leave != ProductionNode.tail_null;
                    if (notEpsilon)
                    {
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("parser{0}.m_ptNextToken++;", this.GrammarName));
                    }
                    builder.AppendLine(GetSpaces(preSpace) + "result.MappedTokenLength = 0;");
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("parser{0}.m_ParserStack.Pop();", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("return Next(result, parser{0});", this.GrammarName));
                }
                else
                {
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("var parser{0} = parser as {1};"
                        , this.GrammarName
                        , GetLL1SyntaxParserName()));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.NodeValue.NodeType = {0}.{1};"
                         , GetEnumVTypeSG()
                         , GetEnumVTypeSGItem(leave)));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.NodeValue.NodeName = parser{0}.m_TokenListSource[parser{0}.m_ptNextToken].Detail;"
                         , this.GrammarName));
                    //builder.AppendLine(GetSpaces(preSpace) +
                    //     string.Format("//result.NodeValue.Position = EnumProductionNodePosition.Leave;"));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.MappedTotalTokenList = parser{0}.m_TokenListSource;", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("result.MappedTokenStartIndex = parser{0}.m_ptNextToken;", this.GrammarName));
                    var notEpsilon = leave != ProductionNode.tail_null;
                    if (notEpsilon)
                    {
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("parser{0}.m_ptNextToken++;", this.GrammarName));
                    }
                    builder.AppendLine(GetSpaces(preSpace) + "result.MappedTokenLength = 1;");
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("parser{0}.m_ParserStack.Pop();", this.GrammarName));
                    builder.AppendLine(GetSpaces(preSpace) +
                         string.Format("return Next(result, parser{0});", this.GrammarName));
                }

                DecreasepreSpace(ref preSpace);

                builder.AppendLine(GetSpaces(preSpace) + "}");
            }
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 分析表中的元素指向的分析函数");
        }
        /// <summary>
        /// 获取分析表中的元素
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserMethodGetFuncParseDerivation(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {// public ... Get...() { return ...; }
            builder.AppendLine(GetSpaces(preSpace) + "#region 获取分析表中的元素");
            builder.AppendLine(GetSpaces(preSpace));

            var terminalList = input.TerminalList;
            //terminalList.Remove(ProductionNode.epsilonLeave);
            terminalList.Add(ProductionNode.startEndLeave);
            var nonTerminalList = input.NonTerminalList;
            var parserMap = input.LL1ParserMapInput;
            foreach (var left in nonTerminalList)
            {
                foreach (var next in terminalList)
                {
                    var derivationList = parserMap.GetDerivationList(left, next);
                    if (derivationList.Count > 0)
                    {
                        builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("/// 对 {0} ::= {1}... 进行分析", left.ToString().ToHtml(), next.ToString().ToHtml()));
                        foreach (var derivation in derivationList)
                        {
                            builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("/// <para>{0}</para>", derivation.ToString().ToHtml()));
                        }
                        builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("public static CandidateFunction<{0}, {1}, {2}>"
                            , GetEnumTokenTypeSG()
                            , GetEnumVTypeSG()
                            , GetTreeNodeValueSG()));
                        builder.AppendLine(string.Format("{0}Get{1}()"
                            , GetSpaces(preSpace+m_preSpaceStep)
                            , GetFieldFuncParseName(left, next)));
                        builder.AppendLine(GetSpaces(preSpace) + "{");
                        builder.AppendLine(string.Format("{0}return {1};"
                            , GetSpaces(preSpace + m_preSpaceStep)
                            , GetFieldFuncParseName(left, next)));
                        builder.AppendLine(GetSpaces(preSpace) + "}");
                    }
                }
            }
            builder.AppendLine(GetSpaces(preSpace));
            //terminalList.Remove(ProductionNode.startEndLeave);
            foreach (var leave in terminalList)
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// 对 叶结点{0} 进行分析", leave.ToString().ToHtml()));
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("public static CandidateFunction<{0}, {1}, {2}>"
                    , GetEnumTokenTypeSG()
                    , GetEnumVTypeSG()
                    , GetTreeNodeValueSG()));
                builder.AppendLine(string.Format("{0}Get{1}()"
                    , GetSpaces(preSpace + m_preSpaceStep)
                    , GetFieldFuncParseName(leave)));
                builder.AppendLine(GetSpaces(preSpace) + "{");
                builder.AppendLine(string.Format("{0}return {1};"
                    , GetSpaces(preSpace + m_preSpaceStep)
                    , GetFieldFuncParseName(leave)));
                builder.AppendLine(GetSpaces(preSpace) + "}");
            }
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 获取分析表中的元素");
        }
        /// <summary>
        /// 用于分析栈操作的字段-非终结点
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserFieldm_NonTerminal(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {//private static readonly EnumVTypeCG m_VList = EnumVTypeCG.VList;
            builder.AppendLine(GetSpaces(preSpace) + "#region 用于分析栈操作的字段-非终结点");
            builder.AppendLine(GetSpaces(preSpace));
            var nonTterminalList = input.NonTerminalList;
            foreach (var node in nonTterminalList)
            {
                builder.AppendLine(string.Format("{0}private static readonly {1} {2} = {1}.{3};"
                    , GetSpaces(preSpace)
                    , GetEnumVTypeSG()
                    , GetFieldStackItem(node)
                    , GetEnumVTypeSGItem(node)));
            }
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 用于分析栈操作的字段-非终结点");
        }
        /// <summary>
        /// 用于分析栈操作的字段-终结点
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserFieldm_Terminal(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {//private static readonly EnumVTypeCG m_constStringLeave = EnumVTypeCG.constStringLeave;
            builder.AppendLine(GetSpaces(preSpace) + "#region 用于分析栈操作的字段-终结点");
            builder.AppendLine(GetSpaces(preSpace));
            var terminalList = input.TerminalList;
            //terminalList.Remove(ProductionNode.epsilonLeave);
            terminalList.Add(ProductionNode.startEndLeave);
            foreach (var node in terminalList)
            {
                builder.AppendLine(string.Format("{0}private static readonly {1} {2} = {1}.{3};"
                    , GetSpaces(preSpace)
                    , GetEnumVTypeSG()
                    , GetFieldStackItem(node)
                    , GetEnumVTypeSGItem(node)));
            }
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 用于分析栈操作的字段-终结点");
        }
        /// <summary>
        /// 分析表中的元素
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateLL1SyntaxParserFieldFuncParseDerivation(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "#region 分析表中的元素");
            builder.AppendLine(GetSpaces(preSpace));

            var terminalList = input.TerminalList;
            //terminalList.Remove(ProductionNode.epsilonLeave);
            terminalList.Add(ProductionNode.startEndLeave);
            var parserMap = input.LL1ParserMapInput;
            var nonTerminalList = input.NonTerminalList;
            foreach (var left in nonTerminalList)
            {
                foreach (var next in terminalList)
                {
                    var derivationList = parserMap.GetDerivationList(left, next);
                    if (derivationList.Count > 0)
                    {
                        builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("/// 对 {0} ::= {1}... 进行分析", left.ToString().ToHtml(), next.ToString().ToHtml()));
                        foreach (var derivation in derivationList)
                        {
                            builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("/// <para>{0}</para>", derivation.ToString().ToHtml()));
                        }
                        builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                        builder.AppendLine(GetSpaces(preSpace) +
                            string.Format("private static CandidateFunction<{0}, {1}, {2}>{3}{4}{5};"
                            , GetEnumTokenTypeSG()
                            , GetEnumVTypeSG()
                            , GetTreeNodeValueSG()
                            , Environment.NewLine
                            , GetSpaces(preSpace + m_preSpaceStep)
                            , GetFieldFuncParseName(left, next)));
                    }
                }
            }
            builder.AppendLine(GetSpaces(preSpace));
            foreach (var leave in terminalList)
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// 对 叶结点{0} 进行分析", leave.ToString().ToHtml()));
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("private static CandidateFunction<{0}, {1}, {2}>{3}{4}{5};"
                    , GetEnumTokenTypeSG()
                    , GetEnumVTypeSG()
                    , GetTreeNodeValueSG()
                    , Environment.NewLine
                    , GetSpaces(preSpace + m_preSpaceStep)
                    , GetFieldFuncParseName(leave)));
            }
            builder.AppendLine(GetSpaces(preSpace));
            builder.AppendLine(GetSpaces(preSpace) + "#endregion 分析表中的元素");
        }

    }
}
