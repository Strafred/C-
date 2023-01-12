namespace Labs;

public interface IProperties
{
    public int ContendersNumber { get; }
    public int RejectNumber { get; }
    
    public int FirstContender { get; }
    public int ThirdContender { get; }
    public int FifthContender { get; }
}