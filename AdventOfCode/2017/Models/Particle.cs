using System;

namespace AdventOfCode._2017.Models
{
    public class Particle
    {
        public int Id { get; set; }

        public int XPos { get; set; }
        public int YPos { get; set; }
        public int ZPos { get; set; }

        public int XVel { get; set; }
        public int YVel { get; set; }
        public int ZVel { get; set; }

        public int XAcc { get; set; }
        public int YAcc { get; set; }
        public int ZAcc { get; set; }

        public long ManhattanDistance
        {
            get
            {
                return Math.Abs(XPos) + Math.Abs(YPos) + Math.Abs(ZPos);
            }
        }
    }
}
