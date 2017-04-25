namespace SudokuSetterAndSolver
{
    partial class DevelopmentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevelopmentForm));
            this.generatePuzzleBtn = new System.Windows.Forms.Button();
            this.convertFileBtn = new System.Windows.Forms.Button();
            this.mainScreenBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // generatePuzzleBtn
            // 
            this.generatePuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.generatePuzzleBtn.ForeColor = System.Drawing.Color.Maroon;
            this.generatePuzzleBtn.Location = new System.Drawing.Point(319, 166);
            this.generatePuzzleBtn.Name = "generatePuzzleBtn";
            this.generatePuzzleBtn.Size = new System.Drawing.Size(166, 90);
            this.generatePuzzleBtn.TabIndex = 11;
            this.generatePuzzleBtn.Text = "Create 10 Puzzles";
            this.generatePuzzleBtn.UseVisualStyleBackColor = false;
            this.generatePuzzleBtn.Click += new System.EventHandler(this.generatePuzzleBtn_Click);
            // 
            // convertFileBtn
            // 
            this.convertFileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.convertFileBtn.ForeColor = System.Drawing.Color.Maroon;
            this.convertFileBtn.Location = new System.Drawing.Point(319, 34);
            this.convertFileBtn.Name = "convertFileBtn";
            this.convertFileBtn.Size = new System.Drawing.Size(166, 81);
            this.convertFileBtn.TabIndex = 10;
            this.convertFileBtn.Text = "Convert";
            this.convertFileBtn.UseVisualStyleBackColor = false;
            this.convertFileBtn.Click += new System.EventHandler(this.convertFileBtn_Click);
            // 
            // mainScreenBtn
            // 
            this.mainScreenBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.mainScreenBtn.ForeColor = System.Drawing.Color.Maroon;
            this.mainScreenBtn.Location = new System.Drawing.Point(319, 296);
            this.mainScreenBtn.Name = "mainScreenBtn";
            this.mainScreenBtn.Size = new System.Drawing.Size(166, 90);
            this.mainScreenBtn.TabIndex = 12;
            this.mainScreenBtn.Text = "Main Screen";
            this.mainScreenBtn.UseVisualStyleBackColor = false;
            this.mainScreenBtn.Click += new System.EventHandler(this.mainScreenBtn_Click);
            // 
            // DevelopmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(527, 416);
            this.Controls.Add(this.mainScreenBtn);
            this.Controls.Add(this.generatePuzzleBtn);
            this.Controls.Add(this.convertFileBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DevelopmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Development Menu : Siwel Sudoku";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button generatePuzzleBtn;
        private System.Windows.Forms.Button convertFileBtn;
        private System.Windows.Forms.Button mainScreenBtn;
    }
}