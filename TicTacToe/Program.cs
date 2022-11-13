using TicTacToe;

GameHandler gameHandler = new GameHandler();
while (gameHandler.IsRunning())
{
    gameHandler.DrawBoard();
    gameHandler.MakeMove();
    if (!gameHandler.IsRunning()) gameHandler.FinishGame();
}