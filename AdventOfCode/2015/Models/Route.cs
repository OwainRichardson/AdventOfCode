using System.Collections.Generic;

namespace AdventOfCode._2015.Models
{
    public class Route
    {
        public List<string> Locations { get; set; } = new List<string>();
        public int TotalDistance { get; set; }
    }
}
