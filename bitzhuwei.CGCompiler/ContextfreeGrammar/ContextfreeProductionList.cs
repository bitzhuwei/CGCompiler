using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 上下文无关产生式列表（文法的主要数据结构）
    /// </summary>
    public class ContextfreeProductionList : List<ContextfreeProduction>,ICloneable
    {
        /// <summary>
        /// 判定两个产生式列表（文法的主要数据结构）是否相同
        /// </summary>
        /// <param name="obj">被比较的对象</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var another = obj as ContextfreeProductionList;
            if (another == null) return false;
            if (this.Count != another.Count) return false;
            for (int i = 0; i < this.Count; i++)
            {
                if (!this[i].Equals(another))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator ==(ContextfreeProductionList a, ContextfreeProductionList b)
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
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 比较两个候选式是否相同
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns></returns>
        public static bool operator !=(ContextfreeProductionList a, ContextfreeProductionList b)
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
        /// 输出各个产生式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var v in this)
            {
                result.Append(v.ToString());
                result.AppendLine();
            }
            return result.ToString();
        }
        /// <summary>
        /// 输出各个产生式
        /// </summary>
        /// <param name="htmlFormat">是否使用HTML或XML格式输出</param>
        /// <returns></returns>
        public string ToString(bool htmlFormat)
        {
            StringBuilder result = new StringBuilder();
            foreach (var v in this)
            {
                result.Append(v.ToString(htmlFormat));
                result.AppendLine();
            }
            return result.ToString();
        }
        /// <summary>
        /// From 
        /// </summary>
        /// <param name="xContextfreeProductionList"></param>
        /// <returns></returns>
        public static ContextfreeProductionList From(XElement xContextfreeProductionList)
        {
            if (xContextfreeProductionList == null) return null;
            if (xContextfreeProductionList.Name != strContextfreeProductionList) return null;
            var result = new ContextfreeProductionList();
            result.AddRange(
                from item in xContextfreeProductionList.Elements(ContextfreeProduction.strContextfreeProduction)
                select ContextfreeProduction.From(item)
                );
            return result;
        }
        /// <summary>
        /// ToXElement
        /// </summary>
        /// <returns></returns>
        public XElement ToXElement()
        {
            var result = new XElement(strContextfreeProductionList,
                new XAttribute("Count", this.Count),
                from item in this
                select item.ToXElement());
            return result;
        }
        /// <summary>
        /// public const string strContextfreeProductionList = "ContextfreeProductionList";
        /// </summary>       
        public const string strContextfreeProductionList = "ContextfreeProductionList";
        /// <summary>
        /// 获取此上下文无关产生式列表的复制品
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var result = new ContextfreeProductionList();
            result.AddRange(
                from item in this
                select (item.Clone() as ContextfreeProduction));
            return result;
        }
    }
}
