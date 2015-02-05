using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    step
    {
        tank{1 2} //{<TankPrefab> <BornPoint>}
    }
    step
    {
        tank{3 4} tank{5 6}
    }
}
"
            };
            foreach (var sourceCode in sourceCodes)
            {
                var lex = new bitzhuwei.LevelCompiler.LexicalAnalyzerLevelCompiler();
                lex.SetSourceCode(sourceCode);
                var tokens = lex.Analyze();
                Console.WriteLine(tokens);
                var parser = new bitzhuwei.LevelCompiler.LL1SyntaxParserLevelCompiler();
                parser.SetTokenListSource(tokens);
                var tree = parser.Parse();
                Console.WriteLine(tree);
                var value = tree.GetValue();
                Console.WriteLine(value);
            }
        }
    }
}
