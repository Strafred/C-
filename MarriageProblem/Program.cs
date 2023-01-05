namespace Labs;

public static class Program
{
    private static void Main(string[] args)
    {
        var hall = new Hall();
        var princess = new Princess(hall);
        princess.ChooseContender();
        Console.WriteLine(princess.GetHappiness());
    }
}