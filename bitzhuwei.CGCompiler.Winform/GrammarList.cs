using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler.Winform
{
    class GrammarList : List<Grammar>
    {
        private int m_SelectedIndex = -1;

        public int SelectedIndex
        {
            get { return m_SelectedIndex; }
            set { m_SelectedIndex = value; }
        }

        public const string strGrammarList = "GrammarList";
        private const string strSelectedIndex = "SelectedIndex";

        public XElement ToXElement()
        {
            var result = new XElement(strGrammarList,
                new XAttribute("Count", this.Count),
                new XAttribute(strSelectedIndex, SelectedIndex),
                from item in this
                select item.ToXElement());
            return result;
        }

        public static GrammarList Parse(XElement xml)
        {
            if (xml == null || xml.Name != strGrammarList) return null;
            var result = new GrammarList();
            foreach (var item in xml.Elements(Grammar.strGrammar))
            {
                result.Add(Grammar.Parse(item));
            }
            result.SelectedIndex = int.Parse(xml.Attribute(strSelectedIndex).Value);
            return result;
        }
    }
}
