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
    public partial class RandomPuzzleGameScreen : Form
    {
        public RandomPuzzleGameScreen()
        {
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
            var tb = (TextBox)sender; //Attaining the textbox that has just been clicked. 
            tb.ReadOnly = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SudokuSolver sudokuSolverTest = new SudokuSolver();

            sudokuSolverTest.solvePuzzle();
        }
    }
}
