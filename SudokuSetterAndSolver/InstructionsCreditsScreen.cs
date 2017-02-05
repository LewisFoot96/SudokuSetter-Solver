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
    public partial class InstructionsCreditsScreen : Form
    {
        #region Constructor 
        public InstructionsCreditsScreen()
        {
            InitializeComponent();
        }
        #endregion

        #region Event handling methods
        private void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
        #endregion 
    }
}
