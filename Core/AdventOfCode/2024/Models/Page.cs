namespace AdventOfCode._2024.Models
{
    public class Page
    {
        public int PageNumber { get; set; }
        public List<int> MustBeBeforePages { get; set; } = new List<int>();
    }
}
