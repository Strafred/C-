namespace Labs;

public class DefaultContenderGenerator : IContenderGenerator
{
    public List<Contender> Contenders { get; private set; }
    
    private const string ContendersFileName = "UniqueNames.csv";
    private static readonly string ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
    private static readonly string ContendersFilePath = Path.Combine(ProjectDirectory, ContendersFileName);
    
    private readonly int _contendersNumber = Constants.ContendersNumber;
    
    public DefaultContenderGenerator()
    {
        Contenders = new List<Contender>();
        GenerateContenders();
    }

    private void GenerateContenders()
    {
        var random = new Random();
        IEnumerable<string> contendersNames = File.ReadLines(ContendersFilePath);
        IEnumerable<int> contendersPoints = Enumerable.Range(1, _contendersNumber).OrderBy(x => random.Next());

        var contenders = contendersNames
            .Zip(contendersPoints, (name, points) => new Contender(name.Replace(",", " "), points))
            .OrderBy(x => random.Next())
            .ToList();

        SeeContenders(contenders);
        Contenders = contenders;
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