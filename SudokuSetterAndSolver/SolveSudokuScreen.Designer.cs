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

        public List<TextBox> listOfTextBoxes = new List<TextBox>();

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
            this.gameBanner = new System.Windows.Forms.PictureBox();
            this.mainMenuBtn = new System.Windows.Forms.Button();
            this.solveBtn = new System.Windows.Forms.Button();
            this.loadFileBtn = new System.Windows.Forms.Button();
            this.fileChooser = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // gameBanner
            // 
            this.gameBanner.Image = global::SudokuSetterAndSolver.Properties.Resources.SSSGameScreenFullBanner_fw;
            this.gameBanner.Location = new System.Drawing.Point(1, -2);
            this.gameBanner.Name = "gameBanner";
            this.gameBanner.Size = new System.Drawing.Size(1467, 250);
            this.gameBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameBanner.TabIndex = 7;
            this.gameBanner.TabStop = false;
            // 
            // mainMenuBtn
            // 
            this.mainMenuBtn.ForeColor = System.Drawing.SystemColors.InfoText;
            this.mainMenuBtn.Location = new System.Drawing.Point(652, 907);
            this.mainMenuBtn.Name = "mainMenuBtn";
            this.mainMenuBtn.Size = new System.Drawing.Size(171, 93);
            this.mainMenuBtn.TabIndex = 9;
            this.mainMenuBtn.Text = "Main Menu";
            this.mainMenuBtn.UseVisualStyleBackColor = true;
            this.mainMenuBtn.Click += new System.EventHandler(this.mainMenuBtn_Click);
            // 
            // solveBtn
            // 
            this.solveBtn.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.solveBtn.Location = new System.Drawing.Point(652, 787);
            this.solveBtn.Name = "solveBtn";
            this.solveBtn.Size = new System.Drawing.Size(171, 93);
            this.solveBtn.TabIndex = 10;
            this.solveBtn.Text = "Solve";
            this.solveBtn.UseVisualStyleBackColor = true;
            this.solveBtn.Click += new System.EventHandler(this.solveBtn_Click);
            // 
            // loadFileBtn
            // 
            this.loadFileBtn.Location = new System.Drawing.Point(904, 846);
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
            // SolveSudokuScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1468, 1012);
            this.Controls.Add(this.loadFileBtn);
            this.Controls.Add(this.solveBtn);
            this.Controls.Add(this.mainMenuBtn);
            this.Controls.Add(this.gameBanner);
            this.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SolveSudokuScreen";
            this.Text = "SolveSudokuScreen";
            ((System.ComponentModel.ISupportInitialize)(this.gameBanner)).EndInit();
            this.ResumeLayout(false);

        }

        private void CreateGrid(int gridSize)
        {
            listOfTextBoxes.Clear();
            //Creating the sudoku grid values. 
            int[,] sudokuGrid = new int[gridSize, gridSize];


            //Creating and popualting the grid with the values attained. 
            int[,] gridMultiDimensionalArray = new int[gridSize, gridSize];
            int rowLocation = 0, columnLocation = 0;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    //Creating a textbox for the each cell, with the valid details. 
                    TextBox txtBox = new TextBox();
                    this.Controls.Add(txtBox);
                    txtBox.Name = i.ToString() + j.ToString();
                    txtBox.ReadOnly = false;
                    txtBox.Size = new System.Drawing.Size(38, 38);
                    txtBox.TabIndex = 0;
                    txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                    txtBox.Click += new System.EventHandler(this.puzzleSquareClick);
                    txtBox.TextChanged += new System.EventHandler(this.puzzleTextChange);
                    //Key press handler to only allow digits 1-9 in the textboxes. 
                    txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckCellEntry);
                    //Limiting the text box to only on character. 
                    txtBox.MaxLength = 1;

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

        #endregion

        private System.Windows.Forms.PictureBox gameBanner;
        private System.Windows.Forms.Button mainMenuBtn;
        private Button solveBtn;
        private Button loadFileBtn;
        private OpenFileDialog fileChooser;
    }
}