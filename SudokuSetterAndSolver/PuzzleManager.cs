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
        /// Method that writes a puzzle to an xml file, based on the class created from the xml schema. 
        /// </summary>
        /// <param name="puzzleArray"></param>
        /// <param name="path"></param>
        public static void WriteToXmlFile(puzzle generatedPuzzle, string directoryLocation)
        {
            //Writing the array and the difficulty to the xml file. 
            var data = generatedPuzzle;
            var serializer = new XmlSerializer(typeof(puzzle));
            using (var stream = new StreamWriter(directoryLocation))
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
