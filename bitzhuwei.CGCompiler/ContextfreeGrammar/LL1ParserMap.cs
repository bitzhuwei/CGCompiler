using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// LL1分析表
    /// </summary>
    public class LL1ParserMap
    {
        /// <summary>
        /// 获取行数
        /// </summary>
        /// <returns></returns>
        public int GetLineCount()
        {
            return this.m_LineCount;
        }
        /// <summary>
        /// 获取列数
        /// </summary>
        /// <returns></returns>
        public int GetColumnCount()
        {
            return this.m_ColumnCount;
        }
        /// <summary>
        /// 分析表
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        public LL1ParserMap(int line, int column)
        {
            //this.m_ParserMap = new DerivationList[line, column];
            //for (int i = 0; i < line; i++)
            //{
            //    for (int j = 0; j < column; j++)
            //    {
            //        this.m_ParserMap[i, j] = new DerivationList();
            //    }
            //}
            //this.m_LeftNodes = new Dictionary<ProductionNode, int>();
            //this.m_NextLeaves = new Dictionary<ProductionNode, int>();
            this.m_LineCount = line;
            this.m_ColumnCount = column;
        }
        /// <summary>
        /// 设置某行的结点类型
        /// </summary>
        /// <param name="line"></param>
        /// <param name="leftNode"></param>
        public void SetLine(int line, ProductionNode leftNode)
        {
            //if (0 <= line && line < this.m_LineCount)
            //    this.m_LeftNodes.Add(leftNode, line);
        }
        /// <summary>
        /// 设置某列的结点类型
        /// </summary>
        /// <param name="column"></param>
        /// <param name="nextLeave"></param>
        public void SetColumn(int column, ProductionNode nextLeave)
        {
            //if (0 <= column && column < this.m_ColumnCount)
            //    this.m_NextLeaves.Add(nextLeave, column);
        }
        ///// <summary>
        ///// 设置给定行、列位置的推导式
        ///// </summary>
        ///// <param name="line"></param>
        ///// <param name="column"></param>
        ///// <param name="derivation"></param>
        //public void SetCell(int line, int column, Derivation derivation)
        //{
        //    if (0 <= line && line < this.m_LineCount
        //        && 0 <= column && column < this.m_ColumnCount)
        //    {
        //        //this.m_ParserMap[line, column].Add(derivation);
        //        string key = string.Format("{0}-{1}",)
        //    }
        //}
        /// <summary>
        /// 设置给定语法类型、单词类型所对应的推导式
        /// </summary>
        /// <param name="leftNode"></param>
        /// <param name="nextLeave"></param>
        /// <param name="function"></param>
        public void SetCell(ProductionNode leftNode, ProductionNode nextLeave, Derivation function)
        {
            //SetCell(this.m_LeftNodes[leftNode], this.m_NextLeaves[nextLeave], function);
            string key = string.Format("Left:{0} Terminal:{1}", leftNode, nextLeave);
            DerivationList list = null;
            if (this.m_ParserMap.TryGetValue(key, out list))
            {
                list.Add(function);
            }
            else
            {
                list = new DerivationList();
                list.Add(function);
                this.m_ParserMap.Add(key, list);
            }
        }

        /// <summary>
        /// 获取推导式列表
        /// </summary>
        /// <param name="leftNode">当前结非终点类型</param>
        /// <param name="nextLeave">要处理的终结点类型</param>
        /// <returns></returns>
        public DerivationList GetDerivationList(ProductionNode leftNode, ProductionNode nextLeave)
        {
            //#if DEBUG
            //            if (this.m_LeftNodes.ContainsKey(leftNode)
            //                && this.m_NextLeaves.ContainsKey(nextLeave))
            //#endif
            //                return this.GetDerivationList(this.m_LeftNodes[leftNode], this.m_NextLeaves[nextLeave]);
            //#if DEBUG
            //            else
            //                return null;
            //#endif
            //                return this.GetDerivationList(this.m_LeftNodes[leftNode], this.m_NextLeaves[nextLeave]);
            string key = string.Format("Left:{0} Terminal:{1}", leftNode, nextLeave);
            DerivationList list = null;
            if (this.m_ParserMap.TryGetValue(key, out list))
            {
                return this.m_ParserMap[key];
            }
            else
            {
                return new DerivationList();
            }
        }
        //        /// <summary>
        //        /// 获取推导式
        //        /// </summary>
        //        /// <param name="line">行数</param>
        //        /// <param name="column">列数</param>
        //        /// <returns></returns>
        //        public DerivationList GetDerivationList(int line, int column)
        //        {
        //#if DEBUG
        //            if (0 <= line && line < this.m_LineCount
        //                && 0 <= column && column < this.m_ColumnCount)
        //#endif
        //                return this.m_ParserMap[line, column];
        //#if DEBUG
        //            else
        //                return null;
        //#endif
        //        }
        /// <summary>
        /// 分析表
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var item in this.m_ParserMap)
            {
                builder.Append(item.Key);
                DerivationList derivationList = item.Value;
                if (derivationList.Count == 0) { builder.AppendLine("    no derivation"); }
                else
                {
                    builder.AppendLine();
                    foreach (var derivation in derivationList)
                    {
                        builder.AppendLine("    " + derivation.ToString());
                    }
                }
            }

            return builder.ToString();
            //return base.ToString();
        }

        private int m_LineCount;
        private int m_ColumnCount;
        //private DerivationList[,] m_ParserMap = null;
        private Dictionary<string, DerivationList> m_ParserMap = new Dictionary<string, DerivationList>();
        //private Dictionary<ProductionNode, int> m_LeftNodes = null;
        //private Dictionary<ProductionNode, int> m_NextLeaves = null;
    }

}
