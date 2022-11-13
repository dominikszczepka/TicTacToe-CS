using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class GameHandler
    {
        private readonly GameResolver gameResolver;
        private readonly SettingsHandler settingsHandler;
        private readonly MoveMaker moveMaker;
        private readonly BoardDisplay display;
        private readonly Board board;
        private readonly AI ai;

        public GameHandler()
        {
            board = new Board();
            ai = new AI(board, this);
            moveMaker = new MoveMaker(board,this);
            gameResolver = new GameResolver(board, this);
            settingsHandler = new SettingsHandler(board);
            display = new BoardDisplay(board);
            Board.Length = 3;
            GameReset();
        }
        public void GameReset()
        {
            board.BoardReset();
            settingsHandler.GetSettings();
            ai.ResetAI(board);
        }
        public void DoAiMove()
        {
            ai.DoBestMove();
        }
        public void MakeMove()
        {
            moveMaker.MakeMove();
        }
        public bool IsRunning()
        {
            return gameResolver.IsRunning();
        }
        public void FinishGame()
        {
            gameResolver.FinishGame();
        }
        public void DrawBoard()
        {
            display.DrawBoard();
        }
        public string GetPosition()
        {
            return moveMaker.GetPosition();
        }
    }
}
