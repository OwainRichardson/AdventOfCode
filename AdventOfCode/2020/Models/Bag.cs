using System.Collections.Generic;

namespace AdventOfCode._2020.Models
{
    public class Bag
    {
        public string Colour { get; set; }
        public int Number { get; set; } = 1;
        public List<Bag> Contains { get; set; } = new List<Bag>();
    }
}
