using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_04_2
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
            List<string> orderedInputs = new List<string>();

            foreach (string item in split)
            {
                orderedInputs.Add(new string(item.OrderBy(x => x).ToArray()));
            }

            if (orderedInputs.GroupBy(x => x).Any(x => x.Count() > 1))
            {
                return false;
            }

            return true;
        }
    }
}
