using AdventOfCode._2024.Models;
using AdventOfCode._2024.Models.Enums;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode._2024
{
    public static class D_16_1
    {
        public static int LowestScore = int.MaxValue;

        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day16.txt");

            List<MazeCoord> maze = ParseInputs(inputs);
            Direction direction = Direction.East;

            FindPaths(maze, maze.Single(m => m.IsStart), direction);

            return "Incomplete";
        }

        private static void FindPaths(List<MazeCoord> maze, MazeCoord current, Direction direction, int score = 0)
        {
            List<MazeCoord> possibleMoves = CalculatePossibleMoves(maze, current, direction)
                                                .Where(c => !c.IsWall)
                                                .ToList();

            if (possibleMoves.Any(c => c.IsEnd))
            {
                if (score < LowestScore)
                {
                    LowestScore = score;
                }

                return;
            }

            if (possibleMoves.Count == 0 )
            {
                Task.Delay(10);

                return;
            }

            foreach (MazeCoord possibleMove in possibleMoves)
            {
                FindPaths(maze, possibleMove, possibleMove.RelativeLocation, score + 1 + Turned(direction, possibleMove.RelativeLocation));
            }
        }

        private static int Turned(Direction direction, Direction relativeLocation)
        {
            if (direction != relativeLocation)
            {
                return 1000;
            }

            return 0;
        }

        private static List<MazeCoord> CalculatePossibleMoves(List<MazeCoord> maze, MazeCoord current, Direction direction)
        {
            MazeCoord north = maze.First(m => m.X == current.X && m.Y == current.Y - 1);
            north.RelativeLocation = Direction.North;
            MazeCoord south = maze.First(m => m.X == current.X && m.Y == current.Y + 1);
            south.RelativeLocation = Direction.South;
            MazeCoord west = maze.First(m => m.X == current.X - 1&& m.Y == current.Y);
            west.RelativeLocation = Direction.West;
            MazeCoord east = maze.First(m => m.X == current.X + 1 && m.Y == current.Y);
            east.RelativeLocation = Direction.East;

            switch (direction)
            {
                case Direction.North:
                    return new List<MazeCoord> { north, west, east };
                case Direction.East:
                    return new List<MazeCoord> { south, east, north };
                case Direction.South:
                    return new List<MazeCoord> { south, east, west };
                case Direction.West:
                    return new List<MazeCoord> { north, west, south };
                default: throw new InvalidOperationException();
            }
        }

        private static List<MazeCoord> ParseInputs(string[] inputs)
        {
            List<MazeCoord> maze = new List<MazeCoord>();

            int y = 0;
            foreach (string input in inputs)
            {
                int x = 0;

                foreach (char c in input)
                {
                    maze.Add(new MazeCoord
                    {
                        X = x,
                        Y = y,
                        Value = c,
                        IsStart = c == 'S',
                        IsEnd = c == 'E',
                        IsWall = c == '#'
                    });

                    x++;
                }

                y++;
            }

            return maze;
        }
    }
}