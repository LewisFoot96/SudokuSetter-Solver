namespace SudokuSetterAndSolver
{
    partial class ConvertTextFileToXMLFile
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
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.fileNameTb = new System.Windows.Forms.TextBox();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(288, 132);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(158, 68);
            this.selectFileBtn.TabIndex = 0;
            this.selectFileBtn.Text = "Select File";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // fileNameTb
            // 
            this.fileNameTb.Location = new System.Drawing.Point(230, 68);
            this.fileNameTb.Name = "fileNameTb";
            this.fileNameTb.Size = new System.Drawing.Size(291, 38);
            this.fileNameTb.TabIndex = 1;
            // 
            // fileChooser
            // 
            this.fileChooser.FileName = "openFileDialog1";
            this.fileChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.fileChooser_FileOk);
            // 
            // ConvertTextFileToXMLFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 238);
            this.Controls.Add(this.fileNameTb);
            this.Controls.Add(this.selectFileBtn);
            this.Name = "ConvertTextFileToXMLFile";
            this.Text = "ConvertTextFileToXMLFile";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.TextBox fileNameTb;
        private System.Windows.Forms.OpenFileDialog fileChooser;
    }
}