using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_09_2
    {
        // X:Y
        private static string[] rope = new string[10] { "0:0", "0:0", "0:0", "0:0", "0:0", "0:0", "0:0", "0:0", "0:0", "0:0" };

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day09.txt").ToArray();

            List<string> tailPositions = new List<string>();
            tailPositions.Add(rope[9]);

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
                            tailPositions.Add(rope[9]);

                            number--;
                        }
                        break;
                    case "D":
                        while (number > 0)
                        {
                            StepDown();
                            tailPositions.Add(rope[9]);

                            number--;
                        }
                        break;
                    case "L":
                        while (number > 0)
                        {
                            StepLeft();
                            tailPositions.Add(rope[9]);

                            number--;
                        }
                        break;
                    case "R":
                        while (number > 0)
                        {
                            StepRight();
                            tailPositions.Add(rope[9]);

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
            int[] ropeSplit = SplitRope(0);
            ropeSplit[0] += 1;

            rope[0] = string.Join(":", ropeSplit);

            MoveTail();
        }

        private static int[] SplitRope(int index)
        {
            return rope[index].Split(':').Select(h => int.Parse(h)).ToArray();
        }

        private static void StepLeft()
        {
            int[] ropeSplit = SplitRope(0);
            ropeSplit[0] -= 1;

            rope[0] = string.Join(":", ropeSplit);

            MoveTail();
        }

        private static void StepDown()
        {
            int[] ropeSplit = SplitRope(0);
            ropeSplit[1] -= 1;

            rope[0] = string.Join(":", ropeSplit);

            MoveTail();
        }

        private static void StepUp()
        {
            int[] ropeSplit = SplitRope(0);
            ropeSplit[1] += 1;

            rope[0] = string.Join(":", ropeSplit);

            MoveTail();
        }

        private static void MoveTail()
        {
            for (int index = 1; index < rope.Length; index++)
            {
                int[] headSplit = SplitRope(index - 1);
                (int x, int y) head = (x: headSplit[0], y: headSplit[1]);

                int[] tailSplit = SplitRope(index);
                (int x, int y) tail = (x: tailSplit[0], y: tailSplit[1]);

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
                else if ((Math.Abs(head.x - tail.x) >= 2) || (Math.Abs(head.y - tail.y) >= 2))
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

                rope[index] = $"{tail.x}:{tail.y}";
            }
        }
    }
}
