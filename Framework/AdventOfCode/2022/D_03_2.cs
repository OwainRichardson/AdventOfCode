using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_03_2
    {
        public static void Execute()
        { 
            string[] rucksacks = File.ReadAllLines(@"2022\Data\day03.txt").ToArray();
            int totalPriorities = 0;

            for (int index = 2; index < rucksacks.Length; index += 3)
            {
                char duplicatedCharacter = rucksacks[index - 2].Intersect(rucksacks[index - 1]).Intersect(rucksacks[index]).Single();

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
