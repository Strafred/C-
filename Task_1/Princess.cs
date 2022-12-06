namespace Labs;

public class Princess
{
    private readonly List<string> _rejectedContenders = new List<string>();
    public string? ChosenContender { get; private set; }
    private readonly Hall _hall;

    public Princess(Hall hall)
    {
        _hall = hall;
    }

    private bool IsBest(string contender)
    {
        foreach (var rejectedContender in _rejectedContenders)
        {
            var betterContender = _hall.Friend.CompareContenders(contender, rejectedContender);
            if (betterContender == rejectedContender)
            {
                return false;
            }
        }

        return true;
    }

    public void ChooseContender() // should throw an error when calling second time
    {
        for (var i = 0; i < Constants.RejectNumber; i++)
        {
            var contenderToSkip = _hall.GetNextContender();
            _rejectedContenders.Add(contenderToSkip);
        }

        var newContender = _hall.GetNextContender();
        while (newContender is not null)
        {
            if (IsBest(newContender))
            {
                ChosenContender = newContender;
                return;
            }

            newContender = _hall.GetNextContender();
        }

        ChosenContender = Constants.NobodyChosen;
    }

    public int GetHappiness()
    {
        return _hall.GetPrincessHappiness(this);
    }
}