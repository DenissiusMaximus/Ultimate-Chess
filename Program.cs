using System.Xml;

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

        Board board = new Board();
        board.setPiece(1, 0, new PiecePawn(board, 1, 0, (int)PieceSide.Black));
        board.printBoard();
    }
}

public class MovementParameters
{
    public int currI { get; set; }
    public int currJ { get; set; }
    public int toI { get; set; }
    public int toJ { get; set; }
    public Board Board { get; set; }
}

public class MovementChek
{
    private int currI { get; set; }
    private int currJ { get; set; }
    private int toI { get; set; }
    private int toJ { get; set; }
    private int Side { get; set; }
    private Board Board { get; set; }

    public MovementChek(MovementParameters parameters)
    {
        this.toJ = parameters.toJ;
        this.toI = parameters.toI;
        this.currJ = parameters.currJ;
        this.currI = parameters.currI;
        this.Board = parameters.Board;

    }

    private bool ChekLine()
    {
        int currI = this.currI;
        int currJ = this.currJ;
        int toI = this.toI;
        int toJ = this.toJ;
        var boardSelf = this.Board;

        int k = currI;
        int l = currJ;
        while (k != toI || l != toJ)
        {
            if (toI < currI)
                k--;
            else if (toI > currI)
                k++;

            if (toJ < currJ)
                l--;
            else if (toJ > currJ)
                l++;

            if (k < 0 || k > 7 || l < 0 || l > 7)
                return false;

            if (boardSelf.board[k, l] is not null)
                return false;
        }

        return true;
    }

    public (int, int) StraightDiagonalMove()
    {
        int toI = this.toI;
        int toJ = this.toJ;

        if (Math.Abs(toI - currI) == Math.Abs(toJ - currJ))
        {
            if (ChekLine())
                return (toI - currI, toJ - currJ);
        }

        return (0, 0);
    }

    public int StraightLineMove()
    {
        int currI = this.currI;
        int currJ = this.currJ;
        int toI = this.toI;
        int toJ = this.toJ;


        if (toI == currI)
        {
            if (ChekLine())
                return toJ - currJ;
        }
        else if (toJ == currJ)
        {
            if (ChekLine())
                return toI - currI;
        }

        return 0;
    }
}

public abstract class PieceBase
{

    public abstract Board Board { get; set; }
    public abstract int currI { get; set; }
    public abstract int currJ { get; set; }
    public abstract int Side { get; set; }
    public abstract int ID { get; set; }
    public abstract List<(int, int)> DirectionsList { get; set; }

    public MovementParameters setMovementParameters(int currI, int currJ, int toI, int toJ, Board Board)
    {
        return new MovementParameters()
        {
            currI = currI,
            currJ = currJ,
            toI = toI,
            toJ = toJ,
            Board = Board
        };
    }

}

public class PiecePawn : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int currI { get; set; }
    public override int currJ { get; set; }
    public override int Side { get; set; }
    public override List<(int, int)> DirectionsList { get; set; }

    public PiecePawn(Board Board, int currI, int currJ, int Side)
    {
        this.currI = currI;
        this.currJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.DirectionsList = new List<(int, int)>();
        this.ID = 1;

    }

    public void setMovementRules(int toI, int toJ)
    {
        DirectionsList.Clear();

        if (this.Side == 1) // white
        {
            if (Board.board[toI, toJ] is null)
                DirectionsList.Add((1, 0));

            if (this.currI == 1)
                if (MovementChek.StraightLineMove() == 2)
                    DirectionsList.Add((2, 0));
            DirectionsList.Add((2, 0));
        }
        else if (this.Side == 2) // black
        {
            DirectionsList.Add((-1, 0));
            if (this.currI == 6)
                DirectionsList.Add((-2, 0));
        }
    }
}

//public class Rook : PieceBase
//{

//}

//public class Knight : PieceBase
//{

//}

//public class Bishop : PieceBase
//{

//}

//public class Queen : PieceBase
//{

//}

//public class King : PieceBase
//{

//}

public class Board
{
    public object[,] board;

    public Board()
    {
        board = new object[8, 8];
    }

    public void setPiece(int i, int j, object Piece)
    {
        board[i, j] = Piece;
    }

    public string IconById(int id, int side)
    {
        if (side == 1)
        {
            Dictionary<int, string> figuresDic = new()
            {
                {1, "♟"},
                {2, "♜"},
                {3, "♞"},
                {4, "♝"},
                {5, "♛"},
                {6, "♚"},
                {0, "-"}
            };
            return figuresDic[id];
        }
        else
        {
            Dictionary<int, string> figuresDic = new()
            {
                {1, "♙"},
                {2, "♖"},
                {3, "♘"},
                {4, "♗"},
                {5, "♕"},
                {6, "♔"},
                {0, "-"}
            };
            return figuresDic[id];
        }

    }

    public void printBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == null)
                    Console.Write("□ ");
                else if (board[i, j] is PieceBase piece)
                {
                    Console.Write(IconById(piece.ID, piece.Side) + " ");
                }

            }
            Console.WriteLine();
        }
    }

    //public void updatePrintedBoard()
    //{
    //    Console.SetCursorPosition(0, 0);
    //    Console.Clear();
    //    for (int i = 0; i < 8; i++)
    //    {
    //        for (int j = 0; j < 8; j++)
    //        {
    //            if (board[i, j] == null)
    //                Console.Write("□ ");
    //        }
    //        Console.WriteLine();
    //    }
    //}
}