namespace AdventOfCode._2023.Models
{
    public class Part
    {
        public int X { get; set; }
        public int M { get; set; }
        public int A { get; set; }
        public int S { get; set; }
        public string Status { get; set; }
        public long Rating
        {
            get
            {
                return X + M + A + S;
            }
        }
    }
}