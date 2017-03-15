using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    partial class MainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solvePuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.easyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.level2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.level6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level9ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level11ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.level12ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uniqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.irregularToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.irregular2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.levelsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1468, 49);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "mainMenuStrip";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPuzzleToolStripMenuItem,
            this.solvePuzzleToolStripMenuItem,
            this.clearPuzzleToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(83, 45);
            this.fIleToolStripMenuItem.Text = "File ";
            // 
            // newPuzzleToolStripMenuItem
            // 
            this.newPuzzleToolStripMenuItem.Name = "newPuzzleToolStripMenuItem";
            this.newPuzzleToolStripMenuItem.Size = new System.Drawing.Size(297, 46);
            this.newPuzzleToolStripMenuItem.Text = "New Puzzle";
            this.newPuzzleToolStripMenuItem.Click += new System.EventHandler(this.newPuzzleToolStripMenuItem_Click);
            // 
            // solvePuzzleToolStripMenuItem
            // 
            this.solvePuzzleToolStripMenuItem.Name = "solvePuzzleToolStripMenuItem";
            this.solvePuzzleToolStripMenuItem.Size = new System.Drawing.Size(297, 46);
            this.solvePuzzleToolStripMenuItem.Text = "Solve Puzzle";
            this.solvePuzzleToolStripMenuItem.Click += new System.EventHandler(this.solvePuzzleToolStripMenuItem_Click);
            // 
            // clearPuzzleToolStripMenuItem
            // 
            this.clearPuzzleToolStripMenuItem.Name = "clearPuzzleToolStripMenuItem";
            this.clearPuzzleToolStripMenuItem.Size = new System.Drawing.Size(297, 46);
            this.clearPuzzleToolStripMenuItem.Text = "Clear Puzzle";
            // 
            // levelsToolStripMenuItem
            // 
            this.levelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.easyToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.hardToolStripMenuItem,
            this.insaneToolStripMenuItem,
            this.uniqueToolStripMenuItem});
            this.levelsToolStripMenuItem.Name = "levelsToolStripMenuItem";
            this.levelsToolStripMenuItem.Size = new System.Drawing.Size(110, 45);
            this.levelsToolStripMenuItem.Text = "Levels";
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level1ToolStripMenuItem,
            this.level2ToolStripMenuItem,
            this.level3ToolStripMenuItem});
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            this.easyToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.easyToolStripMenuItem.Text = "Easy";
            // 
            // level1ToolStripMenuItem
            // 
            this.level1ToolStripMenuItem.Name = "level1ToolStripMenuItem";
            this.level1ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level1ToolStripMenuItem.Text = "Level 1";
            this.level1ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level2ToolStripMenuItem
            // 
            this.level2ToolStripMenuItem.Name = "level2ToolStripMenuItem";
            this.level2ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level2ToolStripMenuItem.Text = "Level 2";
            this.level2ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level3ToolStripMenuItem
            // 
            this.level3ToolStripMenuItem.Name = "level3ToolStripMenuItem";
            this.level3ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level3ToolStripMenuItem.Text = "Level 3";
            this.level3ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level1ToolStripMenuItem1,
            this.level2ToolStripMenuItem1,
            this.level6ToolStripMenuItem});
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.mediumToolStripMenuItem.Text = "Medium";
            // 
            // level1ToolStripMenuItem1
            // 
            this.level1ToolStripMenuItem1.Name = "level1ToolStripMenuItem1";
            this.level1ToolStripMenuItem1.Size = new System.Drawing.Size(327, 46);
            this.level1ToolStripMenuItem1.Text = "Level 4";
            this.level1ToolStripMenuItem1.Click += new System.EventHandler(this.LevelsSelectClick);

            // 
            // level2ToolStripMenuItem1
            // 
            this.level2ToolStripMenuItem1.Name = "level2ToolStripMenuItem1";
            this.level2ToolStripMenuItem1.Size = new System.Drawing.Size(327, 46);
            this.level2ToolStripMenuItem1.Text = "Level 5";
            this.level2ToolStripMenuItem1.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level6ToolStripMenuItem
            // 
            this.level6ToolStripMenuItem.Name = "level6ToolStripMenuItem";
            this.level6ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level6ToolStripMenuItem.Text = "Level 6";
            this.level6ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // hardToolStripMenuItem
            // 
            this.hardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level7ToolStripMenuItem,
            this.level8ToolStripMenuItem,
            this.level9ToolStripMenuItem});
            this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            this.hardToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.hardToolStripMenuItem.Text = "Hard";
            // 
            // level7ToolStripMenuItem
            // 
            this.level7ToolStripMenuItem.Name = "level7ToolStripMenuItem";
            this.level7ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level7ToolStripMenuItem.Text = "Level 7 ";
            this.level7ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level8ToolStripMenuItem
            // 
            this.level8ToolStripMenuItem.Name = "level8ToolStripMenuItem";
            this.level8ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level8ToolStripMenuItem.Text = "Level 8";
            this.level8ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level9ToolStripMenuItem
            // 
            this.level9ToolStripMenuItem.Name = "level9ToolStripMenuItem";
            this.level9ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level9ToolStripMenuItem.Text = "Level 9";
            this.level9ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // insaneToolStripMenuItem
            // 
            this.insaneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level10ToolStripMenuItem,
            this.level11ToolStripMenuItem,
            this.level12ToolStripMenuItem});
            this.insaneToolStripMenuItem.Name = "insaneToolStripMenuItem";
            this.insaneToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.insaneToolStripMenuItem.Text = "Insane";
            // 
            // level10ToolStripMenuItem
            // 
            this.level10ToolStripMenuItem.Name = "level10ToolStripMenuItem";
            this.level10ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level10ToolStripMenuItem.Text = "Level 10";
            this.level10ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level11ToolStripMenuItem
            // 
            this.level11ToolStripMenuItem.Name = "level11ToolStripMenuItem";
            this.level11ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level11ToolStripMenuItem.Text = "Level 11";
            this.level11ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level12ToolStripMenuItem
            // 
            this.level12ToolStripMenuItem.Name = "level12ToolStripMenuItem";
            this.level12ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.level12ToolStripMenuItem.Text = "Level 12";
            this.level12ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // uniqueToolStripMenuItem
            // 
            this.uniqueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.irregularToolStripMenuItem,
            this.irregular2ToolStripMenuItem,
            this.smallGridToolStripMenuItem});
            this.uniqueToolStripMenuItem.Name = "uniqueToolStripMenuItem";
            this.uniqueToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.uniqueToolStripMenuItem.Text = "Unique";
            // 
            // irregularToolStripMenuItem
            // 
            this.irregularToolStripMenuItem.Name = "irregularToolStripMenuItem";
            this.irregularToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.irregularToolStripMenuItem.Text = "Irregular";
            this.irregularToolStripMenuItem.Enabled = false;
            this.irregularToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // irregular2ToolStripMenuItem
            // 
            this.irregular2ToolStripMenuItem.Name = "irregular2ToolStripMenuItem";
            this.irregular2ToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.irregular2ToolStripMenuItem.Text = "Irregular 2";
            this.irregular2ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // smallGridToolStripMenuItem
            // 
            this.smallGridToolStripMenuItem.Name = "smallGridToolStripMenuItem";
            this.smallGridToolStripMenuItem.Size = new System.Drawing.Size(327, 46);
            this.smallGridToolStripMenuItem.Text = "Small Grid";
            this.smallGridToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1468, 1412);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainScreen";
            this.Text = "Main Menu";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CreateRandomPuzzleButtons()
        {
            this.submitPuzzleBtn = new System.Windows.Forms.Button();
            this.solveGeneratedPuzzleBtn = new System.Windows.Forms.Button();
            this.newPuzzleBtn = new System.Windows.Forms.Button();

            // 
            // submitPuzzleBtn
            // 
            this.submitPuzzleBtn.Location = new System.Drawing.Point(225,210);
            this.submitPuzzleBtn.Name = "submitPuzzleBtn";
            this.submitPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.submitPuzzleBtn.TabIndex = 87;
            this.submitPuzzleBtn.Text = "Submit Puzzle";
            this.submitPuzzleBtn.UseVisualStyleBackColor = true;
            this.submitPuzzleBtn.Click += new System.EventHandler(this.submitPuzzleBtn_Click);
            // 
            // solveGeneratedPuzzleBtn
            // 
            this.solveGeneratedPuzzleBtn.Location = new System.Drawing.Point(165, 210);
            this.solveGeneratedPuzzleBtn.Name = "solveGeneratedPuzzleBtn";
            this.solveGeneratedPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.solveGeneratedPuzzleBtn.TabIndex = 88;
            this.solveGeneratedPuzzleBtn.Text = "Solve";
            this.solveGeneratedPuzzleBtn.UseVisualStyleBackColor = true;
            this.solveGeneratedPuzzleBtn.Click += new System.EventHandler(this.solveGeneratedPuzzleBtn_Click);
            // 
            // newPuzzleBtn
            // 
            this.newPuzzleBtn.Location = new System.Drawing.Point(285, 210);
            this.newPuzzleBtn.Name = "newPuzzleBtn";
            this.newPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.newPuzzleBtn.TabIndex = 89;
            this.newPuzzleBtn.Text = "New Puzzle";
            this.newPuzzleBtn.UseVisualStyleBackColor = true;
            this.newPuzzleBtn.Click += new System.EventHandler(this.newPuzzleBtn_Click);


            this.Controls.Add(this.newPuzzleBtn);
            this.Controls.Add(this.solveGeneratedPuzzleBtn);
            this.Controls.Add(this.submitPuzzleBtn);
            this.Controls.SetChildIndex(this.submitPuzzleBtn, 0);
            this.Controls.SetChildIndex(this.solveGeneratedPuzzleBtn, 0);
            this.Controls.SetChildIndex(this.newPuzzleBtn, 0);


        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearPuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solvePuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelsToolStripMenuItem;
        private Button submitPuzzleBtn;
        private Button solveGeneratedPuzzleBtn;
        private Button newPuzzleBtn;
        private ToolStripMenuItem easyToolStripMenuItem;
        private ToolStripMenuItem level1ToolStripMenuItem;
        private ToolStripMenuItem level2ToolStripMenuItem;
        private ToolStripMenuItem level3ToolStripMenuItem;
        private ToolStripMenuItem mediumToolStripMenuItem;
        private ToolStripMenuItem level1ToolStripMenuItem1;
        private ToolStripMenuItem level2ToolStripMenuItem1;
        private ToolStripMenuItem level6ToolStripMenuItem;
        private ToolStripMenuItem hardToolStripMenuItem;
        private ToolStripMenuItem level7ToolStripMenuItem;
        private ToolStripMenuItem level8ToolStripMenuItem;
        private ToolStripMenuItem level9ToolStripMenuItem;
        private ToolStripMenuItem insaneToolStripMenuItem;
        private ToolStripMenuItem level10ToolStripMenuItem;
        private ToolStripMenuItem level11ToolStripMenuItem;
        private ToolStripMenuItem level12ToolStripMenuItem;
        private ToolStripMenuItem uniqueToolStripMenuItem;
        private ToolStripMenuItem irregularToolStripMenuItem;
        private ToolStripMenuItem irregular2ToolStripMenuItem;
        private ToolStripMenuItem smallGridToolStripMenuItem;
    }


}

