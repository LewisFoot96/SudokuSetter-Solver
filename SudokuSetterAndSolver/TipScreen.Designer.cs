namespace SudokuSetterAndSolver
{
    partial class TipScreen
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
            this.tipTitleTb = new System.Windows.Forms.TextBox();
            this.tipTextTb = new System.Windows.Forms.TextBox();
            this.tipImageBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tipImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tipTitleTb
            // 
            this.tipTitleTb.Enabled = false;
            this.tipTitleTb.Location = new System.Drawing.Point(141, 27);
            this.tipTitleTb.Name = "tipTitleTb";
            this.tipTitleTb.Size = new System.Drawing.Size(652, 38);
            this.tipTitleTb.TabIndex = 0;
            // 
            // tipTextTb
            // 
            this.tipTextTb.Enabled = false;
            this.tipTextTb.Location = new System.Drawing.Point(44, 98);
            this.tipTextTb.Multiline = true;
            this.tipTextTb.Name = "tipTextTb";
            this.tipTextTb.Size = new System.Drawing.Size(514, 386);
            this.tipTextTb.TabIndex = 1;
            // 
            // tipImageBox
            // 
            this.tipImageBox.Location = new System.Drawing.Point(593, 192);
            this.tipImageBox.Name = "tipImageBox";
            this.tipImageBox.Size = new System.Drawing.Size(310, 185);
            this.tipImageBox.TabIndex = 2;
            this.tipImageBox.TabStop = false;
            // 
            // TipScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 521);
            this.Controls.Add(this.tipImageBox);
            this.Controls.Add(this.tipTextTb);
            this.Controls.Add(this.tipTitleTb);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TipScreen";
            this.Text = "TipScreen";
            ((System.ComponentModel.ISupportInitialize)(this.tipImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tipTitleTb;
        private System.Windows.Forms.TextBox tipTextTb;
        private System.Windows.Forms.PictureBox tipImageBox;
    }
}