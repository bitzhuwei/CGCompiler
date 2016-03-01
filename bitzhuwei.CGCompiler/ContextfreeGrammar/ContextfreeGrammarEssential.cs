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
        /// <summary>
        /// 获取非终结点列表
        /// </summary>
        public List<ProductionNode> GetNonterminalList()
        {
            var result = new List<ProductionNode>();
            foreach (var item in this.m_ProductionCollection)
            {
                result.Add(item.Left);
            }
            return result;
        }

        /// <summary>
        /// 获取终结点列表
        /// </summary>
        public List<ProductionNode> GetTerminalList()
        {
            //var result =
            //    (from production in this.m_ProductionCollection
            //     from candidate in production.RightCollection
            //     from node in candidate
            //     where (
            //        node.Position == EnumProductionNodePosition.Leave
            //         //|| node.Position == EnumProductionNodePosition.My
            //        )
            //     select node).Distinct().ToList();
            List<ProductionNode> result = new List<ProductionNode>();
            foreach (var production in this.m_ProductionCollection)
            {
                foreach (var candidate in production.RightCollection)
                {
                    foreach (var node in candidate)
                    {
                        if (node.Position == EnumProductionNodePosition.Leave
                            && (!result.Contains(node)))
                        {
                            result.Add(node);
                        }
                    }
                }
            }
            //if (!result.Contains(ProductionNode.my_constString))
            //    foreach (var item in result)
            //    {
            //        if (item.Position == EnumProductionNodePosition.Leave
            //            && item.NodeName.StartsWith("\"")
            //            && item.NodeName.Length > 2
            //            && item.NodeName.EndsWith("\""))
            //        {
            //            result.Add(ProductionNode.my_constString);
            //            break;
            //        }
            //    }
            return result;
        }

        /// <summary>
        /// 获取开始结点
        /// </summary>
        public ProductionNode GetHeadNode()
        {
            if (this.m_ProductionCollection == null
                || this.m_ProductionCollection.Count == 0)
            {
                return null;
            }
            return this.m_ProductionCollection[0].Left;
        }

    }
}
