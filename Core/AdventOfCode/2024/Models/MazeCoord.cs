using AdventOfCode._2024.Models.Enums;

namespace AdventOfCode._2024.Models
{
    public class MazeCoord : Coord
    {
        public string Id { get; set; }
        public bool IsWall { get; set; }
        public char Value { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public Direction RelativeLocation { get; set; }
        public int Distance { get; set; }
    }
}
