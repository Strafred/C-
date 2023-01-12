namespace Labs;

public class DefaultHall : IHall
{
    private readonly List<Contender> _contendersList;
    private List<Contender>.Enumerator _contendersEnumerator;

    public DefaultHall(IContenderGenerator contendersGenerator)
    {
        var contenders = contendersGenerator.Contenders;
        
        _contendersList = new List<Contender>(contenders);
        _contendersEnumerator = _contendersList.GetEnumerator();
    }

    public string? GetNextContender()
    {
        if (_contendersEnumerator.MoveNext())
        {
            return _contendersEnumerator.Current.Name;
        }
        else
        {
            throw new Exception("No more contenders");
        }
    }

    public List<Contender> RevealContenders(DefaultPrincess defaultPrincess)
    {
        if (defaultPrincess.ChosenContender is not null)
        {
            return _contendersList;
        }

        throw new Exception("Princess has not chosen a contender");
    }
}