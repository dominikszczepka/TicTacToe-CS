using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class SettingsHandler
    {
        private readonly Board _board;
        public SettingsHandler(Board board)
        {
            _board = board;
        }
        public void GetSettings()
        {
            string? answer;
            do
            {
                Console.WriteLine("Type for 1 single player mode or 2 for multiplayer mode");
                answer = Console.ReadLine();
            } while (!answer.Equals("1") && !answer.Equals("2"));
            _board.IsSinglePlayer = answer.Equals("1") ? true : false;
            if (_board.IsSinglePlayer)
            {
                do
                {
                    Console.WriteLine("Type O if you want to start first and X if you want to start second");
                    answer = Console.ReadLine();
                } while (!answer.Equals("X") && !answer.Equals("O"));
                _board.Player1Turn = answer.Equals("O") ? true : false;
            }
            else
                _board.Player1Turn = true;
        }
    }
}
