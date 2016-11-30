using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SudokuSetterAndSolver
{

    //THis class maanages the puzzles that will be saved and loaded into the game when required. 
    public class PuzzleManager
    {
        #region Methods 
        /// <summary>
        /// Method to convert an arry into a multi dimensional array, that will contain the puzzle. 
        /// </summary>
        /// <param name="puzzleArray"></param>
        /// <returns></returns>
        public int[,] ConvertArrayToMultiDimensionalArray(int[] puzzleArray)
        {
            //Method variables 
            int[,] sudokuPuzzleMultiDimensionalArray = new int[9,9];
            int rowNumber = 0;
            int columnNumber = 0;

            //creating the multi dimensional array. 
            for (int cellNumber =0;cellNumber<=80;cellNumber++)
            {
                sudokuPuzzleMultiDimensionalArray[rowNumber, columnNumber] = puzzleArray[cellNumber];
                if (cellNumber == 8 || cellNumber % 9 == 8)
                {
                    columnNumber = 0;
                    rowNumber++;
                }
                else
                {
                    columnNumber++;
                }
            }
            return sudokuPuzzleMultiDimensionalArray;
        }

        /// <summary>
        /// Converting the multi dimensional array into a single array, so that it can be written into the xml file. 
        /// </summary>
        /// <param name="puzzleMultiDimensionalArray"></param>
        /// <returns></returns>
        public int[] ConvertMultiDimensional(int[,] puzzleMultiDimensionalArray)
        {
            int[] puzzleArray = new int[81];
            int cellNumber = 0;

            for(int rowNumber = 0;rowNumber<=8;rowNumber++)
            {
                for(int columnNumber=0;columnNumber<=8;columnNumber++)
                {
                    puzzleArray[cellNumber] = puzzleMultiDimensionalArray[rowNumber, columnNumber];
                    cellNumber++;
                }
            }

            return puzzleArray;
        }

        /// <summary>
        /// Method that writes a puzzle to an xml file, based on the class created from the xml schema. 
        /// </summary>
        /// <param name="puzzleArray"></param>
        /// <param name="path"></param>
        public void WriteToXmlFile(int[] puzzleArray, string path)
        {
            //creating an array of objects from the arrat that the puzzle is stored int. 
            List<puzzleCell> testPuzzle = new List<puzzleCell>();

            puzzleCell testPuzzleCell1 = new puzzleCell();
            testPuzzleCell1.value = 1;
            testPuzzleCell1.columnnumber = 1;
            testPuzzleCell1.rownumber = 1;
            testPuzzleCell1.blocknumber = 1;

            puzzleCell testPuzzleCell2 = new puzzleCell();
            testPuzzleCell2.value = 1;
            testPuzzleCell2.columnnumber = 1;
            testPuzzleCell2.rownumber = 1;
            testPuzzleCell2.blocknumber = 1;

            testPuzzle.Add(testPuzzleCell1);
            testPuzzle.Add(testPuzzleCell2);
           

            //Writing the array and the difficulty to the xml file. 
            var data = new puzzle { difficulty="easy", puzzlecells = testPuzzle };
            var serializer = new XmlSerializer(typeof(puzzle));
            using (var stream = new StreamWriter(path))
                serializer.Serialize(stream, data);
        }

       /// <summary>
       /// Method that reads in a sudoku puzzle that is stored within a file, using the xsd that was created. 
       /// </summary>
       /// <param name="path"></param>
       /// <returns></returns>
        public puzzle ReadFromXMlFile(string path)
        {
            puzzle type;
            var serializer = new XmlSerializer(typeof(puzzle));
            using (var reader = XmlReader.Create(path))
            {
                type = serializer.Deserialize(reader) as puzzle;
            }
            return type;

        }
    }

    #endregion
}
