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
        ///// <summary>
        ///// 获取此文法的FIRST集
        ///// <para>请使用<code>ContextfreeGrammar.Normalize()</code>确保文法项已合并</para>
        ///// </summary>
        ///// <returns></returns>
        //public FIRSTCollection GetFirstCollection()
        //{
        //    var result = new FIRSTCollection(this);
        //    result.Clear();
        //    var items = (from production in this.ProductionCollection
        //                 from candidate in production.RightCollection
        //                 select new FIRSTCollectionItem(candidate, production, this));
        //    result.AddRange(items);//初始化FIRST集的数据结构
        //    bool changed = false;
        //    do
        //    {
        //        changed = false;
        //        foreach (var firstCollectionItem in result)
        //        {
        //            foreach (var node in firstCollectionItem.ObjectiveCandidate)
        //            {//对每个候选式产生其FIRST集
        //                if (node.Position == EnumProductionNodePosition.Leave)
        //                {
        //                    changed = firstCollectionItem.Add(node) || changed;
        //                    break;
        //                }
        //                else if (node.Position == EnumProductionNodePosition.NonLeave)
        //                {
        //                    var production = this.GetProduction(node);
        //                    var firstCollections = result.GetFIRSTCollectionItems(production);
        //                    foreach (var fc in firstCollections)
        //                    {
        //                        foreach (var n in fc.Value)
        //                        {
        //                            changed = firstCollectionItem.Add(n) || changed;
        //                        }
        //                    }
        //                    if (!this.CanInferNull(production))
        //                    {
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    } while (changed);

        //    return result;
        //}
        /// <summary>
        /// 获取此文法的FIRST集
        /// <para>请使用<code>ContextfreeGrammar.Normalize()</code>确保文法项已合并</para>
        /// </summary>
        /// <returns></returns>
        public FIRSTCollection GetFirstCollection()
        {
            var result = new FIRSTCollection(this);
            result.Clear();
            try
            {
                var items = (from production in this.ProductionCollection
                             from candidate in production.RightCollection
                             select new FIRSTCollectionItem(candidate, production, this));
                result.AddRange(items);//初始化FIRST集的数据结构
                bool changed = false;
                //int i = 0;
                do
                {
                    changed = false;
                    foreach (var firstCollectionItem in result)
                    {
                        foreach (var node in firstCollectionItem.ObjectiveCandidate)
                        {//对每个候选式产生其FIRST集
                            if (node.Position == EnumProductionNodePosition.Leave)
                            {
                                changed = firstCollectionItem.Add(node) || changed;
                                break;
                            }
                            else if (node.Position == EnumProductionNodePosition.NonLeave)
                            {
                                var production = this.GetProduction(node);
                                var firstCollections = result.GetFIRSTCollectionItems(production);
                                foreach (var fc in firstCollections)
                                {
                                    foreach (var n in fc.Value)
                                    {
                                        changed = firstCollectionItem.Add(n) || changed;
                                    }
                                }
                                if (!this.CanInferNull(production))
                                {
                                    break;
                                }
                            }
                            //else if (node.Position == EnumProductionNodePosition.My)
                            //{
                            //    changed = firstCollectionItem.Add(node) || changed;
                            //    break;
                            //}
                            else
                            {
                                Console.WriteLine("产生式结点类型非法");
                                //Console.ReadKey();
                            }
                        }
                    }
                    //Console.WriteLine("遍：{0}",i++);
                    //Console.WriteLine(this.ToString());
                    //Console.ReadKey(true);
                } while (changed);
            }
            catch (Exception ex)
            {
                if (result.Count > 0)
                    result[0].Add(
                        new ProductionNode(ex.Message, ex.StackTrace,
                            EnumProductionNodePosition.Unknown));
            }

            return result;
        }
        /// <summary>
        /// 获取给定左部的产生式
        /// </summary>
        /// <param name="left">左部</param>
        /// <returns></returns>
        public ContextfreeProduction GetProduction(ProductionNode left)
        {
            foreach (var item in this.ProductionCollection)
            {
                if (item.Left == left)
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 判定给定的产生式能否推导出ε（空）
        /// </summary>
        /// <param name="production">产生式</param>
        /// <returns></returns>
        public bool CanInferNull(ContextfreeProduction production)
        {
            foreach (var candidate in production.RightCollection)
            {
                if (this.CanInferNull(candidate))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 判定给定的候选式能否推导出ε（空）
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public bool CanInferNull(ProductionNodeList candidate)
        {
            foreach (var node in candidate)
            {
                switch (node.Position)
                {
                    case EnumProductionNodePosition.Leave:
                        return false;
                    //break;
                    case EnumProductionNodePosition.NonLeave:
                        var production = this.GetProduction(node);
                        if (!this.CanInferNull(production))
                            return false;
                        break;
                    //case EnumProductionNodePosition.My:
                    //    if (node.NodeName != ProductionNode.my_null.NodeName)
                    //        return false;
                    //    else
                    //        break;
                    case EnumProductionNodePosition.Unknown:
                        Console.WriteLine("文法中存在不合法的候选式{0}", candidate);
                        Console.ReadKey(true);
                        return false;
                    //break;
                    default:
                        return false;
                    //break;
                }
            }
            return false;
        }
        
        
    }
}
