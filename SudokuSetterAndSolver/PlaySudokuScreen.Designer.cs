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
            this.gameBanner = new System.Windows.Forms.PictureBox();
            this.mainMenuBtn = new System.Windows.Forms.Button();
            this.submitPuzzleBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // gameBanner
            // 
            this.gameBanner.Image = global::SudokuSetterAndSolver.Properties.Resources.SSSGameScreenFullBanner_fw;
            this.gameBanner.Location = new System.Drawing.Point(0, -2);
            this.gameBanner.Name = "gameBanner";
            this.gameBanner.Size = new System.Drawing.Size(1467, 250);
            this.gameBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameBanner.TabIndex = 7;
            this.gameBanner.TabStop = false;
            // 
            // mainMenuBtn
            // 
            this.mainMenuBtn.Location = new System.Drawing.Point(663, 896);
            this.mainMenuBtn.Name = "mainMenuBtn";
            this.mainMenuBtn.Size = new System.Drawing.Size(171, 93);
            this.mainMenuBtn.TabIndex = 8;
            this.mainMenuBtn.Text = "Main Menu";
            this.mainMenuBtn.UseVisualStyleBackColor = true;
            this.mainMenuBtn.Click += new System.EventHandler(this.mainMenuBtn_Click);
            // 
            // submitPuzzleBtn
            // 
            this.submitPuzzleBtn.Location = new System.Drawing.Point(663, 762);
            this.submitPuzzleBtn.Name = "submitPuzzleBtn";
            this.submitPuzzleBtn.Size = new System.Drawing.Size(171, 93);
            this.submitPuzzleBtn.TabIndex = 9;
            this.submitPuzzleBtn.Text = "Submit Puzzle";
            this.submitPuzzleBtn.UseVisualStyleBackColor = true;
            this.submitPuzzleBtn.Click += new System.EventHandler(this.submitPuzzleBtn_Click);
            // 
            // timer3
            // 
            this.timer3.Enabled = true;
            // 
            // PlaySudokuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1468, 1012);
            this.Controls.Add(this.submitPuzzleBtn);
            this.Controls.Add(this.mainMenuBtn);
            this.Controls.Add(this.gameBanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PlaySudokuScreen";
            this.Text = "PlaySudokuScreen";
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).EndInit();
            this.ResumeLayout(false);

        }
        //Stores all of the textboxes for the screen. 
        List<TextBox> listOfTextBoxes = new List<TextBox>();

        /// <summary>
        /// Method to create the 9*9 grid to display the loaded in puzzle. 
        /// </summary>
        /// <param name="gridSize"></param>
        private void CreateGrid(int gridSize)
        {
            //Creating the sudoku grid values. 
            loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirectoryLocation);
            //Temporary list that contains all of the values with the grid. 
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= loadedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                //Creating a textbox for the each cell, with the valid details. 
                TextBox txtBox = new TextBox();
                this.Controls.Add(txtBox);
                txtBox.Name = indexNumber.ToString();
                //txtBox.ReadOnly = true;
                txtBox.Size = new System.Drawing.Size(38, 38);
                txtBox.TabIndex = 0;
                txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                txtBox.Click += new System.EventHandler(this.puzzleSquareClick);
                txtBox.TextChanged += new System.EventHandler(this.puzzleTextChange);
                //Key press handler to only allow digits 1-9 in the textboxes. 
                txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckCellEntry);
                //Limiting the text box to only on character. 
                txtBox.MaxLength = 1;
                //Setting the value in the grid text box. 
                txtBox.Text = loadedPuzzle.puzzlecells[indexNumber].value.ToString();

                //Clouring 
                txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                txtBox.ForeColor = Color.Black;

                switch (loadedPuzzle.puzzlecells[indexNumber].blocknumber)
                {
                    case (0):
                    case (8):
                        txtBox.BackColor = Color.LightGreen;
                        break;
                    case (1):
                    case (7):
                        txtBox.BackColor = Color.Pink;
                        break;
                    case (2):
                    case (6):
                        txtBox.BackColor = Color.LightCyan;
                        break;
                    case (3):
                    case (5):
                        txtBox.BackColor = Color.LightYellow;
                        break;
                    default:
                        txtBox.BackColor = Color.LightBlue;
                        break;
                }

                //Ensuring static numbers can not be edited. 
                if (loadedPuzzle.puzzlecells[indexNumber].value != 0)
                {
                    txtBox.Enabled = false;
                }
                else
                {
                    txtBox.Text = "";
                }
                //Position logic
                if (indexNumber == 0)
                {
                    rowLocation = rowLocation + 133;
                    columnLocation = columnLocation + 120;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                else if (indexNumber == 8 || indexNumber % 9 == 8)
                {
                    rowLocation += 38;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                    rowLocation = 95;
                    columnLocation += 17;
                }
                else
                {
                    rowLocation += 38;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }

                listOfTextBoxes.Add(txtBox);
            }
        }

        #endregion

        private System.Windows.Forms.PictureBox gameBanner;
        private System.Windows.Forms.Button mainMenuBtn;
        private Button submitPuzzleBtn;
        private Timer timer1;
        private Timer timer2;
        private Timer timer3;
    }
}