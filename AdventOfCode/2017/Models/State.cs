using System.Collections.Generic;

namespace AdventOfCode._2017.Models
{
    public class State
    {
        public string Id { get; set; }

        public List<Instruction> Instructions { get; set; } = new List<Instruction>();
    }
}
