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
    public partial class DevelopmentForm : Form
    {
        #region Variables 
        puzzle generatedPuzzle;
        #endregion 

        #region Constructor
        public DevelopmentForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void addSolutionsBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            SolutionsAddForm addForm = new SolutionsAddForm();
            addForm.Show();
        }

        private void generatePuzzleBtn_Click(object sender, EventArgs e)
        {
            //Directory location and stop watch to time the creation process. 
            string directoryLocation = "";
            Stopwatch puzzleGenerationTime = new Stopwatch();
            puzzleGenerationTime.Reset();
            puzzleGenerationTime.Start();
            //Creating 10 puzzles using the generator 
            for (int puzzleNumber = 0; puzzleNumber <= 99; puzzleNumber++)
            {
                //Creating puzzles. 
                generatedPuzzle = new puzzle();
                generatedPuzzle.gridsize = 9;
                //Creating a new puzzle. 
                SudokuPuzzleGenerator puzzleGenerator = new SudokuPuzzleGenerator(generatedPuzzle.gridsize);
                GenerateBlankGridStandardSudoku();
                puzzleGenerator.generatedPuzzle = generatedPuzzle;
                puzzleGenerator.CreateSudokuGridXML();
                //Getting all of the empty cellsm to be removed later. 
                List<int> emptyCellList = new List<int>();
                for (int valueInCellNumber = 0; valueInCellNumber <= generatedPuzzle.puzzlecells.Count - 1; valueInCellNumber++)
                {
                    if (generatedPuzzle.puzzlecells[valueInCellNumber].value == 0)
                    {
                        emptyCellList.Add(valueInCellNumber);
                    }
                }

                //Getting difficulty of puzzle, and ensuring correctionness.   
                SudokuSolver solver = new SudokuSolver();
                solver.currentPuzzleToBeSolved = generatedPuzzle;
                solver.EvaluatePuzzleDifficulty();
                generatedPuzzle.difficulty = solver.difficluty;
                generatedPuzzle.type = "normal";
                //Removing the values from the puzzle. 
                for (int puzzleCellNumber = 0; puzzleCellNumber <= generatedPuzzle.puzzlecells.Count - 1; puzzleCellNumber++)
                {

                    foreach (var number in emptyCellList)
                    {
                        if (puzzleCellNumber == number)
                        {
                            generatedPuzzle.puzzlecells[puzzleCellNumber].value = 0;
                        }
                    }
                }
                //Setting file path based on the difficulty of the puzzle. 
                directoryLocation = Path.GetFullPath(@"..\..\") + @"\Puzzles\GeneratedPuzzles";
                string subFolderLocation = "";
                if (generatedPuzzle.difficulty == "Easy")
                {
                    subFolderLocation = @"\EasyPuzzles";
                }
                else if (generatedPuzzle.difficulty == "Medium")
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
                //Svaing puzzle. 
                PuzzleManager.WriteToXmlFile(generatedPuzzle, directoryLocation);
            }
            puzzleGenerationTime.Stop();
            MessageBox.Show("10 Puzzles Successfully Created");
        }

        private void convertFileBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConvertTextFileToXMLFile convertScreen = new ConvertTextFileToXMLFile();
            convertScreen.Show();       
        }
        #endregion

        #region Create blank Sudoku grid. 
        /// <summary>
        /// Method generates a blank sudoku grid. 
        /// </summary>
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
                }
            }
        }

        #endregion

        #region Get Blocks Methods 
        //Method that get the block numbers for a cell using the column and row number. 
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
