namespace bitzhuwei.CompilerBase
{
    partial class SourceCodeTextBox<TEnumTokenType, TEnumVType, TTreeNodeValue>
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
            // SourceCodeTextBox
            // 
            this.AcceptsReturn = true;
            this.AcceptsTab = true;
            this.AllowDrop = true;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HideSelection = false;
            this.MaxLength = 2147483647;
            this.Multiline = true;
            this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.WordWrap = false;
            this.ResumeLayout(false);

        }

        #endregion
    }
}
