namespace Labs;

public interface IHall
{
    public string? GetNextContender();
    public List<Contender> RevealContenders(Princess princess);
}