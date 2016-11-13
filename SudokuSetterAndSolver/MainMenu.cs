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
    public partial class MainMenu : Form
    {
        #region Constructor 
        public MainMenu()
        {
            InitializeComponent();
        }
        #endregion 

        #region Main Menu Button Clicks 

        private void randomPuzzleScreenBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            RandomPuzzleGameScreen randomPuzzleScreen = new RandomPuzzleGameScreen(9);
            randomPuzzleScreen.Show();
        }

        private void gamePlayScreenBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlayLevelsScreen playLevelsPuzzleScreen = new PlayLevelsScreen();
            playLevelsPuzzleScreen.Show();
        }

        private void solveSudokuScreenBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            SolveSudokuScreen solveSudokuScreen = new SolveSudokuScreen();
            solveSudokuScreen.Show();
        }

        private void statisticsScreenBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            StatisticsScreen statsScreen = new StatisticsScreen();
            statsScreen.Show();
        }

        private void instructionsCreditsScreenBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            InstructionsCreditsScreen instrcutionsCreditsScreen = new InstructionsCreditsScreen();
            instrcutionsCreditsScreen.Show(); 
        }


        #endregion

    }
}
