namespace AdventOfCode._2023.Models
{
    public class Rock
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Type { get; set; }

        internal bool CanMoveEast(List<Rock> rocks)
        {
            if (X == rocks.Where(r => r.Type == "#").Max(r => r.X))
            {
                return false;
            }

            if (rocks.Any(r => r.Id != Id && r.X == X + 1 && r.Y == Y))
            {
                return false;
            }

            return true;
        }

        internal bool CanMoveNorth(List<Rock> rocks)
        {
            if (Y == 0)
            {
                return false;
            }

            if (rocks.Any(r => r.Id != Id && r.X == X && r.Y == Y - 1))
            {
                return false;
            }

            return true;
        }

        internal bool CanMoveSouth(List<Rock> rocks)
        { 
            if (Y == rocks.Where(r => r.Type == "#").Max(r => r.Y))
            {
                return false;
            }

            if (rocks.Any(r => r.Id != Id && r.X == X && r.Y == Y + 1))
            {
                return false;
            }

            return true;
        }

        internal bool CanMoveWest(List<Rock> rocks)
        {
            if (X == 0)
            {
                return false;
            }

            if (rocks.Any(r => r.Id != Id && r.X == X - 1 && r.Y == Y))
            {
                return false;
            }

            return true;
        }
    }
}
