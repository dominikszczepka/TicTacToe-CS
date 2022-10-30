using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class BoardDisplay
    {
        public void DrawBoard(String[,] BoardElements)
        {
            Console.WriteLine("  1 2 3");
            Console.WriteLine("A " + BoardElements[0, 0] + "|" + BoardElements[0, 1] + "|" + BoardElements[0, 2]);
            Console.WriteLine("  -----");
            Console.WriteLine("B " + BoardElements[1, 0] + "|" + BoardElements[1, 1] + "|" + BoardElements[1, 2]);
            Console.WriteLine("  -----");
            Console.WriteLine("C " + BoardElements[2, 0] + "|" + BoardElements[2, 1] + "|" + BoardElements[2, 2]);
        }
    }
}
