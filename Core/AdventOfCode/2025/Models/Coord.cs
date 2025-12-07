using System.Text;

namespace AdventOfCode._2025.Models;

public class Coord
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Value { get; set; }
    public bool IsStart { get; set; }
    public int Id { get; set; }
    public StringBuilder Path { get; set; }
}
