using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_07_2
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
                    int distance = Math.Abs(input - position);

                    totalFuel += (distance * (distance + 1)) / 2;
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
