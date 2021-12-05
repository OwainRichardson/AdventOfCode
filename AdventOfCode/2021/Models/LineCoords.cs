namespace AdventOfCode._2021.Models
{
    public class LineCoords
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public bool IsHorizontal()
        {
            return StartY == EndY;
        }

        public bool IsVertical()
        {
            return StartX == EndX;
        }
    }
}
