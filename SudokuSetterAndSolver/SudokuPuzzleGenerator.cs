using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    class SudokuPuzzleGenerator
    {
        #region Global Vairbales 
        //Grids that will contain the created grid, the first will be used to check to ensure the grid is valid 
        int[,] sudokuGrid = new int[9, 9];
        int[,] finalGenenratedPuzzle = new int[9, 9];
        int[,] orginalSudokuGrid = new int[9, 9];
        int _gridSize = 0;
        int rowNumber = 0;
        int columnNumber = 0;
        bool solved = false;
        #endregion

        #region Objects 
        PuzzleManager puzzleManager = new PuzzleManager();
        SudokuSolver solver = new SudokuSolver();
        Random randomNumber = new Random();
        Random randomNumberGenerator = new Random();

        #endregion

        #region Constructor 
        public SudokuPuzzleGenerator(int gridSize)
        {
            _gridSize = gridSize;

        }
        #endregion

        #region Methods 

        //This method is called when the grid is created, on the screen start up. 
        public int[,] CreateSudokuGrid()
        {
            GenerateExampleSudokuGrid();
            return finalGenenratedPuzzle;
        }

        //Create 2 methods, one which diggies holes from a grid and the other one inserts radnom givens in the puzzle. 

        /// <summary>
        /// This method creates a completed grid. 
        /// </summary>
        private void GenerateExampleSudokuGrid()
        {
            //finalGenenratedPuzzle =  new int[,] sudokuGrid;
            solver.sudokuPuzzleMultiExample = sudokuGrid;
            solved = solver.BacktrackinEffcient(true);
            sudokuGrid = solver.sudokuPuzzleMultiExample;
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    orginalSudokuGrid[i, j] = sudokuGrid[i, j];
                }
            }
            DigHoles();
        }

        //could use static numbers list. 
        private void DigHoles()
        {
            //classes do it my reference 
            bool onlyOneSolution = false;
            bool isEqualToOrginal = false;
            //Initially remove 10 candidates from the cells. 
            for (int initialHolesRemoved = 0; initialHolesRemoved <= 30; initialHolesRemoved++)
            {
                while (sudokuGrid[rowNumber, columnNumber] == 0)
                {
                    rowNumber = randomNumber.Next(0, 8);
                    columnNumber = randomNumber.Next(0, 8);
                }
                sudokuGrid[rowNumber, columnNumber] = 0;
            }
            //Maybe create a getter and setter for sudokuMulti 

            SetFinalGeneratedPuzzle();


            solver.sudokuPuzzleMultiExample = sudokuGrid;
            //solver.sudokuPuzzleMultiExample = sudokuGrid;
            solved = solver.BacktrackinEffcient(false);

            isEqualToOrginal = SeeIfGeneratedPuzzleHasTheSameSolutionAsTheOrginal();

            //while (onlyOneSolution == false)
            //{
            //    onlyOneSolution = true; 
            //}

        }

        private void SetFinalGeneratedPuzzle()
        {
            //Getting the value of finalGeneratedPUzzle 
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    finalGenenratedPuzzle[i, j] = sudokuGrid[i, j];
                }
            }
        }

        private bool SeeIfGeneratedPuzzleHasTheSameSolutionAsTheOrginal()
        {
            bool isEqual = true;
            for (int row = 0; row <= 8; row++)
            {
                for (int column = 0; column <= 8; column++)
                {
                    if (sudokuGrid[row, column] != orginalSudokuGrid[row, column])
                    {
                        isEqual = false;
                    }
                }
            }
            return isEqual;
        }

    }


    #endregion
}
