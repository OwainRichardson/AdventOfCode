using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    public static class D_02_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day02_full.txt");

            ParseInput(inputs);
        }

        private static void ParseInput(string[] inputs)
        {
            int total = 0;

            foreach (var row in inputs)
            {
                int[] numbers = row.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                int max = numbers.Max();
                int min = numbers.Min();

                total += (max - min);
            }

            Console.WriteLine(total);
        }
    }
}
