namespace AdventOfCode._2023.Models
{
    public class DesertMap
    {
        public string Directions { get; set; }
        public List<DesertMapDirections> Maps { get; set; } = new List<DesertMapDirections>();
    }
}
