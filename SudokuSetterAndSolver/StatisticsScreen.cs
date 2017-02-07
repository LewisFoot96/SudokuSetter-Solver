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
    public partial class StatisticsScreen : Form
    {
        
        public StatisticsScreen()
        {
            InitializeComponent();
           
        }

        private void mainMenuBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
        }

        private void CreateStaticsTextBlock()
        {
            TextBox staticsTextBox = new TextBox();

            this.Controls.Add(staticsTextBox);
            staticsTextBox.Name = "staticsTb";
            staticsTextBox.ReadOnly = true;
            staticsTextBox.Size = new Size(300, 170);
            staticsTextBox.TabIndex = 0;
            staticsTextBox.TextAlign = HorizontalAlignment.Left;
            staticsTextBox.Location = new System.Drawing.Point(140, 120);
            staticsTextBox.Multiline = true;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Game Statistics:" );
            sb.AppendLine();
            sb.AppendLine("Puzzles Completed: " + StatisticsManager.currentStats.puzzlecompleted);
            sb.AppendLine();
            sb.AppendLine("Quickest Puzzle Time Completion: " + StatisticsManager.currentStats.fastestsolvetime);
            sb.AppendLine();
            sb.AppendLine("Number Of Extreme Puzzle Completed: " + StatisticsManager.currentStats.numberOfExtremePuzzleCompleted);
            sb.AppendLine();
            sb.AppendLine("Current Level: " + +StatisticsManager.currentStats.levelcompleted);

            //Limiting the text box to only on character. 
            staticsTextBox.MaxLength = 1;
            //Setting the value in the grid text box. 
            staticsTextBox.Text = sb.ToString();

            //Clouring 
            staticsTextBox.Font = new Font(staticsTextBox.Font, FontStyle.Bold);
            staticsTextBox.ForeColor = Color.Black;
        }

        private void StatisticsScreen_Load(object sender, EventArgs e)
        {
            StatisticsManager.ReadFromStatisticsFile();
            CreateStaticsTextBlock();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StatisticsManager.currentStats.levelcompleted = 3;
            StatisticsManager.WriteToStatisticsFile();
        }
    }
}
