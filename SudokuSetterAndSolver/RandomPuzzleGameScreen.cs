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

    //For the irregulat puzzlesm will have template puzzles that will be loaded in, these puzzle templates will then be solved. 

    //Maybe create a sudoku grid class, to create the grids and populate from there. 
    public partial class RandomPuzzleGameScreen : Form
    {
        #region Objects 
        SudokuPuzzleGenerator sudokuGridGenerator = new SudokuPuzzleGenerator(9);
        TextBox currentSelectedTextBox = new TextBox();
        public List<TextBox> listOfTextBoxes = new List<TextBox>();
        SudokuSolver sudokuSolver = new SudokuSolver();
        puzzle generatedPuzzle = new puzzle();

        #endregion

        #region Global Variables 
        //Contains the solution to the puzzle on the screen. 
        //Creating the sudoku grid values. 
        int[,] sudokuGrid;
        List<int> sudokuSolutionArray = new List<int>();
        int _puzzleSelection = 0;
        #endregion

        #region Constructor 
        public RandomPuzzleGameScreen(int gridSize, int puzzleSelection)
        {
            _puzzleSelection = puzzleSelection;
            CreateSudokuGrid();
            //gridSize = 9;
            //sudokuGrid = new int[gridSize, gridSize];
            InitializeComponent();
            if (generatedPuzzle.gridsize == 9)
            {
                GenerateStandardSudokuPuzzle();
            }
            else if(generatedPuzzle.gridsize ==16)
            {
                GenerateLargeSudokuPuzzle(1);
            }
            else
            {
                GenerateSmallSudokuPuzzle();
            }
            //CreateGrid(9);
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
                if (generatedPuzzle.puzzlecells[indexValue].value != sudokuSolutionArray[indexValue])
                {
                    return false;
                }
            }
            return true;
        }

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

        private void newPuzzleBtn_Click(object sender, EventArgs e)
        {
            ClearGrid();
            generatedPuzzle.puzzlecells.Clear();
            CreateGrid(9);
        }
        #endregion

        #region Methods 

        private void ClearGrid()
        {
            //currently remove all textboxes when a new puzzle is selected, this may need to be changed. 
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Dispose();
            }
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

        #endregion

        #region Generate Blank Puzzle 
        /// <summary>
        /// This method generates a blank puzzle dependant on the grid size. 
        /// </summary>
        private void CreateSudokuGrid()
        {
            if (_puzzleSelection == 2 || _puzzleSelection == 0)
            {
                generatedPuzzle.gridsize = 9;
            }
            else if (_puzzleSelection == 1)
            {
                generatedPuzzle.gridsize = 16;
            }
            else
            {
                generatedPuzzle.gridsize = 4;
            }

            if (_puzzleSelection != 0)
            {
                GenerateBlankGridStandardSudoku();
            }
        }

        private void GenerateBlankGridStandardSudoku()
        {
            for (int puzzleRowNumber = 0; puzzleRowNumber <= generatedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= generatedPuzzle.gridsize - 1; puzzleColumnNumber++)
                {
                    puzzleCell tempPuzzleCell = new puzzleCell();
                    tempPuzzleCell.rownumber = puzzleRowNumber;
                    tempPuzzleCell.columnnumber = puzzleColumnNumber;
                    if (generatedPuzzle.gridsize == 4)
                    {
                        tempPuzzleCell.blocknumber = GetBlockFour(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else if (generatedPuzzle.gridsize == 9)
                    {
                        tempPuzzleCell.blocknumber = GetBlockNumberNine(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else
                    {
                        tempPuzzleCell.blocknumber = GetBlocNumberSixteen(puzzleRowNumber, puzzleColumnNumber);
                    }
                    generatedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }
        #endregion

        #region Get Blocks Methods 
        private int GetBlockFour(int tempRowNumber, int tempColumnNumber)
        {
            if (tempRowNumber <= 1 && tempColumnNumber <= 1)
            {
                return 0;
            }
            else if (tempRowNumber <= 1 && tempColumnNumber >= 2)
            {
                return 1;
            }
            else if (tempRowNumber >= 2 && tempRowNumber <= 1)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }


        private int GetBlockNumberNine(int tempRowNumber, int tempColumnNumber)
        {
            double blockValue = Math.Sqrt(generatedPuzzle.gridsize);
            if (tempRowNumber <= 2 && tempColumnNumber <= 2)
            {
                return 0;
            }
            else if (tempRowNumber <= 2 && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 1;
            }
            else if (tempRowNumber <= 2 && (tempColumnNumber >= 6 && tempColumnNumber <= 8))
            {
                return 2;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && tempColumnNumber <= 2)
            {
                return 3;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 4;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && (tempColumnNumber >= 6 && tempColumnNumber <= 8))
            {
                return 5;
            }
            else if ((tempRowNumber >= 6 && tempRowNumber <= 8) && tempColumnNumber <= 2)
            {
                return 6;
            }
            else if ((tempRowNumber >= 6 && tempRowNumber <= 8) && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 7;
            }
            else
            {
                return 8;
            }

        }

        private int GetBlocNumberSixteen(int tempRowNumber, int tempColumnNumber)
        {
            if (tempRowNumber <= 3 && tempColumnNumber <= 3)
            {
                return 0;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 1;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 2;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 3;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && tempColumnNumber <= 3)
            {
                return 4;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 5;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 6;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 7;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && tempColumnNumber <= 3)
            {
                return 8;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 9;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 10;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 11;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && tempColumnNumber <= 3)
            {
                return 12;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 13;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 14;
            }
            else
            {
                return 15;
            }

        }

        #endregion
    }
}
