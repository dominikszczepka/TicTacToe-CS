using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class MoveMaker
    {
        private Board _board;
        public MoveMaker(Board board)
        {
            _board = board;
        }
        public void MakeMove()
        {

            if (_board.IsSinglePlayer)
            {
                if (!_board.Player1Turn)
                {
                    _board.DoAiMove();
                    _board.DrawBoard();
                }

                if (_board.Player1Turn || !_board.Player1Turn && _board.IsRunning())
                {
                    Console.WriteLine("Please make your next move");
                    UpdatePositionOnBoard(_board.GetPosition());
                }

                if (_board.Player1Turn && _board.IsRunning())
                    _board.DoAiMove();
            }

            else
            {
                Console.WriteLine((_board.Player1Turn ? "Player1" : "Player2") + " please make your next move");
                UpdatePositionOnBoard(_board.GetPosition());
                _board.Player1Turn = !_board.Player1Turn;
            }
        }
        private void UpdatePositionOnBoard(string pos)
        {

            string letters = "abcdefghijklmnoprstuvwxyz";
            int y = int.Parse(pos.Substring(2)) - 1;
            int x = letters.IndexOf(pos[0].ToString().ToLower());
            _board.BoardElements[x, y] = _board.Player1Turn ? "O" : "X";
        }

    }
}
