using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 产生式列表的列表（产生式右部）
    /// </summary>
    public class RightSection : List<ProductionNodeList>, ICloneable
    {
        /// <summary>
        /// 判定两个产生式列表的列表（产生式右部）是否相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as RightSection;
            if (another == null) return false;
            if (this.Count != another.Count) return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(another))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var element in this)
            {
                builder.Append(element.GetHashCode());
            }
            return builder.ToString().GetHashCode();
            //return base.GetHashCode();
        }
        /// <summary>
        /// &lt;Vn&gt; ::= ProductionNodeList | ProductionNodeList ...
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
                    result.Append(this[i].ToString() + " | ");
                }
                result.Append(this[preCount].ToString());
            }
            return result.ToString();
        }
        /// <summary>
        /// &lt;Vn&gt; ::= ProductionNodeList | ProductionNodeList ...
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
                    result.Append(this[i].ToString(htmlFormat) + " | ");
                }
                result.Append(this[preCount].ToString(htmlFormat));
            }
            return result.ToString();
        }

        /// <summary>
        /// From 
        /// </summary>
        /// <param name="xProductionNodeListList"></param>
        /// <returns></returns>
        public static RightSection From(XElement xProductionNodeListList)
        {
            if (xProductionNodeListList == null) return null;
            if (xProductionNodeListList.Name != strRightSection) return null;
            var result = new RightSection();
            result.AddRange(
                from item in xProductionNodeListList.Elements(ProductionNodeList.strCandidate)
                select ProductionNodeList.From(item)
                );
            return result;
        }

        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strRightSection,
                new XAttribute("CandidateCount", this.Count),
                from item in this
                select item.ToXElement());
            return result;
        }
        /// <summary>
        /// public const string strRightSection = "RightSection";
        /// </summary>       
        public const string strRightSection = "RightSection";

        /// <summary>
        /// 获取此产生式列表的列表（产生式右部）的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new RightSection();
            foreach (var element in this)
            {
                result.Add(element.Clone() as ProductionNodeList);
            }
            return result;
        }
    }

}
