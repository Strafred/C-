namespace Labs;

public static class Program
{
    private static void Main(string[] args)
    {
        var contenderGenerator = new ContenderGenerator(Constants.ContendersNumber);
        var contenders = contenderGenerator.GenerateContenders();

        var hall = new Hall(contenders);
        var friend = new Friend(contenders);
        var princess = new Princess(hall, friend);

        princess.ChooseContender();
        Console.WriteLine(princess.GetHappiness() + " is princess happiness");
    }
}