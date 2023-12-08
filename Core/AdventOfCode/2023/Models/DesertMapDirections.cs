namespace AdventOfCode._2023.Models
{
    public class DesertMapDirections
    {
        public string Location { get; set; }
        public string Right { get; set; }
        public string Left { get; set; }
        public long StartOfPattern { get; set; }
        public long Pattern { get; set; }
        public long CurrentSteps { get; set; }
    }
}
