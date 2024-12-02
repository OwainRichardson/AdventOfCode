using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2024
{
    public static class D_01_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day01.txt").ToArray();

            int total = 0;

            (List<int> first, List<int> second) = ParseInputsToLists(inputs);

            first = first.OrderBy(f => f).ToList();
            second = second.OrderBy(s => s).ToList();

            for (int i = 0; i < first.Count; i++)
            {
                total += Math.Abs(first[i] - second[i]);
            }

            Console.WriteLine(total);
        }

        private static (List<int> first, List<int> second) ParseInputsToLists(string[] inputs)
        {
            List<int> first = new List<int>();
            List<int> second = new List<int>();

            foreach (string input in inputs)
            {
                int[] split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();
                first.Add(split[0]);
                second.Add(split[1]);
            }

            return (first, second);
        }
    }
}
