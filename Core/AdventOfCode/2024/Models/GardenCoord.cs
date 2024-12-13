namespace AdventOfCode._2024.Models
{
    public class GardenCoord : Coord
    {
        public string Value { get; set; }
        public int Group { get; set; }
        public int NumberOfFences { get; set; }
        public List<Side> Sides { get; set; }
    }
}
