using System.Collections.Generic;

namespace AdventOfCode._2021.Models
{
    public class Cave
    {
        public string Name { get; set; }
        public bool BigCave { get; set; }
        public List<string> OtherCaves { get; set; }  = new List<string>();
        public bool Visited { get; set; }
    }
}
