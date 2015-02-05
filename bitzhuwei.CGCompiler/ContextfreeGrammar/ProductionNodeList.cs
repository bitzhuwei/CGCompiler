using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 产生式结点列表（候选式）
    /// </summary>
    public class ProductionNodeList : List<ProductionNode>,ICloneable
    {
        /// <summary>
        /// 判定两个产生式结点列表（候选式）是否相同
        /// </summary>
        /// <param name="obj">被比较的对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as ProductionNodeList;
            if (another == null) return false;
            if (this.Count != another.Count) return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(another[i]))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator ==(ProductionNodeList a, ProductionNodeList b)
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
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator !=(ProductionNodeList a, ProductionNodeList b)
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
            //return base.GetHashCode();
        }

        /// <summary>
        /// &lt;Vn&gt; Vn ...
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            int preCount = this.Count - 1;
            if (preCount >= 0)
            {
                for (int i = 0; i < preCount; i++)
                {
                    result.Append(this[i].ToString() + " ");
                }
                result.Append(this[preCount].ToString());
            }
            else
                result.Append("null");
            return result.ToString();
        }
        /// <summary>
        /// &lt;Vn&gt; Vn ...
        /// </summary>
        /// <param name="htmlFormat">是否使用HTML或XML格式输出</param>
        /// <returns></returns>
        public string ToString(bool htmlFormat)
        {
            StringBuilder result = new StringBuilder();
            int preCount = this.Count - 1;
            if (preCount >= 0)
            {
                for (int i = 0; i < preCount; i++)
                {
                    // todo: &nbsp; problem, use blank space instead
                    result.Append(this[i].ToString(htmlFormat) + " ");
                }
                result.Append(this[preCount].ToString(htmlFormat));
            }
            return result.ToString();
        }
        /// <summary>
        /// From 
        /// </summary>
        /// <param name="xProductionNodeList"></param>
        /// <returns></returns>
        public static ProductionNodeList From(XElement xProductionNodeList)
        {
            if (xProductionNodeList == null) return null;
            if (xProductionNodeList.Name != strCandidate) return null;
            var result = new ProductionNodeList();
            result.AddRange(
                from item in xProductionNodeList.Elements(ProductionNode.strProductionNode)
                select ProductionNode.From(item)
                );
            return result;
        }
        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strCandidate,
                new XAttribute("NodeCount", this.Count),
                from item in this
                select item.ToXElement());
            return result;
        }
        /// <summary>
        /// public const string strCandidate = "ProductionNodeList";
        /// </summary>       
        public const string strCandidate = "ProductionNodeList";
        /// <summary>
        /// 获取此产生式结点列表（候选式）的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new ProductionNodeList();
            result.AddRange(
                from item in this
                select (item.Clone() as ProductionNode));
            return result;
        }
        /// <summary>
        /// ε（空结点）的候选式
        /// </summary>
        public static readonly ProductionNodeList epsilon =
            new ProductionNodeList() { ProductionNode.tail_null };
    }

}
