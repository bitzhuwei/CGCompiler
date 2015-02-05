using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler
{

    internal static class StringExtension
    {
        /// <summary>
        /// 转换为Html格式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToHtml(this string source)
        {
            if (source == null) return null;
            StringBuilder builder = new StringBuilder(source);
            builder.Replace("&", "&amp;");
            builder.Replace("<", "&lt;");
            builder.Replace(">", "&gt;");
            builder.Replace("\"", "&quot;");
            builder.Replace("©", "&copy;");
            builder.Replace("®", "&reg;");
            builder.Replace("×", "&times;");
            builder.Replace("÷", "&divide;");
            return builder.ToString();
        }
    }
    /// <summary>
    /// 2型文法（上下文无关文法）
    /// </summary>
    public partial class ContextfreeGrammar : ICloneable
    {
        /// <summary>
        /// 获取字段<code>m_CurrentColumn</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetFieldm_CurrentColumn()
        {
            return "m_CurrentColumn";
        }
        /// <summary>
        /// 获取字段<code>m_CurrentLine</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetFieldm_CurrentLine()
        {
            return "m_CurrentLine";
        }
        /// <summary>
        /// 获取属性<code>PtNextLetter</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetPropertyPtNextLetter()
        {
            return "PtNextLetter";
        }
        /// <summary>
        /// 获取词法分析器类的名称
        /// </summary>
        /// <param name="grammarName"></param>
        /// <returns></returns>
        public string GetLexicalAnalyzerName()
        {
            return string.Format("LexicalAnalyzer{0}", this.GrammarName);
        }
        /// <summary>
        /// 获取词法分析器基类名称
        /// </summary>
        /// <returns></returns>
        public string GetLexicalAnalyzerBaseName()
        {
            if (string.IsNullOrEmpty(m_LexicalAnalyzerBase))
            {
                var t = typeof(bitzhuwei.CompilerBase.LexicalAnalyzerBase<>);
                m_LexicalAnalyzerBase = t.Name.Split('`')[0];
            }
            return m_LexicalAnalyzerBase;
        }
        static string m_LexicalAnalyzerBase = string.Empty;

        /// <summary>
        /// 获取词法分析器基类名称
        /// </summary>
        /// <returns></returns>
        public string GetLL1SyntaxParserBaseName()
        {
            if (string.IsNullOrEmpty(m_LL1SyntaxParserBase))
            {
                var t = typeof(bitzhuwei.CompilerBase.LL1SyntaxParserBase<,,>);
                m_LL1SyntaxParserBase = t.Name.Split('`')[0];
            }
            return m_LL1SyntaxParserBase;
        }
        static string m_LL1SyntaxParserBase = string.Empty;

        /// <summary>
        /// 获取<code>EnumVTypeSG</code>的默认项的名称
        /// </summary>
        /// <returns></returns>
        public string GetEnumVTypeSGItemDefaultName()
        {
            return "Unknown";
        }
        /// <summary>
        /// 获取<code>EnumTokenTypeSG</code>的默认项的注释
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetEnumVTypeSGItemDefaultNote()
        {
            return "未知的语法结点符号";
        }

        /// <summary>
        /// 获取<code>EnumTokenTypeSG</code>的默认项的名称
        /// </summary>
        /// <returns></returns>
        public string GetEnumTokenTypeSGItemDefaultName()
        {
            return "unknown";
        }
 
        /// <summary>
        /// 获取<code>EnumTokenTypeSG</code>的默认项的注释
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetEnumTokenTypeSGItemDefaultNote()
        {
            return "未知的单词符号";
        }
        /// <summary>
        /// 获取LL语法分析器类的名称
        /// </summary>
        /// <param name="grammarName"></param>
        /// <returns></returns>
        public string GetLL1SyntaxParserName()
        {
            return string.Format("LL1SyntaxParser{0}", this.GrammarName);
        }
        /// <summary>
        /// 获取<code>EnumTokenTypeSG</code>的一项的名称
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string GetEnumTokenTypeSGItem(ProductionNode node)
        {
            var result = string.Format("{0}{1}", IsCGKeyword(node) ? "" : GetKeywordPrefix4Token(), ConvertToIdentifier(node));
            //if (result.EndsWith("Leave"))
            //    result = result.Substring(0, result.Length - 5);
            //if ('a' <= result[0] && result[0] <= 'z')
            //    result = ((char)('A' - 'a' + result[0])) + result.Substring(1);
            return result;
        }
        public string GetKeywordPrefix4Token()
        {
            return "token_";
        }
        public string GetKeywordPrefix4SyntaxTreeNodeLeave(EnumProductionNodePosition position)
        {
            if (position == EnumProductionNodePosition.Leave)
            {
                return "tail_";
            }
            else if (position == EnumProductionNodePosition.NonLeave)
            {
                return "case_";
            }
            else
                return position.ToString();
        }
        /// <summary>
        /// 获取字段<code>m_Map</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetFieldm_Map()
        {
            return "m_Map";
        }

        /// <summary>
        /// 获取字段<code>m_ptNextToken</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetFieldm_ptNextToken()
        {
            return "m_ptNextToken";
        }

        /// <summary>
        /// 获取属性<code>MappedTokenStartIndex</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetPropertyMappedTokenStartIndex()
        {
            return "MappedTokenStartIndex";
        }

        /// <summary>
        /// 获取属性<code>MappedTotalTokenList</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetPropertyMappedTotalTokenList()
        {
            return "MappedTotalTokenList";
        }

        /// <summary>
        /// 获取字段<code>m_TokenListSource</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetFieldm_TokenListSource()
        {
            return "m_TokenListSource";
        }
        /// <summary>
        /// 获取字段<code>m_ParserStack</code>的名称
        /// </summary>
        /// <returns></returns>
        public string GetFieldm_ParserStack()
        {
            return "m_ParserStack";
        }
        /// <summary>
        /// 减少一个单位的缩进
        /// </summary>
        /// <param name="preSpace"></param>
        public void DecreasepreSpace(ref int preSpace)
        {
            preSpace -= m_preSpaceStep;
        }
        /// <summary>
        /// 增加一个单位的缩进
        /// </summary>
        /// <param name="preSpace"></param>
        public void IncreasepreSpace(ref int preSpace)
        {
            preSpace += m_preSpaceStep;
        }
        /// <summary>
        /// 获取给定推导式对应的函数名称
        /// <para>DerivationVn___VList</para>
        /// </summary>
        /// <param name="derivation">推导式</param>
        /// <returns></returns>
        public string GetMethodDerivationName(Derivation derivation)
        {
            //return string.Format("Derivation{0}___{1}",
            //    ConvertToIdentifier(derivation.Left), ConvertToIdentifier(derivation.Right.ToString()));
            return string.Format("Derivation{0}___{1}",
                GetEnumVTypeSGItem(derivation.Left), GetEnumVTypeSGItem(derivation.Right));
        }

        public object GetEnumVTypeSGItem(ProductionNodeList productionNodeList)
        {
            var result = new StringBuilder();
            foreach (var item in productionNodeList)
            {
                result.Append(GetEnumVTypeSGItem(item));
            }
            return result.ToString();
        }
        /// <summary>
        /// Parse<code>Vn</code>___<code>next</code>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public string GetMethodParseName(ProductionNode left, ProductionNode next)
        {
            //return string.Format("Parse{0}___{1}",
            //    ConvertToIdentifier(left), ConvertToIdentifier(next)); 
            return string.Format("Parse{0}___{1}",
                GetEnumVTypeSGItem(left), GetEnumVTypeSGItem(next));
        }
        /// <summary>
        /// Parse<code>leave</code>_
        /// </summary>
        /// <param name="leave">叶结点</param>
        /// <returns></returns>
        public string GetMethodParseName(ProductionNode leave)
        {
            //return string.Format("Parse{0}_", ConvertToIdentifier(leave));
            return string.Format("Parse{0}_", GetEnumVTypeSGItem(leave));
        }
        /// <summary>
        /// 获取<code>EnumVType</code>的一项的名称
        /// <para>constStringLeave</para>
        /// <para>VList</para>
        /// </summary>
        /// <param name="node">此项对应的结点</param>
        /// <returns></returns>
        public string GetEnumVTypeSGItem(ProductionNode node)
        {
            var result = string.Format("{0}", ConvertToIdentifier(node));
            if (node.Position == EnumProductionNodePosition.Leave)
            {
                if ('A' <= result[0] && result[0] <= 'Z')
                    result = ((char)('a' - 'A' + result[0])) + result.Substring(1);
                if (!result.EndsWith("Leave"))
                    result += "Leave";
                result = (IsCGKeyword(node) ? "" : GetKeywordPrefix4SyntaxTreeNodeLeave(node.Position)) + result;
            }
            else if (node.Position == EnumProductionNodePosition.NonLeave)
            {
                if (result.EndsWith("Leave"))
                    result = result.Substring(0, result.Length - 5);
                result = (IsCGKeyword(node) ? "" : GetKeywordPrefix4SyntaxTreeNodeLeave(node.Position)) + result;
            }
            return result;
        }
        /// <summary>
        /// 获取用于堆栈操作的一个字段
        /// <para>m_constStringLeave</para>
        /// <para>m_VList</para>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string GetFieldStackItem(ProductionNode node)
        {
            var result = string.Format("m_{0}", GetEnumVTypeSGItem(node));
            return result;
        }

        /// <summary>
        /// FuncParse<code>Vn</code>___<code>next</code>
        /// </summary>
        /// <param name="left"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public string GetFieldFuncParseName(ProductionNode left, ProductionNode next)
        {
            //return string.Format("FuncParse{0}___{1}",
            //    ConvertToIdentifier(left), ConvertToIdentifier(next));
            return string.Format("FuncParse{0}___{1}",
                GetEnumVTypeSGItem(left), GetEnumVTypeSGItem(next));
        }
        /// <summary>
        /// 对 叶结点<code>leave</code>进行分析
        /// </summary>
        /// <param name="leave">叶结点</param>
        /// <returns></returns>
        public string GetFieldFuncParseName(ProductionNode leave)
        {
            //return string.Format("FuncParse{0}_", ConvertToIdentifier(leave));
            return string.Format("FuncParse{0}_", GetEnumVTypeSGItem(leave));
        }
        /// <summary>
        /// 获取单词枚举类型的类型名
        /// </summary>
        /// <param name="grammarName"></param>
        /// <returns></returns>
        public string GetEnumTokenTypeSG()
        {
            return string.Format("EnumTokenType{0}", this.GrammarName);
        }
        /// <summary>
        /// 获取语法树结点类型的类型名
        /// </summary>
        /// <param name="grammarName"></param>
        /// <returns></returns>
        public string GetEnumVTypeSG()
        {
            return string.Format("EnumVType{0}", this.GrammarName);
        }
        /// <summary>
        /// 获取语法树结点值的类型名
        /// </summary>
        /// <param name="grammarName"></param>
        /// <returns></returns>
        public string GetTreeNodeValueSG()
        {
            return string.Format("TreeNodeValue{0}", this.GrammarName);
        }
        /// <summary>
        /// 把结点信息转换为标识符
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string ConvertToIdentifier(ProductionNode node)
        {
            if (node == null) return "_null";
            if (node == ProductionNode.tail_null) return "epsilon";
            if (node == ProductionNode.startEndLeave) return "startEnd";
            string value = node.NodeName;
            return ConvertToIdentifier(value);
        }
        /// <summary>
        /// 把字符串转换为标识符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertToIdentifier(string value)
        {
            if (string.IsNullOrEmpty(value)) return "_";
            StringBuilder builer = new StringBuilder(value);
            builer.Replace(".", "Dot_");
            builer.Replace(",", "Comma_");
            builer.Replace("+", "Plus_");
            builer.Replace("-", "Minus_");
            builer.Replace("*", "Multiply_");
            builer.Replace("/", "Divide_");
            builer.Replace("%", "Percent_");
            builer.Replace("^", "Xor_");
            builer.Replace("&", "And_");
            builer.Replace("|", "Or_");
            builer.Replace("~", "Reverse_");
            builer.Replace("~", "Reverse_");
            builer.Replace("$", "Dollar_");
            builer.Replace("<", "LessThan_");
            builer.Replace(">", "GreaterThan_");
            builer.Replace("(", "LeftParentheses_");
            builer.Replace(")", "RightParentheses_");
            builer.Replace("[", "LeftBracket_");
            builer.Replace("]", "RightBracket_");
            builer.Replace("{", "LeftBrace_");
            builer.Replace("}", "RightBrace_");
            builer.Replace("!", "Not_");
            builer.Replace("#", "Pound_");
            builer.Replace("\\", "Slash_");
            builer.Replace("?", "Question_");
            builer.Replace("\'", "");
            builer.Replace("\"", "");
            builer.Replace(":", "Colon_");
            builer.Replace(";", "Semicolon_");
            builer.Replace("=", "Equality_");
            builer.Replace(' ', '_');
            builer.Replace('\t', '_');
            builer.Replace('\r', '_');
            builer.Replace('\n', '_');
            var ch = builer[0];
            if (ch != '_'
                && (!
                    (('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z'))
                    )
                )
            {
                builer.Insert(0, '_');
            }
            return builer.ToString();
        }
        /// <summary>
        /// private const ref int m_preSpaceOfLL1SyntaxParser = 4;
        /// </summary>
        public const int m_preSpaceOfLL1SyntaxParser = 4;
        /// <summary>
        /// private const int m_preSpaceStep = 4;
        /// </summary>
        public const int m_preSpaceStep = 4;
        /// <summary>
        /// 获取给定长度的空格串
        /// </summary>
        /// <param name="length">空串长度</param>
        /// <returns></returns>
        public string GetSpaces(int length)
        {
            if (length < 0) return m_spaces[0];
            if (length >= m_spaces.Count)
                lock (m_synObj)
                {
                    if (length >= m_spaces.Count)
                        for (int i = m_spaces.Count; i < length + 1; i++)
                        {
                            m_spaces.Add(m_spaces[m_spaces.Count - 1] + " ");
                        }
                }
            return m_spaces[length];
        }
        public object m_synObj = new object();
        public static List<string> m_spaces = new List<string>()
        {
            "",
            " ",
            "  ",
            "   ",
            "    ",
            "     ",
            "      ",
            "       ",
            "        "
        };
    }
}
