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
            errorSubmitCount = 0;
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
            UpdatePuzzle();
            //Check puzzle enetered by the user against the pre set solution. 
            bool correctPuzzle = CheckPuzzleSolution();
            if (correctPuzzle == true)
            {
                MessageBox.Show("Puzzle Completed! Well Done! Error count: " + errorSubmitCount);

            }
            else
            {
                MessageBox.Show("Puzzle incorrect. Please try again.");
            }
        }

        private void newPuzzleBtn_Click(object sender, EventArgs e)
        {
            errorSubmitCount = 0;
            ClearGrid();
            listOfTextBoxes.Clear();
            loadedPuzzle.puzzlecells.Clear();
            PopUpRandomPuzzleSelection popUpPuzzleSelection = new PopUpRandomPuzzleSelection();
            popUpPuzzleSelection.ShowDialog();
            LoadPuzzleSelection();
        }

        private void solveGeneratedPuzzleBtn_Click(object sender, EventArgs e)
        {
            UpdatePuzzle();
            SolvePuzzle();
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
