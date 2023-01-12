namespace Labs;

public interface IPrincess
{
    public string? ChosenContender { get; }
    public void ChooseContender();
    public int GetHappiness();
}