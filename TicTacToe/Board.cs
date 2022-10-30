using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Board
    {
        private ResolveChecker resolveChecker = new ResolveChecker();
        private MoveMaker moveMaker;
        private BoardDisplay display = new BoardDisplay();
        private AI ai;
        public bool IsSinglePlayer { get; set; }
        public bool Player1Turn { get; set; }
        public bool IsDraw { get; set; }
        public bool IsGameFinished { get; set; }
        public string[,] BoardElements { get; set; }
        public string? WhoWon { get; set; }
        public static int Length { get; set; }

        public Board()
        {
            moveMaker = new MoveMaker(this);
            Length = 3;
            BoardElements = new string[Length, Length];
            BoardReset();
        }
        private void BoardReset()
        {
            ClearBoard();
            GetSettings();
            IsDraw = false;
            IsGameFinished = false;
        }
        private void ClearBoard()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length; j++)
                    BoardElements[i, j] = " ";
            }
        }
        public void DoAiMove()
        {
            if (ai == null) ai = new AI(this);
            ai.DoBestMove();
        }
        private void GetSettings()
        {
            string answer;
            do
            {
                Console.WriteLine("Type for 1 single player mode or 2 for multiplayer mode");
                answer = Console.ReadLine();
            } while (!answer.Equals("1") && !answer.Equals("2"));
            IsSinglePlayer = answer.Equals("1") ? true : false;
            if (IsSinglePlayer)
            {
                do
                {
                    Console.WriteLine("Type O if you want to start first and X if you want to start second");
                    answer = Console.ReadLine();
                } while (!answer.Equals("X") && !answer.Equals("O"));
                Player1Turn = answer.Equals("O") ? true : false;
                ai = new AI(this);
            }
            else
                Player1Turn = true;
        }
        public void MakeMove()
        {
            moveMaker.MakeMove();
        }
        public bool IsRunning()
        {
            return resolveChecker.IsRunning(this);
        }
        public void FinishGame()
        {
            DrawBoard();
            if (IsDraw)
            {
                Console.WriteLine("It's a draw!");
            }
            // if next turn should be for player1, but game ended, player 2 won
            else
                Console.WriteLine((WhoWon.Equals("O") ? "Player1" : "Player2") + " won the game!");

            if (!WillPlayAgain())
            {
                Console.WriteLine("Thank you for playing!");
            }
        }
        public void DrawBoard()
        {
            display.DrawBoard(BoardElements);
        }

        private bool IsPositionValid(string pos)
        {

            string letters = "abcdefghijklmnoprstuvwxyz";
            try
            {
                int y = int.Parse(pos.Substring(2)) - 1;
                int x = letters.IndexOf(pos[0].ToString().ToLower());
                if (BoardElements[x, y] == " ")
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
        public string GetPosition()
        {
            string position;
            do
            {
                position = Console.ReadLine();
            } while (!IsPositionValid(position));
            return position;
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
                BoardReset();
                return true;
            }
            else
                return false;
        }
    }
}
