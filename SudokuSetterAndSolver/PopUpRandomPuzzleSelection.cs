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
        public PopUpRandomPuzzleSelection()
        {
            InitializeComponent();
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            RandomPuzzleGameScreen randomPuzzleScreen = new RandomPuzzleGameScreen(9, puzzleTypeSelection.SelectedIndex);
            randomPuzzleScreen.Show();
        }
    }
}
