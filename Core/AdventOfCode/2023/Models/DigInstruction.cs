using AdventOfCode._2023.Models.Enums;

namespace AdventOfCode._2023.Models
{
    public class DigInstruction
    {
        public Directions Direction { get; set; }
        public int Number { get; set; }
        public string HexCode { get; set; }
    }
}
