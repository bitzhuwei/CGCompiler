using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// 源代码控件
    /// </summary>
    /// <typeparam name="TEnumTokenType">单词的枚举类型</typeparam>
    /// <typeparam name="TEnumVType">语法分析中的结点类型(某Vn or 某Vt)，建议使用枚举类型</typeparam>
    /// <typeparam name="TTreeNodeValue">语法树结点值，根据语音特性自定义类型进行填充</typeparam>
    public interface ISourceCodeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue>
        where TEnumTokenType : struct, IComparable, IFormattable, IConvertible
        where TEnumVType : struct, IComparable, IFormattable, IConvertible
        where TTreeNodeValue : class, ICloneable, new()
    {
        /// <summary>
        /// 获取源代码
        /// </summary>
        /// <returns></returns>
        string GetSourceCode();
        /// <summary>
        /// 设置源代码
        /// </summary>
        void SetSourceCode(string value);
        /// <summary>
        /// 获取词法分析器
        /// </summary>
        /// <returns></returns>
        ILexicalAnalyzer<TEnumTokenType> GetLexicalAnalyzer();
        /// <summary>
        /// 设置词法分析器
        /// </summary>
        /// <param name="value"></param>
        void SetLexicalAnalyzer(ILexicalAnalyzer<TEnumTokenType> value);
        /// <summary>
        /// 获取词法分析得到的单词列表
        /// <para>ITokenListVisable根据此列表显示单词列表</para>
        /// </summary>
        /// <returns></returns>
        TokenList<TEnumTokenType> GetOutputTokenList();
        /// <summary>
        /// 设置词法分析得到的单词列表
        /// <para>ITokenListVisable根据此列表显示单词列表</para>
        /// </summary>
        /// <param name="value"></param>
        void SetOutputTokenList(TokenList<TEnumTokenType> value);
        /// <summary>
        /// 获取语法分析器
        /// </summary>
        /// <returns></returns>
        ISyntaxParser<TEnumTokenType, TEnumVType, TTreeNodeValue> GetSyntaxParser();
        /// <summary>
        /// 设置语法分析器
        /// </summary>
        /// <param name="value"></param>
        void SetSyntaxParser(ISyntaxParser<TEnumTokenType, TEnumVType, TTreeNodeValue> value);
        /// <summary>
        /// 获取语法分析得到的语法树
        /// <para>ISyntaxTreeVisable根据此列表显示语法树</para>
        /// </summary>
        /// <returns></returns>
        SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> GetOutputSyntaxTree();
        /// <summary>
        /// 设置语法分析得到的语法树
        /// <para>ISyntaxTreeVisable根据此列表显示语法树</para>
        /// </summary>
        /// <param name="value"></param>
        void SetOutputSyntaxTree(SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> value);
        /// <summary>
        /// 通知源代码显示控件，高亮给定位置
        /// </summary>
        /// <param name="start">要高亮显示的第一个字符的索引</param>
        /// <param name="length">要高亮的字符串长度</param>
        void NotifyToHighlight(int start, int length);
        /// <summary>
        /// 添加一个单词列表显示器
        /// </summary>
        /// <param name="tokenListViewer"></param>
        void AddTokenListViewer(ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer);
        /// <summary>
        /// 移除一个单词列表显示器
        /// </summary>
        /// <param name="tokenListViewer"></param>
        void RemoveTokenListViewer(ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer);
        /// <summary>
        /// 判断是否包含某单词列表显示器
        /// </summary>
        /// <param name="tokenListViewer"></param>
        /// <returns></returns>
        bool Contains(ITokenListVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer);
        /// <summary>
        /// 添加一个语法树显示器
        /// </summary>
        /// <param name="tokenListViewer"></param>
        void AddSyntaxTreeViewer(ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer);
        /// <summary>
        /// 移除一个语法树显示器
        /// </summary>
        /// <param name="tokenListViewer"></param>
        void RemoveSyntaxTreeViewer(ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer);
        /// <summary>
        /// 判断是否包含某语法树显示器
        /// </summary>
        /// <param name="tokenListViewer"></param>
        /// <returns></returns>
        bool Contains(ISyntaxTreeVisable<TEnumTokenType, TEnumVType, TTreeNodeValue> tokenListViewer);

    }
}
