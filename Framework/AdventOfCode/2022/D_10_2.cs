using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_10_2
    {
        private static List<int> SignalStrengthCycles = new List<int> { 20, 60, 100, 140, 180, 220 };

        public static void Execute()
        {
            Console.WriteLine();

            string[] inputs = File.ReadAllLines(@"2022\Data\day10.txt").ToArray();
            int inputIndex = 0;
            int X = 1;
            int toAdd = 0;

            for (int cycle = 0; cycle < 240; cycle++)
            {
                if (cycle % 40 == 0 && cycle > 0)
                {
                    Console.WriteLine();
                }

                if (inputs[inputIndex] == "noop")
                {
                    WritePixel(X, cycle);

                    inputIndex += 1;
                }
                else
                {
                    if (toAdd != 0)
                    {
                        WritePixel(X, cycle);

                        X += toAdd;
                        toAdd = 0;

                        inputIndex += 1;
                    }
                    else
                    {
                        WritePixel(X, cycle);

                        string pattern = @"^addx\W(-?\d+)$";
                        Regex regex = new Regex(pattern);
                        Match match = regex.Match(inputs[inputIndex]);

                        toAdd += int.Parse(match.Groups[1].Value);
                    }
                }  
            }

            Console.WriteLine();
        }

        private static void WritePixel(int X, int cycle)
        {
            if (X >= (cycle % 40) - 1 && X <= (cycle % 40) + 1)
            {
                Console.Write("#");
            }
            else
            {
                Console.Write(" ");
            }
        }
    }
}
