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
    public partial class PlayLevelsScreen : Form
    {
        #region Constructor 
        public PlayLevelsScreen()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handler Methods 
        private void levelButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            this.Hide();
            //Sneding the button name which will provide a link to the puzzle that will be loaded into the system. 
            PlaySudokuScreen playSudokuScreen = new PlaySudokuScreen(button.Name,9);
            playSudokuScreen.Show();
        }

        private void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
        #endregion
    }
}
