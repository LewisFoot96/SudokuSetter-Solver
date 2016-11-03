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

        //Goes into infinite loop on examples 9 still doesnt work. the only one. 
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
        int[] sudokuPUzzleWithNakedTuple = new int[] { 4, 0, 0, 2, 7, 0, 6, 0, 0, 7, 9, 8, 1, 5, 6, 2, 3, 4, 0, 2, 0, 8, 4, 0, 0, 0, 7, 2, 3, 7, 4, 6, 8, 9, 5, 1, 8, 4, 9, 5, 3, 1, 7, 2, 6, 5, 6, 1, 7, 9, 2, 8, 4, 3, 0, 8, 2, 0, 1, 5, 4, 7, 9, 0, 7, 0, 0, 2, 4, 3, 0, 0, 0, 0, 4, 0, 8, 7, 0, 0, 2 };
        int[] sudokuHiddenSinglesExample = new int[] { 7, 0, 0, 2, 0, 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 6, 3, 0, 0, 0, 4, 0, 0, 9, 0, 0, 0, 6, 9, 1, 0, 0, 0, 0, 2, 3, 0, 0, 2, 1, 8, 0, 0, 0, 6, 0, 0, 0, 0, 6, 0, 5, 0, 0, 4, 0, 3, 0, 7, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 1, 0, 5, 0, 8, 0, 0, 0, 0, 0 };
        int[] sudokuBlockHiddenSingleExample = new int[] { 0, 0, 0, 0, 0, 0, 5, 0, 0, 1, 6, 0, 9, 0, 0, 0, 0, 0, 0, 0, 9, 0, 6, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 0, 0, 0, 2, 0, 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 5, 0, 0, 0, 2, 0, 8, 9, 0, 0, 0, 0, 1, 0, 2, 5, 0, 0, 3, 0, 7, 0, 0, 1, 0, 0, 0, 0, 9 };
        int[] sudokuNakedDoublesColumnBlockExample = new int[] { 0, 8, 0, 0, 9, 0, 0, 3, 0, 0, 3, 0, 0, 0, 0, 0, 6, 9, 9, 0, 2, 0, 6, 3, 1, 5, 8, 0, 2, 0, 8, 0, 4, 5, 9, 0, 8, 5, 1, 9, 0, 7, 0, 4, 6, 3, 9, 4, 6, 0, 5, 8, 7, 0, 5, 6, 3, 0, 4, 0, 9, 8, 7, 2, 0, 0, 0, 0, 0, 0, 1, 5, 0, 1, 0, 0, 5, 0, 0, 2, 0 };

        #endregion

        #region Gloabl Variables 
        //array that stores the static numbers that are within the puzzle. 
        int[,] staticNumbers = new int[9, 9];
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
            SolveUsingRecursiveBactracking();
            //BacktrackingSolve(0);
            //solve(sudokuPuzzleMultiExample, 0);
            //SolveConstraintsProblem(sudokuPuzzleMultiExample, validNumbersForRegion);
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
                    sudokuPuzzleMultiExample[i, j] = sudokuPuzzleExample[singleArrayValue];
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
            //CandidateHandling();
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

        private void NakedTuples()
        {

            NakedTuplesRow("Row");
            //NakedTuplesColumn();
            //NakedTuplesBlock();
        }

        private void NakedTuplesRow(string rowOrColumn)
        {
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            List<int> indexValue = new List<int>();
            List<int> notNullIndexValuesCellsInRow = new List<int>(); 
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);
                
               
                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    //Gets all the none null cells within the row. 
                    for (int cellIndexValue = 0; cellIndexValue <= cadidatesInSingleRow.Count - 1; cellIndexValue++)
                    {
                        if (cadidatesInSingleRow[cellIndexValue] != null)
                        {
                            notNullIndexValuesCellsInRow.Add(cellIndexValue);
                        }
                    }

                    //Loop thay compares all of the cells within the row. 
                    for(int firstIndexValueOfValidCell = 0; firstIndexValueOfValidCell<=notNullIndexValuesCellsInRow.Count-1;firstIndexValueOfValidCell++)
                    {
                        for (int secondIndexValueOfValidCell = 0; secondIndexValueOfValidCell <= cadidatesInSingleRow.Count - 1; secondIndexValueOfValidCell++)
                        {
                            //Need to test this. 
                            //http://stackoverflow.com/questions/12376437/how-to-check-list-a-contains-any-value-from-list-b
                           //if( cadidatesInSingleRow[notNullIndexValuesCellsInRow[firstIndexValueOfValidCell]].Intersect(cadidatesInSingleRow[notNullIndexValuesCellsInRow[secondIndexValueOfValidCell]]).Any() && firstIndexValueOfValidCell !=secondIndexValueOfValidCell)
                            //{

                            //}
                        }
                    }


                    //This all should be using the list within the method, not the actual candidate list, as this adds complictions. 
                    //Seeing of there are any naked tuples within the row. 
                    for (int firstIndexValue = rowNumber - 8; firstIndexValue <= rowNumber; firstIndexValue++)
                    {
                        //Gets all of the index values for the naked tuples, so candidates can be removed. 
                        for (int secondIndexValue = rowNumber - 8; secondIndexValue <= rowNumber; secondIndexValue++)
                        {
                            if (candidatesList[firstIndexValue] != null && candidatesList[secondIndexValue] != null)
                            {
                                //This comapres 2 lists to see if thet are equal 
                                //http://stackoverflow.com/questions/22173762/check-if-two-lists-are-equal
                                if (Enumerable.SequenceEqual(candidatesList[firstIndexValue].OrderBy(fList => fList),
                                    candidatesList[secondIndexValue].OrderBy(sList => sList)) == true && firstIndexValue != secondIndexValue)
                                {
                                    indexValue.Add(firstIndexValue);
                                    indexValue.Add(secondIndexValue);
                                }
                            }
                        }

                        if (indexValue.Count != 0 && candidatesList[indexValue[0]].Count <= indexValue.Count)
                        {
                            //Getting all of the naked values. 
                            List<int> nakedCandidates = candidatesList[firstIndexValue];
                            bool isIndexNumber = false;

                            //Removing naked values from other cells within the grid. 
                            for (int indexValueOfListInRow = rowNumber - 8; indexValueOfListInRow <= rowNumber; indexValueOfListInRow++)
                            {
                                isIndexNumber = false;

                                foreach (int nakedValueIndexNumber in indexValue)
                                {
                                    //If the cell is equal to one of the ones that have the naked tuples in, then do not remove the cnaidates from the cell. 
                                    if (indexValueOfListInRow == nakedValueIndexNumber)
                                    {
                                        isIndexNumber = true;
                                        break;
                                    }
                                }
                                bool indexNumberBool = false;


                                //If the cell is not part of the naked tuples, then removed the candidates, if any from the cell. 
                                if (isIndexNumber != true && candidatesList[indexValueOfListInRow] != null)
                                {
                                    List<int> indexesNotNull = new List<int>();

                                    //Removing null values from list 
                                    for (int candidateIndexValue = 0; candidateIndexValue <= cadidatesInSingleRow.Count - 1; candidateIndexValue++)
                                    {
                                        if (cadidatesInSingleRow[candidateIndexValue] != null)
                                        {
                                            indexesNotNull.Add(candidateIndexValue);
                                        }
                                    }

                                    foreach (int notNullIndexValue in indexesNotNull)
                                    {
                                        //Check to see if the currenetly handled cell contains the naked tuples, then it should not be changed. 
                                        foreach (var indexValueCheck in indexValue)
                                        {
                                            if (rowNumber - indexValueCheck - 1 == notNullIndexValue)
                                            {
                                                indexNumberBool = true;
                                            }
                                        }
                                        if (indexNumberBool == false)
                                        {
                                            foreach (int nakedCandidateNumber in nakedCandidates)
                                            {
                                                //This need to be a for loop. 
                                                for (int indexNumberOfNotNakedCell = 0; indexNumberOfNotNakedCell <= cadidatesInSingleRow[notNullIndexValue].Count; indexNumberOfNotNakedCell++)
                                                {
                                                    foreach (var numberInList in cadidatesInSingleRow[notNullIndexValue])
                                                    {
                                                        if (numberInList == nakedCandidateNumber)
                                                        {
                                                            //Removing candidate from cell. 
                                                            cadidatesInSingleRow[notNullIndexValue].RemoveAt(indexNumberOfNotNakedCell);
                                                            candidatesList[rowNumber - 8 - notNullIndexValue] = cadidatesInSingleRow[notNullIndexValue];
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        indexNumberBool = false;
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

        //This method seems to be complete but need testing before use within the program. 
        private void NakedTuplesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            List<int> indexValue = new List<int>();

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
                //Seeing of there are any naked tuples within the row. 
                for (int firstIndexValue = 0; firstIndexValue <= cadidatesInSingleColumn.Count - 1; firstIndexValue++)
                {
                    //Gets all of the index values for the naked tuples, so candidates can be removed. 
                    for (int secondIndexValue = 0; secondIndexValue <= cadidatesInSingleColumn.Count - 1; secondIndexValue++)
                    {
                        if (cadidatesInSingleColumn[firstIndexValue] != null && cadidatesInSingleColumn[secondIndexValue] != null)
                        {
                            //This comapres 2 lists to see if thet are equal 
                            //http://stackoverflow.com/questions/22173762/check-if-two-lists-are-equal
                            if (Enumerable.SequenceEqual(cadidatesInSingleColumn[firstIndexValue].OrderBy(fList => fList),
                                cadidatesInSingleColumn[secondIndexValue].OrderBy(sList => sList)) == true && firstIndexValue != secondIndexValue)
                            {
                                indexValue.Add(firstIndexValue);
                                indexValue.Add(secondIndexValue);
                            }
                        }
                    }

                    //Not sure about this logic will have to test. 
                    if (indexValue.Count != 0 && cadidatesInSingleColumn[indexValue[0]].Count <= indexValue.Count)
                    {
                        //Getting all of the naked values. 
                        List<int> nakedCandidates = cadidatesInSingleColumn[firstIndexValue];
                        bool isIndexNumber = false;

                        //Removing naked values from other cells within the grid. 
                        for (int indexValueOfListInColumn = 0; indexValueOfListInColumn <= cadidatesInSingleColumn.Count - 1; indexValueOfListInColumn++)
                        {
                            isIndexNumber = false;

                            foreach (int nakedValueIndexNumber in indexValue)
                            {
                                //If the cell is equal to one of the ones that have the naked tuples in, then do not remove the cnaidates from the cell. 
                                if (indexValueOfListInColumn == nakedValueIndexNumber)
                                {
                                    isIndexNumber = true;
                                    break;
                                }
                            }
                            bool indexNumberBool = false;


                            //If the cell is not part of the naked tuples, then removed the candidates, if any from the cell. 
                            if (isIndexNumber != true && cadidatesInSingleColumn[indexValueOfListInColumn] != null)
                            {
                                List<int> indexesNotNull = new List<int>();

                                //Removing null values from list 
                                for (int candidateIndexValue = 0; candidateIndexValue <= cadidatesInSingleColumn.Count - 1; candidateIndexValue++)
                                {
                                    if (cadidatesInSingleColumn[candidateIndexValue] != null)
                                    {
                                        indexesNotNull.Add(candidateIndexValue);
                                    }
                                }

                                foreach (int notNullIndexValue in indexesNotNull)
                                {
                                    //Check to see if the currenetly handled cell contains the naked tuples, then it should not be changed. 
                                    foreach (var indexValueCheck in indexValue)
                                    {
                                        if (indexValueCheck == notNullIndexValue)
                                        {
                                            indexNumberBool = true;
                                        }
                                    }
                                    if (indexNumberBool == false)
                                    {
                                        foreach (int nakedCandidateNumber in nakedCandidates)
                                        {
                                            //This need to be a for loop. 
                                            for (int indexNumberOfNotNakedCell = 0; indexNumberOfNotNakedCell <= cadidatesInSingleColumn[notNullIndexValue].Count - 1; indexNumberOfNotNakedCell++)
                                            {
                                                if (cadidatesInSingleColumn[notNullIndexValue][indexNumberOfNotNakedCell] == nakedCandidateNumber)

                                                {
                                                    //Removing candidate from cell. 
                                                    cadidatesInSingleColumn[notNullIndexValue].RemoveAt(indexNumberOfNotNakedCell);
                                                    candidatesList[notNullIndexValue * 9 + columnNumber] = cadidatesInSingleColumn[notNullIndexValue];
                                                    break;
                                                }

                                            }
                                        }
                                    }
                                    indexNumberBool = false;
                                }
                            }
                        }
                    }
                    indexValue.Clear();
                } //Closing bracket when all comparing has been completed. 
                cadidatesInSingleColumn.Clear();
            }
        }

        private void NakedTuplesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            List<int> indexValue = new List<int>();
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber);
                    //Seeing of there are any naked tuples within the row. 
                    for (int firstIndexValue = 0; firstIndexValue <= listOfCanidadtesForEachCellWithinTheBlock.Count - 1; firstIndexValue++)
                    {
                        //Gets all of the index values for the naked tuples, so candidates can be removed. 
                        for (int secondIndexValue = 0; secondIndexValue <= listOfCanidadtesForEachCellWithinTheBlock.Count - 1; secondIndexValue++)
                        {
                            if (listOfCanidadtesForEachCellWithinTheBlock[firstIndexValue] != null && listOfCanidadtesForEachCellWithinTheBlock[secondIndexValue] != null)
                            {
                                //This comapres 2 lists to see if thet are equal 
                                //http://stackoverflow.com/questions/22173762/check-if-two-lists-are-equal
                                if (Enumerable.SequenceEqual(listOfCanidadtesForEachCellWithinTheBlock[firstIndexValue].OrderBy(fList => fList),
                                    listOfCanidadtesForEachCellWithinTheBlock[secondIndexValue].OrderBy(sList => sList)) == true && firstIndexValue != secondIndexValue)
                                {
                                    indexValue.Add(firstIndexValue);
                                    indexValue.Add(secondIndexValue);
                                }
                            }
                        }

                        //Not sure about this logic will have to test. 
                        if (indexValue.Count != 0 && listOfCanidadtesForEachCellWithinTheBlock[indexValue[0]].Count <= indexValue.Count)
                        {
                            //Getting all of the naked values. 
                            List<int> nakedCandidates = listOfCanidadtesForEachCellWithinTheBlock[firstIndexValue];
                            bool isIndexNumber = false;

                            //Removing naked values from other cells within the grid. 
                            for (int indexValueOfListInColumn = 0; indexValueOfListInColumn <= listOfCanidadtesForEachCellWithinTheBlock.Count - 1; indexValueOfListInColumn++)
                            {
                                isIndexNumber = false;

                                foreach (int nakedValueIndexNumber in indexValue)
                                {
                                    //If the cell is equal to one of the ones that have the naked tuples in, then do not remove the cnaidates from the cell. 
                                    if (indexValueOfListInColumn == nakedValueIndexNumber)
                                    {
                                        isIndexNumber = true;
                                        break;
                                    }
                                }
                                bool indexNumberBool = false;


                                //If the cell is not part of the naked tuples, then removed the candidates, if any from the cell. 
                                if (isIndexNumber != true && listOfCanidadtesForEachCellWithinTheBlock[indexValueOfListInColumn] != null)
                                {
                                    List<int> indexesNotNull = new List<int>();

                                    //Removing null values from list 
                                    for (int candidateIndexValue = 0; candidateIndexValue <= listOfCanidadtesForEachCellWithinTheBlock.Count - 1; candidateIndexValue++)
                                    {
                                        if (listOfCanidadtesForEachCellWithinTheBlock[candidateIndexValue] != null)
                                        {
                                            indexesNotNull.Add(candidateIndexValue);
                                        }
                                    }

                                    foreach (int notNullIndexValue in indexesNotNull)
                                    {
                                        //Check to see if the currenetly handled cell contains the naked tuples, then it should not be changed. 
                                        foreach (var indexValueCheck in indexValue)
                                        {
                                            if (indexValueCheck == notNullIndexValue)
                                            {
                                                indexNumberBool = true;
                                            }
                                        }
                                        if (indexNumberBool == false)
                                        {
                                            foreach (int nakedCandidateNumber in nakedCandidates)
                                            {
                                                //This need to be a for loop. 
                                                for (int indexNumberOfNotNakedCell = 0; indexNumberOfNotNakedCell <= listOfCanidadtesForEachCellWithinTheBlock[notNullIndexValue].Count - 1; indexNumberOfNotNakedCell++)
                                                {
                                                    if (listOfCanidadtesForEachCellWithinTheBlock[notNullIndexValue][indexNumberOfNotNakedCell] == nakedCandidateNumber)
                                                    {
                                                        //Method to get the row and column number, using the row number, the index number and the column number 
                                                        int coordinateValue = 1;
                                                        int startRowNumber = rowNumber - 2;
                                                        int startCoulmnNumber = coulmnNumber - 2;
                                                        int actualRowNumber = 0;
                                                        int actualColumnNumber = 0;

                                                        for (; startRowNumber <= rowNumber; startRowNumber++)
                                                        {
                                                            for (; startCoulmnNumber <= coulmnNumber; startCoulmnNumber++)
                                                            {
                                                                if (notNullIndexValue + 1 == coordinateValue)
                                                                {
                                                                    actualRowNumber = startRowNumber;
                                                                    actualColumnNumber = startCoulmnNumber;
                                                                }
                                                                coordinateValue++;
                                                            }
                                                            startCoulmnNumber = coulmnNumber - 2;
                                                        }
                                                        //Removing candidate from cell. 
                                                        listOfCanidadtesForEachCellWithinTheBlock[notNullIndexValue].RemoveAt(indexNumberOfNotNakedCell);
                                                        candidatesList[9 * actualRowNumber + actualColumnNumber] = listOfCanidadtesForEachCellWithinTheBlock[notNullIndexValue];
                                                        break;
                                                    }

                                                }
                                            }
                                        }
                                        indexNumberBool = false;
                                    }
                                }
                            }
                        }
                        indexValue.Clear();
                    } //Closing bracket when all comparing has been completed. 
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
        }

        private void NakedTupleGeneric(string region, int rowNumber, int columnNumber, List<List<int>> listOfCells)
        {
            List<int> indexValue = new List<int>();


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
                        List<int> validNUmbersInRow = checkRow(sudokuPuzzleMultiExample, i, j);
                        List<int> validNumbersInColumn = checkColumn(sudokuPuzzleMultiExample, i, j);
                        List<int> validNumbersInBlock = checkBlock(sudokuPuzzleMultiExample, i, j);
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

        #region Backtracking Second Attempt

        private void BacktrackingSolve(int previousNumber)
        {
            //Check to see if the puzzle is complete. 
            int emptyCellCount = 0;
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
                        Console.WriteLine(rowNumber);
                        if (rowNumber == 1 && columnNumber == 5)
                        {

                        }

                        if (validNumbers.Count == 0)
                        {
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
                                        BacktrackingSolve(tempPreviousNumber);
                                    }
                                }
                                rowNumber = rowNumber - 1;
                                columnNumber = 8;
                            }
                        }
                        else
                        {
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
                            if (previousNumber != 0)
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
                                            if (tempPreviousNumber == 0)
                                            {

                                            }
                                            BacktrackingSolve(tempPreviousNumber);
                                        }
                                    }
                                    rowNumber = rowNumber - 1;
                                    columnNumber = 8;
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
    }
}
