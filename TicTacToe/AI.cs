using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class AI
    {
        private string sign;
        private Board _board;
        private readonly GameHandler _gameHandler;

        public AI(Board board,GameHandler gameHandler)
        {
            _gameHandler = gameHandler;
            ResetAI(board);
        }
        public void ResetAI(Board board)
        {
            _board = board;
            sign = board.Player1Turn ? "X" : "O";
        }
        public void DoBestMove()
        {
            int[] bestMove = GetBestMove();
            _board.BoardElements[bestMove[0], bestMove[1]]=sign;
        }
        private int Minimax(int depth, bool maxPlayer)
        {
            string otherSign = sign.Equals("O") ? "X" : "O";
            if (!_gameHandler.IsRunning())
            {
                switch (_board.WhoWon)
                {
                    case var value when value == sign:
                        return 1;
                    case var value when value == otherSign:
                        return -1;
                    default:
                        return 0;
                }
            }

            if (maxPlayer)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < Board.Length; i++)
                {
                    for (int j = 0; j < Board.Length; j++)
                    {
                        if (_board.BoardElements[i, j].Equals(" "))
                        {
                            _board.BoardElements[i, j] = sign;
                            int score = Minimax(depth + 1, false);
                            _board.BoardElements[i, j] = " ";
                            bestScore = score > bestScore ? score : bestScore;
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < Board.Length; i++)
                {
                    for (int j = 0; j < Board.Length; j++)
                    {
                        if (_board.BoardElements[i, j].Equals(" "))
                        {
                            _board.BoardElements[i, j] = otherSign;
                            int score = Minimax(depth + 1, true);
                            _board.BoardElements[i, j] = " ";
                            bestScore = score < bestScore ? score : bestScore;
                        }
                    }
                }
                return bestScore;
            }
        }

        private int[] GetBestMove()
        {
            int bestX = 0, bestY = 0;
            int bestScore = int.MinValue;
            for (int x = 0; x < Board.Length; x++)
            {
                for (int y = 0; y < Board.Length; y++)
                {
                    if (_board.BoardElements[x, y] == " ")
                    {
                        _board.BoardElements[x, y] = sign;
                        int score = Minimax(0, false);
                        _board.BoardElements[x, y] = " ";
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestX = x;
                            bestY = y;
                        }
                    }
                }
            }
            int[] result = new int[] { bestX, bestY };
            return result;
        }
    }
}
