using System.Collections.Generic;

namespace AdventOfCode._2021.Models
{
    public class Board
    {
        public List<BoardCoord> BoardCoords { get; set; } = new List<BoardCoord>();

        public bool Completed { get; set; }
    }
}
