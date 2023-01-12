using Labs;
using Moq;

namespace MarriageProblemTests;

[TestFixture]
public class FriendTests
{
    private readonly Mock<IContenderGenerator> _contenderGeneratorMock = new Mock<IContenderGenerator>();
    
    [Test]
    public void CompareContenders_ShouldReturnBestOne()
    {
        _contenderGeneratorMock.Setup(x => x.Contenders).Returns(new List<Contender>
        {
            new Contender("firstContender", 1),
            new Contender("secondContender", 2)
        });
        var friend = new DefaultFriend(_contenderGeneratorMock.Object);  
        
        var knownPrincessContenders = new List<string>
        {
            "firstContender",
            "secondContender"
        };

        var bestContender = friend.CompareContenders("firstContender", "secondContender", knownPrincessContenders);
        
        Assert.That(bestContender, Is.EqualTo("secondContender"));
    }

    [Test]
    public void CompareContenders_ShouldThrowException_WhenPrincessDoesNotKnowContenders()
    {
        _contenderGeneratorMock.Setup(x => x.Contenders).Returns(new List<Contender>
        {
            new Contender("firstContender", 1),
            new Contender("secondContender", 2)
        });
        var friend = new DefaultFriend(_contenderGeneratorMock.Object);  
        
        var knownPrincessContenders = new List<string>
        {
            "firstContender",
            "Vanya Ivanov"
        };
        
        Assert.Throws<Exception>(() => friend.CompareContenders("firstContender", "secondContender", knownPrincessContenders));
    }
}