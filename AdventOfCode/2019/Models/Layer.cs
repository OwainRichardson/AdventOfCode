using System.Collections.Generic;

namespace AdventOfCode._2019.Models
{
    public class Layer
    {
        public List<int> Pixels { get; set; } = new List<int>();
        public int NumberOfZeros { get; set; }
        public int NumberOfOnes { get; set; }
        public int NumberOfTwos { get; set; }
        public int Id { get; set; }
    }
}
