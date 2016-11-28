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
        //Grids that will contain the created grid, the first will be used to check to ensure the grid is valid 
        int[,] sudokuGrid = new int[9, 9];
        int[,] finalGenenratedPuzzle = new int[9, 9];
        public int[,] orginalSudokuGrid = new int[9, 9];
        int _gridSize = 0;
        int rowNumber = 0;
        int columnNumber = 0;
        bool solved = false;
        int rowNumberOfGeneratingBacktrackCell = 0;
        int columnNumberOFGeneratingBacktrackingCell = 0;
        int numberToInsertIntoTheBacktrackingCell = 0;
        string puzzlePifficulty = "";
        #endregion

        #region Objects 
        PuzzleManager puzzleManager = new PuzzleManager();
        SudokuSolver solver = new SudokuSolver();
        Random randomNumber = new Random();
        Random randomNumberGenerator = new Random();

        #endregion

        #region Constructor 
        public SudokuPuzzleGenerator(int gridSize)
        {
            _gridSize = gridSize;

        }
        #endregion

        #region Methods 

        //This method is called when the grid is created, on the screen start up. 
        public int[,] CreateSudokuGrid()
        {
            ClearSudokuGrid(finalGenenratedPuzzle);
            ClearSudokuGrid(sudokuGrid);
            ClearSudokuGrid(orginalSudokuGrid);
            GenerateExampleSudokuGrid();
            return finalGenenratedPuzzle;
        }

        //Create 2 methods, one which diggies holes from a grid and the other one inserts radnom givens in the puzzle. 

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

                    if(solved == false)
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

        
    }
    #endregion
}
