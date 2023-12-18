using AdventOfCode._2023.Models.Enums;

namespace AdventOfCode._2023.Models
{
    public class Beam
    {
        public Directions Direction { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string VisitedTiles { get; set; }
        public int TurnsSinceNonEnergisedTile { get; set; }
        public string Id { get; set; }
    }
}
