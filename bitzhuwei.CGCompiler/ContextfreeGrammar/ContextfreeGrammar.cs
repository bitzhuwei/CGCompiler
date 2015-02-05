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
        /// 判定两个文法是否相同
        /// </summary>
        /// <param name="obj">被比较的对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as ContextfreeGrammar;
            if (another == null) return false;
            return (
                this.m_GrammarName == another.m_GrammarName
                && this.m_ProductionCollection == another.m_ProductionCollection
                );
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator ==(ContextfreeGrammar a, ContextfreeGrammar b)
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
            return (
                a.m_GrammarName == b.m_GrammarName
                && a.m_ProductionCollection == b.m_ProductionCollection
                );
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator !=(ContextfreeGrammar a, ContextfreeGrammar b)
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
        /// 获取规范化之后的上下文无关文法
        /// <para>只合并左侧相同的产生式，不调整文法结构</para>
        /// </summary>
        /// <returns></returns>
        public ContextfreeGrammar Normalize()
        {
            var result = new ContextfreeGrammar();

            result.GrammarName = this.GrammarName;

            var startProduction = this.ProductionCollection[0].Dump();
            //this.ProductionCollection[0].Clone() as ContextfreeProduction<TEnumVType>;
            result.ProductionCollection.Add(startProduction);

            ContextfreeProduction production = null;
            for (int i = 1; i < this.ProductionCollection.Count; i++)
            {//将production加入文法中
                production = this.m_ProductionCollection[i];
                bool thisAdded = false;
                foreach (var added in result.ProductionCollection)//尝试并入原有产生式
                {
                    if (added.Left.Equals(production.Left))//左部相同，应该合并
                    {
                        foreach (var waiting in production.RightCollection)
                        {//将新的候选式加入右部
                            if (!added.Contains(waiting))
                            {
                                added.RightCollection.Add(waiting);
                            }
                        }
                        thisAdded = true;
                    }
                }
                if (!thisAdded)//添加新的产生式
                {
                    result.ProductionCollection.Add(production);
                }
            }

            return result;
        }
        /// <summary>
        /// 获取规范化之后的上下文无关文法
        /// <para>只合并左侧相同的产生式，不调整文法结构</para>
        /// </summary>
        /// <param name="overrideGrammarName">标识是否使用起始非叶节点名作为文法名</param>
        /// <returns></returns>
        public ContextfreeGrammar Normalize(bool overrideGrammarName)
        {
            var result = Normalize();
            if (overrideGrammarName)
                result.GrammarName = m_ProductionCollection[0].Left.NodeName;
            return result;
        }
        /// <summary>
        /// 获取规范化之后的上下文无关文法
        /// <para>只合并左侧相同的产生式，不调整文法结构</para>
        /// </summary>
        /// <param name="grammarName">规范化之后的文法名</param>
        /// <returns></returns>
        public ContextfreeGrammar Normalize(string grammarName)
        {
            var result = Normalize();
            this.GrammarName = grammarName;
            return result;
        }
        /// <summary>
        /// 上下文无法文法的内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name:{0}{1}{2}", m_GrammarName, Environment.NewLine, this.m_ProductionCollection.ToString());
            //return base.ToString();
        }
        /// <summary>
        /// 上下文无法文法的内容
        /// </summary>
        /// <param name="htmlFormat">是否使用HTML或XML格式输出</param>
        /// <returns></returns>
        public string ToString(bool htmlFormat)
        {
            return string.Format("Name:{0}{1}{2}", m_GrammarName, Environment.NewLine, this.m_ProductionCollection.ToString(htmlFormat));            
        }
        #region 属性和字段

        ContextfreeProductionList m_ProductionCollection = new ContextfreeProductionList();
        /// <summary>
        /// 产生式列表
        /// </summary>
        public ContextfreeProductionList ProductionCollection
        {
            get { return m_ProductionCollection; }
            set
            {
                if (value == null)
                    m_ProductionCollection.Clear();
                else
                    m_ProductionCollection = value;
            }
        }

        /// <summary>
        /// 文法名
        /// </summary>
        private string m_GrammarName = "DefaultGrammar";
        /// <summary>
        /// 文法名
        /// </summary>
        public string GrammarName
        {
            get { return m_GrammarName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if ('0' <= value[0] && value[0] <= '9')
                        this.m_GrammarName = "_" + value;
                    else
                        this.m_GrammarName = value;
                }
            }
        }
        private string m_namespace = string.Empty;
        /// <summary>
        /// 命名空间名
        /// </summary>
        public string Namespace
        {
            get
            {
                if (string.IsNullOrEmpty(m_namespace)) return "bitzhuwei.Compiler";
                else return m_namespace;
            }
            set
            {
                this.m_namespace = value;
            }
        }

        #endregion 属性和字段

        /// <summary>
        /// 从<code>XElement</code>中读取一个<code>ContextfreeGrammar</code>对象
        /// </summary>
        /// <param name="xContextfreeGrammar"></param>
        /// <returns></returns>
        public static ContextfreeGrammar From(XElement xContextfreeGrammar)
        {
            if (xContextfreeGrammar == null) return null;
            if (xContextfreeGrammar.Name != strContextfreeGrammar) return null;
            var result = new ContextfreeGrammar();
            result.GrammarName = xContextfreeGrammar.Attribute(strGrammarName).Value;
            result.Namespace = xContextfreeGrammar.Attribute(strNamespace).Value;
            result.ProductionCollection = ContextfreeProductionList.From(
                xContextfreeGrammar.Element(ContextfreeProductionList.strContextfreeProductionList));
            return result;
        }
        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strContextfreeGrammar,
                new XAttribute(strGrammarName, this.m_GrammarName),
                new XAttribute(strNamespace , this.m_namespace),
                new XElement("source_code_of_CGCompiler", this.ToString()),
                this.ProductionCollection.ToXElement()
                );
            return result;
        }
        private const string strNamespace = "Namespace";
        private const string strGrammarName = "GrammarName";
        /// <summary>
        /// public const string strContextfreeGrammar = "ContextfreeGrammar";
        /// </summary>
        public const string strContextfreeGrammar = "ContextfreeGrammar";

        /// <summary>
        /// 获取此上下文无关文法的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new ContextfreeGrammar();
            result.m_GrammarName = this.m_GrammarName;
            result.m_namespace = this.m_namespace;
            result.m_ProductionCollection = this.m_ProductionCollection.Clone() as ContextfreeProductionList;
            return result;
        }

    }
}
