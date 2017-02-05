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
        private bool _currentPage = false;
        #endregion

        #region Constructor 
        public PopUpRandomPuzzleSelection()
        {
            _currentPage = false;
            InitializeComponent();
        }
        public PopUpRandomPuzzleSelection(bool currentPage)
        {
            _currentPage = currentPage;
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
            //Dipslaying puzzle on application. 
            this.Close();
            if (_currentPage == false)
            {
                RandomPuzzleGameScreen randomPuzzleScreen = new RandomPuzzleGameScreen(puzzleTypeSelection.SelectedIndex);
                randomPuzzleScreen.Show();
            }
            else
            {
                RandomPuzzleGameScreen._puzzleSelection = puzzleTypeSelection.SelectedIndex;
            }

        }
        #endregion 
    }
}
