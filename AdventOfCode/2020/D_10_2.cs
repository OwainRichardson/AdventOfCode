using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_10_2
    {
        public static void Execute()
        {
            int[] inputs = File.ReadAllLines(@"2020\Data\day10.txt").Select(x => int.Parse(x)).OrderByDescending(x => x).ToArray();

            Dictionary<int, long> ways = new Dictionary<int, long>();
            foreach (int input in inputs)
            {
                ways.Add(input, inputs.Count(x => x <= input + 3 && x > input));
            }

            Dictionary<int, long> totalWays = new Dictionary<int, long>();
            for (int index = 0; index < inputs.Length; index++)
            {
                if (index == 0)
                {
                    totalWays.Add(inputs[index], 1);
                }
                else
                {
                    long noOfWays = 0;
                    if (totalWays.ContainsKey(inputs[index] + 1)) noOfWays += totalWays[inputs[index] + 1];
                    else if (ways.ContainsKey(inputs[index] + 1)) noOfWays += ways[inputs[index] + 1];
                    if (totalWays.ContainsKey(inputs[index] + 2)) noOfWays += totalWays[inputs[index] + 2];
                    else if (ways.ContainsKey(inputs[index] + 2)) noOfWays += ways[inputs[index] + 2];
                    if (totalWays.ContainsKey(inputs[index] + 3)) noOfWays += totalWays[inputs[index] + 3];
                    else if (ways.ContainsKey(inputs[index] + 3)) noOfWays += ways[inputs[index] + 3];

                    totalWays.Add(inputs[index], noOfWays);
                }
            }

            long total = 0;

            total += totalWays.ContainsKey(1) ? totalWays[1] : 0;
            total += totalWays.ContainsKey(2) ? totalWays[2] : 0;
            total += totalWays.ContainsKey(3) ? totalWays[3] : 0;

            Console.WriteLine(total);
        }
    }
}
