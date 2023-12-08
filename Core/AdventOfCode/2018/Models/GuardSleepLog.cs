namespace AdventOfCode._2018.Models
{
    public class GuardSleepLog
    {
        public int GuardId { get; set; }
        public int[] MinutesAsleep { get; set; } = new int[60];
    }
}
