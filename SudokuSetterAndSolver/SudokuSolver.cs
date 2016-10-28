﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    public class SudokuSolver
    {
        //Example puzzles. 

        //Goes into infinite loop on examples 2,6 and 9. 
        int[,] sudokuPuzzleMultiExample = new int[9, 9];
        int[] sudokuPuzzleExample = new int[] { 0, 0, 0, 2, 6, 0, 7, 0, 1, 6, 8, 0, 0, 7, 0, 0, 9, 0, 1, 9, 0, 0, 0, 4, 5, 0, 0, 8, 2, 0, 1, 0, 0, 0, 4, 0, 0, 0, 4, 6, 0, 2, 9, 0, 0, 0, 5, 0, 0, 0, 3, 0, 2, 8, 0, 0, 9, 3, 0, 0, 0, 7, 4, 0, 4, 0, 0, 5, 0, 0, 3, 6, 7, 0, 3, 0, 1, 8, 0, 0, 0 };
        int[] sudokuPuzzleExample2 = new int[] { 5, 3, 0, 0, 7, 0, 0, 0, 0, 6, 0, 0, 1, 9, 5, 0, 0, 0, 0, 9, 8, 0, 0, 0, 0, 6, 0, 8, 0, 0, 0, 6, 0, 0, 0, 3, 4, 0, 0, 8, 0, 3, 0, 0, 1, 7, 0, 0, 0, 2, 0, 0, 0, 6, 0, 6, 0, 0, 0, 0, 2, 8, 0, 0, 0, 0, 4, 1, 9, 0, 0, 5, 0, 0, 0, 0, 8, 0, 0, 7, 9 };
        int[] sudokuPuzzleExample3 = new int[] { 0, 0, 8, 0, 5, 0, 4, 9, 0, 4, 6, 5, 7, 0, 0, 0, 0, 2, 0, 9, 0, 4, 3, 0, 1, 6, 5, 6, 4, 9, 1, 0, 0, 5, 3, 0, 0, 0, 2, 0, 9, 0, 0, 0, 0, 0, 0, 3, 6, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1, 0, 5, 0, 9, 0, 0, 0, 7, 0, 0, 2, 0, 3, 7, 1, 0, 0, 2, 9, 0, 0 };
        int[] sudokuPuzzleExample4 = new int[] { 0, 1, 0, 7, 3, 0, 8, 0, 0, 6, 0, 0, 8, 2, 0, 0, 1, 3, 0, 8, 0, 0, 9, 0, 7, 0, 0, 0, 4, 9, 0, 0, 2, 0, 0, 8, 0, 0, 6, 0, 0, 0, 3, 0, 0, 7, 0, 0, 4, 0, 0, 2, 6, 0, 0, 0, 5, 0, 4, 0, 0, 2, 0, 4, 7, 0, 0, 6, 9, 0, 0, 1, 0, 0, 2, 0, 7, 1, 0, 8, 0 };
        int[] sudokuPuzzleExample5 = new int[] { 5, 0, 0, 1, 0, 0, 7, 0, 0, 0, 2, 0, 0, 0, 7, 1, 0, 0, 3, 0, 1, 4, 0, 0, 8, 5, 2, 6, 1, 0, 5, 7, 2, 4, 0, 8, 0, 0, 2, 9, 6, 0, 0, 0, 0, 0, 4, 0, 0, 3, 0, 6, 2, 7, 4, 5, 9, 0, 8, 0, 0, 7, 0, 1, 3, 0, 0, 0, 0, 9, 8, 6, 2, 0, 0, 0, 1, 0, 0, 4, 3 };
        int[] sudokuPuzzleExample6 = new int[] { 4, 1, 0, 0, 7, 0, 0, 0, 5, 0, 8, 0, 0, 0, 6, 0, 9, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 7, 4, 0, 1, 3, 0, 0, 5, 3, 0, 0, 0, 0, 0, 1, 2, 0, 0, 4, 3, 0, 8, 7, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 9, 0, 8, 0, 0, 0, 7, 0, 7, 0, 0, 0, 6, 0, 0, 2, 8 };
        int[] sudokuPuzzleExample7 = new int[] { 0, 0, 0, 0, 0, 0, 7, 0, 4, 3, 2, 0, 6, 1, 4, 9, 0, 5, 6, 0, 0, 8, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 7, 0, 2, 0, 0, 9, 0, 4, 8, 5, 0, 7, 0, 0, 8, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 8, 8, 0, 2, 9, 5, 1, 0, 3, 7, 0, 0, 9, 0, 0, 0, 0, 0, 0 };
        int[] sudokuPuzzleExample8 = new int[] { 9, 0, 0, 0, 6, 0, 0, 0, 3, 1, 0, 5, 0, 9, 3, 2, 0, 6, 0, 4, 0, 0, 5, 0, 0, 0, 9, 8, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 4, 8, 7, 0, 0, 0, 0, 7, 0, 2, 6, 0, 1, 0, 0, 8, 2, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 3, 2, 0, 9, 4, 0, 8, 7, 0, 1, 6, 3, 5, 0 };
        int[] sudokuPuzzleExample9 = new int[] { 0, 0, 5, 3, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 2, 0, 0, 7, 0, 0, 1, 0, 5, 0, 0, 4, 0, 0, 0, 0, 5, 3, 0, 0, 0, 1, 0, 0, 7, 0, 0, 0, 6, 0, 0, 3, 2, 0, 0, 0, 8, 0, 0, 6, 0, 5, 0, 0, 0, 0, 9, 0, 0, 4, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 9, 7, 0, 0 };


        //array that stores the static numbers that are within the puzzle. 
        int[,] staticNumbers = new int[9, 9];
        int[] validNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> validNumbersForRegion = new List<int>();
        public void solvePuzzle()
        {
            for (int validValue = 1; validValue <= 9; validValue++)
            {
                validNumbersForRegion.Add(validValue);
            }

            //Generate the puzzle and then solve it. 
            GeneratePuzzle();
            SolveSudokRuleBased();
            SolveUsingRecursiveBactracking();
            solve(sudokuPuzzleMultiExample, 0);
            SolveConstraintsProblem(sudokuPuzzleMultiExample, validNumbersForRegion);
        }

        private void GeneratePuzzle()
        {
            int singleArrayValue = 0;
            
            //Populating multi dimensionsal array. 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    sudokuPuzzleMultiExample[i, j] = sudokuPuzzleExample3[singleArrayValue];
                    if (sudokuPuzzleMultiExample[i, j] != 0)
                    {
                        staticNumbers[i, j] = sudokuPuzzleMultiExample[i, j];
                    }
                    singleArrayValue++;
                }
            }

        }

        private bool CheckToSeeIfPuzzleIsSolved()
        {
            // Check to see if the puzzle is complete.
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
              
                        return true;
                
            }
            else
            {
                return false; 
            }
        }

        #region Rule Based Algorithm 

        //Use human rules to check the puzzle out, inserting any possible values, then use bactrcking to solve the rest of the puzzle. 

        List<List<int>> candidatesList = new List<List<int>>();

        public void SolveSudokRuleBased()
        {

            //Trying to implement hidde singles is not easy. 
            List<int> listOfCandidatesInRow = new List<int>();
            List<int> previousValidNumbersRow = new List<int>();
            int nakedSinglesCount = 0; 


            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                listOfCandidatesInRow.Clear();

                for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    //All of the check to see what numbers are valid for that particular square. 
                    List<int> validNUmbersInRow = checkRow(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                    List<int> validNumbersInColumn = checkColumn(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                    List<int> validNumbersInBlock = checkBlock(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                    List<int> validNumbers = GetValidNumbers(validNumbersInColumn, validNUmbersInRow, validNumbersInBlock);

                    //Static numbers check 
                    if (staticNumbers[rowNumber, columnNumber] == 0)
                    {
                        candidatesList.Add(validNumbers);
                        if (validNumbers.Count == 1)
                        {
                            //Changing values and making them static 
                            nakedSinglesCount ++;
                            staticNumbers[rowNumber, columnNumber] = validNumbers[0];
                            sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbers[0];
                        }
                    }
                    else
                    {
                        candidatesList.Add(null);
                    }

                    listOfCandidatesInRow.AddRange(validNumbers);
                    //Naked singles 
                   


                }


            }

            if(nakedSinglesCount !=0)
            {
                bool solved = CheckToSeeIfPuzzleIsSolved();

                if(solved)
                {
                    Console.WriteLine("Solved"); 
                }
                candidatesList.Clear();
                //Recursive call to see if there is any more naked singles. 
                SolveSudokRuleBased();
            }


        }

        private void CandidateHandling()
        {
            NakedTuples();
            HiddenColumnSingles();
            HiddenBlockSingles();
        }


        private void NakedTuples()
        {
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            List<int> previousCandidates = new List<int>();
            List<int> indexValue = new List<int>();
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);

                //If the row is at an end. 
                if (rowNumber % 8 == 0)
                {
                    //Seeing of there are any naked tuples within the row. 
                    for (int firstIndexValue = rowNumber - 8; firstIndexValue <= rowNumber; firstIndexValue++)
                    {
                        //Gets all of the index values for the naked tuples, so candidates can be removed. 
                        for (int secondIndexValue = rowNumber - 8; secondIndexValue <= rowNumber; secondIndexValue++)
                        {
                            //This comapres 2 lists to see if thet are equal 
                            //http://stackoverflow.com/questions/22173762/check-if-two-lists-are-equal
                            if (Enumerable.SequenceEqual(cadidatesInSingleRow[firstIndexValue].OrderBy(fList => fList),
                                cadidatesInSingleRow[secondIndexValue].OrderBy(sList => sList)) == true)
                            {
                                indexValue.Add(firstIndexValue);
                                indexValue.Add(secondIndexValue);
                                
                            }
                        }

                        //Getting all of the naked values. 
                        List<int> nakedCandidates = cadidatesInSingleRow[indexValue[0]];
                        bool isIndexNumber = false;

                        //Removing naked values from other cells within the grid. 
                        for (int indexValueOfListInRow = rowNumber - 8; indexValueOfListInRow <= rowNumber; indexValueOfListInRow++)
                        {
                            foreach (int nakedValueIndexNumber in indexValue)
                            {
                                //If the cell is equal to one of the ones that have the naked tuples in, then do not remove the cnaidates from the cell. 
                                if (indexValueOfListInRow == nakedValueIndexNumber)
                                {
                                    isIndexNumber = true;
                                    break;
                                }
                            }
                            //If the cell is not part of the naked tuples, then removed the candidates, if any from the cell. 
                            if (isIndexNumber != true)
                            {
                                for (int candidateNumber = 0; candidateNumber <= cadidatesInSingleRow[rowNumber].Count; candidateNumber++)
                                {
                                    foreach (int nakedCandidateNumber in nakedCandidates)
                                    {
                                        foreach (int valueInRowCandiates in cadidatesInSingleRow[rowNumber])
                                        {
                                            if (valueInRowCandiates == nakedCandidateNumber)
                                            {
                                                //Removing candidate from cell. 
                                                candidatesList[rowNumber].RemoveAt(candidateNumber);
                                            }
                                        }
                                    }
                                }
                            }
                           
                        }
                        indexValue.Clear();    
                    } //Closing bracket when all comparing has been completed. 
                    cadidatesInSingleRow.Clear();
                }
            }
        }

        private void HiddenColumnSingles()
        {

        }

        private void HiddenBlockSingles()
        {

        }
        public void HiddenSingles()
        {

        }
        #endregion 

        #region New Recursive backtracking algorithm 

        //Suodku multiexample is the global varibale that stores the grid. 

        public void SolveUsingRecursiveBactracking()
        {
            //Get nexg empt cell, for all candidates 
            //Chnage 
            //Recursive call and then reverse the change 

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

                        foreach (var validNumber in validNumbers)
                        {
                            ChangeGrid(i, j, validNumber);
                            SolveUsingRecursiveBactracking();
                            ReverseGrid(i, j, validNumber);
                        }
                    }
                }
            }

        }

        public void ChangeGrid(int rowNumber, int columnNumber, int validNumber)
        {
            sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumber;
        }

        public void ReverseGrid(int rowNUmber, int columnNumber, int validNumber)
        {
            sudokuPuzzleMultiExample[rowNUmber, columnNumber] = 0;
        }

        #endregion 

        #region Constraint problem 

        //Method takes way to long due to random nature, and the amount of possibilities this creates. 

        private void SolveConstraintsProblem(int[,] puzzle, List<int> tempValidNumberRegion)
        {
            bool rows = false;
            bool columns = false;
            bool minigrids = false;
            Random random = new Random();
            bool solved = false;

            while (rows == false || columns == false || minigrids == false)
            {
                tempValidNumberRegion = validNumbersForRegion;

                rows = GetRowTotalValues(puzzle);
                columns = GetColumnTotalValues(puzzle);
                minigrids = GetBlockTotalValues(puzzle);
                for (int i = 0; i <= 8; i++)
                {

                    for (int z = 0; z <= 8; z++)
                    {
                        for (int listValue = 0; listValue <= tempValidNumberRegion.Count - 1; listValue++)
                        {
                            if (tempValidNumberRegion[listValue] == puzzle[i, z])
                            {
                                tempValidNumberRegion.RemoveAt(listValue);
                            }
                        }
                    }
                    for (int j = 0; j <= 8; j++)
                    {
                        if (puzzle[i, j] == 0)
                        {
                            int randomNumber = tempValidNumberRegion[random.Next(tempValidNumberRegion.Count - 1)];


                            puzzle[i, j] = randomNumber;
                            for (int listValue = 0; listValue <= tempValidNumberRegion.Count - 1; listValue++)
                            {
                                if (tempValidNumberRegion[listValue] == randomNumber)
                                {
                                    tempValidNumberRegion.RemoveAt(listValue);
                                    break;
                                }
                            }
                        }
                    }
                    tempValidNumberRegion = PopulateNumberList(tempValidNumberRegion);


                }
            }
            solved = true;
        }

        private List<int> PopulateNumberList(List<int> numberList)
        {
            for (int validValue = 1; validValue <= 9; validValue++)
            {
                numberList.Add(validValue);
            }

            return numberList;
        }

        private bool GetRowTotalValues(int[,] puzzle)
        {
            int totalValue = 0;
            List<int> rowTotalValues = new List<int>();
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    totalValue += puzzle[i, j];
                }
                rowTotalValues.Add(totalValue);
                totalValue = 0;
            }

            foreach (var value in rowTotalValues)
            {
                if (value != 45)
                {
                    return false;
                }
            }
            return true;
        }

        private bool GetColumnTotalValues(int[,] puzzle)
        {
            int totalValue = 0;
            List<int> coulmnTotalValues = new List<int>();
            for (int i = 0; i <= 8; i++)
            {

                for (int j = 0; j <= 8; j++)
                {
                    totalValue += puzzle[j, i];
                }
                coulmnTotalValues.Add(totalValue);
                totalValue = 0;
            }
            foreach (var value in coulmnTotalValues)
            {
                if (value != 45)
                {
                    return false;
                }
            }
            return true;
        }

        private bool GetBlockTotalValues(int[,] puzzle)
        {
            int kStartPoint = 0;
            int jStartPoint = 0;
            int totalValue = 0;
            List<int> blockTotalValues = new List<int>();
            for (int i = 0; i <= 8; i++)
            {
                for (int k = kStartPoint; k <= kStartPoint + 2; k++)
                {
                    for (int j = jStartPoint; j <= jStartPoint + 2; j++)
                    {
                        totalValue += puzzle[k, j];

                    }
                }
                if (i == 0 || i == 1 || i == 3 || i == 4 || i == 6 || i == 7)
                {
                    jStartPoint += 3;
                }
                else if (i == 2 || i == 5)
                {
                    jStartPoint = 0;
                    kStartPoint += 3;
                }
                blockTotalValues.Add(totalValue);
                totalValue = 0;
            }
            foreach (var value in blockTotalValues)
            {
                if (value != 45)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Backtracking

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
                            if (i == 0 && j == 1)
                            {

                            }
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

        #endregion 

    }
}
