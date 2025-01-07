namespace UltimateChess;
class Ways
{
    private Board board;
    private PieceBase piece;
    private PieceBase piece2 = null;
    public int[,] WaysList;

    public Ways(Board board, PieceBase piece)
    {
        this.board = board;
        this.piece = piece;
    }
    public Ways(Board board, PieceBase piece, PieceBase piece2)
    {
        this.board = board;
        this.piece = piece;
        this.piece2 = piece2;
    }

    public void PrintWays()
    {
        for (int i = -1; i < 8; i++)
        {

            if (i == -1)
            {
                for (int j = -1; j < 8; j++)
                {
                    if (j == -1)
                        Console.Write("     ");
                    else
                        Console.Write(j + "    ");
                }

                Console.WriteLine();
                continue;
            }

            Console.WriteLine();
            Console.Write(i + "    ");

            for (int j = 0; j < 8; j++)
            {
                if (board.СhessBoard[i, j] != null)
                {
                    if (board.СhessBoard[i, j] is PieceBase p)

                        Console.Write(board.IconById(p.ID, p.Side) + "    ");
                }

                else if (WaysList[i, j] == 0)
                    if ((i + j) % 2 == 0)
                        Console.Write("○    ");
                    else
                        Console.Write("⊖    ");

                else if (WaysList[i, j] == 1)
                {
                    Console.Write("██   ");
                }

            }
            Console.WriteLine();
        }
    }

    public void GetWays()
    {
        WaysList = new int[8, 8];

        board.SetPiece(piece);

        if (piece2 != null)
            board.SetPiece(piece2);

        int currI = piece.CurrI;
        int currJ = piece.CurrJ;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (piece.CanMove(i, j))
                    WaysList[i, j] = 1;
            }
        }
    }
}