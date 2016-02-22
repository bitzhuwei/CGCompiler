using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// 在可视化控件中显示词法分析得到的单词列表
    /// </summary>
    /// <typeparam name="TEnumTokenType">单词的枚举类型</typeparam>
    /// <typeparam name="TEnumVType">语法分析中的结点类型(某Vn or 某Vt)，建议使用枚举类型</typeparam>
    /// <typeparam name="TTreeNodeValue">语法树结点值，根据语音特性自定义类型进行填充</typeparam>
    public interface ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>
        where TEnumTokenType : struct, IComparable, IFormattable, IConvertible
        where TEnumVType : struct, IComparable, IFormattable, IConvertible
        where TTreeNodeValue : class, ICloneable, new()
    {

        /// <summary>
        /// 获取所监视的源代码显示控件
        /// </summary>
        ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> GetSourceCodeViewer();
        /// <summary>
        /// 设置所监视的源代码显示控件
        /// </summary>
        void SetSourceCodeViewer(ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> sourceCodeViewer);
        ///// <summary>
        ///// 要显示到控件中的TokenList源
        ///// </summary>
        //TokenList TokenListSource{get;set;}
        /// <summary>
        /// 通知此控件TokenList已更新，需重新显示到控件
        /// </summary>
        void TokenListUpdated();
        /// <summary>
        /// 通知TokenList
        /// </summary>
        /// <param name="tokenIndex">要高亮的单词索引</param>
        /// <param name="indexAtSpace">true表示选中了源代码中的空白处</param>
        void NotifyToHighlight(int tokenIndex, bool indexAtSpace);
    }
}
