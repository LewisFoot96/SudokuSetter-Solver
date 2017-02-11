using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    partial class SolveSudokuScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolveSudokuScreen));
            this.solveBtn = new System.Windows.Forms.Button();
            this.loadFileBtn = new System.Windows.Forms.Button();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            this.difficultyDetermineBtn = new System.Windows.Forms.Button();
            this.validatePuzzleBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // solveBtn
            // 
            this.solveBtn.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.solveBtn.Location = new System.Drawing.Point(643, 789);
            this.solveBtn.Name = "solveBtn";
            this.solveBtn.Size = new System.Drawing.Size(171, 93);
            this.solveBtn.TabIndex = 10;
            this.solveBtn.Text = "Solve";
            this.solveBtn.UseVisualStyleBackColor = true;
            this.solveBtn.Click += new System.EventHandler(this.solveBtn_Click);
            // 
            // loadFileBtn
            // 
            this.loadFileBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.loadFileBtn.Location = new System.Drawing.Point(859, 846);
            this.loadFileBtn.Name = "loadFileBtn";
            this.loadFileBtn.Size = new System.Drawing.Size(181, 92);
            this.loadFileBtn.TabIndex = 11;
            this.loadFileBtn.Text = "Load File";
            this.loadFileBtn.UseVisualStyleBackColor = true;
            this.loadFileBtn.Click += new System.EventHandler(this.loadFileBtn_Click);
            // 
            // fileChooser
            // 
            this.fileChooser.FileName = "openFileDialog1";
            this.fileChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.fileChooser_FileOk);
            // 
            // difficultyDetermineBtn
            // 
            this.difficultyDetermineBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.difficultyDetermineBtn.Location = new System.Drawing.Point(399, 846);
            this.difficultyDetermineBtn.Name = "difficultyDetermineBtn";
            this.difficultyDetermineBtn.Size = new System.Drawing.Size(181, 92);
            this.difficultyDetermineBtn.TabIndex = 12;
            this.difficultyDetermineBtn.Text = "Determine Difficulty";
            this.difficultyDetermineBtn.UseVisualStyleBackColor = true;
            this.difficultyDetermineBtn.Click += new System.EventHandler(this.difficultyDetermineBtn_Click);
            // 
            // validatePuzzleBtn
            // 
            this.validatePuzzleBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.validatePuzzleBtn.Location = new System.Drawing.Point(109, 846);
            this.validatePuzzleBtn.Name = "validatePuzzleBtn";
            this.validatePuzzleBtn.Size = new System.Drawing.Size(181, 92);
            this.validatePuzzleBtn.TabIndex = 13;
            this.validatePuzzleBtn.Text = "Validate";
            this.validatePuzzleBtn.UseVisualStyleBackColor = true;
            this.validatePuzzleBtn.Click += new System.EventHandler(this.validatePuzzleBtn_Click);
            // 
            // SolveSudokuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1471, 1003);
            this.Controls.Add(this.validatePuzzleBtn);
            this.Controls.Add(this.difficultyDetermineBtn);
            this.Controls.Add(this.loadFileBtn);
            this.Controls.Add(this.solveBtn);
            this.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SolveSudokuScreen";
            this.Text = "SolveSudokuScreen";
            this.Controls.SetChildIndex(this.solveBtn, 0);
            this.Controls.SetChildIndex(this.loadFileBtn, 0);
            this.Controls.SetChildIndex(this.difficultyDetermineBtn, 0);
            this.Controls.SetChildIndex(this.validatePuzzleBtn, 0);
            this.ResumeLayout(false);

        }
        #endregion
        private Button solveBtn;
        private Button loadFileBtn;
        private OpenFileDialog fileChooser;
        private Button difficultyDetermineBtn;
        private Button validatePuzzleBtn;
    }
}