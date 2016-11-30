﻿using System;
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

        //Grids that will contain the created grid, the first will be used to check to ensure the grid is valid 
        int[,] sudokuGrid = new int[9, 9];
        int[,] finalGenenratedPuzzle = new int[9, 9];
        public int[,] orginalSudokuGrid = new int[9, 9];
        int _gridSize = 0;
        int rowNumber = 0;
        int columnNumber = 0;
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
        #endregion

        #region Objects 
        PuzzleManager puzzleManager = new PuzzleManager();
        SudokuSolver solver = new SudokuSolver();
        Random randomNumber = new Random();


        public puzzle generatedPuzzle = new puzzle();
        puzzle orginalSolutionToGeneratedPuzzle;
        puzzle tempGeneratedPuzzle;
        puzzleCell puzzleCellBeingHandled = new puzzleCell();
        #endregion

        #region Constructor 
        public SudokuPuzzleGenerator(int gridSize)
        {
            _gridSize = gridSize;

        }
        #endregion

        #region Methods 

        public puzzle CreateSudokuGridXML()
        {
            listOfCellsRemovedValues.Clear();
            orginalSolution.Clear();
            orginalSolutionToGeneratedPuzzle = new puzzle();
            tempGeneratedPuzzle = new puzzle();
            GenerateExampleSudokuGridXML();

            return generatedPuzzle;
        }

        //This method is called when the grid is created, on the screen start up. 
        public int[,] CreateSudokuGrid()
        {
            ClearSudokuGrid(finalGenenratedPuzzle);
            ClearSudokuGrid(sudokuGrid);
            ClearSudokuGrid(orginalSudokuGrid);
            GenerateExampleSudokuGrid();
            ConvertFInalGridIntoSudokuGrid();
            //This is where i need to get the difficluty of the puzzle. 

            solver.sudokuPuzzleMultiExample = sudokuGrid;
            solver.SolveSudokRuleBased();

            puzzlePifficulty = solver.difficluty;
            return finalGenenratedPuzzle;
        }

        //Create 2 methods, one which diggies holes from a grid and the other one inserts radnom givens in the puzzle. 

        private void GenerateExampleSudokuGridXML()
        {
            //Creating blank cells. 
            for (int getBlankCount = 0; getBlankCount <= 80; getBlankCount++)
            {
                generatedPuzzle.puzzlecells.Add(new puzzleCell());
            }
            //Getting the row, column and block numbers for the cells. 
            for (int cellNumberPopulating = 0; cellNumberPopulating <= generatedPuzzle.puzzlecells.Count - 1; cellNumberPopulating++)
            {

                generatedPuzzle.puzzlecells[cellNumberPopulating].rownumber = cellNumberPopulating / 9;
                generatedPuzzle.puzzlecells[cellNumberPopulating].columnnumber = cellNumberPopulating % 9;
                generatedPuzzle.puzzlecells[cellNumberPopulating].blocknumber = GetBlockNumber(generatedPuzzle.puzzlecells[cellNumberPopulating].rownumber, generatedPuzzle.puzzlecells[cellNumberPopulating].columnnumber);
            }

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

        /// <summary>
        /// This method creates a completed grid. 
        /// </summary>
        private void GenerateExampleSudokuGrid()
        {
            //Solving a blank grid. 
            solver.sudokuPuzzleMultiExample = sudokuGrid;
            solved = solver.BacktrackinEffcient(true);
            //Setting up the orginal solution for the puzzle. 
            for (int originalRowNumber = 0; originalRowNumber <= 8; originalRowNumber++)
            {
                for (int originalColumnNumber = 0; originalColumnNumber <= 8; originalColumnNumber++)
                {
                    orginalSudokuGrid[originalRowNumber, originalColumnNumber] = sudokuGrid[originalRowNumber, originalColumnNumber];
                }
            }
            //Removing values from cells until there is a valid solution. 
            DigHoles();
        }

        private void DigHolesXML()
        {
            bool isEqualToOrginal = false;
            int cellValue = 0;
            //Initially remove 10 candidates from the cells. 
            for (int initialHolesRemoved = 0; initialHolesRemoved <= 35; initialHolesRemoved++)
            {
                while (generatedPuzzle.puzzlecells[cellValue].value == 0)
                {
                    cellValue = randomNumber.Next(0, generatedPuzzle.puzzlecells.Count - 1);
                }
                listOfCellsRemovedValues.Add(cellValue); //Adds the cells that have been blanked previously. 
                generatedPuzzle.puzzlecells[cellValue].value = 0;
            }

            solver.currentPuzzleToBeSolved = generatedPuzzle;
            solved = solver.BacktrackingUsingXmlTemplateFile(false);

            if (solved == true)
            {
                isEqualToOrginal = CheckValidSolutionXML();
                //If the puzzle is not equal to the orginal solution, then remove values, until it does. 
                while (isEqualToOrginal == false)
                {
                    while (generatedPuzzle.puzzlecells[cellValue].value == 0)
                    {
                        cellValue = randomNumber.Next(0, generatedPuzzle.puzzlecells.Count - 1);
                    }
                    listOfCellsRemovedValues.Add(cellValue); //Adds the cells that have been blanked previously. 
                    RemoveValuesFromPuzzle();


                    solver.currentPuzzleToBeSolved = generatedPuzzle;
                    solved = solver.BacktrackinEffcient(false);

                    if (solved == false)
                    {
                        generatedPuzzle = new puzzle();
                        CreateSudokuGridXML();

                    }

                    //If the puzzle is equal to the initial solution then it may be a valid puzzle. 
                    isEqualToOrginal = CheckValidSolutionXML();
                }

                if (isEqualToOrginal == true)
                {
                    for (int reverseCellCount = 80; reverseCellCount >= 0; reverseCellCount--)
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
            }
            else
            {
                generatedPuzzle = new puzzle();
                GenerateExampleSudokuGridXML();
            }
        }

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

        /// <summary>
        /// Method that removes numbers within the grid until a valid puzzle is generated. 
        /// </summary>
        private void DigHoles()
        {
            bool isEqualToOrginal = false;
            //Initially remove 10 candidates from the cells. 
            for (int initialHolesRemoved = 0; initialHolesRemoved <= 35; initialHolesRemoved++)
            {
                while (sudokuGrid[rowNumber, columnNumber] == 0)
                {
                    rowNumber = randomNumber.Next(0, 9);
                    columnNumber = randomNumber.Next(0, 9);
                }
                sudokuGrid[rowNumber, columnNumber] = 0;
            }
            //Setting the puzzle to the grid with the removed values. 
            SetFinalGeneratedPuzzle();
            ///Solving the grid that the 35 values have been removed from. 
            solver.sudokuPuzzleMultiExample = sudokuGrid;
            solved = solver.BacktrackinEffcient(false);

            if (solved == true)
            {
                //If the puzzle is equal to the initial solution then it may be a valid puzzle. 
                isEqualToOrginal = SeeIfGeneratedPuzzleHasTheSameSolutionAsTheOrginal();

                //If the puzzle is not equal to the orginal solution, then remove values, until it does. 
                while (isEqualToOrginal == false)
                {
                    //Getting a value to remove. 
                    while (finalGenenratedPuzzle[rowNumber, columnNumber] == 0)
                    {
                        rowNumber = randomNumber.Next(0, 9);
                        columnNumber = randomNumber.Next(0, 9);
                    }
                    finalGenenratedPuzzle[rowNumber, columnNumber] = 0;
                    ConvertFInalGridIntoSudokuGrid();

                    solver.sudokuPuzzleMultiExample = sudokuGrid;
                    solved = solver.BacktrackinEffcient(false);

                    if (solved == false)
                    {
                        ClearSudokuGrid(sudokuGrid);
                        CreateSudokuGrid();

                    }

                    //If the puzzle is equal to the initial solution then it may be a valid puzzle. 
                    isEqualToOrginal = SeeIfGeneratedPuzzleHasTheSameSolutionAsTheOrginal();
                }

                if (isEqualToOrginal == true)
                {
                    //Forcing the bactracking to see if there is another solution, by chnaging a value to be backtracked. 
                    for (int reverseRowNumber = 8; reverseRowNumber >= 0; reverseRowNumber--)
                    {
                        for (int reverseColumnNumber = 8; reverseColumnNumber >= 0; reverseColumnNumber--)
                        {
                            //Getting the valid numbers for the cell. 
                            if (finalGenenratedPuzzle[reverseRowNumber, reverseColumnNumber] == 0)
                            {
                                int previousNumber = sudokuGrid[reverseRowNumber, reverseColumnNumber];
                                sudokuGrid[reverseRowNumber, reverseColumnNumber] = 0;
                                List<int> validNumbersInRow = CheckValidNumbersForRegions.checkRow(sudokuGrid, reverseRowNumber, reverseColumnNumber);
                                List<int> validNumbersInColumn = CheckValidNumbersForRegions.checkColumn(sudokuGrid, reverseRowNumber, reverseColumnNumber);
                                List<int> validNumbersInBlock = CheckValidNumbersForRegions.checkBlock(sudokuGrid, reverseRowNumber, reverseColumnNumber);
                                List<int> validNumbers = CheckValidNumbersForRegions.GetValidNumbers(validNumbersInColumn, validNumbersInRow, validNumbersInBlock);
                                //If there is a cell with 2 or more candidates. 
                                if (validNumbers.Count >= 2)
                                {
                                    //If there is a cell with 2 or more candidates, where the current value is not the last valid number in that list. 
                                    if (previousNumber != validNumbers[validNumbers.Count - 1])
                                    {
                                        for (int validNumberIndexNumber = 0; validNumberIndexNumber <= validNumbers.Count - 1; validNumberIndexNumber++)
                                        {
                                            if (validNumbers[validNumberIndexNumber] > previousNumber)
                                            {
                                                //Testing the new puzzle, which has the changed value. 
                                                sudokuGrid[reverseRowNumber, reverseColumnNumber] = validNumbers[validNumberIndexNumber];
                                                solver.sudokuPuzzleMultiExample = sudokuGrid;
                                                solved = solver.BacktrackinEffcient(false);
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
                    }
                }
            }
            else
            {
                ClearSudokuGrid(sudokuGrid);
                GenerateExampleSudokuGrid();
            }
        }


        //Method to get that cell to change. So get a cell with more than one candidate, and if this candidate is not the last then make it the current bakctack cell. 
        //Need to manually backtrack to see if there is another solution. 

        //THe generating cell now contains this value, so need to pass this puzzle into the solver. by rmeoving all values in the solution up until that cell. 
        //valid numbers in cell contains all of the values that where valid for that cell, therefore it will be the one of the number in that value cell that is used for the backtracking. 

        private void SetFinalGeneratedPuzzle()
        {
            //Getting the value of finalGeneratedPUzzle 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    finalGenenratedPuzzle[i, j] = sudokuGrid[i, j];
                }
            }
        }

        private void ConvertFInalGridIntoSudokuGrid()
        {
            //Getting the value of finalGeneratedPUzzle 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    sudokuGrid[i, j] = finalGenenratedPuzzle[i, j];
                }
            }
        }

        private bool SeeIfGeneratedPuzzleHasTheSameSolutionAsTheOrginal()
        {
            bool isEqual = true;
            for (int row = 0; row <= 8; row++)
            {
                for (int column = 0; column <= 8; column++)
                {
                    if (sudokuGrid[row, column] != orginalSudokuGrid[row, column])
                    {
                        isEqual = false;
                    }
                }
            }
            return isEqual;
        }

        //Method that clears the sudoku grid. 
        private void ClearSudokuGrid(int[,] grid)
        {
            for (int clearRowNumber = 0; clearRowNumber <= 8; clearRowNumber++)
            {
                for (int clearColumnNumber = 0; clearColumnNumber <= 8; clearColumnNumber++)
                {
                    grid[clearRowNumber, clearColumnNumber] = 0;
                }
            }
        }

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


        #region GetBlockNumber 

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



    }



}
