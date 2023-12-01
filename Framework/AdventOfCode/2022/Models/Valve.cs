using System.Collections.Generic;

namespace AdventOfCode._2022.Models
{
    public class Valve
    {
        public string Name { get; set; }
        public int FlowRate { get; set; }
        public List<string> Tunnels { get; set; }
    }
}
