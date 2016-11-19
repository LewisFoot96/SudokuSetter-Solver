using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    //Refactoring needs to be done between the columns and rows, with the hidden singles and also the naked rows and columns.

    public class SudokuSolver
    {
        #region Objects 
        //Puzzle manager, that handles the loading and wiritng to xml files for the game. 
        PuzzleManager puzzleManager = new PuzzleManager();
        //Details of the current 
        puzzle puzzleDetails;
        #endregion

        #region Global Variables 
        //Contains the puzzle. 
        public int[,] sudokuPuzzleMultiExample = new int[9, 9];
        //array that stores the static numbers that are within the puzzle. 
        public int[,] staticNumbers = new int[9, 9];

        //Row and column number of the current cell being handled. 
        int rowNumber = 0;
        int columnNumber = 0;

        //All of the check to see what numbers are valid for that particular square. 
        List<int> validNUmbersInRow = new List<int>();
        List<int> validNumbersInColumn = new List<int>();
        List<int> validNumbersInBlock = new List<int>();
        List<int> validNumbersInCell = new List<int>();
        List<int> validNumbersInRegion = new List<int>();
        List<int> nonValidumbersInRegion = new List<int>();
        List<int> numberPositionsInRegion = new List<int>();

        //Random number generate, used where necessary.
        Random randomNumberGenerator = new Random();
        //Counter that is used for counting loops within the program. 
        int counter = 0;

        //List of the cell coordinates that will be used for solving, the order of cells to be handled. 
        List<List<int>> cellNumbersForLogicalEffcientOrder = new List<List<int>>();
        //Used to store the number in the cell that is being bactracked to, beofore clearing that cell. 
        int previousNumberInCell = 0;
        //The number of the cell being handled within the ordered list. 
        int numberOfCellToBeHandled = 0;

        //The number of candidates that are in a cell. 
        int candidateTotalNumber = 1;
        //current cell being handled 
        int currentCellNumberHandled = 0;

        //Stop watch that will time the algorithm. 
        Stopwatch stopWatch = new Stopwatch();
        TimeSpan timeSpan = TimeSpan.FromSeconds(30);
        //List that contains all of the candidates for each cell this should be used for candidate reference within the program. 
        List<List<int>> candidatesList = new List<List<int>>();
        //counts the number of times the rules based algorithm has been exectuted. 
        int methodRunNumber = 0;

        #endregion

        #region Main Method 
        public bool solvePuzzle()
        {
            //Generate the puzzle and then solve it. 
            GeneratePuzzle();
            //Solving the puzzle. 
            bool solved = SolveSudokRuleBased();
            return solved;
        }

        #endregion 

        #region General Methods 
        //Method that generates the example puzzle. 
        private void GeneratePuzzle()
        {
            //Loaing in a puzzle from a test file and creating the puzzle, along with static numbers. 
            puzzleDetails = puzzleManager.ReadFromXMlFile("C:\\Users\\New\\Documents\\Sudoku\\Application\\SudokuSetterAndSolver\\SudokuSetterAndSolver\\Puzzles\\TestPuzzles\\test21.xml");
            int[] puzzleArray = puzzleDetails.puzzlecells.Cast<int>().ToArray();
            sudokuPuzzleMultiExample = puzzleManager.ConvertArrayToMultiDimensionalArray(puzzleArray);
            staticNumbers = sudokuPuzzleMultiExample;
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
        public bool SolveSudokRuleBased()
        {
            //Contains the list of candiates in each cell from simple analysis, not including human solvint methods procesing. 
            List<List<int>> tempCandiateList = new List<List<int>>();
            tempCandiateList.Clear();
            //Number of naked singles within the puzzle, reset everytime this method is executed. 
            int nakedSinglesCount = 0;

            ///Going through all of the cells. 
            for (rowNumber = 0; rowNumber <= 8; rowNumber++)
            {
                for (columnNumber = 0; columnNumber <= 8; columnNumber++)
                {
                    //Static numbers check 
                    if (staticNumbers[rowNumber, columnNumber] == 0)
                    {
                        checkBlock();
                        checkColumn();
                        checkRow();
                        GetValidNumbers();
                        validNumbersInBlock.Clear();
                        validNumbersInColumn.Clear();
                        validNUmbersInRow.Clear();
                        tempCandiateList.Add(new List<int>(validNumbersInCell));
                        validNumbersInCell.Clear();
                    }
                    else
                    {
                        tempCandiateList.Add(null);
                    }
                    validNumbersInCell.Clear();
                }
            }

            //Check to see if its the first run of the method, and setting the orginal 
            if (methodRunNumber == 0)
            {
                candidatesList = tempCandiateList;
            }
            else
            {
                CompareCandidateLists(tempCandiateList); //comparing the candidate list and the temp, to get the current values.
            }
            //The correct candidate list is now in place. 

            //Gets all the naked singles witin the puzzle. 
            int rowNumberCheck = 0;
            int columnCheckNumber = 0;
            for (int indexOfCandidateValue = 0; indexOfCandidateValue <= candidatesList.Count - 1; indexOfCandidateValue++)
            {
                if (candidatesList[indexOfCandidateValue] != null)
                {
                    if (candidatesList[indexOfCandidateValue].Count == 1) //Naked singles 
                    {
                        nakedSinglesCount++;
                        foreach (int nakedValue in candidatesList[indexOfCandidateValue]) //Insert naked single. 
                        {
                            staticNumbers[rowNumberCheck, columnCheckNumber] = nakedValue;
                            sudokuPuzzleMultiExample[rowNumberCheck, columnCheckNumber] = nakedValue;
                            candidatesList[indexOfCandidateValue] = null;
                        }
                    }
                }
                //Row and column logic to determine current cell. 
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
            //Resetting values and increasing method count. 
            rowNumberCheck = 0;
            columnCheckNumber = 0;
            methodRunNumber++;

            //If there were naked singles, then see if puzzle is solved, if not then recurse. 
            if (nakedSinglesCount != 0)
            {
                bool solved = CheckToSeeIfPuzzleIsSolved();
                if (solved)
                {
                    Console.WriteLine("Solved");
                }
                else
                { SolveSudokRuleBased(); }

            }
            //Checks to see if puzzle is solved. 
            bool checkSolved = CheckToSeeIfPuzzleIsSolved();
            if (checkSolved)
            {
                Console.WriteLine("Solved");
            }
            //all the below methods seem to work togher and solve puzzles. 
            // HiddenSingles();
            checkSolved = CheckToSeeIfPuzzleIsSolved();
            if (checkSolved)
            {
                Console.WriteLine("Solved");
            }
            //CandidateHandling();
            //Backtracking
           bool solvedBacktracking = BacktrackinEffcient(false);
            return solvedBacktracking;
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
            NakedDoubles();
            HiddenDoubles();
            NakedTriples();
            //Naked Triples 
            //HIdden Triples 
        }

        #endregion

        #region Hiddens Singles 
        //Method that calls all of the hidden methods, to see if ther are any hidden values in the grid. 
        private void HiddenSingles()
        {
            //Resetting bools. 
            bool hiddenRowBool = false;
            bool hiddenColumnBool = false;
            bool hiddenBlockBool = false;
            //GOing through the hidden singles, and returning to the main method if they are found within a region, to update the puzzle, acting as a human. 
            hiddenRowBool = HiddenRowSingles();
            if (hiddenRowBool == true)
            {
                SolveSudokRuleBased();
            }
            hiddenColumnBool = HiddenColumnSingles();
            if (hiddenColumnBool == true)
            {
                SolveSudokRuleBased();
            }
            hiddenBlockBool = HiddenBlockSingles();
            if (hiddenBlockBool == true)
            {
                SolveSudokRuleBased();
            }
        }

        private bool HiddenColumnSingles()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheColumn = new List<List<int>>();
            bool hiddenColumnBool = false;
            int hiddenColumnSinglesCount = 0;

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
                hiddenColumnSinglesCount += HiddenSinglesGeneric("column", listOfCanidadtesForEachCellWithinTheColumn, 0, columnNumber);
                listOfCanidadtesForEachCellWithinTheColumn.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
            }
            if (hiddenColumnSinglesCount >= 1)
            {
                hiddenColumnBool = true;
            }
            return hiddenColumnBool;
        }

        private bool HiddenBlockSingles()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            bool hiddenBlockBool = false;
            int hiddenBlockSinglesCount = 0;

            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber);

                    hiddenBlockSinglesCount += HiddenSinglesGeneric("block", listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber);

                    listOfCanidadtesForEachCellWithinTheBlock.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
                }
            }
            if (hiddenBlockSinglesCount >= 1)
            {
                hiddenBlockBool = true;
            }
            return hiddenBlockBool;
        }

        //The return bool value is returning false, if the last row does not have a hidden single, thereofre a counting system may need to be implemented. 
        private bool HiddenRowSingles()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheRow = new List<List<int>>();
            int _rowNumber = 0;
            bool hiddenRowBool = false;
            int hiddenRowCount = 0;

            //Going through all if the candidate cells. 
            for (int candidateIndexNumber = 0; candidateIndexNumber <= candidatesList.Count - 1; candidateIndexNumber++)
            {
                //Adding values to the row.
                listOfCanidadtesForEachCellWithinTheRow.Add(candidatesList[candidateIndexNumber]);
                //When the end of a row has been reached. 
                if (candidateIndexNumber % 9 == 8 || candidateIndexNumber == 8)
                {
                    hiddenRowCount += HiddenSinglesGeneric("row", listOfCanidadtesForEachCellWithinTheRow, _rowNumber, 0);
                    listOfCanidadtesForEachCellWithinTheRow.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
                    _rowNumber++; //Increasing the row number
                }
            }
            //There where hidden singles. 
            if (hiddenRowCount >= 1)
            {
                hiddenRowBool = true;
            }
            return hiddenRowBool;
        }

        //May need to implement this method at somepoint. 
        private int HiddenSinglesGeneric(string region, List<List<int>> listOfCells, int rowNumber, int columnNumber)
        {
            List<int> notNullIndexList = new List<int>();
            List<int> individualNumbers = new List<int>();
            List<int> valuesUsed = new List<int>();
            int hiddenCount = 0;
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
                                    hiddenCount++;
                                    //Updating the grid and corresponding candidates. 
                                    staticNumbers[actualRowNumber, actualColumnNumber] = indivdualValue;
                                    sudokuPuzzleMultiExample[actualRowNumber, actualColumnNumber] = indivdualValue;
                                    candidatesList[9 * actualRowNumber + actualColumnNumber] = null;
                                    break;
                                }
                                else if (region == "column")
                                {
                                    hiddenCount++;
                                    //Updating the grid and corresponding candidates. 
                                    staticNumbers[notNullIndexList[candidateValues], columnNumber] = indivdualValue;
                                    sudokuPuzzleMultiExample[notNullIndexList[candidateValues], columnNumber] = indivdualValue;
                                    candidatesList[9 * notNullIndexList[candidateValues] + columnNumber] = null;
                                    break;
                                }
                                else
                                {
                                    hiddenCount++;
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
            return hiddenCount;
        }

        #endregion

        #region Naked Doubles 
        private void NakedDoubles()
        {
            //Resetting 
            bool nakedDoubleRowBool = false;
            bool nakedDoubleColumnBool = false;
            bool nakedDoubleBlockBool = false;
            //Adding any naked doubles, if there isnt any go to hidden doubles. 
            nakedDoubleRowBool = NakedDoubleRow();
            nakedDoubleColumnBool = NakedDoublesColumn();
            nakedDoubleBlockBool = NakedDoublesBlock();

            if (nakedDoubleRowBool == true || nakedDoubleColumnBool == true || nakedDoubleBlockBool == true)
            {
                SolveSudokRuleBased();
            }
        }

        private bool NakedDoubleRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            bool nakedDoubleBool = false;
            int nakedDoubleRowCount = 0;
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);

                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    nakedDoubleRowCount += GetNakedDoubles(cadidatesInSingleRow, rowNumber, 0, 0, "row");
                    cadidatesInSingleRow.Clear();
                }
            }
            if (nakedDoubleRowCount >= 1)
            {
                nakedDoubleBool = true;
            }
            return nakedDoubleBool;
        }

        private bool NakedDoublesColumn()
        {
            bool nakedDoubleCoumnBool = false;
            int nakedDoubleColumnCount = 0;
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
                nakedDoubleColumnCount += GetNakedDoubles(cadidatesInSingleColumn, 0, columnNumber, 0, "column");
                cadidatesInSingleColumn.Clear();
            }

            if (nakedDoubleColumnCount >= 1)
            {
                nakedDoubleCoumnBool = true;
            }
            return nakedDoubleCoumnBool;
        }

        private bool NakedDoublesBlock()
        {
            bool nakedDoubleBlockBool = false;
            int nakedDoubleBlockCount = 0;
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber); //Get cell values for that block. 
                    nakedDoubleBlockCount += GetNakedDoubles(listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber, 0, "block");
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
            if (nakedDoubleBlockCount >= 1)
            {
                nakedDoubleBlockBool = true;
            }
            return nakedDoubleBlockBool;
        }

        private int GetNakedDoubles(List<List<int>> cadidatesInSingleRow, int rowNumber, int columnNumber, int blockNumber, string regionTitle)
        {
            int nakedDoubleCount = 0;
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
                                            nakedDoubleCount++;
                                            candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        else if (regionTitle == "column")
                                        {
                                            nakedDoubleCount++;
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
                                            nakedDoubleCount++;
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
            return nakedDoubleCount;
        }

        #endregion

        #region Hidden Doubles 

        private void HiddenDoubles()
        {
            //Resetting 
            bool hiddenDoubleRowBool = false;
            bool hiddenDoubleColumnBool = false;
            bool hiddenDoubleBlockBool = false;
            //Getting the hidden doubles. 
            hiddenDoubleRowBool = HiddenDoublesRow();
            hiddenDoubleColumnBool = HiddenDoublesColumn();
            hiddenDoubleBlockBool = HiddenDoublesBlock();
            //If there is hidden doubles then begin the initial method again. 
            if (hiddenDoubleBlockBool == true || hiddenDoubleColumnBool == true || hiddenDoubleRowBool == true)
            {
                SolveSudokRuleBased();
            }

        }

        private bool HiddenDoublesRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            bool hiddenDoubleRowBool = false;
            int hiddenDoubleRowCount = 0;
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);

                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    hiddenDoubleRowCount += GetHiddenDoubles(cadidatesInSingleRow, rowNumber, 0, "row");
                    cadidatesInSingleRow.Clear();
                }
            }
            if (hiddenDoubleRowCount >= 1)
            {
                hiddenDoubleRowBool = true;
            }
            return hiddenDoubleRowBool;
        }
        private bool HiddenDoublesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            bool hiddenDoubleColumnBool = false;
            int hiddenDoubleColumnCount = 0;
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
                hiddenDoubleColumnCount += GetHiddenDoubles(cadidatesInSingleColumn, 0, columnNumber, "column");
                cadidatesInSingleColumn.Clear();
            }
            if (hiddenDoubleColumnCount >= 1)
            {
                hiddenDoubleColumnBool = true;
            }
            return hiddenDoubleColumnBool;
        }
        private bool HiddenDoublesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            bool hiddenDoubleBlockBool = false;
            int hiddenDoubleBlockCount = 0;
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber); //Get cell values for that block. 
                    hiddenDoubleBlockCount += GetHiddenDoubles(listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber, "block");
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
            if (hiddenDoubleBlockCount >= 1)
            {
                hiddenDoubleBlockBool = true;
            }
            return hiddenDoubleBlockBool;
        }

        //The logic for this method is worng. 
        private int GetHiddenDoubles(List<List<int>> cells, int rowNumber, int columnNumber, string regionTitle)
        {
            //Stores all of the index values for the cells that are not null within the region. 
            List<int> notNullIndexValuesCellsInRow = new List<int>();
            int hiddenDoubleCount = 0;

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
                                            hiddenDoubleCount++;
                                            candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        else if (regionTitle == "column")
                                        {
                                            hiddenDoubleCount++;
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
                                            hiddenDoubleCount++;
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

            return hiddenDoubleCount;
        }
        #endregion

        #region Hidden Triples

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

        #endregion 

        #region Naked Triples 

        private void NakedTriples()
        {
            bool nakedTripleRowBool = false;
            bool nakedTripleColumnBool = false;
            bool nakedTripleBlockBool = false;
            nakedTripleRowBool = NakedTriplesRow();
            nakedTripleColumnBool = NakedTriplesColumn();
            nakedTripleBlockBool = NakedTriplesBlock();

            if (nakedTripleRowBool == true || nakedTripleColumnBool == true || nakedTripleBlockBool == true)
            {
                SolveSudokRuleBased();
            }
        }

        private bool NakedTriplesRow()
        {
            //Getting the naked triples for all of the rows wihtin the grid. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            bool nakedTripleRowBool = false;
            int nakedTripleRowCount = 0;
            for (int rowNumber = 0; rowNumber <= 80; rowNumber++)
            {
                cadidatesInSingleRow.Add(candidatesList[rowNumber]);
                //If the row is at an end. 
                if (rowNumber % 9 == 8 || rowNumber == 8)
                {
                    nakedTripleRowCount += GetNakedTriples(cadidatesInSingleRow, rowNumber, 0, 0, "row");
                    cadidatesInSingleRow.Clear();
                }
            }
            if (nakedTripleRowCount >= 1)
            {
                nakedTripleRowBool = true;
            }
            return nakedTripleRowBool;
        }

        private bool NakedTriplesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            bool nakedTripleColumnBool = false;
            int nakedTripleColumnCount = 0;
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
                nakedTripleColumnCount += GetNakedTriples(cadidatesInSingleColumn, 0, columnNumber, 0, "column");
                cadidatesInSingleColumn.Clear();
            }
            if (nakedTripleColumnCount >= 1)
            {
                nakedTripleColumnBool = true;
            }
            return nakedTripleColumnBool;
        }

        private bool NakedTriplesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            bool nakedTripleBlockBool = false;
            int nakedTripleBlockCount = 0;
            //Gets all the values from each block. 
            for (int rowNumber = 2; rowNumber <= 8; rowNumber += 3)
            {
                for (int coulmnNumber = 2; coulmnNumber <= 8; coulmnNumber += 3)
                {
                    listOfCanidadtesForEachCellWithinTheBlock = getSudokuValuesInBox(rowNumber, coulmnNumber); //Get cells values for that block. 
                    nakedTripleBlockCount += GetNakedTriples(listOfCanidadtesForEachCellWithinTheBlock, rowNumber, coulmnNumber, 0, "block");
                    listOfCanidadtesForEachCellWithinTheBlock.Clear();
                }
            }
            if (nakedTripleBlockCount >= 1)
            {
                nakedTripleBlockBool = true;
            }
            return nakedTripleBlockBool;
        }

        private int GetNakedTriples(List<List<int>> cadidatesInSingleRow, int rowNumber, int columnNumber, int blockNumber, string regionTitle)
        {
            List<int> notNullIndexValuesCellsInRow = new List<int>();
            int nakedTripleCount = 0;
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
                                                nakedTripleCount++;
                                                candidatesList[rowNumber - 8 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            else if (regionTitle == "column")
                                            {
                                                nakedTripleCount++;
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
                                                nakedTripleCount++;
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
            return nakedTripleCount;
        }

        #endregion

        #region Remove Candidates Method 

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

        //New backtracking no recursion, works, needs to be tested with harder puzzles. 
        #region Backtracking More Efficient Test 
        public bool BacktrackinEffcient(bool generating)
        {
            //Starting the timer
            stopWatch.Start();
            //setting the starting candidate number value. 
            candidateTotalNumber = 1;

            //Creating 81 blank lists. 
            for (int empty = 0; empty <= 80; empty++)
            {
                List<int> blankList = new List<int>();
                cellNumbersForLogicalEffcientOrder.Add(blankList);
            }
            bool solved = false;
            int startingValue = 0;

            //This process get the valid cells, in the order they should be handled based on the number of candidtates in the cell. 
            while (candidateTotalNumber <= 9 || solved == true)
            {
                for (currentCellNumberHandled = startingValue; currentCellNumberHandled <= 80; currentCellNumberHandled++)
                {
                    for (rowNumber = 0; rowNumber <= 8; rowNumber++)
                    {
                        for (columnNumber = 0; columnNumber <= 8; columnNumber++)
                        {
                            if (sudokuPuzzleMultiExample[rowNumber, columnNumber] == 0)
                            {
                                checkBlock();
                                checkColumn();
                                checkRow();
                                GetValidNumbers();
                                validNumbersInBlock.Clear();
                                validNumbersInColumn.Clear();
                                validNUmbersInRow.Clear();

                                if (validNumbersInCell.Count == candidateTotalNumber)
                                {
                                    cellNumbersForLogicalEffcientOrder[startingValue].Add(rowNumber);
                                    cellNumbersForLogicalEffcientOrder[startingValue].Add(columnNumber);
                                    validNumbersInCell.Clear();
                                    startingValue++;

                                }
                                validNumbersInCell.Clear();
                            }
                        }
                    }
                    candidateTotalNumber++;
                }
            }

            //Resetting the starting value, so it cycles through all of the cells. 
            startingValue = 0;
            numberOfCellToBeHandled = 0;

            for (startingValue = numberOfCellToBeHandled; startingValue <= cellNumbersForLogicalEffcientOrder.Count - 1; startingValue++)
            {
                if(stopWatch.Elapsed >=timeSpan)
                {
                    return false; 
                }
                if (cellNumbersForLogicalEffcientOrder[startingValue].Count == 0)
                {
                    break;
                }
                numberOfCellToBeHandled = startingValue;
                rowNumber = cellNumbersForLogicalEffcientOrder[startingValue][0];
                columnNumber = cellNumbersForLogicalEffcientOrder[startingValue][1];

                checkBlock();
                checkColumn();
                checkRow();
                GetValidNumbers();
                validNumbersInBlock.Clear();
                validNumbersInColumn.Clear();
                validNUmbersInRow.Clear();

                //if (candidatesList.Count != 0)
                //{
                //    CompareCandidateForCell();
                //}

                if (validNumbersInCell.Count == 0)
                {
                    previousNumberInCell = sudokuPuzzleMultiExample[cellNumbersForLogicalEffcientOrder[numberOfCellToBeHandled - 1][0], cellNumbersForLogicalEffcientOrder[numberOfCellToBeHandled - 1][1]];

                    sudokuPuzzleMultiExample[cellNumbersForLogicalEffcientOrder[startingValue - 1][0], cellNumbersForLogicalEffcientOrder[startingValue - 1][1]] = 0;
                    startingValue -= 2;
                }
                else
                {
                    if (previousNumberInCell == 0)
                    {
                        if (generating == false)
                        {
                            sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbersInCell[0];
                        }
                        //Random number generation for creating a puzzle. 
                        else
                        {
                            int randomNumber = randomNumberGenerator.Next(validNumbersInCell.Count);
                            sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbersInCell[randomNumber];
                        }
                    }

                    else
                    {
                        //Need to back track further. 
                        if (previousNumberInCell == validNumbersInCell[validNumbersInCell.Count - 1])
                        {
                            previousNumberInCell = sudokuPuzzleMultiExample[cellNumbersForLogicalEffcientOrder[numberOfCellToBeHandled - 1][0], cellNumbersForLogicalEffcientOrder[numberOfCellToBeHandled - 1][1]];
                            sudokuPuzzleMultiExample[cellNumbersForLogicalEffcientOrder[startingValue - 1][0], cellNumbersForLogicalEffcientOrder[startingValue - 1][1]] = 0;
                            startingValue -= 2;

                        }
                        else
                        {
                            //Maybe  something wrong with this statement 
                            int correctNumber = randomNumberGenerator.Next(validNumbersInCell.Count);
                            //this gives the value of the index, so this then would be used to get the other value. 
                            if (generating == true)
                            {
                                if (validNumbersInCell[correctNumber] == previousNumberInCell)
                                {
                                    for (counter = 0; counter <= validNumbersInCell.Count - 1; counter++)
                                    {
                                        if (counter != correctNumber)
                                        {
                                            sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbersInCell[counter];
                                            previousNumberInCell = 0;
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbersInCell[correctNumber];
                                    previousNumberInCell = 0;

                                }
                            }
                            else
                            {

                                for (counter = 0; counter <= validNumbersInCell.Count - 1; counter++)
                                {

                                    //If the valid number has already been used in the cell, then the next number will need to be inserted or the backtracking will need to go back further. 
                                    if (validNumbersInCell[counter] == previousNumberInCell || validNumbersInCell[counter] < previousNumberInCell)
                                    {

                                    }
                                    else //Set the valid number to the cell, and submit the new grid. 
                                    {
                                        sudokuPuzzleMultiExample[rowNumber, columnNumber] = validNumbersInCell[counter];
                                        previousNumberInCell = 0;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                validNumbersInCell.Clear();
            }
            cellNumbersForLogicalEffcientOrder.Clear();
            return true; 

        }

        /// <summary>
        /// This method compares the candidate list, with the valid numbers in the cell, if there is valid numbers that have been removd in earlier processing then they are removed from the list 
        /// </summary>
        private void CompareCandidateForCell()
        {
            //this method 
            List<int> finalCandidateList = new List<int>();
            //Need to compare the valid numbers in the cell with the candidates that have been attained from previous processing. 
            foreach (int candidateValue in candidatesList[rowNumber * 9 + columnNumber])
            {
                if (validNumbersInCell.Contains(candidateValue))
                {
                    finalCandidateList.Add(candidateValue);
                }
            }
            validNumbersInCell = finalCandidateList;
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
        private void checkBlock()
        {
            //Need to work out how to get all the values out of each box. 

            if (rowNumber + 1 == 2 || rowNumber + 1 == 5 || rowNumber + 1 == 8)
            {
                if (columnNumber + 1 == 2 || columnNumber + 1 == 5 || columnNumber + 1 == 8)
                {
                    //Middle square 
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber - 1]);
                }
                else if (columnNumber + 1 == 3 || columnNumber + 1 == 6 || columnNumber + 1 == 9)
                {
                    //Middle right square
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber - 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber - 2]);
                }
                else
                {
                    //Middle left
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber + 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber + 2]);
                }
                //This if it is at the middle of a block. 
            }
            else if (rowNumber + 1 == 3 || rowNumber + 1 == 6 || rowNumber + 1 == 9)
            {
                if (columnNumber + 1 == 2 || columnNumber + 1 == 5 || columnNumber + 1 == 8)
                {
                    //Bottom middle 
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber + 1]);
                }
                else if (columnNumber + 1 == 3 || columnNumber + 1 == 6 || columnNumber + 1 == 9)
                {
                    //Botom righ corner 
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber - 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber - 2]);
                }
                else
                {
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 1, columnNumber + 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber + 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber - 2, columnNumber + 1]);
                }
            }
            else
            {
                if (columnNumber + 1 == 2 || columnNumber + 1 == 5 || columnNumber + 1 == 8)
                {
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber - 1]);
                }
                else if (columnNumber + 1 == 3 || columnNumber + 1 == 6 || columnNumber + 1 == 9)
                {
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber - 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber - 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber - 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber - 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber - 1]);
                }
                else
                {
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber + 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 2]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 2, columnNumber + 1]);
                    numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber + 1, columnNumber + 2]);
                }
            }

            foreach (var value in numberPositionsInRegion)
            {
                if (value == 0)
                {

                }
                else
                {
                    nonValidumbersInRegion.Add(value);
                }
            }
            for (counter = 1; counter <= 9; counter++)
            {
                if (nonValidumbersInRegion.Contains(counter) == false)
                {
                    validNumbersInBlock.Add(counter);
                }
            }
            numberPositionsInRegion.Clear();
            nonValidumbersInRegion.Clear();
        }

        //There is something wrong with this method atm. 
        private void checkColumn()
        {
            for (counter = 0; counter <= 8; counter++)
            {
                numberPositionsInRegion.Add(sudokuPuzzleMultiExample[counter, columnNumber]);
            }

            foreach (var value in numberPositionsInRegion)
            {
                if (value == 0)
                {

                }
                else
                {
                    nonValidumbersInRegion.Add(value);
                }
            }
            //Getting all the valid numbers i.e. the numbers that are not already in the column. 
            for (counter = 1; counter <= 9; counter++)
            {
                if (nonValidumbersInRegion.Contains(counter) == false)
                {
                    validNumbersInColumn.Add(counter);
                }
            }
            numberPositionsInRegion.Clear();
            nonValidumbersInRegion.Clear();
        }

        private void checkRow()
        {
            for (counter = 0; counter <= 8; counter++)
            {
                numberPositionsInRegion.Add(sudokuPuzzleMultiExample[rowNumber, counter]);
            }

            foreach (var value in numberPositionsInRegion)
            {
                if (value == 0)
                {

                }
                else
                {
                    nonValidumbersInRegion.Add(value);
                }
            }
            //Getting all the valid numbers i.e. the numbers that are not already in the column. 
            //Get the valid number that can be within this row. These should be in number order. 
            for (counter = 1; counter <= 9; counter++)
            {
                if (nonValidumbersInRegion.Contains(counter) == false)
                {
                    validNUmbersInRow.Add(counter);
                }
            }
            nonValidumbersInRegion.Clear();
            numberPositionsInRegion.Clear();
        }

        private void GetValidNumbers()
        {
            foreach (var columnValue in validNumbersInColumn)
            {
                if (validNUmbersInRow.Contains(columnValue) && validNumbersInBlock.Contains(columnValue))
                {
                    validNumbersInCell.Add(columnValue);
                }
            }
        }

        #endregion

    }
}
