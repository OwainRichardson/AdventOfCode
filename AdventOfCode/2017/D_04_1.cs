using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    public static class D_04_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day04_full.txt");
            int total = 0;

            foreach (var input in inputs)
            {
                if (CheckIfPasswordIsValid(input))
                {
                    total++;
                }
            }

            Console.WriteLine(total);
        }

        private static bool CheckIfPasswordIsValid(string input)
        {
            var split = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (split.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                return false;
            }

            return true;
        }
    }
}
