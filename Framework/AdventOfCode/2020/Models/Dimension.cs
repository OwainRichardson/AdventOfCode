using System.Collections.Generic;

namespace AdventOfCode._2020.Models
{
    public class Dimension
    {
        public int Z { get; set; }
        public int W { get; set; }
        public List<Coordinate> Grid { get; set; }
    }
}
