using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017
{
    public static class D_16_2
    {
        public static void Execute()
        {
            string inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day16_full.txt")[0];

            string[] instructions = inputs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string[] programs = new string[16] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p" };

            string[] original = new string[16];
            programs.CopyTo(original, 0);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            int duplicate = 0;
            int i = 1;

            string[] combos = new string[1000];

            while (duplicate == 0)
            {
                foreach (string instruction in instructions)
                {
                    programs = DoDance(instruction, programs);
                }

                combos[i - 1] = string.Join("", programs);

                if (i != 1 && string.Join("", original) == string.Join("", programs))
                {
                    duplicate = i;
                    break;
                }

                i++;
            }

            Console.WriteLine(duplicate);
            int indexOfCorrectAnswer = 1000000000 % duplicate;
            Console.WriteLine($"Correct answer is at index {indexOfCorrectAnswer - 1}");
            Console.WriteLine(combos[indexOfCorrectAnswer - 1]);

            watch.Stop();

            Console.WriteLine($"took {watch.Elapsed.TotalSeconds}");
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
