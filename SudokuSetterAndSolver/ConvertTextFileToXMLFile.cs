using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SudokuSetterAndSolver
{
    //http://www.sudokukingdom.com/ - link to the puzzles I used for the first set if test puzzles. 
    public partial class ConvertTextFileToXMLFile : Form
    {
        #region Field Variables 
        //Puzzle values
        char[] characters;
        int[] numbersInPuzzle;
        //Puzzle that will be stored into the xml file. 
        puzzle xmlPuzzle = new puzzle();
        //Location to store the test file. 
        string fileDirctoryLocation = "";
        #endregion

        #region Constructor 
        public ConvertTextFileToXMLFile()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler Methods 
        /// <summary>
        /// Open file choose when file is selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectFileBtn_Click(object sender, EventArgs e)
        {
            fileChooser.ShowDialog();
        }

        /// <summary>
        /// When the text file is confirmed and selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileChooser_FileOk(object sender, CancelEventArgs e)
        {
            //Creating new xml puzzle and setting directiry location of the file. 
            xmlPuzzle = new puzzle();
            fileDirctoryLocation = fileChooser.FileName;

            //https://msdn.microsoft.com/en-us/library/ezwyzy7b.aspx
            // Example #1
            // Read the file as one string.
            string text = File.ReadAllText(fileDirctoryLocation);

            characters = text.ToCharArray();

            numbersInPuzzle = new int[characters.Length];
            // http://stackoverflow.com/questions/239103/c-sharp-char-to-int
            for (int arrayCount = 0; arrayCount <= characters.Length - 1; arrayCount++)
            {
                numbersInPuzzle[arrayCount] = characters[arrayCount] - '0';
            }
            //Method to create puzzle. 
            CreateXMLFIle();
        }
        #endregion

        #region Methods 
        /// <summary>
        /// Method to create the xml puzzle for a list of integers
        /// </summary>
        private void CreateXMLFIle()
        {   //Resetting values. 
            int rowNumber = 0;
            int columnNumber = 0;
            int blockNumber = 0;
            //Setting grid size, based on input. 
            if (numbersInPuzzle.Length  == 81)
            {
                xmlPuzzle.gridsize = 9;
            }
            else if (numbersInPuzzle.Length == 256)
            {
                xmlPuzzle.gridsize = 16;
            }
            else
            {
                xmlPuzzle.gridsize = 4;
            }
            //Looping through the nymbers in the puzzle. 
            for (int cellIndexNumber = 0; cellIndexNumber <= numbersInPuzzle.Length-1 ; cellIndexNumber++)
            {   //Getting bloxks numbers 
                if(xmlPuzzle.gridsize ==9)
                {
                    blockNumber = GetBlockNumberNine(rowNumber, columnNumber);
                }
                else if(xmlPuzzle.gridsize ==16)
                {
                    blockNumber =GetBlocNumberSixteen(rowNumber, columnNumber);
                }
                else
                {
                    blockNumber = GetBlockFour(rowNumber, columnNumber);
                }
                //Creating a cell and setting all of its values. 
                puzzleCell tempPuzzleCell = new puzzleCell();
                tempPuzzleCell.blocknumber = blockNumber;
                tempPuzzleCell.rownumber = rowNumber;
                tempPuzzleCell.columnnumber = columnNumber;
                tempPuzzleCell.value = numbersInPuzzle[cellIndexNumber];
                xmlPuzzle.puzzlecells.Add(tempPuzzleCell);
                //Iterations to get correct row and column numbers. 
                if (cellIndexNumber == 8 || cellIndexNumber % 9 == 8)
                {
                    rowNumber++;
                    columnNumber = 0;
                }
                else
                {
                    columnNumber++;
                }
            }
            //Saving the file in the same area. 
            string filePath = Path.GetDirectoryName(fileDirctoryLocation);
            CreateAndSaveXmlFile(filePath);
        }

        /// <summary>
        /// Method that saves the created xml puzzle file. 
        /// </summary>
        /// <param name="directoryLocation"></param>
        private void CreateAndSaveXmlFile(string directoryLocation)
        {
        //http://stackoverflow.com/questions/6530424/generating-xml-file-using-xsd-file
            xmlPuzzle.type = "regular";
            string extension = fileNameTb.Text;
            string saveFileLocation = directoryLocation + "\\" + extension + ".xml";

            var serializer = new XmlSerializer(typeof(puzzle));
            using (var stream = new StreamWriter(saveFileLocation))
                serializer.Serialize(stream, xmlPuzzle);
        }
        #endregion

        #region GetBlockNumbers 
        //Getting the block numbers for the puzzles based in the column and row number of that cell. 
        private int GetBlockFour(int tempRowNumber, int tempColumnNumber)
        {
            if (tempRowNumber <= 1 && tempColumnNumber <= 1)
            {
                return 0;
            }
            else if (tempRowNumber <= 1 && tempColumnNumber >= 2)
            {
                return 1;
            }
            else if (tempRowNumber >= 2 && tempRowNumber <= 1)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        private int GetBlockNumberNine(int tempRowNumber, int tempColumnNumber)
        {
            if (tempRowNumber <= 2 && tempColumnNumber <= 2)
            {
                return 0;
            }
            else if (tempRowNumber <= 2 && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 1;
            }
            else if (tempRowNumber <= 2 && (tempColumnNumber >= 6 && tempColumnNumber <= 8))
            {
                return 2;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && tempColumnNumber <= 2)
            {
                return 3;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 4;
            }
            else if ((tempRowNumber >= 3 && tempRowNumber <= 5) && (tempColumnNumber >= 6 && tempColumnNumber <= 8))
            {
                return 5;
            }
            else if ((tempRowNumber >= 6 && tempRowNumber <= 8) && tempColumnNumber <= 2)
            {
                return 6;
            }
            else if ((tempRowNumber >= 6 && tempRowNumber <= 8) && (tempColumnNumber >= 3 && tempColumnNumber <= 5))
            {
                return 7;
            }
            else
            {
                return 8;
            }

        }

        private int GetBlocNumberSixteen(int tempRowNumber, int tempColumnNumber)
        {
            if (tempRowNumber <= 3 && tempColumnNumber <= 3)
            {
                return 0;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 1;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 2;
            }
            else if (tempRowNumber <= 3 && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 3;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && tempColumnNumber <= 3)
            {
                return 4;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 5;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 6;
            }
            else if ((tempRowNumber >= 4 && tempRowNumber <= 7) && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 7;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && tempColumnNumber <= 3)
            {
                return 8;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 9;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 10;
            }
            else if ((tempRowNumber >= 8 && tempRowNumber <= 11) && (tempColumnNumber >= 12 && tempColumnNumber <= 15))
            {
                return 11;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && tempColumnNumber <= 3)
            {
                return 12;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && (tempColumnNumber >= 4 && tempColumnNumber <= 7))
            {
                return 13;
            }
            else if ((tempRowNumber >= 12 && tempRowNumber <= 15) && (tempColumnNumber >= 8 && tempColumnNumber <= 11))
            {
                return 14;
            }
            else
            {
                return 15;
            }

        }
        #endregion
    }
}
