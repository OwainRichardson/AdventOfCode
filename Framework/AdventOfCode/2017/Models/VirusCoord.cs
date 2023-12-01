namespace AdventOfCode._2017.Models
{
    public class VirusCoord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Infected { get; set; } = false;

        public bool Clean { get; set; } = true;
        public bool Weakened { get; set; } = false;
        public bool Flagged { get; set; } = false;
    }
}
