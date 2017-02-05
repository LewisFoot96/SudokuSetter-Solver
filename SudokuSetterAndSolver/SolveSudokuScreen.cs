using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        string fileDirctoryLocation = "";
        PuzzleManager puzzleManager = new PuzzleManager();
        puzzle loadedPuzzle = new puzzle();
        public static int _puzzleSelection;
        #endregion

        #region Constructor 
        public SolveSudokuScreen()
        {
            //need to handle the differne puzzles based on the pop up box 
            InitializeComponent();
            //Generate Blank Grid. 

            if (_puzzleSelection == 0)
            {
                loadedPuzzle.gridsize = 16;
            }
            else if (_puzzleSelection == 1)
            {

                loadedPuzzle.gridsize = 9;
            }
            else
            {
                loadedPuzzle.gridsize = 4;
            }


            loadedPuzzle.type = "regualr";
            GenerateBlankGridStandardSudoku();
            if (_puzzleSelection == 0)
            {
                GenerateLargeSudokuPuzzle();

            }
            else if (_puzzleSelection == 1)
            {
                GenerateStandardSudokuPuzzle();
            }
            else
            {
                GenerateSmallSudokuPuzzle();
            }
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

            sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;
            Stopwatch tempStopWatch = new Stopwatch();
            tempStopWatch.Reset();
            tempStopWatch.Start();
            bool puzzleSolved = sudokuSolver.SolveSudokuRuleBasedXML();
            Console.WriteLine(tempStopWatch.Elapsed.TotalSeconds);
            Console.WriteLine(tempStopWatch.Elapsed.TotalMilliseconds);
            tempStopWatch.Stop();
            loadedPuzzle = sudokuSolver.currentPuzzleToBeSolved;

            if (puzzleSolved == true)
            {

                for (int cellNumberCount = 0; cellNumberCount <= loadedPuzzle.puzzlecells.Count - 1; cellNumberCount++)
                {
                    foreach (var textBoxCurrent in listOfTextBoxes)
                    {
                        if (textBoxCurrent.Name == cellNumberCount.ToString())
                        {
                            textBoxCurrent.Text = loadedPuzzle.puzzlecells[cellNumberCount].value.ToString();
                            break;
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
            ClearTextBoxesGrid();
            listOfTextBoxes.Clear();
            fileDirctoryLocation = fileChooser.FileName;
            loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirctoryLocation);

            List<int> listOfSudokuValues = new List<int>();

            foreach (var cell in loadedPuzzle.puzzlecells)
            {
                listOfSudokuValues.Add(cell.value);
            }
            if (loadedPuzzle.gridsize == 9)
            {
                GenerateStandardSudokuPuzzle();
            }
            else if (loadedPuzzle.gridsize == 16)
            {
                GenerateLargeSudokuPuzzle();
            }
            else
            {
                GenerateSmallSudokuPuzzle();
            }
        }

        private void ClearTextBoxesGrid()
        {
            //currently remove all textboxes when a new puzzle is selected, this may need to be changed. 
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Dispose();
            }
        }

        private void difficultyDetermineBtn_Click(object sender, EventArgs e)
        {
            sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;

            sudokuSolver.EvaluatePuzzleDifficulty();
            bool puzzleSolved = sudokuSolver.BacktrackingUsingXmlTemplateFile(false);
            loadedPuzzle = sudokuSolver.currentPuzzleToBeSolved;

            for (int cellNumberCount = 0; cellNumberCount <= loadedPuzzle.puzzlecells.Count - 1; cellNumberCount++)
            {
                foreach (var textBoxCurrent in listOfTextBoxes)
                {
                    if (textBoxCurrent.Name == cellNumberCount.ToString())
                    {
                        textBoxCurrent.Text = loadedPuzzle.puzzlecells[cellNumberCount].value.ToString();
                        break;
                    }
                }
            }
            MessageBox.Show(sudokuSolver.difficluty);
        }

        #endregion

        #region Blank Grid Methods

        private void GenerateBlankGridStandardSudoku()
        {
            for (int puzzleRowNumber = 0; puzzleRowNumber <= loadedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= loadedPuzzle.gridsize - 1; puzzleColumnNumber++)
                {
                    puzzleCell tempPuzzleCell = new puzzleCell();
                    tempPuzzleCell.rownumber = puzzleRowNumber;
                    tempPuzzleCell.columnnumber = puzzleColumnNumber;
                    if (loadedPuzzle.gridsize == 4)
                    {
                        tempPuzzleCell.blocknumber = GetBlockFour(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else if (loadedPuzzle.gridsize == 9)
                    {
                        tempPuzzleCell.blocknumber = GetBlockNumberNine(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else
                    {
                        tempPuzzleCell.blocknumber = GetBlocNumberSixteen(puzzleRowNumber, puzzleColumnNumber);
                    }
                    loadedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }

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
            else if (tempRowNumber >= 2 && tempColumnNumber <= 1)
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
            double blockValue = Math.Sqrt(loadedPuzzle.gridsize);
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

        /// <summary>
        /// Validate button click that determines whether the solution that has been created is correct and valid, i.e. all of the sudoku contraints are met. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validatePuzzleBtn_Click(object sender, EventArgs e)
        {
            bool validRow = false;
            bool validColumn = false;
            bool validBlock = false;

            validRow = ValidateRow();
            validColumn = ValidateColumn();
            validBlock = ValidateBlock();

            if(validRow == true && validColumn == true && validBlock == true)
            {
                MessageBox.Show("Puzzle solution correct");
            }
            else
            {
                MessageBox.Show("Puzzle solution incorrect!");
            }
        }

        #endregion

        private bool ValidateRow()
        {
            List<int> numbersInRow = new List<int>();
            bool validRows = true;
            for (int rowNumberValidate =0;rowNumberValidate<=8;rowNumberValidate++)
            {
                //Adding all number is that row to the list. 
                foreach(var cell in loadedPuzzle.puzzlecells)
                {
                    if (cell.rownumber == rowNumberValidate)
                    {
                        numbersInRow.Add(cell.value);
                    }
                }
                //Check valid numbers 
                validRows = CheckValidNumbers(numbersInRow);
                if(validRows == false)
                {
                    return false;
                }
                numbersInRow.Clear();
            }
            return true;
        }

        private bool ValidateColumn()
        {
            List<int> numbersInColumn = new List<int>();
            bool validColumns = true;
            for (int columnNumberValidate = 0; columnNumberValidate <= 8; columnNumberValidate++)
            {
                //Adding all number is that row to the list. 
                foreach (var cell in loadedPuzzle.puzzlecells)
                {
                    if (cell.columnnumber == columnNumberValidate)
                    {
                        numbersInColumn.Add(cell.value);
                    }
                }
                //Check valid numbers 
                validColumns = CheckValidNumbers(numbersInColumn);
                if (validColumns == false)
                {
                    return false;
                }
                numbersInColumn.Clear();
            }
            return true;
        }
        private bool ValidateBlock()
        {
            List<int> numbersInBlock = new List<int>();
            bool validBlocks = true;
            for (int blockNumberValidate = 0; blockNumberValidate <= 8; blockNumberValidate++)
            {
                //Adding all number is that row to the list. 
                foreach (var cell in loadedPuzzle.puzzlecells)
                {
                    if (cell.blocknumber == blockNumberValidate)
                    {
                        numbersInBlock.Add(cell.value);
                    }
                }
                //Check valid numbers 
                validBlocks = CheckValidNumbers(numbersInBlock);
                if (validBlocks == false)
                {
                    return false;
                }
                numbersInBlock.Clear();
            }
            return true;
        }

        private bool CheckValidNumbers(List<int> listOfNumbers)
        {
            List<int> numbersUsed = new List<int>();
            foreach(var number in listOfNumbers)
            {            
                if(number ==0)
                {
                    return false;
                }
                foreach(var usedNumber in numbersUsed)
                {
                    if(usedNumber == number)
                    {
                        return false;
                    }
                    else
                    {
                        numbersUsed.Add(number);
                    }
                }
            }
            return true;
        }

    }




}
