using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    public class SudokuSolver
    {
        //Example puzzles. 
        int[,] sudokuPuzzleMultiExample = new int[9, 9];
        int[] sudokuPuzzleExample = new int[] { 0, 0, 0, 2, 6, 0, 7, 0, 1, 6, 8, 0, 0, 7, 0, 0, 9, 0, 1, 9, 0, 0, 0, 4, 5, 0, 0, 8, 2, 0, 1, 0, 0, 0, 4, 0, 0, 0, 4, 6, 0, 2, 9, 0, 0, 0, 5, 0, 0, 0, 3, 0, 2, 8, 0, 0, 9, 3, 0, 0, 0, 7, 4, 0, 4, 0, 0, 5, 0, 0, 3, 6, 7, 0, 3, 0, 1, 8, 0, 0, 0 };
        int[] sudokuPuzzleExample2 = new int[] { 5, 3, 0, 0, 7, 0, 0, 0, 0, 6, 0, 0, 1, 9, 5, 0, 0, 0, 0, 9, 8, 0, 0, 0, 0, 6, 0, 8, 0, 0, 0, 6, 0, 0, 0, 3, 4, 0, 0, 8, 0, 3, 0, 0, 1, 7, 0, 0, 0, 2, 0, 0, 0, 6, 0, 6, 0, 0, 0, 0, 2, 8, 0, 0, 0, 0, 4, 1, 9, 0, 0, 5, 0, 0, 0, 0, 8, 0, 0, 7, 9 };
        int[] sudokuPuzzleExample3 = new int[] { 0, 0, 8, 0, 5, 0, 4, 9, 0, 4, 6, 5, 7, 0, 0, 0, 0, 2, 0, 9, 0, 4, 3, 0, 1, 6, 5, 6, 4, 9, 1, 0, 0, 5, 3, 0, 0, 0, 2, 0, 9, 0, 0, 0, 0, 0, 0, 3, 6, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1, 0, 5, 0, 9, 0, 0, 0, 7, 0, 0, 2, 0, 3, 7, 1, 0, 0, 2, 9, 0, 0 };
        int[] sudokuPuzzleExample4 = new int[] { 0, 1, 0, 7, 3, 0, 8, 0, 0, 6, 0, 0, 8, 2, 0, 0, 1, 3, 0, 8, 0, 0, 9, 0, 7, 0, 0, 0, 4, 9, 0, 0, 2, 0, 0, 8, 0, 0, 6, 0, 0, 0, 3, 0, 0, 7, 0, 0, 4, 0, 0, 2, 6, 0, 0, 0, 5, 0, 4, 0, 0, 2, 0, 4, 7, 0, 0, 6, 9, 0, 0, 1, 0, 0, 2, 0, 7, 1, 0, 8, 0 };
        int[] sudokuPuzzleExample5 = new int[] { 5, 0, 0, 1, 0, 0, 7, 0, 0, 0, 2, 0, 0, 0, 7, 1, 0, 0, 3, 0, 1, 4, 0, 0, 8, 5, 2, 6, 1, 0, 5, 7, 2, 4, 0, 8, 0, 0, 2, 9, 6, 0, 0, 0, 0, 0, 4, 0, 0, 3, 0, 6, 2, 7, 4, 5, 9, 0, 8, 0, 0, 7, 0, 1, 3, 0, 0, 0, 0, 9, 8, 6, 2, 0, 0, 0, 1, 0, 0, 4, 3 };


        //array that stores the static numbers that are within the puzzle. 
        int[,] staticNumbers = new int[9, 9];

        public void solvePuzzle()
        {
            //Generate the puzzle and then solve it. 
            GeneratePuzzle();
            solve(sudokuPuzzleMultiExample, 0);
        }

        public void solve(int[,] sudokuGrid, int previousNumber)
        {
            //Check to see if the puzzle is complete. 
            int emptyCellCount = 0;
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (sudokuPuzzleMultiExample[i, j] == 0)
                    {
                        emptyCellCount++;
                    }
                }
            }

            //If the puzzle is complete then a solution is found. 
            if (emptyCellCount == 0)
            {
                for (int i = 0; i <= 8; i++)
                {
                    for (int j = 0; j <= 8; j++)
                    {
                        Console.WriteLine(sudokuPuzzleMultiExample[i, j]);
                    }
                }
            }

            //Else get the values within the puzzle. 
            else
            {
                //Cycle through all of the cells until an empty one. 
                for (int i = 0; i <= 8; i++)
                {
                    for (int j = 0; j <= 8; j++)
                    {
                        //If the cell is empty and the cell is not a static number. 
                        if (sudokuPuzzleMultiExample[i, j] == 0 && staticNumbers[i, j] == 0)
                        {
                            //All of the check to see what numbers are valid for that particular square. 
                            List<int> validNUmbersInRow = checkRow(sudokuPuzzleMultiExample, i, j);
                            List<int> validNumbersInColumn = checkColumn(sudokuPuzzleMultiExample, i, j);
                            List<int> validNumbersInBlock = checkBlock(sudokuPuzzleMultiExample, i, j);
                            List<int> validNumbers = GetValidNumbers(validNumbersInColumn, validNUmbersInRow, validNumbersInBlock);

                            //If there are no valud numbers then backtrack. 
                            if (validNumbers.Count == 0)
                            {
                                //Select the previous cell. 
                                j = j - 1;
                                //Backtrack through the cells, until there is on that isnt static. 
                                for (; j >= -1; j--)
                                {
                                    previousNumber = 0;
                                    //If the backtracking requires to go back a row. This statemetn may not work. 
                                    if (j == -1 && i != 0)
                                    {
                                        i = i - 1;
                                        j = 8;
                                        if ((staticNumbers[i, j] == 0))
                                        {
                                            previousNumber = sudokuPuzzleMultiExample[i, j];
                                            sudokuPuzzleMultiExample[i, j] = 0;
                                            break;
                                        }
                                    }
                                    //If it is the first cell in the grid. 
                                    else if (j == -1 && i == 0)
                                    {
                                        sudokuPuzzleMultiExample[0, 0] = 0;
                                    }
                                    //Else if it is not static then  change the number of the cell that has been backtracked to, to 0 and then recursive with the new grid. 
                                    else if (staticNumbers[i, j] == 0)
                                    {
                                        previousNumber = sudokuPuzzleMultiExample[i, j];
                                        sudokuPuzzleMultiExample[i, j] = 0;
                                        break;
                                    }
                                }
                                //recusre the new grid with the backtacking added. 
                                solve(sudokuPuzzleMultiExample, previousNumber);
                            }
                            //If there is valid numbers, then one need to be set. 
                            else
                            {
                                //Getting all of the valid numbers in  the cell. 
                                foreach (var validNumber in validNumbers)
                                {
                                    //If the valid number has already been used in the cell, then the next number will need to be inserted or the backtracking will need to go back further. 
                                    if (validNumber == previousNumber || validNumber < previousNumber)
                                    {

                                    }
                                    else //Set the valid number to the cell, and submit the new grid. 
                                    {
                                        sudokuPuzzleMultiExample[i, j] = validNumber;
                                        solve(sudokuPuzzleMultiExample, 0);
                                    }
                                }
                                //Further backtracing, smae method as before to backtrack further
                                sudokuPuzzleMultiExample[i, j] = 0;
                                j = j - 1;
                                //so try to solve it with that number if not then start again. 
                                for (; j >= -1; j--)
                                {
                                    previousNumber = 0;

                                    if (j == -1 && i != 0)
                                    {
                                        i = i - 1;
                                        j = 8;
                                        if ((staticNumbers[i, j] == 0))
                                        {
                                            previousNumber = sudokuPuzzleMultiExample[i, j];
                                            sudokuPuzzleMultiExample[i, j] = 0;
                                            break;
                                        }
                                    }
                                    else if (j == -1 && i == 0)
                                    {
                                        sudokuPuzzleMultiExample[0, 0] = 0;
                                    }
                                    else if (staticNumbers[i, j] == 0)
                                    {
                                        previousNumber = sudokuPuzzleMultiExample[i, j];
                                        sudokuPuzzleMultiExample[i, j] = 0;
                                        break;
                                    }
                                }
                                solve(sudokuPuzzleMultiExample, previousNumber);
                            }
                        }
                    }
                }

                //        if (game board is full)
                //    return SUCCESS
                //else
                //    next_square = getNextEmptySquare()
                //    for each value that can legally be put in next_square
                //        put value in next_square(i.e.modify game state)
                //        if (solve(game)) return SUCCESS
                //        remove value from next_square (i.e.backtrack to a previous state)
                //return FAILURE
            }
        }

        private void GeneratePuzzle()
        {
            int singleArrayValue = 0;
            //Populating multi dimensionsal array. 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    sudokuPuzzleMultiExample[i, j] = sudokuPuzzleExample5[singleArrayValue];
                    if (sudokuPuzzleMultiExample[i, j] != 0)
                    {
                        staticNumbers[i, j] = sudokuPuzzleMultiExample[i, j];
                    }
                    singleArrayValue++;
                }
            }

        }

        private List<int> checkBlock(int[,] sudokuPuzzleExample, int squareRowNumber, int squareColumnNumber)
        {
            List<int> validNumbersInBlock = new List<int>();
            List<int> nonValidNumbersInBlock = new List<int>();
            List<int> numbersPositionsInBlock = new List<int>();

            int squareRowNumberLogic = squareRowNumber + 1;
            int squareColumnNumberLogic = squareColumnNumber + 1;

            //Need to work out how to get all the values out of each box. 

            if (squareRowNumberLogic == 2 || squareRowNumberLogic == 5 || squareRowNumberLogic == 8)
            {
                if (squareColumnNumberLogic == 2 || squareColumnNumberLogic == 5 || squareColumnNumberLogic == 8)
                {
                    //Middle square 
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber - 1]);
                }
                else if (squareColumnNumberLogic == 3 || squareColumnNumberLogic == 6 || squareColumnNumberLogic == 9)
                {
                    //Middle right square
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber - 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber - 2]);
                }
                else
                {
                    //Middle left
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber + 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber + 2]);
                }
                //This if it is at the middle of a block. 
            }
            else if (squareRowNumberLogic == 3 || squareRowNumberLogic == 6 || squareRowNumberLogic == 9)
            {
                if (squareColumnNumberLogic == 2 || squareColumnNumberLogic == 5 || squareColumnNumberLogic == 8)
                {
                    //Bottom middle 
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber + 1]);
                }
                else if (squareColumnNumberLogic == 3 || squareColumnNumberLogic == 6 || squareColumnNumberLogic == 9)
                {
                    //Botom righ corner 
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber - 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber - 2]);
                }
                else
                {
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 1, squareColumnNumber + 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber + 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber - 2, squareColumnNumber + 1]);
                }
            }
            else
            {
                if (squareColumnNumberLogic == 2 || squareColumnNumberLogic == 5 || squareColumnNumberLogic == 8)
                {
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber - 1]);
                }
                else if (squareColumnNumberLogic == 3 || squareColumnNumberLogic == 6 || squareColumnNumberLogic == 9)
                {
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber - 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber - 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber - 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber - 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber - 1]);
                }
                else
                {
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber + 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 2]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 2, squareColumnNumber + 1]);
                    numbersPositionsInBlock.Add(sudokuPuzzleExample[squareRowNumber + 1, squareColumnNumber + 2]);
                }
            }

            foreach (var value in numbersPositionsInBlock)
            {
                if (value == 0)
                {

                }
                else
                {
                    nonValidNumbersInBlock.Add(value);
                }
            }
            validNumbersInBlock = GetValidNumbers(nonValidNumbersInBlock);
            return validNumbersInBlock;
        }

        //There is something wrong with this method atm. 
        private List<int> checkColumn(int[,] sudokuPuzzleExample, int squareRowNumber, int squareColumnNumber)
        {
            List<int> validNumbersInColumn = new List<int>();
            List<int> nonValidNumbersInColumn = new List<int>();
            List<int> numbersInColumn = new List<int>();

            for (int z = 0; z <= 8; z++)
            {

                numbersInColumn.Add(sudokuPuzzleExample[z, squareColumnNumber]);

            }

            foreach (var value in numbersInColumn)
            {
                if (value == 0)
                {

                }
                else
                {
                    nonValidNumbersInColumn.Add(value);
                }
            }
            //Getting all the valid numbers i.e. the numbers that are not already in the column. 
            validNumbersInColumn = GetValidNumbers(nonValidNumbersInColumn);
            return validNumbersInColumn;
        }

        private List<int> checkRow(int[,] sudokuExamplePuzzle, int squareRowNumber, int squareColumnNumber)
        {
            List<int> validNumbersInRow = new List<int>();
            List<int> nonValidNumbersInRow = new List<int>();
            List<int> numbersInRow = new List<int>();

            for (int z = 0; z <= 8; z++)
            {

                numbersInRow.Add(sudokuExamplePuzzle[squareRowNumber, z]);

            }

            foreach (var value in numbersInRow)
            {
                if (value == 0)
                {

                }
                else
                {
                    nonValidNumbersInRow.Add(value);
                }
            }
            //Getting all the valid numbers i.e. the numbers that are not already in the column. 
            validNumbersInRow = GetValidNumbers(nonValidNumbersInRow);
            return validNumbersInRow;
        }

        //Methods that gets all the valid values dependant on the values that are currently in the row, column or block. 
        private List<int> GetValidNumbers(List<int> nonValidNumbers)
        {
            List<int> validNumbers = new List<int>();
            //Get the valid number that can be within this row. These should be in number order. 
            for (int y = 1; y <= 9; y++)
            {
                if (nonValidNumbers.Contains(y) == false)
                {
                    validNumbers.Add(y);
                }
            }
            return validNumbers;
        }

        private List<int> GetValidNumbers(List<int> columnNumbers, List<int> rowNumbers, List<int> blockNumbers)
        {
            List<int> validNumbers = new List<int>();

            foreach (var columnValue in columnNumbers)
            {
                if (rowNumbers.Contains(columnValue) && blockNumbers.Contains(columnValue))
                {
                    validNumbers.Add(columnValue);
                }
            }

            return validNumbers;
        }

    }
}
