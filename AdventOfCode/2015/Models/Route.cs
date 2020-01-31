using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015.Models
{
    public class Route
    {
        public List<string> Locations { get; set; } = new List<string>();
        public int TotalDistance { get; set; }
    }
}
