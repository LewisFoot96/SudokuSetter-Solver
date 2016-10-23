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
            CreateGrid(9);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1463, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(205, 141);
            this.button1.TabIndex = 85;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RandomPuzzleGameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1724, 930);
            this.Controls.Add(this.button1);
            this.Name = "RandomPuzzleGameScreen";
            this.Text = "RandomPuzzleGameScreen";
            this.ResumeLayout(false);

        }

        private void CreateGrid(int gridSize)
        {
            //Creating the sudoku grid values. 
            int[,] sudokuGrid = new int[gridSize, gridSize];
            SudokuPuzzleGenerator sudokuGridGenerator = new SudokuPuzzleGenerator(9);
            sudokuGrid= sudokuGridGenerator.CreateSudokuGrid();

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
                    txtBox.ReadOnly = true;
                    txtBox.Size = new System.Drawing.Size(70, 38);
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
                    //Ensuring static numbers can not be edited. 
                    if(sudokuGrid[i, j] !=0)
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
                        rowLocation = rowLocation + 70;
                        columnLocation = columnLocation + 50;              
                    }
                    else
                    {
                        rowLocation = rowLocation + 70;
                    }
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                rowLocation = 0;
                columnLocation = columnLocation + 45;
            }
        }

        #endregion
        private System.Windows.Forms.Button button1;
    }
}