using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    public partial class MainScreen : Form
    {
        #region Field Variables 
        //Contains the text box that is currently being handled
        protected TextBox currentSelectedTextBox = new TextBox();
        protected List<TextBox> listOfTextBoxes = new List<TextBox>();
        //Solver to solve puzzles
        protected SudokuSolver sudokuSolver;
        protected string fileDirctoryLocation = "";
        protected PuzzleManager puzzleManager;
        //Puzzle that is being handled on the screen. 
        protected puzzle loadedPuzzle;
        //Selections from option boxes, to load specific puzzle. 
        public static int _puzzleSelection;
        public static int _puzzleSelectionSolve;
        //Generator 
        protected SudokuPuzzleGenerator sudokuPuzzleGenerator;
        //Error count in currently submitted puzzle. 
        protected int errorSubmitCount = 0;
        //Manage stats for users. 
        private StatisticsManager statsManager;

        //Currernt time 
        int currentTime = 0;
        //Level Variables 
        int currentLevel;
        int levelCount;
        int levelSelected;
        //Scores
        int currentScore = 0;
        #endregion

        #region Constructor 
        public MainScreen()
        {
            statsManager = new StatisticsManager();
            sudokuSolver = new SudokuSolver();
            puzzleManager = new PuzzleManager();
            loadedPuzzle = new puzzle();
            sudokuPuzzleGenerator = new SudokuPuzzleGenerator();
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
            //Displaying selection box. 
            PopUpRandomPuzzleSelection randomPuzzlePopUp = new PopUpRandomPuzzleSelection();
            randomPuzzlePopUp.ShowDialog();
            //If one has been selected. 
            if (PopUpRandomPuzzleSelection.isPuzzleTypeSelected)
            {
                // Clearing the screen and displaying the puzzle selection pop up.
                ClearScreen();
                //Load buttons         
                CreateRandomPuzzleButtons();
                //Setting screen title 
                this.Text = "Random Puzzle : Siwel Sudoku";
                //Create blank puzzle 
                LoadPuzzleSelection();
                //Start puzzle attempt. 
                StartTimerAndAddInfo();
                SetStartingScore();
            }
        }

        private void LevelsSelectClick(object sender, EventArgs e)
        {
            //Resetting the solving time. 
            currentTime = 0;
            ClearScreen();
            CreateLevelPuzzleButtons();
            var menuOption = (ToolStripMenuItem)sender;
            //Getting directory location of the loaded puzzle. 
            fileDirctoryLocation = Path.GetFullPath(@"..\..\") + @"\Puzzles\LevelsPuzzles";
            fileDirctoryLocation += @"\" + menuOption.Text + ".xml";

            //Get the level selected. 
            string levelString = Regex.Match(menuOption.Text, @"\d+").Value;
            if (levelString != "")
            {
                levelSelected = Int32.Parse(levelString);
            }
            //Maually setting level number, if different format. 
            if (menuOption.Text == "irregular")
            {
                levelSelected = 15;
            }
            else if (menuOption.Text == "smallgrid1")
            {
                levelSelected = 13;
            }
            else if (menuOption.Text == "smallgrid2")
            {
                levelSelected = 14;
            }

            //Loading the puzzle from storage. 
            LoadPuzzleFile();
            //Setting starting score for game
            SetStartingScore();

            //Diplay set up
            this.Text = "Level:" + levelSelected.ToString() + " : Siwel Sudoku"; ;
            StartTimerAndAddInfo();
        }

        private void solvePuzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creating blank grid. 
            CreateSolveGrid();
        }

        /// <summary>
        /// Method when in development to display development screen. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void developmentBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            DevelopmentForm developmentFrm = new DevelopmentForm();
            developmentFrm.Show();
        }
        #endregion

        #region ClearScreen

        /// <summary>
        /// Method to clear the screen, so an other option can be displayed. 
        /// </summary>
        private void ClearScreen()
        {
            errorSubmitCount = 0;
            solutionDisplayInfoTb.Visible = false;
            logoStartUpPb.Visible = false;
            developmentBtn.Visible = false;
            ClearGrid();
            DeleteButtons();
            listOfTextBoxes.Clear();
            loadedPuzzle.puzzlecells.Clear();
            staticsDispalyTb.Visible = false;
            puzzlesInformationTb.Visible = false;
            timerText.Visible = false;
        }

        private void ClearGrid()
        {
            //currently remove all textboxes when a new puzzle is selected, this may need to be changed. 
            foreach (var textBox in listOfTextBoxes)
            {
                textBox.Dispose();
            }
        }

        private void DeleteButtons()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (Controls[i] is Button)
                {
                    Controls.RemoveAt(i);
                    i--;
                }
            }
        }

        #endregion

        #region Set Information TextBox text 

        private void SetInformationText()
        {
            StatisticsManager.ReadFromStatisticsFile();
            //Setting up the score and the difficulty of the current puzzle the user is solving. 
            puzzlesInformationTb.Text = "Difficulty= " + loadedPuzzle.difficulty + " Error count= " + errorSubmitCount + " Score= " + currentScore +
                " Hints: " + StatisticsManager.currentStats.hintNumber;
        }

        private void StartTimerAndAddInfo()
        {
            SetInformationText();
            currentTime = 0;
            //Resetting time and making the timer text box visible. 
            puzzleTimer.Stop();
            puzzleTimer.Start();
            timerText.Visible = true;
            puzzlesInformationTb.Visible = true;
        }

        private void SetSolvingDetailsToTextBox(double executionTime, string uniqueSting, string difficultyString)
        {
            //Getting the execution time
            string executionDisplayString = "";
            if (executionTime == 0.0)
            {
                executionDisplayString = "N/A";
            }
            else
            {
                executionDisplayString = executionTime.ToString();
            }
            solutionDisplayInfoTb.Visible = true;
            //Setting up the score and the difficulty of the current puzzle the user is solving. 
            solutionDisplayInfoTb.Text = "Difficulty= " + difficultyString + " Solving Time= "
                + executionDisplayString + " (ms) Mutilpe Solutions= " + uniqueSting;
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
                        //Enabling and diabling menu options. 
                        if (levelCount > currentLevel)
                        {
                            subItem.Enabled = false;
                        }
                        else
                        {
                            subItem.Enabled = true;
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
                //Puzzle completion, stoping time, displaying current score 
                puzzleTimer.Stop();
                TimeSpan time = TimeSpan.FromSeconds(currentTime);
                MessageBox.Show("Puzzle Completed! Well Done! Score: " + currentScore);
                StatisticsManager.RandomPuzzleCompleted(loadedPuzzle.difficulty, currentScore, (decimal)time.TotalSeconds, loadedPuzzle.type);
                SetInformationText();
            }
            else
            {
                MessageBox.Show("Puzzle incorrect. Please try again.");
            }

        }

        private void newPuzzleBtn_Click(object sender, EventArgs e)
        {
            //Puzzle selection
            PopUpRandomPuzzleSelection popUpPuzzleSelection = new PopUpRandomPuzzleSelection();
            popUpPuzzleSelection.ShowDialog();
            //Making sure one has been selected. 
            if (PopUpRandomPuzzleSelection.isPuzzleTypeSelected)
            {
                errorSubmitCount = 0;
                ClearGrid();
                listOfTextBoxes.Clear();
                loadedPuzzle.puzzlecells.Clear();
                LoadPuzzleSelection();
                SetStartingScore();
                //Reseting page for new puzzle. 
                StartTimerAndAddInfo();
            }
        }

        //Method only used in development. 
        private void solveGeneratedPuzzleBtn_Click(object sender, EventArgs e)
        {
            UpdatePuzzle();
            SolvePuzzle();
        }
        #endregion

        #region Event Methods Solve Puzzle

        /// <summary>
        /// Used for development to allow the user to select a file to load. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadFileBtn_Click(object sender, EventArgs e)
        {
            fileChooser.ShowDialog();
        }

        /// <summary>
        /// For development, when the user has chosen a puzzle to load. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileChooser_FileOk(object sender, CancelEventArgs e)
        {
            //Clearing current puzzle and loading in the new one.
            ClearTextBoxesGrid();
            listOfTextBoxes.Clear();
            fileDirctoryLocation = fileChooser.FileName;
            loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirctoryLocation);
            //Generating puzzle based on selection. 
            if (loadedPuzzle.gridsize == 9)
            {
                GenerateStandardSudokuPuzzle(false);
            }
            else
            {
                GenerateSmallSudokuPuzzle(true);
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
            bool validPuzle = ValidateSolvePuzzleEntry();
            int emptyCellCountNumber = 0;
            bool fewStaticNumbers = false;
            for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
            {
                if (loadedPuzzle.puzzlecells[cellNumber].value == 0)
                {
                    emptyCellCountNumber++;
                }
            }

            if (emptyCellCountNumber >= 56)
            {
                fewStaticNumbers = true;
            }
            if (validPuzle)
            {
                sudokuSolver = new SudokuSolver();
                //Timing algorithm and determining difficulty. 
                sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;
                Stopwatch executionTimeSw = new Stopwatch();
                executionTimeSw.Start();
                string uniqueString = sudokuSolver.EvaluatePuzzleDifficulty();

                if (fewStaticNumbers && uniqueString != "Error")
                {
                    uniqueString = "Not Unique";
                }
                //If there is no error then all the stuff.
                if (uniqueString != "Error")
                {
                    executionTimeSw.Stop();
                    double executionTimeValue = executionTimeSw.Elapsed.Milliseconds;
                    executionTimeSw.Reset();
                    loadedPuzzle.difficulty = sudokuSolver.difficluty;
                    //Updating puzzle on the screen. 
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
                    //Updating details of the puzzle that has been entered. 
                    SetSolvingDetailsToTextBox(executionTimeValue, uniqueString, loadedPuzzle.difficulty);
                }
                else
                {
                    executionTimeSw.Stop();
                    executionTimeSw.Reset();
                    MessageBox.Show("Puzzle cannot be successfully solved. Irregular solving functionality is not fully correct within the game.");
                }
            }
            else
            {
                MessageBox.Show("Incorrect puzzle entered! Please check entry.");
            }
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

        /// <summary>
        /// Resetting puzzle on clear click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearPuzzleBtn_Click(object sender, EventArgs e)
        {
            //Resetting puzzle. 
            SetSolvingDetailsToTextBox(0.0, "N/A", "N/A");
            foreach (var tb in listOfTextBoxes)
            {
                tb.Text = "";
                tb.Enabled = true;
            }
        }

        /// <summary>
        /// Selects new puzzle to solve. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newSolvePuzzleBtn_Click(object sender, EventArgs e)
        {
            CreateSolveGrid();
        }

        /// <summary>
        /// Method to load a new blank grid for the user. 
        /// </summary>
        private void CreateSolveGrid()
        {
            //Solving puzzle option. 
            PopUpSolverScreen solverPopUp = new PopUpSolverScreen();
            solverPopUp.ShowDialog();
            if (PopUpSolverScreen.isPuzzleSelected)
            {
                this.Text = "Solve Puzzle : Siwel Sudoku";
                ClearScreen();
                //Setting puzzle details. 
                SetSolvingDetailsToTextBox(0.0, "N/A", "N/A");
                if (_puzzleSelectionSolve == 3)
                {
                    loadedPuzzle.gridsize = 4;
                }
                else
                {
                    loadedPuzzle.gridsize = 9;
                }
                //Creating the blank grid for the screen. 
                loadedPuzzle.type = "regular";
                if (_puzzleSelectionSolve >= 2)
                {
                    GenerateBlankGridStandardSudoku();
                }
                //Generating random puzzle based on selection. 
                if (_puzzleSelectionSolve == 0)
                {
                    loadedPuzzle.type = "irregular";
                    GenerateFirstTemplateIrregular();
                    GenerateStandardSudokuPuzzle(false);
                }
                else if (_puzzleSelectionSolve == 1)
                {
                    loadedPuzzle.type = "irregular";
                    GenerateSecondTemplateIrregular();
                    GenerateStandardSudokuPuzzle(false);
                }
                else if (_puzzleSelectionSolve == 2)
                {
                    GenerateStandardSudokuPuzzle(false);
                }
                else
                {
                    GenerateSmallSudokuPuzzle(true);
                }
                //Creating the solve button, to provide actions. 
                CreateSolveButtons();
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
            //Updating the puzzle object with the values within the textboxes on the puzzle.           
            UpdatePuzzle();
            //Check puzzle enetered by the user against the pre set solution. 
            bool correctPuzzle = CheckPuzzleSolution();
            if (correctPuzzle == true)
            {
                //Puzzle completion
                puzzleTimer.Stop();
                MessageBox.Show(" Level:" + levelSelected + " completed! Well Done!  Final Score: " + currentScore);
                TimeSpan time = TimeSpan.FromSeconds(currentTime);
                string puzzleDifficulty = loadedPuzzle.difficulty.ToLower();
                bool extremeBool = false;
                if (puzzleDifficulty == "extreme")
                {
                    extremeBool = true;
                }
                //If the user completes the level that is last on their list, unlock the next one. 
                if (currentLevel == levelSelected)
                {
                    StatisticsManager.currentStats.levelcompleted++;
                }
                StatisticsManager.LeveledPuzzleComlpeted(StatisticsManager.currentStats.levelcompleted, (decimal)time.TotalSeconds, extremeBool, puzzleDifficulty, currentScore, loadedPuzzle.type, levelSelected);
                LevelsUpdate();
                //Set enabled menu options
                levelCount = 1;
                SetEnabledMenuOptions(levelsToolStripMenuItem);
            }
            else
            {
                MessageBox.Show("Puzzle incorrect. Please try again.");
            }
            SetInformationText();
        }

        /// <summary>
        /// Method to get hints for the puzzle. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hintsBtn_Click(object sender, EventArgs e)
        {
            //Updating puzzle and reading hints. 
            UpdatePuzzle();
            StatisticsManager.ReadFromStatisticsFile();
            int hintNumber = StatisticsManager.currentStats.hintNumber;
            //Updating hint count if possible.
            if (hintNumber > 0)
            {
                RevealValueFromHint();
                StatisticsManager.UpdateHints(-1);
                LevelsUpdate();
            }
            else
            {
                MessageBox.Show("No hints left!");
            }
            //Updating info. 
            SetInformationText();
        }

        /// <summary>
        /// Method to get region hints for the puzzle. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hintsRegionBtn_Click(object sender, EventArgs e)
        {
            UpdatePuzzle();
            StatisticsManager.ReadFromStatisticsFile();
            int hintNumber = StatisticsManager.currentStats.hintNumber;
            //Providing the hint adn revealing the region. 
            if (hintNumber >= 5)
            {
                RevealValueFromRegionHint();
                StatisticsManager.UpdateHints(-5);
                LevelsUpdate();
            }
            else
            {
                MessageBox.Show("No hints left!");
            }
            SetInformationText();
        }

        /// <summary>
        /// Method to get tip for the puzzle. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tipBtn_Click(object sender, EventArgs e)
        {
            //Getting the current statisitcs. 
            StatisticsManager.ReadFromStatisticsFile();
            int currentHintNumber = StatisticsManager.currentStats.hintNumber;
            if (currentHintNumber >= 5)
            {
                //Decreasing number of hints by 5 and storing this. 
                currentHintNumber -= 5;
                StatisticsManager.UpdateHints(-5);
                //Getting a random tip to display. 
                Random tipRandomNumber = new Random();
                int tipValue = tipRandomNumber.Next(1, 10);
                //Displaying the tip. 
                TipScreen tipScreen = new TipScreen(tipValue);
                tipScreen.ShowDialog();
            }
            else
            {
                //If the user does not have enough hints for a tip. 
                MessageBox.Show("Not hints left!");
            }
        }

        private void RevealValueFromRegionHint()
        {
            //Random value to select a region, 
            Random randomRegion = new Random();
            //Getting either a block, column or row. 
            int randomRegionValue = randomRegion.Next(1, 4);
            List<int> regionValues = new List<int>();
            //Making sure the region has blank values before completin it. 
            int blankCount = 0;

            //List of cells edited. 
            List<int> cellNumbersEdited = new List<int>();
            //Filling in a region, from a random choice. 
            if (randomRegionValue == 1)
            {
                for (int rowNumberTemp = 0; rowNumberTemp <= loadedPuzzle.gridsize - 1; rowNumberTemp++)
                {
                    //row
                    for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
                    {
                        if (loadedPuzzle.puzzlecells[cellNumber].rownumber == rowNumberTemp)
                        {
                            regionValues.Add(loadedPuzzle.puzzlecells[cellNumber].value);
                        }
                    }
                    for (int rowNumberValueIndex = 0; rowNumberValueIndex <= regionValues.Count - 1; rowNumberValueIndex++)
                    {
                        if (regionValues[rowNumberValueIndex] == 0)
                        {
                            blankCount++;
                        }
                    }
                    if (blankCount >= 1)
                    {
                        //Complete region 
                        for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
                        {
                            if (loadedPuzzle.puzzlecells[cellNumber].rownumber == rowNumberTemp)
                            {
                                cellNumbersEdited.Add(cellNumber);
                                loadedPuzzle.puzzlecells[cellNumber].value = loadedPuzzle.puzzlecells[cellNumber].solutionvalue;
                            }
                        }
                        break;
                    }
                }
            }
            else if (randomRegionValue == 2)
            {
                //column
                for (int columnNumberTemp = 0; columnNumberTemp <= loadedPuzzle.gridsize - 1; columnNumberTemp++)
                {
                    for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
                    {
                        if (loadedPuzzle.puzzlecells[cellNumber].columnnumber == columnNumberTemp)
                        {
                            regionValues.Add(loadedPuzzle.puzzlecells[cellNumber].value);
                        }
                    }
                    for (int columnNumberValueIndex = 0; columnNumberValueIndex <= regionValues.Count - 1; columnNumberValueIndex++)
                    {
                        if (regionValues[columnNumberValueIndex] == 0)
                        {
                            blankCount++;
                        }
                    }
                    if (blankCount >= 1)
                    {
                        //Complete region 
                        for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
                        {
                            if (loadedPuzzle.puzzlecells[cellNumber].columnnumber == columnNumberTemp)
                            {
                                cellNumbersEdited.Add(cellNumber);
                                loadedPuzzle.puzzlecells[cellNumber].value = loadedPuzzle.puzzlecells[cellNumber].solutionvalue;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                //block 
                for (int blockNumberTemp = 0; blockNumberTemp <= loadedPuzzle.gridsize - 1; blockNumberTemp++)
                {
                    for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
                    {
                        if (loadedPuzzle.puzzlecells[cellNumber].blocknumber == blockNumberTemp)
                        {
                            regionValues.Add(loadedPuzzle.puzzlecells[cellNumber].value);
                        }
                    }
                    for (int blockNumberValueIndex = 0; blockNumberValueIndex <= regionValues.Count - 1; blockNumberValueIndex++)
                    {
                        if (regionValues[blockNumberValueIndex] == 0)
                        {
                            blankCount++;
                        }
                    }
                    if (blankCount >= 1)
                    {
                        //Complete region 
                        for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
                        {
                            if (loadedPuzzle.puzzlecells[cellNumber].blocknumber == blockNumberTemp)
                            {
                                cellNumbersEdited.Add(cellNumber);
                                loadedPuzzle.puzzlecells[cellNumber].value = loadedPuzzle.puzzlecells[cellNumber].solutionvalue;
                            }
                        }
                        break;
                    }
                }
            }

            //Updating puzzle in line with loaded puzzle. 
            for (int cellNumberCount = 0; cellNumberCount <= loadedPuzzle.puzzlecells.Count - 1; cellNumberCount++)
            {
                foreach (var textBoxCurrent in listOfTextBoxes)
                {
                    if (textBoxCurrent.Name == cellNumberCount.ToString())
                    {
                        for (int cellValueEnterIndex = 0; cellValueEnterIndex <= cellNumbersEdited.Count - 1; cellValueEnterIndex++)
                        {
                            if (int.Parse(textBoxCurrent.Name) == cellNumbersEdited[cellValueEnterIndex])
                            {
                                textBoxCurrent.ForeColor = Color.Green;
                                textBoxCurrent.Text = loadedPuzzle.puzzlecells[cellNumbersEdited[cellValueEnterIndex]].value.ToString();
                                textBoxCurrent.Enabled = false;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void RevealValueFromHint()
        {
            int cellNumberChange = 0;
            //Getting a blank cell and revealing a number
            for (int cellNumber = 0; cellNumber <= loadedPuzzle.puzzlecells.Count - 1; cellNumber++)
            {
                if (loadedPuzzle.puzzlecells[cellNumber].value == 0)
                {
                    cellNumberChange = cellNumber;
                    loadedPuzzle.puzzlecells[cellNumber].value = loadedPuzzle.puzzlecells[cellNumber].solutionvalue;
                    break;
                }
            }

            //Updating puzzle in line with loaded puzzle. 
            for (int cellNumberCount = 0; cellNumberCount <= loadedPuzzle.puzzlecells.Count - 1; cellNumberCount++)
            {
                foreach (var textBoxCurrent in listOfTextBoxes)
                {

                    if (textBoxCurrent.Name == cellNumberCount.ToString())
                    {
                        if (cellNumberCount == cellNumberChange)
                        {
                            textBoxCurrent.ForeColor = Color.Green;
                            textBoxCurrent.Text = loadedPuzzle.puzzlecells[cellNumberCount].value.ToString();
                            textBoxCurrent.Enabled = false;
                        }
                        break;
                    }
                }
            }
        }

        #endregion

        #region Event Handler Puzzle Text boxes. 

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

        /// <summary>
        /// Method to handle the key presses witin the sudoku cells, to ensure only number 1-9 are entrered. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckCellEntry(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //Cell entry validation, if the entered character is between 1 - 9, then enter it into the text box. 
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
            //Allowing backspace
            // http://stackoverflow.com/questions/1191698/how-can-i-accept-the-backspace-key-in-the-keypress-event
            else if (e.KeyChar == (char)8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Timer
        /// <summary>
        /// Method that handles the timer on the screen. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void puzzleTimer_Tick(object sender, EventArgs e)
        {
            //http://stackoverflow.com/questions/463642/what-is-the-best-way-to-convert-seconds-into-hourminutessecondsmilliseconds
            currentTime++;
            //Every mintute decrese the score 
            if (currentTime % 60 == 0)
            {
                currentScore -= 1;
            }
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.Text = time.ToString(@"hh\:mm\:ss");
            SetInformationText();
        }

        #endregion

        #region Scores Methods 

        /// <summary>
        /// Method to create start score depending on the puzzle being selected. 
        /// </summary>
        private void SetStartingScore()
        {
            currentScore = 0;
            if (loadedPuzzle.gridsize == 9)
            {
                switch (loadedPuzzle.difficulty.ToLower())
                {
                    case "easy":
                        currentScore = 20;
                        break;
                    case "medium":
                        currentScore = 30;
                        break;
                    case "hard":
                        currentScore = 40;
                        break;
                    case "extreme":
                        currentScore = 50;
                        break;
                    default:
                        currentScore = 20;
                        break;
                }
            }
            else
            {
                currentScore = 10;
            }
        }

        #endregion

        #region Puzzle Handling Methods

        protected void LoadPuzzleFile()
        {
            //Creating the sudoku grid values. 
            loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirctoryLocation);
            //Generate correct sized grid to be displayed. 
            if (loadedPuzzle.gridsize == 9)
            {
                GenerateStandardSudokuPuzzle(false);
            }
            else
            {
                GenerateSmallSudokuPuzzle(true);
            }
        }

        protected void LoadPuzzleSelection()
        {
            //Determinging which sudoku puzzle is generated based upon the users selection. 
            if (_puzzleSelection == 1)
            {
                loadedPuzzle.gridsize = 9;
                loadedPuzzle.type = "normal";
                GenerateBlankGridStandardSudoku();
                GeneratePuzzle();
                GenerateStandardSudokuPuzzle(true);
            }
            else if (_puzzleSelection == 0)
            {
                loadedPuzzle.type = "irregular";
                loadedPuzzle.gridsize = 9;
                //Seleting which irregular template to use. 
                //Random newRandomNumber = new Random();
                /*
                int irregularRandom = newRandomNumber.Next(0, 1);
                if (irregularRandom == 1)
                {
                    GenerateFirstTemplateIrregular();
                }
                else
                {
                    GenerateSecondTemplateIrregular();
                }
               
                GeneratePuzzle();
                */
                //Loading puzzle form file as cannot generate them
                Random randomIrregular = new Random();
                int randomIrregularValue = randomIrregular.Next(1, 4);
                string fileDirctoryLocationIrregular = Path.GetFullPath(@"..\..\") + @"Puzzles\TestPuzzles\IrregularPuzzles";
                fileDirctoryLocationIrregular += @"\irregulartest" + randomIrregularValue + @".xml";
                //Creating puzzle 
                loadedPuzzle = puzzleManager.ReadFromXMlFile(fileDirctoryLocationIrregular);
                GenerateStandardSudokuPuzzle(false);
            }
            else
            {
                //Creating small puzzle 
                loadedPuzzle.gridsize = 4;
                loadedPuzzle.type = "small";
                loadedPuzzle.difficulty = "Easy";
                GenerateBlankGridStandardSudoku();
                GeneratePuzzle();
                GenerateSmallSudokuPuzzle(false);
            }
        }

        /// <summary>
        /// Generate puzzle using generator class. 
        /// </summary>
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
            //Updating puzzle form textboxes
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

        /// <summary>
        /// Checking the puzzle the user has submitted. 
        /// </summary>
        /// <returns></returns>
        protected bool CheckSubmittedPuzzleXML()
        {
            //Update generated puzzle 
            UpdatePuzzle();
            for (int indexValue = 0; indexValue <= loadedPuzzle.puzzlecells.Count - 1; indexValue++)
            {
                //If valid submit will return true. 
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
            //Getting all of the error cells. 
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

        /// <summary>
        /// Method to set the colour of the error cells. 
        /// </summary>
        /// <param name="errorCellNumbers"></param>
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
                    else
                    {
                        GetSmallPuzzleColour(cellNumber);
                    }
                }
                foreach (var errorCell in errorCellNumbers)
                {
                    if (cellNumber == errorCell && loadedPuzzle.puzzlecells[cellNumber].value != 0)
                    {
                        //Updating scores. 
                        errorSubmitCount += 1;
                        if (currentScore > 0)
                        {
                            currentScore -= 1;
                        }
                        listOfTextBoxes[cellNumber].BackColor = Color.Red;
                    }
                }
            }
        }
        #endregion

        #region Solve Puzzle 

        protected bool ValidateSolvePuzzleEntry()
        {
            UpdatePuzzle();
            bool validatePuzzle = false;

            //Validating puzzle 
            bool validRow = false;
            bool validColumn = false;
            bool validBlock = false;
            validRow = ValidateRow();
            validColumn = ValidateColumn();
            validBlock = ValidateBlock();
            //If puzzle is correct
            if (validRow == true && validColumn == true && validBlock == true)
            {
                validatePuzzle = true;
            }
            return validatePuzzle;
        }
        protected void SolvePuzzle()
        {
            bool validatePuzzle = false;
            //Validating puzzle 
            bool validRow = false;
            bool validColumn = false;
            bool validBlock = false;
            validRow = ValidateRow();
            validColumn = ValidateColumn();
            validBlock = ValidateBlock();
            //If puzzle is correct
            if (validRow == true && validColumn == true && validBlock == true)
            {
                validatePuzzle = true;
            }

            if (validatePuzzle)
            {
                sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;
                Stopwatch tempStopWatch = new Stopwatch();
                tempStopWatch.Reset();
                tempStopWatch.Start();
                bool puzzleSolved = false;
                puzzleSolved = sudokuSolver.BacktrackingUsingXmlTemplateFile(false);
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
            else
            {
                MessageBox.Show("Puzzle incorrectly entered. Please check and try again!");
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
        #endregion

        #region Validate Puzzles Method 
        //Methods to validate whether a row, column or block is correct, with the sudoku constraints.
        protected bool ValidateRow()
        {
            List<int> numbersInRow = new List<int>();
            bool validRows = true;
            for (int rowNumberValidate = 0; rowNumberValidate <= loadedPuzzle.gridsize - 1; rowNumberValidate++)
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
            for (int columnNumberValidate = 0; columnNumberValidate <= loadedPuzzle.gridsize - 1; columnNumberValidate++)
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
            for (int blockNumberValidate = 0; blockNumberValidate <= loadedPuzzle.gridsize - 1; blockNumberValidate++)
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
            //Going through list and seeing if numbers repeat. 
            foreach (var number in listOfNumbers)
            {
                int numberCount = 0;
                numbersUsed.Add(number);
                foreach (var usedNumber in numbersUsed)
                {

                    if (usedNumber == number && number != 0)
                    {
                        numberCount++;

                    }
                    if (numberCount >= 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #endregion

        #region Get Cell Colour Methods 

        //Methods to set the colours of the grids for the game. 
        private void GetStandardPuzzleColour(int textBoxNumber)
        {
            switch (loadedPuzzle.puzzlecells[textBoxNumber].blocknumber)
            {
                case (0):                 
                    listOfTextBoxes[textBoxNumber].BackColor = Color.PaleTurquoise;
                    break;
                case (8):                
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightBlue;
                    break;
                case (1):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightPink;
                    break;
                case (7):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.Pink;
                    break;
                case (2):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightGreen;                
                    break;
                case (6):                  
                    listOfTextBoxes[textBoxNumber].BackColor = Color.PaleGreen;
                    break;
                case (3):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.PaleGoldenrod;
                    break;
                case (5):
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightYellow;
                    break;
                default:
                    listOfTextBoxes[textBoxNumber].BackColor = Color.LightCyan;
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
        protected void GenerateStandardSudokuPuzzle(bool activePuzzle)
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
                    columnLocation = columnLocation + 65;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                else if (indexNumber == 8 || indexNumber % 9 == 8)
                {
                    rowLocation += 38;
                    txtBox.Location = new Point(rowLocation, columnLocation);
                    rowLocation = 45;
                    columnLocation += 17;
                }
                else
                {
                    rowLocation += 38;
                    txtBox.Location = new Point(rowLocation, columnLocation);
                }
            }
            if (activePuzzle)
            {
                SudokuSolver sudokuSolver = new SudokuSolver();
                sudokuSolver.currentPuzzleToBeSolved = loadedPuzzle;
                sudokuSolver.EvaluatePuzzleDifficulty();
                loadedPuzzle.difficulty = sudokuSolver.difficluty;
            }
        }

        /// <summary>
        /// Method to generate random small sudoku puzzle. 
        /// </summary>
        protected void GenerateSmallSudokuPuzzle(bool blank)
        {
            if (!blank)
            {
                sudokuPuzzleGenerator.generatedPuzzle = loadedPuzzle;
                sudokuPuzzleGenerator.CreateSudokuGridXML();
            }
            int rowLocation = 0, columnLocation = 0;
            for (int indexNumber = 0; indexNumber <= loadedPuzzle.puzzlecells.Count - 1; indexNumber++)
            {
                //Creating a textbox for the each cell, with the valid details. 
                TextBox txtBox = new TextBox();
                listOfTextBoxes.Add(txtBox);
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
                    rowLocation = rowLocation + 163;
                    columnLocation = columnLocation + 65;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
                else if (indexNumber == 3 || indexNumber % 4 == 3)
                {
                    rowLocation += 46;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                    rowLocation = 117;
                    columnLocation += 38;
                }
                else
                {
                    rowLocation += 46;
                    txtBox.Location = new System.Drawing.Point(rowLocation, columnLocation);
                }
            }
        }
        #endregion

        #region IrregularPuzzles

        /// <summary>
        /// Method that creates the first irregular puzzle template. 
        /// </summary>
        protected void GenerateFirstTemplateIrregular()
        {
            //Creting the irregular puzzle. 
            for (int puzzleRowNumber = 0; puzzleRowNumber <= loadedPuzzle.gridsize - 1; puzzleRowNumber++)
            {
                for (int puzzleColumnNumber = 0; puzzleColumnNumber <= loadedPuzzle.gridsize - 1; puzzleColumnNumber++)
                {
                    puzzleCell tempPuzzleCell = new puzzleCell();
                    tempPuzzleCell.rownumber = puzzleRowNumber;
                    tempPuzzleCell.columnnumber = puzzleColumnNumber;

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

        /// <summary>
        /// Method that creates the second irregular puzzle template
        /// </summary>
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

        #region Statistics 
        /// <summary>
        /// Loadintg the stats to the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearScreen();
            //Load statistics textbox
            staticsDispalyTb.Visible = true;
            this.Text = "Statistics : Siwel Sudoku";
            LoadStaticsAndDisplay();
        }

        /// <summary>
        /// Method to load and display all users stats. 
        /// </summary>
        private void LoadStaticsAndDisplay()
        {
            //Dispalying all of the stats 
            StatisticsManager.ReadFromStatisticsFile();
            staticsDispalyTb.Text += "Hints:" + StatisticsManager.currentStats.hintNumber + "\r\n";
            //Converting number of seconds
            TimeSpan span = TimeSpan.FromSeconds((double)StatisticsManager.currentStats.fastestsolvetime);
            string formattedTime = span.ToString(@"hh\:mm\:ss");
            staticsDispalyTb.Text += "Fastest solving time:" + formattedTime + " (hh\\:mm\\:ss)\r\n";
            staticsDispalyTb.Text += "Levles completed:" + StatisticsManager.currentStats.levelcompleted + "\r\n";
            staticsDispalyTb.Text += "Number of puzzles completed:" + StatisticsManager.currentStats.puzzlecompleted + "\r\n";
            staticsDispalyTb.Text += "Extreme Puzzle Completed:" + StatisticsManager.currentStats.numberOfExtremePuzzleCompleted + "\r\n";
            staticsDispalyTb.Text += "Extreme Puzzle High Score:" + StatisticsManager.currentStats.extremeHighScore + "\r\n";
            staticsDispalyTb.Text += "Hard Puzzle High Score:" + StatisticsManager.currentStats.hardHighScore + "\r\n";
            staticsDispalyTb.Text += "Medium Puzzle High Score:" + StatisticsManager.currentStats.mediumHighScore + "\r\n";
            staticsDispalyTb.Text += "Easy Puzzle High Score:" + StatisticsManager.currentStats.easyHighScore + "\r\n";
            staticsDispalyTb.Text += "Number Of Regular Puzzles completed:" + StatisticsManager.currentStats.numberofRegularCompleted + "\r\n";
            staticsDispalyTb.Text += "Number Of Irregular Puzzles completed:" + StatisticsManager.currentStats.numberofIrregularCompleted + "\r\n";
            staticsDispalyTb.Text += "Number Of Small (4x4) Puzzles completed:" + StatisticsManager.currentStats.numberofSmallGridCompleted + "\r\n";
        }

        #endregion
    }
}
