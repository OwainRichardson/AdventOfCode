using System.Collections.Generic;

namespace AdventOfCode._2019.Models
{
    public class Orbit
    {
        public string StartPoint { get; set; }
        public string DirectMap { get; set; }
        public List<string> IndirectMaps { get; set; } = new List<string>();
    }
}
