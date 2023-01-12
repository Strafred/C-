using Labs;

namespace MarriageProblemTests;

[TestFixture]
public class ContenderGeneratorTests
{
    [Test]
    public void GenerateContenders_ShouldReturnUniqueContenders()
    {
        IContenderGenerator generator = new DefaultContenderGenerator(new DefaultProperties());

        var uniqueContenders = generator.Contenders
            .GroupBy(c => c.Name)
            .Select(g => g.First())
            .ToList();
            
        Assert.That(uniqueContenders.Count, Is.EqualTo(generator.Contenders.Count));
    }
}