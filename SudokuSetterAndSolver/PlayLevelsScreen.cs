﻿using System;
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
        public PlayLevelsScreen()
        {
            InitializeComponent();
        }

        private void levelButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            this.Hide();
            PlaySudokuScreen playSudokuScreen = new PlaySudokuScreen(button.Name);
            playSudokuScreen.Show();
        }

        private void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}
