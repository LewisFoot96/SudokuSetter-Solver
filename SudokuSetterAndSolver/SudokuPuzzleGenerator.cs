using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    class SudokuPuzzleGenerator
    {

        int _gridSize = 0;
        PuzzleManager puzzleManager = new PuzzleManager();
        
        public SudokuPuzzleGenerator(int gridSize)
        {
            _gridSize = gridSize;
        }

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

            puzzleManager.SavePuzzleToFile(gridValue);
          
            return gridValue;
        }


        
    }
}
