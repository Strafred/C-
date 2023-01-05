namespace Labs;

public class Hall
{
    private readonly List<Contender> _contendersList;
    private List<Contender>.Enumerator _contendersEnumerator;

    public Hall(List<Contender> contenders)
    {
        _contendersList = new List<Contender>(contenders);
        _contendersEnumerator = _contendersList.GetEnumerator();
    }

    public string? GetNextContender()
    {
        return _contendersEnumerator.MoveNext() ? _contendersEnumerator.Current.Name : null;
    }

    public List<Contender> RevealContenders(Princess princess)
    {
        if (princess.ChosenContender is not null)
        {
            return _contendersList;
        }

        throw new Exception("Princess has not chosen a contender");
    }
}