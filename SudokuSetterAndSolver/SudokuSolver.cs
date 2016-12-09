using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    //For some reason, the code carries on going down the code after backtracking has occured, this is incorrect funcitonality and needs to be sorted. 
    //Is the candidate list being transferred to the backtracking method. Does this really matter, as its only the human models that need to know this. 
    //The rule based algorithm may need a lot more work to make compatible with the new xml files. 
    public class SudokuSolver
    {
        #region Objects 
        //Puzzle manager, that handles the loading and wiritng to xml files for the game. 
        PuzzleManager puzzleManager = new PuzzleManager();
        //Details of the current 
        puzzle puzzleDetails;
        #endregion

        #region Global Variables 

        //sudoku mulit it used to input the hidden singles within the application. 
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

        List<int> logicalOrderOfCellsXml = new List<int>();
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
        //List that contains all of the candidates for each cell this should be used for candidate reference within the program. 
        List<List<int>> candidatesList = new List<List<int>>();
        //counts the number of times the rules based algorithm has been exectuted. 
        int methodRunNumber = 0;

        bool solvedBacktracking = false;
        public string difficluty;
        //Directory location of the file that is being solved. 
        string loadFileDirectoryLocation = "C:\\Users\\New\\Documents\\Sudoku\\Application\\SudokuSetterAndSolver\\SudokuSetterAndSolver\\Puzzles\\TestPuzzles\\test22.xml";

        //Get all the cells with the corrrect row , column and block number, this will then allow easier handling.
        public puzzle currentPuzzleToBeSolved = new puzzle();
        //THis will be the cell that is currently being handled by the solver. 
        puzzleCell puzzleCellCurrentlyBeingHandled = new puzzleCell();
        puzzle staticPuzzle = new puzzle();

        #endregion

        #region Main Method 


        public bool solvePuzzleXMl()
        {
            difficluty = "easy";
            //Generate the puzzle and then solve it. 
            GeneratePuzzleXML();
            //Solving the puzzle. 
            bool solved = SolveSudokuRuleBasedXML();
            return solved;
        }

        public bool solvePuzzleXML(string directoryLocation)
        {
            difficluty = "easy";
            loadFileDirectoryLocation = directoryLocation;
            //Generate the puzzle and then solve it. 
            GeneratePuzzleXML();
            //Solving the puzzle. 
            bool solved = SolveSudokuRuleBasedXML();
            return solved;
        }

        #endregion 

        #region General Methods 

        private void GeneratePuzzleXML()
        {
            //Loaing in a puzzle from a test file and creating the puzzle, along with static numbers. 
            currentPuzzleToBeSolved = puzzleManager.ReadFromXMlFile(loadFileDirectoryLocation);
        }

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
            difficluty = "easy";
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
                candidatesList = tempCandiateList;
            }
            else
            {
                CompareCandidateLists(tempCandiateList); //comparing the candidate list and the temp, to get the current values.
            }

            for (int indexOfCandidateValue = 0; indexOfCandidateValue <= candidatesList.Count - 1; indexOfCandidateValue++)
            {
                if (candidatesList[indexOfCandidateValue] != null)
                {
                    if (candidatesList[indexOfCandidateValue].Count == 1) //Naked singles 
                    {
                        nakedSinglesCount++;
                        foreach (int nakedValue in candidatesList[indexOfCandidateValue]) //Insert naked single. 
                        {
                            currentPuzzleToBeSolved.puzzlecells[indexOfCandidateValue].value = nakedValue;
                            candidatesList[indexOfCandidateValue] = null;
                        }
                    }
                }
            }
            methodRunNumber++;

            //If there were naked singles, then see if puzzle is solved, if not then recurse. 
            if (nakedSinglesCount != 0)
            {
                bool solved = CheckToSeeIfPuzzleSolvedXML();
                if (solved)
                {
                    return true;
                }
                else
                { SolveSudokuRuleBasedXML(); }

            }
            //Checks to see if puzzle is solved. 
            bool checkSolved = CheckToSeeIfPuzzleSolvedXML();
            if (checkSolved)
            {
                MessageBox.Show("Human Solving Methods Completed! Puzzle Completed. Difficulty: " + difficluty);
                return true;
            }
            //all the below methods seem to work togher and solve puzzles. 
            HiddenSingles();
            checkSolved = CheckToSeeIfPuzzleSolvedXML();
            if (checkSolved)
            {
                MessageBox.Show("Human Solving Methods Completed! Puzzle Completed. Difficulty: " + difficluty);
                return true;
            }
            difficluty = "medium";
            //CandidateHandling();
            difficluty = "veryhard";
            MessageBox.Show("Human Solving Methods Completed! Puzzle not completed. Diffiuclty: Very Hard. Backtracking will begin.");

            solvedBacktracking = BacktrackingUsingXmlTemplateFile(false);
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
            difficluty = "hard";
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
                SolveSudokuRuleBasedXML();
            }
            hiddenColumnBool = HiddenColumnSingles();
            if (hiddenColumnBool == true)
            {
                SolveSudokuRuleBasedXML();
            }
            hiddenBlockBool = HiddenBlockSingles();
            if (hiddenBlockBool == true)
            {
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
                for(int cellIndexNumber =0;cellIndexNumber<=currentPuzzleToBeSolved.puzzlecells.Count-1;cellIndexNumber++)
                {
                    if(indexRowNumber == currentPuzzleToBeSolved.puzzlecells[cellIndexNumber].rownumber)
                    {
                        listOfCanidadtesForEachCellWithinTheRow.Add(cellIndexNumber);
                    }
                }
                hiddenRowCount += HiddenSinglesGeneric("row", listOfCanidadtesForEachCellWithinTheRow, _rowNumber, 0,0);
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
                SolveSudokuRuleBasedXML();
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
                SolveSudokuRuleBasedXML();
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
                SolveSudokuRuleBasedXML();
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


            //Resetting the starting value, so it cycles through all of the cells. 
            startingValue = 0;
            numberOfCellToBeHandled = 0;

            if (emptyCellListCount > logicalOrderOfCellsXml.Count && generating == false)
            {
                return false;
            }

            for (startingValue = numberOfCellToBeHandled; startingValue <= logicalOrderOfCellsXml.Count - 1; startingValue++)
            {
                if (stopWatch.Elapsed.Seconds >= 5)
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
                //foreach (var printCell in currentPuzzleToBeSolved.puzzlecells)
                //{
                //    Console.Write(printCell.value);
                //}
                //Console.WriteLine();
                validNumbersInCell.Clear();
            }
            Console.WriteLine(stopWatch.Elapsed.TotalSeconds);
            cellNumbersForLogicalEffcientOrder.Clear();
            stopWatch.Stop();
            return true;
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

        #region Generic Method
        private int[,] ConvertListToMultiDimensionalArray(List<int> puzzleInList, int gridSize)
        {
            int[,] puzzleArray = new int[gridSize, gridSize];
            int rowNumber = 0;
            int columnNumber = 0;

            for (int cellNumber = 0; cellNumber <= puzzleInList.Count - 1; cellNumber++)
            {
                puzzleArray[rowNumber, columnNumber] = puzzleInList[cellNumber];
                if (columnNumber == 8 || columnNumber % 9 == 8)
                {
                    rowNumber++;
                    columnNumber = 0;
                }
                else
                {
                    columnNumber++;
                }
            }


            return puzzleArray;
        }

        #endregion

    }
}
