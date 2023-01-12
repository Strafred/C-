using Labs;
using Moq;

namespace MarriageProblemTests;

[TestFixture]
public class HallTests
{
    private readonly Mock<IContenderGenerator> _contenderGeneratorMock = new Mock<IContenderGenerator>();

    [Test]
    public void GetNextContender_ShouldFailWhenNoMoreContenders()
    {
        _contenderGeneratorMock.Setup(x => x.Contenders).Returns(new List<Contender>
        {
            new Contender("firstContender", 1),
            new Contender("secondContender", 2)
        });
        IHall hall = new DefaultHall(_contenderGeneratorMock.Object);

        hall.GetNextContender();
        hall.GetNextContender();

        Assert.Throws<Exception>(() => hall.GetNextContender());
    }

    [Test]
    public void GetNextContender_ShouldGetNext()
    {
        _contenderGeneratorMock.Setup(x => x.Contenders).Returns(
            new List<Contender>
            {
                new Contender("firstContender", 1),
                new Contender("secondContender", 2),
                new Contender("thirdContender", 3)
            });
        
        var hall = new DefaultHall(_contenderGeneratorMock.Object);
        Assert.That(hall.GetNextContender(), Is.EqualTo("firstContender"));
        Assert.That(hall.GetNextContender(), Is.EqualTo("secondContender"));
        Assert.That(hall.GetNextContender(), Is.EqualTo("thirdContender"));
    }
}