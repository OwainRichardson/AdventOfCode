using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class D_08_1
    {
        private static int _charsInMemory = 0;
        private static int _charsInString = 0;

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day08_full.txt");

            for (int i = 0; i < input.Length; i++)
            {
                _charsInString += input[i].Length;

                var memory = input[i];
                memory = Regex.Replace(memory.Trim('"').Replace("\\\"", "a").Replace("\\\\", "b"), "\\\\x[a-f0-9]{2}", "c");

                _charsInMemory += memory.Length;
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(_charsInString - _charsInMemory);
            Console.ResetColor();
        }
    }
}
