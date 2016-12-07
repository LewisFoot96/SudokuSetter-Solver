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
        private bool _currentPage = false;

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

        private void confirmBtn_Click(object sender, EventArgs e)
        {
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
    }
}
