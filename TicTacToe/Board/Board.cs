using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Board
{
    internal class Board
    {
        private AI ai;
        private static bool isSinglePlayer;
        private static bool player1Turn;
        private static bool isDraw;
        public bool isGameFinished { set; get; }
        private string[,] boardElements;
        private static string whoWon;
        private static int length;

        public Board()
        {
            length = 3;
            boardElements = new string[length,length];
            BoardReset();
        }
        private void BoardReset()
        {
            clearBoard();
            getSettings();
            isDraw = false;
            isGameFinished = false;
        }
        private void clearBoard()
        {

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                    boardElements[i,j] = " ";
            }
        }
        private void getSettings()
        {
            string answer;
            do
            {
                Console.WriteLine("Type for 1 single player mode or 2 for multiplayer mode");
                answer=Console.ReadLine();
            } while (!answer.Equals("1") && !answer.Equals("2"));
            isSinglePlayer = answer.Equals("1") ? true : false;
            if (isSinglePlayer)
            {
                do
                {
                    Console.WriteLine("Type O if you want to start first and X if you want to start second");
                    answer = Console.ReadLine();
                } while (!answer.Equals("X") && !answer.Equals("O"));
                player1Turn = answer.Equals("O") ? true : false;
                ai = new AI();
            }
            else
                player1Turn = true;
        }
        public void makeMove()
        {

            if (isSinglePlayer)
            {
                if (!player1Turn)
                {
                    ai.doBestMove(this);
                    drawBoard();
                }

                if (player1Turn || (!player1Turn && IsRunning()))
                {
                    Console.WriteLine("Please make your next move");
                    updatePositionOnBoard(getPosition());
                }

                if (player1Turn && IsRunning())
                    ai.doBestMove(this);
            }

            else
            {
                Console.WriteLine((player1Turn ? "Player1" : "Player2") + " please make your next move");
                updatePositionOnBoard(getPosition());
                player1Turn = !player1Turn;
            }
        }
        public bool IsRunning()
        {
            isGameFinished = false;
            isDraw = false;
            List<string> results = new List<string>();
            bool tmpisSpace = false;
            string tmpString;

            for (int i = 0; i < length; i++)
            {
                tmpString = "";
                for (int j = 0; j < length; j++)
                {
                    tmpString += boardElements[i,j];
                }
                results.Add(tmpString);
            }

            for (int i = 0; i < length; i++)
            {
                tmpString = "";
                for (int j = 0; j < length; j++)
                {
                    tmpString += (boardElements[j,i]);
                }
                results.Add(tmpString);
            }

            tmpString = "";
            for (int i = 0; i < length; i++)
                tmpString += (boardElements[i,i]);
            results.Add(tmpString);

            tmpString = "";
            for (int i = 0; i < length; i++)
                tmpString += (boardElements[i,length - 1 - i]);
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
                whoWon = results.Contains("XXX") ? "X" : "O";
                isGameFinished = true;
                return false;
            }
            // draw
            else if (!tmpisSpace)
            {
                whoWon = "Draw";
                isDraw = true;
                isGameFinished = true;
                return false;
            }
            else
            {
                whoWon = "";
                return true;
            }
        }
        public void finishGame()
        {
            drawBoard();
            if (isDraw)
            {
                Console.WriteLine("It's a draw!");
            }
            // if next turn should be for player1, but game ended, player 2 won
            else
                Console.WriteLine((whoWon.Equals("O") ? "Player1" : "Player2") + " won the game!");

            if (!willPlayAgain())
            {
                Console.WriteLine("Thank you for playing!");
            }
        }
        public void drawBoard()
        {

            Console.WriteLine("  1 2 3");
            Console.WriteLine("A " + boardElements[0,0] + "|" + boardElements[0,1] + "|" + boardElements[0,2]);
            Console.WriteLine("  -----");
            Console.WriteLine("B " + boardElements[1,0] + "|" + boardElements[1,1] + "|" + boardElements[1,2]);
            Console.WriteLine("  -----");
            Console.WriteLine("C " + boardElements[2,0] + "|" + boardElements[2,1] + "|" + boardElements[2,2]);
        }

        private void updatePositionOnBoard(String pos)
        {

            String letters = "abcdefghijklmnoprstuvwxyz";
            int y = int.Parse(pos.Substring(2)) - 1;
            int x = letters.IndexOf((pos[0].ToString().ToLower()));
            boardElements[x,y] = player1Turn ? "O" : "X";
        }
        private void updatePositionOnBoard(int x, int y, string sign)
        {
            boardElements[x,y] = sign;
        }
        private bool isPositionValid(string pos)
        {

            string letters = "abcdefghijklmnoprstuvwxyz";
            try
            {
                int y = int.Parse(pos.Substring(2)) - 1;
                int x = letters.IndexOf(pos[0].ToString().ToLower());
                if (boardElements[x,y] == " ")
                    return true;
                else
                {
                    Console.WriteLine("Position already taken");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Incorrect position, try again");
                return false;
            }
        }
        private string getPosition()
        {
            string position;
            do
            {
                position = Console.ReadLine();
            } while (!isPositionValid(position));
            return position;
        }
        private bool willPlayAgain()
        {
            string answer;
            do
            {
                Console.WriteLine("Do you want to play again? Type yes or no");
                answer = Console.ReadLine();
            } while (!answer.Equals("yes") && !answer.Equals("no"));
            if (answer.Equals("yes"))
            {
                BoardReset();
                return true;
            }
            else
                return false;
        }
        private class AI
        {
            private string sign;

            public AI()
            {

                this.sign = player1Turn ? "X" : "O";
            }

            public void doBestMove(Board board)
            {
                int[] bestMove = getBestMove(board);
                board.updatePositionOnBoard(bestMove[0], bestMove[1], sign);
            }

            private int minimax(int depth, bool maxPlayer, Board board)
            {

                String otherSign = sign.Equals("O") ? "X" : "O";
                if (!board.IsRunning())
                {
                    switch(whoWon)
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
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            if (board.boardElements[i,j].Equals(" "))
                            {
                                board.boardElements[i,j] = sign;
                                int score = minimax(depth + 1, false, board);
                                board.boardElements[i,j] = " ";
                                bestScore = score > bestScore ? score : bestScore;
                            }
                        }
                    }
                    return bestScore;
                }
                else
                {
                    int bestScore = int.MaxValue;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            if (board.boardElements[i,j].Equals(" "))
                            {
                                board.boardElements[i,j] = otherSign;
                                int score = minimax(depth + 1, true,board);
                                board.boardElements[i,j] = " ";
                                bestScore = score < bestScore ? score : bestScore;
                            }
                        }
                    }
                    return bestScore;
                }
            }

            private int[] getBestMove(Board board)
            {
                int bestX = 0, bestY = 0;
                int bestScore = int.MinValue;
                for (int x = 0; x < length; x++)
                {
                    for (int y = 0; y < length; y++)
                    {
                        if (board.boardElements[x,y] == " ")
                        {
                            board.boardElements[x,y] = sign;
                            int score = minimax(0, false, board);
                            board.boardElements[x,y] = " ";
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
}
