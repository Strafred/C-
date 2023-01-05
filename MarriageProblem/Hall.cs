namespace Labs;

public class Hall
{
    private readonly List<Contender> _contendersList;
    private List<Contender>.Enumerator _contendersEnumerator;
    public Friend Friend { get; }

    public Hall()
    {
        var contenderGenerator = new ContenderGenerator(Constants.ContendersNumber);
        _contendersList = contenderGenerator.GenerateContenders();
        _contendersEnumerator = _contendersList.GetEnumerator();
        Friend = new Friend(_contendersList);
        SeeContenders();
    }

    public string? GetNextContender()
    {
        return _contendersEnumerator.MoveNext() ? _contendersEnumerator.Current.Name : null;
    }

    public int GetPrincessHappiness(Princess princess)
    {
        var chosenContenderName = princess.ChosenContender;
        
        if (chosenContenderName == Constants.NobodyChosen)
        {
            return Constants.StayAlonePoints;
        }

        if (chosenContenderName == null)
        {
            throw new Exception("Принцесса ещё никого не выбирала!");
        }
        
        var chosenContender = _contendersList.Find(contender => contender.Name == chosenContenderName);

        if (chosenContender == null)
        {
            throw new Exception("В холле нет такого контендера!!!"); // Если выбрала несуществующего
        }

        Console.WriteLine(chosenContenderName + " is chosen by princess.");
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

    private void SeeContenders()
    {
        var i = 1;
        foreach (var contender in _contendersList)
        {
            Console.WriteLine(i + ") " + contender);
            i++;
        }

        Console.WriteLine("-------------------------");
    }
}