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
    public partial class SplashScreen : Form
    {
        #region Variables 
        //Timer value
        int timerValue = 0;
        #endregion

        #region Constrcutor 
        public SplashScreen()
        {
            //Creating the form and starting the timer. 
            InitializeComponent();
            animationTimer.Start();
        }

        #endregion

        #region Event Handlers 
        /// <summary>
        /// Method to increase the size of the picture and go onto the main screen after some time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            timerValue++;
            animationPb.Width = (animationPb.Width +10);
            animationPb.Height = (animationPb.Height + 10);

            if (timerValue ==28)
            {
                this.Hide();
                MainScreen mainScreen = new MainScreen();
                mainScreen.Show();
            }          
        }
        #endregion
    }
}
