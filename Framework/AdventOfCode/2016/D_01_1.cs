using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    public static class D_01_1
    {
        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day01_full.txt")[0];
            var instructions = input.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            int x = 0;
            int y = 0;
            int currentDirection = Direction.North;
            foreach (var instruction in instructions)
            {
                int value = int.Parse(Regex.Match(instruction, @"\d+").Value);

                if (instruction.StartsWith("R"))
                {
                    currentDirection = (currentDirection + 1) % 4;

                    CalculateCoords(ref x, ref y, currentDirection, value);
                }
                else if (instruction.StartsWith("L"))
                {
                    currentDirection = (currentDirection - 1 + 4) % 4;

                    CalculateCoords(ref x, ref y, currentDirection, value);
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            Console.Write($"Total distance away is: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(Math.Abs(x) + Math.Abs(y));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void CalculateCoords(ref int x, ref int y, int currentDirection, int value)
        {
            switch (currentDirection)
            {
                case Direction.North:
                    y += value;
                    break;
                case Direction.East:
                    x += value;
                    break;
                case Direction.South:
                    y -= value;
                    break;
                case Direction.West:
                    x -= value;
                    break;
                default:
                    break;
            }
        }
    }
}
