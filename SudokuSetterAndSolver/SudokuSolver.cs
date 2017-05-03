using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    public class SudokuSolver
    {
        #region Objects 
        //Puzzle manager, that handles the loading and wiritng to xml files for the game. 
        PuzzleManager puzzleManager = new PuzzleManager();
        #endregion
        //0,1,2,3 will be the difficulty values. Human will have a 2 weighting. 
        #region Global Variables 
        //All of the check to see what numbers are valid for that particular square. 
        List<int> validNUmbersInRow = new List<int>();
        List<int> validNumbersInColumn = new List<int>();
        List<int> validNumbersInBlock = new List<int>();
        List<int> validNumbersInCell = new List<int>();

        //Random number generate, used where necessary.
        Random randomNumberGenerator = new Random();
        //Counter that is used for counting loops within the program. 
        int counter = 0;

        //List of the cell coordinates that will be used for solving, the order of cells to be handled. 
        List<List<int>> cellNumbersForLogicalEffcientOrder = new List<List<int>>();

        List<int> logicalOrderOfCellsXml = new List<int>();
        //Used to store the number in the cell that is being bactracked to, beofore clearing that cell. 
        int previousNumberInCell = 0;
        //The number of the cell being handled within the ordered list. 
        int numberOfCellToBeHandled = 0;

        //The number of candidates that are in a cell. 
        int candidateTotalNumber = 1;

        //Stop watch that will time the algorithm. 
        Stopwatch stopWatch = new Stopwatch();
        //List that contains all of the candidates for each cell this should be used for candidate reference within the program. 
        List<List<int>> candidatesList = new List<List<int>>();
        //counts the number of times the rules based algorithm has been exectuted. 
        int methodRunNumber = 0;
        bool solvedBacktracking = false;
        public string difficluty;
        int totalNumberOfCandidates = 0;
        int totalNumberOfCandidatesDifficulty = 0;
        int executionTimeDifficulty = 0;
        int numberOfStaticNumbers = 0;
        int numberOfStaticNumberDifficulty = 0;
        int humanSolvingDifficulty = 0;
        int backtrackingNodesCount = 0;
        int singlesCount = 0;
        int doublesCount = 0;
        int triplesCount = 0;
        int hiddenCount = 0;

        bool backtrackingBool = false;
        //Get all the cells with the corrrect row , column and block number, this will then allow easier handling.
        public puzzle currentPuzzleToBeSolved = new puzzle();
        //THis will be the cell that is currently being handled by the solver. 
        puzzleCell puzzleCellCurrentlyBeingHandled = new puzzleCell();
        //List of cells that can be removed from the puzzle, after it has been solved. 
        List<int> listOfCellsToBeRemoved = new List<int>();
        #endregion

        #region General Methods 

        /// <summary>
        /// Method to check whether the puzzle has been solved at various points. 
        /// </summary>
        /// <returns></returns>
        private bool CheckToSeeIfPuzzleSolvedXML()
        {
            foreach (var cell in currentPuzzleToBeSolved.puzzlecells)
            {
                if (cell.value == 0)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion 

        #region Rule Based Algorithm 

        public bool SolveSudokuRuleBasedXML()
        {
            backtrackingBool = false;
            humanSolvingDifficulty = 0;

            //Contains the list of candiates in each cell from simple analysis, not including human solvint methods procesing. 
            List<List<int>> tempCandiateList = new List<List<int>>();
            tempCandiateList.Clear();
            //Number of naked singles within the puzzle, reset everytime this method is executed. 
            int nakedSinglesCount = 0;

            for (int cellIndexValueCandidates = 0; cellIndexValueCandidates <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexValueCandidates++)
            {
                puzzleCellCurrentlyBeingHandled = currentPuzzleToBeSolved.puzzlecells[cellIndexValueCandidates];

                if (puzzleCellCurrentlyBeingHandled.value == 0)
                {
                    GetValuesForRowXmlPuzzleTemplate();
                    GetValuesForColumnXmlPuzzleTemplate();
                    GetValuesForBlockXmlPuzzleTemplate();
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

            //Check to see if its the first run of the method, and setting the orginal 
            if (methodRunNumber == 0)
            {
                for (int nonBlankCellCount = 0; nonBlankCellCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; nonBlankCellCount++)
                {
                    if (currentPuzzleToBeSolved.puzzlecells[nonBlankCellCount].value != 0)
                    {
                        numberOfStaticNumbers++;
                    }
                    else
                    {
                        listOfCellsToBeRemoved.Add(nonBlankCellCount);
                    }
                }
                candidatesList = tempCandiateList;

                for (int firstCandidateInPuzzle = 0; firstCandidateInPuzzle <= candidatesList.Count - 1; firstCandidateInPuzzle++)
                {
                    if (candidatesList[firstCandidateInPuzzle] != null)
                    {
                        totalNumberOfCandidates += candidatesList[firstCandidateInPuzzle].Count;
                    }
                }
            }
            else
            {
                CompareCandidateLists(tempCandiateList); //comparing the candidate list and the temp, to get the current values.
            }

            bool nakedBool = false;
            for (int indexOfCandidateValue = 0; indexOfCandidateValue <= candidatesList.Count - 1; indexOfCandidateValue++)
            {
                if (candidatesList[indexOfCandidateValue] != null)
                {
                    if (candidatesList[indexOfCandidateValue].Count == 1) //Naked singles 
                    {
                        nakedSinglesCount++;
                        nakedBool = true;
                        foreach (int nakedValue in candidatesList[indexOfCandidateValue]) //Insert naked single. 
                        {
                            currentPuzzleToBeSolved.puzzlecells[indexOfCandidateValue].value = nakedValue;
                            candidatesList[indexOfCandidateValue] = null;
                        }
                    }
                }
            }
            if (nakedBool)
            {
                singlesCount++;
            }
            methodRunNumber++;

            //If there were naked singles, then see if puzzle is solved, if not then recurse. 
            if (nakedSinglesCount != 0)
            {
                bool solved = CheckToSeeIfPuzzleSolvedXML();
                if (solved)
                {
                    //CheckUniqueSolution();
                    return true;
                }
                else
                { SolveSudokuRuleBasedXML(); }
            }
            //Checks to see if puzzle is solved. 
            bool checkSolved = CheckToSeeIfPuzzleSolvedXML();
            if (checkSolved)
            {
                //CheckUniqueSolution();
                //MessageBox.Show("Human Solving Methods Completed! Puzzle Completed. Difficulty: " + difficluty);
                return true;
            }
            //all the below methods seem to work togher and solve puzzles. 
            HiddenSingles();
            checkSolved = CheckToSeeIfPuzzleSolvedXML();
            if (checkSolved)
            {
                //CheckUniqueSolution();
                //MessageBox.Show("Human Solving Methods Completed! Puzzle Completed. Difficulty: " + difficluty);
                return true;
            }
            humanSolvingDifficulty = 1;
            CandidateHandling();
            humanSolvingDifficulty = 3;
            //MessageBox.Show("Human Solving Methods Completed! Puzzle not completed. Diffiuclty: Very Hard. Backtracking will begin.");

            backtrackingBool = true;
            solvedBacktracking = BacktrackingUsingXmlTemplateFile(false);
            //CheckUniqueSolution();
            
            return solvedBacktracking;
        }

      /// <summary>
      /// Method that updates the candidates list so candidates can be removed from the puzzle correspondingly. 
      /// </summary>
      /// <param name="tempCandidateList"></param>
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
            //Candidate handling methods 
            NakedDoubles();
            HiddenDoubles();
            NakedTriples();
            HiddenTriples();
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
                hiddenCount++;
                SolveSudokuRuleBasedXML();
            }
            hiddenColumnBool = HiddenColumnSingles();
            if (hiddenColumnBool == true)
            {
                hiddenCount++;
                SolveSudokuRuleBasedXML();
            }
            hiddenBlockBool = HiddenBlockSingles();
            if (hiddenBlockBool == true)
            {
                hiddenCount++;
                SolveSudokuRuleBasedXML();
            }
        }

        private bool HiddenColumnSingles()
        {
            List<int> indexValuesOfColumn = new List<int>();
            bool hiddenColumnBool = false;
            int hiddenColumnSinglesCount = 0;

            //Search through all of the columns. 
            for (int columnNumber = 0; columnNumber <= currentPuzzleToBeSolved.gridsize - 1; columnNumber++)
            {
                //Get all the values within that column 
                for (int candiateIndexNumber = 0; candiateIndexNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; candiateIndexNumber++)
                {
                    if (columnNumber == currentPuzzleToBeSolved.puzzlecells[candiateIndexNumber].columnnumber)
                    {
                        indexValuesOfColumn.Add(candiateIndexNumber);
                    }
                }
                //Seeing if there is any hidden column singles, if there is input them into the grid and return as true; 
                hiddenColumnSinglesCount += HiddenSinglesGeneric("column", indexValuesOfColumn, 0, columnNumber, 0);
                indexValuesOfColumn.Clear();
            }
            if (hiddenColumnSinglesCount >= 1)
            {
                hiddenColumnBool = true;
            }
            return hiddenColumnBool;
        }

        private bool HiddenBlockSingles()
        {
            List<int> listOfIndexCanidadtesForEachCellWithinTheBlock = new List<int>();
            bool hiddenBlockBool = false;
            int hiddenBlockSinglesCount = 0;

            for (int blockNumber = 0; blockNumber <= currentPuzzleToBeSolved.gridsize - 1; blockNumber++)
            {
                for (int indexNumberOfCell = 0; indexNumberOfCell <= currentPuzzleToBeSolved.puzzlecells.Count - 1; indexNumberOfCell++)
                {
                    if (currentPuzzleToBeSolved.puzzlecells[indexNumberOfCell].blocknumber == blockNumber)
                    {
                        listOfIndexCanidadtesForEachCellWithinTheBlock.Add(indexNumberOfCell);
                    }
                }
                hiddenBlockSinglesCount += HiddenSinglesGeneric("block", listOfIndexCanidadtesForEachCellWithinTheBlock, 0, 0, blockNumber);
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
            List<int> listOfCanidadtesForEachCellWithinTheRow = new List<int>();
            int _rowNumber = 0;
            bool hiddenRowBool = false;
            int hiddenRowCount = 0;

            for (int indexRowNumber = 0; indexRowNumber <= currentPuzzleToBeSolved.gridsize - 1; indexRowNumber++)
            {
                for (int cellIndexNumber = 0; cellIndexNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexNumber++)
                {
                    if (indexRowNumber == currentPuzzleToBeSolved.puzzlecells[cellIndexNumber].rownumber)
                    {
                        listOfCanidadtesForEachCellWithinTheRow.Add(cellIndexNumber);
                    }
                }
                hiddenRowCount += HiddenSinglesGeneric("row", listOfCanidadtesForEachCellWithinTheRow, _rowNumber, 0, 0);
            }
            //There where hidden singles. 
            if (hiddenRowCount >= 1)
            {
                hiddenRowBool = true;
            }
            return hiddenRowBool;
        }

        //May need to implement this method at somepoint. 
        private int HiddenSinglesGeneric(string region, List<int> indexListOfCells, int rowNumber, int columnNumber, int blockNumber)
        {
            List<int> notNullIndexList = new List<int>();
            List<int> individualNumbers = new List<int>();
            List<int> valuesUsed = new List<int>();
            int hiddenCount = 0;
            //Removing all null values from the candidate lists in the column. 
            for (int indexValue = 0; indexValue <= indexListOfCells.Count - 1; indexValue++)
            {
                if (candidatesList[indexListOfCells[indexValue]] != null)
                {
                    notNullIndexList.Add(indexListOfCells[indexValue]);
                }
            }

            //Going through all of the cells witin  the region 
            for (int firstIndexNumberOfCandidateCell = 0; firstIndexNumberOfCandidateCell <= notNullIndexList.Count - 1; firstIndexNumberOfCandidateCell++)
            {
                //Going through all of the candidates in that cell. 
                foreach (var candidateValue in candidatesList[notNullIndexList[firstIndexNumberOfCandidateCell]])
                {
                    //if the indivdual numbers list already contains 
                    if (individualNumbers.Contains(candidateValue))
                    {
                        //Remove that value from the indivdual numbers list. 
                        for (int valueToRemove = 0; valueToRemove <= individualNumbers.Count - 1; valueToRemove++)
                        {
                            if (individualNumbers[valueToRemove] == candidateValue)
                            {
                                individualNumbers.RemoveAt(valueToRemove);
                                valuesUsed.Add(candidateValue); //Making sure it is not added back to the list. 
                            }
                        }
                    }
                    else //If the list does not contain the number. 
                    {
                        bool valuesUsedBool = false; //Determines whether the number that is not in the list has been used before. 
                        foreach (var alreadyUsed in valuesUsed)
                        {
                            if (candidateValue == alreadyUsed)
                            {
                                valuesUsedBool = true;
                            }
                        }
                        //If the candidate has not be used before add it to the list. 
                        if (valuesUsedBool == false)
                        {
                            individualNumbers.Add(candidateValue);
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
                foreach (var indivdualNumber in individualNumbers)
                {
                    for (int indexValueOfCellsInRegion = 0; indexValueOfCellsInRegion <= notNullIndexList.Count - 1; indexValueOfCellsInRegion++)
                    {
                        //Going through all of the candidates in that cell. 
                        foreach (var candidateValue in candidatesList[notNullIndexList[indexValueOfCellsInRegion]])
                        {
                            if (indivdualNumber == candidateValue)
                            {

                                candidatesList[notNullIndexList[indexValueOfCellsInRegion]] = null;
                                currentPuzzleToBeSolved.puzzlecells[notNullIndexList[indexValueOfCellsInRegion]].value = indivdualNumber;
                                notNullIndexList.RemoveAt(indexValueOfCellsInRegion);
                                hiddenCount++;
                                break;
                            }
                        }
                    }
                }
            }
            indexListOfCells.Clear(); //Clearning the ready for the new list to be inserted and that to be handled. 
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
                doublesCount++;
                SolveSudokuRuleBasedXML();
            }
        }

        private bool NakedDoubleRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            bool nakedDoubleBool = false;
            int nakedDoubleRowCount = 0;

            for (int rowValueNumber = 0; rowValueNumber <= currentPuzzleToBeSolved.gridsize - 1; rowValueNumber++)
            {
                for (int cellIndexNumberCount = 0; cellIndexNumberCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexNumberCount++)
                {
                    if (rowValueNumber == currentPuzzleToBeSolved.puzzlecells[cellIndexNumberCount].rownumber)
                    {
                        cadidatesInSingleRow.Add(candidatesList[cellIndexNumberCount]);
                    }
                }
                nakedDoubleRowCount += GetNakedDoubles(cadidatesInSingleRow, rowValueNumber, 0, 0, "row");
                cadidatesInSingleRow.Clear();
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
            for (int _columnNumber = 0; _columnNumber <= 8; _columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; candiateIndexNumber++)
                {
                    if (_columnNumber == currentPuzzleToBeSolved.puzzlecells[candiateIndexNumber].columnnumber)
                    {
                        cadidatesInSingleColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                nakedDoubleColumnCount += GetNakedDoubles(cadidatesInSingleColumn, 0, _columnNumber, 0, "column");
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

            for (int blockNumberValue = 0; blockNumberValue <= currentPuzzleToBeSolved.gridsize - 1; blockNumberValue++)
            {
                for (int cellIndexCount = 0; cellIndexCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexCount++)
                {
                    if (blockNumberValue == currentPuzzleToBeSolved.puzzlecells[cellIndexCount].blocknumber)
                    {
                        listOfCanidadtesForEachCellWithinTheBlock.Add(candidatesList[cellIndexCount]);
                    }
                }
                nakedDoubleBlockCount += GetNakedDoubles(listOfCanidadtesForEachCellWithinTheBlock, blockNumberValue, 0, 0, "block");
                listOfCanidadtesForEachCellWithinTheBlock.Clear();
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
                                            candidatesList[rowNumber * 9 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        else if (regionTitle == "column")
                                        {
                                            nakedDoubleCount++;
                                            candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        //If the region is a block, then further handling. 
                                        else
                                        {
                                            int blockNumberCount = 0;
                                            for (int cellIndexFindBlockValueCount = 0; cellIndexFindBlockValueCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexFindBlockValueCount++)
                                            {
                                                if (rowNumber == currentPuzzleToBeSolved.puzzlecells[cellIndexFindBlockValueCount].blocknumber)
                                                {
                                                    if (blockNumberCount == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                                    {
                                                        candidatesList[cellIndexFindBlockValueCount] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                                    }
                                                    blockNumberCount++;
                                                }
                                            }
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
                doublesCount++;
                SolveSudokuRuleBasedXML();
            }
        }

        private bool HiddenDoublesRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            bool hiddenDoubleRowBool = false;
            int hiddenDoubleRowCount = 0;

            for (int rowNumberValue = 0; rowNumberValue <= currentPuzzleToBeSolved.gridsize - 1; rowNumberValue++)
            {
                for (int indexNumberOfCell = 0; indexNumberOfCell <= currentPuzzleToBeSolved.puzzlecells.Count - 1; indexNumberOfCell++)
                {
                    if (rowNumberValue == currentPuzzleToBeSolved.puzzlecells[indexNumberOfCell].rownumber)
                    {
                        cadidatesInSingleRow.Add(candidatesList[indexNumberOfCell]);
                    }
                }
                hiddenDoubleRowCount += GetHiddenDoubles(cadidatesInSingleRow, rowNumberValue, 0, "row");
                cadidatesInSingleRow.Clear();
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
            for (int columnNumber = 0; columnNumber <= currentPuzzleToBeSolved.gridsize - 1; columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; candiateIndexNumber++)
                {
                    if (columnNumber == currentPuzzleToBeSolved.puzzlecells[candiateIndexNumber].columnnumber)
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
            for (int blockNumberValue = 0; blockNumberValue <= currentPuzzleToBeSolved.gridsize - 1; blockNumberValue++)
            {
                for (int cellIndexNumber = 0; cellIndexNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexNumber++)
                {
                    if (blockNumberValue == currentPuzzleToBeSolved.puzzlecells[cellIndexNumber].blocknumber)
                    {
                        listOfCanidadtesForEachCellWithinTheBlock.Add(candidatesList[cellIndexNumber]);
                    }
                }
                hiddenDoubleBlockCount += GetHiddenDoubles(listOfCanidadtesForEachCellWithinTheBlock, blockNumberValue, 0, "block");
                listOfCanidadtesForEachCellWithinTheBlock.Clear();
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
                    if (firstNumber == 3 && secondNumber == 7)
                    {

                    }
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
                                            candidatesList[rowNumber * 9 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        else if (regionTitle == "column")
                                        {
                                            hiddenDoubleCount++;
                                            candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                        }
                                        //If the region is a block, then further handling. 
                                        else
                                        {
                                            int blockNumberCount = 0;
                                            for (int cellIndexFindBlockValueCount = 0; cellIndexFindBlockValueCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexFindBlockValueCount++)
                                            {
                                                if (rowNumber == currentPuzzleToBeSolved.puzzlecells[cellIndexFindBlockValueCount].blocknumber)
                                                {
                                                    if (blockNumberCount == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                                    {
                                                        candidatesList[cellIndexFindBlockValueCount] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                                    }
                                                    blockNumberCount++;
                                                }
                                            }
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

        private void HiddenTriples()
        {
            //Need to create this method. 
        }

        private void HiddenTriplesRow()
        {
            //Getting all of the naked doubles for that row. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();

            for (int rowNumberValue = 0; rowNumberValue <= currentPuzzleToBeSolved.gridsize - 1; rowNumberValue++)
            {
                for (int cellIndexValue = 0; cellIndexValue <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexValue++)
                {
                    if (rowNumberValue == currentPuzzleToBeSolved.puzzlecells[cellIndexValue].rownumber)
                    {
                        cadidatesInSingleRow.Add(candidatesList[cellIndexValue]);
                    }
                }
                GetHiddenTriples(cadidatesInSingleRow, rowNumberValue, 0, "row");
                cadidatesInSingleRow.Clear();
            }
        }

        private void HiddenTriplesColumn()
        {
            List<List<int>> cadidatesInSingleColumn = new List<List<int>>();
            //Search through all of the columns. 
            for (int _columnNumber = 0; _columnNumber <= currentPuzzleToBeSolved.gridsize - 1; _columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; candiateIndexNumber++)
                {
                    if (_columnNumber == currentPuzzleToBeSolved.puzzlecells[candiateIndexNumber].columnnumber)
                    {
                        cadidatesInSingleColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                GetHiddenTriples(cadidatesInSingleColumn, 0, _columnNumber, "column");
                cadidatesInSingleColumn.Clear();
            }
        }

        private void HiddenTriplesBlock()
        {
            List<List<int>> listOfCanidadtesForEachCellWithinTheBlock = new List<List<int>>();
            //Gets all the values from each block. 

            for (int blockNumberValue = 0; blockNumberValue <= currentPuzzleToBeSolved.gridsize - 1; blockNumberValue++)
            {
                for (int cellNumberIndexCount = 0; cellNumberIndexCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellNumberIndexCount++)
                {
                    if (blockNumberValue == currentPuzzleToBeSolved.puzzlecells[cellNumberIndexCount].blocknumber)
                    {
                        listOfCanidadtesForEachCellWithinTheBlock.Add(candidatesList[cellNumberIndexCount]);
                    }
                }
                GetHiddenTriples(listOfCanidadtesForEachCellWithinTheBlock, blockNumberValue, 0, "block");
                listOfCanidadtesForEachCellWithinTheBlock.Clear();
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
                                                candidatesList[rowNumber * 9 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            else if (regionTitle == "column")
                                            {
                                                candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            //If the region is a block, then further handling. 
                                            else
                                            {
                                                int blockNumberCount = 0;
                                                for (int cellIndexFindBlockValueCount = 0; cellIndexFindBlockValueCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexFindBlockValueCount++)
                                                {
                                                    if (rowNumber == currentPuzzleToBeSolved.puzzlecells[cellIndexFindBlockValueCount].blocknumber)
                                                    {
                                                        if (blockNumberCount == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                                        {
                                                            candidatesList[cellIndexFindBlockValueCount] = cells[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                                        }
                                                        blockNumberCount++;
                                                    }
                                                }
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
                triplesCount++;
                SolveSudokuRuleBasedXML();
            }
        }

        private bool NakedTriplesRow()
        {
            //Getting the naked triples for all of the rows wihtin the grid. 
            List<List<int>> cadidatesInSingleRow = new List<List<int>>();
            bool nakedTripleRowBool = false;
            int nakedTripleRowCount = 0;

            for (int rowNumberValue = 0; rowNumberValue <= currentPuzzleToBeSolved.gridsize - 1; rowNumberValue++)
            {
                for (int cellNumberIndexValue = 0; cellNumberIndexValue <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellNumberIndexValue++)
                {
                    if (rowNumberValue == currentPuzzleToBeSolved.puzzlecells[cellNumberIndexValue].rownumber)
                    {
                        cadidatesInSingleRow.Add(candidatesList[cellNumberIndexValue]);
                    }
                }
                nakedTripleRowCount += GetNakedTriples(cadidatesInSingleRow, rowNumberValue, 0, 0, "row");
                cadidatesInSingleRow.Clear();
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
            for (int _columnNumber = 0; _columnNumber <= currentPuzzleToBeSolved.gridsize - 1; _columnNumber++)
            {
                for (int candiateIndexNumber = 0; candiateIndexNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; candiateIndexNumber++)
                {
                    if (_columnNumber == currentPuzzleToBeSolved.puzzlecells[candiateIndexNumber].columnnumber)
                    {
                        cadidatesInSingleColumn.Add(candidatesList[candiateIndexNumber]);
                    }
                }
                nakedTripleColumnCount += GetNakedTriples(cadidatesInSingleColumn, 0, _columnNumber, 0, "column");
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

            for (int blockNumberValue = 0; blockNumberValue <= currentPuzzleToBeSolved.gridsize - 1; blockNumberValue++)
            {
                for (int cellNumberIndexValue = 0; cellNumberIndexValue <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellNumberIndexValue++)
                {
                    if (blockNumberValue == currentPuzzleToBeSolved.puzzlecells[cellNumberIndexValue].blocknumber)
                    {
                        listOfCanidadtesForEachCellWithinTheBlock.Add(candidatesList[cellNumberIndexValue]);
                    }
                }
                nakedTripleBlockCount += GetNakedTriples(listOfCanidadtesForEachCellWithinTheBlock, blockNumberValue, 0, 0, "block");
                listOfCanidadtesForEachCellWithinTheBlock.Clear();
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
                                //If the cell is not contained within te match list remove the correct values from this cell. 
                                if (tripleCell == false)
                                {
                                    //Goinng through all of the candidate within this cell, not in the triple. 
                                    for (int candidateValueIndex = 0; candidateValueIndex <= cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]].Count - 1; candidateValueIndex++)
                                    {
                                        if (cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == firstNumber || cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == secondNumber || cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]][candidateValueIndex] == thirdNumber)
                                        {
                                            cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]].RemoveAt(candidateValueIndex);
                                            if (regionTitle == "row")
                                            {
                                                nakedTripleCount++;
                                                candidatesList[rowNumber * 9 + notNullIndexValuesCellsInRow[candidatesIndexValues]] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            else if (regionTitle == "column")
                                            {
                                                nakedTripleCount++;
                                                candidatesList[notNullIndexValuesCellsInRow[candidatesIndexValues] * 9 + columnNumber] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                            }
                                            else
                                            {
                                                int blockNumberCount = 0;
                                                for (int cellIndexFindBlockValueCount = 0; cellIndexFindBlockValueCount <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellIndexFindBlockValueCount++)
                                                {
                                                    if (rowNumber == currentPuzzleToBeSolved.puzzlecells[cellIndexFindBlockValueCount].blocknumber)
                                                    {
                                                        if (blockNumberCount == notNullIndexValuesCellsInRow[candidatesIndexValues])
                                                        {
                                                            candidatesList[cellIndexFindBlockValueCount] = cadidatesInSingleRow[notNullIndexValuesCellsInRow[candidatesIndexValues]];
                                                        }
                                                        blockNumberCount++;
                                                    }
                                                }
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

        //This method also now solves irregular puzzles. Also seems to be generic for differnet grid sizes. 
        #region More Effcient Backtracking method for the xml class file example
        public bool BacktrackingUsingXmlTemplateFile(bool generating)
        {
            //If valid numbers loist count is less than the none empty cells then return false, as invalid solution. 
            logicalOrderOfCellsXml.Clear();

            int emptyCellListCount = 0;
            for (int checkEmptyCellValue = 0; checkEmptyCellValue <= currentPuzzleToBeSolved.puzzlecells.Count - 1; checkEmptyCellValue++)
            {
                if (currentPuzzleToBeSolved.puzzlecells[checkEmptyCellValue].value == 0)
                {
                    emptyCellListCount++;
                }
            }

            //Starting the timer
            stopWatch.Reset();
            stopWatch.Start();
            //setting the starting candidate number value. 
            candidateTotalNumber = 1;

            int startingValue = 0;
            while (candidateTotalNumber <= currentPuzzleToBeSolved.gridsize)
            {
                for (int currentCellBeingHandled = startingValue; currentCellBeingHandled <= currentPuzzleToBeSolved.puzzlecells.Count - 1; currentCellBeingHandled++)
                {
                    puzzleCellCurrentlyBeingHandled = currentPuzzleToBeSolved.puzzlecells[currentCellBeingHandled];

                    if (puzzleCellCurrentlyBeingHandled.value == 0)
                    {
                        GetValuesForRowXmlPuzzleTemplate();
                        GetValuesForColumnXmlPuzzleTemplate();
                        GetValuesForBlockXmlPuzzleTemplate();
                        GetValidNumbers();
                        validNumbersInBlock.Clear();
                        validNumbersInColumn.Clear();
                        validNUmbersInRow.Clear();

                        if (validNumbersInCell.Count == candidateTotalNumber)
                        {
                            logicalOrderOfCellsXml.Add(currentCellBeingHandled);
                            validNumbersInCell.Clear();
                        }
                        validNumbersInCell.Clear();
                    }
                }
                candidateTotalNumber++;
            }

            ////Order cells based on blocks 
            //for(int blockNumberTemp =0;blockNumberTemp<=currentPuzzleToBeSolved.gridsize-1;blockNumberTemp++)
            //{
            //    for (int cellReferenceNumber=0;cellReferenceNumber<=currentPuzzleToBeSolved.puzzlecells.Count-1;cellReferenceNumber++)
            //    {
            //        if(currentPuzzleToBeSolved.puzzlecells[cellReferenceNumber].blocknumber ==blockNumberTemp)
            //        {
            //            logicalOrderOfCellsXml.Add(cellReferenceNumber);
            //        }

            //    }
            //}

            //Resetting the starting value, so it cycles through all of the cells. 
            startingValue = 0;
            numberOfCellToBeHandled = 0;

            if (emptyCellListCount > logicalOrderOfCellsXml.Count && generating == false)
            {
                return false;
            }

            for (startingValue = numberOfCellToBeHandled; startingValue <= logicalOrderOfCellsXml.Count - 1; startingValue++)
            {

                if (stopWatch.Elapsed.Seconds >= 2)
                {
                    logicalOrderOfCellsXml.Clear();
                    return false;
                }
                numberOfCellToBeHandled = startingValue;
                puzzleCellCurrentlyBeingHandled = currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[startingValue]];

                GetValuesForRowXmlPuzzleTemplate();
                GetValuesForColumnXmlPuzzleTemplate();
                GetValuesForBlockXmlPuzzleTemplate();
                GetValidNumbers();
                validNumbersInBlock.Clear();
                validNumbersInColumn.Clear();
                validNUmbersInRow.Clear();

                if (validNumbersInCell.Count == 0)
                {
                    if(startingValue ==0)
                    {
                        return false;
                    }
                    backtrackingNodesCount++;
                    previousNumberInCell = currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[startingValue - 1]].value;
                    currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[numberOfCellToBeHandled - 1]].value = 0;
                    startingValue -= 2;
                }
                else
                {
                    if (previousNumberInCell == 0)
                    {
                        if (generating == false)
                        {
                            currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[numberOfCellToBeHandled]].value = validNumbersInCell[0];
                        }
                        //Random number generation for creating a puzzle. 
                        else
                        {
                            int randomNumber = randomNumberGenerator.Next(validNumbersInCell.Count);
                            currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[numberOfCellToBeHandled]].value = validNumbersInCell[randomNumber];
                        }
                    }
                    else
                    {
                        //Need to back track further. 
                        if (previousNumberInCell == validNumbersInCell[validNumbersInCell.Count - 1])
                        {
                            if (startingValue == 0)
                            {
                                logicalOrderOfCellsXml.Clear();
                                return false;
                            }
                            backtrackingNodesCount++;
                            previousNumberInCell = currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[startingValue - 1]].value;
                            currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[numberOfCellToBeHandled - 1]].value = 0;
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
                                            currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[numberOfCellToBeHandled]].value = validNumbersInCell[counter];
                                            previousNumberInCell = 0;
                                            break;
                                        }
                                    }

                                }
                                else
                                {
                                    currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[numberOfCellToBeHandled]].value = validNumbersInCell[correctNumber];
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
                                        currentPuzzleToBeSolved.puzzlecells[logicalOrderOfCellsXml[numberOfCellToBeHandled]].value = validNumbersInCell[counter];

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
            //If the puzzle has been solved. 
            cellNumbersForLogicalEffcientOrder.Clear();
            stopWatch.Stop();
            return true;
        }
        #endregion

        #region Check Unique Solution

        public string  CheckUniqueSolution()
        {
            int[] solvedPuzzlesOriginalValue = new int[currentPuzzleToBeSolved.puzzlecells.Count];
            for (int solvedPuzzleValues=0;solvedPuzzleValues<= currentPuzzleToBeSolved.puzzlecells.Count-1;solvedPuzzleValues++ )
            {
                solvedPuzzlesOriginalValue[solvedPuzzleValues] = currentPuzzleToBeSolved.puzzlecells[solvedPuzzleValues].value;
            }
            //Finding the cell to induce further backtracking 
            for (int reverseCellCount = currentPuzzleToBeSolved.puzzlecells.Count - 1; reverseCellCount >= 0; reverseCellCount--)
            {   
                //Get a vlaid cell, that a value has been removed from, the last number that has had a value removed, 
                bool isRemovedCell = false;
                for (int removeCellIndex = 0; removeCellIndex <= listOfCellsToBeRemoved.Count - 1; removeCellIndex++)
                {
                    if (reverseCellCount == listOfCellsToBeRemoved[removeCellIndex])
                    {
                        isRemovedCell = true;
                    }
                }
                if (isRemovedCell == false) //If there has been no value removed from this cell then skip the iteration. 
                {
                    continue;
                }
                //Setting prervious number, so can not be used. 
                int previousNumberReverse = currentPuzzleToBeSolved.puzzlecells[reverseCellCount].value;
                currentPuzzleToBeSolved.puzzlecells[reverseCellCount].value = 0;
                //Getting valid numebrs for that cell. 
                List<int> validNumbersInRow = CheckValidNumbersForRegions.GetValuesForRowXmlPuzzleTemplate(currentPuzzleToBeSolved, currentPuzzleToBeSolved.puzzlecells[reverseCellCount]);
                List<int> validNumbersInColumn = CheckValidNumbersForRegions.GetValuesForColumnXmlPuzzleTemplate(currentPuzzleToBeSolved, currentPuzzleToBeSolved.puzzlecells[reverseCellCount]);
                List<int> validNumbersInBlock = CheckValidNumbersForRegions.GetValuesForBlockXmlPuzzleTemplate(currentPuzzleToBeSolved, currentPuzzleToBeSolved.puzzlecells[reverseCellCount]);
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
                                currentPuzzleToBeSolved.puzzlecells[reverseCellCount].value = validNumbers[validNumberIndexNumber];
                                
                               bool solvedSecond =BacktrackingUsingXmlTemplateFile(false);
                                for (int resetCellValueIndex = 0; resetCellValueIndex <= currentPuzzleToBeSolved.puzzlecells.Count - 1; resetCellValueIndex++)
                                {
                                    currentPuzzleToBeSolved.puzzlecells[resetCellValueIndex].value = solvedPuzzlesOriginalValue[resetCellValueIndex];
                                }
                                //there is another solution. Therefore another value needs to be removed. 
                                if (solvedSecond == true)
                                {
                                    return "Not unique";
                                }
                                //If there is no other solution, then it is a unique solution. 
                                else
                                {
                                   return "Unique";
                                }
                            }
                        }
                    }
                }
            }
            return "Unique";
        }

        #endregion

        #region Neighbourhood Operator

        public void NeighBourHoodOperatoralgorithm()
        {
            List<int> cellIndexes = new List<int>();
            //Entering the numbers, ensuring the block constraint is met. 
            for (int cellNumber = 0; cellNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; cellNumber++)
            {
                if (currentPuzzleToBeSolved.puzzlecells[cellNumber].value == 0)
                {
                    puzzleCellCurrentlyBeingHandled = currentPuzzleToBeSolved.puzzlecells[cellNumber];
                    GetValuesForBlockXmlPuzzleTemplate();
                    currentPuzzleToBeSolved.puzzlecells[cellNumber].value = validNumbersInBlock[0];
                    cellIndexes.Add(cellNumber);
                }
            }
            Random randomBlockNumber = new Random();
            int numberOfErros = GetPuzzleErrors(); 

            while (numberOfErros != 0)
            {
                int tempNumberOfErrors = numberOfErros;
                //Apply operator 
                
                List<int> blockValues = new List<int>();

                while (blockValues.Count <= 1)
                {
                    int blockNumber = randomBlockNumber.Next(0, 8);
                    for (int nonStaticCellNumber = 0; nonStaticCellNumber <= cellIndexes.Count - 1; nonStaticCellNumber++)
                    {
                        if (currentPuzzleToBeSolved.puzzlecells[cellIndexes[ nonStaticCellNumber]].blocknumber == blockNumber)
                        {
                            blockValues.Add(nonStaticCellNumber);
                        }
                    }
                }

                //Making sure they are different values 
                int firstRandom = randomNumberGenerator.Next(0, blockValues.Count);
                int secondRandom = randomNumberGenerator.Next(0, blockValues.Count);
                while (firstRandom == secondRandom)
                {
                    firstRandom = randomNumberGenerator.Next(0, blockValues.Count);
                    secondRandom = randomNumberGenerator.Next(0, blockValues.Count);
                }
                //Swap values
                int firstValue = currentPuzzleToBeSolved.puzzlecells[blockValues[firstRandom]].value;
                int secondValue = currentPuzzleToBeSolved.puzzlecells[blockValues[secondRandom]].value;
                currentPuzzleToBeSolved.puzzlecells[blockValues[firstRandom]].value = secondValue;
                currentPuzzleToBeSolved.puzzlecells[blockValues[secondRandom]].value = firstValue;

               //Re-assessing the errors count and then commiting change if correct. 
                numberOfErros = GetPuzzleErrors();

                if(numberOfErros >tempNumberOfErrors)
                {
                    numberOfErros = tempNumberOfErrors;
                    //Undo change and do next neighbourhood operator
                    currentPuzzleToBeSolved.puzzlecells[blockValues[firstRandom]].value = firstValue;
                    currentPuzzleToBeSolved.puzzlecells[blockValues[secondRandom]].value = secondValue;
                }
                else if(numberOfErros < tempNumberOfErrors)
                {
                    Console.WriteLine("Better");
                }
            }
        }

        /// <summary>
        /// Method to get the erros count of the current puzzle. 
        /// </summary>
        /// <returns></returns>
        private int GetPuzzleErrors()
        {
            int errorCount = 0;
            errorCount += GetRowErrors();
            errorCount += GetColumnErrors();
            return errorCount;
        }

        private int GetRowErrors()
        {
            int errorCount = 0;
            //Puzzle is now filled with values but there may be errors. 
            for (int tempRowNumber = 0; tempRowNumber <= currentPuzzleToBeSolved.gridsize - 1; tempRowNumber++)
            {
                List<int> rowNumberValues = new List<int>();
                for (int getColumnNumber = 0; getColumnNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; getColumnNumber++)
                {
                    //Adding all the values in the column. 
                    if (currentPuzzleToBeSolved.puzzlecells[getColumnNumber].rownumber == tempRowNumber)
                    {
                        rowNumberValues.Add(currentPuzzleToBeSolved.puzzlecells[getColumnNumber].value);
                    }
                }

                //Lookin for the errors within the row
                List<int> rowCount = new List<int>();
                //Getting the occurences of each 
                for (int value = 1; value <= currentPuzzleToBeSolved.gridsize; value++)
                {
                    int numberCountForValue = 0;
                    for (int rowListValueTemp = 0; rowListValueTemp <= rowNumberValues.Count - 1; rowListValueTemp++)
                    {
                        if (value == rowNumberValues[rowListValueTemp])
                        {
                            numberCountForValue++;
                        }
                    }
                    rowCount.Add(numberCountForValue);
                }
                int correctOccurence = 0;
                //If the number count if more than 1, add an error. 
                for (int rowErrorCountValue = 0; rowErrorCountValue <= rowCount.Count - 1; rowErrorCountValue++)
                {
                    if (rowCount[rowErrorCountValue] == 1)
                    {
                        correctOccurence++;
                    }
                }
                //Setting then number of errors for that row. 
                errorCount += (currentPuzzleToBeSolved.gridsize - correctOccurence);

            }
            return errorCount;
        }

        private int GetColumnErrors()
        {
            int errorCount = 0;
            //Puzzle is now filled with values but there may be errors. 
            for (int tempColumnNumber = 0; tempColumnNumber <= currentPuzzleToBeSolved.gridsize - 1; tempColumnNumber++)
            {
                List<int> columnNumberValues = new List<int>();
                for (int getRowNumber = 0; getRowNumber <= currentPuzzleToBeSolved.puzzlecells.Count - 1; getRowNumber++)
                {
                    //Adding all the values in the column. 
                   if(currentPuzzleToBeSolved.puzzlecells[getRowNumber].columnnumber == tempColumnNumber)
                    {
                        columnNumberValues.Add(currentPuzzleToBeSolved.puzzlecells[getRowNumber].value);
                    }
                }

                //Lookin for the errors within the row
                List<int>columnCount = new List<int>();
                //Getting the occurences of each 
                for (int value = 1; value <= currentPuzzleToBeSolved.gridsize; value++)
                {
                    int numberCountForValue = 0;
                    for (int rowListValueTemp = 0; rowListValueTemp <= columnNumberValues.Count - 1; rowListValueTemp++)
                    {
                        if (value == columnNumberValues[rowListValueTemp])
                        {
                            numberCountForValue++;
                        }
                    }
                    columnCount.Add(numberCountForValue);
                }
                int correctOccurence = 0;
                //If the number count if more than 1, add an error. 
                for (int rowErrorCountValue = 0; rowErrorCountValue <= columnCount.Count - 1; rowErrorCountValue++)
                {
                    if (columnCount[rowErrorCountValue] == 1)
                    {
                        correctOccurence++;
                    }
                }
                //Setting then number of errors for that row. 
                errorCount += (currentPuzzleToBeSolved.gridsize - correctOccurence);

            }
            return errorCount;
        }

        #endregion

        #region Methods for checking rows columns and blocks using the XML generated class

        //Methods for getting the values using the xml file. 
        private void GetValuesForRowXmlPuzzleTemplate()
        {
            List<int> numbersInRow = new List<int>();
            List<int> nonValidNumberInRow = new List<int>();

            foreach (var cell in currentPuzzleToBeSolved.puzzlecells)
            {
                if (cell.rownumber == puzzleCellCurrentlyBeingHandled.rownumber)
                {
                    numbersInRow.Add(cell.value);
                }
            }

            foreach (var valueInCell in numbersInRow)
            {
                if (valueInCell != 0)
                {
                    nonValidNumberInRow.Add(valueInCell);
                }
            }

            validNUmbersInRow = GetValidNumbersXml(nonValidNumberInRow);

        }

        private void GetValuesForColumnXmlPuzzleTemplate()
        {
            List<int> numbersInColumn = new List<int>();
            List<int> nonValidNumberInColumn = new List<int>();

            foreach (var cell in currentPuzzleToBeSolved.puzzlecells)
            {
                if (cell.columnnumber == puzzleCellCurrentlyBeingHandled.columnnumber)
                {
                    numbersInColumn.Add(cell.value);
                }
            }

            foreach (var valueInCell in numbersInColumn)
            {
                if (valueInCell != 0)
                {
                    nonValidNumberInColumn.Add(valueInCell);
                }
            }

            validNumbersInColumn = GetValidNumbersXml(nonValidNumberInColumn);
        }

        private void GetValuesForBlockXmlPuzzleTemplate()
        {
            List<int> numbersInBlock = new List<int>();
            List<int> nonValidNumberInBlock = new List<int>();

            foreach (var cell in currentPuzzleToBeSolved.puzzlecells)
            {
                if (cell.blocknumber == puzzleCellCurrentlyBeingHandled.blocknumber)
                {
                    numbersInBlock.Add(cell.value);
                }
            }

            foreach (var valueInCell in numbersInBlock)
            {
                if (valueInCell != 0)
                {
                    nonValidNumberInBlock.Add(valueInCell);
                }
            }

            validNumbersInBlock = GetValidNumbersXml(nonValidNumberInBlock);
        }

        private List<int> GetValidNumbersXml(List<int> nonValidNumbers)
        {
            List<int> validNumbers = new List<int>();
            for (int y = 1; y <= currentPuzzleToBeSolved.gridsize; y++)
            {
                if (nonValidNumbers.Contains(y) == false)
                {
                    validNumbers.Add(y);
                }
            }

            return validNumbers;
        }

        #endregion

        #region Methods to check valid values 

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

        #region Evaluate Difficulty

        public string EvaluatePuzzleDifficulty()
        {
            if (backtrackingNodesCount > 0)
            {

            }

            difficluty = "";
            //Includes human model, also get the string from the Human model. 
            Stopwatch tempStopWatch = new Stopwatch();
            tempStopWatch.Reset();
            tempStopWatch.Start();
            bool rule = SolveSudokuRuleBasedXML();
            string uniqueString = CheckUniqueSolution();
            /*
            Console.WriteLine(tempStopWatch.Elapsed.TotalSeconds);
            Console.WriteLine(tempStopWatch.Elapsed.TotalMilliseconds);
            Console.WriteLine(numberOfStaticNumbers);
            Console.WriteLine(totalNumberOfCandidates);

            Console.WriteLine("" + singlesCount + " " + hiddenCount + " " + doublesCount + " " + triplesCount);
            */
            if (currentPuzzleToBeSolved.gridsize != 4)
            {
                executionTimeDifficulty = EvaluateExecutionTime(tempStopWatch.Elapsed.TotalSeconds);
                tempStopWatch.Stop();
                totalNumberOfCandidatesDifficulty = EvaluateTotalNumberOfCandidatesDifficulty(totalNumberOfCandidates);
                numberOfStaticNumberDifficulty = EvaluateNumberOfStaticNumbers(numberOfStaticNumbers);
                humanSolvingDifficulty = CalculateHumanDifficultyValue();
                Console.WriteLine(humanSolvingDifficulty);
                if (backtrackingBool)
                {
                    Console.WriteLine("Backtracking used.");
                }
                FinalDifficulty();

                humanSolvingDifficulty = 0;
                executionTimeDifficulty = 0;
                totalNumberOfCandidates = 0;
                numberOfStaticNumberDifficulty = 0;
                numberOfStaticNumbers = 0;
                methodRunNumber = 0;
                singlesCount = 0;
                hiddenCount = 0;
                doublesCount = 0;
                triplesCount = 0;
            }
            else
            {
                difficluty = "Easy";
            }
            return uniqueString;
        }

        /// <summary>
        /// Method that evaluates the execution time of the puzzle that has been generated, this will determine the difficulty of the puzzle. 
        /// </summary>
        /// <returns></returns>
        private int EvaluateExecutionTime(double time)
        {
            //Change to return int, to get the time if the solving time. 
            if (time <= 0.015)
            {
                return 0;
            }
            else if (time > 0.015 && time <= 0.03)
            {
                return 1;
            }
            else if (time > 0.03 && time <= 0.055)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private int EvaluateTotalNumberOfCandidatesDifficulty(int totalNumber)
        {
            if (totalNumber >= 0 && totalNumber <= 150)
            {
                return 0;
            }
            else if (totalNumber > 150 && totalNumber <= 180)
            {
                return 1;
            }
            else if (totalNumber > 180 && totalNumber <= 197)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private int EvaluateNumberOfStaticNumbers(int totalNumber)
        {
            if (totalNumber >= 31)
            {
                return 0;
            }
            else if (totalNumber < 31 && totalNumber >= 28)
            {
                return 1;
            }
            else if (totalNumber > 28 && totalNumber <= 26)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private int CalculateHumanDifficultyValue()
        {
            //Getting the human difficulty from the number of occurrences of singles, doubles and triples. 
            decimal totaloccurences = singlesCount + +hiddenCount+ doublesCount + triplesCount;
            decimal totalValue = (singlesCount * 1) + (hiddenCount*3)+(doublesCount * 9) + (triplesCount * 27);
            decimal tempDifficulty = totalValue / totaloccurences;
            decimal roundedValue = Math.Round(tempDifficulty, 0);
            //Converting to int
            int difficultyResult = Convert.ToInt32(roundedValue);
            //Retunring the result in terms of the difficlity metrics. 
            return difficultyResult - 1;
        }

        private void FinalDifficulty()
        {
            double totals = (humanSolvingDifficulty * 2) + totalNumberOfCandidatesDifficulty + executionTimeDifficulty + numberOfStaticNumberDifficulty;

            totals += 4;
            double difficlutyRating = totals / 5;

            if (difficlutyRating <= 1.5)
            {
                difficluty = "Easy";
            }
            else if (difficlutyRating > 1.5 && difficlutyRating <= 2.5)
            {
                difficluty = "Medium";
            }
            else if (difficlutyRating > 2.5 && difficlutyRating <= 3.5)
            {
                difficluty = "Hard";
            }
            else
            {
                difficluty = "Extreme";
            }
        }
        #endregion 
    }
}
