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

        private puzzle generatedPuzzle;
        puzzle solvingPuzzle;

        private void generatePuzzleBtn_Click(object sender, EventArgs e)
        {
            string directoryLocation = "";
            Stopwatch puzzleGenerationTime = new Stopwatch();
            puzzleGenerationTime.Reset();
            puzzleGenerationTime.Start();
            for (int puzzleNumber = 0; puzzleNumber <= 100; puzzleNumber++)
            {
                generatedPuzzle = new puzzle();
                solvingPuzzle = new puzzle();
                generatedPuzzle.gridsize = 9;
                solvingPuzzle.gridsize = 9;
                SudokuPuzzleGenerator puzzleGenerator = new SudokuPuzzleGenerator(generatedPuzzle.gridsize);
                GenerateBlankGridStandardSudoku();
                puzzleGenerator.generatedPuzzle = generatedPuzzle;
                puzzleGenerator.CreateSudokuGridXML();

                List<int> emptyCellList = new List<int>();

                for (int valueInCellNumber =0;valueInCellNumber<=generatedPuzzle.puzzlecells.Count-1;valueInCellNumber++)
                {
                    if(generatedPuzzle.puzzlecells[valueInCellNumber].value ==0)
                    {
                        emptyCellList.Add(valueInCellNumber);
                    }
                }


                for (int puzzleCellNumber = 0; puzzleCellNumber <= generatedPuzzle.puzzlecells.Count - 1; puzzleCellNumber++)
                {
                    solvingPuzzle.puzzlecells[puzzleCellNumber].value = generatedPuzzle.puzzlecells[puzzleCellNumber].value;
                }
                //THis fills in the full puzzle. therefore need to create a new puzzle that does not change.       
                SudokuSolver solver = new SudokuSolver();
                solver.currentPuzzleToBeSolved = solvingPuzzle;
                solver.EvaluatePuzzleDifficulty();
                generatedPuzzle.difficulty = solver.difficluty;
                generatedPuzzle.type = "normal";

                for(int puzzleCellNumber =0;puzzleCellNumber<=generatedPuzzle.puzzlecells.Count-1;puzzleCellNumber++)
                {
                    foreach(var number in emptyCellList)
                    {
                        if(puzzleCellNumber == number)
                        {
                            generatedPuzzle.puzzlecells[puzzleCellNumber].value = 0;
                        }
                    }
                }

                directoryLocation = Path.GetFullPath(@"..\..\") + @"\Puzzles\GeneratedPuzzles";
                string subFolderLocation = "";

                if (generatedPuzzle.difficulty == "Easy")
                {
                    subFolderLocation = @"\EasyPuzzles";
                }
                else if(generatedPuzzle.difficulty == "Medium")
                {
                    subFolderLocation = @"\MediumPuzzles";
                }
                else if (generatedPuzzle.difficulty == "Hard")
                {
                    subFolderLocation = @"\HardPuzzles";
                }
                else
                {
                    subFolderLocation = @"\ExtremePuzzles";
                }

                directoryLocation += subFolderLocation;
                //http://stackoverflow.com/questions/2242564/file-count-from-a-folder
                // searches the current directory
                int fCount = Directory.GetFiles(directoryLocation, "*", SearchOption.TopDirectoryOnly).Length;
                fCount += 1;
                directoryLocation += @"\" + subFolderLocation + fCount + ".xml";


                PuzzleManager.WriteToXmlFile(generatedPuzzle, directoryLocation);
            }
            
            puzzleGenerationTime.Stop();
        }

        #region Create and Clear Blank Puzzle 

        private void GenerateBlankGridStandardSudoku()
        {
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
                    solvingPuzzle.puzzlecells.Add(tempPuzzleCell);
                }
            }
        }

        #endregion

        #region Get Blocks Methods 
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
    }
}
