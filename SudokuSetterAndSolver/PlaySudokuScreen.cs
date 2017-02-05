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
    public partial class PlaySudokuScreen : Form
    {
        #region Field Variables 
        TextBox currentSelectedTextBox = new TextBox();
        //Will contain the location of the file that is for the selected level. 
        string fileDirectoryLocation;
        //Loaded puzzle for that level
        puzzle loadedPuzzle;
        //Manages the reading and writing of puzzles to xml files. 
        PuzzleManager puzzleManager;
        #endregion

        #region Constructor 
        public PlaySudokuScreen()
        {
            InitializeComponent();       
        }

        public PlaySudokuScreen(string fileNameFromButtonPress, int gridSize)
        {
            loadedPuzzle = new puzzle();
            puzzleManager = new PuzzleManager();
            loadedPuzzle.gridsize = gridSize;
            //Getting directory location of the loaded puzzle. 
            fileDirectoryLocation = Path.GetFullPath(@"..\..\") + @"\Puzzles\LevelsPuzzles";
            fileDirectoryLocation += @"\" + fileNameFromButtonPress + ".xml";      
            InitializeComponent();
            CreateGrid(9);
        } 

        #endregion

        #region Event Handling Methods 
        /// <summary>
        /// Going back to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void puzzleTextChange(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
        }
        private void puzzleSquareClick(object sender, EventArgs e)
        {
            //Disabling the previous textbox
            currentSelectedTextBox.ReadOnly = true;
            var tb = (TextBox)sender; //Attaining the textbox that has just been clicked. 
            tb.ReadOnly = false;

            //Setting the selected textbox as the current one. 
            currentSelectedTextBox = tb;
        }

        /// <summary>
        /// Method to handle the key presses witin the sudoku cells, to ensure only number 1-9 are entrered. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckCellEntry(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Cell entry validation, if the entered character is between 1 - 9, then enter it into the text box. 
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion 

    }
}
