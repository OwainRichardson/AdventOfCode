using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_04_1
    {
        public static void Execute()
        { 
            string[] elfPairs = File.ReadAllLines(@"2022\Data\day04.txt").ToArray();
            int overlappingPairs = 0;

            foreach (string elfPair in elfPairs)
            {
                string[] elfSplit = elfPair.Split(',');
                string elf1 = elfSplit[0];
                string elf2 = elfSplit[1];

                int[] elf1Boundaries = elf1.Split('-').Select(b => int.Parse(b)).ToArray();
                int[] elf2Boundaries = elf2.Split('-').Select(b => int.Parse(b)).ToArray();

                if (elf1Boundaries[0] >= elf2Boundaries[0] && elf1Boundaries[1] <= elf2Boundaries[1])
                {
                    overlappingPairs += 1;
                    continue;
                }

                if (elf2Boundaries[0] >= elf1Boundaries[0] && elf2Boundaries[1] <= elf1Boundaries[1])
                {
                    overlappingPairs += 1;
                    continue;
                }
            }

            Console.WriteLine(overlappingPairs);
        }
    }
}
