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
    public static class D_02_2
    {
        public static void Execute()
        {
            string[,] keypad = new string[5, 5] { { null, null, "5", null, null }, { null, "2", "6", "A", null  }, { "1", "3", "7", "B", "D" }, { null, "4", "8", "C", null }, { null, null, "9", null, null } };
            int x = 0;
            int y = 2;

            //PrintKeypad(keypad);

            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day02_full.txt");
            string answer = string.Empty;

            foreach (var input in inputs)
            {
                foreach (Char instruction in input)
                {
                    switch (instruction.ToString())
                    {
                        case Direction.Up:
                            if (y > 0 && keypad[x, y - 1] != null)
                            {
                                y -= 1;
                            }
                            break;
                        case Direction.Right:
                            if (x < 4 && keypad[x + 1, y] != null)
                            {
                                x += 1;
                            }
                            break;
                        case Direction.Down:
                            if (y < 4 && keypad[x, y + 1] != null)
                            {
                                y += 1;
                            }
                            break;
                        case Direction.Left:
                            if (x > 0 && keypad[x - 1, y] != null)
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

        private static void PrintKeypad(string[,] keypad)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Console.Write($"{keypad[x, y]}\t");
                }

                Console.WriteLine();
            }
        }
    }
}
