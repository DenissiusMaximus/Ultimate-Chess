using System.Drawing;
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

        board.SetPiece(new PiecePawn(board, 1, 0, (int)PieceSide.White));
        board.SetPiece(new PiecePawn(board, 1, 1, (int)PieceSide.White));
        board.SetPiece(new PiecePawn(board, 1, 2, (int)PieceSide.White));

        board.PrintBoard();

        string input;
        string[] cord;
        int i, j;
        int toi, toj;

        do
        {
            input = Console.ReadLine();
            if (input == "0")
                break;

            cord = input.Split(' ');

            i = int.Parse(cord[0]);
            j = int.Parse(cord[1]);

            input = Console.ReadLine();
            if (input == "0")
                break;

            cord = input.Split(' ');

            toi = int.Parse(cord[0]);
            toj = int.Parse(cord[1]);

            PieceBase piece = (PieceBase)board.board[i, j];
            piece.Move(toi, toj);

            board.updatePrintedBoard();

        } while (input != "0");
    }
}

public class MovementParameters
{
    public int currI { get; set; }
    public int currJ { get; set; }
    public int toI { get; set; }
    public int toJ { get; set; }
    public int Side { get; set; }
    public Board? Board { get; set; }
}

public class MovementChek
{
    private  int CurrI { get; set; }
    private int CurrJ { get; set; }
    private int ToI { get; set; }
    private int ToJ { get; set; }
    private Board Board { get; set; }

    public MovementChek(MovementParameters parameters)
    {
        this.ToJ = parameters.toJ;
        this.ToI = parameters.toI;
        this.CurrJ = parameters.currJ;
        this.CurrI = parameters.currI;
        this.Board = parameters.Board ?? throw new ArgumentNullException("Board is null");

    }

    private bool ChekLine()
    {
        int k = CurrI;
        int l = CurrJ;
        while (k != ToI || l != ToJ)
        {
            if (ToI < CurrI)
                k--;
            else if (ToI > CurrI)
                k++;

            if (ToJ < CurrJ)
                l--;
            else if (ToJ > CurrJ)
                l++;

            if (k < 0 || k > 7 || l < 0 || l > 7)
                return false;

            if (Board.board[k, l] is not null)
                return false;
        }

        return true;
    }

    public (int, int) StraightDiagonalMove()
    {
        if (Math.Abs(ToI - CurrI) == Math.Abs(ToJ - CurrJ))
        {
            if (ChekLine())
                return (ToI - CurrI, ToJ - CurrJ);
        }

        return (0, 0);
    }

    public int StraightLineMove()
    {
        if (ToI == CurrI)
        {
            if (ChekLine())
                return ToJ - CurrJ;
        }
        else if (ToJ == CurrJ)
        {
            if (ChekLine())
                return ToI - CurrI;
        }

        return 0;
    }
}

public abstract class PieceBase
{

    public abstract Board Board { get; set; }
    public abstract int CurrI { get; set; }
    public abstract int CurrJ { get; set; }
    public abstract int Side { get; set; }
    public abstract int ID { get; set; }
    public abstract List<(int, int)> DirectionsList { get; set; }
    public abstract void SetMovementRules(int toI, int toJ);


    public MovementParameters SetMovementParameters(int currI, int currJ, int toI, int toJ, int Side, Board Board)
    {
        return new MovementParameters()
        {
            currI = currI,
            currJ = currJ,
            toI = toI,
            toJ = toJ,
            Side = Side,
            Board = Board
        };
    }

    public void Move(int toI, int toJ)
    {
        SetMovementRules(toI, toJ);

        if (DirectionsList.Contains((toI - CurrI, toJ - CurrJ)))
        {
            Board.board[toI, toJ] = this;
            Board.board[CurrI, CurrJ] = null;
            CurrI = toI;
            CurrJ = toJ;
        }
        else
            Console.WriteLine("Invalid move");
    }

}

public class PiecePawn : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int CurrI { get; set; }
    public override int CurrJ { get; set; }
    public override int Side { get; set; }
    public override List<(int, int)> DirectionsList { get; set; }

    public PiecePawn(Board Board, int currI, int currJ, int Side)
    {
        this.CurrI = currI;
        this.CurrJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.DirectionsList = new List<(int, int)>();
        this.ID = 1;
    }

    public override void SetMovementRules(int toI, int toJ)
    {
        DirectionsList.Clear();

        MovementChek MovementChek = new MovementChek(SetMovementParameters(CurrI, CurrJ, toI, toJ, Side, Board));

        if (this.Side == 1) // white
        {
            if (Board.board[toI, toJ] is null)
                DirectionsList.Add((1, 0));

            if (this.CurrI == 1)
                if (MovementChek.StraightLineMove() == 2)
                    DirectionsList.Add((2, 0));
        }
        else if (this.Side == 2) // black
        {
            if (Board.board[toI, toJ] is null)
                DirectionsList.Add((-1, 0));

            if (this.CurrI == 6)
                if (MovementChek.StraightLineMove() == 2)
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

    public void SetPiece(object piece)
    {
        if (piece is PieceBase p)
            board[p.CurrI, p.CurrJ] = piece;
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

    public void PrintBoard()
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

    public void updatePrintedBoard()
    {
        Console.SetCursorPosition(0, 0);
        Console.Clear();

        PrintBoard();
    }
}