using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    partial class RandomPuzzleGameScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomPuzzleGameScreen));
            this.submitPuzzleBtn = new System.Windows.Forms.Button();
            this.solveGeneratedPuzzleBtn = new System.Windows.Forms.Button();
            this.newPuzzleBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // submitPuzzleBtn
            // 
            this.submitPuzzleBtn.Location = new System.Drawing.Point(643, 767);
            this.submitPuzzleBtn.Name = "submitPuzzleBtn";
            this.submitPuzzleBtn.Size = new System.Drawing.Size(167, 100);
            this.submitPuzzleBtn.TabIndex = 87;
            this.submitPuzzleBtn.Text = "Submit Puzzle";
            this.submitPuzzleBtn.UseVisualStyleBackColor = true;
            this.submitPuzzleBtn.Click += new System.EventHandler(this.submitPuzzleBtn_Click);
            // 
            // solveGeneratedPuzzleBtn
            // 
            this.solveGeneratedPuzzleBtn.Location = new System.Drawing.Point(872, 851);
            this.solveGeneratedPuzzleBtn.Name = "solveGeneratedPuzzleBtn";
            this.solveGeneratedPuzzleBtn.Size = new System.Drawing.Size(171, 102);
            this.solveGeneratedPuzzleBtn.TabIndex = 88;
            this.solveGeneratedPuzzleBtn.Text = "Solve";
            this.solveGeneratedPuzzleBtn.UseVisualStyleBackColor = true;
            this.solveGeneratedPuzzleBtn.Click += new System.EventHandler(this.solveGeneratedPuzzleBtn_Click);
            // 
            // newPuzzleBtn
            // 
            this.newPuzzleBtn.Location = new System.Drawing.Point(414, 851);
            this.newPuzzleBtn.Name = "newPuzzleBtn";
            this.newPuzzleBtn.Size = new System.Drawing.Size(171, 102);
            this.newPuzzleBtn.TabIndex = 89;
            this.newPuzzleBtn.Text = "New Puzzle";
            this.newPuzzleBtn.UseVisualStyleBackColor = true;
            this.newPuzzleBtn.Click += new System.EventHandler(this.newPuzzleBtn_Click);
            // 
            // RandomPuzzleGameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1471, 1003);
            this.Controls.Add(this.newPuzzleBtn);
            this.Controls.Add(this.solveGeneratedPuzzleBtn);
            this.Controls.Add(this.submitPuzzleBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RandomPuzzleGameScreen";
            this.Text = "RandomPuzzleGameScreen";
            this.Controls.SetChildIndex(this.submitPuzzleBtn, 0);
            this.Controls.SetChildIndex(this.solveGeneratedPuzzleBtn, 0);
            this.Controls.SetChildIndex(this.newPuzzleBtn, 0);
            this.ResumeLayout(false);

        }
        #endregion


        private Button submitPuzzleBtn;
        private Button solveGeneratedPuzzleBtn;
        private Button newPuzzleBtn;
    }
}