using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;

namespace bitzhuwei.CGCompiler.Winform
{
    public partial class FormInsertNewGrammar : Form
    {
        class CodeGeneratorParam
        {
            public string sourceCode;
            public string compilerName;
            public string folder;
            public string _namespace;
            public CodeGeneratorParam(string sourceCode, string compilerName, string _namespace, string folder)
            {
                this.sourceCode = sourceCode;
                this.compilerName = compilerName;
                this._namespace = _namespace;
                this.folder = folder;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public FormInsertNewGrammar()
        //{
        //    InitializeComponent();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="existedGramars"></param>
        internal FormInsertNewGrammar(IEnumerable<Grammar> existedGramars)
        {
            InitializeComponent();
            //this.txtSourceCode.Text = sourceCode;
            this.existedGramars = existedGramars;
        }

        private void btnBrowseFolder_Click(object sender, EventArgs e)
        {
            if (generatedCodeFolderBrowser.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                txtCodeFolder.Text = generatedCodeFolderBrowser.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGrammarName.Text))
            {
                MessageBox.Show("文法名称不要不填哦亲！", "提示");
                return;
            } 
            if (string.IsNullOrEmpty(txtNamespace.Text))
            {
                MessageBox.Show("命名空间不要不填哦亲！", "提示");
                return;
            }
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (txtNamespace.Text.Contains(c))
                {
                    MessageBox.Show("命名空间不要包含字符【" + c + "】哦亲！", "提示");
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtCompilerName.Text))
            {
                MessageBox.Show("编译器名不要不填哦亲！", "提示");
                return;
            }
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                if (txtCompilerName.Text.Contains(c))
                {
                    MessageBox.Show("编译器名不要包含字符【" + c + "】哦亲！", "提示");
                    return;
                }
            }
            if (txtCompilerName.Text.Contains('.'))
            {
                MessageBox.Show("编译器名不要包含字符【" + "." + "】哦亲！", "提示");
                return;
            }
            if (string.IsNullOrEmpty(txtCodeFolder.Text))
            {
                MessageBox.Show("生成代码存放的位置不要不选哦亲！", "提示");
                return;
            }
            if (!Directory.Exists(txtCodeFolder.Text))
            {
                MessageBox.Show("您选择的文件夹不存在哦亲！","提示");
                return;
            }

            foreach (var item in this.existedGramars)
            {
                if (item.GrammarName == this.txtGrammarName.Text)
                {
                    MessageBox.Show("您设置的文法名以存在，请使用一个新名字吧！");
                    return;
                }
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            //this.m_config.Save(configFilename);
        }

        private IEnumerable<Grammar> existedGramars;
        
        private void fullname_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(txtCodeFolder.Text)
                && IsValidNamespaceName(txtNamespace.Text)
                && IsValidCompilerName(txtCompilerName.Text))
            {
                this.btnOK.Enabled = true;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private static bool IsValidNamespaceName(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName)) return false;
            if (namespaceName.StartsWith(" ") || namespaceName.EndsWith(" ")) return false;
            bool isValid = true;
            var parts = namespaceName.Split('.');
            foreach (var space in parts)
            {
                if (!IsValidIdentifier(space))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool IsValidCompilerName(string compilerName)
        {
            return IsValidIdentifier(compilerName);
        }

        private static bool IsValidIdentifier(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value.StartsWith(" ") || value.EndsWith(" ")) return false;
            var head = value[0];
            if ((!('a' <= head && head <= 'z')) && (!('A' <= head && head <= 'Z'))
                && (!(head != '_')))
                return false;
            var isValid = true;
            foreach (var item in value)
            {
                if ((!('a' <= item && item <= 'z')) && (!('A' <= item && item <= 'Z'))
                    && (!('0' <= item && item <= '9')))
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        internal Grammar GetGrammar()
        {
            var result = new Grammar()
            {
                Namespace = this.txtNamespace.Text,
                CompilerName = this.txtCompilerName.Text,
                Content = this.txtSourceCode.Text,
                GrammarName = this.txtGrammarName.Text,
                CodeFolder = this.txtCodeFolder.Text
            };
            return result;
        }

    }
}
