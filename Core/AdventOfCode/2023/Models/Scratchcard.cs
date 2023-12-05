namespace AdventOfCode._2023.Models
{
    public class Scratchcard
    {
        public int Id { get; set; }
        public List<int> WinningNumbers { get; set; }
        public List<int> YourNumbers { get; set; }
        public int NumberOfCards { get; set; } = 1;
    }
}
