using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    public class SudokuSolver
    {
        //Need a multi dimensions array to solve the puzzle. 
        int[,] sudokuPuzzleMultiExample = new int[9, 9];
        int[] sudokuPuzzleExample = new int[] { 0, 0, 0, 2, 6, 0, 7, 0, 1, 6, 8, 0, 0, 7, 0, 0, 9, 0, 1, 9, 0, 0, 0, 4, 5, 0, 0, 8, 2, 0, 1, 0, 0, 0, 4, 0, 0, 0, 4, 6, 0, 2, 9, 0, 0, 0, 5, 0, 0, 0, 3, 0, 2, 8, 0, 0, 9, 3, 0, 0, 0, 7, 4, 0, 4, 0, 0, 5, 0, 0, 3, 6, 7, 0, 3, 0, 1, 8, 0, 0, 0 };

        public void solvePuzzle()
        {
            int[,] staticNumbers = new int[9, 9];
            int singleArrayValue = 0;
            //Populating multi dimensionsal array. 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    sudokuPuzzleMultiExample[i, j] = sudokuPuzzleExample[singleArrayValue];
                    if (sudokuPuzzleMultiExample[i, j] != 0)
                    {
                        staticNumbers[i, j] = sudokuPuzzleMultiExample[i, j];
                    }
                    singleArrayValue++;
                }
            }
            bool backtracking = false;
            //Main loop that will provide the back tracking. 
            for (int k = 0; k <= 8; k++)
            {
                for (int f = 0; f <= 8; f++)
                {
                    if (staticNumbers[k, f] == 0)
                    {
                        List<int> validNUmbersInRow = checkRow(sudokuPuzzleMultiExample, k, f);
                        List<int> validNumbersInColumn = checkColumn(sudokuPuzzleMultiExample, k, f);
                        List<int> validNumbersInBlock = checkBlock(sudokuPuzzleMultiExample, k, f);
                        List<int> validNumbers = GetValidNumbers(validNumbersInColumn, validNUmbersInRow, validNumbersInBlock);
                        
                        if (validNumbers.Count == 0)
                        {
                            //backtrack 
                            bool staticNumbersBool = false;
                            if (f != 0 || k == 0)
                            {
                                f = f - 1;
                                while (f>=0)
                                {
                                    if (staticNumbers[k, f] != 0)
                                    {
                                        f = f - 1;
                                        staticNumbersBool = true;
                                    }
                                    else
                                    {
                                        f = f - 1;
                                        break;
                                    }

                                }

                                if (f == -2)
                                {
                                    k = k - 2;
                                }
                                //f = f-2;

                            }
                            else
                            {
                                k = k - 1;
                            }

                        }
                        else
                        {
                            foreach (var validNumber in validNumbers)
                            {
                                if (sudokuPuzzleMultiExample[k, f] == validNumber)
                                {

                                }
                                else
                                {
                                    sudokuPuzzleMultiExample[k, f] = validNumber;
                                    break;
                                }

                            }
                        }

                    }
                    //This is the point where the puzzle should be solved 
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

            if (squareRowNumberLogic % 2 == 0)
            {
                if (squareColumnNumberLogic % 2 == 0 && squareColumnNumberLogic != 4)
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
                else if (squareColumnNumberLogic % 3 == 0)
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
            else if (squareRowNumberLogic % 3 == 0)
            {
                if (squareColumnNumberLogic % 2 == 0 && squareColumnNumberLogic != 4)
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
                else if (squareColumnNumberLogic % 3 == 0)
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
                if (squareColumnNumberLogic % 2 == 0 && squareColumnNumberLogic != 4)
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
                else if (squareColumnNumberLogic % 3 == 0)
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
                if (z != squareColumnNumber)
                {
                    numbersInColumn.Add(sudokuPuzzleExample[z, squareColumnNumber]);
                }
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
                if (z != squareRowNumber)
                {
                    numbersInRow.Add(sudokuExamplePuzzle[squareRowNumber, z]);
                }
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
