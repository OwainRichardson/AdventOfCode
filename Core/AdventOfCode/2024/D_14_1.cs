using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2024
{
    public static class D_14_1
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day14.txt");

            List<Robot> robots = ParseInputs(inputs);

            int numberOfSeconds = 100;
            int currentSecond = 1;
            int maxWidth = robots.Max(r => r.Position.X + 1);
            int maxHeight = robots.Max(r => r.Position.Y + 1);

            int midX = (int)Math.Floor((double)maxWidth / 2);
            int midY = (int)Math.Floor((double)maxHeight / 2);

            while (currentSecond <= numberOfSeconds)
            {
                MoveRobots(robots, maxWidth, maxHeight);

                currentSecond++;
            }

            //DrawRobots(robots, maxWidth, maxHeight, midX, midY);

            long total = 0;

            total += robots.Count(r => r.Position.X < midX && r.Position.Y < midY);
            total *= robots.Count(r => r.Position.X < midX && r.Position.Y > midY);
            total *= robots.Count(r => r.Position.X > midX && r.Position.Y < midY);
            total *= robots.Count(r => r.Position.X > midX && r.Position.Y > midY);

            return total.ToString();
        }

        private static void DrawRobots(List<Robot> robots, int maxWidth, int maxHeight, int midX, int midY)
        {
            Console.WriteLine();

            for (int y = 0; y < maxHeight; y++)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    if (x == midX || y == midY)
                    {
                        Console.Write(" ");
                    }
                    else if (!robots.Any(r => r.Position.X == x && r.Position.Y == y))
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(robots.Count(r => r.Position.X == x && r.Position.Y == y));
                    }
                }

                Console.WriteLine();
            }
        }

        private static void MoveRobots(List<Robot> robots, int maxWidth, int maxHeight)
        {
            foreach (Robot robot in robots)
            {
                robot.Position = new Coord { X = robot.Position.X + robot.Velocity.X, Y = robot.Position.Y + robot.Velocity.Y };

                if (robot.Position.Y < 0)
                {
                    robot.Position.Y += maxHeight;
                }
                else if (robot.Position.Y >= maxHeight)
                {
                    robot.Position.Y -= maxHeight;
                }

                if (robot.Position.X >= maxWidth)
                {
                    robot.Position.X -= maxWidth;
                }
                else if (robot.Position.X < 0)
                {
                    robot.Position.X += maxWidth;
                }
            }
        }

        private static List<Robot> ParseInputs(string[] inputs)
        {
            string pattern = @"^p=([-]?\d+),([-]?\d+)\Wv=([-]?\d+),([-]?\d+)$";
            Regex regex = new Regex(pattern);

            List<Robot> robots = new List<Robot>();

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);

                Robot robot = new Robot
                {
                    Position = new Coord
                    {
                        X = int.Parse(match.Groups[1].Value),
                        Y = int.Parse(match.Groups[2].Value)
                    },
                    Velocity = new Coord
                    {
                        X = int.Parse(match.Groups[3].Value),
                        Y = int.Parse(match.Groups[4].Value)
                    }
                };

                robots.Add(robot);
            }

            return robots;
        }
    }
}