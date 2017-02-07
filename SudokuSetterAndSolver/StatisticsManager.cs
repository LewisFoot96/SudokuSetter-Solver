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

        public static void ReadFromStatisticsFile()
        {
            string fileDirectoryLocation = Path.GetFullPath(@"..\..\");
            fileDirectoryLocation += @"\Statistics.xml";
            var serializer = new XmlSerializer(typeof(statistics));
            using (var reader = XmlReader.Create(fileDirectoryLocation))
            {
                currentStats = serializer.Deserialize(reader) as statistics;
            }

        }
        #endregion 
    }
}
