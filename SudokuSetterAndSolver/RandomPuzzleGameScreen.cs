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

    //For the irregulat puzzlesm will have template puzzles that will be loaded in, these puzzle templates will then be solved. 

    //Maybe create a sudoku grid class, to create the grids and populate from there. 
    public partial class RandomPuzzleGameScreen : Form
    {
        #region Objects 
        SudokuPuzzleGenerator sudokuGridGenerator = new SudokuPuzzleGenerator(9);
        TextBox currentSelectedTextBox = new TextBox();
        public List<TextBox> listOfTextBoxes = new List<TextBox>();
        SudokuSolver sudokuSolver = new SudokuSolver();
        puzzle generatedPuzzle = new puzzle();
        #endregion

        #region Global Variables 
        //Contains the solution to the puzzle on the screen. 
        List<int> sudokuSolutionArray = new List<int>();
        public static int _puzzleSelection = 0;
        #endregion

        #region Constructor 
        public RandomPuzzleGameScreen(int puzzleSelection)
        {
            //Puzzle selection is the type of puzzle that will be created. 
            _puzzleSelection = puzzleSelection;
            InitializeComponent();
            CreateSudokuGrid();
        }

        #endregion

        #region Event Methods 
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

        private void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        /// <summary>
        /// Method to see if the puzzle entered by the user is correct. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitPuzzleBtn_Click(object sender, EventArgs e)
        {
            for (int indexNumber = 0; indexNumber <= generatedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                string cellName = indexNumber.ToString();
                foreach (var textBox in listOfTextBoxes)
                {
                    if (cellName == textBox.Name)
                    {
                        if (textBox.Text != "")
                        {
                            generatedPuzzle.puzzlecells[indexNumber].value = Int32.Parse(textBox.Text);
                        }
                        else
                        {
                            generatedPuzzle.puzzlecells[indexNumber].value = 0;
                        }
                    }
                }
            }
            //Check puzzle enetered by the user against the pre set solution. 
            bool correctPuzzle = CheckSubmittedPuzzleXML();
            if (correctPuzzle == true)
            {
                MessageBox.Show("Puzzle Completed! Well Done!");
            }
            else
            {
                MessageBox.Show("Puzzle incorrect. Please try again.");
            }
        }

        private bool CheckSubmittedPuzzleXML()
        {
            for (int indexValue = 0; indexValue <= generatedPuzzle.puzzlecells.Count - 1; indexValue++)
            {
                if (generatedPuzzle.puzzlecells[indexValue].value != sudokuSolutionArray[indexValue])
                {
                    return false;
                }
            }
            return true;
        }

        private void solveGeneratedPuzzleBtn_Click(object sender, EventArgs e)
        {
            sudokuSolver.currentPuzzleToBeSolved = generatedPuzzle;
            bool puzzleSolved = sudokuSolver.BacktrackingUsingXmlTemplateFile(false);
            generatedPuzzle = sudokuSolver.currentPuzzleToBeSolved;

            if (puzzleSolved == true)
            {

                for (int cellLocationNumber = 0; cellLocationNumber <= generatedPuzzle.puzzlecells.Count - 1; cellLocationNumber++)
                {
                    foreach (var textBoxCurrent in listOfTextBoxes)
                    {
                        if (textBoxCurrent.Name == cellLocationNumber.ToString())
                        {
                            textBoxCurrent.Text = generatedPuzzle.puzzlecells[cellLocationNumber].value.ToString();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No solution, invalid solution");
            }

        }

        private void newPuzzleBtn_Click(object sender, EventArgs e)
        {
            ClearGrid();
            generatedPuzzle.puzzlecells.Clear();
            PopUpRandomPuzzleSelection popUpPuzzleSelection = new PopUpRandomPuzzleSelection(true);
            popUpPuzzleSelection.ShowDialog();
            CreateSudokuGrid();

        }
        #endregion

        #region Methods 

        private void ClearGrid()
        {
            //currently remove all textboxes when a new puzzle is selected, this may need to be changed. 
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Dispose();
            }
        }
        #endregion

        #region Generate Blank Puzzle 
        /// <summary>
        /// This method generates a blank puzzle dependant on the grid size. 
        /// </summary>
        private void CreateSudokuGrid()
        {
            //Determinging which sudoku puzzle is generated based upon the users selection. 
            if (_puzzleSelection == 2)
            {
                generatedPuzzle.gridsize = 9;
                GenerateBlankGridStandardSudoku();
                GenerateStandardSudokuPuzzle();
            }
            else if (_puzzleSelection == 0)
            {
                generatedPuzzle.gridsize = 9;
                //Seleting which irregular template to use. 
                Random newRandomNumber = new Random();
                int irregularRandom = newRandomNumber.Next(0, 1);
                if (irregularRandom == 1)
                {
                    GenerateFirstTemplateIrregular();
                }
                else
                {
                    GenerateSecondTemplateIrregular();
                }
                GenerateStandardSudokuPuzzle();
            }
            else if (_puzzleSelection == 1)
            {
                generatedPuzzle.gridsize = 16;
                GenerateBlankGridStandardSudoku();
                GenerateLargeSudokuPuzzle();
            }
            else
            {
                generatedPuzzle.gridsize = 4;
                GenerateBlankGridStandardSudoku();
                GenerateSmallSudokuPuzzle();
            }
        }

        /// <summary>
        /// Method that create a blank sudoku grid for the random puzzle. 
        /// </summary>
        private void GenerateBlankGridStandardSudoku()
        {
            //Creating the puzzle and all cells, in line with the choosen size. 
            for (int puzzleRowNumber = 0; puzzleRowNumber <= generatedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= generatedPuzzle.gridsize - 1; puzzleColumnNumber++)
                {
                    puzzleCell tempPuzzleCell = new puzzleCell();
                    tempPuzzleCell.rownumber = puzzleRowNumber;
                    tempPuzzleCell.columnnumber = puzzleColumnNumber;
                    if (generatedPuzzle.gridsize == 4)
                    {
                        tempPuzzleCell.blocknumber = GetBlockFour(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else if (generatedPuzzle.gridsize == 9)
                    {
                        tempPuzzleCell.blocknumber = GetBlockNumberNine(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else
                    {
                        tempPuzzleCell.blocknumber = GetBlocNumberSixteen(puzzleRowNumber, puzzleColumnNumber);
                    }
                    generatedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }
        #endregion

        #region Get Blocks Methods 
        //Methods that get the block number for the cell that is being handled. 
        private int GetBlockFour(int tempRowNumber, int tempColumnNumber)
        {
            if (tempRowNumber <= 1 && tempColumnNumber <= 1)
            {
                return 0;
            }
            else if (tempRowNumber <= 1 && tempColumnNumber >= 2)
            {
                return 1;
            }
            else if (tempRowNumber >= 2 && tempColumnNumber <= 1)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private int GetBlockNumberNine(int tempRowNumber, int tempColumnNumber)
        {
            double blockValue = Math.Sqrt(generatedPuzzle.gridsize);
            if (tempRowNumber <= 2 && tempColumnNumber <= 2)
            {
                return 0;
            }
            else if (tempRowNumber <= 2 && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 1;
            }
            else if (tempRowNumber <= 2 && (tempColumnNumber >= 6 && tempColumnNumber <= 8))
            {
                return 2;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && tempColumnNumber <= 2)
            {
                return 3;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 4;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && (tempColumnNumber >= 6 && tempColumnNumber <= 8))
            {
                return 5;
            }
            else if ((tempRowNumber >= 6 && tempRowNumber <= 8) && tempColumnNumber <= 2)
            {
                return 6;
            }
            else if ((tempRowNumber >= 6 && tempRowNumber <= 8) && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 7;
            }
            else
            {
                return 8;
            }

        }

        private int GetBlocNumberSixteen(int tempRowNumber, int tempColumnNumber)
        {
            if (tempRowNumber <= 3 && tempColumnNumber <= 3)
            {
                return 0;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 1;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 2;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 3;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && tempColumnNumber <= 3)
            {
                return 4;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 5;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 6;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 7;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && tempColumnNumber <= 3)
            {
                return 8;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 9;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 10;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 11;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && tempColumnNumber <= 3)
            {
                return 12;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 13;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 14;
            }
            else
            {
                return 15;
            }

        }

        #endregion

        #region IrregularPuzzles

        private void GenerateFirstTemplateIrregular()
        {
            for (int puzzleRowNumber = 0; puzzleRowNumber <= generatedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= generatedPuzzle.gridsize - 1; puzzleColumnNumber++)
                {
                    puzzleCell tempPuzzleCell = new puzzleCell();
                    tempPuzzleCell.rownumber = puzzleRowNumber;
                    tempPuzzleCell.columnnumber = puzzleColumnNumber;

                    //if((puzzleRowNumber==0 && puzzleColumnNumber ==2) || (puzzleRowNumber == 2 && puzzleColumnNumber == 8) || (puzzleRowNumber == 3 && puzzleColumnNumber == 7) || (puzzleRowNumber == 7 && puzzleColumnNumber == 5))
                    //{
                    //    tempPuzzleCell.value = 1;
                    //}
                    //else if ((puzzleRowNumber == 1 && puzzleColumnNumber == 5) || (puzzleRowNumber == 5 && puzzleColumnNumber == 0) || (puzzleRowNumber == 7 && puzzleColumnNumber == 3) || (puzzleRowNumber == 8 && puzzleColumnNumber == 6))
                    //{
                    //    tempPuzzleCell.value = 2;
                    //}
                    //else if ((puzzleRowNumber == 1 && puzzleColumnNumber == 4) || (puzzleRowNumber == 4 && puzzleColumnNumber == 1) )
                    //{
                    //    tempPuzzleCell.value = 3;
                    //}
                    //else if ((puzzleRowNumber == 4 && puzzleColumnNumber == 7) || (puzzleRowNumber == 6 && puzzleColumnNumber == 0))
                    //{
                    //    tempPuzzleCell.value = 4;
                    //}
                    //else if ((puzzleRowNumber == 2 && puzzleColumnNumber == 0) || (puzzleRowNumber == 4 && puzzleColumnNumber == 6) || (puzzleRowNumber == 6 && puzzleColumnNumber == 8) || (puzzleRowNumber == 8 && puzzleColumnNumber == 5))
                    //{
                    //    tempPuzzleCell.value = 5;
                    //}
                    //else if ((puzzleRowNumber == 0 && puzzleColumnNumber == 3) || (puzzleRowNumber == 3 && puzzleColumnNumber == 8) || (puzzleRowNumber == 5 && puzzleColumnNumber == 7) || (puzzleRowNumber == 6 && puzzleColumnNumber == 4))
                    //{
                    //    tempPuzzleCell.value = 6;
                    //}
                    //else if ((puzzleRowNumber == 5 && puzzleColumnNumber == 1) )
                    //{
                    //    tempPuzzleCell.value = 7;
                    //}
                    //else if ((puzzleRowNumber == 1 && puzzleColumnNumber == 3) || (puzzleRowNumber == 2 && puzzleColumnNumber == 4) || (puzzleRowNumber == 4 && puzzleColumnNumber == 2) )
                    //{
                    //    tempPuzzleCell.value = 8;
                    //}
                    //else if ((puzzleRowNumber == 0 && puzzleColumnNumber == 6) || (puzzleRowNumber == 3 && puzzleColumnNumber == 1) || (puzzleRowNumber == 7 && puzzleColumnNumber == 4) || (puzzleRowNumber == 8 && puzzleColumnNumber == 2))
                    //{
                    //    tempPuzzleCell.value = 9;
                    //}

                    if ((puzzleRowNumber == 0 && puzzleColumnNumber == 0) || (puzzleRowNumber == 1 && puzzleColumnNumber == 0) || (puzzleRowNumber == 1 && puzzleColumnNumber == 1) || (puzzleRowNumber == 1 && puzzleColumnNumber == 2) || (puzzleRowNumber == 1 && puzzleColumnNumber == 3) || (puzzleRowNumber == 2 && puzzleColumnNumber == 0) || (puzzleRowNumber == 2 && puzzleColumnNumber == 1) || (puzzleRowNumber == 2 && puzzleColumnNumber == 3) || (puzzleRowNumber == 3 && puzzleColumnNumber == 0))
                    {
                        tempPuzzleCell.blocknumber = 0;
                    }
                    else if ((puzzleRowNumber == 0 && puzzleColumnNumber == 1) || (puzzleRowNumber == 0 && puzzleColumnNumber == 2) || (puzzleRowNumber == 0 && puzzleColumnNumber == 3) || (puzzleRowNumber == 0 && puzzleColumnNumber == 4) || (puzzleRowNumber == 0 && puzzleColumnNumber == 5) || (puzzleRowNumber == 0 && puzzleColumnNumber == 6) || (puzzleRowNumber == 0 && puzzleColumnNumber == 7) || (puzzleRowNumber == 1 && puzzleColumnNumber == 4) || (puzzleRowNumber == 2 && puzzleColumnNumber == 4))
                    {
                        tempPuzzleCell.blocknumber = 1;
                    }
                    else if ((puzzleRowNumber == 0 && puzzleColumnNumber == 8) || (puzzleRowNumber == 1 && puzzleColumnNumber == 5) || (puzzleRowNumber == 1 && puzzleColumnNumber == 6) || (puzzleRowNumber == 1 && puzzleColumnNumber == 7) || (puzzleRowNumber == 1 && puzzleColumnNumber == 8) || (puzzleRowNumber == 2 && puzzleColumnNumber == 5) || (puzzleRowNumber == 2 && puzzleColumnNumber == 7) || (puzzleRowNumber == 2 && puzzleColumnNumber == 8) || (puzzleRowNumber == 3 && puzzleColumnNumber == 8))
                    {
                        tempPuzzleCell.blocknumber = 2;
                    }
                    else if ((puzzleRowNumber == 2 && puzzleColumnNumber == 2) || (puzzleRowNumber == 3 && puzzleColumnNumber == 1) || (puzzleRowNumber == 3 && puzzleColumnNumber == 2) || (puzzleRowNumber == 4 && puzzleColumnNumber == 0) || (puzzleRowNumber == 4 && puzzleColumnNumber == 1) || (puzzleRowNumber == 5 && puzzleColumnNumber == 0) || (puzzleRowNumber == 6 && puzzleColumnNumber == 0) || (puzzleRowNumber == 7 && puzzleColumnNumber == 0) || (puzzleRowNumber == 8 && puzzleColumnNumber == 0))
                    {
                        tempPuzzleCell.blocknumber = 3;
                    }
                    else if ((puzzleRowNumber == 3 && puzzleColumnNumber == 3) || (puzzleRowNumber == 3 && puzzleColumnNumber == 4) || (puzzleRowNumber == 3 && puzzleColumnNumber == 5) || (puzzleRowNumber == 4 && puzzleColumnNumber == 2) || (puzzleRowNumber == 4 && puzzleColumnNumber == 3) || (puzzleRowNumber == 4 && puzzleColumnNumber == 4) || (puzzleRowNumber == 4 && puzzleColumnNumber == 5) || (puzzleRowNumber == 4 && puzzleColumnNumber == 6) || (puzzleRowNumber == 5 && puzzleColumnNumber == 4))
                    {
                        tempPuzzleCell.blocknumber = 4;
                    }
                    else if ((puzzleRowNumber == 2 && puzzleColumnNumber == 6) || (puzzleRowNumber == 3 && puzzleColumnNumber == 6) || (puzzleRowNumber == 3 && puzzleColumnNumber == 7) || (puzzleRowNumber == 4 && puzzleColumnNumber == 7) || (puzzleRowNumber == 4 && puzzleColumnNumber == 8) || (puzzleRowNumber == 5 && puzzleColumnNumber == 8) || (puzzleRowNumber == 6 && puzzleColumnNumber == 8) || (puzzleRowNumber == 7 && puzzleColumnNumber == 8) || (puzzleRowNumber == 8 && puzzleColumnNumber == 8))
                    {
                        tempPuzzleCell.blocknumber = 5;
                    }
                    else if ((puzzleRowNumber == 5 && puzzleColumnNumber == 1) || (puzzleRowNumber == 5 && puzzleColumnNumber == 2) || (puzzleRowNumber == 6 && puzzleColumnNumber == 1) || (puzzleRowNumber == 7 && puzzleColumnNumber == 1) || (puzzleRowNumber == 8 && puzzleColumnNumber == 1) || (puzzleRowNumber == 8 && puzzleColumnNumber == 2) || (puzzleRowNumber == 8 && puzzleColumnNumber == 3) || (puzzleRowNumber == 8 && puzzleColumnNumber == 4) || (puzzleRowNumber == 7 && puzzleColumnNumber == 3))
                    {
                        tempPuzzleCell.blocknumber = 6;
                    }
                    else if ((puzzleRowNumber == 5 && puzzleColumnNumber == 3) || (puzzleRowNumber == 5 && puzzleColumnNumber == 5) || (puzzleRowNumber == 6 && puzzleColumnNumber == 2) || (puzzleRowNumber == 6 && puzzleColumnNumber == 3) || (puzzleRowNumber == 6 && puzzleColumnNumber == 4) || (puzzleRowNumber == 6 && puzzleColumnNumber == 5) || (puzzleRowNumber == 6 && puzzleColumnNumber == 6) || (puzzleRowNumber == 7 && puzzleColumnNumber == 2) || (puzzleRowNumber == 7 && puzzleColumnNumber == 6))
                    {
                        tempPuzzleCell.blocknumber = 7;
                    }
                    else if ((puzzleRowNumber == 7 && puzzleColumnNumber == 4) || (puzzleRowNumber == 7 && puzzleColumnNumber == 5) || (puzzleRowNumber == 8 && puzzleColumnNumber == 5) || (puzzleRowNumber == 8 && puzzleColumnNumber == 6) || (puzzleRowNumber == 8 && puzzleColumnNumber == 7) || (puzzleRowNumber == 7 && puzzleColumnNumber == 7) || (puzzleRowNumber == 6 && puzzleColumnNumber == 7) || (puzzleRowNumber == 5 && puzzleColumnNumber == 7) || (puzzleRowNumber == 5 && puzzleColumnNumber == 6))
                    {
                        tempPuzzleCell.blocknumber = 8;
                    }
                    else
                    {
                        tempPuzzleCell.blocknumber = 8;
                    }
                    generatedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }

        private void GenerateSecondTemplateIrregular()
        {
            for (int puzzleRowNumber = 0; puzzleRowNumber <= generatedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= generatedPuzzle.gridsize - 1; puzzleColumnNumber++)
                {
                    puzzleCell tempPuzzleCell = new puzzleCell();
                    tempPuzzleCell.rownumber = puzzleRowNumber;
                    tempPuzzleCell.columnnumber = puzzleColumnNumber;

                    if ((puzzleRowNumber == 0 && puzzleColumnNumber == 0) || (puzzleRowNumber == 0 && puzzleColumnNumber == 1) || (puzzleRowNumber == 0 && puzzleColumnNumber == 2) || (puzzleRowNumber == 0 && puzzleColumnNumber == 3) || (puzzleRowNumber == 1 && puzzleColumnNumber == 0) || (puzzleRowNumber == 1 && puzzleColumnNumber == 1) || (puzzleRowNumber == 2 && puzzleColumnNumber == 0) || (puzzleRowNumber == 2 && puzzleColumnNumber == 1) || (puzzleRowNumber == 3 && puzzleColumnNumber == 0))
                    {
                        tempPuzzleCell.blocknumber = 0;
                    }
                    else if ((puzzleRowNumber == 0 && puzzleColumnNumber == 4) || (puzzleRowNumber == 0 && puzzleColumnNumber == 5) || (puzzleRowNumber == 0 && puzzleColumnNumber == 6) || (puzzleRowNumber == 0 && puzzleColumnNumber == 7) || (puzzleRowNumber == 1 && puzzleColumnNumber == 2) || (puzzleRowNumber == 1 && puzzleColumnNumber == 3) || (puzzleRowNumber == 1 && puzzleColumnNumber == 4) || (puzzleRowNumber == 2 && puzzleColumnNumber == 2) || (puzzleRowNumber == 2 && puzzleColumnNumber == 3))
                    {
                        tempPuzzleCell.blocknumber = 1;
                    }
                    else if ((puzzleRowNumber == 0 && puzzleColumnNumber == 8) || (puzzleRowNumber == 1 && puzzleColumnNumber == 8) || (puzzleRowNumber == 2 && puzzleColumnNumber == 8) || (puzzleRowNumber == 3 && puzzleColumnNumber == 8) || (puzzleRowNumber == 4 && puzzleColumnNumber == 8) || (puzzleRowNumber == 5 && puzzleColumnNumber == 8) || (puzzleRowNumber == 6 && puzzleColumnNumber == 8) || (puzzleRowNumber == 7 && puzzleColumnNumber == 8) || (puzzleRowNumber == 7 && puzzleColumnNumber == 7))
                    {
                        tempPuzzleCell.blocknumber = 2;
                    }
                    else if ((puzzleRowNumber == 1 && puzzleColumnNumber == 5) || (puzzleRowNumber == 1 && puzzleColumnNumber == 6) || (puzzleRowNumber == 1 && puzzleColumnNumber == 7) || (puzzleRowNumber == 2 && puzzleColumnNumber == 4) || (puzzleRowNumber == 2 && puzzleColumnNumber == 5) || (puzzleRowNumber == 2 && puzzleColumnNumber == 6) || (puzzleRowNumber == 2 && puzzleColumnNumber == 7) || (puzzleRowNumber == 3 && puzzleColumnNumber == 6) || (puzzleRowNumber == 3 && puzzleColumnNumber == 7))
                    {
                        tempPuzzleCell.blocknumber = 3;
                    }
                    else if ((puzzleRowNumber == 3 && puzzleColumnNumber == 1) || (puzzleRowNumber == 3 && puzzleColumnNumber == 2) || (puzzleRowNumber == 4 && puzzleColumnNumber == 0) || (puzzleRowNumber == 4 && puzzleColumnNumber == 1) || (puzzleRowNumber == 5 && puzzleColumnNumber == 0) || (puzzleRowNumber == 5 && puzzleColumnNumber == 1) || (puzzleRowNumber == 6 && puzzleColumnNumber == 0) || (puzzleRowNumber == 6 && puzzleColumnNumber == 1) || (puzzleRowNumber == 7 && puzzleColumnNumber == 0))
                    {
                        tempPuzzleCell.blocknumber = 4;
                    }
                    else if ((puzzleRowNumber == 3 && puzzleColumnNumber == 3) || (puzzleRowNumber == 3 && puzzleColumnNumber == 4) || (puzzleRowNumber == 3 && puzzleColumnNumber == 5) || (puzzleRowNumber == 4 && puzzleColumnNumber == 2) || (puzzleRowNumber == 4 && puzzleColumnNumber == 3) || (puzzleRowNumber == 4 && puzzleColumnNumber == 4) || (puzzleRowNumber == 4 && puzzleColumnNumber == 5) || (puzzleRowNumber == 4 && puzzleColumnNumber == 6) || (puzzleRowNumber == 5 && puzzleColumnNumber == 2))
                    {
                        tempPuzzleCell.blocknumber = 5;
                    }
                    else if ((puzzleRowNumber == 4 && puzzleColumnNumber == 7) || (puzzleRowNumber == 5 && puzzleColumnNumber == 3) || (puzzleRowNumber == 5 && puzzleColumnNumber == 4) || (puzzleRowNumber == 5 && puzzleColumnNumber == 5) || (puzzleRowNumber == 5 && puzzleColumnNumber == 6) || (puzzleRowNumber == 5 && puzzleColumnNumber == 7) || (puzzleRowNumber == 6 && puzzleColumnNumber == 2) || (puzzleRowNumber == 6 && puzzleColumnNumber == 3) || (puzzleRowNumber == 6 && puzzleColumnNumber == 4))
                    {
                        tempPuzzleCell.blocknumber = 6;
                    }
                    else if ((puzzleRowNumber == 7 && puzzleColumnNumber == 1) || (puzzleRowNumber == 7 && puzzleColumnNumber == 2) || (puzzleRowNumber == 7 && puzzleColumnNumber == 3) || (puzzleRowNumber == 8 && puzzleColumnNumber == 0) || (puzzleRowNumber == 8 && puzzleColumnNumber == 1) || (puzzleRowNumber == 8 && puzzleColumnNumber == 2) || (puzzleRowNumber == 8 && puzzleColumnNumber == 3) || (puzzleRowNumber == 8 && puzzleColumnNumber == 4) || (puzzleRowNumber == 8 && puzzleColumnNumber == 5))
                    {
                        tempPuzzleCell.blocknumber = 7;
                    }
                    else
                    {
                        tempPuzzleCell.blocknumber = 8;
                    }
                    generatedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }


        #endregion 
    }
}
