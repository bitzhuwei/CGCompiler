using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 文法的FIRST集中的一项
    /// </summary>
    public class FIRSTCollectionItem
    {
        /// <summary>
        /// 创建一个文法的FIRST集中的一项
        /// <para>一项也是一个集合，其元素为产生式结点（只有叶结点）</para>
        /// </summary>
        /// <param name="candiate">指向的候选式</param>
        /// <param name="production">指向的产生式</param>
        /// <param name="grammar">指向的文法</param>
        public FIRSTCollectionItem(ProductionNodeList candiate, ContextfreeProduction production, ContextfreeGrammar grammar)
        {
            this.m_ObjectiveCandidate = candiate;
            this.m_ObjectiveProduction = production;
            this.m_ObjectiveGrammar = grammar;
        }
        /// <summary>
        /// 添加元素到此FIRST集
        /// <para>若不包含此元素，则添加，并返回true</para>
        /// <para>若元素为null或已包含此元素或元素不是叶结点，则不会添加，并返回false</para>
        /// </summary>
        /// <param name="item"></param>
        /// <returns>若添加，返回true；若没有添加，返回false</returns>
        public bool Add(ProductionNode item)
        {
            if (item == null
                || this.m_Value.Contains(item)
                //|| (item.Position != EnumProductionNodePosition.Leave && item.Position != EnumProductionNodePosition.My)
                )
                return false;
            this.m_Value.Add(item);
            return true;
        }

        /// <summary>
        /// 此项FIRST集指向的候选式
        /// </summary>
        ProductionNodeList m_ObjectiveCandidate;
        /// <summary>
        /// 此项FIRST集指向的候选式
        /// </summary>
        public ProductionNodeList ObjectiveCandidate
        {
            get { return m_ObjectiveCandidate; }
            set { m_ObjectiveCandidate = value; }
        }

        ContextfreeProduction m_ObjectiveProduction;
        /// <summary>
        /// 此项FIRST集指向的产生式
        /// </summary>
        public ContextfreeProduction ObjectiveProduction
        {
            get { return m_ObjectiveProduction; }
            set { m_ObjectiveProduction = value; }
        }

        ContextfreeGrammar m_ObjectiveGrammar;
        /// <summary>
        /// 此项FIRST集指向的文法
        /// </summary>
        public ContextfreeGrammar ObjectiveGrammar
        {
            get { return m_ObjectiveGrammar; }
            set { m_ObjectiveGrammar = value; }
        }

        List<ProductionNode> m_Value = new List<ProductionNode>();
        /// <summary>
        /// 获取FIRST集中的元素列表
        /// </summary>
        public IEnumerable<ProductionNode> Value
        {
            get { return m_Value; }
        }
            
        private string GetValue()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            int count = this.m_Value.Count;
            if (count > 0)
            {
                for (int i = 0; i < this.m_Value.Count - 1; i++)
                {
                    builder.Append(" " + this.m_Value[i].NodeName + ",");
                }
                builder.Append(" " + this.m_Value[count - 1].NodeName + " ");
            }
            builder.Append("}");
            return builder.ToString();
        }
        /// <summary>
        /// 已重写
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("FIRST({0})={1}", m_ObjectiveCandidate, GetValue());
            //return base.ToString();
        }
    }
}
