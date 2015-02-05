namespace bitzhuwei.CompilerBase
{
    partial class SyntaxTreeView<TEnumTokenType, TEnumVType, TTreeNodeValue>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SyntaxTreeView
            // 
            this.CheckBoxes = true;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HideSelection = false;
            this.Indent = 14;
            this.LineColor = System.Drawing.Color.Black;
            this.ShowNodeToolTips = true;
            this.ResumeLayout(false);

        }

        #endregion
    }
}
