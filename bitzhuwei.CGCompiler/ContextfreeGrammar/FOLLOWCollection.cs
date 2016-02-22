using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 文法的FOLLOW集
    /// </summary>
    public class FOLLOWCollection : List<FOLLOWCollectionItem>
    {
        /// <summary>
        /// 创建文法的FOLLOW集
        /// </summary>
        /// <param name="grammar">对应的文法</param>
        public FOLLOWCollection(ContextfreeGrammar grammar)
        {
            this.m_Grammar = grammar;
        }
        /// <summary>
        /// 对应的文法
        /// </summary>
        private ContextfreeGrammar m_Grammar = null;
        /// <summary>
        /// 对应的文法
        /// </summary>
        /// <returns></returns>
        public ContextfreeGrammar GetGrammar()
        {
            return m_Grammar;
        }
        /// <summary>
        /// 获取FOLLOW集内容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var builer = new StringBuilder();
            foreach (var item in this)
            {
                builer.AppendLine(item.ToString());
            }
            return builer.ToString();
            //return base.ToString();
        }
        /// <summary>
        /// 获取给定结点的FOLLOW集的一项
        /// </summary>
        /// <param name="left">给定的结点</param>
        /// <returns></returns>
        public FOLLOWCollectionItem GetItem(ProductionNode left)
        {
            foreach (var item in this)
            {
                if (item.ObjectiveProduction.Left == left)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
