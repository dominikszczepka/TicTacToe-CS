using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class PositionValidityChecker
    {
        public bool IsPositionValid(string pos, string[,] boardElements)
        {

            string letters = "abcdefghijklmnoprstuvwxyz";
            try
            {
                int y = int.Parse(pos.Substring(2)) - 1;
                int x = letters.IndexOf(pos[0].ToString().ToLower());
                if (boardElements[x, y] == " ")
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
    }
}
