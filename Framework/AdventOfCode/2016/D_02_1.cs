using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.IO;

namespace AdventOfCode._2016
{
    public static class D_02_1
    {
        public static void Execute()
        {
            int[,] keypad = new int[3, 3] { { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 } };
            int x = 1;
            int y = 1;

            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day02_full.txt");
            string answer = string.Empty;

            foreach (var input in inputs)
            {
                foreach (Char instruction in input)
                {
                    switch (instruction.ToString())
                    {
                        case Direction.Up:
                            if (y > 0)
                            {
                                y -= 1;
                            }
                            break;
                        case Direction.Right:
                            if (x < 2)
                            {
                                x += 1;
                            }
                            break;
                        case Direction.Down:
                            if (y < 2)
                            {
                                y += 1;
                            }
                            break;
                        case Direction.Left:
                            if (x > 0)
                            {
                                x -= 1;
                            }
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }

                answer = $"{answer}{keypad[x, y]}";
            }

            Console.Write("Code is: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(answer);
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void PrintKeypad(int[,] keypad)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.Write($"{keypad[x, y]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
