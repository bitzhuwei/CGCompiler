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
        /// 获取此文法的LL1分析表
        /// <para>可能是非LL1文法的LL1分析表（即至少有一项包含多个推导式）</para>
        /// </summary>
        /// <returns></returns>
        public LL1ParserMap GetLL1ParserMap()
        {
            var firstCollection = this.GetFirstCollection();
            var followCollection = this.GetFollowCollection();

            var terminalList = this.GetTerminalList();
            //terminalList.Remove(ProductionNode.epsilonLeave);
            terminalList.Add(ProductionNode.startEndLeave);
            int line = followCollection.Count;// +terminalList.Count;
            int column = terminalList.Count;
            var result = new LL1ParserMap(line, column);
            for (int i = 0; i < line; i++)
            {
                result.SetLine(i, followCollection[i].ObjectiveProduction.Left);
            }
            for (int i = 0; i < column; i++)
            {
                result.SetColumn(i, terminalList[i]);
            }

            foreach (var firstCollectionItem in firstCollection)
            {
                var left = firstCollectionItem.ObjectiveProduction.Left;
                var candidate = firstCollectionItem.ObjectiveCandidate;
                foreach (var element in firstCollectionItem.Value)
                {
                    if (element == ProductionNode.tail_null)
                    {
                        foreach (var followCollectionItem in followCollection)
                        {
                            if (left == followCollectionItem.ObjectiveProduction.Left)
                            {
                                foreach (var next in followCollectionItem.Value)
                                {
                                    result.SetCell(left, next,
                                        new Derivation() { Left = left, Right = candidate });
                                }
                            }
                        }
                    }
                    else
                        result.SetCell(left, element,
                            new Derivation() { Left = left, Right = candidate });
                }
            }

            return result;
        }
    }
}
