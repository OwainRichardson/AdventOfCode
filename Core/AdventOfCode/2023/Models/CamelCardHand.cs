using AdventOfCode._2023.Models.Enums;

namespace AdventOfCode._2023.Models
{
    public class CamelCardHand
    {
        public string Hand { get; set; }
        public int Bet { get; set; }
        public CamelCardHandType HandType { get; set; }
        public string Order { get; set; }
    }
}
