namespace Labs;

public interface IPrincess
{
    public List<string> PastContenders { get; }
    public string? ChosenContender { get; }
    public void ChooseContender();
    public int GetHappiness();
}