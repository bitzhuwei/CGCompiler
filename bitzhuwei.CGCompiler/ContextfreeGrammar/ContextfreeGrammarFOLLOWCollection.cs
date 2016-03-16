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
        /// 获取此文法的FOLLOW集
        /// <para>请使用<code>ContextfreeGrammar.Normalize()</code>确保文法项已合并</para>
        /// </summary>
        /// <returns></returns>
        public FOLLOWCollection GetFollowCollection()
        {
            var result = new FOLLOWCollection(this);
            result.Clear();
            var items = (from production in this.ProductionCollection
                         select new FOLLOWCollectionItem(production, this));
            result.AddRange(items);
            bool changed = false;
            result[0].Add(ProductionNode.startEndLeave);
            var firstCollection = this.GetFirstCollection();
            do
            {
                changed = false;
                foreach (var production in this.m_ProductionCollection)
                {
                    foreach (var candidate in production.RightCollection)
                    {
                        for (int i = 0; i < candidate.Count - 1; i++)
                        {
                            var node = candidate[i];
                            if (node.Position == EnumProductionNodePosition.NonLeave)
                            {
                                var targetItem = result.GetItem(node);
                                int j = i + 1;
                                for (; j < candidate.Count; j++)
                                {
                                    var postNode = candidate[j];
                                    if (postNode.Position == EnumProductionNodePosition.Leave)
                                    {
                                        changed = targetItem.Add(postNode) || changed;
                                        break;
                                    }
                                    else if (postNode.Position == EnumProductionNodePosition.NonLeave)
                                    {
                                        var postNodePrd = this.GetProduction(postNode);
                                        bool notNull = true;
                                        foreach (var postNodeCand in postNodePrd.RightCollection)
                                        {
                                            var firstCollectionItem = firstCollection.GetItem(postNodeCand);
                                            foreach (var item in firstCollectionItem.Value)
                                            {
                                                if (item != ProductionNode.tail_null)
                                                {
                                                    changed = targetItem.Add(item) || changed;
                                                }
                                                else
                                                    notNull = false;
                                            }
                                        }
                                        if (notNull)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (j == candidate.Count)
                                {
                                    var leftNodeFollowCollection = result.GetItem(production.Left);
                                    foreach (var item in leftNodeFollowCollection.Value)
                                    {
                                        changed = targetItem.Add(item) || changed;
                                    }
                                }
                            }
                        }
                        if (candidate.Count == 0)
                        {
                            Console.WriteLine("候选式不含有效结点！");
                            Console.ReadKey(true);
                        }
                        else
                        {
                            var lastNode = candidate[candidate.Count - 1];
                            if (lastNode.Position == EnumProductionNodePosition.NonLeave)
                            {
                                var targetItem = result.GetItem(lastNode);
                                var followCollectionItem = result.GetItem(production.Left);
                                foreach (var item in followCollectionItem.Value)
                                {
                                    changed = targetItem.Add(item) || changed;
                                }
                            }
                        }
                    }
                }
            } while (changed);
            return result;
        }

        ///// <summary>
        ///// 获取此文法的FOLLOW集
        ///// <para>请使用<code>ContextfreeGrammar.Normalize()</code>确保文法项已合并</para>
        ///// </summary>
        ///// <returns></returns>
        //public FOLLOWCollection GetFollowCollection()
        //{
        //    var result = new FOLLOWCollection(this);
        //    result.Clear();
        //    try
        //    {
        //        var items = (from production in this.ProductionCollection
        //                     select new FOLLOWCollectionItem(production, this));
        //        result.AddRange(items);
        //        bool changed = false;
        //        //int pass = 0;
        //        result[0].Add(ProductionNode.startEndLeave);
        //        var firstCollection = this.GetFirstCollection();
        //        do
        //        {
        //            changed = false;
        //            foreach (var production in this.m_ProductionCollection)
        //            {
        //                foreach (var candidate in production.RightCollection)
        //                {
        //                    for (int i = 0; i < candidate.Count - 1; i++)
        //                    {
        //                        var node = candidate[i];
        //                        if (node.Position == EnumProductionNodePosition.NonLeave)
        //                        {
        //                            var targetItem = result.GetItem(node);
        //                            int j = i + 1;
        //                            for (; j < candidate.Count; j++)
        //                            {
        //                                var postNode = candidate[j];
        //                                if (postNode.Position == EnumProductionNodePosition.Leave)
        //                                {
        //                                    changed = targetItem.Add(postNode) || changed;
        //                                    break;
        //                                }
        //                                else if (postNode.Position == EnumProductionNodePosition.NonLeave)
        //                                {
        //                                    var postNodePrd = this.GetProduction(postNode);
        //                                    bool notNull = true;
        //                                    foreach (var postNodeCand in postNodePrd.RightCollection)
        //                                    {
        //                                        var firstCollectionItem = firstCollection.GetItem(postNodeCand);
        //                                        foreach (var item in firstCollectionItem.Value)
        //                                        {
        //                                            if (item != ProductionNode.tail_null)
        //                                            {
        //                                                changed = targetItem.Add(item) || changed;
        //                                            }
        //                                            else
        //                                                notNull = false;
        //                                        }
        //                                    }
        //                                    if (notNull)
        //                                    {
        //                                        break;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    Console.WriteLine("产生式结点类型非法");
        //                                    Console.ReadKey(true);
        //                                }
        //                            }
        //                            if (j == candidate.Count)
        //                            {
        //                                var leftNodeFollowCollection = result.GetItem(production.Left);
        //                                foreach (var item in leftNodeFollowCollection.Value)
        //                                {
        //                                    changed = targetItem.Add(item) || changed;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (candidate.Count == 0)
        //                    {
        //                        Console.WriteLine("候选式不含有效结点！");
        //                        Console.ReadKey(true);
        //                    }
        //                    else
        //                    {
        //                        var lastNode = candidate[candidate.Count - 1];
        //                        if (lastNode.Position == EnumProductionNodePosition.NonLeave)
        //                        {
        //                            var targetItem = result.GetItem(lastNode);
        //                            var followCollectionItem = result.GetItem(production.Left);
        //                            foreach (var item in followCollectionItem.Value)
        //                            {
        //                                changed = targetItem.Add(item) || changed;
        //                            }
        //                        }
        //                    }
        //                }
        //                //Console.WriteLine("遍：{0}", pass++);
        //                //Console.WriteLine(result.ToString());
        //                //Console.ReadKey(true);
        //            }
        //        } while (changed);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (result.Count > 0)
        //            result[0].Add(
        //                new ProductionNode(ex.Message, ex.StackTrace,
        //                    EnumProductionNodePosition.Unknown));
        //    }

        //    return result;
        //}
        
    }
}
