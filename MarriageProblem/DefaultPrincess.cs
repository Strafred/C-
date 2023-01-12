using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Labs;

public class DefaultPrincess : IHostedService, IPrincess
{
    public List<string> PastContenders { get; private set; } = new List<string>();
    public string? ChosenContender { get; private set; }

    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IProperties _properties;

    public DefaultPrincess(IHall hall, IFriend friend, IHostApplicationLifetime appLifetime, IProperties properties)
    {
        _hall = hall;
        _friend = friend;
        _appLifetime = appLifetime;
        _properties = properties;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() =>
        {
            try
            {
                ChooseContender();
                Console.WriteLine(GetHappiness() + " is princess happiness");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _appLifetime.StopApplication();
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private bool IsBest(string contender)
    {
        var knownContenders = new List<string>(PastContenders);
        knownContenders.Add(contender);

        foreach (var rejectedContender in PastContenders)
        {
            var betterContender = _friend.CompareContenders(contender, rejectedContender, knownContenders);
            if (betterContender == rejectedContender)
            {
                PastContenders = knownContenders;
                return false;
            }
        }

        PastContenders = knownContenders;
        return true;
    }

    public void ChooseContender()
    {
        if (ChosenContender is not null)
        {
            throw new Exception("Contender is already chosen!");
        }

        for (var i = 0; i < _properties.RejectNumber; i++)
        {
            var contenderToSkip = _hall.GetNextContender();
            PastContenders.Add(contenderToSkip);
        }
        
        for (int i = 0; i < _properties.ContendersNumber - _properties.RejectNumber; i++)
        {
            var newContender = _hall.GetNextContender();
            
            if (IsBest(newContender))
            {
                ChosenContender = newContender;
                return;
            }
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

        if (chosenContender.Points == _properties.FirstContender)
        {
            return Constants.NormalChoicePoints;
        }
        if (chosenContender.Points == _properties.ThirdContender)
        {
            return Constants.GoodChoicePoints;
        }
        if (chosenContender.Points == _properties.FifthContender)
        {
            return Constants.BestChoicePoints;
        }

        return Constants.BadChoicePoints;
    }
}