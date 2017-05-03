using System.Drawing;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipScreen));
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
            this.tipTextTb.Size = new System.Drawing.Size(353, 456);
            this.tipTextTb.TabIndex = 1;
            // 
            // tipImageBox
            // 
            this.tipImageBox.Location = new System.Drawing.Point(434, 98);
            this.tipImageBox.Name = "tipImageBox";
            this.tipImageBox.Size = new System.Drawing.Size(512, 456);
            this.tipImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.tipImageBox.TabIndex = 2;
            this.tipImageBox.TabStop = false;
            // 
            // TipScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(1005, 590);
            this.Controls.Add(this.tipImageBox);
            this.Controls.Add(this.tipTextTb);
            this.Controls.Add(this.tipTitleTb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TipScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tip";
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