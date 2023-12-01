using System.Collections.Generic;

namespace AdventOfCode._2018.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public Dictionary<string, bool> Coords = new Dictionary<string, bool>();
        public bool Overlapped { get; set; } = false;
    }
}
