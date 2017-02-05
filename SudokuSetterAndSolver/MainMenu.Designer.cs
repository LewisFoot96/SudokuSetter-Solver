﻿namespace SudokuSetterAndSolver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.gamePlayScreenBtn = new System.Windows.Forms.Button();
            this.randomPuzzleScreenBtn = new System.Windows.Forms.Button();
            this.solveSudokuScreenBtn = new System.Windows.Forms.Button();
            this.statisticsScreenBtn = new System.Windows.Forms.Button();
            this.instructionsCreditsScreenBtn = new System.Windows.Forms.Button();
            this.gameBanner = new System.Windows.Forms.PictureBox();
            this.convertFileBtn = new System.Windows.Forms.Button();
            this.generatePuzzleBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // gamePlayScreenBtn
            // 
            this.gamePlayScreenBtn.Location = new System.Drawing.Point(588, 272);
            this.gamePlayScreenBtn.Name = "gamePlayScreenBtn";
            this.gamePlayScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.gamePlayScreenBtn.TabIndex = 0;
            this.gamePlayScreenBtn.Text = "Play";
            this.gamePlayScreenBtn.UseVisualStyleBackColor = true;
            this.gamePlayScreenBtn.Click += new System.EventHandler(this.gamePlayScreenBtn_Click);
            // 
            // randomPuzzleScreenBtn
            // 
            this.randomPuzzleScreenBtn.Location = new System.Drawing.Point(588, 420);
            this.randomPuzzleScreenBtn.Name = "randomPuzzleScreenBtn";
            this.randomPuzzleScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.randomPuzzleScreenBtn.TabIndex = 2;
            this.randomPuzzleScreenBtn.Text = "Random Puzzle";
            this.randomPuzzleScreenBtn.UseVisualStyleBackColor = true;
            this.randomPuzzleScreenBtn.Click += new System.EventHandler(this.randomPuzzleScreenBtn_Click);
            // 
            // solveSudokuScreenBtn
            // 
            this.solveSudokuScreenBtn.Location = new System.Drawing.Point(588, 568);
            this.solveSudokuScreenBtn.Name = "solveSudokuScreenBtn";
            this.solveSudokuScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.solveSudokuScreenBtn.TabIndex = 3;
            this.solveSudokuScreenBtn.Text = "Solve Sudoku";
            this.solveSudokuScreenBtn.UseVisualStyleBackColor = true;
            this.solveSudokuScreenBtn.Click += new System.EventHandler(this.solveSudokuScreenBtn_Click);
            // 
            // statisticsScreenBtn
            // 
            this.statisticsScreenBtn.Location = new System.Drawing.Point(588, 718);
            this.statisticsScreenBtn.Name = "statisticsScreenBtn";
            this.statisticsScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.statisticsScreenBtn.TabIndex = 4;
            this.statisticsScreenBtn.Text = "Statistics ";
            this.statisticsScreenBtn.UseVisualStyleBackColor = true;
            this.statisticsScreenBtn.Click += new System.EventHandler(this.statisticsScreenBtn_Click);
            // 
            // instructionsCreditsScreenBtn
            // 
            this.instructionsCreditsScreenBtn.Location = new System.Drawing.Point(588, 869);
            this.instructionsCreditsScreenBtn.Name = "instructionsCreditsScreenBtn";
            this.instructionsCreditsScreenBtn.Size = new System.Drawing.Size(266, 121);
            this.instructionsCreditsScreenBtn.TabIndex = 5;
            this.instructionsCreditsScreenBtn.Text = "Instructions/credits";
            this.instructionsCreditsScreenBtn.UseVisualStyleBackColor = true;
            this.instructionsCreditsScreenBtn.Click += new System.EventHandler(this.instructionsCreditsScreenBtn_Click);
            // 
            // gameBanner
            // 
            this.gameBanner.Image = ((System.Drawing.Image)(resources.GetObject("gameBanner.Image")));
            this.gameBanner.Location = new System.Drawing.Point(1, -2);
            this.gameBanner.Name = "gameBanner";
            this.gameBanner.Size = new System.Drawing.Size(1467, 250);
            this.gameBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameBanner.TabIndex = 6;
            this.gameBanner.TabStop = false;
            // 
            // convertFileBtn
            // 
            this.convertFileBtn.Location = new System.Drawing.Point(1016, 377);
            this.convertFileBtn.Name = "convertFileBtn";
            this.convertFileBtn.Size = new System.Drawing.Size(250, 136);
            this.convertFileBtn.TabIndex = 7;
            this.convertFileBtn.Text = "Convert";
            this.convertFileBtn.UseVisualStyleBackColor = true;
            this.convertFileBtn.Click += new System.EventHandler(this.convertFileBtn_Click);
            // 
            // generatePuzzleBtn
            // 
            this.generatePuzzleBtn.Location = new System.Drawing.Point(1016, 556);
            this.generatePuzzleBtn.Name = "generatePuzzleBtn";
            this.generatePuzzleBtn.Size = new System.Drawing.Size(250, 145);
            this.generatePuzzleBtn.TabIndex = 8;
            this.generatePuzzleBtn.Text = "Create 10 Puzzles";
            this.generatePuzzleBtn.UseVisualStyleBackColor = true;
            this.generatePuzzleBtn.Click += new System.EventHandler(this.generatePuzzleBtn_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1468, 1012);
            this.Controls.Add(this.generatePuzzleBtn);
            this.Controls.Add(this.convertFileBtn);
            this.Controls.Add(this.gameBanner);
            this.Controls.Add(this.instructionsCreditsScreenBtn);
            this.Controls.Add(this.statisticsScreenBtn);
            this.Controls.Add(this.solveSudokuScreenBtn);
            this.Controls.Add(this.randomPuzzleScreenBtn);
            this.Controls.Add(this.gamePlayScreenBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button gamePlayScreenBtn;
        private System.Windows.Forms.Button randomPuzzleScreenBtn;
        private System.Windows.Forms.Button solveSudokuScreenBtn;
        private System.Windows.Forms.Button statisticsScreenBtn;
        private System.Windows.Forms.Button instructionsCreditsScreenBtn;
        private System.Windows.Forms.PictureBox gameBanner;
        private System.Windows.Forms.Button convertFileBtn;
        private System.Windows.Forms.Button generatePuzzleBtn;
    }
}

