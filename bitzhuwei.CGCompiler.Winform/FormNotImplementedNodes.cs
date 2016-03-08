using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bitzhuwei.CGCompiler.Winform
{
    public partial class FormNotImplementedNodes : Form
    {
        private List<ProductionNode> notImplementedNodeList;
        public FormNotImplementedNodes(List<ProductionNode> notImplementedNodeList)
        {
            InitializeComponent();

            this.notImplementedNodeList = notImplementedNodeList;
        }

        private void FormNotImplementedNodes_Load(object sender, EventArgs e)
        {
            var list = this.notImplementedNodeList;
            if (list == null) { return; }

            StringBuilder builder = new StringBuilder();
            foreach (var node in list)
            {
                builder.Append('<');
                builder.Append(node.NodeName);
                builder.Append('>');
                builder.Append(" ::= ");
                builder.Append("\"");
                builder.Append("tmp_" + node.NodeName);
                builder.Append("\"");
                builder.Append(" ;");
                builder.AppendLine();
            }

            this.NotimplementedNodeListCode = this.txtContent.Text;
            this.txtContent.Text = builder.ToString();
        }



        public string NotimplementedNodeListCode { get; set; }

        private void btnAddToGrammar_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
