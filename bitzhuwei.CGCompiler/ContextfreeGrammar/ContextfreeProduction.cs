using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 上下文无关产生式
    /// </summary>
    public class ContextfreeProduction : ICloneable
    {
        /// <summary>
        /// 判定两个产生式是否相同
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as ContextfreeProduction;
            if (another == null) return false;
            for (int i = 0; i < this.RightCollection.Count; i++)
            {
                if (!this.RightCollection[i].Equals(another.RightCollection[i]))
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
        public static bool operator ==(ContextfreeProduction a, ContextfreeProduction b)
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
            for (int i = 0; i < a.RightCollection.Count; i++)
            {
                if (!a.RightCollection[i].Equals(b.RightCollection[i]))
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
        public static bool operator !=(ContextfreeProduction a, ContextfreeProduction b)
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
        ///// <summary>
        ///// 获取一个消除了左递归的产生式
        ///// </summary>
        ///// <returns></returns>
        //public List<ContextfreeProduction<TEnumVType>> EliminateLeftRecursion()
        //{
        //    var result = new List<ContextfreeProduction<TEnumVType>>();
        //    var origin = this.Clone() as ContextfreeProduction<TEnumVType>;
        //    var recuresionList = new List<ProductionNodeList<TEnumVType>>();
        //    var nonRecuresionList = new List<ProductionNodeList<TEnumVType>>();

        //    foreach (var item in origin.RightCollection)
        //    {
        //        if (item[0].Equals(origin.Left))
        //        {
        //            recuresionList.Add(item);
        //        }
        //        else
        //        {
        //            nonRecuresionList.Add(item);
        //        }
        //    }
        //    if (recuresionList.Count == 0)
        //    {
        //        result.Add(origin);
        //        return result;
        //    }
        //    else
        //    {
        //        var newProduction = new ContextfreeProduction<TEnumVType>();
        //        //var newLeft = new ProductionNode<TEnumVType>(origin.Left.NodeName+"_Recuresion",);
        //        foreach (var item in recuresionList)
        //        {

        //        }
        //    }
        //}
        /// <summary>
        /// 获取一个归并了产生式右部的重复内容的新的产生式
        /// <para>原有产生式不受影响</para>
        /// </summary>
        /// <returns></returns>
        public ContextfreeProduction Dump()
        {
            var result = this.Clone() as ContextfreeProduction;
            
            for (int i = 0; i < result.RightCollection.Count - 1; i++)
            {
                for (int j = i + 1; j < result.RightCollection.Count; j++)
                {
                    if (result.RightCollection[i].Equals(result.RightCollection[j]))
                    {
                        result.RightCollection.Remove(result.RightCollection[j]);
                        j--;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 给出产生式内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ::= {1};", Left.ToString(), RightCollection.ToString());
            //return base.ToString();
        }
        /// <summary>
        /// 给出产生式内容
        /// <para>&lt;Vn&gt; ::= ProductionNodeList | ProductionNodeList ...</para>
        /// </summary>
        /// <param name="htmlFormat">是否使用HTML或XML格式输出</param>
        /// <returns></returns>
        public string ToString(bool htmlFormat)
        {
            var left = this.Left.ToString(htmlFormat);
            var rightCollection = this.RightCollection.ToString(htmlFormat);
            return string.Format("{0} ::= {1};", left, rightCollection);
            //return string.Format("{0} ::= {1};", Left.ToString(htmlFormat), RightCollection.ToString(htmlFormat));
        }
        /*
         * G:
         * S ::= ABC
         * 
         * S ::= A B C
         * [S] ::= [A][B][C]
         */

        /// <summary>
        /// 产生式左部
        /// </summary>
        private ProductionNode m_Left = new ProductionNode("BIT祝威 SmileWei", EnumProductionNodePosition.Unknown);
        /// <summary>
        /// 产生式左部
        /// </summary>
        public ProductionNode Left
        {
            get { return m_Left; }
            set { if (value != null) m_Left = value; }
        }

        /// <summary>
        /// 产生式右部
        /// </summary>
        private RightSection m_RightCollection = new RightSection();
        /// <summary>
        /// 产生式右部
        /// </summary>
        public RightSection RightCollection
        {
            get { return m_RightCollection; }
            set { m_RightCollection = value; }
        }
        /// <summary>
        /// 判定此产生式右部是否包含给定的候选式
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public bool Contains(ProductionNodeList candidate)
        {
            if (candidate == null) return false;
            foreach (var originalCandidate in this.RightCollection)
            {
                if (originalCandidate.Equals(candidate))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// From 
        /// </summary>
        /// <param name="xContextfreeProduction"></param>
        /// <returns></returns>
        public static ContextfreeProduction From(XElement xContextfreeProduction)
        {
            if (xContextfreeProduction == null) return null;
            if (xContextfreeProduction.Name != strContextfreeProduction) return null;
            var result = new ContextfreeProduction();
            result.Left = ProductionNode.From(xContextfreeProduction.Element(ProductionNode.strProductionNode));
            result.RightCollection = RightSection.From(xContextfreeProduction.Element(RightSection.strRightSection));
            return result;
        }

        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strContextfreeProduction,
                this.Left.ToXElement(),
                this.RightCollection.ToXElement());
            return result;
        }
        /// <summary>
        /// public const string strContextfreeProduction = "ContextfreeProduction";
        /// </summary>       
        public const string strContextfreeProduction = "ContextfreeProduction";

        /// <summary>
        /// 获取此上下文无关产生式的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new ContextfreeProduction();
            if (this.Left != null)
                result.Left = this.Left.Clone() as ProductionNode;
            if (this.RightCollection != null)
                result.RightCollection = this.RightCollection.Clone() as RightSection;
            return result;
        }
    }
}
