namespace AdventOfCode._2025.Models;

public class JunctionBox
{
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public Dictionary<int, double> ConnectedTo { get; set; } = new();
}
