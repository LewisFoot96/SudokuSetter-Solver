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
        int[,] finalGenenratedPuzzle = new int[9, 9];
        int _gridSize = 0;
        PuzzleManager puzzleManager = new PuzzleManager();
        SudokuSolver solver = new SudokuSolver();
        Random randomNumber = new Random();
        int rowNumber = 0;
        int columnNumber = 0;

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

        /// <summary>
        /// This method creates a completed grid. 
        /// </summary>
        private void GenerateExampleSudokuGrid()
        {
            solver.sudokuPuzzleMultiExample = sudokuGrid;          
            solver.BacktrackinEffcient(true);
            sudokuGrid = solver.sudokuPuzzleMultiExample;
            DigHoles();
        }

        private void DigHoles()
        {
            bool onlyOneSolution = false;
            //Initially remove 10 candidates from the cells. 
            for (int initialHolesRemoved = 0; initialHolesRemoved <= 40; initialHolesRemoved++)
            {
                while (sudokuGrid[rowNumber, columnNumber] == 0)
                {
                    rowNumber = randomNumber.Next(0, 8);
                    columnNumber = randomNumber.Next(0, 8);
                }         
                sudokuGrid[rowNumber, columnNumber] = 0;
            }

            while (onlyOneSolution == false)
            {
                onlyOneSolution = true; 
            }
            //check to see if the puzzle is solvable. 
            //finalGenenratedPuzzle = sudokuGrid;
            solver.sudokuPuzzleMultiExample = sudokuGrid;
            solver.BacktrackinEffcient(false);
            //Need to then check if there is another solution. 

        }
      
    }
}
