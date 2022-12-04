namespace Labs;

public class Princess
{
    private List<Contender> _pastContenders = new List<Contender>();

    public void ProcessContenders(Hall hall)
    {
        var newContender = hall.GetNextContender();
        while (newContender is not null)
        {
            // first 37% go in trash
            // TODO: hall.Friend.compare(...)
            
            _pastContenders.Add(newContender);
            newContender = hall.GetNextContender();
        }
    }
}