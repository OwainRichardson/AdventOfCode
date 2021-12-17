using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public static class D_12_1
    {
        private static List<int> _numbers = new List<int>();

        public static void Execute()
        {
            var input = string.Join(" ", File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day12_full.txt"));

            Regex r = new Regex(@"(-*\d*)", RegexOptions.IgnoreCase);

            Match m = r.Match(input);

            while (m.Success)
            {
                if (m.Groups[0].Length > 0)
                {
                    _numbers.Add(int.Parse(m.Groups[0].Value));
                }

                m = m.NextMatch();
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine($"{_numbers.Sum()}");
            Console.ResetColor();
        }
    }
}