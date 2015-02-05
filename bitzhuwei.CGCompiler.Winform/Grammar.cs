using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler.Winform
{
    class Grammar
    {
        public const string strGrammar = "Grammar";

        private const string strGrammarName = "GrammarName";
        private const string strContent = "Content";

        private const string strNamespace = "namespace";
        private const string strCodeFolder = "CodeFolder";
        private const string strCompilerName = "CompilerName";

        public string Namespace { get; set; }

        public string CodeFolder { get; set; }

        public string CompilerName { get; set; }

        public string GrammarName { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return this.GrammarName;
            //return base.ToString();
        }

        public XElement ToXElement()
        {
            var result = new XElement(strGrammar,
                new XElement(strCompilerName, CompilerName),
                new XElement(strCodeFolder, CodeFolder),
                new XElement(strNamespace, Namespace),
                new XElement(strGrammarName, GrammarName),
                new XElement(strContent, Content)
                );
            return result;
        }

        public static Grammar Parse(XElement xml)
        {
            if (xml == null || xml.Name != strGrammar) return null;
            var result = new Grammar();
            result.CompilerName = xml.Element(strCompilerName).Value;
            result.CodeFolder = xml.Element(strCodeFolder).Value;
            result.Namespace = xml.Element(strNamespace).Value;
            result.GrammarName = xml.Element(strGrammarName).Value;
            result.Content = xml.Element(strContent).Value.Replace("\n", Environment.NewLine);
            return result;
        }
    }
}
