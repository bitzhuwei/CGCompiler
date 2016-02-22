using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// ContextfreeGrammar的源代码控件
    /// </summary>
    public class SourceCodeCGTextBox : SourceCodeTextBox<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>, ISourceCodeVisable<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>
    {
        /// <summary>
        /// ContextfreeGrammar的源代码控件
        /// </summary>
        public SourceCodeCGTextBox()
        {
            this.SetLexicalAnalyzer(new LexicalAnalyzerCG());
            this.SetSyntaxParser(new LL1SyntaxParserCG());
        }
    }
}
