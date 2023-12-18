using AdventOfCode._2023.Models.Enums;

namespace AdventOfCode._2023.Models
{
    public class PipePiece
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int? Distance { get; set; }
        public char Type { get; set; }
        public bool IsLoop { get; set; }
        public bool IsContainedByLoop { get; set; }
        public Directions? FirstDirectionToInLoop { get; set; }
        public Directions? SecondDirectionToInLoop { get; set; }
    }
}
