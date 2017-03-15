using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    public partial class MainScreen : Form
    {
        #region Field Variables 
        protected TextBox currentSelectedTextBox = new TextBox();
        protected SudokuSolver sudokuSolver = new SudokuSolver();
        protected string fileDirctoryLocation = "";
        protected PuzzleManager puzzleManager = new PuzzleManager();
        protected puzzle loadedPuzzle = new puzzle();
        public static int _puzzleSelection;
        public static int _puzzleSelectionSolve;
        protected List<TextBox> listOfTextBoxes = new List<TextBox>();
        protected SudokuPuzzleGenerator sudokuPuzzleGenerator = new SudokuPuzzleGenerator(9);
        protected List<int> sudokuSolutionArray = new List<int>();
        protected int errorSubmitCount = 0;
        private static int puzzleSelectionType;
        private StatisticsManager statsManager;
        int currentLevel;
        int levelCount;
        #endregion 
        #region Constructor 
        public MainScreen()
        {
            statsManager = new StatisticsManager();
            //Gets the current level the user has completed. 
            LevelsUpdate();
            //Determines which levels should be playable
            levelCount = 1;
            //Contains logic to enable the correct levels.          
            InitializeComponent();

            //Set enabled menu options
            levelCount = 1;
            SetEnabledMenuOptions(levelsToolStripMenuItem);
        }

        #endregion

        #region Menu Option Event Handlers

        private void newPuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopUpRandomPuzzleSelection randomPuzzlePopUp = new PopUpRandomPuzzleSelection();
            randomPuzzlePopUp.ShowDialog();
            //Load buttons
            CreateRandomPuzzleButtons();
            //Create blank puzzle 
            LoadPuzzleSelection();  
        }

        private void solvePuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopUpSolverScreen solverPopUp = new PopUpSolverScreen();
            solverPopUp.ShowDialog();
            if (_puzzleSelectionSolve == 0)
            {
                loadedPuzzle.gridsize = 16;
            }
            else if (_puzzleSelectionSolve == 1)
            {

                loadedPuzzle.gridsize = 9;
            }
            else
            {
                loadedPuzzle.gridsize = 4;
            }

            loadedPuzzle.type = "regualr";
            GenerateBlankGridStandardSudoku();
            if (_puzzleSelectionSolve == 0)
            {
                GenerateLargeSudokuPuzzle();

            }
            else if (_puzzleSelectionSolve == 1)
            {
                GenerateStandardSudokuPuzzle();
            }
            else
            {
                GenerateSmallSudokuPuzzle();
            }
            CreateSolveButtons();
        }

        #endregion

        #region Levels Logic 

        /// <summary>
        /// Method to get the current level the user is up to. 
        /// </summary>
        private void LevelsUpdate()
        {
            StatisticsManager.ReadFromStatisticsFile();
            currentLevel = StatisticsManager.currentStats.levelcompleted;
        }

        private void LevelsSelectClick(object sender, EventArgs e)
        {
            CreateLevelPuzzleButtons();
            ClearGrid();
            listOfTextBoxes.Clear();
            loadedPuzzle.puzzlecells.Clear();
            var menuOption = (ToolStripMenuItem)sender;
            //This is where i will need to load the puzzle. 
            loadedPuzzle = new puzzle();
            puzzleManager = new PuzzleManager();
            loadedPuzzle.gridsize = 9;
            //Getting directory location of the loaded puzzle. 
            fileDirctoryLocation = Path.GetFullPath(@"..\..\") + @"\Puzzles\LevelsPuzzles";
            fileDirctoryLocation += @"\" + menuOption.Text + ".xml";
            LoadPuzzleFile();
        }

        /// <summary>
        /// Metthod to disable levels user cannot acces. 
        /// </summary>
        /// <param name="item"></param>
        private void SetEnabledMenuOptions(ToolStripMenuItem item)
        {
            //Going through the levels menu
            foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
            {
                if (dropDownItem.HasDropDownItems)
                {
                    foreach (ToolStripMenuItem subItem in dropDownItem.DropDownItems)
                    {
                        if (levelCount > currentLevel)
                        {
                            subItem.Enabled = false;
                        }
                        levelCount++;
                    }
                }
            }
        }
        #endregion

        #region Event Methods Random Puzzle 

        /// <summary>
        /// Method to see if the puzzle entered by the user is correct. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitPuzzleBtn_Click(object sender, EventArgs e)
        {
            UpdatePuzzle();
            //Check puzzle enetered by the user against the pre set solution. 
            bool correctPuzzle = CheckPuzzleSolution();
            if (correctPuzzle == true)
            {
                MessageBox.Show("Puzzle Completed! Well Done! Error count: " + errorSubmitCount);

            }
            else
            {
                MessageBox.Show("Puzzle incorrect. Please try again.");
            }
        }

        private void newPuzzleBtn_Click(object sender, EventArgs e)
        {
            errorSubmitCount = 0;
            ClearGrid();
            listOfTextBoxes.Clear();
            loadedPuzzle.puzzlecells.Clear();
            PopUpRandomPuzzleSelection popUpPuzzleSelection = new PopUpRandomPuzzleSelection(true);
            popUpPuzzleSelection.ShowDialog();
            LoadPuzzleSelection();
        }

        private void solveGeneratedPuzzleBtn_Click(object sender, EventArgs e)
        {
            UpdatePuzzle();
            SolvePuzzle();
        }
        #endregion

        #region Event Methods Solve Puzzle

        private void loadFileBtn_Click(object sender, EventArgs e)
        {
            fileChooser.ShowDialog();
        }

        private void fileChooser_FileOk(object sender, CancelEventArgs e)
        {
            ClearTextBoxesGrid();
            listOfTextBoxes.Clear();
            fileDirctoryLocation = fileChooser.FileName;
            loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirctoryLocation);

            List<int> listOfSudokuValues = new List<int>();

            foreach (var cell in loadedPuzzle.puzzlecells)
            {
                listOfSudokuValues.Add(cell.value);
            }
            if (loadedPuzzle.gridsize == 9)
            {
                GenerateStandardSudokuPuzzle();
            }
            else if (loadedPuzzle.gridsize == 16)
            {
                GenerateLargeSudokuPuzzle();
            }
            else
            {
                GenerateSmallSudokuPuzzle();
            }
        }

        private void ClearTextBoxesGrid()
        {
            //currently remove all textboxes when a new puzzle is selected, this may need to be changed. 
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Dispose();
            }
        }

        /// <summary>
        /// If the user wishes to determine the difficulty of the puzzle. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void difficultyDetermineBtn_Click(object sender, EventArgs e)
        {
            sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;

            sudokuSolver.EvaluatePuzzleDifficulty();
            bool puzzleSolved = sudokuSolver.BacktrackingUsingXmlTemplateFile(false);
            loadedPuzzle = sudokuSolver.currentPuzzleToBeSolved;

            for (int cellNumberCount = 0; cellNumberCount <= loadedPuzzle.puzzlecells.Count - 1; cellNumberCount++)
            {
                foreach (var textBoxCurrent in listOfTextBoxes)
                {
                    if (textBoxCurrent.Name == cellNumberCount.ToString())
                    {
                        textBoxCurrent.Text = loadedPuzzle.puzzlecells[cellNumberCount].value.ToString();
                        break;
                    }
                }
            }
            MessageBox.Show(sudokuSolver.difficluty);
        }

        /// <summary>
        /// Validate button click that determines whether the solution that has been created is correct and valid, i.e. all of the sudoku contraints are met. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validatePuzzleBtn_Click(object sender, EventArgs e)
        {   //Validating puzzle 
            bool validRow = false;
            bool validColumn = false;
            bool validBlock = false;
            validRow = ValidateRow();
            validColumn = ValidateColumn();
            validBlock = ValidateBlock();
            //If puzzle is correct
            if (validRow == true && validColumn == true && validBlock == true)
            {
                MessageBox.Show("Puzzle solution correct");
            }
            else //If puzzle is incorrect
            {
                MessageBox.Show("Puzzle solution incorrect!");
            }
        }

        #endregion

        #region Event Methods Levels Puzzle 

        /// <summary>
        /// Method to see if the puzzle entered by the user is correct. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submitLevelPuzzleBtn_Click(object sender, EventArgs e)
        {
            UpdatePuzzle();
            //Check puzzle enetered by the user against the pre set solution. 
            bool correctPuzzle = CheckPuzzleSolution();
            if (correctPuzzle == true)
            {
                MessageBox.Show("Puzzle Completed! Well Done! Error count: " + errorSubmitCount);

                //Need to then handle Setting the current level the user is on. 

            }
            else
            {
                MessageBox.Show("Puzzle incorrect. Please try again.");
            }
        }

        #endregion

        #region Methods Random Puzzle 

        private void ClearGrid()
        {
            //currently remove all textboxes when a new puzzle is selected, this may need to be changed. 
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Dispose();
            }
        }
        #endregion

        #region Event Handler Methods Template

        protected void puzzleTextChange(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;
        }
        protected void puzzleSquareClick(object sender, EventArgs e)
        {
            //Disabling the previous textbox
            currentSelectedTextBox.ReadOnly = true;
            var tb = (TextBox)sender; //Attaining the textbox that has just been clicked. 
            tb.ReadOnly = false;

            //Setting the selected textbox as the current one. 
            currentSelectedTextBox = tb;
        }

        protected void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        /// <summary>
        /// Method to handle the key presses witin the sudoku cells, to ensure only number 1-9 are entrered. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckCellEntry(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Cell entry validation, if the entered character is between 1 - 9, then enter it into the text box. 
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion

        #region Load Puzzle Methods

        protected void LoadPuzzleFile()
        {
            //Creating the sudoku grid values. 
            loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirctoryLocation);
            GenerateStandardSudokuPuzzle();
        }

        protected void LoadPuzzleSelection()
        {
            //Determinging which sudoku puzzle is generated based upon the users selection. 
            if (_puzzleSelection == 2)
            {

                loadedPuzzle.gridsize = 9;
                GenerateBlankGridStandardSudoku();
                GeneratePuzzle();
                GenerateStandardSudokuPuzzle();
            }
            else if (_puzzleSelection == 0)
            {
                loadedPuzzle.gridsize = 9;
                //Seleting which irregular template to use. 
                Random newRandomNumber = new Random();
                int irregularRandom = newRandomNumber.Next(0, 1);
                if (irregularRandom == 1)
                {
                    //GenerateFirstTemplateIrregular();
                }
                else
                {
                    //GenerateSecondTemplateIrregular();
                }
                GenerateStandardSudokuPuzzle();
            }
            else if (_puzzleSelection == 1)
            {
                loadedPuzzle.gridsize = 16;
                GenerateBlankGridStandardSudoku();
                GeneratePuzzle();
                GenerateLargeSudokuPuzzle();
            }
            else
            {
                loadedPuzzle.gridsize = 4;
                GenerateBlankGridStandardSudoku();
                GeneratePuzzle();
                GenerateSmallSudokuPuzzle();
            }
        }

        private void GeneratePuzzle()
        {
            sudokuPuzzleGenerator.generatedPuzzle = loadedPuzzle;
            sudokuPuzzleGenerator.CreateSudokuGridXML();
        }

        /// <summary>
        /// Method to update the puzzle with the current values entered into the textboxes. 
        /// </summary>
        protected void UpdatePuzzle()
        {
            for (int index = 0; index <= listOfTextBoxes.Count - 1; index++)
            {
                if (listOfTextBoxes[index].Text == "")
                {
                    loadedPuzzle.puzzlecells[index].value = 0;
                }
                else
                {
                    loadedPuzzle.puzzlecells[index].value = int.Parse(listOfTextBoxes[index].Text);
                }
            }
        }

        protected bool CheckSubmittedPuzzleXML()
        {
            //Update generated puzzle 
            UpdatePuzzle();
            for (int indexValue = 0; indexValue <= loadedPuzzle.puzzlecells.Count - 1; indexValue++)
            {
                if (loadedPuzzle.puzzlecells[indexValue].value != loadedPuzzle.puzzlecells[indexValue].solutionvalue)
                {
                    bool validRow = false;
                    bool validColumn = false;
                    bool validBlock = false;
                    validRow = ValidateRow();
                    validColumn = ValidateColumn();
                    validBlock = ValidateBlock();
                    if (validBlock == true && validColumn == true && validRow == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Method to check whether the solution the user has entered is correct. 
        /// </summary>
        protected bool CheckPuzzleSolution()
        {
            List<int> errorCell = new List<int>();
            for (int index = 0; index <= loadedPuzzle.puzzlecells.Count - 1; index++)
            {
                if (loadedPuzzle.puzzlecells[index].value != loadedPuzzle.puzzlecells[index].solutionvalue)
                {
                    errorCell.Add(index);

                }
            }
            if (errorCell.Count > 0)
            {
                //Set error cells
                SetErrorCells(errorCell);

                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetErrorCells(List<int> errorCellNumbers)
        {
            for (int cellNumber = 0; cellNumber <= listOfTextBoxes.Count - 1; cellNumber++)
            {
                if (listOfTextBoxes[cellNumber].BackColor == Color.Red)
                {
                    //Resetting colours of grid. 
                    //Getting colour of cell. 
                    if (loadedPuzzle.gridsize == 9)
                    {
                        GetStandardPuzzleColour(cellNumber);
                    }
                    else if (loadedPuzzle.gridsize == 4)
                    {
                        GetSmallPuzzleColour(cellNumber);
                    }
                    else
                    {
                        GetLargePuzzleColour(cellNumber);
                    }
                }
                foreach (var errorCell in errorCellNumbers)
                {

                    if (cellNumber == errorCell && loadedPuzzle.puzzlecells[cellNumber].value != 0)
                    {
                        errorSubmitCount += 1;
                        listOfTextBoxes[cellNumber].BackColor = Color.Red;
                    }
                }
            }
        }

        #endregion

        #region Solve Puzzle 

        protected void SolvePuzzle()
        {
            sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;
            Stopwatch tempStopWatch = new Stopwatch();
            tempStopWatch.Reset();
            tempStopWatch.Start();
            bool puzzleSolved = sudokuSolver.SolveSudokuRuleBasedXML();
            Console.WriteLine(tempStopWatch.Elapsed.TotalSeconds);
            Console.WriteLine(tempStopWatch.Elapsed.TotalMilliseconds);
            tempStopWatch.Stop();
            loadedPuzzle = sudokuSolver.currentPuzzleToBeSolved;

            if (puzzleSolved == true)
            {
                for (int cellNumberCount = 0; cellNumberCount <= loadedPuzzle.puzzlecells.Count - 1; cellNumberCount++)
                {
                    foreach (var textBoxCurrent in listOfTextBoxes)
                    {
                        if (textBoxCurrent.Name == cellNumberCount.ToString())
                        {
                            textBoxCurrent.Text = loadedPuzzle.puzzlecells[cellNumberCount].value.ToString();
                            break;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No solution, invalid solution");
            }
        }

        #endregion 

        #region Blank Grid Methods

        /// <summary>
        /// Method to generate a blank sudoku grid 
        /// </summary>
        protected void GenerateBlankGridStandardSudoku()
        {   //Creating a blank grid depending on the 
            for (int puzzleRowNumber = 0; puzzleRowNumber <= loadedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= loadedPuzzle.gridsize - 1; puzzleColumnNumber++)
                {
                    puzzleCell tempPuzzleCell = new puzzleCell();
                    tempPuzzleCell.rownumber = puzzleRowNumber;
                    tempPuzzleCell.columnnumber = puzzleColumnNumber;
                    if (loadedPuzzle.gridsize == 4)
                    {
                        tempPuzzleCell.blocknumber = GetBlockFour(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else if (loadedPuzzle.gridsize == 9)
                    {
                        tempPuzzleCell.blocknumber = GetBlockNumberNine(puzzleRowNumber, puzzleColumnNumber);
                    }
                    else
                    {
                        tempPuzzleCell.blocknumber = GetBlocNumberSixteen(puzzleRowNumber, puzzleColumnNumber);
                    }
                    loadedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }
        #endregion

        #region Block Numbers Methods 

        //Methods to get the block number for the cell that i currently being handled. 
        protected int GetBlockFour(int tempRowNumber, int tempColumnNumber)
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

        protected int GetBlockNumberNine(int tempRowNumber, int tempColumnNumber)
        {
            double blockValue = Math.Sqrt(loadedPuzzle.gridsize);
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

        protected int GetBlocNumberSixteen(int tempRowNumber, int tempColumnNumber)
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

        #region Validate Puzzles Method 
        //Methods to validate whether a row, column or block is correct, with the sudoku constraints.
        protected bool ValidateRow()
        {
            List<int> numbersInRow = new List<int>();
            bool validRows = true;
            for (int rowNumberValidate = 0; rowNumberValidate <= 8; rowNumberValidate++)
            {
                //Adding all number is that row to the list. 
                foreach (var cell in loadedPuzzle.puzzlecells)
                {
                    if (cell.rownumber == rowNumberValidate)
                    {
                        numbersInRow.Add(cell.value);
                    }
                }
                //Check valid numbers 
                validRows = CheckValidNumbers(numbersInRow);
                if (validRows == false)
                {
                    return false;
                }
                numbersInRow.Clear();
            }
            return true;
        }
        protected bool ValidateColumn()
        {
            List<int> numbersInColumn = new List<int>();
            bool validColumns = true;
            for (int columnNumberValidate = 0; columnNumberValidate <= 8; columnNumberValidate++)
            {
                //Adding all number is that row to the list. 
                foreach (var cell in loadedPuzzle.puzzlecells)
                {
                    if (cell.columnnumber == columnNumberValidate)
                    {
                        numbersInColumn.Add(cell.value);
                    }
                }
                //Check valid numbers 
                validColumns = CheckValidNumbers(numbersInColumn);
                if (validColumns == false)
                {
                    return false;
                }
                numbersInColumn.Clear();
            }
            return true;
        }
        protected bool ValidateBlock()
        {
            List<int> numbersInBlock = new List<int>();
            bool validBlocks = true;
            for (int blockNumberValidate = 0; blockNumberValidate <= 8; blockNumberValidate++)
            {
                //Adding all number is that row to the list. 
                foreach (var cell in loadedPuzzle.puzzlecells)
                {
                    if (cell.blocknumber == blockNumberValidate)
                    {
                        numbersInBlock.Add(cell.value);
                    }
                }
                //Check valid numbers 
                validBlocks = CheckValidNumbers(numbersInBlock);
                if (validBlocks == false)
                {
                    return false;
                }
                numbersInBlock.Clear();
            }
            return true;
        }

        /// <summary>
        /// Method that checks to ensure the numbers in a region are correct and there is numbers 1-9 exaclty etc. 
        /// </summary>
        /// <param name="listOfNumbers"></param>
        /// <returns></returns>
        private bool CheckValidNumbers(List<int> listOfNumbers)
        {
            List<int> numbersUsed = new List<int>();
            foreach (var number in listOfNumbers)
            {
                if (number == 0)
                {
                    return false;
                }
                foreach (var usedNumber in numbersUsed)
                {
                    if (usedNumber == number)
                    {
                        return false;
                    }
                    else
                    {
                        numbersUsed.Add(number);
                    }
                }
            }
            return true;
        }

        #endregion

        #region Get Cell Colour Methods 

        private void GetStandardPuzzleColour(int textBoxNumber)
        {
            switch (loadedPuzzle.puzzlecells[textBoxNumber].blocknumber)
            {
                case (0):
                case (8):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightGreen;
                    break;
                case (1):
                case (7):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.Pink;
                    break;
                case (2):
                case (6):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightCyan;
                    break;
                case (3):
                case (5):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightYellow;
                    break;
                default:
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightBlue;
                    break;
            }
        }

        private void GetLargePuzzleColour(int textBoxNumber)
        {
            switch (loadedPuzzle.puzzlecells[textBoxNumber].blocknumber)
            {
                case (0):
                case (3):
                case (12):
                case (15):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightGreen;
                    break;
                case (1):
                case (4):
                case (14):
                case (11):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.Pink;
                    break;
                case (2):
                case (8):
                case (7):
                case (13):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightYellow;
                    break;
                default:
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightBlue;
                    break;
            }

        }

        private void GetSmallPuzzleColour(int textBoxNumber)
        {
            switch (loadedPuzzle.puzzlecells[textBoxNumber].blocknumber)
            {
                case (0):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightGreen;
                    break;
                case (1):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.Pink;
                    break;
                case (2):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightCyan;
                    break;
                case (3):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightYellow;
                    break;
                default:
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightBlue;
                    break;
            }
        }

        #endregion

        #region Generating Puzzles
        ///Method to generate random stadnard puzzle  
        protected void GenerateStandardSudokuPuzzle()
        {
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= loadedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                //Creating a textbox for the each cell, with the valid details. 
                TextBox txtBox = new TextBox();
                listOfTextBoxes.Add(txtBox);
                this.Controls.Add(txtBox);
                txtBox.Name = indexNumber.ToString();
                //txtBox.ReadOnly = true;
                txtBox.Size = new System.Drawing.Size(38, 38);
                txtBox.TabIndex = 0;
                txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                txtBox.Click += new System.EventHandler(this.puzzleSquareClick);
                txtBox.TextChanged += new System.EventHandler(this.puzzleTextChange);
                //Key press handler to only allow digits 1-9 in the textboxes. 
                txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckCellEntry);
                //Limiting the text box to only on character. 
                txtBox.MaxLength = 1;
                //Setting the value in the grid text box. 
                txtBox.Text = loadedPuzzle.puzzlecells[indexNumber].value.ToString();

                //Clouring 
                txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                txtBox.ForeColor = Color.Black;

                GetStandardPuzzleColour(indexNumber);

                //Ensuring static numbers can not be edited. 
                if (loadedPuzzle.puzzlecells[indexNumber].value != 0)
                {
                    txtBox.Enabled = false;
                }
                else
                {
                    txtBox.Text = "";
                }
                //Position logic
                if (indexNumber == 0)
                {
                    rowLocation = rowLocation + 83;
                    columnLocation = columnLocation + 50;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                else if (indexNumber == 8 || indexNumber % 9 == 8)
                {
                    rowLocation += 38;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                    rowLocation = 45;
                    columnLocation += 17;
                }
                else
                {
                    rowLocation += 38;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }


            }

            SudokuSolver sudokuSolver = new SudokuSolver();
            sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;
            sudokuSolver.EvaluatePuzzleDifficulty();
            string difficulty = sudokuSolver.difficluty;
            //MessageBox.Show(difficulty);
        }
        /// <summary>
        /// Method to generate random large puzzle. 
        /// </summary>
        protected void GenerateLargeSudokuPuzzle()
        {
            sudokuPuzzleGenerator.generatedPuzzle = loadedPuzzle;
            sudokuPuzzleGenerator.CreateSudokuGridXML();
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= loadedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                //Creating a textbox for the each cell, with the valid details. 
                TextBox txtBox = new TextBox();
                this.Controls.Add(txtBox);
                txtBox.Name = indexNumber.ToString();
                txtBox.Size = new System.Drawing.Size(26, 26);
                txtBox.TabIndex = 0;
                txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                txtBox.Font = new Font(txtBox.Font.FontFamily, 7);
                txtBox.Click += new System.EventHandler(this.puzzleSquareClick);
                txtBox.TextChanged += new System.EventHandler(this.puzzleTextChange);
                //Key press handler to only allow digits 1-9 in the textboxes. 
                txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckCellEntry);
                //Limiting the text box to only on character. 
                txtBox.MaxLength = 1;
                //Setting the value in the grid text box. 
                txtBox.Text = loadedPuzzle.puzzlecells[indexNumber].value.ToString();

                //Clouring 
                txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                txtBox.ForeColor = Color.Black;

                GetLargePuzzleColour(indexNumber);
                //Ensuring static numbers can not be edited. 
                if (loadedPuzzle.puzzlecells[indexNumber].value != 0)
                {
                    txtBox.Enabled = false;
                }
                else
                {
                    txtBox.Text = "";
                }
                //Position logic
                if (indexNumber == 0)
                {
                    rowLocation = rowLocation + 106;
                    columnLocation = columnLocation + 100;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                else if (indexNumber == 15 || indexNumber % 16 == 15)
                {
                    rowLocation += 26;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                    columnLocation += 13;
                    rowLocation = 80;
                }
                else
                {
                    rowLocation += 26;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }

                listOfTextBoxes.Add(txtBox);
            }

        }
        /// <summary>
        /// Method to generate random small sudoku puzzle. 
        /// </summary>
        protected void GenerateSmallSudokuPuzzle()
        {
            sudokuPuzzleGenerator.generatedPuzzle = loadedPuzzle;
            sudokuPuzzleGenerator.CreateSudokuGridXML();
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= loadedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                //Creating a textbox for the each cell, with the valid details. 
                TextBox txtBox = new TextBox();
                this.Controls.Add(txtBox);
                txtBox.Name = indexNumber.ToString();
                //txtBox.ReadOnly = true;
                txtBox.Size = new System.Drawing.Size(46, 60);
                txtBox.TabIndex = 0;
                txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                txtBox.Font = new Font(txtBox.Font.FontFamily, 20);
                txtBox.Click += new System.EventHandler(this.puzzleSquareClick);
                txtBox.TextChanged += new System.EventHandler(this.puzzleTextChange);
                //Key press handler to only allow digits 1-9 in the textboxes. 
                txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckCellEntry);
                //Limiting the text box to only on character. 
                txtBox.MaxLength = 1;
                //Setting the value in the grid text box. 
                txtBox.Text = loadedPuzzle.puzzlecells[indexNumber].value.ToString();

                //Clouring 
                txtBox.Font = new Font(txtBox.Font, FontStyle.Bold);
                txtBox.ForeColor = Color.Black;

                GetSmallPuzzleColour(indexNumber);

                //Ensuring static numbers can not be edited. 
                if (loadedPuzzle.puzzlecells[indexNumber].value != 0)
                {
                    txtBox.Enabled = false;
                }
                else
                {
                    txtBox.Text = "";
                }
                //Position logic
                if (indexNumber == 0)
                {
                    rowLocation = rowLocation + 210;
                    columnLocation = columnLocation + 110;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                else if (indexNumber == 3 || indexNumber % 4 == 3)
                {
                    rowLocation += 46;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                    rowLocation = 164;
                    columnLocation += 38;
                }
                else
                {
                    rowLocation += 46;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                listOfTextBoxes.Add(txtBox);
            }
        }
        #endregion

        #region IrregularPuzzles
        protected void GenerateFirstTemplateIrregular()
        {
            for (int puzzleRowNumber = 0; puzzleRowNumber <= loadedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= loadedPuzzle.gridsize - 1; puzzleColumnNumber++)
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
                    loadedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }

        protected void GenerateSecondTemplateIrregular()
        {
            for (int puzzleRowNumber = 0; puzzleRowNumber <= loadedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= loadedPuzzle.gridsize - 1; puzzleColumnNumber++)
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
                    loadedPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }
        #endregion
    }
}
