using TicTacToe.Board;

Board board = new Board();
while (!board.isGameFinished)
{
    board.drawBoard();
    board.makeMove();
    if (!board.IsRunning()) board.finishGame();
}