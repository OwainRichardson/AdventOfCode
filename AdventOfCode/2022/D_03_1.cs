using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_03_1
    {
        public static void Execute()
        { 
            string[] rucksacks = File.ReadAllLines(@"2022\Data\day03.txt").ToArray();
            int totalPriorities = 0;

            foreach (string rucksack in rucksacks)
            {
                int halfLength = rucksack.Length / 2;
                char[] firstCompartment = rucksack.Take(halfLength).ToArray();
                char[] secondCompartment = rucksack.Skip(halfLength).ToArray();

                char duplicatedCharacter = firstCompartment.Intersect(secondCompartment).Single();

                if (char.IsLower(duplicatedCharacter))
                {
                    totalPriorities += duplicatedCharacter % 32;
                }
                else
                {
                    totalPriorities += (duplicatedCharacter % 32) + 26;
                }
            }

            Console.WriteLine(totalPriorities);
        }
    }
}
