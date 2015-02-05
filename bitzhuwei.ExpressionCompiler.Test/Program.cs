using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bitzhuwei.ExpressionCompiler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceCodes = new string[]
            {
                "37",
                "19 * 19 - 18 * 18",
                "(19 + 18) * (19 - 18)",
            };
            foreach (var sourceCode in sourceCodes)
            {
                var lex = new bitzhuwei.ExpressionCompiler.LexicalAnalyzerExpression();
                lex.SetSourceCode(sourceCode);
                var tokens = lex.Analyze();
                Console.WriteLine(tokens);
                var parser = new bitzhuwei.ExpressionCompiler.LL1SyntaxParserExpression();
                parser.SetTokenListSource(tokens);
                var tree = parser.Parse();
                Console.WriteLine(tree);
                var value = tree.GetValue();
                Console.WriteLine(value);
            }
        }
    }
}
