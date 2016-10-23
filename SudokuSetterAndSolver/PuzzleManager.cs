using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    //THis class maanages the puzzles that will be saved and loaded into the game when required. 
    class PuzzleManager
    {
        string testFilePath;
        public void SavePuzzleToFile(int[,] puzzleArray)
        {
            List<string> puzzleStringList = CreateStringList(puzzleArray);
            string filePath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            filePath += @"\Puzzles\TestPuzzle.txt";

            testFilePath = filePath;

            StreamWriter file = new StreamWriter(testFilePath);
            foreach (string puzzleStringElement in puzzleStringList)
            {
                if (puzzleStringElement != "")
                {
                    file.Write(puzzleStringElement);
                }
            }
            file.Close();

            int[,] puzzleValues =LoadPuzzleFromFile(testFilePath);

        }

        public int[,] LoadPuzzleFromFile(string fileLocation)
        {
            System.IO.StreamReader myFile = new System.IO.StreamReader(fileLocation);
            string fileData = myFile.ReadToEnd();

            string[] words = fileData.Split(',');

            List<string> listOfNumbers = words.ToList();

            listOfNumbers.RemoveAt(listOfNumbers.Count - 1);

             int[,] puzzleValues = ConveryArrayToMultiDimensionalArray(listOfNumbers);
            return puzzleValues;
        }


        private List<string> CreateStringList(int[,] puzzleValues)
        {
            List<string> puzzleStringList = new List<string>();

            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    puzzleStringList.Add(puzzleValues[i, j].ToString());
                    puzzleStringList.Add(",");
                }
            }


            return puzzleStringList;
        }

        private int[,] ConveryArrayToMultiDimensionalArray(List<string> listOfStringValues)
        {
            int gridSize = (int)Math.Sqrt(listOfStringValues.Count);
            string[] puzzlesValuesStringArray = new string[listOfStringValues.Count];
            int[] puzzlesValuesIntArray = new int[listOfStringValues.Count];

            int[,] puzzleValues = new int[gridSize, gridSize];

            puzzlesValuesStringArray = listOfStringValues.ToArray();

            puzzlesValuesIntArray = Array.ConvertAll(puzzlesValuesStringArray, s => int.Parse(s));

            int singleArrayValue = 0; 

            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    puzzleValues[i, j] = puzzlesValuesIntArray[singleArrayValue];
                   
                    singleArrayValue++;
                }
            }

            return puzzleValues;
        }
    }
}
