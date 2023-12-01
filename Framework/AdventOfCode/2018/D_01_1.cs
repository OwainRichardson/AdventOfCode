using System;
using System.IO;

namespace AdventOfCode._2018
{
    public static class D_01_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2018\Data\day01_full.txt");

            int total = 0;
            foreach (string input in inputs)
            {
                total += int.Parse(input);
            }

            Console.WriteLine(total);
        }
    }
}
