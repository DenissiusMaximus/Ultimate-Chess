namespace UltimateChess;

public static class PieceSettings
{
    public static string IconById(int id, int side)
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
}