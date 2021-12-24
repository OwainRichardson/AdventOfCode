using System.Collections.Generic;

namespace AdventOfCode._2021.Models
{
    public class BeaconCoords
    {
        public int Id { get; set; }
        public int Coord1 { get; set; }
        public int Coord2 { get; set; }
        public int Coord3 { get; set; }
        public List<long> AbsoluteDistanceToOtherBeacons { get; set; } = new List<long>();        
    }
}
