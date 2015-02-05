using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 文法的FIRST集
    /// </summary>
    public class FIRSTCollection : List<FIRSTCollectionItem>
    {
        /// <summary>
        /// 创建文法的FIRST集
        /// </summary>
        /// <param name="grammar">对应的文法</param>
        public FIRSTCollection(ContextfreeGrammar grammar)
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
        /// 获取FIRST集内容
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
        /// 获取给定产生式对应的所有FIRST集
        /// </summary>
        /// <param name="production">产生式</param>
        /// <returns></returns>
        public IEnumerable<FIRSTCollectionItem> GetFIRSTCollectionItems(ContextfreeProduction production)
        {
            var items = from item in this
                        where item.ObjectiveProduction == production
                        select item;
            return items.AsEnumerable();
        }
        /// <summary>
        /// 获取与候选式对应的FIRST集的项
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public FIRSTCollectionItem GetItem(ProductionNodeList candidate)
        {
            foreach (var item in this)
            {
                if (item.ObjectiveCandidate == candidate)
                {
                    return item;
                }
            }
            return null;
        }


        public bool Conflicts()
        {
            foreach (var production in this.m_Grammar.ProductionCollection)
            {
                var firsts = new List<FIRSTCollectionItem>();
                foreach (var candidate in production.RightCollection)
                {
                    var first = this.GetItem(candidate);
                    if (first != null)
                    {
                        for (int i = 0; i < firsts.Count; i++)
                        {
                            foreach (var value in firsts[i].Value)
                            {
                                foreach (var value2 in first.Value)
                                {
                                    if (value .Equals(value2))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public ContextfreeProduction GetConflicted()
        {
            ContextfreeProduction result = null;
            foreach (var production in this.m_Grammar.ProductionCollection)
            {
                var firsts = new List<FIRSTCollectionItem>();
                foreach (var candidate in production.RightCollection)
                {
                    var first = this.GetItem(candidate);
                    if (first != null)
                    {
                        for (int i = 0; i < firsts.Count; i++)
                        {
                            foreach (var value in firsts[i].Value)
                            {
                                foreach (var value2 in first.Value)
                                {
                                    if (value.Equals(value2))
                                    {
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
