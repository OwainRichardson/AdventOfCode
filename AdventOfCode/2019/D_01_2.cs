using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class D_01_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day01_full.txt");
            List<int> numbers = inputs.Select(x => int.Parse(x)).ToList();

            int total = 0;

            foreach (var number in numbers)
            {
                //Console.WriteLine($"mass: {number} gives fuel: {CalculateFuelRequirement(number)}");
                var fuelRequirement = CalculateFuelRequirement(number);

                total += CalculateFuelFuelRequirement(fuelRequirement);
            }

            Console.WriteLine($"Total fuel required: {total}");
        }

        private static int CalculateFuelFuelRequirement(double fuelRequirement)
        {
            var total = fuelRequirement;

            while (fuelRequirement > 0)
            {
                var requirement = CalculateFuelRequirement(fuelRequirement);

                if (requirement > 0)
                {
                    fuelRequirement = requirement;
                    total += requirement;
                }
                else
                {
                    break;
                }
            }

            return (int)total;
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
