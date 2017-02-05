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
        TextBox currentSelectedTextBox = new TextBox();
        string fileDirectoryLocation;
        puzzle loadedPuzzle;
        PuzzleManager puzzleManager; 
        public PlaySudokuScreen()
        {
            InitializeComponent();       
        }

        public PlaySudokuScreen(string fileNameFromButtonPress, int gridSize)
        {
            loadedPuzzle = new puzzle();
            puzzleManager = new PuzzleManager();
            fileDirectoryLocation = "C:\\Users\\New\\Documents\\Sudoku\\Application\\SudokuSetterAndSolver\\SudokuSetterAndSolver\\Puzzles\\LevelsPuzzles\\";
            fileDirectoryLocation = Path.GetFullPath(@"..\..\") + @"\Puzzles\LevelsPuzzles";
            fileDirectoryLocation += @"\" + fileNameFromButtonPress + ".xml";
            gridSize = 9;
            InitializeComponent();
        }

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
        /// Method to handel the key presses witin the sudoku cells, to ensure only number 1-9 are entrered. 
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

        private int[,] ConvertListToMultiDimensionalArray(List<int> puzzleInList, int gridSize)
        {
            int[,] puzzleArray = new int[gridSize, gridSize];
            int rowNumber = 0;
            int columnNumber = 0;

            for (int cellNumber = 0; cellNumber <= puzzleInList.Count - 1; cellNumber++)
            {
                puzzleArray[rowNumber, columnNumber] = puzzleInList[cellNumber];
                if (columnNumber == 8 || columnNumber % 9 == 8)
                {
                    rowNumber++;
                    columnNumber = 0;
                }
                else
                {
                    columnNumber++;
                }
            }


            return puzzleArray;
        }

    }
}
