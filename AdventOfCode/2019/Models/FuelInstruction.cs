using System.Collections.Generic;

namespace AdventOfCode._2019.Models
{
    public class FuelInstruction
    {
        public Fuel Creates { get; set; }
        public List<Fuel> Requires { get; set; } = new List<Fuel>();
    }
}
