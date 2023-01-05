namespace Labs;

public class Princess
{
    private readonly List<string> _rejectedContenders = new List<string>();
    public string? ChosenContender { get; private set; }
    private readonly Hall _hall;
    private readonly Friend _friend;

    public Princess(Hall hall, Friend friend)
    {
        _hall = hall;
        _friend = friend;
    }

    private bool IsBest(string contender)
    {
        foreach (var rejectedContender in _rejectedContenders)
        {
            var betterContender = _friend.CompareContenders(contender, rejectedContender);
            if (betterContender == rejectedContender)
            {
                return false;
            }
        }

        return true;
    }

    public void ChooseContender()
    {
        if (ChosenContender is not null)
        {
            throw new Exception("Contender is already chosen!");
        }

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
        if (ChosenContender == Constants.NobodyChosen)
        {
            Console.WriteLine(Constants.NobodyChosen);
            return Constants.StayAlonePoints;
        }

        var chosenContender = _hall.RevealContenders(this).Find(contender => contender.Name == ChosenContender);
        if (chosenContender == null)
        {
            throw new Exception("No such contender!");
        }

        Console.WriteLine(ChosenContender + " is chosen by princess.");
        Console.WriteLine(chosenContender.Points + " - his points");
        switch (chosenContender.Points)
        {
            case Constants.FirstContender:
                return Constants.NormalChoicePoints;
            case Constants.ThirdContender:
                return Constants.GoodChoicePoints;
            case Constants.FifthContender:
                return Constants.BestChoicePoints;
            default:
                return Constants.BadChoicePoints;
        }
    }
}