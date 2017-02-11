using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    partial class PlaySudokuScreen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaySudokuScreen));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.submitPuzzleBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            // 
            // submitPuzzleBtn
            // 
            this.submitPuzzleBtn.Location = new System.Drawing.Point(643, 791);
            this.submitPuzzleBtn.Name = "submitPuzzleBtn";
            this.submitPuzzleBtn.Size = new System.Drawing.Size(171, 93);
            this.submitPuzzleBtn.TabIndex = 9;
            this.submitPuzzleBtn.Text = "Submit Puzzle";
            this.submitPuzzleBtn.UseVisualStyleBackColor = true;
            this.submitPuzzleBtn.Click += new System.EventHandler(this.submitPuzzleBtn_Click);
            // 
            // PlaySudokuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1500, 1100);
            this.Controls.Add(this.submitPuzzleBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlaySudokuScreen";
            this.Text = "PlaySudokuScreen";
            this.Controls.SetChildIndex(this.submitPuzzleBtn, 0);
            this.ResumeLayout(false);

        }

        #endregion
        private Timer timer1;
        private Timer timer2;
        private Timer timer3;
        private Button submitPuzzleBtn;
    }
}