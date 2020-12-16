using System.Collections.Generic;

namespace AdventOfCode._2020.Models
{
    public class TicketField
    {
        public string Name { get; set; }
        public List<int> AcceptableNumbers { get; set; } = new List<int>();
    }
}
