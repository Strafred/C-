using System.Collections;

namespace Labs;

public class Hall
{
    private readonly List<Contender> _contendersList;
    private List<Contender>.Enumerator _contendersEnumerator;
    private readonly Friend _friend;
    public Friend Friend { get; init; }
    
    public Hall()
    {
        _contendersList = GenerateContenders(Constants.ContendersNumber);
        _contendersEnumerator = _contendersList.GetEnumerator();
        _friend = new Friend(_contendersList);
    }

    private static List<Contender> GenerateContenders(int contendersNumber)
    {
        var random = new Random();
        IEnumerable<string> contendersNames = File.ReadLines(Constants.ContendersFilePath);
        IEnumerable<int> contendersPoints = Enumerable.Range(1, contendersNumber).OrderBy(x => random.Next());

        return contendersNames
            .Zip(contendersPoints, (name, points) => new Contender(name.Replace(","," "), points))
            .OrderBy(x => random.Next())
            .ToList();
    }
    
    public Contender GetNextContender()
    {
        return _contendersEnumerator.MoveNext() ? _contendersEnumerator.Current : null;
    }

    public void SeeContenders()
    {
        foreach (var contender in _contendersList)
        {
            Console.WriteLine(contender);
        }
        Console.WriteLine("-------------------------");
    }
}