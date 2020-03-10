namespace AdventOfCode._2017.Models
{
    public class Instruction
    {
        public int Value { get; set; }

        public int Write { get; set; }
        public int Move { get; set; }
        public string NewState { get; set; }
    }
}
