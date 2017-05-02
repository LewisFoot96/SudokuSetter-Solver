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
        #region Field Variables 
        //If a puzzle option has been selected. 
        public static bool isPuzzleSelected = false;
        #endregion

        #region Constructor 

        public PopUpSolverScreen()
        {
            //No puzzle has been selected. 
            isPuzzleSelected = false;
            InitializeComponent();
        }
        #endregion

        #region Event Handler Methods 

        /// <summary>
        /// Method for when a puzzle option has been selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmSolverSelectionBtn_Click(object sender, EventArgs e)
        {
            //Closing form and returning selected puzzle option. 
            this.Close();
            isPuzzleSelected = true;
            MainScreen._puzzleSelectionSolve = solveSudokuSelectionCb.SelectedIndex;
        }
        #endregion
    }
}
