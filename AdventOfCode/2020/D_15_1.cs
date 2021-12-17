using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_15_1
    {
        public static void Execute()
        {
            string inputs = File.ReadAllLines(@"2020\Data\day15.txt")[0];
            int[] firstNumbers = inputs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

            Dictionary<int, int> numbersSaid = new Dictionary<int, int>();

            int turn = 1;
            foreach (int number in firstNumbers)
            {
                numbersSaid.Add(turn, number);
                turn += 1;
            }

            while (turn <= 2020)
            {
                var lastNumberSpoken = numbersSaid.Last();

                var exists = numbersSaid.LastOrDefault(x => x.Value == lastNumberSpoken.Value && turn - 1 != x.Key);

                if (DoesNotExist(exists))
                {
                    numbersSaid.Add(turn, 0);
                }
                else
                {
                    numbersSaid.Add(turn, turn - 1 - exists.Key);
                }

                turn += 1;
            }

            Console.WriteLine(numbersSaid[2020]);
        }

        private static bool DoesNotExist(KeyValuePair<int, int> exists)
        {
            return exists.Key == 0 && exists.Value == 0;
        }
    }
}