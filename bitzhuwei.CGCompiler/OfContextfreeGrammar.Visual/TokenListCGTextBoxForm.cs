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
    public partial class TokenListCGTextBoxForm : Form
    {
        /// <summary>
        /// 单词列表窗口
        /// </summary>
        public TokenListCGTextBoxForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 单词列表窗口
        /// </summary>
        /// <param name="sourceCodeViewer"></param>
        public TokenListCGTextBoxForm(ISourceCodeVisable<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> sourceCodeViewer)
        {
            InitializeComponent();

            this.txtTokenList.SetSourceCodeViewer(sourceCodeViewer);
            
            Application.Idle += new EventHandler(this.Application_Idle);
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="e"></param>
        //protected override void OnClosing(CancelEventArgs e)
        //{
        //    //ISourceCodeVisable sourceCodeViewer = (this.txtTokenList as ITokenListVisable).SourceCodeViewer;
        //    //if (sourceCodeViewer != null)
        //    //    sourceCodeViewer.RemoveTokenListViewer(this.txtTokenList);
        //    base.OnClosing(e);
        //}

        void Application_Idle(object sender, EventArgs e)
        {
            this.lblTokenListInfo.Text = this.txtTokenList.ToString();
        }


        private void TokenListTextBoxForm_Load(object sender, EventArgs e)
        {
            (this.txtTokenList as ITokenListVisable<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>).TokenListUpdated();
        }
    }
}
