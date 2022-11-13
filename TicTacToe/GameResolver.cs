using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameResolver
    {
        private readonly Board _board;
        private readonly GameHandler _gameHandler;
        public GameResolver (Board board, GameHandler gameHandler)
        {
            _board = board;
            _gameHandler = gameHandler;
        }

        public void FinishGame()
        {
            _gameHandler.DrawBoard();
            if (_board.IsDraw)
            {
                Console.WriteLine("It's a draw!");
            }
            // if next turn should be for player1, but game ended, player 2 won
            else
                Console.WriteLine((_board.WhoWon.Equals("O") ? "Player1" : "Player2") + " won the game!");

            if (!WillPlayAgain())
            {
                Console.WriteLine("Thank you for playing!");
            }
        }
        private bool WillPlayAgain()
        {
            string answer;
            do
            {
                Console.WriteLine("Do you want to play again? Type yes or no");
                answer = Console.ReadLine();
            } while (!answer.Equals("yes") && !answer.Equals("no"));
            if (answer.Equals("yes"))
            {
                _gameHandler.GameReset();
                return true;
            }
            else
                return false;
        }
        public bool IsRunning()
        {
            _board.IsGameFinished = false;
            _board.IsDraw = false;
            List<string> results = new List<string>();
            bool tmpisSpace = false;
            string tmpString;

            for (int i = 0; i < Board.Length; i++)
            {
                tmpString = "";
                for (int j = 0; j < Board.Length; j++)
                {
                    tmpString += _board.BoardElements[i, j];
                }
                results.Add(tmpString);
            }

            for (int i = 0; i < Board.Length; i++)
            {
                tmpString = "";
                for (int j = 0; j < Board.Length; j++)
                {
                    tmpString += _board.BoardElements[j, i];
                }
                results.Add(tmpString);
            }

            tmpString = "";
            for (int i = 0; i < Board.Length; i++)
                tmpString += _board.BoardElements[i, i];
            results.Add(tmpString);

            tmpString = "";
            for (int i = 0; i < Board.Length; i++)
                tmpString += _board.BoardElements[i, Board.Length - 1 - i];
            results.Add(tmpString);

            // check if there is any space left
            foreach (string result in results)
            {
                if (result.Contains(" "))
                    tmpisSpace = true;
            }
            // game ended
            if (results.Contains("XXX") || results.Contains("OOO"))
            {
                _board.WhoWon = results.Contains("XXX") ? "X" : "O";
                _board.IsGameFinished = true;
                return false;
            }
            // draw
            else if (!tmpisSpace)
            {
                _board.WhoWon = "Draw";
                _board.IsDraw = true;
                _board.IsGameFinished = true;
                return false;
            }
            else
            {
                _board.WhoWon = "";
                return true;
            }
        }
    }
}
