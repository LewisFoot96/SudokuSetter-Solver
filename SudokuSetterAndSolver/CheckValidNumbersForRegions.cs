using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    class CheckValidNumbersForRegions
    {
        #region Method 

        /// <summary>
        /// Method that gets all of the valid cell based upon the cells that are not avaliable. 
        /// </summary>
        /// <param name="nonValidNumbers"></param>
        /// <returns></returns>
        public static List<int> GetValidNumbers(List<int> nonValidNumbers, int puzzleLength)
        {
            List<int> validNumbers = new List<int>();
            //Get the valid number that can be within this row. These should be in number order. 
            for (int y = 1; y <= Math.Sqrt(puzzleLength) - 1; y++)
            {
                if (nonValidNumbers.Contains(y) == false)
                {
                    validNumbers.Add(y);
                }
            }
            return validNumbers;
        }

        /// <summary>
        /// Method to create a list of values that are within the region of the current cell being handled. 
        /// </summary>
        /// <param name="columnNumbers"></param>
        /// <param name="rowNumbers"></param>
        /// <param name="blockNumbers"></param>
        /// <returns></returns>
        public static List<int> GetValidNumbers(List<int> columnNumbers, List<int> rowNumbers, List<int> blockNumbers)
        {
            //Getting the numbers that are valid. 
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

       /// <summary>
       /// Method that gets all of the numbers within a specific row. 
       /// </summary>
       /// <param name="currentPuzzleToBeSolved">puzzle</param>
       /// <param name="puzzleCellCurrentlyBeingHandled">current cell</param>
       /// <returns></returns>
        public static  List<int> GetValuesForRowXmlPuzzleTemplate(puzzle currentPuzzleToBeSolved, puzzleCell puzzleCellCurrentlyBeingHandled)
        {
            List<int> numbersInRow = new List<int>();
            List<int> nonValidNumberInRow = new List<int>();
            List<int> validNumbersInRow = new List<int>();
            //Get row and value
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
            //Return non valid values. 
            return validNumbersInRow = GetValidNumbers(nonValidNumberInRow, currentPuzzleToBeSolved.puzzlecells.Count);
        }

        /// <summary>
        /// Method that gets the values in a common based on the cell that is being handled.
        /// </summary>
        /// <param name="currentPuzzleToBeSolved">puzzle</param>
        /// <param name="puzzleCellCurrentlyBeingHandled">current cell</param>
        /// <returns></returns>
        public static List<int> GetValuesForColumnXmlPuzzleTemplate(puzzle currentPuzzleToBeSolved, puzzleCell puzzleCellCurrentlyBeingHandled)
        {
            List<int> numbersInColumn = new List<int>();
            List<int> nonValidNumberInColumn = new List<int>();
            List<int> validNumbersInColumn = new List<int>();
            //Get column and values. 
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
            //return non valud values. 
            return validNumbersInColumn = GetValidNumbers(nonValidNumberInColumn, currentPuzzleToBeSolved.puzzlecells.Count);
        }

        /// <summary>
        /// Method that gets all the values in the block based on the current cell being handled. 
        /// </summary>
        /// <param name="currentPuzzleToBeSolved">puzzle</param>
        /// <param name="puzzleCellCurrentlyBeingHandled">current cell</param>
        /// <returns></returns>
        public static List<int> GetValuesForBlockXmlPuzzleTemplate(puzzle currentPuzzleToBeSolved, puzzleCell puzzleCellCurrentlyBeingHandled)
        {
            List<int> numbersInBlock = new List<int>();
            List<int> nonValidNumberInBlock = new List<int>();
            List<int> validNumbersInBlock = new List<int>();
            //Getting blocks and the number in the block.
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
            //Returning the numbers that are in the block, the ones that cannot be valid numbers within the cell that is currently being handled. 
            return validNumbersInBlock = GetValidNumbers(nonValidNumberInBlock, currentPuzzleToBeSolved.puzzlecells.Count);
        }
        #endregion
    }
}
