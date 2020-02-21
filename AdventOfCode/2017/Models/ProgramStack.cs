using System.Collections.Generic;

namespace AdventOfCode._2017.Models
{
    public class ProgramStack
    {
        public string ProgramName { get; set; }
        public int Weight { get; set; }
        public int TotalWeight { get; set; }
        public List<string> DependentPrograms { get; set; } = new List<string>();
        public List<int> DependentWeights { get; set; } = new List<int>();
    }
}
