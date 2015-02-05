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

        #region 生成整个语法树结点枚举类型文件的源代码

        /// <summary>
        /// 生成整个语法树结点枚举类型<code>EnumVTypeSG</code>文件的源代码
        /// </summary>
        /// <returns></returns>
        public string GenerateEnumVType()
        {
            int preSpace = 0;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateEnumVType(ref preSpace, input);
        }
        /// <summary>
        /// 生成整个语法树结点枚举类型<code>EnumVTypeSG</code>文件的源代码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateEnumVType(LL1GeneraterInput input)
        {
            int preSpace = 0;

            return GenerateEnumVType(ref preSpace, input);
        }

        /// <summary>
        /// 生成整个语法树结点枚举类型<code>EnumVTypeSG</code>文件的源代码
        /// </summary>
        /// <param name="this.GrammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateEnumVType(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("namespace {0}", this.Namespace));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GenerateEnumVTypeClass(ref preSpace, input));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace));
            return builder.ToString();
        }

        #endregion 生成整个语法树结点枚举类型文件的源代码

        #region 生成语法树结点枚举类型的源代码

        /// <summary>
        /// 生成<code>EnumVTypeSG</code>的源码
        /// </summary>
        /// <returns></returns>
        public string GenerateEnumVTypeClass()
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateEnumVTypeClass(ref preSpace, input);
        }
        /// <summary>
        /// 生成<code>EnumVTypeSG</code>的源码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateEnumVTypeClass(LL1GeneraterInput input)
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;

            return GenerateEnumVTypeClass(ref preSpace, input);
        }
      
        /// <summary>
        /// 生成<code>EnumVTypeSG</code>的源码
        /// </summary>
        /// <param name="this.GrammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateEnumVTypeClass(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("/// 文法{0}的语法树结点枚举类型", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("public enum {0}"
                , GetEnumVTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            GenerateEnumVTypeItemUnknown(builder, ref preSpace, input);
            GenerateEnumVTypeWithoutItemUnknown(builder, ref preSpace, input);

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            return builder.ToString();
        }


        #endregion 生成语法树结点枚举类型的源代码
        /// <summary>
        /// 获取<code>EnumVTypeSG</code>除默认项以外的选项
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateEnumVTypeWithoutItemUnknown(
            StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            var nonTerminalList = input.NonTerminalList;
            foreach (var nonTerminal in nonTerminalList)
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// {0}", this.GetProduction(nonTerminal).ToString(true)));
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("{0},"
                    , GetEnumVTypeSGItem(nonTerminal)));
            }
            var terminalList = input.TerminalList;
            terminalList.Add(ProductionNode.startEndLeave);
            foreach (var terminal in terminalList)
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// {0}", terminal.NodeNote.ToHtml()));
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("{0},"
                    , GetEnumVTypeSGItem(terminal)));
            }
        }
        /// <summary>
        /// 获取<code>EnumVTypeSG</code>的默认项
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateEnumVTypeItemUnknown(
            StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// {0}", GetEnumVTypeSGItemDefaultNote()));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0},"
                , GetEnumVTypeSGItemDefaultName()));
        }


    }
}
