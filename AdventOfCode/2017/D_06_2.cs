using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_06_2
    {
        public static void Execute()
        {
            string inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day06_full.txt")[0];

            int[] banks = inputs.Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

            PlayGame(banks);
        }

        private static void PlayGame(int[] banks)
        {
            List<string> previousConfigurations = new List<string>();
            previousConfigurations.Add(string.Join("", banks));
            int cycles = 0;

            while (previousConfigurations.GroupBy(x => x).All(x => x.Count() <= 1))
            {
                banks = Redistribute(banks);

                previousConfigurations.Add(string.Join("", banks));

                cycles++;
            }

            int firstCycle = cycles;

            string firstConfig = previousConfigurations.GroupBy(x => x).First(x => x.Count() > 1).Select(x => x).Distinct().Single();

            while (previousConfigurations.Count(x => x == firstConfig) <= 2)
            {
                banks = Redistribute(banks);

                previousConfigurations.Add(string.Join("", banks));

                cycles++;
            }

            Console.WriteLine(cycles - firstCycle);
        }

        private static int[] Redistribute(int[] inputs)
        {
            int max = inputs.Max();
            int index = Array.IndexOf(inputs, max);

            inputs[index] = 0;
            index = (index + 1) % inputs.Length;
            while (max > 0)
            {
                inputs[index]++;
                max--;
                index = (index + 1) % inputs.Length;
            }

            return inputs;
        }
    }
}
