using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_09_1
    {
        // X-Y
        private static (int x, int y) head = (x: 0, y: 0);
        private static (int x, int y) tail = (x: 0, y: 0);

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day09.txt").ToArray();

            List<(int x, int y)> tailPositions = new List<(int x, int y)>();
            tailPositions.Add(tail);

            string pattern = @"^(\w{1})\W(\d+)$";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);
                int number = int.Parse(match.Groups[2].Value);

                switch (match.Groups[1].Value)
                {
                    case "U":
                        while (number > 0)
                        {
                            StepUp();
                            tailPositions.Add(tail);

                            number--;
                        }
                        break;
                    case "D":
                        while (number > 0)
                        {
                            StepDown();
                            tailPositions.Add(tail);

                            number--;
                        }
                        break;
                    case "L":
                        while (number > 0)
                        {
                            StepLeft();
                            tailPositions.Add(tail);

                            number--;
                        }
                        break;
                    case "R":
                        while (number > 0)
                        {
                            StepRight();
                            tailPositions.Add(tail);

                            number--;
                        }
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            Console.WriteLine(tailPositions.Distinct().Count());
        }

        private static void StepRight()
        {
            head = (x: head.x += 1, y: head.y);

            MoveTail();
        }
        private static void StepLeft()
        {
            head.x -= 1;

            MoveTail();
        }

        private static void StepDown()
        {
            head.y -= 1;

            MoveTail();
        }

        private static void StepUp()
        {
            head.y += 1;

            MoveTail();
        }

        private static void MoveTail()
        {
            if (head.x == tail.x && (Math.Abs(head.y - tail.y) >= 2))
            {
                if (head.y > tail.y)
                {
                    tail.y += 1;
                }
                else
                {
                    tail.y -= 1;
                }
            }
            else if (head.y == tail.y && (Math.Abs(head.x - tail.x) >= 2))
            {
                if (head.x > tail.x)
                {
                    tail.x += 1;
                }
                else
                {
                    tail.x -= 1;
                }
            }
            else if ((Math.Abs(head.x - tail.x) >= 2) || ((Math.Abs(head.y - tail.y) >= 2)))
            {
                if (head.x > tail.x && head.y > tail.y)
                {
                    tail.x += 1;
                    tail.y += 1;
                }
                else if (head.x > tail.x && head.y < tail.y)
                {
                    tail.x += 1;
                    tail.y -= 1;
                }
                else if (head.x < tail.x && head.y > tail.y)
                {
                    tail.x -= 1;
                    tail.y += 1;
                }
                else if (head.x < tail.x && head.y < tail.y)
                {
                    tail.x -= 1;
                    tail.y -= 1;
                }
            }
        }
    }
}
