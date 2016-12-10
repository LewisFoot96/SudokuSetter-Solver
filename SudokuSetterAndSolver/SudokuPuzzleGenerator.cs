using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    class SudokuPuzzleGenerator
    {
        #region Global Vairbales 
        //The number of static numbers should intially be used to determine the difficulty, then the other methods will contribute to the difficulty determination.

        int _gridSize = 0;
        bool solved = false;
        string puzzlePifficulty = "";
        int executionTime;
        int executionTimeDifficulty;
        int totalCandidates;
        int totalNumberCandidatesDifficulty;
        int humanModelDifficultyLevel;
        int staticNumbersTotal;
        int staticNumbersDifficulty;
        public List<int> orginalSolution = new List<int>();
        List<int> listOfCellsRemovedValues = new List<int>();
        List<int> listOfCellsNotRemoved = new List<int>();
        #endregion

        #region Objects 
        PuzzleManager puzzleManager = new PuzzleManager();
        SudokuSolver solver = new SudokuSolver();
        Random randomNumber = new Random();

        public puzzle generatedPuzzle = new puzzle();
        puzzleCell puzzleCellBeingHandled = new puzzleCell();
        #endregion

        #region Constructor 
        public SudokuPuzzleGenerator(int gridSize)
        {
            _gridSize = gridSize;

        }
        #endregion

        #region Main Methods 

        public puzzle CreateSudokuGridXML()
        {         
            //Clearing generated cell and creating a new one one, and returning this. 
            solved = false; 
            listOfCellsRemovedValues.Clear();
            listOfCellsNotRemoved.Clear();
            orginalSolution.Clear();
            GenerateExampleSudokuGridXML();
            return generatedPuzzle;
        }

        private void GenerateExampleSudokuGridXML()
        {
            ////Creating blank cells. 
            //for (int getBlankCount = 0; getBlankCount <= 80; getBlankCount++)
            //{
            //    listOfCellsNotRemoved.Add(getBlankCount);
            //    generatedPuzzle.puzzlecells.Add(new puzzleCell());
            //}
            //Getting the row, column and block numbers for the cells. 

            for(int getBlankCellCount=0;getBlankCellCount<=generatedPuzzle.puzzlecells.Count-1;getBlankCellCount++)
            {
                listOfCellsNotRemoved.Add(getBlankCellCount);
            }

            //for (int cellNumberPopulating = 0; cellNumberPopulating <= generatedPuzzle.puzzlecells.Count - 1; cellNumberPopulating++)
            //{
            //    generatedPuzzle.puzzlecells[cellNumberPopulating].rownumber = cellNumberPopulating / generatedPuzzle.gridsize;
            //    generatedPuzzle.puzzlecells[cellNumberPopulating].columnnumber = cellNumberPopulating % generatedPuzzle.gridsize;

            //    if(generatedPuzzle.gridsize ==9)
            //    {
            //        generatedPuzzle.puzzlecells[cellNumberPopulating].blocknumber = GetBlockNumberNine(generatedPuzzle.puzzlecells[cellNumberPopulating].rownumber, generatedPuzzle.puzzlecells[cellNumberPopulating].columnnumber);
            //    }
            //    else if (generatedPuzzle.gridsize==16)
            //    {
            //        generatedPuzzle.puzzlecells[cellNumberPopulating].blocknumber = GetBlocNumberSixteen(generatedPuzzle.puzzlecells[cellNumberPopulating].rownumber, generatedPuzzle.puzzlecells[cellNumberPopulating].columnnumber);
            //    }
            //    else
            //    {
            //        generatedPuzzle.puzzlecells[cellNumberPopulating].blocknumber = GetBlockFour(generatedPuzzle.puzzlecells[cellNumberPopulating].rownumber, generatedPuzzle.puzzlecells[cellNumberPopulating].columnnumber);
            //    }
            //}

            //Solving blank grid. 
            solver.currentPuzzleToBeSolved = generatedPuzzle;
            solved = solver.BacktrackingUsingXmlTemplateFile(true);

            for (int orginalSolutionCounter = 0; orginalSolutionCounter <= generatedPuzzle.puzzlecells.Count - 1; orginalSolutionCounter++)
            {
                orginalSolution.Add(generatedPuzzle.puzzlecells[orginalSolutionCounter].value);
            }

            DigHolesXML();

            //Need to added the grid from the intitial solution and then remove values. 
            for(int cellIndexValue =0; cellIndexValue<=generatedPuzzle.puzzlecells.Count-1;cellIndexValue++)
            {
                generatedPuzzle.puzzlecells[cellIndexValue].value = orginalSolution[cellIndexValue];
            }
            RemoveValuesFromPuzzle();    
        }

        private void DigHolesXML()
        {
            bool isEqualToOrginal = false;
            int cellValue = 0;
            int blankCellNumber = 0;
            if(generatedPuzzle.gridsize ==9)
            {
                blankCellNumber = 35;
            }
            else if(generatedPuzzle.gridsize ==16)
            {
                blankCellNumber = 35; 
            }
            else
            {
                blankCellNumber = 6;
            }
            //Initially remove 10 candidates from the cells. 
            for (int initialHolesRemoved = 0; initialHolesRemoved <= blankCellNumber; initialHolesRemoved++)
            {
                while (generatedPuzzle.puzzlecells[cellValue].value == 0)
                {
                    cellValue = randomNumber.Next(0, generatedPuzzle.puzzlecells.Count - 1);
                }
                listOfCellsRemovedValues.Add(cellValue); //Adds the cells that have been blanked previously. 

                RemoveValuesFromListOfNonRemovedCells(cellValue);

                generatedPuzzle.puzzlecells[cellValue].value = 0;
            }

            solver.currentPuzzleToBeSolved = generatedPuzzle;
            solved = solver.BacktrackingUsingXmlTemplateFile(false);

            if (solved == true)
            {
                isEqualToOrginal = CheckValidSolutionXML();
                ////If the puzzle is not equal to the orginal solution, then remove values, until it does. 
                //while (isEqualToOrginal == false)
                //{
                //    cellValue = randomNumber.Next(0, listOfCellsNotRemoved.Count - 1);

                //    listOfCellsRemovedValues.Add(cellValue ); //Adds the cells that have been blanked previously. 
                //    RemoveValuesFromListOfNonRemovedCells(cellValue);
                //    RemoveValuesFromPuzzle();


                //    solver.currentPuzzleToBeSolved = generatedPuzzle;
                //    solved = solver.BacktrackingUsingXmlTemplateFile(false);

                //    if (solved == false || listOfCellsNotRemoved.Count <=17)
                //    {
                //        generatedPuzzle.puzzlecells.Clear();
                //        CreateSudokuGridXML();

                //    }

                //    //If the puzzle is equal to the initial solution then it may be a valid puzzle. 
                //    isEqualToOrginal = CheckValidSolutionXML();
                //}

                if (isEqualToOrginal == true)
                {
                    for (int reverseCellCount = generatedPuzzle.puzzlecells.Count-1; reverseCellCount >= 0; reverseCellCount--)
                    {
                        //Get a vlaid cell, that a value has been removed from. 
                        bool isRemovedCell = false;
                        for (int removeCellIndex = 0; removeCellIndex <= listOfCellsRemovedValues.Count - 1; removeCellIndex++)
                        {
                            if (reverseCellCount == listOfCellsRemovedValues[removeCellIndex])
                            {
                                isRemovedCell = true;
                            }
                        }
                        if (isRemovedCell == false) //If there has been no value removed from this cell then skip the iteration. 
                        {
                            continue;
                        }

                        int previousNumberReverse = generatedPuzzle.puzzlecells[reverseCellCount].value;
                        generatedPuzzle.puzzlecells[reverseCellCount].value = 0;

                        List<int> validNumbersInRow = CheckValidNumbersForRegions.GetValuesForRowXmlPuzzleTemplate(generatedPuzzle, generatedPuzzle.puzzlecells[reverseCellCount]);
                        List<int> validNumbersInColumn = CheckValidNumbersForRegions.GetValuesForColumnXmlPuzzleTemplate(generatedPuzzle, generatedPuzzle.puzzlecells[reverseCellCount]);
                        List<int> validNumbersInBlock = CheckValidNumbersForRegions.GetValuesForBlockXmlPuzzleTemplate(generatedPuzzle, generatedPuzzle.puzzlecells[reverseCellCount]);
                        List<int> validNumbers = CheckValidNumbersForRegions.GetValidNumbers(validNumbersInColumn, validNumbersInRow, validNumbersInBlock);

                        if (validNumbers.Count >= 2)
                        {
                            //If there is a cell with 2 or more candidates, where the current value is not the last valid number in that list. 
                            if (previousNumberReverse != validNumbers[validNumbers.Count - 1])
                            {
                                for (int validNumberIndexNumber = 0; validNumberIndexNumber <= validNumbers.Count - 1; validNumberIndexNumber++)
                                {
                                    if (validNumbers[validNumberIndexNumber] > previousNumberReverse)
                                    {
                                        //Testing the new puzzle, which has the changed value. 
                                        generatedPuzzle.puzzlecells[reverseCellCount].value = validNumbers[validNumberIndexNumber];
                                        solver.currentPuzzleToBeSolved = generatedPuzzle;
                                        solved = solver.BacktrackingUsingXmlTemplateFile(false);
                                        //there is another solution. Therefore another value needs to be removed. 
                                        if (solved == true)
                                        {
                                            //delete another number. if there is no solution after a puzzle has been submitted, then recurse on the generating method, 
                                        }
                                        //If there is no other solution, then it is a unique solution. 
                                        else
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //generatedPuzzle.puzzlecells.Clear();
                    foreach(var clearCell in generatedPuzzle.puzzlecells)
                    {
                        clearCell.value = 0;
                    }

                    CreateSudokuGridXML();
                }
            }
            else
            {
                foreach (var clearCell in generatedPuzzle.puzzlecells)
                {
                    clearCell.value = 0;
                }
                // generatedPuzzle.puzzlecells.Clear();
                CreateSudokuGridXML();
            }
        }

        /// <summary>
        /// Method to check to see if the puzzle that has been solved is equal to the orginal solution for the puzzle. 
        /// </summary>
        /// <returns></returns>
        private bool CheckValidSolutionXML()
        {
            for (int checkPuzzleCounter = 0; checkPuzzleCounter <= generatedPuzzle.puzzlecells.Count - 1; checkPuzzleCounter++)
            {
                if (generatedPuzzle.puzzlecells[checkPuzzleCounter].value != orginalSolution[checkPuzzleCounter])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Remove Values Methods 
        private void RemoveValuesFromListOfNonRemovedCells(int cellValue)
        {
            for (int notRemoveCellIndex = 0; notRemoveCellIndex <= listOfCellsNotRemoved.Count - 1; notRemoveCellIndex++)
            {
                if (listOfCellsNotRemoved[notRemoveCellIndex] == cellValue)
                {
                    listOfCellsNotRemoved.RemoveAt(notRemoveCellIndex);
                    return;
                }
            }

        }

        /// <summary>
        /// Method that removes the values from the puzzles that have been dug out. 
        /// </summary>
        private void RemoveValuesFromPuzzle()
        {
            for (int cellNumberTemp = 0; cellNumberTemp <= generatedPuzzle.puzzlecells.Count - 1; cellNumberTemp++)
            {
                foreach (var removeValueCell in listOfCellsRemovedValues)
                {
                    //Removing value from cell. 
                    if (cellNumberTemp == removeValueCell)
                    {
                        generatedPuzzle.puzzlecells[cellNumberTemp].value = 0;
                        break;
                    }
                }
            }
        }

        #endregion

        #region GetValues Methods 
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
            else if (tempRowNumber >= 2 && tempRowNumber <= 1)
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



        private int GetBlockNumber(int tempRowNumber, int tempColumnNumber)
        {
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

        #region Evaluate Difficulty

        private string EvaluatePuzzleDifficulty()
        {
            //Execution times
            //total number of candidates
            //Number os static numbers 
            //Human model
            return "";
        }

        /// <summary>
        /// Method that evaluates the execution time of the puzzle that has been generated, this will determine the difficulty of the puzzle. 
        /// </summary>
        /// <returns></returns>
        private int EvaluateExecutionTime()
        {
            if (executionTime == 1)
            {

            }
            return 1;
        }
        #endregion 
    }
}
