using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 推导式列表
    /// </summary>
    public class DerivationList : List<Derivation>
    {
        /// <summary>
        /// 判定两个推导式列表是否相同
        /// </summary>
        /// <param name="obj">被比较的对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as DerivationList;
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
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator ==(DerivationList a, DerivationList b)
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
        public static bool operator !=(DerivationList a, DerivationList b)
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
        /// 输出各个产生式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var v in this)
            {
                result.Append(v.ToString());
                result.AppendLine();
            }
            return result.ToString();
        }
        /// <summary>
        /// 输出各个产生式
        /// </summary>
        /// <param name="htmlFormat">是否使用HTML或XML格式输出</param>
        /// <returns></returns>
        public string ToString(bool htmlFormat)
        {
            StringBuilder result = new StringBuilder();
            foreach (var v in this)
            {
                result.Append(v.ToString(htmlFormat));
                result.AppendLine();
            }
            return result.ToString();
        }
        

        /// <summary>
        /// From 
        /// </summary>
        /// <param name="xDerivationList"></param>
        /// <returns></returns>
        public static DerivationList From(XElement xDerivationList)
        {
            if (xDerivationList == null) return null;
            if (xDerivationList.Name != strDerivationList) return null;
            var result = new DerivationList();
            result.AddRange(
                from item in xDerivationList.Elements(Derivation.strDerivation)
                select Derivation.From(item)
                );
            return result;
        }
        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strDerivationList,
                new XAttribute("Count", this.Count),
                from item in this
                select item.ToXElement());
            return result;
        }

        /// <summary>
        /// public const string strDerivation = "DerivationList";
        /// </summary>       
        public const string strDerivationList = "DerivationList";
        /// <summary>
        /// 获取此推导式的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new DerivationList();
            foreach (var item in this)
            {
                var derivation = item.Clone() as Derivation;
                result.Add(derivation);
            }
            //if (this.Left != null)
            //    result.Left = this.Left.Clone() as ProductionNode;
            //if (this.Right != null)
            //    result.Right = this.Right.Clone() as ProductionNodeList;
            return result;
        }

        //private ProductionNode m_Left = ProductionNode.startEndLeave;
        ///// <summary>
        ///// 推导式的左部
        ///// </summary>
        //public ProductionNode Left   
        //{
        //    get { return m_Left; }
        //    set { if (value != null) m_Left = value; }
        //}

        //private ProductionNodeList m_Right = ProductionNodeList.epsilon;
        ///// <summary>
        ///// 推导式的右部
        ///// </summary>
        //public ProductionNodeList Right
        //{
        //    get { return m_Right; }
        //    set { if (value != null) m_Right = value; }
        //}
    }
}
