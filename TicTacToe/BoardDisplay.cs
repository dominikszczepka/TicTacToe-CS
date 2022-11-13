using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    internal class BoardDisplay
    {
        private Board _board;
        public BoardDisplay(Board board)
        {
            _board = board;
        }
        public void DrawBoard()
        {
            Console.WriteLine("  1 2 3");
            Console.WriteLine("A " + _board.BoardElements[0, 0] + "|" + _board.BoardElements[0, 1] + "|" + _board.BoardElements[0, 2]);
            Console.WriteLine("  -----");
            Console.WriteLine("B " + _board.BoardElements[1, 0] + "|" + _board.BoardElements[1, 1] + "|" + _board.BoardElements[1, 2]);
            Console.WriteLine("  -----");
            Console.WriteLine("C " + _board.BoardElements[2, 0] + "|" + _board.BoardElements[2, 1] + "|" + _board.BoardElements[2, 2]);
        }
    }
}
