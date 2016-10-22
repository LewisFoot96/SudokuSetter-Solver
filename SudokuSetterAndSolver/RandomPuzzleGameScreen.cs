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
        TextBox currentSelectedTextBox = new TextBox();
        public RandomPuzzleGameScreen(int gridSize)
        {
            CreateGrid(gridSize);
            InitializeComponent();
        }
        private void a1_TextChanged(object sender, EventArgs e)
        {
            
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            SudokuSolver sudokuSolverTest = new SudokuSolver();

            sudokuSolverTest.solvePuzzle();
        }
    }
}
