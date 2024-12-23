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

            MazeCoord start = maze.Single(m => m.IsStart);
            start.RelativeLocation = Direction.East;

            FindPaths(maze, start, new List<string>(), direction);

            return LowestScore.ToString();
        }

        private static void FindPaths(List<MazeCoord> maze, MazeCoord current, List<string> previousPath, Direction direction)
        {
            if (previousPath.Contains($"{current.X}:{current.Y}"))
            {
                return;
            }

            direction = current.RelativeLocation;

            previousPath.Add($"{current.X}:{current.Y}");

            if (CalculateScore(previousPath) > LowestScore) return;

            List<MazeCoord> possibleMoves = CalculatePossibleMoves(maze, current)
                                                .Where(c => !c.IsWall
                                                          && !previousPath.Contains($"{c.X}:{c.Y}"))
                                                .ToList();

            if (possibleMoves.Any(c => c.IsEnd))
            {
                int score = CalculateScore(previousPath);
                if (score < LowestScore)
                {
                    LowestScore = score + 1;
                }

                //MapPath(maze, previousPath, score);

                return;
            }

            if (possibleMoves.Count == 0)
            {
                Task.Delay(10);

                return;
            }

            foreach (MazeCoord possibleMove in possibleMoves)
            {
                FindPaths(maze, possibleMove, new List<string>(previousPath), direction);
            }
        }

        private static int CalculateScore(List<string> previousPath)
        {
            int score = 0;
            Direction direction = Direction.East;
            for (int index = 0; index < previousPath.Count - 1; index++)
            {
                string currentCoord = previousPath[index];
                string nextCoord = previousPath[index + 1];

                int[] currentSplit = currentCoord.Split(':').Select(s => int.Parse(s)).ToArray();
                int[] nextSplit = nextCoord.Split(':').Select(s => int.Parse(s)).ToArray();

                if (currentSplit[1] < nextSplit[1])
                {
                    Direction directionOfTravel = Direction.North;
                    if (direction != directionOfTravel)
                    {
                        direction = directionOfTravel;
                        score += 1000;
                    }
                }
                else if (currentSplit[1] > nextSplit[1])
                {
                    Direction directionOfTravel = Direction.South;
                    if (direction != directionOfTravel)
                    {
                        direction = directionOfTravel;
                        score += 1000;
                    }
                }
                else if (currentSplit[0] > nextSplit[0])
                {
                    Direction directionOfTravel = Direction.West;
                    if (direction != directionOfTravel)
                    {
                        direction = directionOfTravel;
                        score += 1000;
                    }
                }
                else if (currentSplit[0] < nextSplit[0])
                {
                    Direction directionOfTravel = Direction.East;
                    if (direction != directionOfTravel)
                    {
                        direction = directionOfTravel;
                        score += 1000;
                    }
                }

                score += 1;
            }

            return score;
        }

        private static void MapPath(List<MazeCoord> maze, List<string> previousPath, int score = 0)
        {
            Console.WriteLine(score);

            for (int y = 0; y <= maze.Max(m => m.Y); y++)
            {
                for (int x = 0; x <= maze.Max(m => m.X); x++)
                {
                    MazeCoord current = maze.First(c => c.Y == y && c.X == x);

                    if (current.IsStart) Console.Write("S");
                    else if (current.IsEnd) Console.Write("E");
                    else if (current.IsWall) Console.Write("#");
                    else if (previousPath.Contains($"{current.X}:{current.Y}")) Console.Write("^");
                    else Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        private static List<MazeCoord> CalculatePossibleMoves(List<MazeCoord> maze, MazeCoord current)
        {
            MazeCoord north = maze.First(m => m.X == current.X && m.Y == current.Y - 1);
            north.RelativeLocation = Direction.North;
            MazeCoord south = maze.First(m => m.X == current.X && m.Y == current.Y + 1);
            south.RelativeLocation = Direction.South;
            MazeCoord west = maze.First(m => m.X == current.X - 1 && m.Y == current.Y);
            west.RelativeLocation = Direction.West;
            MazeCoord east = maze.First(m => m.X == current.X + 1 && m.Y == current.Y);
            east.RelativeLocation = Direction.East;

            return new List<MazeCoord> { north, east, south, west };
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