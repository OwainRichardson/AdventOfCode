using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_01_1
    {
        public static void Execute()
        {
            int[] inputs = File.ReadAllLines(@"2020\Data\day01.txt").Select(x => int.Parse(x)).ToArray();
            int total = 2020;
            bool found = false;

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    if (i == j) continue;

                    if (inputs[i] + inputs[j] == total)
                    {
                        found = true;

                        int answer = inputs[i] * inputs[j];
                        Console.WriteLine(answer);

                        break;
                    }
                }

                if (found) break;
            }
        }
    }
}
