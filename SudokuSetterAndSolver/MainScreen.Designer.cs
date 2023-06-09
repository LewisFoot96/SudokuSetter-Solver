﻿using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solvePuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.statisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.puzzleTimer = new System.Windows.Forms.Timer(this.components);
            this.timerText = new System.Windows.Forms.TextBox();
            this.puzzlesInformationTb = new System.Windows.Forms.TextBox();
            this.staticsDispalyTb = new System.Windows.Forms.TextBox();
            this.developmentBtn = new System.Windows.Forms.Button();
            this.logoStartUpPb = new System.Windows.Forms.PictureBox();
            this.solutionDisplayInfoTb = new System.Windows.Forms.TextBox();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoStartUpPb)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.levelsToolStripMenuItem,
            this.statisticsToolStripMenuItem});
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Name = "mainMenuStrip";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPuzzleToolStripMenuItem,
            this.solvePuzzleToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            resources.ApplyResources(this.fIleToolStripMenuItem, "fIleToolStripMenuItem");
            // 
            // newPuzzleToolStripMenuItem
            // 
            this.newPuzzleToolStripMenuItem.Name = "newPuzzleToolStripMenuItem";
            resources.ApplyResources(this.newPuzzleToolStripMenuItem, "newPuzzleToolStripMenuItem");
            this.newPuzzleToolStripMenuItem.Click += new System.EventHandler(this.newPuzzleToolStripMenuItem_Click);
            // 
            // solvePuzzleToolStripMenuItem
            // 
            this.solvePuzzleToolStripMenuItem.Name = "solvePuzzleToolStripMenuItem";
            resources.ApplyResources(this.solvePuzzleToolStripMenuItem, "solvePuzzleToolStripMenuItem");
            this.solvePuzzleToolStripMenuItem.Click += new System.EventHandler(this.solvePuzzleToolStripMenuItem_Click);
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
            resources.ApplyResources(this.levelsToolStripMenuItem, "levelsToolStripMenuItem");
            // 
            // easyToolStripMenuItem
            // 
            this.easyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level1ToolStripMenuItem,
            this.level2ToolStripMenuItem,
            this.level3ToolStripMenuItem});
            this.easyToolStripMenuItem.Name = "easyToolStripMenuItem";
            resources.ApplyResources(this.easyToolStripMenuItem, "easyToolStripMenuItem");
            // 
            // level1ToolStripMenuItem
            // 
            this.level1ToolStripMenuItem.Name = "level1ToolStripMenuItem";
            resources.ApplyResources(this.level1ToolStripMenuItem, "level1ToolStripMenuItem");
            this.level1ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level2ToolStripMenuItem
            // 
            this.level2ToolStripMenuItem.Name = "level2ToolStripMenuItem";
            resources.ApplyResources(this.level2ToolStripMenuItem, "level2ToolStripMenuItem");
            this.level2ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level3ToolStripMenuItem
            // 
            this.level3ToolStripMenuItem.Name = "level3ToolStripMenuItem";
            resources.ApplyResources(this.level3ToolStripMenuItem, "level3ToolStripMenuItem");
            this.level3ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level1ToolStripMenuItem1,
            this.level2ToolStripMenuItem1,
            this.level6ToolStripMenuItem});
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            resources.ApplyResources(this.mediumToolStripMenuItem, "mediumToolStripMenuItem");
            // 
            // level1ToolStripMenuItem1
            // 
            this.level1ToolStripMenuItem1.Name = "level1ToolStripMenuItem1";
            resources.ApplyResources(this.level1ToolStripMenuItem1, "level1ToolStripMenuItem1");
            this.level1ToolStripMenuItem1.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level2ToolStripMenuItem1
            // 
            this.level2ToolStripMenuItem1.Name = "level2ToolStripMenuItem1";
            resources.ApplyResources(this.level2ToolStripMenuItem1, "level2ToolStripMenuItem1");
            this.level2ToolStripMenuItem1.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level6ToolStripMenuItem
            // 
            this.level6ToolStripMenuItem.Name = "level6ToolStripMenuItem";
            resources.ApplyResources(this.level6ToolStripMenuItem, "level6ToolStripMenuItem");
            this.level6ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // hardToolStripMenuItem
            // 
            this.hardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level7ToolStripMenuItem,
            this.level8ToolStripMenuItem,
            this.level9ToolStripMenuItem});
            this.hardToolStripMenuItem.Name = "hardToolStripMenuItem";
            resources.ApplyResources(this.hardToolStripMenuItem, "hardToolStripMenuItem");
            // 
            // level7ToolStripMenuItem
            // 
            this.level7ToolStripMenuItem.Name = "level7ToolStripMenuItem";
            resources.ApplyResources(this.level7ToolStripMenuItem, "level7ToolStripMenuItem");
            this.level7ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level8ToolStripMenuItem
            // 
            this.level8ToolStripMenuItem.Name = "level8ToolStripMenuItem";
            resources.ApplyResources(this.level8ToolStripMenuItem, "level8ToolStripMenuItem");
            this.level8ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level9ToolStripMenuItem
            // 
            this.level9ToolStripMenuItem.Name = "level9ToolStripMenuItem";
            resources.ApplyResources(this.level9ToolStripMenuItem, "level9ToolStripMenuItem");
            this.level9ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // insaneToolStripMenuItem
            // 
            this.insaneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.level10ToolStripMenuItem,
            this.level11ToolStripMenuItem,
            this.level12ToolStripMenuItem});
            this.insaneToolStripMenuItem.Name = "insaneToolStripMenuItem";
            resources.ApplyResources(this.insaneToolStripMenuItem, "insaneToolStripMenuItem");
            // 
            // level10ToolStripMenuItem
            // 
            this.level10ToolStripMenuItem.Name = "level10ToolStripMenuItem";
            resources.ApplyResources(this.level10ToolStripMenuItem, "level10ToolStripMenuItem");
            this.level10ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level11ToolStripMenuItem
            // 
            this.level11ToolStripMenuItem.Name = "level11ToolStripMenuItem";
            resources.ApplyResources(this.level11ToolStripMenuItem, "level11ToolStripMenuItem");
            this.level11ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // level12ToolStripMenuItem
            // 
            this.level12ToolStripMenuItem.Name = "level12ToolStripMenuItem";
            resources.ApplyResources(this.level12ToolStripMenuItem, "level12ToolStripMenuItem");
            this.level12ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // uniqueToolStripMenuItem
            // 
            this.uniqueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.irregularToolStripMenuItem,
            this.irregular2ToolStripMenuItem,
            this.smallGridToolStripMenuItem});
            this.uniqueToolStripMenuItem.Name = "uniqueToolStripMenuItem";
            resources.ApplyResources(this.uniqueToolStripMenuItem, "uniqueToolStripMenuItem");
            // 
            // irregularToolStripMenuItem
            // 
            this.irregularToolStripMenuItem.Name = "irregularToolStripMenuItem";
            resources.ApplyResources(this.irregularToolStripMenuItem, "irregularToolStripMenuItem");
            this.irregularToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // irregular2ToolStripMenuItem
            // 
            this.irregular2ToolStripMenuItem.Name = "irregular2ToolStripMenuItem";
            resources.ApplyResources(this.irregular2ToolStripMenuItem, "irregular2ToolStripMenuItem");
            this.irregular2ToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // smallGridToolStripMenuItem
            // 
            this.smallGridToolStripMenuItem.Name = "smallGridToolStripMenuItem";
            resources.ApplyResources(this.smallGridToolStripMenuItem, "smallGridToolStripMenuItem");
            this.smallGridToolStripMenuItem.Click += new System.EventHandler(this.LevelsSelectClick);
            // 
            // statisticsToolStripMenuItem
            // 
            this.statisticsToolStripMenuItem.Name = "statisticsToolStripMenuItem";
            resources.ApplyResources(this.statisticsToolStripMenuItem, "statisticsToolStripMenuItem");
            this.statisticsToolStripMenuItem.Click += new System.EventHandler(this.statisticsToolStripMenuItem_Click);
            // 
            // puzzleTimer
            // 
            this.puzzleTimer.Interval = 1000;
            this.puzzleTimer.Tick += new System.EventHandler(this.puzzleTimer_Tick);
            // 
            // timerText
            // 
            this.timerText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.timerText, "timerText");
            this.timerText.Name = "timerText";
            // 
            // puzzlesInformationTb
            // 
            this.puzzlesInformationTb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.puzzlesInformationTb, "puzzlesInformationTb");
            this.puzzlesInformationTb.Name = "puzzlesInformationTb";
            // 
            // staticsDispalyTb
            // 
            this.staticsDispalyTb.BackColor = System.Drawing.Color.Aqua;
            resources.ApplyResources(this.staticsDispalyTb, "staticsDispalyTb");
            this.staticsDispalyTb.Name = "staticsDispalyTb";
            // 
            // developmentBtn
            // 
            resources.ApplyResources(this.developmentBtn, "developmentBtn");
            this.developmentBtn.Name = "developmentBtn";
            this.developmentBtn.UseVisualStyleBackColor = true;
            this.developmentBtn.Click += new System.EventHandler(this.developmentBtn_Click);
            // 
            // logoStartUpPb
            // 
            resources.ApplyResources(this.logoStartUpPb, "logoStartUpPb");
            this.logoStartUpPb.Name = "logoStartUpPb";
            this.logoStartUpPb.TabStop = false;
            // 
            // solutionDisplayInfoTb
            // 
            this.solutionDisplayInfoTb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            resources.ApplyResources(this.solutionDisplayInfoTb, "solutionDisplayInfoTb");
            this.solutionDisplayInfoTb.Name = "solutionDisplayInfoTb";
            // 
            // MainScreen
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Controls.Add(this.solutionDisplayInfoTb);
            this.Controls.Add(this.logoStartUpPb);
            this.Controls.Add(this.developmentBtn);
            this.Controls.Add(this.staticsDispalyTb);
            this.Controls.Add(this.puzzlesInformationTb);
            this.Controls.Add(this.timerText);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.Name = "MainScreen";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoStartUpPb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void CreateRandomPuzzleButtons()
        {
            this.submitPuzzleBtn = new System.Windows.Forms.Button();
            //this.solveGeneratedPuzzleBtn = new System.Windows.Forms.Button();
            this.newPuzzleBtn = new System.Windows.Forms.Button();
            this.hintBtn = new Button();
            this.hintsRegionBtn = new Button();
            this.tipBtn = new Button();

            // 
            // submitPuzzleBtn
            // 
            this.submitPuzzleBtn.Location = new System.Drawing.Point(105,230);
            this.submitPuzzleBtn.Name = "submitPuzzleBtn";
            this.submitPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.submitPuzzleBtn.TabIndex = 87;
            this.submitPuzzleBtn.Text = "Submit Puzzle";
            this.submitPuzzleBtn.UseVisualStyleBackColor = true;
            this.submitPuzzleBtn.Click += new System.EventHandler(this.submitPuzzleBtn_Click);
            this.submitPuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.submitPuzzleBtn.ForeColor = System.Drawing.Color.Maroon;
            // 
            // solveGeneratedPuzzleBtn - Development button
            // 
            /*
            this.solveGeneratedPuzzleBtn.Location = new System.Drawing.Point(75, 230);
            this.solveGeneratedPuzzleBtn.Name = "solveGeneratedPuzzleBtn";
            this.solveGeneratedPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.solveGeneratedPuzzleBtn.TabIndex = 88;
            this.solveGeneratedPuzzleBtn.Text = "Solve";
            this.solveGeneratedPuzzleBtn.UseVisualStyleBackColor = true;
            this.solveGeneratedPuzzleBtn.Click += new System.EventHandler(this.solveGeneratedPuzzleBtn_Click);
            this.solveGeneratedPuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.solveGeneratedPuzzleBtn.ForeColor = System.Drawing.Color.Black; 
            */     
            // 
            // newPuzzleBtn
            // 
            this.newPuzzleBtn.Location = new System.Drawing.Point(166, 230);
            this.newPuzzleBtn.Name = "newPuzzleBtn";
            this.newPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.newPuzzleBtn.TabIndex = 89;
            this.newPuzzleBtn.Text = "New Puzzle";
            this.newPuzzleBtn.UseVisualStyleBackColor = true;
            this.newPuzzleBtn.Click += new System.EventHandler(this.newPuzzleBtn_Click);
            this.newPuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.newPuzzleBtn.ForeColor = System.Drawing.Color.Maroon;

            // 
            // hintsBtn
            // 
            this.hintBtn.Location = new System.Drawing.Point(226, 230);
            this.hintBtn.Name = "hintsBtn";
            this.hintBtn.Size = new System.Drawing.Size(60, 35);
            this.hintBtn.TabIndex = 87;
            this.hintBtn.Text = " Cell\r\n (1 hint)";
            this.hintBtn.UseVisualStyleBackColor = true;
            this.hintBtn.Click += new System.EventHandler(this.hintsBtn_Click);
            this.hintBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.hintBtn.ForeColor = System.Drawing.Color.Maroon;
            // 
            // hintsRegionBtn
            // 
            this.hintsRegionBtn.Location = new System.Drawing.Point(286, 230);
            this.hintsRegionBtn.Name = "hintsRegionBtn";
            this.hintsRegionBtn.Size = new System.Drawing.Size(60, 35);
            this.hintsRegionBtn.TabIndex = 87;
            this.hintsRegionBtn.Text = "Region\r\n (5 hints)";
            this.hintsRegionBtn.UseVisualStyleBackColor = true;
            this.hintsRegionBtn.Click += new System.EventHandler(this.hintsRegionBtn_Click);
            this.hintsRegionBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.hintsRegionBtn.ForeColor = System.Drawing.Color.Maroon;
            //
            // tipBtn
            //
            this.tipBtn.Location = new System.Drawing.Point(346, 230);
            this.tipBtn.Name = "tipBtn";
            this.tipBtn.Size = new System.Drawing.Size(60, 35);
            this.tipBtn.TabIndex = 88;
            this.tipBtn.Text = "Tip\r\n (5 hints)";
            this.tipBtn.UseVisualStyleBackColor = true;
            this.tipBtn.Click += new System.EventHandler(this.tipBtn_Click);
            this.tipBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tipBtn.ForeColor = System.Drawing.Color.Maroon;

            this.Controls.Add(this.newPuzzleBtn);
            //this.Controls.Add(this.solveGeneratedPuzzleBtn);
            this.Controls.Add(this.submitPuzzleBtn);
            this.Controls.SetChildIndex(this.submitPuzzleBtn, 1);
            //this.Controls.SetChildIndex(this.solveGeneratedPuzzleBtn, 2);
            this.Controls.SetChildIndex(this.newPuzzleBtn, 3);
            this.Controls.Add(this.hintBtn);
            this.Controls.SetChildIndex(this.hintBtn, 4);
            this.Controls.Add(this.hintsRegionBtn);
            this.Controls.SetChildIndex(this.hintsRegionBtn, 5);
            this.Controls.Add(this.tipBtn);
            this.Controls.SetChildIndex(this.tipBtn, 6);
        }

        private void CreateLevelPuzzleButtons()
        {
            this.submitLevelPuzzleBtn = new System.Windows.Forms.Button();
            this.hintsLevelBtn = new Button();
            this.tipBtn = new Button();
            this.hintsRegionLevelBtn = new Button();

            // 
            // submitPuzzleBtn
            // 
            this.submitLevelPuzzleBtn.Location = new System.Drawing.Point(134, 230);
            this.submitLevelPuzzleBtn.Name = "submitLevelPuzzleBtn";
            this.submitLevelPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.submitLevelPuzzleBtn.TabIndex = 87;
            this.submitLevelPuzzleBtn.Text = "Submit Puzzle";
            this.submitLevelPuzzleBtn.UseVisualStyleBackColor = true;
            this.submitLevelPuzzleBtn.Click += new System.EventHandler(this.submitLevelPuzzleBtn_Click);
            this.submitLevelPuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.submitLevelPuzzleBtn.ForeColor = System.Drawing.Color.Maroon;
            // 
            // hintsBtn
            // 
            this.hintsLevelBtn.Location = new System.Drawing.Point(194, 230);
            this.hintsLevelBtn.Name = "hintsBtn";
            this.hintsLevelBtn.Size = new System.Drawing.Size(60, 35);
            this.hintsLevelBtn.TabIndex = 87;
            this.hintsLevelBtn.Text = " Cell\r\n (1 hint)";
            this.hintsLevelBtn.UseVisualStyleBackColor = true;
            this.hintsLevelBtn.Click += new System.EventHandler(this.hintsBtn_Click);
            this.hintsLevelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.hintsLevelBtn.ForeColor = System.Drawing.Color.Maroon;
            // 
            // hintsRegionBtn
            // 
            this.hintsRegionLevelBtn.Location = new System.Drawing.Point(254, 230);
            this.hintsRegionLevelBtn.Name = "hintsRegionBtn";
            this.hintsRegionLevelBtn.Size = new System.Drawing.Size(60, 35);
            this.hintsRegionLevelBtn.TabIndex = 87;
            this.hintsRegionLevelBtn.Text = "Region\r\n (5 hints)";
            this.hintsRegionLevelBtn.UseVisualStyleBackColor = true;
            this.hintsRegionLevelBtn.Click += new System.EventHandler(this.hintsRegionBtn_Click);
            this.hintsRegionLevelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.hintsRegionLevelBtn.ForeColor = System.Drawing.Color.Maroon;
            //
            // tipBtn
            //
            this.tipBtn.Location = new System.Drawing.Point(314, 230);
            this.tipBtn.Name = "tipBtn";
            this.tipBtn.Size = new System.Drawing.Size(60, 35);
            this.tipBtn.TabIndex = 88;
            this.tipBtn.Text = "Tip\r\n (5 hints)";
            this.tipBtn.UseVisualStyleBackColor = true;
            this.tipBtn.Click += new System.EventHandler(this.tipBtn_Click);
            this.tipBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tipBtn.ForeColor = System.Drawing.Color.Maroon;

            this.Controls.Add(this.submitLevelPuzzleBtn);
            this.Controls.SetChildIndex(this.submitLevelPuzzleBtn, 0);
            this.Controls.Add(this.hintsLevelBtn);
            this.Controls.SetChildIndex(this.hintsLevelBtn, 1);
            this.Controls.Add(this.hintsRegionLevelBtn);
            this.Controls.SetChildIndex(this.hintsRegionLevelBtn, 2);
            this.Controls.Add(this.tipBtn);
            this.Controls.SetChildIndex(this.tipBtn, 3);
        }

        private void CreateSolveButtons()
        {
            //this.solveGeneratedPuzzleBtn = new System.Windows.Forms.Button();
            this.loadFileBtn = new System.Windows.Forms.Button();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            this.difficultyDetermineBtn = new System.Windows.Forms.Button();
            //this.validatePuzzleBtn = new System.Windows.Forms.Button();
            this.newSolvePuzzleBtn = new Button();
            this.clearPuzzleBtn = new Button();
            /*
            // 
            // solveGeneratedPuzzleBtn
            // 
            this.solveGeneratedPuzzleBtn.Location = new System.Drawing.Point(192, 230);
            this.solveGeneratedPuzzleBtn.Name = "solveGeneratedPuzzleBtn";
            this.solveGeneratedPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.solveGeneratedPuzzleBtn.TabIndex = 88;
            this.solveGeneratedPuzzleBtn.Text = "Solve";
            this.solveGeneratedPuzzleBtn.UseVisualStyleBackColor = true;
            this.solveGeneratedPuzzleBtn.Click += new System.EventHandler(this.solveGeneratedPuzzleBtn_Click);
            System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.solveGeneratedPuzzleBtn.ForeColor = System.Drawing.Color.Black;
             */
            // 
            // loadFileBtn
            // 
            this.loadFileBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.loadFileBtn.Location = new System.Drawing.Point(342, 230);
            this.loadFileBtn.Name = "loadFileBtn";
            this.loadFileBtn.Size = new System.Drawing.Size(60, 35);
            this.loadFileBtn.TabIndex = 11;
            this.loadFileBtn.Text = "Load File";
            this.loadFileBtn.UseVisualStyleBackColor = true;
            this.loadFileBtn.Click += new System.EventHandler(this.loadFileBtn_Click);
            this.loadFileBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.loadFileBtn.ForeColor = System.Drawing.Color.Black;
           
            // 
            // fileChooser
            // 
            this.fileChooser.FileName = "openFileDialog1";
            this.fileChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.fileChooser_FileOk);
            // 
            // difficultyDetermineBtn
            // 
            this.difficultyDetermineBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.difficultyDetermineBtn.Location = new System.Drawing.Point(162, 230);
            this.difficultyDetermineBtn.Name = "difficultyDetermineBtn";
            this.difficultyDetermineBtn.Size = new System.Drawing.Size(60, 35);
            this.difficultyDetermineBtn.TabIndex = 12;
            this.difficultyDetermineBtn.Text = "Difficulty";
            this.difficultyDetermineBtn.UseVisualStyleBackColor = true;
            this.difficultyDetermineBtn.Click += new System.EventHandler(this.difficultyDetermineBtn_Click);
            this.difficultyDetermineBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.difficultyDetermineBtn.ForeColor = System.Drawing.Color.Maroon;
            /*
            // 
            // validatePuzzleBtn
            // 
            this.validatePuzzleBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.validatePuzzleBtn.Location = new System.Drawing.Point(72, 230);
            this.validatePuzzleBtn.Name = "validatePuzzleBtn";
            this.validatePuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.validatePuzzleBtn.TabIndex = 13;
            this.validatePuzzleBtn.Text = "Validate";
            this.validatePuzzleBtn.UseVisualStyleBackColor = true;
            this.validatePuzzleBtn.Click += new System.EventHandler(this.validatePuzzleBtn_Click);
            this.validatePuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.validatePuzzleBtn.ForeColor = System.Drawing.Color.Black;
            */
            // 
            // newSolvePuzzleBtn
            // 
            this.newSolvePuzzleBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.newSolvePuzzleBtn.Location = new System.Drawing.Point(222, 230);
            this.newSolvePuzzleBtn.Name = "newSolvePuzzleBtn";
            this.newSolvePuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.newSolvePuzzleBtn.TabIndex = 14;
            this.newSolvePuzzleBtn.Text = "New Puzzle";
            this.newSolvePuzzleBtn.UseVisualStyleBackColor = true;
            this.newSolvePuzzleBtn.Click += new System.EventHandler(this.newSolvePuzzleBtn_Click);
            this.newSolvePuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.newSolvePuzzleBtn.ForeColor = System.Drawing.Color.Maroon;

            // 
            // clearPuzzleBtn
            // 
            this.clearPuzzleBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.clearPuzzleBtn.Location = new System.Drawing.Point(282, 230);
            this.clearPuzzleBtn.Name = "clearPuzzleBtn";
            this.clearPuzzleBtn.Size = new System.Drawing.Size(60, 35);
            this.clearPuzzleBtn.TabIndex = 15;
            this.clearPuzzleBtn.Text = "Clear";
            this.clearPuzzleBtn.UseVisualStyleBackColor = true;
            this.clearPuzzleBtn.Click += new System.EventHandler(this.clearPuzzleBtn_Click);
            this.clearPuzzleBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.clearPuzzleBtn.ForeColor = System.Drawing.Color.Maroon;

            //this.Controls.Add(this.validatePuzzleBtn);
            this.Controls.Add(this.difficultyDetermineBtn);
            this.Controls.Add(this.loadFileBtn);
            //this.Controls.Add(this.solveGeneratedPuzzleBtn);
            this.Controls.Add(this.newSolvePuzzleBtn);
            this.Controls.Add(this.clearPuzzleBtn);
            //this.Controls.SetChildIndex(this.solveGeneratedPuzzleBtn, 0);
            this.Controls.SetChildIndex(this.loadFileBtn, 1);
            this.Controls.SetChildIndex(this.difficultyDetermineBtn,2);
            //this.Controls.SetChildIndex(this.validatePuzzleBtn, 3);
            this.Controls.SetChildIndex(this.newSolvePuzzleBtn, 4);
            this.Controls.SetChildIndex(this.clearPuzzleBtn, 5);
        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solvePuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem levelsToolStripMenuItem;
        private Button submitPuzzleBtn;
        private Button submitLevelPuzzleBtn;
        //private Button solveGeneratedPuzzleBtn;
        private Button newPuzzleBtn;
        private Button loadFileBtn;
        private OpenFileDialog fileChooser;
        private Button difficultyDetermineBtn;
       // private Button validatePuzzleBtn;
        private Button hintsLevelBtn;
        private Button hintsRegionLevelBtn;
        private Button hintBtn;
        private Button hintsRegionBtn;
        private Button tipBtn;
        private Button clearPuzzleBtn;
        private Button newSolvePuzzleBtn;
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
        private Timer puzzleTimer;
        private TextBox timerText;
        private TextBox puzzlesInformationTb;
        private ToolStripMenuItem statisticsToolStripMenuItem;
        private TextBox staticsDispalyTb;
        private Button developmentBtn;
        private PictureBox logoStartUpPb;
        private TextBox solutionDisplayInfoTb;
    }
}

