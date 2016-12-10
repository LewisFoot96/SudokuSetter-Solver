using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    public partial class MainMenu : Form
    {
        public static int puzzleSelection = 0;
        public static bool popUpCompleted = false; 
        #region Constructor 
        public MainMenu()
        {       
            InitializeComponent();
        }
        #endregion 

        #region Main Menu Button Clicks 

        private void randomPuzzleScreenBtn_Click(object sender, EventArgs e)
        {
            PopUpRandomPuzzleSelection puzzleSelectionPopup = new PopUpRandomPuzzleSelection();
            puzzleSelectionPopup.ShowDialog();
            this.Hide();
        }

        private void gamePlayScreenBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlayLevelsScreen playLevelsPuzzleScreen = new PlayLevelsScreen();
            playLevelsPuzzleScreen.Show();
        }

        private void solveSudokuScreenBtn_Click(object sender, EventArgs e)
        {
            PopUpSolverScreen puzzleSelectionPopup = new PopUpSolverScreen();
            puzzleSelectionPopup.ShowDialog();
            this.Hide();
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

        private void convertFileBtn_Click(object sender, EventArgs e)
        {
            ConvertTextFileToXMLFile convertScreen = new ConvertTextFileToXMLFile();
            convertScreen.Show();
            this.Hide();
        }
    }
}
