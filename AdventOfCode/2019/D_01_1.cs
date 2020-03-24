using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class D_01_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day01_full.txt");
            List<int> numbers = inputs.Select(x => int.Parse(x)).ToList();

            int total = 0;

            foreach (var number in numbers)
            {
                //Console.WriteLine($"mass: {number} gives fuel: {CalculateFuelRequirement(number)}");
                total += CalculateFuelRequirement(number);
            }

            Console.WriteLine($"Total fuel required: {total}");
        }

        public static int CalculateFuelRequirement(double mass)
        {
            double fuelRequirement = mass / 3;

            fuelRequirement = Math.Floor(fuelRequirement);

            fuelRequirement = fuelRequirement - 2;

            return (int)fuelRequirement;
        }
    }
}
