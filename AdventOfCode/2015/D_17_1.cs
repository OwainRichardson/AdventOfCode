using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2015
{
    public class D_17_1
    {
        public static int _toStore = 150;

        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day17_full.txt");

            List<int> containers = ParseContainers(inputs);

            int[] array = new int[_toStore + 1];
            Dictionary<string, int> mem = new Dictionary<string, int>();

            var result = CountSets(containers.OrderByDescending(x => x).ToArray(), 150, containers.Count - 1, mem);

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(result);
            Console.ResetColor();
        }

        private static int CountSets(int[] array, int total, int i, Dictionary<string, int> mem)
        {
            int toReturn = 0;
            string key = $"{total}:{i}";
            if (mem.ContainsKey(key))
            {
                return mem[key];
            }

            if (total == 0)
            {
                return 1;
            }
            else if (total < 0 || i < 0)
            {
                return 0;
            }
            else if (total < array[i])
            {
                return CountSets(array, total, i - 1, mem);
            }
            else
            {
                toReturn = CountSets(array, total - array[i], i - 1, mem) + CountSets(array, total, i - 1, mem);
            }

            mem[key] = toReturn;

            return toReturn;
        }

        private static List<int> ParseContainers(string[] inputs)
        {
            List<int> containers = new List<int>();
            foreach (var input in inputs)
            {
                containers.Add(int.Parse(input));
            }

            return containers;
        }
    }
}