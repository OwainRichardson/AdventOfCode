using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_06_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day06_full.txt");

            var inputLength = inputs[0].Length;
            string answer = string.Empty;

            CustomConsoleColour.SetAnswerColour();

            for (int i = 0; i < inputLength; i++)
            {
                var firstLetters = inputs.Select(x => x.Substring(i, 1)).GroupBy(x => x);
                var mostCommonFirstLetter = firstLetters.Min(x => x.Count());

                Console.Write($"{firstLetters.First(x => x.Count() == mostCommonFirstLetter).Key}");                
            }

            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
