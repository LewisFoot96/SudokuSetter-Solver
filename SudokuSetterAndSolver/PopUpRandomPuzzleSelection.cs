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
    public partial class PopUpRandomPuzzleSelection : Form
    {
        #region Field Variables 
        //If a puzzle has been selected. 
        public static bool isPuzzleTypeSelected = false;
        #endregion

        #region Constructor 
        public PopUpRandomPuzzleSelection()
        {
            isPuzzleTypeSelected = false;
            InitializeComponent();
        }
        #endregion

        #region Event Handler Methods 
        /// <summary>
        /// If a puzzle has been selected and the confirm button has been pressed. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmBtn_Click(object sender, EventArgs e)
        {
            isPuzzleTypeSelected = true;
            //Dipslaying puzzle on application. 
            this.Close();

            MainScreen._puzzleSelection = puzzleTypeSelection.SelectedIndex;
        }
        #endregion 
    }
}
