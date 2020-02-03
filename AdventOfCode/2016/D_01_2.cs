using AdventOfCode._2015.Models;
using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_01_2
    {
        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day01_full.txt")[0];
            var instructions = input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            List<Coord> coords = new List<Coord>();

            int currentDirection = Direction.North;
            foreach (var instruction in instructions)
            {
                int value = int.Parse(Regex.Match(instruction, @"\d+").Value);

                if (instruction.StartsWith("R"))
                {
                    currentDirection = (currentDirection + 1) % 4;

                    CalculateCoords(currentDirection, value, ref coords);
                }
                else if (instruction.StartsWith("L"))
                {
                    currentDirection = (currentDirection - 1 + 4) % 4;

                    CalculateCoords(currentDirection, value, ref coords);
                }
                else
                {
                    throw new ArgumentException();
                }

                bool anyDuplicates = CheckForDuplicates(coords);

                if (anyDuplicates)
                {
                    break;
                }
            }

            Coord duplicate = GetDuplicate(coords);

            Console.Write($"Total distance away is: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(Math.Abs(duplicate.X) + Math.Abs(duplicate.Y));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static Coord GetDuplicate(List<Coord> coords)
        {
            return coords.GroupBy(c => new
            {
                c.X,
                c.Y
            }).First(x => x.Count() > 1).First();
        }

        private static bool CheckForDuplicates(List<Coord> coords)
        {
            return coords.GroupBy(c => new
            {
                c.X,
                c.Y
            }).Any(x => x.Count() > 1);
        }

        private static void CalculateCoords(int currentDirection, int value, ref List<Coord> coords)
        {
            Coord lastCoord = coords.LastOrDefault();

            if (lastCoord == null)
            {
                lastCoord = new Coord
                {
                    X = 0,
                    Y = 0
                };
            }

            switch (currentDirection)
            {
                case Direction.North:

                    for (int i = 1; i <= value; i++)
                    {
                        coords.Add(new Coord { X = lastCoord.X, Y = lastCoord.Y + i });
                    }
                    break;
                case Direction.East:

                    for (int i = 1; i <= value; i++)
                    {
                        coords.Add(new Coord { X = lastCoord.X + i, Y = lastCoord.Y });
                    }

                    break;
                case Direction.South:

                    for (int i = 1; i <= value; i++)
                    {
                        coords.Add(new Coord { X = lastCoord.X, Y = lastCoord.Y - i });
                    }
                    break;
                case Direction.West:

                    for (int i = 1; i <= value; i++)
                    {
                        coords.Add(new Coord { X = lastCoord.X - i, Y = lastCoord.Y });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
