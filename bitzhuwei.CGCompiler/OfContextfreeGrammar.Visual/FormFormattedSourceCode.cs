using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bitzhuwei.CGCompiler
{
    public partial class FormFormattedSourceCode : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceCode">源代码</param>
        public FormFormattedSourceCode(string sourceCode)
        {
            InitializeComponent();
            this.textBox1.Text = sourceCode;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.textBox1.Text);
            MessageBox.Show("源代码文本已经复制到剪贴板！", "提示");
        }
    }
}
