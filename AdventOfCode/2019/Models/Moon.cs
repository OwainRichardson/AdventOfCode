using System.Collections.Generic;

namespace AdventOfCode._2019.Models
{
    public class Moon
    {
        public int Id { get; set; }
        public List<long> XCyclePattern { get; set; } = new List<long>();
        public List<long> YCyclePattern { get; set; } = new List<long>();
        public List<long> ZCyclePattern { get; set; } = new List<long>();
        public long XCycleLength { get; set; }
        public long YCycleLength { get; set; }
        public long ZCycleLength { get; set; }
        public long TotalCycleLength { get; set; }
        public Vector Position { get; set; }
        public Vector Velocity { get; set; } = new Vector();
    }
}
