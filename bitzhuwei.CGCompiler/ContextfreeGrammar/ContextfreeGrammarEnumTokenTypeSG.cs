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
        #region 生成整个单词枚举类型文件的源代码
        /// <summary>
        /// 生成整个<code>EnumTokenTypeSG</code>枚举类型文件的源代码
        /// </summary>
        /// <returns></returns>
        public string GenerateEnumTokenType()
        {
            int preSpace = 0;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateEnumTokenType(ref preSpace, input);
        }
        /// <summary>
        /// 生成整个<code>EnumTokenTypeSG</code>枚举类型文件的源代码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateEnumTokenType(LL1GeneraterInput input)
        {
            int preSpace = 0;

            return GenerateEnumTokenType(ref preSpace, input);
        }


        /// <summary>
        /// 生成整个<code>EnumTokenTypeSG</code>枚举类型文件的源代码
        /// </summary>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateEnumTokenType(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("namespace {0}", this.Namespace));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GenerateEnumTokenTypeClass(ref preSpace, input));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace));
            return builder.ToString();
        }
        #endregion 生成整个单词枚举类型文件的源代码

        #region 生成单词枚举类型的源代码

        /// <summary>
        /// 生成<code>EnumTokenTypeSG</code>的源码
        /// </summary>
        /// <returns></returns>
        public string GenerateEnumTokenTypeClass()
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;
            LL1GeneraterInput input = new LL1GeneraterInput(this);
            return GenerateEnumTokenTypeClass(ref preSpace, input);
        }
        /// <summary>
        /// 生成<code>EnumTokenTypeSG</code>的源码
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public string GenerateEnumTokenTypeClass(LL1GeneraterInput input)
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;

            return GenerateEnumTokenTypeClass(ref preSpace, input);
        }

        /// <summary>
        /// 生成<code>EnumTokenTypeSG</code>的源码
        /// </summary>
        /// <param name="this.GrammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GenerateEnumTokenTypeClass(ref int preSpace, LL1GeneraterInput input)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("/// 文法{0}的单词枚举类型", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("public enum {0}"
                , GetEnumTokenTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            GenerateEnumTokenTypeItemUnknown(builder, ref preSpace, input);
            //GenerateEnumTokenTypeitem_null_identifier_number(builder, ref preSpace, input);
            GenerateEnumTokenTypeWithoutItemUnknown(builder, ref preSpace, input);

            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            return builder.ToString();
        }
        #endregion 生成单词枚举类型的源代码

        /// <summary>
        /// 获取<code>EnumTokenTypeSG</code>除默认项以外的选项
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateEnumTokenTypeWithoutItemUnknown(
            StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            var terminalList = input.TerminalList;
            //var map = GetTokenMap(input);
            //if (map.Contains(EnumCharTypeCG.Letter))
            //{
            //    builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            //    builder.AppendLine(GetSpaces(preSpace) + "/// 标识符");
            //    builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            //    builder.AppendLine(GetSpaces(preSpace) + "Identifier,");
            //}
            //if (map.Contains(EnumCharTypeCG.Number))
            //{
            //    builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            //    builder.AppendLine(GetSpaces(preSpace) + "/// 常数");
            //    builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            //    builder.AppendLine(GetSpaces(preSpace) + "Number,");
            //}
            //terminalList.Remove(ProductionNode.epsilonLeave);
            terminalList.Add(ProductionNode.startEndLeave);
            var needIdentifier = false;
            var identifierExists = false;
            foreach (var terminal in terminalList)
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// {0}", terminal.NodeNote.ToHtml()));
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("{0},"
                    , GetEnumTokenTypeSGItem(terminal)));
                if (terminal.NodeName == "identifier") { identifierExists = true; }
                if (!needIdentifier)
                {
                    var ct = GetCharType(terminal.NodeName[0]);
                    if (ct == EnumCharType.Letter || ct == EnumCharType.UnderLine)
                    {
                        needIdentifier = true;
                    }
                }
            }
            if (needIdentifier &&(!identifierExists))
            {
                builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("/// {0}", "标识符".ToHtml()));
                builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                builder.AppendLine(GetSpaces(preSpace) +
                    string.Format("{0},", "identifier"));
            }
        }

        private bool IsCGKeyword(ProductionNode item)
        {
            if (item == ProductionNode.tail_null
                || item == ProductionNode.tail_identifier
                || item == ProductionNode.tail_number
                || item == ProductionNode.tail_constString)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 获取<code>EnumTokenTypeSG</code>的默认项
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        /// <param name="input"></param>
        private void GenerateEnumTokenTypeItemUnknown(
            StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("/// {0}", GetEnumTokenTypeSGItemDefaultNote()));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("{0},"
                , GetEnumTokenTypeSGItemDefaultName()));
        }
        private void GenerateEnumTokenTypeitem_null_identifier_number(StringBuilder builder, ref int preSpace, LL1GeneraterInput input)
        {
            foreach (var item in input.TerminalList)
            {
                if (item == ProductionNode.tail_null || item == ProductionNode.tail_identifier || item == ProductionNode.tail_number)
                {
                    builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("/// {0}", item.NodeNote.ToHtml()));
                    builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
                    builder.AppendLine(GetSpaces(preSpace) +
                        string.Format("{0},", GetEnumVTypeSGItem(item)));
                }
            }
        }
    }
}
