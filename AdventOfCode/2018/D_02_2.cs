using AdventOfCode._2018.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    public static class D_02_2
    {
        public static int NumberWithDuplicates = 0;
        public static int NumberWithTriples = 0;

        public static void Execute()
        {
            var codes = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2018\Data\day02_full.txt");

            foreach (var code in codes)
            {
                CheckForInputsOffByOneValue(code, codes);
            }
        }

        private static void CheckForInputsOffByOneValue(string codeToCheck, string[] codes)
        {
            List<CharFrequency> charFrequencies = new List<CharFrequency>();

            foreach (var code in codes)
            {
                var codeLength = code.Length;
                var charsTheSame = 0;
                List<string> sameChars = new List<string>();

                for (int i = 0; i < code.Length; i++)
                {
                    if (code[i].ToString() == codeToCheck[i].ToString())
                    {
                        charsTheSame += 1;
                        sameChars.Add(code[i].ToString());
                    }
                }

                if (charsTheSame + 1 == codeLength)
                {
                    //Console.WriteLine($"codes {code} and {codeToCheck} are only 1 char different");
                    //Console.WriteLine();
                    foreach (var c in sameChars)
                    {
                        Console.Write($"{c}");
                    }
                    Console.WriteLine();

                }
            }
        }
    }
}
