namespace Labs;

public class Contender
{
    public string Name { get; set; }
    public int Points { get; set; }
    
    public Contender(string name, int points)
    {
        Name = name;
        Points = points;
    }
    
    public override string ToString()
    {
        return Name + " " + Points;
    }
}