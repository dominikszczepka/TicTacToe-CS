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
        private readonly PositionValidityChecker positionValidityChecker;
        public bool IsSinglePlayer { get; set; }
        public bool Player1Turn { get; set; }
        public bool IsDraw { get; set; }
        public bool IsGameFinished { get; set; }
        public string[,] BoardElements { get; set; }
        public string? WhoWon { get; set; }
        public static int Length { get; set; }

        public Board()
        {
            positionValidityChecker = new PositionValidityChecker();
            Length = 3;
            BoardElements = new string[Length, Length];
            BoardReset();
        }
        public void BoardReset()
        {
            ClearBoard();
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
        public bool IsPositionValid(string pos)
        {
            return positionValidityChecker.IsPositionValid(pos, BoardElements);
        }
    }
}
