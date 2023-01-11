namespace Labs;

public interface IContenderGenerator
{
    public List<Contender> Contenders { get; }
    public void GenerateContenders();
}