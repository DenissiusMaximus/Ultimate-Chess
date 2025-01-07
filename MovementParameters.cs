using System.ComponentModel;

namespace UltimateChess;

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
    private int CurrI { get; set; }
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
        this.Board = parameters.Board ?? throw new ArgumentNullException("СhessBoard is null");

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

            if (Board.СhessBoard[k, l] is not null && k != ToI && l != ToJ)
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
