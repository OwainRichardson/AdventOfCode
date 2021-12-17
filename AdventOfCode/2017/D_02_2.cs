using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    public static class D_02_2
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

                for (int i = 0; i < numbers.Length; i++)
                {
                    for (int j = 0; j < numbers.Length; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (numbers[i] % numbers[j] == 0)
                        {
                            total += (numbers[i] / numbers[j]);
                        }
                    }
                }
            }

            Console.WriteLine(total);
        }
    }
}
