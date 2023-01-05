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

        return contendersNames
            .Zip(contendersPoints, (name, points) => new Contender(name.Replace(",", " "), points))
            .OrderBy(x => random.Next())
            .ToList();
    }
}