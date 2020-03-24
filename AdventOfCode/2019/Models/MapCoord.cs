using System;

namespace AdventOfCode._2019.Models
{
    public class MapCoord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsAsteroid { get; set; }
        public double Ratio
        {
            get
            {
                return Y == 0 ? 0 : (double)X / (double)Y;
            }
        }
        public double Degrees
        {
            get
            {
                var deg = ((Math.Atan2(Y, X) * 180) / Math.PI) + 90;

                if (deg < 0)
                {
                    deg += 360;
                }
                if (deg == 360)
                {
                    deg = 0;
                }

                return deg;
            }
        }
        public int ManhattanDistance
        {
            get
            {
                return Math.Abs(X) + Math.Abs(Y);
            }
        }
        public bool Destroyed { get; set; } = false;
        public int OriginalX { get; internal set; }
        public int OriginalY { get; internal set; }
    }
}
