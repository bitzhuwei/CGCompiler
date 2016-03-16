using System;
using System.Collections.Generic;


namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// LL1语法分析器基类
    /// </summary>
    /// <typeparam name="TEnumTokenType">单词的枚举类型</typeparam>
    /// <typeparam name="TEnumVType">语法分析中的结点类型(某Vn or 某Vt)，建议使用枚举类型</typeparam>
    /// <typeparam name="TTreeNodeValue">语法树结点值，根据语音特性自定义类型进行填充</typeparam>
    public abstract partial class LR1SyntaxParserBase<TEnumTokenType, TEnumVType, TTreeNodeValue> 
        : ISyntaxParser<TEnumTokenType, TEnumVType, TTreeNodeValue>
        where TEnumVType : struct, IComparable, IConvertible, IFormattable
        where TEnumTokenType : struct, IComparable, IFormattable, IConvertible
        where TTreeNodeValue : class, ICloneable, new()
    {

        public TokenList<TEnumTokenType> GetTokenListSource()
        {
            throw new NotImplementedException();
        }

        public void SetTokenListSource(TokenList<TEnumTokenType> value)
        {
            throw new NotImplementedException();
        }

        public SyntaxTree<TEnumTokenType, TEnumVType, TTreeNodeValue> Parse()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
