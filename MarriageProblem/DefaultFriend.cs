namespace Labs;

public class DefaultFriend : IFriend
{
    private readonly Dictionary<string, int> _knownContenders = new Dictionary<string, int>();

    public DefaultFriend(IContenderGenerator contenderGenerator)
    {
        var contenders = contenderGenerator.Contenders;
        
        foreach (var contender in contenders)
        {
            _knownContenders.Add(contender.Name, contender.Points);
        }
    }

    public string? CompareContenders(string firstContenderName, string secondContenderName)
    {
        if (_knownContenders[firstContenderName] > _knownContenders[secondContenderName])
        {
            return firstContenderName;
        }

        if (_knownContenders[firstContenderName] < _knownContenders[secondContenderName])
        {
            return secondContenderName;
        }

        return null;
    }
}