using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    public class SudokuPuzzleGenerator
    {
        #region Global Vairbales 
        //Grid size, whether solved and orginal solution. 
        int _gridSize = 0;
        bool solved = false;
        //Cells handling
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

        #region Main Methods 

        /// <summary>
        /// Creating a new sudoku puzzle. 
        /// </summary>
        /// <returns></returns>
        public puzzle CreateSudokuGridXML()
        {         
            //Clearing generated cell and creating a new one one, and returning this. Resetting values.
            solved = false; 
            listOfCellsRemovedValues.Clear();
            listOfCellsNotRemoved.Clear();
            GenerateExampleSudokuGridXML();
            return generatedPuzzle;
        }

        /// <summary>
        /// Generate the puzzle. 
        /// </summary>
        private void GenerateExampleSudokuGridXML()
        {   
            //Getting all blank cells at the start.      
            for(int getBlankCellCount=0;getBlankCellCount<=generatedPuzzle.puzzlecells.Count-1;getBlankCellCount++)
            {
                listOfCellsNotRemoved.Add(getBlankCellCount);
            }

            //Solving blank grid. 
            solver.currentPuzzleToBeSolved = generatedPuzzle;

            solved = solver.BacktrackingUsingXmlTemplateFile(true);

            //Setting the orginal solution of the puzzle. 
            for (int orginalSolutionCounter = 0; orginalSolutionCounter <= generatedPuzzle.puzzlecells.Count - 1; orginalSolutionCounter++)
            {
                generatedPuzzle.puzzlecells[orginalSolutionCounter].solutionvalue = generatedPuzzle.puzzlecells[orginalSolutionCounter].value;
            }
            //Digging holes within the solved puzzle. 
            DigHolesXML();

            //Need to added the grid from the intitial solution and then remove values. 
            for(int cellIndexValue =0; cellIndexValue<=generatedPuzzle.puzzlecells.Count-1;cellIndexValue++)
            {
                generatedPuzzle.puzzlecells[cellIndexValue].value = generatedPuzzle.puzzlecells[cellIndexValue].solutionvalue;
            }
            RemoveValuesFromPuzzle();  //remvoing values to make puzzle valid.   
        }

        /// <summary>
        /// Method to dig holes within the puzzle. 
        /// </summary>
        private void DigHolesXML()
        {
            //Creating variables 
            bool isEqualToOrginal = false;
            int cellValue = 0;
            int blankCellNumber = 0;
            //Depending on grid size, remove a certain amount of numbers from the puzzle initially. 
            if(generatedPuzzle.gridsize ==9)
            {
                //If you increase this number the puzzle generated will become harder. 
                blankCellNumber = 48;
            }
            else if(generatedPuzzle.gridsize ==16)
            {
                blankCellNumber = 30; 
            }
            else
            {
                blankCellNumber = 6;
            }
            //Initially set number of candidates from puzzle. 
            for (int initialHolesRemoved = 0; initialHolesRemoved <= blankCellNumber; initialHolesRemoved++)
            {
                while (generatedPuzzle.puzzlecells[cellValue].value == 0)
                {
                    cellValue = randomNumber.Next(0, generatedPuzzle.puzzlecells.Count - 1);
                }
                //Updating puzzle and cell handlers. 
                listOfCellsRemovedValues.Add(cellValue); //Adds the cells that have been blanked previously. 
                RemoveValuesFromListOfNonRemovedCells(cellValue);
                generatedPuzzle.puzzlecells[cellValue].value = 0;
            }

            //Checking to see if puzzle is solvable. 
            solver.currentPuzzleToBeSolved = generatedPuzzle;
            solved = solver.BacktrackingUsingXmlTemplateFile(false);
            if (solved == true)
            {
                //Check to see if solution is eqaual to the orignal solution, to ensure only one solution possible. 
                isEqualToOrginal = CheckValidSolutionXML();
                if (isEqualToOrginal == true)
                {   //Finding the cell to induce further backtracking 
                    for (int reverseCellCount = generatedPuzzle.puzzlecells.Count-1; reverseCellCount >= 0; reverseCellCount--)
                    {   //If too many values have been removed. 
                        if(listOfCellsNotRemoved.Count >=64)
                        {
                            CreateSudokuGridXML();
                        }
                        //Get a vlaid cell, that a value has been removed from, the last number that has had a value removed, 
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
                        //Setting prervious number, so can not be used. 
                        int previousNumberReverse = generatedPuzzle.puzzlecells[reverseCellCount].value;
                        generatedPuzzle.puzzlecells[reverseCellCount].value = 0;
                        //Getting valid numebrs for that cell. 
                        List<int> validNumbersInRow = CheckValidNumbersForRegions.GetValuesForRowXmlPuzzleTemplate(generatedPuzzle, generatedPuzzle.puzzlecells[reverseCellCount]);
                        List<int> validNumbersInColumn = CheckValidNumbersForRegions.GetValuesForColumnXmlPuzzleTemplate(generatedPuzzle, generatedPuzzle.puzzlecells[reverseCellCount]);
                        List<int> validNumbersInBlock = CheckValidNumbersForRegions.GetValuesForBlockXmlPuzzleTemplate(generatedPuzzle, generatedPuzzle.puzzlecells[reverseCellCount]);
                        List<int> validNumbers = CheckValidNumbersForRegions.GetValidNumbers(validNumbersInColumn, validNumbersInRow, validNumbersInBlock);
                        //If the cell contains more than 1 candidate then the backtracking can occur. 
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
                else //Create new puzzle. 
                {
                    foreach(var clearCell in generatedPuzzle.puzzlecells)
                    {
                        clearCell.value = 0;
                    }
                    CreateSudokuGridXML();
                }
            }
            else //Create new puzzle 
            {
                foreach (var clearCell in generatedPuzzle.puzzlecells)
                {
                    clearCell.value = 0;
                }
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
                if (generatedPuzzle.puzzlecells[checkPuzzleCounter].value != generatedPuzzle.puzzlecells[checkPuzzleCounter].solutionvalue)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Remove Values Methods 
        //Methods updating lists for the puzzle. 
        /// <summary>
        /// Method that updates cell hadnling list, to say the list of non removed values from cells. 
        /// </summary>
        /// <param name="cellValue"></param>
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

        #region Get Blocks Methods 
        //Method to get the correct block number based on the row and column number of the cell currently being handled. 
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

        #endregion
    }
}
