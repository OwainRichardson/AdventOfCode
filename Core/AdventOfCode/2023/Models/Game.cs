namespace AdventOfCode._2023.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public List<GameSet> Sets { get; set; } = new List<GameSet>();
        public int GamePower { get; set; }
    }
}
