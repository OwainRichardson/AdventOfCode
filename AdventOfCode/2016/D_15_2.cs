using AdventOfCode._2016.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public class D_15_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day15_full.txt");

            Run(inputs);
        }

        private static void Run(string[] inputs)
        {
            List<Disc> discs = ParseInputs(inputs);
            discs.Add(new Disc { Id = discs.Max(x => x.Id) + 1, NumberOfPositions = 11, Position = 0 });
            bool success = false;
            int delay = 0;

            while (!success)
            {
                if (discs.All(x => (x.Position + delay + x.Id) % x.NumberOfPositions == 0))
                {
                    Console.WriteLine(delay);
                    success = true;
                }

                delay++;
            }
        }

        private static List<Disc> ParseInputs(string[] inputs)
        {
            string pattern = @"Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+).";
            Regex regex = new Regex(pattern);
            List<Disc> discs = new List<Disc>();

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);

                Disc disc = new Disc();
                disc.Id = int.Parse(match.Groups[1].Value);
                disc.NumberOfPositions = int.Parse(match.Groups[2].Value);
                disc.Position = int.Parse(match.Groups[3].Value);

                discs.Add(disc);
            }

            return discs;
        }
    }
}
