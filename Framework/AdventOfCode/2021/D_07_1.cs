using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_07_1
    {
        public static void Execute()
        {
            int[] inputs = File.ReadAllLines(@"2021\Data\day07.txt").Single().Split(',').Select(i => int.Parse(i)).ToArray();

            int lowestFuel = 0;
            for (int position = inputs.Min(); position <= inputs.Max(); position++)
            {
                int totalFuel = 0;

                foreach(int input in inputs)
                {
                    totalFuel += Math.Abs(input - position);
                }

                if (lowestFuel == 0 || totalFuel < lowestFuel)
                {
                    lowestFuel = totalFuel;
                }
            }

            Console.WriteLine(lowestFuel);
        }
    }
}
