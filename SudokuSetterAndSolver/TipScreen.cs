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
using System.Xml;
using System.Xml.Serialization;

namespace SudokuSetterAndSolver
{
    public partial class TipScreen : Form
    {
        private int _tipSelection = 0;
        private tip loadedTip;
        public TipScreen(int tipSelection)
        {       
            InitializeComponent();
            //Setting up tip
            loadedTip = new tip();
            _tipSelection = tipSelection;
            SetUpTip();
        }

        /// <summary>
        /// Method to set up the tip to be displayed. 
        /// </summary>
        private void SetUpTip()
        {
            ReadFromTipFile();
            tipTitleTb.Text = loadedTip.tiptitle;
            tipTextTb.Text = loadedTip.tipcontent;

            //Creating the new image. 
            string imagePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\Tips\Images\"+ loadedTip.tippicturedirectorylocation;

            try
            {
                tipImageBox.Image = Image.FromFile(imagePath);
            }
            catch(Exception e)
            {
                Console.Write("Image can not be found");
            }
        }

        /// <summary>
        /// Method to read the loaded file from the xml file store. 
        /// </summary>
        private void ReadFromTipFile()
        {
            string fileDirectoryLocation = Path.GetFullPath(@"..\..\");
            fileDirectoryLocation += @"Tips\tip"+_tipSelection+".xml";
            var serializer = new XmlSerializer(typeof(tip));
            using (var reader = XmlReader.Create(fileDirectoryLocation))
            {
                loadedTip = serializer.Deserialize(reader) as tip;
            }
        }
    }
}
