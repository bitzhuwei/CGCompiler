using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler.Winform
{
    internal class Config
    {
        public void Save(string filename)
        {
            if (string.IsNullOrEmpty(filename)) return;
            if (!filename.Contains(".")) filename += ".xml";
            this.ToXElement().Save(filename);
        }


        public XElement ToXElement()
        {
            var result = new XElement(strConfig,

                this.Grammars.ToXElement());
            return result;
        }

        public static Config Parse(XElement xml)
        {
            if (xml == null || xml.Name != strConfig) return null;
            var result = new Config();
            result.Grammars = GrammarList.Parse(xml.Element(GrammarList.strGrammarList));
            return result;
        }

        public const string strConfig = "Config";

        public GrammarList Grammars
        {
            get { return m_grammars; }
            set { m_grammars = value; }
        }

        private GrammarList m_grammars = new GrammarList();


    }
}