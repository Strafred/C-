using Labs;
using Microsoft.Extensions.Hosting;
using Moq;

namespace MarriageProblemTests;

[TestFixture]
public class PrincessTests
{
    private readonly Mock<IHostApplicationLifetime> _hostApplicationLifetime = new Mock<IHostApplicationLifetime>();
    private readonly Mock<IProperties> _properties;
    private Mock<IContenderGenerator> _contenderGenerator;

    private IHall _hall;
    private IFriend _friend;

    public PrincessTests()
    {
        _properties = new Mock<IProperties>();
        _properties.Setup(x => x.ContendersNumber).Returns(5);
        _properties.Setup(x => x.RejectNumber).Returns(3);
        _properties.Setup(x => x.FirstContender).Returns(5);
        _properties.Setup(x => x.ThirdContender).Returns(3);
        _properties.Setup(x => x.FifthContender).Returns(1);
    }

    [Test]
    public void ChooseContender_ShouldStayAlone_WhenDescendingSequence()
    {
        _contenderGenerator = new Mock<IContenderGenerator>();
        _contenderGenerator.Setup(x => x.Contenders)
            .Returns(new List<Contender>
            {
                new Contender("firstContender", 5),
                new Contender("secondContender", 4),
                new Contender("thirdContender", 3),
                new Contender("fourthContender", 2),
                new Contender("fifthContender", 1),
            });

        _hall = new DefaultHall(_contenderGenerator.Object);
        _friend = new DefaultFriend(_contenderGenerator.Object);

        IPrincess princess = new DefaultPrincess(
            _hall,
            _friend,
            _hostApplicationLifetime.Object,
            _properties.Object);

        princess.ChooseContender();
        Assert.That(princess.GetHappiness(), Is.EqualTo(Constants.StayAlonePoints));
    }

    [Test]
    public void ChooseContender_ShouldChooseBest_AfterSkips()
    {
        _contenderGenerator = new Mock<IContenderGenerator>();
        _contenderGenerator.Setup(x => x.Contenders)
            .Returns(new List<Contender>
            {
                new Contender("firstContender", 1),
                new Contender("secondContender", 2),
                new Contender("thirdContender", 3),
                new Contender("fourthContender", 4),
                new Contender("fifthContender", 5),
            });

        _hall = new DefaultHall(_contenderGenerator.Object);
        _friend = new DefaultFriend(_contenderGenerator.Object);

        IPrincess princess = new DefaultPrincess(
            _hall,
            _friend,
            _hostApplicationLifetime.Object,
            _properties.Object);

        princess.ChooseContender();
        Assert.That(princess.ChosenContender, Is.EqualTo("fourthContender"));
        Assert.That(princess.PastContenders.Count, Is.EqualTo(4));
    }
}