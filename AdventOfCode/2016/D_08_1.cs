using AdventOfCode.Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    public static class D_08_1
    {
        private readonly static int _rowSize = 50;
        private readonly static int _columnSize = 6;

        public static void Execute()
        {
            string[,] screen = new string[_columnSize, _rowSize];

            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day08_full.txt");

            for (int y = 0; y < _columnSize; y++)
            {
                for (int x = 0; x < _rowSize; x++)
                {
                    screen[y, x] = ".";
                }
            }

            foreach (var instruction in inputs)
            {
                ActionInstruction(instruction, ref screen);
            }

            Console.Write("Number of pixels lit: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(CountOnPixels(screen));
            Console.ResetColor();
            Console.WriteLine();
        }

        private static int CountOnPixels(string[,] screen)
        {
            int total = 0;

            for (int y = 0; y < _columnSize; y++)
            {
                for (int x = 0; x < _rowSize; x++)
                {
                    if (screen[y, x] == "#")
                    {
                        total += 1;
                    }
                }
            }

            return total;
        }

        private static void PrintScreen(string[,] screen)
        {
            for (int y = 0; y < _columnSize; y++)
            {
                for (int x = 0; x < _rowSize; x++)
                {
                    Console.Write(screen[y, x]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void ActionInstruction(string instruction, ref string[,] screen)
        {
            if (instruction.Contains("rect"))
            {
                var match = Regex.Match(instruction, @"(\d+)x(\d+)");

                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);

                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        screen[i, j] = "#";
                    }
                }
            }
            else if (instruction.Contains("rotate column"))
            {
                var match = Regex.Match(instruction, @"x=(\d+) by (\d+)");

                int column = int.Parse(match.Groups[1].Value);
                int moveBy = int.Parse(match.Groups[2].Value);

                string[] tempColumn = new string[_columnSize];

                for (int i = 0; i < _columnSize; i++)
                {
                    tempColumn[(i + moveBy) % _columnSize] = screen[i, column];
                }

                for (int i = 0; i < _columnSize; i++)
                {
                     screen[i, column] = tempColumn[i];
                }
            }
            else if (instruction.Contains("rotate row"))
            {
                var match = Regex.Match(instruction, @"y=(\d+) by (\d+)");

                int row = int.Parse(match.Groups[1].Value);
                int moveBy = int.Parse(match.Groups[2].Value);

                string[] tempRow = new string[_rowSize];

                for (int i = 0; i < _rowSize; i++)
                {
                    tempRow[(i + moveBy) % _rowSize] = screen[row, i];
                }

                for (int i = 0; i < _rowSize; i++)
                {
                    screen[row, i] = tempRow[i];
                }
            }
        }
    }
}
