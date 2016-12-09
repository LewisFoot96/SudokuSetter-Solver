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
    public partial class PopUpSolverScreen : Form
    {
        private bool _currentPage = false;
        public PopUpSolverScreen()
        {
            _currentPage = false;
            InitializeComponent();
        }

        public PopUpSolverScreen(bool currentPage)
        {
            _currentPage = currentPage;
            InitializeComponent();

        }

        private void confirmSolverSelectionBtn_Click(object sender, EventArgs e)
        {
            this.Close();

            if (_currentPage == false)
            {
                SolveSudokuScreen._puzzleSelection = solveSudokuSelectionCb.SelectedIndex;
                SolveSudokuScreen solverPuzzleScreen = new SolveSudokuScreen();
                solverPuzzleScreen.Show();
            }
            else
            {
                SolveSudokuScreen._puzzleSelection = solveSudokuSelectionCb.SelectedIndex;
                RandomPuzzleGameScreen._puzzleSelection = solveSudokuSelectionCb.SelectedIndex;
            }
        }
    }
}
