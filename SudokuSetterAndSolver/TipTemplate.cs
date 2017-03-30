using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSetterAndSolver
{
    class TipTemplate
    {
        private string tipTile;
        private string tipContent;

        public string TipTile
        {
            get
            {
                return tipTile;
            }

            set
            {
                tipTile = value;
            }
        }

        public string TipContent
        {
            get
            {
                return tipContent;
            }

            set
            {
                tipContent = value;
            }
        }
    }
}
