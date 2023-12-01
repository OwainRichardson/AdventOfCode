using AdventOfCode._2018.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2018
{
    public static class D_02_1
    {
        public static int NumberWithDuplicates = 0;
        public static int NumberWithTriples = 0;

        public static void Execute()
        {
            var codes = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2018\Data\day02_full.txt");

            foreach (var code in codes)
            {
                CalculateDuplicatesAndTriples(code);
            }

            Console.WriteLine(NumberWithDuplicates * NumberWithTriples);
        }

        private static void CalculateDuplicatesAndTriples(string code)
        {
            List<CharFrequency> charFrequencies = new List<CharFrequency>();

            foreach (char c in code)
            {
                if (charFrequencies.Any(x => x.Value == c.ToString()))
                {
                    var freq = charFrequencies.First(x => x.Value == c.ToString());
                    freq.Occurences += 1;
                }
                else
                {
                    charFrequencies.Add(new CharFrequency { Value = c.ToString(), Occurences = 1 });
                }
            }

            if (charFrequencies.Any(x => x.Occurences == 2))
            {
                NumberWithDuplicates += 1;
            }
            if (charFrequencies.Any(x => x.Occurences == 3))
            {
                NumberWithTriples += 1;
            }
        }
    }
}
