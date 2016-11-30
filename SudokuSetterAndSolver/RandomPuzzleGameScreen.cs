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

    //Maybe create a sudoku grid class, to create the grids and populate from there. 
    public partial class RandomPuzzleGameScreen : Form
    {
        #region Objects 
        SudokuPuzzleGenerator sudokuGridGenerator = new SudokuPuzzleGenerator(9);
        TextBox currentSelectedTextBox = new TextBox();
        public List<TextBox> listOfTextBoxes = new List<TextBox>();
        SudokuSolver sudokuSolver = new SudokuSolver();
        puzzle generatedPuzzle = new puzzle();

        List<int> sudokuSolutionArray = new List<int>();
        #endregion

        #region Global Variables 
        //Contains the solution to the puzzle on the screen. 
        int[,] sudokuPuzzleSolution;
        //Contains the puzzle when the user presses submit.      
        int[,] submittedPuzzle;
        //Creating the sudoku grid values. 
        int[,] sudokuGrid;

        #endregion

        #region Constructor 
        public RandomPuzzleGameScreen(int gridSize)
        {
            gridSize = 9;
            sudokuPuzzleSolution = new int[gridSize, gridSize];
            submittedPuzzle = new int[gridSize, gridSize];
            sudokuGrid = new int[gridSize, gridSize];
            InitializeComponent();
            CreateGrid(9);
        }

        #endregion

        #region Event Methods 

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

        private void submitPuzzleBtn_Click(object sender, EventArgs e)
        {
            for (int indexNumber = 0; indexNumber <= generatedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                string cellName = generatedPuzzle.puzzlecells[indexNumber].rownumber.ToString() + generatedPuzzle.puzzlecells[indexNumber].columnnumber.ToString();
                foreach (var textBox in listOfTextBoxes)
                {
                    if (cellName == textBox.Name)
                    {
                        if (textBox.Text != "")
                        {
                            generatedPuzzle.puzzlecells[indexNumber].value = Int32.Parse(textBox.Text);
                        }
                        else
                        {
                            generatedPuzzle.puzzlecells[indexNumber].value = 0;
                        }
                    }
                }
            }

            bool correctPuzzle = CheckSubmittedPuzzleXML();

            if (correctPuzzle == true)
            {
                MessageBox.Show("Puzzle Completed! Well Done!");
            }
            else
            {
                MessageBox.Show("Puzzle incorrect. Please try again.");
            }
        }

        private bool CheckSubmittedPuzzleXML()
        {
            for (int indexValue = 0; indexValue <= generatedPuzzle.puzzlecells.Count - 1; indexValue++)
            {
                if(generatedPuzzle.puzzlecells[indexValue].value != sudokuSolutionArray[indexValue])
                {
                    return false; 
                }
            }
            return true; 
        }

        #endregion

        #region Method
        private bool CheckSubmittedPuzzle()
        {
            //Goes through all of the values within the puzzle. 
            for (int checkRowNumber = 0; checkRowNumber <= 8; checkRowNumber++)
            {
                for (int checkColumnNumber = 0; checkColumnNumber <= 8; checkColumnNumber++)
                {
                    if (submittedPuzzle[checkRowNumber, checkColumnNumber] != sudokuPuzzleSolution[checkRowNumber, checkColumnNumber])
                    {
                        return false;  //If ther solution is not correct. 
                    }
                }
            }

            return true;
        }
        #endregion

        private void solveGeneratedPuzzleBtn_Click(object sender, EventArgs e)
        {
            sudokuSolver.currentPuzzleToBeSolved = generatedPuzzle;
            bool puzzleSolved = sudokuSolver.BacktrackingUsingXmlTemplateFile(false);
            generatedPuzzle = sudokuSolver.currentPuzzleToBeSolved;

            if (puzzleSolved == true)
            {
                foreach (var cell in generatedPuzzle.puzzlecells)
                {
                    foreach (var textBoxCurrent in listOfTextBoxes)
                    {
                        if (textBoxCurrent.Name == cell.rownumber.ToString() + cell.columnnumber.ToString())
                        {
                            textBoxCurrent.Text = cell.value.ToString();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No solution, invalid solution");
            }

        }
        private void GetPuzzle()
        {
            //Creating the grid from the entered numbers. 
            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    //submittedPuzzle 
                    string cellName = rowNumber.ToString() + columnNumber.ToString();
                    foreach (var textBox in listOfTextBoxes)
                    {
                        if (cellName == textBox.Name)
                        {
                            if (textBox.Text != "")
                            {
                                submittedPuzzle[rowNumber, columnNumber] = Int32.Parse(textBox.Text);
                            }
                            else
                            {
                                submittedPuzzle[rowNumber, columnNumber] = 0;
                            }

                        }
                    }
                }
            }
            sudokuSolver.sudokuPuzzleMultiExample = submittedPuzzle;
            bool solved = sudokuSolver.BacktrackinEffcient(false);
            if (solved == true)
            {
                SetGridSolved();
            }
        }

        private void SetGridSolved()
        {
            //Creating the grid from the entered numbers. 
            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    //submittedPuzzle 
                    string cellName = rowNumber.ToString() + columnNumber.ToString();
                    foreach (var textBox in listOfTextBoxes)
                    {
                        if (cellName == textBox.Name)
                        {
                            textBox.Text = submittedPuzzle[rowNumber, columnNumber].ToString();
                        }
                    }
                }
            }
        }

        private void PopulateNewPuzzle()
        {
            for (int rowNumberNew = 0; rowNumberNew <= 8; rowNumberNew++)
            {
                for (int columnNumberNew = 0; columnNumberNew <= 8; columnNumberNew++)
                {
                    string cellName = rowNumberNew.ToString() + columnNumberNew.ToString();
                    foreach (var textBox in listOfTextBoxes)
                    {
                        textBox.Enabled = true;
                        if (cellName == textBox.Name)
                        {
                            if (sudokuGrid[rowNumberNew, columnNumberNew] == 0)
                            {
                                textBox.Text = "";                        
                            }
                            else
                            {
                                textBox.Enabled = false;
                                textBox.Text = sudokuGrid[rowNumberNew, columnNumberNew].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void ClearGrid()
        {
            //currently remove all textboxes when a new puzzle is selected, this may need to be changed. 
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Dispose();
            }
        }

        private void ClearSudokuGridVaribale()
        {
            for (int clearRowNumber = 0; clearRowNumber <= 8; clearRowNumber++)
            {
                for (int clearColumnNumber = 0; clearColumnNumber <= 8; clearColumnNumber++)
                {
                    sudokuGrid[clearRowNumber, clearColumnNumber] = 0;
                }
            }
        }

        private void newPuzzleBtn_Click(object sender, EventArgs e)
        {
            ClearGrid();
            generatedPuzzle.puzzlecells.Clear();
            CreateGrid(9);
        }

        private int[,] ConvertListToMultiDimensionalArray(List<int> puzzleInList, int gridSize)
        {
            int[,] puzzleArray = new int[gridSize, gridSize];
            int rowNumber = 0;
            int columnNumber = 0;

            for (int cellNumber = 0; cellNumber <= puzzleInList.Count - 1; cellNumber++)
            {
                puzzleArray[rowNumber, columnNumber] = puzzleInList[cellNumber];
                if (columnNumber == 8 || columnNumber % 9 == 8)
                {
                    rowNumber++;
                    columnNumber = 0;
                }
                else
                {
                    columnNumber++;
                }
            }


            return puzzleArray;
        }
    }
}
