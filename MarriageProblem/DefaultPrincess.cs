using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Labs;

public class DefaultPrincess : IHostedService, IPrincess
{
    private List<string> _pastContenders = new List<string>();
    public string? ChosenContender { get; private set; }

    private readonly IHall _hall;
    private readonly IFriend _friend;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IHostEnvironment _hostEnvironment;

    public DefaultPrincess(IHall hall, IFriend friend, IHostApplicationLifetime appLifetime, IHostEnvironment environment)
    {
        _hall = hall;
        _friend = friend;
        _appLifetime = appLifetime;
        _hostEnvironment = environment;
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
        var knownContenders = new List<string>(_pastContenders);
        knownContenders.Add(contender);

        foreach (var rejectedContender in _pastContenders)
        {
            var betterContender = _friend.CompareContenders(contender, rejectedContender, knownContenders);
            if (betterContender == rejectedContender)
            {
                _pastContenders = knownContenders;
                return false;
            }
        }

        _pastContenders = knownContenders;
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
            _pastContenders.Add(contenderToSkip);
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