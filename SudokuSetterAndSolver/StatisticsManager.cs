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
    public class StatisticsManager
    {
        #region Filed Varibales 
        //Puzzle that will contain the current stats at run time. 
        public static statistics currentStats = new statistics();
        //LOcation of the statistics file within the application. 
        static string  fileDirectoryLocation = Path.GetFullPath(@"..\..\") + @"\Statistics.xml";
        #endregion

        #region Methods 
        /// <summary>
        /// Method to write and update the xml file to store the statistics. 
        /// </summary>
        public static void WriteToStatisticsFile()
        {
            File.WriteAllText(fileDirectoryLocation, string.Empty);
            //Writing the array and the difficulty to the xml file. 
            var data = currentStats;
            var serializer = new XmlSerializer(typeof(statistics));
            using (var stream = new StreamWriter(fileDirectoryLocation))
                serializer.Serialize(stream, data);
        }

        /// <summary>
        /// Method to read the statistics file of the game, update. 
        /// </summary>
        public static void ReadFromStatisticsFile()
        {
            //Reading file and setting stats object to it, updating it. 
            string fileDirectoryLocation = Path.GetFullPath(@"..\..\");
            fileDirectoryLocation += @"\Statistics.xml";
            var serializer = new XmlSerializer(typeof(statistics));
            using (var reader = XmlReader.Create(fileDirectoryLocation))
            {
                currentStats = serializer.Deserialize(reader) as statistics;
            }

        }

        /// <summary>
        /// Method to update the statistics when a user has compelted a leveled puzzle. 
        /// </summary>
        /// <param name="levelCompleted"></param>
        /// <param name="solvingTime"></param>
        /// <param name="extreme"></param>
        /// <param name="difficulty"></param>
        /// <param name="score"></param>
        public static void LeveledPuzzleComlpeted(int levelCompleted, decimal solvingTime, bool extreme, string difficulty, int score, string puzzleType)
        {
            //Reading, updating and writing. 
            ReadFromStatisticsFile();
            currentStats.levelcompleted = levelCompleted;
            if(difficulty.ToLower() == "extreme")
            {
                currentStats.extremeHighScore++;
            }
            currentStats.hintNumber += levelCompleted;
            UpdatePuzzlesCompleted(puzzleType);
            UpdateFastestSolvingTime(solvingTime);
            HighScoresUpdate(difficulty, score);
            WriteToStatisticsFile();
        }

        /// <summary>
        /// Method for when the user completes a random puzzle. 
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="score"></param>
        /// <param name="solvingTime"></param>
        public static void RandomPuzzleCompleted(string difficulty, int score, decimal solvingTime, string puzzleType)
        {
            //Reading, updating and writing. 
            ReadFromStatisticsFile();
            switch(difficulty.ToLower())
            {
                case "extreme":
                    currentStats.extremeHighScore++;
                    currentStats.hintNumber += 4;
                    break;
                case "hard":
                    currentStats.hintNumber += 3;
                    break;
                case "medium":
                    currentStats.hintNumber += 2;
                    break;
                case "easy":
                    currentStats.hintNumber += 1;
                    break;
            }        
            UpdatePuzzlesCompleted(puzzleType);
            UpdateFastestSolvingTime(solvingTime);
            HighScoresUpdate(difficulty, score);
            WriteToStatisticsFile();
        }

        /// <summary>
        /// Updating the fastest solving time of a puzzle. 
        /// </summary>
        /// <param name="solvingTime"></param>
        private static void UpdateFastestSolvingTime(decimal solvingTime)
        {
            if (currentStats.fastestsolvetime > solvingTime || currentStats.fastestsolvetime ==0)
            {
                currentStats.fastestsolvetime = solvingTime;
            }
        }

        /// <summary>
        /// Method to update the users high score, depending on difficulty. 
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="score"></param>
        private static void HighScoresUpdate(string difficulty, int score)
        {
            //Updating the high score for that level of difficulty.
            switch(difficulty.ToLower())
            {
                case "easy":
                    if(currentStats.easyHighScore < score)
                    {
                        currentStats.easyHighScore = score;
                    }
                    break;
                case "medium":
                    if (currentStats.mediumHighScore < score)
                    {
                        currentStats.mediumHighScore = score;
                    }
                    break;
                case "hard":
                    if (currentStats.hardHighScore < score)
                    {
                        currentStats.hardHighScore = score;
                    }
                    break;
                case "extreme":
                    if (currentStats.extremeHighScore < score)
                    {
                        currentStats.extremeHighScore = score;
                    }
                    break;
            }
        }
        
        /// <summary>
        /// Updating the users hints value. 
        /// </summary>
        /// <param name="hintUpdateValue"></param>
        public static void UpdateHints(int hintUpdateValue)
        {
            ReadFromStatisticsFile();
            currentStats.hintNumber += hintUpdateValue;
            WriteToStatisticsFile();
        }

        /// <summary>
        /// Method to update the completed puzzle stats for the game. 
        /// </summary>
        /// <param name="puzzleType"></param>
        private static void UpdatePuzzlesCompleted(string puzzleType)
        {
            //Based on the types update the puzzles completed. 
            currentStats.puzzlecompleted++;
            if(puzzleType.ToLower() == "irregular")
            {
                currentStats.numberofIrregularCompleted++;
            }
            else if (puzzleType.ToLower() == "normal")
            {
                currentStats.numberofRegularCompleted++;
            }
            else
            {
                currentStats.numberofSmallGridCompleted++;
            }
        }
        #endregion 
    }
}
