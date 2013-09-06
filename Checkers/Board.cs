using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers
{
    /// <summary>
    /// This class represents the checkers board.
    /// </summary>
    class Board : List<List<string>>
    {
        public Board()
        {
            for (int i = 0; i < 8; i++)
            {
                if (i < 3) Add(BuildInitialRow("X", i));
                if (i > 2 && i < 5) Add(BuildInitialRow(".", i));
                if (i > 4) Add(BuildInitialRow("O", i));
            }
        }

        private List<string> BuildInitialRow(string type, int rowNum)
        {
            string firstType = ".", secondType = ".";
            if (rowNum % 2 == 0)
            {
                firstType = "."; 
                secondType = type;
            }
            else
            {
                firstType = type;
                secondType = ".";
            }
            
            return new List<string>() { firstType, secondType, firstType, secondType, firstType, secondType, firstType, secondType };
        }

        public new string ToString()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("  0 1 2 3 4 5 6 7");

            for (int i = 0; i < 8; i++)
            {
                output.Append(i.ToString());
                output.Append(" ");
                output.AppendLine(this[i].Aggregate((x, y) => (x + " " + y)));
            }

            return output.ToString();
        }
    }
}
