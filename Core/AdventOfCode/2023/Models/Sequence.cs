namespace AdventOfCode._2023.Models
{
    public class Sequence
    {
        public int Id { get; set; }
        public Sequence ChildSequence { get; set; }
        public List<int> SequenceValues { get; set; }
    }
}
