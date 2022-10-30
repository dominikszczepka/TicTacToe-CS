using TicTacToe;

Board board = new Board();
while (board.IsRunning())
{
    board.DrawBoard();
    board.MakeMove();
    if (!board.IsRunning()) board.FinishGame();
}