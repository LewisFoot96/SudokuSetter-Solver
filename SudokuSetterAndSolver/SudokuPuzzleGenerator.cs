using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    class SudokuPuzzleGenerator
    {
        int[,] sudokuGrid = new int[9, 9];
        int _gridSize = 0;
        PuzzleManager puzzleManager = new PuzzleManager();
        SudokuSolver solver = new SudokuSolver();

        Random randomNumberGenerator = new Random();

        public SudokuPuzzleGenerator(int gridSize)
        {
            _gridSize = gridSize;
            
        }

        //This method is called when the grid is created, on the screen start up. 
        public int[,] CreateSudokuGrid()
        {
            Random randomNumber = new Random();
            int[,] gridValue = new int[_gridSize, _gridSize];

            for (int i = 0; i < _gridSize; i++)
            {
                for (int j = 0; j < _gridSize; j++)
                {
                    gridValue[i, j] = randomNumber.Next(0, 9);
                }
            }

            //puzzleManager.SavePuzzleToFile(gridValue);

            GenerateExampleSudokuGrid();

          
            return sudokuGrid;
        }

        //Create 2 methods, one which diggies holes from a grid and the other one inserts radnom givens in the puzzle. 

        private void GenerateExampleSudokuGrid()
        {
            int staticNumberLimit = 55; 
            for(int i =0;i<=staticNumberLimit;i++)
            {
                int rowNumber = randomNumberGenerator.Next(0, 9);
                int columnNumber = randomNumberGenerator.Next(0, 9);
                List<int> blockNumbers = CheckValidNumbersForRegions.checkBlock(sudokuGrid, rowNumber, columnNumber);
                List<int> columnNumbers = CheckValidNumbersForRegions.checkColumn(sudokuGrid, rowNumber, columnNumber);
                List<int> rowNumbers = CheckValidNumbersForRegions.checkRow(sudokuGrid, rowNumber, columnNumber);
                List<int> validNumbers = CheckValidNumbersForRegions.GetValidNumbers(columnNumbers, rowNumbers, blockNumbers);

                if(validNumbers.Count !=0 && sudokuGrid[rowNumber,columnNumber]==0)
                {
                    int indexForNumber = randomNumberGenerator.Next(validNumbers.Count);
                    sudokuGrid[rowNumber, columnNumber] = validNumbers[indexForNumber];
                }
                else
                {
                    staticNumberLimit++;
                }
            }
            //THis is how it should be done but needs more work. 
            solver.sudokuPuzzleMultiExample = sudokuGrid;
            solver.staticNumbers = sudokuGrid;
        
            //solver.BacktrackingSolve(0);

        }

        private void DigHoles()
        {

        }


        
    }
}
