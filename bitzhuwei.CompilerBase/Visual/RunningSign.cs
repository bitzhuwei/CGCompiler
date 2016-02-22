using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// 运行标记
    /// <para>每次获取运行标记，其将自动翻转到下一个状态</para>
    /// </summary>
    class RunningSign
    {
        /// <summary>
        /// 获取运行标记
        /// </summary>
        public string Sign
        {
            get
            {
                if (m_Sign == "-")
                {
                    m_Sign = "\\";
                    return "-";
                }
                if (m_Sign == "\\")
                {
                    m_Sign = "|";
                    return "\\";
                }
                if (m_Sign == "|")
                {
                    m_Sign = "/";
                    return "|";
                }
                if (m_Sign == "/")
                {
                    m_Sign = "-";
                    return "/";
                }
                return "Wrong Sign!";
            }
            private set
            {
                m_Sign = value;
            }
        }
        private string m_Sign = "-";
    }
}
