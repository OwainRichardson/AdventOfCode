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
    public static class D_07_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day07_full.txt");

            int total = 0;
            foreach (var input in inputs)
            {
                bool isValid = false;
                bool hyperTextIsPalindrome = false;
                var hypernetMatches = Regex.Matches(input, @"\[(\w+)\]");

                foreach (Match match in hypernetMatches)
                {
                    if (ContainsABBAPalindrome(match.Groups[1].Value))
                    {
                        hyperTextIsPalindrome = true;
                        break;
                    }
                }

                if (hyperTextIsPalindrome)
                {
                    continue;
                }

                var matches = Regex.Matches(input, @"(\w*)\[");

                foreach (Match match in matches)
                {
                    var group = match.Groups[1].Value;

                    if (ContainsABBAPalindrome(group))
                    {
                        total++;
                        isValid = true;
                        break;
                    }
                }

                if (!isValid)
                {
                    matches = Regex.Matches(input, @"\](\w*)");
                    foreach (Match match in matches)
                    {
                        var group = match.Groups[1].Value;

                        if (ContainsABBAPalindrome(group))
                        {
                            total++;
                            break;
                        }
                    }
                }
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(total);
            Console.ResetColor();
        }

        private static bool ContainsABBAPalindrome(string hypernet)
        {
            for (int i = 0; i < hypernet.Length - 3; i++)
            {
                if (hypernet[i] == hypernet[i + 3] && hypernet[i + 1] == hypernet[i + 2] && hypernet[i] != hypernet[i + 1])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
