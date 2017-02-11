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

    //For the irregulat puzzlesm will have template puzzles that will be loaded in, these puzzle templates will then be solved. 

    //Maybe create a sudoku grid class, to create the grids and populate from there. 
    public partial class RandomPuzzleGameScreen : TemplateScreen
    {

        #region Constructor 
        public RandomPuzzleGameScreen(int puzzleSelection)
        {
            //Puzzle selection is the type of puzzle that will be created. 
            _puzzleSelection = puzzleSelection;
            InitializeComponent();
            LoadPuzzleSelection();
        }

        #endregion

        #region Event Methods 

        /// <summary>
        /// Method to see if the puzzle entered by the user is correct. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitPuzzleBtn_Click(object sender, EventArgs e)
        {
            for (int indexNumber = 0; indexNumber <= loadedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                string cellName = indexNumber.ToString();
                foreach (var textBox in listOfTextBoxes)
                {
                    if (cellName == textBox.Name)
                    {
                        if (textBox.Text != "")
                        {
                            loadedPuzzle.puzzlecells[indexNumber].value = Int32.Parse(textBox.Text);
                        }
                        else
                        {
                            loadedPuzzle.puzzlecells[indexNumber].value = 0;
                        }
                    }
                }
            }
            //Check puzzle enetered by the user against the pre set solution. 
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
            //Update generated puzzle 
            UpdateloadedPuzzle();
            for (int indexValue = 0; indexValue <= loadedPuzzle.puzzlecells.Count - 1; indexValue++)
            {
                if (loadedPuzzle.puzzlecells[indexValue].value != sudokuSolutionArray[indexValue])
                {
                    bool validRow = false;
                    bool validColumn = false;
                    bool validBlock = false;
                    validRow = ValidateRow();
                    validColumn = ValidateColumn();
                    validBlock = ValidateBlock();
                    if (validBlock == true && validColumn == true && validRow == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void UpdateloadedPuzzle()
        {
            for (int index = 0; index <= listOfTextBoxes.Count - 1; index++)
            {
                if (listOfTextBoxes[index].Text == "")
                {
                    loadedPuzzle.puzzlecells[index].value = 0;
                }
                else
                {
                    loadedPuzzle.puzzlecells[index].value = Int32.Parse(listOfTextBoxes[index].Text);
                }
            }
        }

        private bool ValidateSolution()
        {
            return true;
        }

        private void solveloadedPuzzleBtn_Click(object sender, EventArgs e)
        {
            sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;
            bool puzzleSolved = sudokuSolver.BacktrackingUsingXmlTemplateFile(false);
            loadedPuzzle = sudokuSolver.currentPuzzleToBeSolved;

            if (puzzleSolved == true)
            {
                for (int cellLocationNumber = 0; cellLocationNumber <= loadedPuzzle.puzzlecells.Count - 1; cellLocationNumber++)
                {
                    foreach (var textBoxCurrent in listOfTextBoxes)
                    {
                        if (textBoxCurrent.Name == cellLocationNumber.ToString())
                        {
                            textBoxCurrent.Text = loadedPuzzle.puzzlecells[cellLocationNumber].value.ToString();
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
            listOfTextBoxes.Clear();
            loadedPuzzle.puzzlecells.Clear();
            PopUpRandomPuzzleSelection popUpPuzzleSelection = new PopUpRandomPuzzleSelection(true);
            popUpPuzzleSelection.ShowDialog();
            LoadPuzzleSelection();

        }

        private void solveGeneratedPuzzleBtn_Click(object sender, EventArgs e)
        {
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
        #endregion
    }
}
