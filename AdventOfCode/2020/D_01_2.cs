using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_01_2
    {
        public static void Execute()
        {
            int[] inputs = File.ReadAllLines(@"2020\Data\day01_full.txt").Select(x => int.Parse(x)).ToArray();
            int total = 2020;
            bool found = false;

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs.Length; j++)
                {
                    for (int k = 0; k < inputs.Length; k++)
                    {
                        if (i == j || j == k || i == k) continue;

                        if (inputs[i] + inputs[j] + inputs[k] == total)
                        {
                            found = true;

                            int answer = inputs[i] * inputs[j] * inputs[k];
                            Console.WriteLine(answer);

                            break;
                        }
                    }

                    if (found) break;
                }

                if (found) break;
            }
        }
    }
}
