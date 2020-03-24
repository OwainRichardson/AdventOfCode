namespace AdventOfCode._2019.Models
{
    public class RobotCoord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Colour { get; set; } = 0;
        public bool ColourChanged { get; set; } = false;
        public bool Current { get; set; }
    }
}
