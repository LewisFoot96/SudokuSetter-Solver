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
    public partial class SolveSudokuScreen : TemplateScreen
    {
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

        /// <summary>
        /// If the user wishes to determine the difficulty of the puzzle. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Validate button click that determines whether the solution that has been created is correct and valid, i.e. all of the sudoku contraints are met. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validatePuzzleBtn_Click(object sender, EventArgs e)
        {   //Validating puzzle 
            bool validRow = false;
            bool validColumn = false;
            bool validBlock = false;
            validRow = ValidateRow();
            validColumn = ValidateColumn();
            validBlock = ValidateBlock();
            //If puzzle is correct
            if (validRow == true && validColumn == true && validBlock == true)
            {
                MessageBox.Show("Puzzle solution correct");
            }
            else //If puzzle is incorrect
            {
                MessageBox.Show("Puzzle solution incorrect!");
            }
        }

        #endregion
    }
}
