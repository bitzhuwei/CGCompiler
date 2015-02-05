using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.LevelCompiler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceCodes = new string[]
            {
@"
level
{
    
     tank{1 2} | tank{3 4} 
}
"
            };
            foreach (var sourceCode in sourceCodes)
            {
                var lex = new LexicalAnalyzerLevelCompiler();
                lex.SetSourceCode(sourceCode);
                var tokens = lex.Analyze();
                Console.WriteLine(tokens);
                var parser = new LL1SyntaxParserLevelCompiler();
                parser.SetTokenListSource(tokens);
                var tree = parser.Parse();
                Console.WriteLine(tree);
                var value = tree.GetValue();
                Console.WriteLine(value);
            }
        }
    }
}
