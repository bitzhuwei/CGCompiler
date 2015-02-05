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
    /// <summary>
    /// 源代码窗口
    /// </summary>
    public partial class SourceCodeTextBoxForm : Form
    {
        /// <summary>
        /// 源代码窗口
        /// </summary>
        public SourceCodeTextBoxForm()
        {
            InitializeComponent();

            appIdleEvent = new EventHandler(this.Application_Idle);

        }
        /// <summary>
        /// 源代码窗口
        /// </summary>
        /// <param name="tokenListViewer">要绑定的单词列表控件</param>
        public SourceCodeTextBoxForm(ITokenListVisable<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG> tokenListViewer)
        {
            InitializeComponent();

            appIdleEvent = new EventHandler(this.Application_Idle);
            this.txtSourceCodeCG.AddTokenListViewer(tokenListViewer);
        }
        /// <summary>
        /// 源代码窗口
        /// </summary>
        /// <param name="tokenListViewerCollection">要绑定的单词列表控件</param>
        public SourceCodeTextBoxForm(IEnumerable<ITokenListVisable<EnumTokenTypeCG, EnumVTypeCG, TreeNodeValueCG>> tokenListViewerCollection)
        {
            InitializeComponent();

            appIdleEvent = new EventHandler(this.Application_Idle);
            foreach (var v in tokenListViewerCollection)
            {
                this.txtSourceCodeCG.AddTokenListViewer(v);
            }
        }

        private void SourceCodeTextBoxForm_Load(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Application_Idle);
        }

        void Application_Idle(object sender, EventArgs e)
        {
            this.lblSourceCodeCGInfo.Text = this.txtSourceCodeCG.ToString();
        }

        #region 属性和字段

        private EventHandler appIdleEvent;

        #endregion 属性和字段
    }
}
