using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2024
{
    public static class D_14_2
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day14.txt");

            List<Robot> robots = ParseInputs(inputs);

            int currentSecond = 1;
            int maxWidth = robots.Max(r => r.Position.X + 1);
            int maxHeight = robots.Max(r => r.Position.Y + 1);

            while (true)
            {
                MoveRobots(robots, maxWidth, maxHeight);

                if (IsXmasTree(robots))
                {
                    DrawRobots(robots, maxWidth, maxHeight, currentSecond);
                    break;
                }

                currentSecond++;
            }

            return currentSecond.ToString();
        }

        private static bool IsXmasTree(List<Robot> robots)
        {
            foreach (Robot robot in robots)
            {
                if (robots.Exists(r => r.Position.X == robot.Position.X && r.Position.Y == robot.Position.Y + 1)
                                && robots.Exists(r => r.Position.X == robot.Position.X - 1 && r.Position.Y == robot.Position.Y + 1)
                                && robots.Exists(r => r.Position.X == robot.Position.X + 1 && r.Position.Y == robot.Position.Y + 1)
                                && robots.Exists(r => r.Position.X == robot.Position.X - 2 && r.Position.Y == robot.Position.Y + 2)
                                && robots.Exists(r => r.Position.X == robot.Position.X - 1 && r.Position.Y == robot.Position.Y + 2)
                                && robots.Exists(r => r.Position.X == robot.Position.X && r.Position.Y == robot.Position.Y + 2)
                                && robots.Exists(r => r.Position.X == robot.Position.X + 1 && r.Position.Y == robot.Position.Y + 2)
                                && robots.Exists(r => r.Position.X == robot.Position.X + 2 && r.Position.Y == robot.Position.Y + 2)
                                )
                {
                    return true;
                }
            }

            return false;
        }

        private static void DrawRobots(List<Robot> robots, int maxWidth, int maxHeight, int currentSecond)
        {
            Console.WriteLine(currentSecond);

            for (int y = 0; y < maxHeight; y++)
            {
                for (int x = 0; x < maxWidth; x++)
                {
                    if (!robots.Any(r => r.Position.X == x && r.Position.Y == y))
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write("X");
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