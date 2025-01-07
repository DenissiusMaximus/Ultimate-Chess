namespace UltimateChess;
public abstract class PieceBase
{
    public abstract Board Board { get; set; }
    public abstract int CurrI { get; set; }
    public abstract int CurrJ { get; set; }
    public abstract int Side { get; set; }
    public abstract int ID { get; set; }
    public abstract bool CanMove(int toI, int toJ);
    public abstract bool CanAttack(int toI, int toJ);

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

    public bool ChekBorders(int toI, int toJ)
    {
        if (CurrI + toI < 0 || CurrI + toI > 7 || CurrJ + toJ < 0 || CurrJ + toJ > 7)
            return false;

        return true;
    }

    public void Move(int toI, int toJ)
    {
        if (ChekBorders(toI, toJ) is false)
            return;

        if (Board.СhessBoard[toI, toJ] is null)
        {
            if (CanMove(toI, toJ))
            {
                ReplacePieces(toI, toJ);

                return;
            }
        }

        Attack(toI, toJ);

    }

    private void Attack(int toI, int toJ)
    {
        PieceBase p1 = (PieceBase)Board.СhessBoard[CurrI, CurrJ];
        PieceBase p2 = (PieceBase)Board.СhessBoard[toI, toJ];

        if (Board.СhessBoard[toI, toJ] is not null)
        {
            if (p1.Side == p2.Side)
                return;

            if (CanAttack(toI, toJ))
            {
                Board.KillPiece((PieceBase)Board.СhessBoard[toI, toJ]);

                ReplacePieces(toI, toJ);
            }
        }
        else if(Board.СhessBoard[CurrI, CurrJ] is PiecePawn)
        {
            int pos = 1;

            if (Board.СhessBoard[CurrI, CurrJ - 1] is PiecePawn)
                pos = -1;

            p2 = (PiecePawn)Board.СhessBoard[CurrI, CurrJ + pos];

            if (p1.Side == p2.Side)
                return;

            if (CanAttack(toI, toJ))
            {
                Board.KillPiece((PieceBase)Board.СhessBoard[CurrI, CurrJ + pos]);

                ReplacePieces(toI, toJ);
            }
        }
    }

    private void ReplacePieces(int i, int j)
    {
        Board.СhessBoard[i, j] = this;
        Board.СhessBoard[CurrI, CurrJ] = null;
        CurrI = i;
        CurrJ = j;
    }

}

public class PiecePawn : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int CurrI { get; set; }
    public override int CurrJ { get; set; }
    public override int Side { get; set; }
    private bool Jump = false;

    public PiecePawn(Board Board, int currI, int currJ, int Side)
    {
        this.CurrI = currI;
        this.CurrJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.ID = 1;
    }

    public override bool CanAttack(int toI, int toJ)
    {
        int pos = 0;
        int posSide = 0;

        if (Side == (int)PieceSide.White)
            posSide = -1;
        else if (Side == (int)PieceSide.Black)
            posSide = 1;


        if (CurrI - 1 == toI && CurrJ - 1 == toJ)
            return true;
        else if (CurrI + posSide == toI && CurrJ + 1 == toJ)
            return true;

        if (Board.СhessBoard[CurrI, CurrJ - 1] is PiecePawn)
            pos = -1;
        else if (Board.СhessBoard[CurrI, CurrJ + 1] is PiecePawn)
            pos = 1;

        PiecePawn p = (PiecePawn)Board.СhessBoard[CurrI, CurrJ + pos];

        if (p.Jump)
            if (CurrI + posSide == toI && CurrJ - pos == toJ)
                return true;

        return false;
    }

    public override bool CanMove(int toI, int toJ)
    {

        MovementChek MovementChek = new MovementChek(SetMovementParameters(CurrI, CurrJ, toI, toJ, Side, Board));

        int pos1 = 0;
        int pos2 = 0;
        int posSide = -1;



        if (this.Side == (int)PieceSide.White)
        {
            pos1 = -1;
            pos2 = -2;
            posSide = 6;
        }
        else if (this.Side == (int)PieceSide.Black)
        {
            pos1 = 1;
            pos2 = 2;
            posSide = 1;
        }

        if (Board.СhessBoard[toI, toJ] == null)
        {
            if (toI == CurrI + pos1 && toJ == CurrJ)
                return true;

            else if (CurrI == posSide && toI == CurrI + pos2 && toJ == CurrJ)
            {
                Jump = true;
                return true;
            }
        }

        return false;
    }

}

public class PieceRook : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int CurrI { get; set; }
    public override int CurrJ { get; set; }
    public override int Side { get; set; }

    public PieceRook(Board Board, int currI, int currJ, int Side)
    {
        this.CurrI = currI;
        this.CurrJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.ID = 2;
    }

    public override bool CanAttack(int toI, int toJ)
    {
        return CanMove(toI, toJ);
    }

    public override bool CanMove(int toI, int toJ)
    {
        MovementChek MovementChek = new MovementChek(SetMovementParameters(CurrI, CurrJ, toI, toJ, Side, Board));

        if (toI == CurrI)
        {
            if (CurrJ + MovementChek.StraightLineMove() == toJ)
                return true;
        }
        else if (toJ == CurrJ)
        {
            if (CurrI + MovementChek.StraightLineMove() == toI)
                return true;
        }

        return false;
    }
}

public class PieceKnight : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int CurrI { get; set; }
    public override int CurrJ { get; set; }
    public override int Side { get; set; }

    public PieceKnight(Board Board, int currI, int currJ, int Side)
    {
        this.CurrI = currI;
        this.CurrJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.ID = 3;
    }

    public override bool CanAttack(int toI, int toJ)
    {
        return CanMove(toI, toJ);
    }

    public override bool CanMove(int toI, int toJ)
    {
        List<(int, int)> movementList = new()
        {
            (+2, -1), (+2, +1),
            (-2, +1), (-2, -1),
            (-1, +2), (+1, +2),
            (-1, -2), (+1, -2),
        };

        foreach (var (i, j) in movementList)
        {
            if (ChekBorders(i, j))
                continue;

            if (Board.СhessBoard[CurrI + i, CurrJ + j] == null)
            {
                if (CurrI + i == toI && CurrJ + j == toJ)
                    return true;
            }
        }

        return false;
    }
}

public class PieceBishop : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int CurrI { get; set; }
    public override int CurrJ { get; set; }
    public override int Side { get; set; }

    public PieceBishop(Board Board, int currI, int currJ, int Side)
    {
        this.CurrI = currI;
        this.CurrJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.ID = 4;
    }

    public override bool CanAttack(int toI, int toJ)
    {
        return CanMove(toI, toJ);
    }

    public override bool CanMove(int toI, int toJ)
    {

        MovementChek MovementChek = new MovementChek(SetMovementParameters(CurrI, CurrJ, toI, toJ, Side, Board));

        var checkDiagonalMove = MovementChek.StraightDiagonalMove();

        if (CurrI + checkDiagonalMove.Item1 == toI && CurrJ + checkDiagonalMove.Item2 == toJ)
            return true;

        return false;
    }
}

public class PieceQueen : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int CurrI { get; set; }
    public override int CurrJ { get; set; }
    public override int Side { get; set; }

    public PieceQueen(Board Board, int currI, int currJ, int Side)
    {
        this.CurrI = currI;
        this.CurrJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.ID = 5;
    }

    public override bool CanAttack(int toI, int toJ)
    {
        return CanMove(toI, toJ);
    }

    public override bool CanMove(int toI, int toJ)
    {

        MovementChek MovementChek = new MovementChek(SetMovementParameters(CurrI, CurrJ, toI, toJ, Side, Board));

        if (toI == CurrI)
        {
            if (CurrJ + MovementChek.StraightLineMove() == toJ)
                return true;
        }
        else if (toJ == CurrJ)
        {
            if (CurrI + MovementChek.StraightLineMove() == toI)
                return true;
        }

        var checkDiagonalMove = MovementChek.StraightDiagonalMove();

        if (CurrI + checkDiagonalMove.Item1 == toI && CurrJ + checkDiagonalMove.Item2 == toJ)
            return true;


        return false;
    }
}

public class PieceKing : PieceBase
{
    public override int ID { get; set; }
    public override Board Board { get; set; }
    public override int CurrI { get; set; }
    public override int CurrJ { get; set; }
    public override int Side { get; set; }

    public PieceKing(Board Board, int currI, int currJ, int Side)
    {
        this.CurrI = currI;
        this.CurrJ = currJ;
        this.Board = Board;
        this.Side = Side;
        this.ID = 6;
    }

    public bool IsInCheck(int checkI = -1, int checkJ = -1)
    {
        if(checkJ == -1)
        {
            checkJ = CurrJ;
            checkI = CurrI;
        }

        foreach (var p in Board.AlivePieces)
        {
            int i = p.CurrI;
            int j = p.CurrJ;

            if (p.Side != Side)
            {
                if (p.CanAttack(checkI, checkJ))
                    return true;
            }
        }

        return false;
    }

    public override bool CanAttack(int toI, int toJ)
    {
        if(IsInCheck(toI, toJ))
            return false;

        return CanMove(toI, toJ);
    }

    public override bool CanMove(int toI, int toJ)
    {
        if (IsInCheck(toI, toJ))
            return false;

        MovementChek MovementChek = new MovementChek(SetMovementParameters(CurrI, CurrJ, toI, toJ, Side, Board));

        List<(int, int)> movementList = new()
        {
            (+1, 0), (-1, 0),
            (0, +1), (0, -1),
            (+1, +1), (-1, -1),
            (+1, -1), (-1, +1)
        };

        foreach (var (i, j) in movementList)
        {
            if (ChekBorders(i, j))
                continue;

            if (Board.СhessBoard[CurrI + i, CurrJ + j] == null)
            {
                if (CurrI + i == toI && CurrJ + j == toJ)
                    return true;
            }
        }

        return false;
    }
}