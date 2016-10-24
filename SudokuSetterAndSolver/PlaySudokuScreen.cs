using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuSetterAndSolver
{
    public partial class PlaySudokuScreen : Form
    {
        public PlaySudokuScreen()
        {
            InitializeComponent();
        }

        public PlaySudokuScreen(string fileNameFromButtonPress)
        {
            //The string that is passed into here will be the file exntesion value, to load in the correct file. 
        }
    }
}
