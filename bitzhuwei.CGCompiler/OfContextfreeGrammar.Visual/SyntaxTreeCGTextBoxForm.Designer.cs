namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 
    /// </summary>
    partial class SyntaxTreeCGTextBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyntaxTreeCGTextBoxForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblSyntaxTreeViewInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtSyntaxTreeCG = new bitzhuwei.CGCompiler.SyntaxTreeCGTextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSyntaxTreeViewInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblSyntaxTreeViewInfo
            // 
            this.lblSyntaxTreeViewInfo.Name = "lblSyntaxTreeViewInfo";
            this.lblSyntaxTreeViewInfo.Size = new System.Drawing.Size(68, 17);
            this.lblSyntaxTreeViewInfo.Text = "语法树信息";
            this.lblSyntaxTreeViewInfo.Click += new System.EventHandler(this.lblSyntaxTreeViewInfo_Click);
            // 
            // txtSyntaxTreeCG
            // 
            this.txtSyntaxTreeCG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSyntaxTreeCG.HideSelection = false;
            this.txtSyntaxTreeCG.Location = new System.Drawing.Point(0, 0);
            this.txtSyntaxTreeCG.MaxLength = 2147483647;
            this.txtSyntaxTreeCG.Multiline = true;
            this.txtSyntaxTreeCG.Name = "txtSyntaxTreeCG";
            this.txtSyntaxTreeCG.ReadOnly = true;
            this.txtSyntaxTreeCG.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSyntaxTreeCG.Size = new System.Drawing.Size(284, 240);
            this.txtSyntaxTreeCG.TabIndex = 1;
            // 
            // TextBoxSyntaxTreeCGForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtSyntaxTreeCG);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextBoxSyntaxTreeCGForm";
            this.Text = "语法树";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblSyntaxTreeViewInfo;
        private SyntaxTreeCGTextBox txtSyntaxTreeCG;
    }
}