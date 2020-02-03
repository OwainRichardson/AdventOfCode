using System.Collections.Generic;

namespace AdventOfCode._2015.Models
{
    public class Pack
    {
        public List<int> Group1 { get; set; } = new List<int>();
        public List<int> Group2 { get; set; } = new List<int>();
        public List<int> Group3 { get; set; } = new List<int>();

        public long Group1QE
        {
            get
            {
                long total = 1;

                foreach (var item in Group1)
                {
                    total *= item;
                }

                return total;
            }
        }
    }
}
