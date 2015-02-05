using System;
using System.Text;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// Description of ProductionNode.
    /// </summary>
    public class ProductionNode : ICloneable
    {
        /// <summary>
        /// ε（空结点）
        /// </summary>
        public static readonly ProductionNode tail_null =
            new ProductionNode("null", EnumProductionNodePosition.Leave);
            //new ProductionNode("null", EnumProductionNodePosition.My);
        public static readonly ProductionNode tail_identifier =
            new ProductionNode("identifier", EnumProductionNodePosition.Leave);
        //public static readonly ProductionNode my_constString =
        //    new ProductionNode("constString", EnumProductionNodePosition.Leave);
            //new ProductionNode("identifier", EnumProductionNodePosition.My);
        public static readonly ProductionNode tail_number =
            new ProductionNode("number", EnumProductionNodePosition.Leave);
            //new ProductionNode("number", EnumProductionNodePosition.My);
        public static readonly ProductionNode tail_constString =
            new ProductionNode("constString", EnumProductionNodePosition.Leave);
        //new ProductionNode("constString", EnumProductionNodePosition.My);
        /// <summary>
        /// 用于语法分析的开始、结束标识
        /// <para>"#"</para>
        /// </summary>
        public static readonly ProductionNode startEndLeave =
            new ProductionNode("#", EnumProductionNodePosition.Leave);
        /// <summary>
        /// 判定两个结点是否相同
        /// </summary>
        /// <param name="obj">被比较的对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as ProductionNode;
            if (another == null) return false;
            if (m_NodeName == another.m_NodeName
                && m_NodeNote == another.m_NodeNote
                && m_Position == another.m_Position
                )
                return true;
            return false;
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator == (ProductionNode a, ProductionNode b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            if ((object)a == null)
            {
                if ((object)b == null) return true;
                else return false;
            }
            else if ((object)b == null) return false;
            return (a.m_NodeName == b.m_NodeName
                && a.m_NodeNote == b.m_NodeNote
                && a.m_Position == b.m_Position
                );
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator !=(ProductionNode a, ProductionNode b)
        {
            return !(a == b);
        }
        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// 结点名，用以区分结点
        /// </summary>
        public string NodeName
        {
            get { return m_NodeName; }
            set
            {
                if (string.IsNullOrEmpty(m_NodeName) || string.IsNullOrEmpty(m_NodeName))
                    return;
                m_NodeName = value;
            }
        }
        /// <summary>
        /// 结点名，用以区分结点
        /// </summary>
        string m_NodeName = "?";
        /// <summary>
        /// 注释
        /// </summary>
        private string m_NodeNote = "";
        /// <summary>
        /// 注释
        /// </summary>
        public string NodeNote
        {
            get { return m_NodeNote; }
            set { m_NodeNote = value; }
        }

        /// <summary>
        /// 产生式结点
        /// </summary>
        /// <param name="name">结点名称</param>
        /// <param name="position">结点位置（叶节点，非叶节点，未知）</param>
        public ProductionNode(string name, EnumProductionNodePosition position)
            : this(name, name, position)
        { }
        /// <summary>
        /// 产生式结点
        /// </summary>
        /// <param name="name">结点名称</param>
        /// <param name="note">注释</param>
        /// <param name="position">结点位置（叶节点，非叶节点，未知）</param>
        public ProductionNode(string name, string note, EnumProductionNodePosition position)
        {
            NodeName = name;
            m_NodeNote = note;
            Position = position;
        }
        EnumProductionNodePosition m_Position;
        /// <summary>
        /// 产生式结点类型：叶结点，非叶结点，未知类型的结点
        /// </summary>
        public EnumProductionNodePosition Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }
        /// <summary>
        /// 根据结点是叶节点、非叶节点输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = null;
            switch (m_Position)
            {
                case EnumProductionNodePosition.Leave:
                    result = string.Format("{0}", m_NodeName);
                    break;
                case EnumProductionNodePosition.NonLeave:
                    result = string.Format("<{0}>", m_NodeName);
                    break;
                //case EnumProductionNodePosition.My:
                //    result = string.Format("{0}", m_NodeName);
                //    break;
                case EnumProductionNodePosition.Unknown:
                    result = string.Format("?{0}", m_NodeName);
                    break;
                default:
                    result = string.Format("??{0}", m_NodeName);
                    break;
            }
            return result;
            //return base.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlFormat">是否使用HTML或XML格式输出</param>
        /// <returns></returns>
        public string ToString(bool htmlFormat)
        {
            if (htmlFormat)
            {
                StringBuilder builder = new StringBuilder(ToString());
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
            else return ToString();
        }
        /// <summary>
        /// From 
        /// </summary>
        /// <param name="xProductionNode"></param>
        /// <returns></returns>
        public static ProductionNode From(XElement xProductionNode)
        {
            if (xProductionNode == null) return null;
            if (xProductionNode.Name != strProductionNode) return null;
            var result = new ProductionNode(
                xProductionNode.Attribute(strName).Value,
                xProductionNode.Attribute(strNote).Value,
                (EnumProductionNodePosition)Enum.Parse(
                typeof(EnumProductionNodePosition),
                xProductionNode.Attribute(strPosition).Value));
            return result;
        }
        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strProductionNode,
                new XAttribute(strName, this.m_NodeName),
                new XAttribute(strNote, this.m_NodeNote),
                new XAttribute(strPosition, this.m_Position)
                );
            return result;
        }

        private const string strName = "NodeName";
        private const string strNote = "NodeNote";
        private const string strPosition = "Position";
        /// <summary>
        /// public const string strProductionNode = "ProductionNode";
        /// </summary>
        public const string strProductionNode = "ProductionNode";

        /// <summary>
        /// 获取此结点的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new ProductionNode(m_NodeName, m_NodeNote, m_Position);
            return result;
        }
    }
    /// <summary>
    /// 产生式结点类型：叶结点，非叶结点，未知类型的结点
    /// </summary>
    public enum EnumProductionNodePosition
    {
        /// <summary>
        /// 叶结点
        /// </summary>
        Leave,
        /// <summary>
        /// 非叶结点
        /// </summary>
        NonLeave,
        ///// <summary>
        ///// 文法的关键字，用""引起来的字母串
        ///// </summary>
        //My,
        /// <summary>
        /// 未知类型的结点
        /// </summary>
        Unknown
    }
}
