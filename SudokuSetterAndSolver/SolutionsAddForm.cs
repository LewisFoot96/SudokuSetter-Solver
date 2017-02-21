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

namespace SudokuSetterAndSolver
{
    public partial class SolutionsAddForm : Form
    {
        public SolutionsAddForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Method to add solutions to any xml file necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addSolutionsBtn_Click(object sender, EventArgs e)
        {
            PuzzleManager puzzleManager = new PuzzleManager();

            //http://www.csharp-examples.net/get-files-from-directory/
            string[] filePaths = Directory.GetFiles(directoryLocationTb.Text);

            for(int i=0;i<=filePaths.Length-1;i++)
            {
                puzzle puzzle = puzzleManager.ReadFromXMlFile(filePaths[i]);
                puzzle finalPuzzle = puzzleManager.ReadFromXMlFile(filePaths[i]);

                SudokuSolver solver = new SudokuSolver();
                solver.currentPuzzleToBeSolved = puzzle;
                solver.SolveSudokuRuleBasedXML();

                for(int cellNumber =0;cellNumber<=puzzle.puzzlecells.Count-1;cellNumber++)
                {
                    finalPuzzle.puzzlecells[cellNumber].solutionvalue = puzzle.puzzlecells[cellNumber].value;
                }
                //http://stackoverflow.com/questions/4999988/to-clear-the-contents-of-a-file
                File.WriteAllText(filePaths[i], string.Empty);
                PuzzleManager.WriteToXmlFile(finalPuzzle, filePaths[i]);
            }
            
        }

    }
}
