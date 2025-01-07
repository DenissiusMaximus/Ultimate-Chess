namespace UltimateChess;

class ExceptionHandler
{
    public static List<string> Errors = new List<string>();

    public static void AddError(string msg)
    {
        Errors.Add(msg);
    }

    public static void PrintErrors()
    {
        foreach (var error in Errors)
        {
            Console.WriteLine(error);
        }
    }

    public static void PrintAndAddError(string msg)
    {
        Errors.Add(msg);
        Console.WriteLine(msg);
    }

    public static void ThrowError(string msg)
    {
        throw new Exception(msg);
    }
}
