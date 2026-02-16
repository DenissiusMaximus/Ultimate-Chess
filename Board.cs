namespace UltimateChess;

public class Board
{
    public PieceBase[,] СhessBoard;
    public List<PieceBase> AlivePieces = new();
    public List<PieceBase> DeadPieces = new();

    public Board()
    {
        СhessBoard = new PieceBase[8, 8];
    }

    public void KillPiece(PieceBase piece)
    {
        if (piece is null)
            ExceptionHandler.PrintAndAddError("Piece is null");

        this.СhessBoard[piece.CurrI, piece.CurrJ] = null;
        DeadPieces.Add(piece);
        AlivePieces.Remove(piece);
    }

    public void SetPiece(PieceBase piece)
    {
        СhessBoard[piece.CurrI, piece.CurrJ] = piece;

        AlivePieces.Add(piece);
    }

    public void SetBasePieces()
    {
        for (int k = 0; k < 8; k++)
        {
            SetPiece(new PiecePawn(this, 1, k, (int)PieceSide.Black));
            SetPiece(new PiecePawn(this, 6, k, (int)PieceSide.White));
        }

        for(int k = 0; k < 8; k += 7)
        {
            SetPiece(new PieceRook(this, k, 0, (int)PieceSide.Black));
            SetPiece(new PieceRook(this, k, 7, (int)PieceSide.Black));

            SetPiece(new PieceKnight(this, k, 1, (int)PieceSide.Black));
            SetPiece(new PieceKnight(this, k, 6, (int)PieceSide.Black));

            SetPiece(new PieceBishop(this, k, 2, (int)PieceSide.Black));
            SetPiece(new PieceBishop(this, k, 5, (int)PieceSide.Black));

            SetPiece(new PieceQueen(this, k, 3, (int)PieceSide.Black));
            SetPiece(new PieceQueen(this, k, 3, (int)PieceSide.White));

            SetPiece(new PieceKing(this, k, 4, (int)PieceSide.Black));
            SetPiece(new PieceKing(this, k, 4, (int)PieceSide.White));
        }
                
    }

    public void PrintBoard()
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
                if (СhessBoard[i, j] == null)
                    if ((i + j) % 2 == 0)
                        Console.Write("○    ");
                    else
                        Console.Write("⊖    ");
                else if (СhessBoard[i, j] is PieceBase piece)
                {
                    Console.Write(PieceSettings.IconById(piece.ID, piece.Side) + "    ");
                }

            }
            Console.WriteLine();
        }
    }

    public void updatePrintedBoard()
    {
        Console.SetCursorPosition(0, 0);
        Console.Clear();

        PrintBoard();
    }
}