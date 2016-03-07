namespace bitzhuwei.CGCompiler.Winform
{
    partial class FormNotImplementedNodes
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
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnQuit = new System.Windows.Forms.Button();
            this.btnAddToGrammar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.Location = new System.Drawing.Point(12, 12);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(957, 528);
            this.txtContent.TabIndex = 0;
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnQuit.Location = new System.Drawing.Point(894, 546);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 1;
            this.btnQuit.Text = "退出";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnAddToGrammar
            // 
            this.btnAddToGrammar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToGrammar.Location = new System.Drawing.Point(685, 546);
            this.btnAddToGrammar.Name = "btnAddToGrammar";
            this.btnAddToGrammar.Size = new System.Drawing.Size(203, 23);
            this.btnAddToGrammar.TabIndex = 1;
            this.btnAddToGrammar.Text = "添加到文法";
            this.btnAddToGrammar.UseVisualStyleBackColor = true;
            this.btnAddToGrammar.Click += new System.EventHandler(this.btnAddToGrammar_Click);
            // 
            // FormNotImplementedNodes
            // 
            this.AcceptButton = this.btnAddToGrammar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnQuit;
            this.ClientSize = new System.Drawing.Size(981, 581);
            this.Controls.Add(this.btnAddToGrammar);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.txtContent);
            this.Name = "FormNotImplementedNodes";
            this.Text = "FormNotImplementedNodes";
            this.Load += new System.EventHandler(this.FormNotImplementedNodes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Button btnAddToGrammar;
    }
}