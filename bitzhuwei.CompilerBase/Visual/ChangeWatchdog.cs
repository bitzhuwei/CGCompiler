using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// 源代码改变而未分析的次数同步平台
    /// </summary>
    class ChangeWatchdog
    {
        public override string ToString()
        {
            return string.Format("not dealt count: {0}", NotDealtCount());
            //return base.ToString();
        }
        /// <summary>
        /// 改变次数加1
        /// </summary>
        public void Increase()
        {
            lock (m_SynObj)
            {
                m_ChangedCount++;
                m_TotalChangedCount++;
            }
        }
        /// <summary>
        /// 覆盖掉分析过的改变
        /// </summary>
        /// <param name="count">分析过的改变次数</param>
        public void Decrease(int count)
        {
            lock (m_SynObj)
            {
                if (m_ChangedCount<count)
                {
                    throw new Exception("刚刚分析后覆盖的改变次数超过了源代码改变而尚未分析的次数！");
                }
                m_ChangedCount -= count;
                m_DealtCount++;
            }
        }
        /// <summary>
        /// 发生改变而尚未处理的次数
        /// </summary>
        /// <returns></returns>
        public int NotDealtCount()
        {
            int c = 0;
            lock (m_SynObj)
            {
                c = m_ChangedCount;
            }
            return c;
        }
        /// <summary>
        /// 发生改变的次数总计
        /// </summary>
        /// <returns></returns>
        public int TotalChangedCount()
        {
            int c = 0;
            lock (m_SynObj)
            {
                c = m_TotalChangedCount;
            }
            return c;
        }
        /// <summary>
        /// 处理过的次数总计
        /// </summary>
        /// <returns></returns>
        public int DealtCount()
        {
            int c = 0;
            lock (m_SynObj)
            {
                c = m_DealtCount;
            }
            return c;
        }

        /// <summary>
        /// 源代码改变而尚未分析的次数
        /// </summary>
        private int m_ChangedCount = 0;
        private object m_SynObj = new object();

        private int m_TotalChangedCount = 0;
        private int m_DealtCount = 0;
    }
}
