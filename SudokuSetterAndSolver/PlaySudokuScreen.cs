using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    public partial class PlaySudokuScreen : TemplateScreen
    {
        #region Constructor 
        public PlaySudokuScreen()
        {
            InitializeComponent();
        }

        public PlaySudokuScreen(string fileNameFromButtonPress, int gridSize)
        {
            loadedPuzzle = new puzzle();
            puzzleManager = new PuzzleManager();
            loadedPuzzle.gridsize = 9;
            //Getting directory location of the loaded puzzle. 
            fileDirctoryLocation = Path.GetFullPath(@"..\..\") + @"\Puzzles\LevelsPuzzles";
            fileDirctoryLocation += @"\" + fileNameFromButtonPress + ".xml";
            InitializeComponent();
            LoadPuzzleFile();
        }

        #endregion

        #region Event Handler Methods 

        /// <summary>
        /// Method if the user submits a puzzle attempt. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitPuzzleBtn_Click(object sender, EventArgs e)
        {
            UpdatePuzzle();
            if (sudokuSolutionArray.Count > 0)
            {
                bool result = CheckPuzzleSolution();
                if (result == true)
                {
                    MessageBox.Show("Puzzle correct! Well done!");
                }
                else
                {
                    MessageBox.Show("Puzzle incorrect! Please Try again!");
                }
            }
            else
            {
                MessageBox.Show("Puzzle incorrect! Please Try again!");
            }

        }

        #endregion 
    }
}
