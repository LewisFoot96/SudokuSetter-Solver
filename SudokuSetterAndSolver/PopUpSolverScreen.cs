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
        public static bool isPuzzleSelected = false;
        public PopUpSolverScreen()
        {
            isPuzzleSelected = false;
            InitializeComponent();
        }


        private void confirmSolverSelectionBtn_Click(object sender, EventArgs e)
        {
            this.Close();

            isPuzzleSelected = true;

            MainScreen._puzzleSelectionSolve = solveSudokuSelectionCb.SelectedIndex;
        }
    }
}
