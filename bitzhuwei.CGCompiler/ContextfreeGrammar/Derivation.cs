using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 推导式
    /// </summary>
    public class Derivation : ICloneable
    {
        /// <summary>
        /// 获取推导式
        /// </summary>
        public Derivation() { }
        /// <summary>
        /// 获取推导式
        /// </summary>
        /// <param name="left">左部</param>
        /// <param name="right">右部</param>
        public Derivation(ProductionNode left, ProductionNodeList right)
        {
            if (left != null) this.Left = left;
            if (right != null) this.Right = right;
        }
        /// <summary>
        /// 判定两个推导式是否相同
        /// </summary>
        /// <param name="obj">被比较的对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as Derivation;
            if (another == null) return false;
            if (this.m_Left != another.m_Left
                || this.m_Right != another.m_Right)
                return false;
            return true;
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator ==(Derivation a, Derivation b)
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
            if (a.m_Left != b.m_Left
                || a.m_Right != b.m_Right)
                return false;
            return true;
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator !=(Derivation a, Derivation b)
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
        /// &lt;Vn&gt; ::= ProductionNodeList
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} ::= {1};", m_Left.ToString(), m_Right.ToString());
        }
        /// <summary>
        /// &lt;Vn&gt; ::= ProductionNodeList
        /// </summary>
        /// <param name="htmlFormat">是否使用html或XML格式输出</param>
        /// <returns></returns>
        public string ToString(bool htmlFormat)
        {
            return string.Format("{0} ::= {1};", m_Left.ToString(htmlFormat), m_Right.ToString(htmlFormat));
        }

        /// <summary>
        /// From 
        /// </summary>
        /// <param name="xDerivation"></param>
        /// <returns></returns>
        public static Derivation From(XElement xDerivation)
        {
            if (xDerivation == null) return null;
            if (xDerivation.Name != strDerivation) return null;
            var result = new Derivation();
            result.Left = ProductionNode.From(xDerivation.Element(strLeft));
            result.Right = ProductionNodeList.From(xDerivation.Element(strRight));
            return result;
        }
        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strDerivation,
                m_Left.ToXElement(),
                m_Right.ToXElement()
                );
            return result;
        }

        /// <summary>
        /// public const string strLeft = "Left";
        /// </summary>       
        private const string strLeft = "Left";
        /// <summary>
        /// public const string strRight = "Right";
        /// </summary>       
        private const string strRight = "Right";
        /// <summary>
        /// public const string strDerivation = "Derivation";
        /// </summary>       
        public const string strDerivation = "Derivation";
        /// <summary>
        /// 获取此推导式的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new Derivation();
            if (this.Left != null)
                result.Left = this.Left.Clone() as ProductionNode;
            if (this.Right != null)
                result.Right = this.Right.Clone() as ProductionNodeList;
            return result;
        }

        private ProductionNode m_Left = ProductionNode.startEndLeave;
        /// <summary>
        /// 推导式的左部
        /// </summary>
        public ProductionNode Left
        {
            get { return m_Left; }
            set { if (value != null) m_Left = value; }
        }

        private ProductionNodeList m_Right = ProductionNodeList.epsilon;
        /// <summary>
        /// 推导式的右部
        /// </summary>
        public ProductionNodeList Right
        {
            get { return m_Right; }
            set { if (value != null) m_Right = value; }
        }
    }
}
