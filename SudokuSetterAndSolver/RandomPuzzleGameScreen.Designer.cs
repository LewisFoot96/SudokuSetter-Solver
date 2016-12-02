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
            this.mainMenuBtn = new System.Windows.Forms.Button();
            this.gameBanner = new System.Windows.Forms.PictureBox();
            this.submitPuzzleBtn = new System.Windows.Forms.Button();
            this.solveGeneratedPuzzleBtn = new System.Windows.Forms.Button();
            this.newPuzzleBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenuBtn
            // 
            this.mainMenuBtn.Location = new System.Drawing.Point(672, 896);
            this.mainMenuBtn.Name = "mainMenuBtn";
            this.mainMenuBtn.Size = new System.Drawing.Size(167, 100);
            this.mainMenuBtn.TabIndex = 85;
            this.mainMenuBtn.Text = "Main Menu ";
            this.mainMenuBtn.UseVisualStyleBackColor = true;
            this.mainMenuBtn.Click += new System.EventHandler(this.mainMenuBtn_Click);
            // 
            // gameBanner
            // 
            this.gameBanner.Image = global::SudokuSetterAndSolver.Properties.Resources.SSSGameScreenFullBanner_fw;
            this.gameBanner.Location = new System.Drawing.Point(0, -2);
            this.gameBanner.Name = "gameBanner";
            this.gameBanner.Size = new System.Drawing.Size(1467, 250);
            this.gameBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameBanner.TabIndex = 86;
            this.gameBanner.TabStop = false;
            // 
            // submitPuzzleBtn
            // 
            this.submitPuzzleBtn.Location = new System.Drawing.Point(672, 770);
            this.submitPuzzleBtn.Name = "submitPuzzleBtn";
            this.submitPuzzleBtn.Size = new System.Drawing.Size(167, 100);
            this.submitPuzzleBtn.TabIndex = 87;
            this.submitPuzzleBtn.Text = "Submit Puzzle";
            this.submitPuzzleBtn.UseVisualStyleBackColor = true;
            this.submitPuzzleBtn.Click += new System.EventHandler(this.submitPuzzleBtn_Click);
            // 
            // solveGeneratedPuzzleBtn
            // 
            this.solveGeneratedPuzzleBtn.Location = new System.Drawing.Point(899, 851);
            this.solveGeneratedPuzzleBtn.Name = "solveGeneratedPuzzleBtn";
            this.solveGeneratedPuzzleBtn.Size = new System.Drawing.Size(171, 102);
            this.solveGeneratedPuzzleBtn.TabIndex = 88;
            this.solveGeneratedPuzzleBtn.Text = "Solve";
            this.solveGeneratedPuzzleBtn.UseVisualStyleBackColor = true;
            this.solveGeneratedPuzzleBtn.Click += new System.EventHandler(this.solveGeneratedPuzzleBtn_Click);
            // 
            // newPuzzleBtn
            // 
            this.newPuzzleBtn.Location = new System.Drawing.Point(451, 851);
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
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1464, 1012);
            this.Controls.Add(this.newPuzzleBtn);
            this.Controls.Add(this.solveGeneratedPuzzleBtn);
            this.Controls.Add(this.submitPuzzleBtn);
            this.Controls.Add(this.gameBanner);
            this.Controls.Add(this.mainMenuBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RandomPuzzleGameScreen";
            this.Text = "RandomPuzzleGameScreen";
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).EndInit();
            this.ResumeLayout(false);
        }

        private void CreateGrid(int gridSize)
        {
            sudokuGridGenerator.generatedPuzzle = generatedPuzzle;
            generatedPuzzle = sudokuGridGenerator.CreateSudokuGridXML();

            sudokuSolutionArray = sudokuGridGenerator.orginalSolution;

            List<int> listOfSudokuValues = new List<int>();

            foreach (var cell in generatedPuzzle.puzzlecells)
            {
                listOfSudokuValues.Add(cell.value);
            }

            sudokuGrid = ConvertListToMultiDimensionalArray(listOfSudokuValues, 9);

            int rowLocation = 0, columnLocation = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    //Creating a textbox for the each cell, with the valid details. 
                    TextBox txtBox = new TextBox();
                    this.Controls.Add(txtBox);
                    txtBox.Name = i.ToString() + j.ToString();
                    txtBox.ReadOnly = true;
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
                    txtBox.Text = sudokuGrid[i, j].ToString();

                    //Clouring 
                    txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                    txtBox.ForeColor = Color.Black;
                    if ((i <= 2 && j <= 2) || (i >= 6 && j >= 6))
                    {
                        txtBox.BackColor = Color.LightGreen;
                    }
                    else if ((i <= 2 && (j >= 3 && j <= 5)) || (i >= 6 && (j >= 3 && j <= 5)))
                    {
                        txtBox.BackColor = Color.Pink;
                    }
                    else if ((i <= 2 && j >= 6) || (i >= 6 && j <= 2))
                    {
                        txtBox.BackColor = Color.LightCyan;
                    }
                    else if (((i >= 3 && i <= 5) && j <= 2) || ((i >= 3 && i <= 5) && j >= 6))
                    {
                        txtBox.BackColor = Color.LightYellow;
                    }
                    else
                    {
                        txtBox.BackColor = Color.LightBlue;
                    }

                    //Ensuring static numbers can not be edited. 
                    if (sudokuGrid[i, j] != 0)
                    {
                        txtBox.Enabled = false;
                    }
                    else
                    {
                        txtBox.Text = "";
                    }
                    //Position logic
                    if (i == 0 && j == 0)
                    {
                        rowLocation = rowLocation + 118;
                        columnLocation = columnLocation + 120;
                    }
                    else
                    {
                        rowLocation = rowLocation + 38;
                    }
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                    listOfTextBoxes.Add(txtBox);
                }
                rowLocation = 80;
                columnLocation = columnLocation + 17;
            }
        }

        private void GenerateStandardSudokuPuzzle()
        {
            sudokuGridGenerator.generatedPuzzle = generatedPuzzle;
            sudokuGridGenerator.CreateSudokuGridXML();
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= generatedPuzzle.puzzlecells.Count - 1; indexNumber++)
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
                txtBox.Text = generatedPuzzle.puzzlecells[indexNumber].value.ToString();

                //Clouring 
                txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                txtBox.ForeColor = Color.Black;

                switch (generatedPuzzle.puzzlecells[indexNumber].blocknumber)
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
                if (generatedPuzzle.puzzlecells[indexNumber].value != 0)
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
                    rowLocation = rowLocation + 118;
                    columnLocation = columnLocation + 120;
                }
                else if (indexNumber == 8 || indexNumber % 9 == 8)
                {
                    columnLocation = columnLocation + 17;                
                    rowLocation = 80;
                }
                else
                {
                    rowLocation += 38;
                }
                txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                listOfTextBoxes.Add(txtBox);
            }
        }

        private void GenerateLargeSudokuPuzzle()
        {
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= generatedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                //Creating a textbox for the each cell, with the valid details. 
                TextBox txtBox = new TextBox();
                this.Controls.Add(txtBox);
                txtBox.Name = indexNumber.ToString();
                txtBox.Size = new System.Drawing.Size(20, 20);
                txtBox.TabIndex = 0;
                txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                txtBox.Font = new Font(txtBox.Font.FontFamily, 10);
                txtBox.Click += new System.EventHandler(this.puzzleSquareClick);
                txtBox.TextChanged += new System.EventHandler(this.puzzleTextChange);
                //Key press handler to only allow digits 1-9 in the textboxes. 
                txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckCellEntry);
                //Limiting the text box to only on character. 
                txtBox.MaxLength = 1;
                //Setting the value in the grid text box. 
                txtBox.Text = generatedPuzzle.puzzlecells[indexNumber].value.ToString();

                //Clouring 
                txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                txtBox.ForeColor = Color.Black;

                switch (generatedPuzzle.puzzlecells[indexNumber].blocknumber)
                {
                    case (0):
                    case (3):
                    case (12):
                    case (15):
                        txtBox.BackColor = Color.LightGreen;
                        break;
                    case (1):
                    case (4):
                    case (14):
                    case (11):
                        txtBox.BackColor = Color.Pink;
                        break;
                    case (2):
                    case (8):
                    case (7):
                    case (13):
                        txtBox.BackColor = Color.LightYellow;
                        break;
                    default:
                        txtBox.BackColor = Color.LightBlue;
                        break;
                }

                //Ensuring static numbers can not be edited. 
                if (generatedPuzzle.puzzlecells[indexNumber].value != 0)
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
                    rowLocation = rowLocation + 80;
                    columnLocation = columnLocation + 110;                
                }
                else if (indexNumber == 15 || indexNumber % 16 == 15)
                {
                    columnLocation += 10;
                    rowLocation = 60;
                }
                else
                {
                    rowLocation += 20;
                }
                txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                listOfTextBoxes.Add(txtBox);
            }

        }

        private void GenerateSmallSudokuPuzzle()
        {
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= generatedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                //Creating a textbox for the each cell, with the valid details. 
                TextBox txtBox = new TextBox();
                this.Controls.Add(txtBox);
                txtBox.Name = indexNumber.ToString();
                //txtBox.ReadOnly = true;
                txtBox.Size = new System.Drawing.Size(46,46);
                txtBox.TabIndex = 0;
                txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                txtBox.Font = new Font(txtBox.Font.FontFamily, 18);
                txtBox.Click += new System.EventHandler(this.puzzleSquareClick);
                txtBox.TextChanged += new System.EventHandler(this.puzzleTextChange);
                //Key press handler to only allow digits 1-9 in the textboxes. 
                txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckCellEntry);
                //Limiting the text box to only on character. 
                txtBox.MaxLength = 1;
                //Setting the value in the grid text box. 
                txtBox.Text = generatedPuzzle.puzzlecells[indexNumber].value.ToString();

                //Clouring 
                txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                txtBox.ForeColor = Color.Black;

                switch (generatedPuzzle.puzzlecells[indexNumber].blocknumber)
                {
                    case (0):
                        txtBox.BackColor = Color.LightGreen;
                        break;
                    case (1):
                        txtBox.BackColor = Color.Pink;
                        break;
                    case (2):
                        txtBox.BackColor = Color.LightCyan;
                        break;
                    case (3):
                        txtBox.BackColor = Color.LightYellow;
                        break;
                    default:
                        txtBox.BackColor = Color.LightBlue;
                        break;
                }

                //Ensuring static numbers can not be edited. 
                if (generatedPuzzle.puzzlecells[indexNumber].value != 0)
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
                    rowLocation = rowLocation + 180;
                    columnLocation = columnLocation + 110;
                }
                else if (indexNumber == 3 || indexNumber % 4 == 3)
                {

                    rowLocation = 134;
                    columnLocation += 23;
                }
                else
                {
                    rowLocation += 46;
                    
                }
                txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                listOfTextBoxes.Add(txtBox);
            }
        }

        #endregion
        private System.Windows.Forms.Button mainMenuBtn;
        private PictureBox gameBanner;
        private Button submitPuzzleBtn;
        private Button solveGeneratedPuzzleBtn;
        private Button newPuzzleBtn;
    }
}