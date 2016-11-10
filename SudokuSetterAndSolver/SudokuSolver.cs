using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    //Things to do, test the naked values more, create a global candidates list, that is updated accordingly. 
    //Refactoring needs to be done between the columns and rows, with the hidden singles and also the naked rows and columns.

    //BY this week want to get all the nakeds done, have a look at hidden tuples and also try and sort out recursive bactracking with David. 

    public class SudokuSolver
    {
        #region Example Puzzles
        //Example puzzles. 

        //Goes into infinite loop on examples 2 and 9 on backtracking algorith, sudokuBlockHiddenSingleExample sudokuHiddenSinglesExample
        public int[,] sudokuPuzzleMultiExample = new int[9, 9];
        int[] sudokuPuzzleExample = new int[] { 0, 0, 0, 2, 6, 0, 7, 0, 1, 6, 8, 0, 0, 7, 0, 0, 9, 0, 1, 9, 0, 0, 0, 4, 5, 0, 0, 8, 2, 0, 1, 0, 0, 0, 4, 0, 0, 0, 4, 6, 0, 2, 9, 0, 0, 0, 5, 0, 0, 0, 3, 0, 2, 8, 0, 0, 9, 3, 0, 0, 0, 7, 4, 0, 4, 0, 0, 5, 0, 0, 3, 6, 7, 0, 3, 0, 1, 8, 0, 0, 0 };
        int[] sudokuPuzzleExample2 = new int[] { 5, 3, 0, 0, 7, 0, 0, 0, 0, 6, 0, 0, 1, 9, 5, 0, 0, 0, 0, 9, 8, 0, 0, 0, 0, 6, 0, 8, 0, 0, 0, 6, 0, 0, 0, 3, 4, 0, 0, 8, 0, 3, 0, 0, 1, 7, 0, 0, 0, 2, 0, 0, 0, 6, 0, 6, 0, 0, 0, 0, 2, 8, 0, 0, 0, 0, 4, 1, 9, 0, 0, 5, 0, 0, 0, 0, 8, 0, 0, 7, 9 };
        int[] sudokuPuzzleExample3 = new int[] { 0, 0, 8, 0, 5, 0, 4, 9, 0, 4, 6, 5, 7, 0, 0, 0, 0, 2, 0, 9, 0, 4, 3, 0, 1, 6, 5, 6, 4, 9, 1, 0, 0, 5, 3, 0, 0, 0, 2, 0, 9, 0, 0, 0, 0, 0, 0, 3, 6, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 1, 0, 5, 0, 9, 0, 0, 0, 7, 0, 0, 2, 0, 3, 7, 1, 0, 0, 2, 9, 0, 0 };
        int[] sudokuPuzzleExample4 = new int[] { 0, 1, 0, 7, 3, 0, 8, 0, 0, 6, 0, 0, 8, 2, 0, 0, 1, 3, 0, 8, 0, 0, 9, 0, 7, 0, 0, 0, 4, 9, 0, 0, 2, 0, 0, 8, 0, 0, 6, 0, 0, 0, 3, 0, 0, 7, 0, 0, 4, 0, 0, 2, 6, 0, 0, 0, 5, 0, 4, 0, 0, 2, 0, 4, 7, 0, 0, 6, 9, 0, 0, 1, 0, 0, 2, 0, 7, 1, 0, 8, 0 };
        int[] sudokuPuzzleExample5 = new int[] { 5, 0, 0, 1, 0, 0, 7, 0, 0, 0, 2, 0, 0, 0, 7, 1, 0, 0, 3, 0, 1, 4, 0, 0, 8, 5, 2, 6, 1, 0, 5, 7, 2, 4, 0, 8, 0, 0, 2, 9, 6, 0, 0, 0, 0, 0, 4, 0, 0, 3, 0, 6, 2, 7, 4, 5, 9, 0, 8, 0, 0, 7, 0, 1, 3, 0, 0, 0, 0, 9, 8, 6, 2, 0, 0, 0, 1, 0, 0, 4, 3 };
        int[] sudokuPuzzleExample6 = new int[] { 4, 1, 0, 0, 7, 0, 0, 0, 5, 0, 8, 0, 0, 0, 6, 0, 9, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 7, 4, 0, 1, 3, 0, 0, 5, 3, 0, 0, 0, 0, 0, 1, 2, 0, 0, 4, 3, 0, 8, 7, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 9, 0, 8, 0, 0, 0, 7, 0, 7, 0, 0, 0, 6, 0, 0, 2, 8 };
        int[] sudokuPuzzleExample7 = new int[] { 0, 0, 0, 0, 0, 0, 7, 0, 4, 3, 2, 0, 6, 1, 4, 9, 0, 5, 6, 0, 0, 8, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 7, 0, 2, 0, 0, 9, 0, 4, 8, 5, 0, 7, 0, 0, 8, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 3, 0, 0, 8, 8, 0, 2, 9, 5, 1, 0, 3, 7, 0, 0, 9, 0, 0, 0, 0, 0, 0 };
        int[] sudokuPuzzleExample8 = new int[] { 9, 0, 0, 0, 6, 0, 0, 0, 3, 1, 0, 5, 0, 9, 3, 2, 0, 6, 0, 4, 0, 0, 5, 0, 0, 0, 9, 8, 0, 0, 0, 0, 0, 4, 7, 1, 0, 0, 4, 8, 7, 0, 0, 0, 0, 7, 0, 2, 6, 0, 1, 0, 0, 8, 2, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 3, 2, 0, 9, 4, 0, 8, 7, 0, 1, 6, 3, 5, 0 };
        int[] sudokuPuzzleExample9 = new int[] { 0, 0, 5, 3, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 2, 0, 0, 7, 0, 0, 1, 0, 5, 0, 0, 4, 0, 0, 0, 0, 5, 3, 0, 0, 0, 1, 0, 0, 7, 0, 0, 0, 6, 0, 0, 3, 2, 0, 0, 0, 8, 0, 0, 6, 0, 5, 0, 0, 0, 0, 9, 0, 0, 4, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 9, 7, 0, 0 };
        int[] sudokuPUzzleWithNakedTuple = new int[] { 4, 0, 0, 2, 7, 0, 6, 0, 0, 7, 9, 8, 1, 5, 6, 2, 3, 4, 0, 2, 0, 8, 4, 0, 0, 0, 7, 2, 3, 7, 4, 6, 8, 9, 5, 1, 8, 4, 9, 5, 3, 1, 7, 2, 6, 5, 6, 1, 7, 9, 2, 8, 4, 3, 0, 8, 2, 0, 1, 5, 4, 7, 9, 0, 7, 0, 0, 2, 4, 3, 0, 0, 0, 0, 4, 0, 8, 7, 0, 0, 2 };
        int[] sudokuHiddenSinglesExample = new int[] { 7, 0, 0, 2, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 6, 3, 0, 0, 0, 4, 0, 0, 9, 0, 0, 0, 6, 9, 1, 0, 0, 0, 0, 2, 3, 0, 0, 2, 1, 8, 0, 0, 0, 6, 0, 0, 0, 0, 6, 0, 5, 0, 0, 4, 0, 3, 0, 7, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1, 0, 5, 0, 8, 0, 0, 0, 0, 0 };
        int[] sudokuBlockHiddenSingleExample = new int[] { 0, 0, 0, 0, 0, 0, 5, 0, 0, 1, 6, 0, 9, 0, 0, 0, 0, 0, 0, 0, 9, 0, 6, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 5, 0, 0, 0, 2, 0, 8, 9, 0, 0, 0, 0, 1, 0, 2, 5, 0, 0, 3, 0, 7, 0, 0, 1, 0, 0, 0, 0, 9 };
        int[] sudokuNakedDoublesColumnBlockExample = new int[] { 0, 8, 0, 0, 9, 0, 0, 3, 0, 0, 3, 0, 0, 0, 0, 0, 6, 9, 9, 0, 2, 0, 6, 3, 1, 5, 8, 0, 2, 0, 8, 0, 4, 5, 9, 0, 8, 5, 1, 9, 0, 7, 0, 4, 6, 3, 9, 4, 6, 0, 5, 8, 7, 0, 5, 6, 3, 0, 4, 0, 9, 8, 7, 2, 0, 0, 0, 0, 0, 0, 1, 5, 0, 1, 0, 0, 5, 0, 0, 2, 0 };
        int[] nakedTripleRowExample = new int[] { 0, 7, 0, 4, 0, 8, 0, 2, 9, 0, 0, 2, 0, 0, 0, 0, 0, 4, 8, 5, 4, 0, 2, 0, 0, 0, 7, 0, 0, 8, 3, 7, 4, 2, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 2, 6, 1, 7, 0, 0, 0, 0, 0, 0, 9, 3, 6, 1, 2, 2, 0, 0, 0, 0, 0, 4, 0, 3, 1, 3, 0, 6, 4, 2, 0, 7, 0 };
        int[] nakedDoubleRowExample = new int[] { 4, 0, 0, 0, 0, 0, 9, 3, 8, 0, 3, 2, 0, 9, 4, 1, 0, 0, 0, 9, 5, 3, 0, 0, 2, 4, 0, 3, 7, 0, 6, 0, 9, 0, 0, 4, 5, 2, 9, 0, 0, 1, 6, 7, 3, 6, 0, 4, 7, 0, 3, 0, 9, 0, 9, 5, 7, 0, 0, 8, 3, 0, 0, 0, 0, 3, 9, 0, 0, 4, 0, 0, 2, 4, 0, 0, 3, 0, 7, 0, 9 };
        int[] nakedBlockExample = new int[] { 2, 9, 4, 5, 1, 3, 0, 0, 6, 6, 0, 0, 8, 4, 2, 3, 1, 9, 3, 0, 0, 6, 9, 7, 2, 5, 4, 0, 0, 0, 0, 5, 6, 0, 0, 0, 0, 4, 0, 0, 8, 0, 0, 6, 0, 0, 0, 0, 4, 7, 0, 0, 0, 0, 7, 3, 0, 1, 6, 4, 0, 0, 5, 9, 0, 0, 7, 3, 5, 0, 0, 1, 4, 0, 0, 9, 2, 8, 6, 3, 7 };
        int[] nakedColumnExample = new int[] { 6, 0, 0, 8, 0, 2, 7, 3, 5, 7, 0, 2, 3, 5, 6, 9, 4, 0, 3, 0, 0, 4, 0, 7, 0, 6, 2, 1, 0, 0, 9, 7, 5, 0, 2, 4, 2, 0, 0, 1, 8, 3, 0, 7, 9, 0, 7, 9, 6, 2, 4, 0, 0, 3, 4, 0, 0, 5, 6, 0, 2, 0, 7, 0, 6, 7, 2, 4, 0, 3, 0, 0, 9, 2, 0, 7, 3, 8, 4, 0, 6 };
        int[] hiddenDoublesAll = new int[] { 7, 2, 0, 4, 0, 8, 0, 3, 0, 0, 8, 0, 0, 0, 0, 0, 4, 7, 4, 0, 1, 0, 7, 6, 8, 0, 2, 8, 1, 0, 7, 3, 9, 0, 0, 0, 0, 0, 0, 8, 5, 1, 0, 0, 0, 0, 0, 0, 2, 6, 4, 0, 8, 0, 2, 0, 9, 6, 8, 0, 4, 1, 3, 3, 4, 0, 0, 0, 0, 0, 0, 8, 1, 6, 8, 9, 4, 3, 2, 7, 5 };
        int[] hiddenTreplesRowColumn = new int[] { 0, 0, 0, 0, 0, 1, 0, 3, 0, 2, 3, 1, 0, 9, 0, 0, 0, 0, 0, 6, 5, 0, 0, 3, 1, 0, 0, 6, 7, 8, 9, 2, 4, 3, 0, 0, 1, 0, 3, 0, 5, 0, 0, 0, 6, 0, 0, 0, 1, 3, 6, 7, 0, 0, 0, 0, 9, 3, 6, 0, 5, 7, 0, 0, 0, 6, 0, 1, 9, 8, 4, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0 };

        //All of the check to see what numbers are valid for that particular square. 
        List<int> validNUmbersInRow;
        List<int> validNumbersInColumn;
        List<int> validNumbersInBlock;
        List<int> validNumbersInCell;


        #endregion

        #region Gloabl Variables 
        //array that stores the static numbers that are within the puzzle. 
        public int[,] staticNumbers = new int[9, 9];
        int[] validNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> validNumbersForRegion = new List<int>();
        #endregion 

        #region Main Method 
        public void solvePuzzle()
        {
            for (int validValue = 1; validValue <= 9; validValue++)
            {
                validNumbersForRegion.Add(validValue);
            }

            //Need to anlayse the way in which the puzzle will be solved, need to go to the next step if there where no hiddens added for example. 

            //Generate the puzzle and then solve it. 
            GeneratePuzzle();
            //SolveSudokRuleBased();
            // SolveUsingRecursiveBactracking();
            //BacktrackingSolve(0);
            //solve(sudokuPuzzleMultiExample, 0);
            //SolveConstraintsProblem(sudokuPuzzleMultiExample, validNumbersForRegion);
            //SolveStoaticSearch(sudokuPuzzleMultiExample, validNumbersForRegion);
            BacktrackingSolve(0);
        }

        #endregion 

        #region General Methods 
        //Method that generates the example puzzle. 
        private void GeneratePuzzle()
        {
            int singleArrayValue = 0;

            //Populating multi dimensionsal array. 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    sudokuPuzzleMultiExample[i, j] = sudokuPuzzleExample2[singleArrayValue];
                    if (sudokuPuzzleMultiExample[i, j] != 0)
                    {
                        staticNumbers[i, j] = sudokuPuzzleMultiExample[i, j];
                    }
                    singleArrayValue++;
                }
            }
        }

        //Method that checks whether the puzzle has been solved or not. 
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
                        break;
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
        #endregion 

        #region Rule Based Algorithm 

        //Use human rules to check the puzzle out, inserting any possible values, then use bactrcking to solve the rest of the puzzle. 

        //This is the global varibale to store the candidates list. This needs to be carefully 
        //Mayve be there are more candidates in the new puzzle, remove them using the old method. This is just about to be implemtted.  THen should be tested on the example that i have created/ 
        List<List<int>> candidatesList = new List<List<int>>();

        int methodRunNumber = 0;

        public void SolveSudokRuleBased()
        {
            //Trying to implement hidde singles is not easy. 
            List<int> listOfCandidatesInRow = new List<int>();
            List<int> previousValidNumbersRow = new List<int>();
            List<List<int>> tempCandiateList = new List<List<int>>();

            tempCandiateList.Clear();
            int nakedSinglesCount = 0;

            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
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
                        tempCandiateList.Add(validNumbers);

                    }
                    else
                    {
                        tempCandiateList.Add(null);
                    }
                }
            }
            //Check to see if its the first run of the method, and setting the orginal 
            if (methodRunNumber == 0)
            {
                candidatesList = tempCandiateList;
            }
            else
            {
                CompareCandidateLists(tempCandiateList);
            }


            int rowNumberCheck = 0;
            int columnCheckNumber = 0;
            for (int indexOfCandidateValue = 0; indexOfCandidateValue <= candidatesList.Count - 1; indexOfCandidateValue++)
            {
                if (candidatesList[indexOfCandidateValue] != null)
                {
                    if (candidatesList[indexOfCandidateValue].Count == 1)
                    {
                        nakedSinglesCount++;
                        foreach (int nakedValue in candidatesList[indexOfCandidateValue])
                        {
                            staticNumbers[rowNumberCheck, columnCheckNumber] = nakedValue;
                            sudokuPuzzleMultiExample[rowNumberCheck, columnCheckNumber] = nakedValue;
                            candidatesList[indexOfCandidateValue] = null;
                        }
                    }
                }

                if (indexOfCandidateValue % 9 == 8 || indexOfCandidateValue == 8)
                {
                    columnCheckNumber = 0;
                    rowNumberCheck++;
                }
                else
                {
                    columnCheckNumber++;
                }
            }

            rowNumberCheck = 0;
            columnCheckNumber = 0;

            methodRunNumber++;

            if (nakedSinglesCount != 0)
            {
                bool solved = CheckToSeeIfPuzzleIsSolved();

                if (solved)
                {
                    Console.WriteLine("Solved");
                }

                //Recursive call to see if there is any more naked singles. 
                SolveSudokRuleBased();
            }
            //HiddenSingles();
            CandidateHandling();
            //solve(0);

            SolveUsingRecursiveBactracking();
        }

        //Method that creates the correct candidate list. 
        private void CompareCandidateLists(List<List<int>> tempCandidateList)
        {
            for (int indexValue = 0; indexValue <= 80; indexValue++)
            {
                List<int> finalCandidateList = new List<int>();
                if (candidatesList[indexValue] == null || tempCandidateList[indexValue] == null)
                {
                    candidatesList[indexValue] = null;
                }
                //http://stackoverflow.com/questions/22173762/check-if-two-lists-are-equal
                else if (Enumerable.SequenceEqual(candidatesList[indexValue].OrderBy(fList => fList),
                    tempCandidateList[indexValue].OrderBy(sList => sList)) == true)
                {
                    //If the lists are equal, leave them as they are.
                }
                else //If they are noty equal create the new list of candidates, by combining 
                {
                    foreach (int candidateValue in candidatesList[indexValue])
                    {
                        if (tempCandidateList[indexValue].Contains(candidateValue))
                        {
                            finalCandidateList.Add(candidateValue);
                        }
                    }
                    candidatesList[indexValue] = finalCandidateList;

                }
            }
        }

        private void CandidateHandling()
        {
            NakedTuples();
            // HiddenColumnSingles();
            //HiddenBlockSingles();
        }

        #endregion 

        #region Nakeds
        //Methods that get all the naked doubles and triples for the differnet regions within the grid. 
        private void NakedTuples()
        {

            // NakedTuplesRow("Row");
            //NakedTuplesColumn();
            //NakedTuplesBlock();
            //NakedTriplesColumn();
            //HiddenTriplesColumn();
        }

        private void NakedTriplesRow()
        {
            //Getting the naked triples for all of the rows wihtin the grid. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);
                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    GetNakedTriples(cadidatesInSingleRow, rowNumber, 0, 0, "row");
                    cadidatesInSingleRow.Clear();
                }
            }
        }

        private void NakedDoubleRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);

                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    GetNakedDoubles(cadidatesInSingleRow, rowNumber, 0, 0, "row");
                    cadidatesInSingleRow.Clear();
                }
            }
        }

        private void NakedDoublesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            //Search through all of the columns. 
            for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= 80; candiateIndexNumber++)
                {
                    if (columnNumber == candiateIndexNumber || candiateIndexNumber % 9 == columnNumber)
                    {
                        cadidatesInSingleColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                GetNakedDoubles(cadidatesInSingleColumn, 0, columnNumber, 0, "column");
                cadidatesInSingleColumn.Clear();
            }
        }

        private void NakedTriplesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            //Search through all of the columns. 
            for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= 80; candiateIndexNumber++)
                {
                    if (columnNumber == candiateIndexNumber || candiateIndexNumber % 9 == columnNumber)
                    {
                        cadidatesInSingleColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                GetNakedTriples(cadidatesInSingleColumn, 0, columnNumber, 0, "column");
                cadidatesInSingleColumn.Clear();
            }
        }

        private void NakedDoublesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber); //Get cell values for that block. 
                    GetNakedDoubles(listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber, 0, "block");
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
        }

        private void NakedTriplesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber); //Get cells values for that block. 
                    GetNakedTriples(listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber, 0, "block");
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
        }

        private void GetNakedDoubles(List<List<int>> cadidatesInSingleRow, int rowNumber, int columnNumber, int blockNumber, string regionTitle)
        {
            //Stores all of the index values for the cells that are not null within the region. 
            List<int> notNullIndexValuesCellsInRow = new List<int>();
            //Gets all the none null cells within the row. 
            for (int cellIndexValue = 0; cellIndexValue <= cadidatesInSingleRow.Count - 1; cellIndexValue++)
            {
                if (cadidatesInSingleRow[cellIndexValue] != null)
                {
                    notNullIndexValuesCellsInRow.Add(cellIndexValue);
                }
            }

            //Cycling through all of the possible double number combination. 
            for (int firstNumber = 1; firstNumber <= 9; firstNumber++)
            {
                for (int secondNumber = 1; secondNumber <= 9; secondNumber++)
                {
                    //List that contains all of the matches. 
                    List<int> matchList = new List<int>();
                    //Going through all of the cells. 
                    for (int indexValueForMatch = 0; indexValueForMatch <= notNullIndexValuesCellsInRow.Count - 1; indexValueForMatch++)
                    {
                        //Making sure all numbers within the combination are unique. 
                        if (firstNumber != secondNumber)
                        {
                            int containsCount = 0;
                            //If the cell contains the current doubles combination. 
                            if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(firstNumber)) { containsCount++; }
                            if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(secondNumber)) { containsCount++; }
                            //This will also match for hidden double.
                            if (containsCount == 2 && cadidatesInSingleRow[notNullIndexValuesCellsInRow[indexValueForMatch]].Count == 2)
                            {
                                //if there is a match, add it to the match list, the match provides the index of the matched cell. 
                                matchList.Add(notNullIndexValuesCellsInRow[indexValueForMatch]);
                            }
                        }
                    }

                    //A naked double has been found. 
                    if (matchList.Count == 2)
                    {
                        //Going through all of fhe none null cells.
                        for (int candidatesIndexValues = 0; candidatesIndexValues <= notNullIndexValuesCellsInRow.Count - 1; candidatesIndexValues++)
                        {
                            //If the cell is one which contains the naked doubles, we do not want to remove the candidates. 
                            bool doubleCell = false;
                            foreach (var value in matchList)
                            {
                                if (value == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                {
                                    doubleCell = true;
                                    break;
                                }
                            }
                            //If the cell is not a naked cell, then possible candidates will be removed from the cell. 
                            if (doubleCell == false)
                            {
                                for (int candidateValueIndex = 0; candidateValueIndex <= cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]].Count - 1; candidateValueIndex++)
                                {
                                    if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == firstNumber || cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == secondNumber)
                                    {
                                        cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]].RemoveAt(candidateValueIndex);
                                        if (regionTitle == "row")
                                        {
                                            candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        else if (regionTitle == "column")
                                        {
                                            candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        //If the region is a block, then further handling. 
                                        else
                                        {
                                            //Method to get the row and column number, using the row number, the index number and the column number 
                                            int coordinateValue = 1;
                                            int startRowNumber = rowNumber - 2;
                                            int startCoulmnNumber = columnNumber - 2;
                                            int actualRowNumber = 0;
                                            int actualColumnNumber = 0;
                                            //Getting row number and column of the current cell. 
                                            for (; startRowNumber <= rowNumber; startRowNumber++)
                                            {
                                                for (; startCoulmnNumber <= columnNumber; startCoulmnNumber++)
                                                {
                                                    if (notNullIndexValuesCellsInRow[candidatesIndexValues] + 1 == coordinateValue)
                                                    {
                                                        actualRowNumber = startRowNumber;
                                                        actualColumnNumber = startCoulmnNumber;
                                                    }
                                                    coordinateValue++;
                                                }
                                                startCoulmnNumber = columnNumber - 2;
                                            }
                                            //Removing candidate from cell. 
                                            candidatesList[9 * actualRowNumber + actualColumnNumber] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        candidateValueIndex = 0;
                                    }
                                }
                            }

                        }
                        matchList.Clear();
                    }
                }
            }
            notNullIndexValuesCellsInRow.Clear();
            cadidatesInSingleRow.Clear();
        }

        private void GetNakedTriples(List<List<int>> cadidatesInSingleRow, int rowNumber, int columnNumber, int blockNumber, string regionTitle)
        {
            List<int> notNullIndexValuesCellsInRow = new List<int>();
            //Gets all the none null cells within the row. 
            for (int cellIndexValue = 0; cellIndexValue <= cadidatesInSingleRow.Count - 1; cellIndexValue++)
            {
                if (cadidatesInSingleRow[cellIndexValue] != null)
                {
                    notNullIndexValuesCellsInRow.Add(cellIndexValue);
                }
            }
            //Going through all of the possible triple combinations. 
            for (int firstNumber = 1; firstNumber <= 9; firstNumber++)
            {
                for (int secondNumber = 1; secondNumber <= 9; secondNumber++)
                {
                    List<int> matchList = new List<int>();
                    for (int thirdNumber = 1; thirdNumber <= 9; thirdNumber++)
                    {
                        for (int indexValueForMatch = 0; indexValueForMatch <= notNullIndexValuesCellsInRow.Count - 1; indexValueForMatch++)
                        {
                            //Making sure all numbers within the combination are unique. 
                            if (firstNumber != secondNumber && firstNumber != thirdNumber && secondNumber != thirdNumber)
                            {
                                int containsCount = 0;
                                if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(firstNumber)) { containsCount++; }
                                if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(secondNumber)) { containsCount++; }
                                if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(thirdNumber)) { containsCount++; }
                                //This will also match for hidden triples. 
                                if (containsCount >= 2 && containsCount == cadidatesInSingleRow[notNullIndexValuesCellsInRow[indexValueForMatch]].Count)
                                {
                                    //if there is a match, add it to the match list, the match provides the index of the matched cell. 
                                    matchList.Add(notNullIndexValuesCellsInRow[indexValueForMatch]);
                                }
                            }
                        }

                        //A naked treble has been found. Need to tes this in isolation. To see if it pics up the naked triple in the 5 row. 
                        if (matchList.Count == 3)
                        {
                            //Removing the candidates from the other cells apart from the naked triple cells. 
                            for (int candidatesIndexValues = 0; candidatesIndexValues <= notNullIndexValuesCellsInRow.Count - 1; candidatesIndexValues++)
                            {
                                bool tripleCell = false;
                                foreach (var value in matchList)
                                {
                                    if (value == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                    {
                                        tripleCell = true;
                                        break;
                                    }
                                }
                                if (tripleCell == false)
                                {
                                    for (int candidateValueIndex = 0; candidateValueIndex <= cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]].Count - 1; candidateValueIndex++)
                                    {
                                        if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == firstNumber || cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == secondNumber || cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == thirdNumber)
                                        {
                                            cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]].RemoveAt(candidateValueIndex);
                                            if (regionTitle == "row")
                                            {
                                                candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            else if (regionTitle == "column")
                                            {
                                                candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            else
                                            {
                                                //Method to get the row and column number, using the row number, the index number and the column number 
                                                int coordinateValue = 1;
                                                int startRowNumber = rowNumber - 2;
                                                int startCoulmnNumber = columnNumber - 2;
                                                int actualRowNumber = 0;
                                                int actualColumnNumber = 0;

                                                for (; startRowNumber <= rowNumber; startRowNumber++)
                                                {
                                                    for (; startCoulmnNumber <= columnNumber; startCoulmnNumber++)
                                                    {
                                                        if (notNullIndexValuesCellsInRow[candidatesIndexValues] + 1 == coordinateValue)
                                                        {
                                                            actualRowNumber = startRowNumber;
                                                            actualColumnNumber = startCoulmnNumber;
                                                        }
                                                        coordinateValue++;
                                                    }
                                                    startCoulmnNumber = columnNumber - 2;
                                                }
                                                //Removing candidate from cell. 
                                                candidatesList[9 * actualRowNumber + actualColumnNumber] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            candidateValueIndex = 0;
                                        }
                                    }
                                }
                            }

                        }
                        matchList.Clear();
                    }
                }
            }
            notNullIndexValuesCellsInRow.Clear();
            cadidatesInSingleRow.Clear();
        }

        #endregion

        #region HiddenDoubles 

        private void HiddenDoublesRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);

                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    GetHiddenDoubles(cadidatesInSingleRow, rowNumber, 0, "row");
                    cadidatesInSingleRow.Clear();
                }
            }
        }
        private void HiddenDoublesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            //Search through all of the columns. 
            for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= 80; candiateIndexNumber++)
                {
                    if (columnNumber == candiateIndexNumber || candiateIndexNumber % 9 == columnNumber)
                    {
                        cadidatesInSingleColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                GetHiddenDoubles(cadidatesInSingleColumn, 0, columnNumber, "column");
                cadidatesInSingleColumn.Clear();
            }
        }
        private void HiddenDoublesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber); //Get cell values for that block. 
                    GetHiddenDoubles(listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber, "block");
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
        }

        private void HiddenTriplesRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);

                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    GetHiddenTriples(cadidatesInSingleRow, rowNumber, 0, "row");
                    cadidatesInSingleRow.Clear();
                }
            }
        }

        private void HiddenTriplesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            //Search through all of the columns. 
            for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= 80; candiateIndexNumber++)
                {
                    if (columnNumber == candiateIndexNumber || candiateIndexNumber % 9 == columnNumber)
                    {
                        cadidatesInSingleColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                GetHiddenTriples(cadidatesInSingleColumn, 0, columnNumber, "column");
                cadidatesInSingleColumn.Clear();
            }
        }

        private void HiddenTriplesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber); //Get cell values for that block. 
                    GetHiddenTriples(listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber, "block");
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
        }

        //The logic for this method is worng. 
        private void GetHiddenDoubles(List<List<int>> cells, int rowNumber, int columnNumber, string regionTitle)
        {
            //Stores all of the index values for the cells that are not null within the region. 
            List<int> notNullIndexValuesCellsInRow = new List<int>();
            //Gets all the none null cells within the row. 
            for (int cellIndexValue = 0; cellIndexValue <= cells.Count - 1; cellIndexValue++)
            {
                if (cells[cellIndexValue] != null)
                {
                    notNullIndexValuesCellsInRow.Add(cellIndexValue);
                }
            }

            //Cycling through all of the possible double number combination. 
            for (int firstNumber = 1; firstNumber <= 9; firstNumber++)
            {
                for (int secondNumber = 1; secondNumber <= 9; secondNumber++)
                {
                    //List that contains all of the matches. 
                    List<int> matchList = new List<int>();
                    List<int> matchDoubleList = new List<int>();
                    for (int indexValueForMatch = 0; indexValueForMatch <= notNullIndexValuesCellsInRow.Count - 1; indexValueForMatch++)
                    {
                        // Making sure all numbers within the combination are unique. 
                        if (firstNumber != secondNumber)
                        {
                            int containsFristCount = 0;
                            int containsSecondCount = 0;
                            //If the cell contains the current doubles combination. 
                            if (cells[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(firstNumber)) { containsFristCount++; }
                            if (cells[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(secondNumber)) { containsSecondCount++; }
                            //This will also match for hidden double.
                            if (containsFristCount == 1 && containsSecondCount == 1)
                            {
                                matchDoubleList.Add(notNullIndexValuesCellsInRow[indexValueForMatch]);
                            }
                            else if (containsFristCount == 1 || containsSecondCount == 1)
                            {
                                //if there is a match, add it to the match list, the match provides the index of the matched cell. 
                                matchList.Add(notNullIndexValuesCellsInRow[indexValueForMatch]);
                            }
                        }
                    }
                    //A hidden double has been found. 
                    if (matchList.Count == 0 && matchDoubleList.Count == 2)
                    {
                        List<List<int>> test = candidatesList;
                        //Going through all of fhe none null cells.
                        for (int candidatesIndexValues = 0; candidatesIndexValues <= notNullIndexValuesCellsInRow.Count - 1; candidatesIndexValues++)
                        {
                            //If the cell is one which contains the naked doubles, we do not want to remove the candidates. 
                            bool doubleCell = false;
                            foreach (var value in matchDoubleList)
                            {
                                if (value == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                {
                                    doubleCell = true;
                                    break;
                                }
                            }

                            //Removing all of the other candidates in the cells that are  double or triple. 
                            if (doubleCell == true)
                            {
                                for (int candidateValueIndex = 0; candidateValueIndex <= cells[notNullIndexValuesCellsInRow[candidatesIndexValues]].Count - 1; candidateValueIndex++)
                                {
                                    if (cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] != firstNumber && cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] != secondNumber)
                                    {
                                        cells[notNullIndexValuesCellsInRow[candidatesIndexValues]].RemoveAt(candidateValueIndex);
                                        if (regionTitle == "row")
                                        {
                                            candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        else if (regionTitle == "column")
                                        {
                                            candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        //If the region is a block, then further handling. 
                                        else
                                        {
                                            //Method to get the row and column number, using the row number, the index number and the column number 
                                            int coordinateValue = 1;
                                            int startRowNumber = rowNumber - 2;
                                            int startCoulmnNumber = columnNumber - 2;
                                            int actualRowNumber = 0;
                                            int actualColumnNumber = 0;
                                            //Getting row number and column of the current cell. 
                                            for (; startRowNumber <= rowNumber; startRowNumber++)
                                            {
                                                for (; startCoulmnNumber <= columnNumber; startCoulmnNumber++)
                                                {
                                                    if (notNullIndexValuesCellsInRow[candidatesIndexValues] + 1 == coordinateValue)
                                                    {
                                                        actualRowNumber = startRowNumber;
                                                        actualColumnNumber = startCoulmnNumber;
                                                    }
                                                    coordinateValue++;
                                                }
                                                startCoulmnNumber = columnNumber - 2;
                                            }
                                            //Removing candidate from cell. 
                                            candidatesList[9 * actualRowNumber + actualColumnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        candidateValueIndex = 0;
                                    }
                                }
                            }

                        }
                        matchList.Clear();
                        matchDoubleList.Clear();
                    }
                }
            }
            notNullIndexValuesCellsInRow.Clear();
            cells.Clear();
        }

        //The logic is wrong, it is picking up values that should not be there. It is picking up, things with 2 items in each which is not correct. May need to 
        private void GetHiddenTriples(List<List<int>> cells, int rowNumber, int columnNumber, string regionTitle)
        {
            List<int> notNullIndexValuesCellsInRow = new List<int>();
            //Gets all the none null cells within the row. 
            for (int cellIndexValue = 0; cellIndexValue <= cells.Count - 1; cellIndexValue++)
            {
                if (cells[cellIndexValue] != null)
                {
                    notNullIndexValuesCellsInRow.Add(cellIndexValue);
                }
            }
            //Going through all of the possible triple combinations. 
            for (int firstNumber = 1; firstNumber <= 9; firstNumber++)
            {
                for (int secondNumber = 1; secondNumber <= 9; secondNumber++)
                {
                    List<int> matchList = new List<int>();
                    List<int> tripleMatchList = new List<int>();
                    for (int thirdNumber = 1; thirdNumber <= 9; thirdNumber++)
                    {
                        for (int indexValueForMatch = 0; indexValueForMatch <= notNullIndexValuesCellsInRow.Count - 1; indexValueForMatch++)
                        {
                            //Making sure all numbers within the combination are unique. 
                            if (firstNumber != secondNumber && firstNumber != thirdNumber && secondNumber != thirdNumber)
                            {
                                int containsCount = 0;
                                if (cells[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(firstNumber)) { containsCount++; }
                                if (cells[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(secondNumber)) { containsCount++; }
                                if (cells[notNullIndexValuesCellsInRow[indexValueForMatch]].Contains(thirdNumber)) { containsCount++; }
                                //This will also match for hidden triples. 
                                if (containsCount >= 2)
                                {
                                    //if there is a match, add it to the match list, the match provides the index of the matched cell. 
                                    tripleMatchList.Add(notNullIndexValuesCellsInRow[indexValueForMatch]);
                                }
                                else if (containsCount >= 1)
                                {
                                    matchList.Add(notNullIndexValuesCellsInRow[indexValueForMatch]);
                                }
                            }
                        }
                        //A naked treble has been found. Need to tes this in isolation. To see if it pics up the naked triple in the 5 row. 
                        if (tripleMatchList.Count == 3 && matchList.Count == 0)
                        {
                            //Removing the candidates from the other cells apart from the naked triple cells. 
                            for (int candidatesIndexValues = 0; candidatesIndexValues <= notNullIndexValuesCellsInRow.Count - 1; candidatesIndexValues++)
                            {
                                bool tripleCell = false;
                                foreach (var value in tripleMatchList)
                                {
                                    if (value == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                    {
                                        tripleCell = true;
                                        break;
                                    }
                                }
                                if (tripleCell == true)
                                {
                                    for (int candidateValueIndex = 0; candidateValueIndex <= cells[notNullIndexValuesCellsInRow[candidatesIndexValues]].Count - 1; candidateValueIndex++)
                                    {
                                        if (cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] != firstNumber && cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] != secondNumber && cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] != thirdNumber)
                                        {
                                            cells[notNullIndexValuesCellsInRow[candidatesIndexValues]].RemoveAt(candidateValueIndex);
                                            if (regionTitle == "row")
                                            {
                                                candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            else if (regionTitle == "column")
                                            {
                                                candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            //If the region is a block, then further handling. 
                                            else
                                            {
                                                //Method to get the row and column number, using the row number, the index number and the column number 
                                                int coordinateValue = 1;
                                                int startRowNumber = rowNumber - 2;
                                                int startCoulmnNumber = columnNumber - 2;
                                                int actualRowNumber = 0;
                                                int actualColumnNumber = 0;
                                                //Getting row number and column of the current cell. 
                                                for (; startRowNumber <= rowNumber; startRowNumber++)
                                                {
                                                    for (; startCoulmnNumber <= columnNumber; startCoulmnNumber++)
                                                    {
                                                        if (notNullIndexValuesCellsInRow[candidatesIndexValues] + 1 == coordinateValue)
                                                        {
                                                            actualRowNumber = startRowNumber;
                                                            actualColumnNumber = startCoulmnNumber;
                                                        }
                                                        coordinateValue++;
                                                    }
                                                    startCoulmnNumber = columnNumber - 2;
                                                }
                                                //Removing candidate from cell. 
                                                candidatesList[9 * actualRowNumber + actualColumnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            candidateValueIndex = 0;
                                        }
                                    }
                                }
                            }
                        }
                        matchList.Clear();
                        tripleMatchList.Clear();
                    }
                }
            }
            notNullIndexValuesCellsInRow.Clear();
            cells.Clear();
        }

        private void RemoveCandidates(List<int> notNullIndexValuesCellsInRow, List<int> matchList, List<List<int>> cells, int rowNumber, int columnNumber, string regionTitle, int firstNumber, int secondNumber, int thirdNumber)
        {
            //Going through all of fhe none null cells.
            for (int candidatesIndexValues = 0; candidatesIndexValues <= notNullIndexValuesCellsInRow.Count - 1; candidatesIndexValues++)
            {
                //If the cell is one which contains the naked doubles, we do not want to remove the candidates. 
                bool doubleCell = false;
                foreach (var value in matchList)
                {
                    if (value == notNullIndexValuesCellsInRow[candidatesIndexValues])
                    {
                        doubleCell = true;
                        break;
                    }
                }
                //If the cell is not a naked cell, then possible candidates will be removed from the cell. 
                if (doubleCell == false)
                {
                    for (int candidateValueIndex = 0; candidateValueIndex <= cells[notNullIndexValuesCellsInRow[candidatesIndexValues]].Count - 1; candidateValueIndex++)
                    {
                        if (cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == firstNumber || cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == secondNumber || cells[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == thirdNumber)
                        {
                            cells[notNullIndexValuesCellsInRow[candidatesIndexValues]].RemoveAt(candidateValueIndex);
                            if (regionTitle == "row")
                            {
                                candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                            }
                            else if (regionTitle == "column")
                            {
                                candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                            }
                            //If the region is a block, then further handling. 
                            else
                            {
                                //Method to get the row and column number, using the row number, the index number and the column number 
                                int coordinateValue = 1;
                                int startRowNumber = rowNumber - 2;
                                int startCoulmnNumber = columnNumber - 2;
                                int actualRowNumber = 0;
                                int actualColumnNumber = 0;
                                //Getting row number and column of the current cell. 
                                for (; startRowNumber <= rowNumber; startRowNumber++)
                                {
                                    for (; startCoulmnNumber <= columnNumber; startCoulmnNumber++)
                                    {
                                        if (notNullIndexValuesCellsInRow[candidatesIndexValues] + 1 == coordinateValue)
                                        {
                                            actualRowNumber = startRowNumber;
                                            actualColumnNumber = startCoulmnNumber;
                                        }
                                        coordinateValue++;
                                    }
                                    startCoulmnNumber = columnNumber - 2;
                                }
                                //Removing candidate from cell. 
                                candidatesList[9 * actualRowNumber + actualColumnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                            }
                            candidateValueIndex = 0;
                        }
                    }
                }

            }
        }

        #endregion 

        #region Hiddens
        //Method that calls all of the hidden methods, to see if ther are any hidden values in the grid. 
        private void HiddenSingles()
        {
            bool hiddenRowBool = false;
            bool hiddenColumnBool = false;
            bool hiddenBlockBool = false;
            hiddenRowBool = HiddenRowSingles();
            //hiddenColumnBool = HiddenColumnSingles();
            //hiddenBlockBool = HiddenBlockSingles();

            //If there is hidden values, then recurse and try the naked singles method. 
            if (hiddenBlockBool == true || hiddenColumnBool == true || hiddenRowBool == true)
            {
                SolveSudokRuleBased();
            }
        }

        private bool HiddenColumnSingles()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheColumn = new List<List<int>>();
            bool hiddenColumnBool = false;

            //Search through all of the columns. 
            for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
            {
                //Get all the values within that column 
                for (int candiateIndexNumber = 0; candiateIndexNumber <= 80; candiateIndexNumber++)
                {
                    if (columnNumber == candiateIndexNumber || candiateIndexNumber % 9 == columnNumber)
                    {
                        listOfCanidadtesForEachCellWithinTheColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                //Seeing if there is any hidden column singles, if there is input them into the grid and return as true; 
                hiddenColumnBool = HiddenSinglesGeneric("column", listOfCanidadtesForEachCellWithinTheColumn, 0, columnNumber);
                listOfCanidadtesForEachCellWithinTheColumn.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
            }
            return hiddenColumnBool;
        }

        private bool HiddenBlockSingles()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            bool hiddenBlockBool = false;

            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber);

                    hiddenBlockBool = HiddenSinglesGeneric("block", listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber);

                    listOfCanidadtesForEachCellWithinTheBlock.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
                }
            }
            return hiddenBlockBool;
        }

        private bool HiddenRowSingles()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheRow = new List<List<int>>();

            int rowNumber = 0;
            bool hiddenRowBool = false;

            //Going through all if the candidate cells. 
            for (int candidateIndexNumber = 0; candidateIndexNumber <= candidatesList.Count - 1; candidateIndexNumber++)
            {
                //Adding values to the row.
                listOfCanidadtesForEachCellWithinTheRow.Add(candidatesList[candidateIndexNumber]);
                //When the end of a row has been reached. 
                if (candidateIndexNumber % 9 == 8 || candidateIndexNumber == 8)
                {
                    hiddenRowBool = HiddenSinglesGeneric("row", listOfCanidadtesForEachCellWithinTheRow, rowNumber, 0);
                    listOfCanidadtesForEachCellWithinTheRow.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
                    rowNumber++; //Increasing the row number
                }
            }
            return hiddenRowBool;
        }

        //May need to implement this method at somepoint. 
        private bool HiddenSinglesGeneric(string region, List<List<int>> listOfCells, int rowNumber, int columnNumber)
        {
            List<int> notNullIndexList = new List<int>();
            List<int> individualNumbers = new List<int>();
            List<int> valuesUsed = new List<int>();
            bool hiddenBool = false;
            //Removing all null values from the candidate lists in the column. 
            for (int indexValue = 0; indexValue <= listOfCells.Count - 1; indexValue++)
            {
                if (listOfCells[indexValue] != null)
                {
                    notNullIndexList.Add(indexValue);
                }
            }

            //For each non null cell within the row. 
            foreach (var firstIndexNumber in notNullIndexList)
            {
                //Get all the candidates from tha cell. 
                foreach (var listValue in listOfCells[firstIndexNumber])
                {
                    //if the indivdual numbers list already contains 
                    if (individualNumbers.Contains(listValue))
                    {
                        //Remove that value from the indivdual numbers list. 
                        for (int valueToRemove = 0; valueToRemove <= individualNumbers.Count - 1; valueToRemove++)
                        {
                            if (individualNumbers[valueToRemove] == listValue)
                            {
                                individualNumbers.RemoveAt(valueToRemove);
                                valuesUsed.Add(listValue); //Making sure it is not added back to the list. 
                            }
                        }
                    }
                    else //If the list does not contain the number. 
                    {
                        bool valuesUsedBool = false; //Determines whether the number that is not in the list has been used before. 
                        foreach (var alreadyUsed in valuesUsed)
                        {
                            if (listValue == alreadyUsed)
                            {
                                valuesUsedBool = true;
                            }
                        }
                        //If the candidate has not be used before add it to the list. 
                        if (valuesUsedBool == false)
                        {
                            individualNumbers.Add(listValue);
                        }
                        else
                        {
                            valuesUsedBool = false;
                        }
                    }
                }
            }

            if (individualNumbers.Count >= 1)
            {
                //search all of the candidates in each cell within the row, if a candidate value matched then the value must be that within the cell, as it is a single hidden value. 
                for (int candidateValues = 0; candidateValues <= notNullIndexList.Count - 1; candidateValues++)
                {
                    //Going through any of the hidden values within the row. 
                    foreach (var indivdualValue in individualNumbers)
                    {
                        foreach (var valueInCell in listOfCells[notNullIndexList[candidateValues]])
                        {
                            //If the hidden value i contained within that cell, then that must be its value. 
                            if (indivdualValue == valueInCell)
                            {
                                if (region == "block")
                                {
                                    //Method to get the row and column number, using the row number, the index number and the column number 
                                    int coordinateValue = 1;
                                    int startRowNumber = rowNumber - 2;
                                    int startCoulmnNumber = columnNumber - 2;
                                    int actualRowNumber = 0;
                                    int actualColumnNumber = 0;

                                    for (; startRowNumber <= rowNumber; startRowNumber++)
                                    {
                                        for (; startCoulmnNumber <= columnNumber; startCoulmnNumber++)
                                        {
                                            if (notNullIndexList[candidateValues] + 1 == coordinateValue)
                                            {
                                                actualRowNumber = startRowNumber;
                                                actualColumnNumber = startCoulmnNumber;
                                            }
                                            coordinateValue++;
                                        }
                                        startCoulmnNumber = columnNumber - 2;

                                    }
                                    hiddenBool = true;
                                    //Updating the grid and corresponding candidates. 
                                    staticNumbers[actualRowNumber, actualColumnNumber] = indivdualValue;
                                    sudokuPuzzleMultiExample[actualRowNumber, actualColumnNumber] = indivdualValue;
                                    candidatesList[9 * actualRowNumber + actualColumnNumber] = null;
                                    break;
                                }
                                else if (region == "column")
                                {
                                    hiddenBool = true;
                                    //Updating the grid and corresponding candidates. 
                                    staticNumbers[notNullIndexList[candidateValues], columnNumber] = indivdualValue;
                                    sudokuPuzzleMultiExample[notNullIndexList[candidateValues], columnNumber] = indivdualValue;
                                    candidatesList[9 * notNullIndexList[candidateValues] + columnNumber] = null;
                                    break;
                                }
                                else
                                {
                                    hiddenBool = true;
                                    //Updating the grid and corresponding candidates. 
                                    staticNumbers[rowNumber, notNullIndexList[candidateValues]] = indivdualValue;
                                    sudokuPuzzleMultiExample[rowNumber, notNullIndexList[candidateValues]] = indivdualValue;
                                    candidatesList[9 * rowNumber + notNullIndexList[candidateValues]] = null;
                                    break;
                                }

                            }
                        }
                    }
                }

            }
            listOfCells.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
            notNullIndexList.Clear();
            valuesUsed.Clear();
            return hiddenBool;
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
            //if (emptyCellCount == 0)
            //{
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    Console.Write(sudokuPuzzleMultiExample[i, j]);
                }
            }
            Console.WriteLine();
            //}

            //Get the next blank cell. 
            //Cycle through all of the cells until an empty one. 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    //If the cell is empty and the cell is not a static number. 
                    if (sudokuPuzzleMultiExample[i, j] == 0 && staticNumbers[i, j] == 0)
                    {
                        //All of the check to see what numbers are valid for that particular square. 
                        validNUmbersInRow = checkRow(sudokuPuzzleMultiExample, i, j);
                        validNumbersInColumn = checkColumn(sudokuPuzzleMultiExample, i, j);
                        validNumbersInBlock = checkBlock(sudokuPuzzleMultiExample, i, j);
                        List<int> validNumbers = GetValidNumbers(validNumbersInColumn, validNUmbersInRow, validNumbersInBlock);

                        foreach (var validNumber in validNumbers)
                        {
                            ChangeGrid(i, j, validNumber);
                            SolveUsingRecursiveBactracking();
                        }
                        ReverseGrid(i, j, 0);
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

        #region Stoatchi Search ALgorithm 

        //Enter random numbers into the grid,asses the totals of each row. 
        private void SolveStoaticSearch(int[,] puzzle, List<int> tempValidNumberRegion)
        {
            Random random = new Random();
            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    //Getting all the valid numbers in that row. 
                    for (int listValue = 0; listValue <= tempValidNumberRegion.Count - 1; listValue++)
                    {
                        //Gets all the values that are avliable wihtin that row. 
                        if (tempValidNumberRegion[listValue] == puzzle[rowNumber, columnNumber])
                        {
                            tempValidNumberRegion.RemoveAt(listValue);
                        }
                    }
                }


                for (int j = 0; j <= 8; j++)
                {
                    if (puzzle[rowNumber, j] == 0)
                    {
                        int randomNumber = tempValidNumberRegion[random.Next(tempValidNumberRegion.Count)];

                        puzzle[rowNumber, j] = randomNumber;
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
            bool values = GetColumnTotals(puzzle);
        }


        private bool GetColumnTotals(int[,] puzzle)
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


        #endregion 

        #region Constraint problem 

        //Method takes way to long due to random nature, and the amount of possibilities this creates. 

        private void SolveConstraintsProblem(int[,] puzzle, List<int> tempValidNumberRegion)
        {
            bool columns = false;
            bool minigrids = false;
            Random random = new Random();
            bool solved = false;

            while (columns == false || minigrids == false)
            {
                tempValidNumberRegion = validNumbersForRegion;
                //Get the starting totals of regions 
                columns = GetColumnTotalValues(puzzle);
                minigrids = GetBlockTotalValues(puzzle);

                //Going through all of the cells and entering random values. 
                for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
                {
                    for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                    {
                        for (int listValue = 0; listValue <= tempValidNumberRegion.Count - 1; listValue++)
                        {
                            //Gets all the values that are avliable wihtin that row. 
                            if (tempValidNumberRegion[listValue] == puzzle[rowNumber, columnNumber])
                            {
                                tempValidNumberRegion.RemoveAt(listValue);
                            }
                        }
                    }
                    for (int j = 0; j <= 8; j++)
                    {
                        if (puzzle[rowNumber, j] == 0)
                        {
                            int randomNumber = tempValidNumberRegion[random.Next(tempValidNumberRegion.Count)];

                            puzzle[rowNumber, j] = randomNumber;
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

        #region Backtracking Second Attempt

        public void BacktrackingSolve(int previousNumber)
        {
            //Remove ints and change to statics, general fields not in local variables. Create evetything outside of the recursive call. Not using memory ineffinctly. 
            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    if (sudokuPuzzleMultiExample[rowNumber, columnNumber] == 0)
                    {
                        //All of the check to see what numbers are valid for that particular square. 
                        validNUmbersInRow = checkRow(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                        validNumbersInColumn = checkColumn(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                        validNumbersInBlock = checkBlock(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                        validNumbersInCell = GetValidNumbers(validNumbersInColumn, validNUmbersInRow, validNumbersInBlock);

                        //if(sudokuPuzzleMultiExample[0,6]==9 && sudokuPuzzleMultiExample[0,2]==4 && sudokuPuzzleMultiExample[0,3]==6  )
                        //{

                        //}
                        //if(sudokuPuzzleMultiExample[0,5]==0)
                        //{

                        //}
                        //if(rowNumber ==0)
                        //{

                        //}


                        if (validNumbersInCell.Count == 0)
                        {
                            //for (int i = 0; i <= 8; i++)
                            //{
                            //    for (int j = 0; j <= 8; j++)
                            //    {
                            //        Console.Write(sudokuPuzzleMultiExample[i, j]);
                            //    }
                            //}
                            //Console.WriteLine();
                            // Get the previous value that is not a static number

                            if (columnNumber == 0)
                            {
                                rowNumber = rowNumber - 1;
                                columnNumber = 8;
                            }
                            else
                            {
                                columnNumber = columnNumber - 1;
                            }

                            for (int i = rowNumber; i >= 0; i--)
                            {
                                for (int j = columnNumber; j >= 0; j--)
                                {
                                    int tempPreviousNumber = 0;
                                    if (staticNumbers[i, j] == 0)
                                    {
                                        tempPreviousNumber = sudokuPuzzleMultiExample[i, j];
                                        sudokuPuzzleMultiExample[i, j] = 0;
                                        BacktrackingSolve(tempPreviousNumber);
                                    }
                                }
                                rowNumber = rowNumber - 1;
                                columnNumber = 8;
                            }
                        }
                        else
                        {
                            if (previousNumber == 0)
                            {
                                sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbers[0];
                            }

                            else
                            {
                                //Need to back track further. 
                                if (previousNumber == validNumbers[validNumbersInCell.Count - 1])
                                {
                                    if (columnNumber == 0)
                                    {
                                        rowNumber = rowNumber - 1;
                                        columnNumber = 8;
                                    }
                                    else
                                    {
                                        columnNumber = columnNumber - 1;
                                    }

                                    for (int i = rowNumber; i >= 0; i--)
                                    {
                                        for (int j = columnNumber; j >= 0; j--)
                                        {
                                            int tempPreviousNumber = 0;
                                            if (staticNumbers[i, j] == 0)
                                            {
                                                tempPreviousNumber = sudokuPuzzleMultiExample[i, j];
                                                sudokuPuzzleMultiExample[i, j] = 0;

                                                BacktrackingSolve(tempPreviousNumber);
                                            }
                                        }
                                        rowNumber = rowNumber - 1;
                                        columnNumber = 8;
                                    }
                                }
                                else
                                {
                                    //Maybe  something wrong with this statement 
                                    foreach (var validNumber in validNumbers)
                                    {
                                        //If the valid number has already been used in the cell, then the next number will need to be inserted or the backtracking will need to go back further. 
                                        if (validNumber == previousNumber || validNumber < previousNumber)
                                        {

                                        }
                                        else //Set the valid number to the cell, and submit the new grid. 
                                        {
                                            sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumber;
                                            previousNumber = 0;
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            bool solved = true;
        }

        #endregion 

        #region Backtracking

        public void solve(int previousNumber)
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
                                solve(previousNumber);
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
                                        solve(0);
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
                                solve(previousNumber);
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

        #endregion

        #region Methods for getting values out of blocks, rows and columns, by passing values. 

        private List<List<int>> getSudokuValuesInBox(int rowNumber, int columnNumber)
        {
            List<List<int>> numbersPositionsInBlock = new List<List<int>>();

            int candidateValueNumber = 9 * rowNumber + columnNumber;
            for (int rowRemove = 0; rowRemove <= 2; rowRemove++)
            {
                for (int columnRemove = 0; columnRemove <= 2; columnRemove++)
                {
                    numbersPositionsInBlock.Add(candidatesList[candidateValueNumber - (columnRemove + rowRemove * 9)]);
                }
            }

            //Reversing the list so it is in the correct order. 
            numbersPositionsInBlock.Reverse();

            return numbersPositionsInBlock;

        }

        #endregion

        #region Methods to check valid values 
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

        #region Iterative Method 

        public void IterativeMethod(int previousNumber)
        {
            for (int rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                for (int columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    if (sudokuPuzzleMultiExample[rowNumber, columnNumber] == 0)
                    {
                        //All of the check to see what numbers are valid for that particular square. 
                        List<int> validNUmbersInRow = checkRow(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                        List<int> validNumbersInColumn = checkColumn(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                        List<int> validNumbersInBlock = checkBlock(sudokuPuzzleMultiExample, rowNumber, columnNumber);
                        List<int> validNumbers = GetValidNumbers(validNumbersInColumn, validNUmbersInRow, validNumbersInBlock);

                        //if(sudokuPuzzleMultiExample[0,6]==9 && sudokuPuzzleMultiExample[0,2]==4 && sudokuPuzzleMultiExample[0,3]==6  )
                        //{

                        //}
                        //if(sudokuPuzzleMultiExample[0,5]==0)
                        //{

                        //}
                        //if(rowNumber ==0)
                        //{

                        //}


                        if (validNumbers.Count == 0)
                        {
                            //for (int i = 0; i <= 8; i++)
                            //{
                            //    for (int j = 0; j <= 8; j++)
                            //    {
                            //        Console.Write(sudokuPuzzleMultiExample[i, j]);
                            //    }
                            //}
                            //Console.WriteLine();
                            //Get the previous value that is not a static number 

                            if (columnNumber == 0)
                            {
                                rowNumber = rowNumber - 1;
                                columnNumber = 8;
                            }
                            else
                            {
                                columnNumber = columnNumber - 1;
                            }

                            for (int i = rowNumber; i >= 0; i--)
                            {
                                for (int j = columnNumber; j >= 0; j--)
                                {
                                    int tempPreviousNumber = 0;
                                    if (staticNumbers[i, j] == 0)
                                    {
                                        tempPreviousNumber = sudokuPuzzleMultiExample[i, j];
                                        sudokuPuzzleMultiExample[i, j] = 0;
                                        i = 0;
                                        j = 0;
                                        return;
                                    }
                                }
                                rowNumber = rowNumber - 1;
                                columnNumber = 8;
                            }
                        }
                        else
                        {
                            if (previousNumber == 0)
                            {
                                sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbers[0];
                            }

                            else
                            {
                                //Need to back track further. 
                                if (previousNumber == validNumbers[validNumbers.Count - 1])
                                {
                                    if (columnNumber == 0)
                                    {
                                        rowNumber = rowNumber - 1;
                                        columnNumber = 8;
                                    }
                                    else
                                    {
                                        columnNumber = columnNumber - 1;
                                    }

                                    for (int i = rowNumber; i >= 0; i--)
                                    {
                                        for (int j = columnNumber; j >= 0; j--)
                                        {
                                            int tempPreviousNumber = 0;
                                            if (staticNumbers[i, j] == 0)
                                            {
                                                tempPreviousNumber = sudokuPuzzleMultiExample[i, j];
                                                sudokuPuzzleMultiExample[i, j] = 0;

                                                i = 0;
                                                j = 0;
                                                return;
                                            }
                                        }
                                        rowNumber = rowNumber - 1;
                                        columnNumber = 8;
                                    }
                                }
                                else
                                {
                                    //Maybe  something wrong with this statement 
                                    foreach (var validNumber in validNumbers)
                                    {
                                        //If the valid number has already been used in the cell, then the next number will need to be inserted or the backtracking will need to go back further. 
                                        if (validNumber == previousNumber || validNumber < previousNumber)
                                        {

                                        }
                                        else //Set the valid number to the cell, and submit the new grid. 
                                        {
                                            sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumber;
                                            previousNumber = 0;
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }

            bool solved = true;
        }

        #endregion
    }
}
