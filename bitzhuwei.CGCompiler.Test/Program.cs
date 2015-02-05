using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bitzhuwei.CGCompiler;

namespace bitzhuwei.CGCompiler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sourceCode =
            //    "<Start> ::= <Value> <RightOpt>;"
            //    +"<Value> ::= number | identifier;"
            //    +"<RightOpt> ::= <Opt> <Value> <RightOpt> | null;"
            //    +"<Opt> ::= \"Add\" | \"-\";";

            var sourceCode =
                "<Start>  ::= <Vn> \"::=\" <VList> \";\" <PList>;"
                + "<PList>  ::= <Vn> \"::=\" <VList> \";\" <PList> | null;"
                + "<VList>  ::= <V> <VOpt>;"
                + "<V>      ::= <Vn> | <Vt>;"
                + "<VOpt>   ::= <V> <VOpt> | \"|\" <V> <VOpt> | null;"
                + "<Vn>     ::= \"<\" identifier \">\";"
                + "<Vt>     ::= \"null\" | \"identifier\" | \"number\" | \"constString\" | constString;";

            var lex = new LexicalAnalyzerCG();
            lex.SetSourceCode(sourceCode);
            var tokens = lex.Analyze();
            Console.WriteLine(tokens);
            var parser = new LL1SyntaxParserCG();
            parser.SetTokenListSource(tokens);
            var tree = parser.Parse();
            Console.WriteLine(tree);
            var grammar = tree.GetGrammar();
            Console.WriteLine(grammar);
        }
    }
}
