using System;

namespace AdventOfCode._2017.Models
{
    public class MemoryLocation
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public int ManhattanDistance
        {
            get
            {
                return Math.Abs(X) + Math.Abs(Y);
            }
        }
    }
}
