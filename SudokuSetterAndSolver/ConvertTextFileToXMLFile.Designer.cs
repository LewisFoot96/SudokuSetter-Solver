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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvertTextFileToXMLFile));
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.fileNameTb = new System.Windows.Forms.TextBox();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            this.mainScreenBtn = new System.Windows.Forms.Button();
            this.puzzleTitleLb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.selectFileBtn.ForeColor = System.Drawing.Color.Maroon;
            this.selectFileBtn.Location = new System.Drawing.Point(504, 103);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(158, 68);
            this.selectFileBtn.TabIndex = 0;
            this.selectFileBtn.Text = "Select File";
            this.selectFileBtn.UseVisualStyleBackColor = false;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // fileNameTb
            // 
            this.fileNameTb.Location = new System.Drawing.Point(229, 50);
            this.fileNameTb.Name = "fileNameTb";
            this.fileNameTb.Size = new System.Drawing.Size(433, 38);
            this.fileNameTb.TabIndex = 1;
            // 
            // fileChooser
            // 
            this.fileChooser.FileName = "openFileDialog1";
            this.fileChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.fileChooser_FileOk);
            // 
            // mainScreenBtn
            // 
            this.mainScreenBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mainScreenBtn.ForeColor = System.Drawing.Color.Maroon;
            this.mainScreenBtn.Location = new System.Drawing.Point(302, 233);
            this.mainScreenBtn.Name = "mainScreenBtn";
            this.mainScreenBtn.Size = new System.Drawing.Size(158, 68);
            this.mainScreenBtn.TabIndex = 2;
            this.mainScreenBtn.Text = "Back";
            this.mainScreenBtn.UseVisualStyleBackColor = false;
            this.mainScreenBtn.Click += new System.EventHandler(this.mainScreenBtn_Click);
            // 
            // puzzleTitleLb
            // 
            this.puzzleTitleLb.AutoSize = true;
            this.puzzleTitleLb.Location = new System.Drawing.Point(36, 50);
            this.puzzleTitleLb.Name = "puzzleTitleLb";
            this.puzzleTitleLb.Size = new System.Drawing.Size(171, 32);
            this.puzzleTitleLb.TabIndex = 3;
            this.puzzleTitleLb.Text = "Puzzle Title:";
            // 
            // ConvertTextFileToXMLFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(719, 348);
            this.Controls.Add(this.puzzleTitleLb);
            this.Controls.Add(this.mainScreenBtn);
            this.Controls.Add(this.fileNameTb);
            this.Controls.Add(this.selectFileBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConvertTextFileToXMLFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Convert Text File to XML : Siwel Sudoku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.TextBox fileNameTb;
        private System.Windows.Forms.OpenFileDialog fileChooser;
        private System.Windows.Forms.Button mainScreenBtn;
        private System.Windows.Forms.Label puzzleTitleLb;
    }
}