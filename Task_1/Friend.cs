namespace Labs;

public class Friend
{
    private readonly List<Contender> _knownContenders;

    public List<Contender> KnownContenders { get; set; }
    
    public Friend(List<Contender> contenders)
    {
        _knownContenders = contenders;
    }
}