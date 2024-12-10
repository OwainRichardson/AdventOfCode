namespace AdventOfCode._2024.Models
{
    public class TrailheadCoord : Coord
    {
        public bool HasBeenReached { get; set; }
        public int Value { get; set; }
        public int NumberOfRoutesThatGetHere { get; set; }
    }
}
