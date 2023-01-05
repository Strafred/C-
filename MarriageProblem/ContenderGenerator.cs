namespace Labs;

public class ContenderGenerator
{
    private const string ContendersFileName = "UniqueNames.csv";

    private static readonly string ProjectDirectory =
        Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

    private static readonly string ContendersFilePath = Path.Combine(ProjectDirectory, ContendersFileName);
    private readonly int _contendersNumber;

    public ContenderGenerator(int contendersNumber)
    {
        _contendersNumber = contendersNumber;
    }

    public List<Contender> GenerateContenders()
    {
        var random = new Random();
        IEnumerable<string> contendersNames = File.ReadLines(ContendersFilePath);
        IEnumerable<int> contendersPoints = Enumerable.Range(1, _contendersNumber).OrderBy(x => random.Next());

        var contenders = contendersNames
            .Zip(contendersPoints, (name, points) => new Contender(name.Replace(",", " "), points))
            .OrderBy(x => random.Next())
            .ToList();

        SeeContenders(contenders);
        return contenders;
    }

    private void SeeContenders(List<Contender> contenders)
    {
        var i = 1;
        foreach (var contender in contenders)
        {
            Console.WriteLine(i + ") " + contender);
            i++;
        }

        Console.WriteLine("----------------------");
    }
}