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

        #region 生成整个TreeNodeValue类文件的源码

        /// <summary>
        /// 生成整个<code>TreeNodeValue</code>类文件的源码
        /// </summary>
        /// <returns></returns>
        public string GenerateTreeNodeValueType()
        {
            int preSpace = 0;

            return GenerateTreeNodeValueType( ref preSpace);
        }

        /// <summary>
        /// 获取<code>TreeNodeValue</code>的源码
        /// </summary>
        /// <param name="this.GrammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <returns></returns>
        public string GenerateTreeNodeValueType(ref int preSpace)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("namespace {0}", this.Namespace));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GenerateTreeNodeValueTypeClass(ref preSpace));
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
            builder.AppendLine(GetSpaces(preSpace));
            return builder.ToString();
        }

        #endregion 生成整个TreeNodeValue类文件的源码

        #region 生成TreeNodeValue类的源码
        /// <summary>
        /// 获取<code>TreeNodeValue</code>类的源码
        /// </summary>
        /// <returns></returns>
        public string GenerateTreeNodeValueTypeClass()
        {
            int preSpace = m_preSpaceOfLL1SyntaxParser;
            return GenerateTreeNodeValueTypeClass( ref preSpace);
        }

        /// <summary>
        /// 获取<code>TreeNodeValue</code>类的源码
        /// </summary>
        /// <param name="this.GrammarName">文法名</param>
        /// <param name="preSpace">预留空白长度</param>
        /// <returns></returns>
        public string GenerateTreeNodeValueTypeClass(ref int preSpace)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("/// 文法{0}的语法树结点的值", this.GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + string.Format("public class {0} : System.ICloneable"
                , GetTreeNodeValueSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);

            GenerateTreeNodeValueTypePropertyNodeName(builder, ref preSpace);
            GenerateTreeNodeValueTypePropertyNodeType(builder, ref preSpace);
            GenerateTreeNodeValueTypeMethodToString(builder, ref preSpace);
            GenerateTreeNodeValueTypeICloneableMethod(builder, ref preSpace);
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");

            return builder.ToString();
        }

        private void GenerateTreeNodeValueTypeICloneableMethod(StringBuilder builder, ref int preSpace)
        {
            builder.AppendLine(GetSpaces(preSpace) + "public object Clone()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            IncreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + string.Format("var result = new TreeNodeValue{0}();", GrammarName));
            builder.AppendLine(GetSpaces(preSpace) + "result.NodeName = this.NodeName;");
            builder.AppendLine(GetSpaces(preSpace) + "result.NodeType = this.NodeType;");
            builder.AppendLine(GetSpaces(preSpace) + "return result;");
            DecreasepreSpace(ref preSpace);
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }

        #endregion 生成TreeNodeValue类的源码

        /// <summary>
        /// 获取<code>TreeNodeValue</code>的<code>ToString()</code>方法
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        private void GenerateTreeNodeValueTypeMethodToString(
            StringBuilder builder, ref int preSpace)
        {
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// \"名称, 类型\"");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// <returns></returns>");
            builder.AppendLine(GetSpaces(preSpace) + "public override string ToString()");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                string.Format("bool leave = this.NodeType.ToString().EndsWith(\"Leave\");"));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                string.Format("if (NodeName != NodeType.ToString())"));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                string.Format("{ return string.Format(\"{0}\", leave ? \"\" : \"[\", NodeName, NodeType, leave ? \"\" : \"]\"); }",
                    "{0}{1} <= {2}{3}"));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                string.Format("else"));
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) +
                string.Format("{ return string.Format(\"{0}\", leave ? \"\" : \"[\", NodeName, leave ? \"\" : \"]\"); }",
                    "{0}{1}{2}"));
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }
        /// <summary>
        /// 获取<code>TreeNodeValue</code>的<code>NodeType</code>属性
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        private void GenerateTreeNodeValueTypePropertyNodeType(
            StringBuilder builder, ref int preSpace)
        {
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("private {0} m_NodeType = {0}.{1};"
                , GetEnumVTypeSG()
                , GetEnumVTypeSGItemDefaultName()));
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 结点类型");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) +
                string.Format("public {0} NodeType"
                , GetEnumVTypeSG()));
            builder.AppendLine(GetSpaces(preSpace) + "{");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "get { return m_NodeType; }");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "set { m_NodeType = value; }");
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }
        /// <summary>
        /// 获取<code>TreeNodeValue</code>的<code>NodeName</code>属性
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="this.GrammarName"></param>
        /// <param name="preSpace"></param>
        private void GenerateTreeNodeValueTypePropertyNodeName(
            StringBuilder builder, ref int preSpace)
        {
            builder.AppendLine(GetSpaces(preSpace) + "private string m_NodeName = string.Empty;");
            builder.AppendLine(GetSpaces(preSpace) + "/// <summary>");
            builder.AppendLine(GetSpaces(preSpace) + "/// 结点名称");
            builder.AppendLine(GetSpaces(preSpace) + "/// </summary>");
            builder.AppendLine(GetSpaces(preSpace) + "public string NodeName");
            builder.AppendLine(GetSpaces(preSpace) + "{");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "get { return m_NodeName; }");
            builder.AppendLine(GetSpaces(preSpace + m_preSpaceStep) + "set { m_NodeName = value; }");
            builder.AppendLine(GetSpaces(preSpace) + "}");
        }
        
    }
}
