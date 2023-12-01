using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_10_1
    {
        private static List<int> SignalStrengthCycles = new List<int> { 20, 60, 100, 140, 180, 220 };

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day10.txt").ToArray();

            int cycle = 1;
            int X = 1;
            long signalStrength = 0;

            foreach (string input in inputs)
            {
                signalStrength += CheckSignalStrength(cycle, X);

                if (input == "noop")
                {
                    cycle += 1;
                    continue;
                } 
                else
                {
                    cycle += 1;

                    signalStrength += CheckSignalStrength(cycle, X);

                    string pattern = @"^addx\W(-?\d+)$";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);

                    X += int.Parse(match.Groups[1].Value);

                    cycle += 1;
                }
            }

            Console.WriteLine(signalStrength);
        }

        private static long CheckSignalStrength(int cycle, int x)
        {
            if (SignalStrengthCycles.Contains(cycle))
            {
                return cycle * x;
            }

            return 0;
        }
    }
}
