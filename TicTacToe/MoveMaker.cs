using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class MoveMaker
    {
        private readonly Board _board;
        private readonly GameHandler _gameHandler;
        public MoveMaker(Board board, GameHandler gameHandler)
        {
            _board = board;
            _gameHandler = gameHandler;
        }
        public string GetPosition()
        {
            string position;
            do
            {
                position = Console.ReadLine();
            } while (!_board.IsPositionValid(position));
            return position;
        }
        public void MakeMove()
        {

            if (_board.IsSinglePlayer)
            {
                if (!_board.Player1Turn)
                {
                    _gameHandler.DoAiMove();
                    _gameHandler.DrawBoard();
                }

                if (_board.Player1Turn || !_board.Player1Turn && _gameHandler.IsRunning())
                {
                    Console.WriteLine("Please make your next move");
                    UpdatePositionOnBoard(_gameHandler.GetPosition());
                }

                if (_board.Player1Turn && _gameHandler.IsRunning())
                    _gameHandler.DoAiMove();
            }

            else
            {
                Console.WriteLine((_board.Player1Turn ? "Player1" : "Player2") + " please make your next move");
                UpdatePositionOnBoard(_gameHandler.GetPosition());
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
