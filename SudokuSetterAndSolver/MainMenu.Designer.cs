namespace SudokuSetterAndSolver
{
    partial class MainMenu
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
            this.gamePlayScreenBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.randomPuzzleScreenBtn = new System.Windows.Forms.Button();
            this.solveSudokuScreenBtn = new System.Windows.Forms.Button();
            this.statisticsScreenBtn = new System.Windows.Forms.Button();
            this.instructionsCreditsScreenBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gamePlayScreenBtn
            // 
            this.gamePlayScreenBtn.Location = new System.Drawing.Point(588, 181);
            this.gamePlayScreenBtn.Name = "gamePlayScreenBtn";
            this.gamePlayScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.gamePlayScreenBtn.TabIndex = 0;
            this.gamePlayScreenBtn.Text = "Play";
            this.gamePlayScreenBtn.UseVisualStyleBackColor = true;
            this.gamePlayScreenBtn.Click += new System.EventHandler(this.gamePlayScreenBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1463, 156);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // randomPuzzleScreenBtn
            // 
            this.randomPuzzleScreenBtn.Location = new System.Drawing.Point(588, 329);
            this.randomPuzzleScreenBtn.Name = "randomPuzzleScreenBtn";
            this.randomPuzzleScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.randomPuzzleScreenBtn.TabIndex = 2;
            this.randomPuzzleScreenBtn.Text = "Random Puzzle";
            this.randomPuzzleScreenBtn.UseVisualStyleBackColor = true;
            this.randomPuzzleScreenBtn.Click += new System.EventHandler(this.randomPuzzleScreenBtn_Click);
            // 
            // solveSudokuScreenBtn
            // 
            this.solveSudokuScreenBtn.Location = new System.Drawing.Point(588, 477);
            this.solveSudokuScreenBtn.Name = "solveSudokuScreenBtn";
            this.solveSudokuScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.solveSudokuScreenBtn.TabIndex = 3;
            this.solveSudokuScreenBtn.Text = "Solve Sudoku";
            this.solveSudokuScreenBtn.UseVisualStyleBackColor = true;
            this.solveSudokuScreenBtn.Click += new System.EventHandler(this.solveSudokuScreenBtn_Click);
            // 
            // statisticsScreenBtn
            // 
            this.statisticsScreenBtn.Location = new System.Drawing.Point(588, 636);
            this.statisticsScreenBtn.Name = "statisticsScreenBtn";
            this.statisticsScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.statisticsScreenBtn.TabIndex = 4;
            this.statisticsScreenBtn.Text = "Statistics ";
            this.statisticsScreenBtn.UseVisualStyleBackColor = true;
            this.statisticsScreenBtn.Click += new System.EventHandler(this.statisticsScreenBtn_Click);
            // 
            // instructionsCreditsScreenBtn
            // 
            this.instructionsCreditsScreenBtn.Location = new System.Drawing.Point(588, 786);
            this.instructionsCreditsScreenBtn.Name = "instructionsCreditsScreenBtn";
            this.instructionsCreditsScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.instructionsCreditsScreenBtn.TabIndex = 5;
            this.instructionsCreditsScreenBtn.Text = "Instructions/credits";
            this.instructionsCreditsScreenBtn.UseVisualStyleBackColor = true;
            this.instructionsCreditsScreenBtn.Click += new System.EventHandler(this.instructionsCreditsScreenBtn_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1455, 953);
            this.Controls.Add(this.instructionsCreditsScreenBtn);
            this.Controls.Add(this.statisticsScreenBtn);
            this.Controls.Add(this.solveSudokuScreenBtn);
            this.Controls.Add(this.randomPuzzleScreenBtn);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gamePlayScreenBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button gamePlayScreenBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button randomPuzzleScreenBtn;
        private System.Windows.Forms.Button solveSudokuScreenBtn;
        private System.Windows.Forms.Button statisticsScreenBtn;
        private System.Windows.Forms.Button instructionsCreditsScreenBtn;
    }
}

