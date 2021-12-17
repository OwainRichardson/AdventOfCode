using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_06_1
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"2021\Data\day06.txt").Single();

            int[] inputs = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();
            Dictionary<int, long> fish = new Dictionary<int, long> { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 } };
            foreach (int i in inputs)
            {
                fish[i] += 1;
            }

            int totalDays = 80;

            for (int day = 1; day <= totalDays; day++)
            {
                IncrementDay(fish);
            }

            Console.WriteLine(fish.Values.Sum());
        }

        private static void IncrementDay(Dictionary<int, long> fish)
        {
            long newFishToSpawn = 0;
            for (int day = 0; day <= 8; day++)
            {
                if (day == 0)
                {
                    newFishToSpawn = fish[day];
                    continue;
                }

                fish[day - 1] = fish[day];
            }

            fish[6] += newFishToSpawn;
            fish[8] = newFishToSpawn;
        }
    }
}
