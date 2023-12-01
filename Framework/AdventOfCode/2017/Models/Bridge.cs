using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2017.Models
{
    public class Bridge
    {
        public Bridge DeepCopy()
        {
            Bridge other = (Bridge)this.MemberwiseClone();
            int[] tempPorts = new int[this.Ports.Count];
                
            this.Ports.CopyTo(tempPorts);
            other.Ports = tempPorts.ToList();

            return other;
        }

        public List<int> Ports = new List<int>();
    }
}
