using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    public partial class SolveSudokuScreen : Form
    {
        #region Field Variables 
        TextBox currentSelectedTextBox = new TextBox();
        SudokuSolver sudokuSolver = new SudokuSolver();
        int[,] sudokuGrid;
        string fileDirctoryLocation = "";
        PuzzleManager puzzleManager = new PuzzleManager();
        puzzle loadedPuzzle = new puzzle();

        #endregion


        #region Constructor 
        public SolveSudokuScreen()
        {
            InitializeComponent();
            CreateGrid(9);
            sudokuGrid = new int[9, 9];
        }
        #endregion

        #region Event Handlers Methods 

        private void puzzleTextChange(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
        }
        private void puzzleSquareClick(object sender, EventArgs e)
        {
            //Disabling the previous textbox
            currentSelectedTextBox.ReadOnly = true;
            var tb = (TextBox)sender; //Attaining the textbox that has just been clicked. 
            tb.ReadOnly = false;

            //Setting the selected textbox as the current one. 
            currentSelectedTextBox = tb;
        }

        /// <summary>
        /// Method to handel the key presses witin the sudoku cells, to ensure only number 1-9 are entrered. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckCellEntry(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Cell entry validation, if the entered character is between 1 - 9, then enter it into the text box. 
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        /// <summary>
        /// If the users wish to try and solve the puzzle they have entered. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void solveBtn_Click(object sender, EventArgs e)
        {
            //In here need to make a call to the recursive backtracking algorithm, to solve the puzzle that the user has entered.
            //The puzzle solving should have a time out on it, if this time out is past, the puzzle is deemed unsolavable. 
            sudokuSolver.sudokuPuzzleMultiExample = sudokuGrid;
            bool solved = sudokuSolver.solvePuzzle(fileDirctoryLocation);
            sudokuGrid = sudokuSolver.sudokuPuzzleMultiExample;
            if (solved == true)
            {
                //Creating the grid from the entered numbers. 
                for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
                {
                    for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                    {
                        string cellName = rowNumber.ToString() + columnNumber.ToString();
                        foreach (var textBox in listOfTextBoxes)
                        {
                            if (cellName == textBox.Name)
                            {
                                if (textBox.Text == "")
                                {
                                    textBox.Text = sudokuGrid[rowNumber, columnNumber].ToString();
                                }
                                
                            }
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("No solution, invalid solution");
            }
            }

        private void loadFileBtn_Click(object sender, EventArgs e)
        {
            fileChooser.ShowDialog();
        }

        private void fileChooser_FileOk(object sender, CancelEventArgs e)
        {
            ClearGrid();
            fileDirctoryLocation = fileChooser.FileName;
            loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirctoryLocation);

            int[] puzzleArray = loadedPuzzle.puzzlecells.Cast<int>().ToArray();
            sudokuGrid = puzzleManager.ConvertArrayToMultiDimensionalArray(puzzleArray);
            SetLoadedGrid();
        }

        private void SetLoadedGrid()
        {
            //Creating the grid from the entered numbers. 
            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    string cellName = rowNumber.ToString() + columnNumber.ToString();
                    foreach (var textBox in listOfTextBoxes)
                    {
                        if (cellName == textBox.Name)
                        {
                            //Enures zeros aren't entered into the grid. 
                            if (sudokuGrid[rowNumber, columnNumber] != 0)
                            {
                                textBox.Text = sudokuGrid[rowNumber, columnNumber].ToString();
                            }
                            

                        }
                    }

                }
            }
        }

        private void ClearGrid()
        {
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Clear();
            }
        }

    }

        #endregion 
}
