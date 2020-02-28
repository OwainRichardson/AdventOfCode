using System.Collections.Generic;

namespace AdventOfCode._2017.Models
{
    public class Pipe
    {
        public int Id { get; set; }
        public List<int> DirectPipes { get; set; } = new List<int>();
    }
}
