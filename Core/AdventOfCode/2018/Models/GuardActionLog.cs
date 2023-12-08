using AdventOfCode._2018.Models.Enums;

namespace AdventOfCode._2018.Models
{
    public class GuardActionLog
    {
        public int? GuardId { get; set; }
        public string Date { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public GuardSleepAction Action { get; set; }
    }
}
