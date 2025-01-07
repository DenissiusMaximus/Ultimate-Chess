namespace UltimateChess;

public enum PieceSide
{
    White = 1,
    Black = 2
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var board = new Board();

        board.SetPiece(new PiecePawn(board, 1, 1, (int)PieceSide.Black));
        board.SetPiece(new PiecePawn(board, 3, 2, (int)PieceSide.White));

        PieceBase piece1 = (PieceBase)board.СhessBoard[1, 1];
        PieceBase piece2 = (PieceBase)board.СhessBoard[3, 2];

        board.PrintBoard();

        piece1.Move(3, 1);
        board.PrintBoard();
        Console.WriteLine();
        Console.WriteLine();

        piece2.Move(2, 1);

        board.PrintBoard();
        Console.WriteLine();
        Console.WriteLine();
    }
}




//      (0,0)  (0,1)  (0,2)  (0,3)  (0,4)  (0,5)  (0,6)  (0,7)

//      (1,0)  (1,1)  (1,2)  (1,3)  (1,4)  (1,5)  (1,6)  (1,7)

//      (2,0)  (2,1)  (2,2)  (2,3)  (2,4)  (2,5)  (2,6)  (2,7)

//      (3,0)  (3,1)  (3,2)  (3,3)  (3,4)  (3,5)  (3,6)  (3,7)

//      (4,0)  (4,1)  (4,2)  (4,3)  (4,4)  (4,5)  (4,6)  (4,7)

//      (5,0)  (5,1)  (5,2)  (5,3)  (5,4)  (5,5)  (5,6)  (5,7)

//      (6,0)  (6,1)  (6,2)  (6,3)  (6,4)  (6,5)  (6,6)  (6,7)

//      (7,0)  (7,1)  (7,2)  (7,3)  (7,4)  (7,5)  (7,6)  (7,7)

