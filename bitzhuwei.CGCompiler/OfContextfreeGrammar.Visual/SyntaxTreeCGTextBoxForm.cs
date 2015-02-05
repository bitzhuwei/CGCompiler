using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using bitzhuwei.CompilerBase;

namespace bitzhuwei.CGCompiler
{
    public partial class SyntaxTreeCGTextBoxForm : Form
    {
        /// <summary>
        /// 语法树可视化控件
        /// </summary>
        public SyntaxTreeCGTextBoxForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 语法树可视化控件
        /// </summary>
        /// <param name="sourceCodeViewer">源代码控件</param>
        public SyntaxTreeCGTextBoxForm(ISourceCodeVisable<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> sourceCodeViewer)
        {
            InitializeComponent();

            this.txtSyntaxTreeCG.SetSourceCodeViewer(sourceCodeViewer);
            //(this.trvSyntaxTree as ISyntaxTreeVisable<EnumTokenTypeCG, EnumVTypeCG>).SetSourceCodeViewer(sourceCodeViewer);

            Application.Idle += new EventHandler(this.Application_Idle);
        }
        void Application_Idle(object sender, EventArgs e)
        {
            this.lblSyntaxTreeViewInfo.Text = this.txtSyntaxTreeCG.ToString();
        }

        private void SyntaxTreeViewForm_Load(object sender, EventArgs e)
        {
            this.txtSyntaxTreeCG.SyntaxTreeUpdated();
            //(this.trvSyntaxTree as ISyntaxTreeVisable<EnumTokenTypeCG, EnumVTypeCG>).SyntaxTreeUpdated();
        }

        private void lblSyntaxTreeViewInfo_Click(object sender, EventArgs e)
        {
            var sourceViewer = this.txtSyntaxTreeCG.GetSourceCodeViewer();
            (new FormFormattedSourceCode(
                sourceViewer.GetOutputSyntaxTree().GetFormatted())
                )
                .Show();
        }
    }
}
