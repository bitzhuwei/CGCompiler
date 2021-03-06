namespace bitzhuwei.LR1DemoCompiler
{
    /// <summary>
    /// 文法LR1Demo的语法树结点的值
    /// </summary>
    public class TreeNodeValueLR1Demo : System.ICloneable
    {
        private string m_NodeName = string.Empty;
        /// <summary>
        /// 结点名称
        /// </summary>
        public string NodeName
        {
            get { return m_NodeName; }
            set { m_NodeName = value; }
        }
        private EnumVTypeLR1Demo m_NodeType = EnumVTypeLR1Demo.Unknown;
        /// <summary>
        /// 结点类型
        /// </summary>
        public EnumVTypeLR1Demo NodeType
        {
            get { return m_NodeType; }
            set { m_NodeType = value; }
        }
        /// <summary>
        /// "名称, 类型"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            bool leave = this.NodeType.ToString().EndsWith("Leave");
            if (NodeName != NodeType.ToString())
            { return string.Format("{0}{1} <= {2}{3}", leave ? "" : "[", NodeName, NodeType, leave ? "" : "]"); }
            else
            { return string.Format("{0}{1}{2}", leave ? "" : "[", NodeName, leave ? "" : "]"); }
        }
        public object Clone()
        {
            var result = new TreeNodeValueLR1Demo();
            result.NodeName = this.NodeName;
            result.NodeType = this.NodeType;
            return result;
        }
    }

}

