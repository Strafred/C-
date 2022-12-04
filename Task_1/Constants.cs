namespace Labs;

public static class Constants
{
    public const int ContendersNumber = 100;
    public const double RejectPercent = 0.37;
    
    private const string ContendersFileName = "UniqueNames.csv";
    private static readonly string ProjectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
    public static readonly string ContendersFilePath = Path.Combine(ProjectDirectory, ContendersFileName);
}