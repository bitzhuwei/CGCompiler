namespace bitzhuwei.CGCompiler
{
    /// <summary>
    /// 
    /// </summary>
    partial class TokenListCGTextBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TokenListCGTextBoxForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTokenListInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtTokenList = new bitzhuwei.CGCompiler.TokenListCGTextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTokenListInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 379);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(382, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblTokenListInfo
            // 
            this.lblTokenListInfo.Name = "lblTokenListInfo";
            this.lblTokenListInfo.Size = new System.Drawing.Size(80, 17);
            this.lblTokenListInfo.Text = "单词列表信息";
            // 
            // txtTokenList
            // 
            this.txtTokenList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTokenList.HideSelection = false;
            this.txtTokenList.Location = new System.Drawing.Point(0, 0);
            this.txtTokenList.MaxLength = 2147483647;
            this.txtTokenList.Multiline = true;
            this.txtTokenList.Name = "txtTokenList";
            this.txtTokenList.ReadOnly = true;
            this.txtTokenList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTokenList.Size = new System.Drawing.Size(382, 379);
            this.txtTokenList.TabIndex = 1;
            this.txtTokenList.Text = "更新源代码以在此显示其分析结果";
            // 
            // TokenListTextBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 401);
            this.Controls.Add(this.txtTokenList);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TokenListTextBoxForm";
            this.Text = "单词流";
            this.Load += new System.EventHandler(this.TokenListTextBoxForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTokenListInfo;
        private TokenListCGTextBox txtTokenList;
    }
}