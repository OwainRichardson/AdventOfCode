using System.Collections.Generic;

namespace AdventOfCode._2021.Models
{
    public class Scanner
    {
        public int Id { get; set; }
        public List<BeaconCoords> Beacons { get; set; } = new List<BeaconCoords>();
        public string Location { get; set; }
    }
}
