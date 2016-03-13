using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGL.GLSL430Compiler.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] vertFiles = System.IO.Directory.GetFiles(".", "*.vert", System.IO.SearchOption.AllDirectories);
            string[] geomFiles = System.IO.Directory.GetFiles(".", "*.geom", System.IO.SearchOption.AllDirectories);
            string[] fragFiles = System.IO.Directory.GetFiles(".", "*.frag", System.IO.SearchOption.AllDirectories);

            var filenames = vertFiles.Union(geomFiles).Union(fragFiles);
            foreach (var filename in filenames)
            {
                var originalOut = Console.Out;
                File.Delete(filename + ".txt");

                var writer = new StreamWriter(filename + ".txt");
                Console.SetOut(writer);
                var sourceCode = System.IO.File.ReadAllText(filename);
                var lex = new LexicalAnalyzerGLSL430();
                lex.SetSourceCode(sourceCode);
                var tokens = lex.Analyze();
                Console.WriteLine(tokens);
                var parser = new LL1SyntaxParserGLSL430();
                parser.SetTokenListSource(tokens);
                var tree = parser.Parse();
                Console.WriteLine(tree);
                //var value = tree.GetValue();
                //Console.WriteLine(value);
                writer.Flush();
                writer.Close();
            }
        }
    }
}
