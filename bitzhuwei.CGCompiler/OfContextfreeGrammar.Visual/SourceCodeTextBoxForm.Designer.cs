namespace bitzhuwei.CGCompiler
{
    partial class SourceCodeTextBoxForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceCodeTextBoxForm));
            this.statusSourceCodeCG = new System.Windows.Forms.StatusStrip();
            this.lblSourceCodeCGInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtSourceCodeCG = new SourceCodeCGTextBox();
            this.statusSourceCodeCG.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusSourceCodeCG
            // 
            this.statusSourceCodeCG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSourceCodeCGInfo});
            this.statusSourceCodeCG.Location = new System.Drawing.Point(0, 334);
            this.statusSourceCodeCG.Name = "statusSourceCodeCG";
            this.statusSourceCodeCG.Size = new System.Drawing.Size(469, 22);
            this.statusSourceCodeCG.TabIndex = 0;
            this.statusSourceCodeCG.Text = "statusStrip1";
            // 
            // lblSourceCodeCGInfo
            // 
            this.lblSourceCodeCGInfo.Name = "lblSourceCodeCGInfo";
            this.lblSourceCodeCGInfo.Size = new System.Drawing.Size(128, 17);
            this.lblSourceCodeCGInfo.Text = "源代码和词法分析信息";
            // 
            // txtSourceCodeCG
            // 
            this.txtSourceCodeCG.AcceptsReturn = true;
            this.txtSourceCodeCG.AcceptsTab = true;
            this.txtSourceCodeCG.AllowDrop = true;
            this.txtSourceCodeCG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSourceCodeCG.HideSelection = false;
            this.txtSourceCodeCG.Location = new System.Drawing.Point(0, 0);
            this.txtSourceCodeCG.MaxLength = 2147483647;
            this.txtSourceCodeCG.Multiline = true;
            this.txtSourceCodeCG.Name = "txtSourceCodeCG";
            this.txtSourceCodeCG.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSourceCodeCG.Size = new System.Drawing.Size(469, 334);
            this.txtSourceCodeCG.TabIndex = 1;
            this.txtSourceCodeCG.WordWrap = false;
            // 
            // SourceCodeTextBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 356);
            this.Controls.Add(this.txtSourceCodeCG);
            this.Controls.Add(this.statusSourceCodeCG);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SourceCodeTextBoxForm";
            this.Text = "SourceCodeTextBoxForm";
            this.Load += new System.EventHandler(this.SourceCodeTextBoxForm_Load);
            this.statusSourceCodeCG.ResumeLayout(false);
            this.statusSourceCodeCG.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusSourceCodeCG;
        private System.Windows.Forms.ToolStripStatusLabel lblSourceCodeCGInfo;
        private SourceCodeCGTextBox txtSourceCodeCG;
    }
}