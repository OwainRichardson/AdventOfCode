using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_16_1
    {
        public static void Execute()
        {
            string inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day16_full.txt")[0];

            string[] instructions = inputs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string[] programs = new string[16] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p" };

            foreach (string instruction in instructions)
            {
                programs = DoDance(instruction, programs);
            }

            Console.WriteLine(string.Join(" ", programs));
        }

        private static string[] DoDance(string instruction, string[] programs)
        {
            string spinPattern = @"s(\d+)";
            string exchangePattern = @"x(\d+)\/(\d+)";
            string partnerPattern = @"p(\w)\/(\w+)";

            if (Regex.Match(instruction, spinPattern).Success)
            {
                Match match = Regex.Match(instruction, spinPattern);
                int spin = int.Parse(match.Groups[1].Value);

                string[] temp = new string[programs.Length];

                for (int i = 0; i < programs.Length; i++)
                {
                    temp[(i + spin) % programs.Length] = programs[i];
                }

                programs = temp;
            }
            else if (Regex.Match(instruction, exchangePattern).Success)
            {
                Match match = Regex.Match(instruction, exchangePattern);
                int firstIndex = int.Parse(match.Groups[1].Value);
                int secondIndex = int.Parse(match.Groups[2].Value);
                string[] temp = new string[programs.Length];
                programs.CopyTo(temp, 0);

                temp[firstIndex] = programs[secondIndex];
                temp[secondIndex] = programs[firstIndex];

                programs = temp;
            }
            else if (Regex.Match(instruction, partnerPattern).Success)
            {
                Match match = Regex.Match(instruction, partnerPattern);

                string firstProgram = match.Groups[1].Value;
                int indexOfFirstProgram = Array.IndexOf(programs, firstProgram);

                string secondProgram = match.Groups[2].Value;
                int indexOfSecondProgram = Array.IndexOf(programs, secondProgram);

                string[] temp = new string[programs.Length];
                programs.CopyTo(temp, 0);

                temp[indexOfFirstProgram] = programs[indexOfSecondProgram];
                temp[indexOfSecondProgram] = programs[indexOfFirstProgram];

                programs = temp;
            }

            return programs;
        }
    }
}
