using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bitzhuwei.CompilerBase
{
    /// <summary>
    /// 词法分析器选择器
    /// </summary>
    public partial class FormLexicalAnalyzerPicker : Form
    {
        /// <summary>
        /// 词法分析器选择器
        /// </summary>
        /// <param name="typeCollection"></param>
        public FormLexicalAnalyzerPicker(IList<Type> typeCollection)
        {
            InitializeComponent();
            foreach (var v in typeCollection)
            {
                this.lstAnalyzerTypes.Items.Add(v.FullName);
            }
        }

        /// <summary>
        /// 选中的类型全名
        /// </summary>
        public string SelectedTypeFullName
        {
            get
            {
                return this.lstAnalyzerTypes.SelectedItem.ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.lstAnalyzerTypes.SelectedItem == null)
            {
                MessageBox.Show("您尚未选中一个词法分析器类型", "提示");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lstAnalyzerTypes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lstAnalyzerTypes.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                DialogResult = DialogResult.OK;
            }
        }

    }
}
