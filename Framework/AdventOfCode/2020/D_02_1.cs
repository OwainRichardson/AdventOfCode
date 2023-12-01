using AdventOfCode.Common;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_02_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day02.txt");

            string pattern = @"^(\d+)-(\d+)\s{1}(\w{1}):\s{1}(\w+)$";
            Regex regex = new Regex(pattern);
            int numberOfValidPasswords = 0;

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);
                int min = match.Groups[1].Value.ToInt();
                int max = match.Groups[2].Value.ToInt();
                string character = match.Groups[3].Value;
                string password = match.Groups[4].Value;

                if (PasswordIsValid(min, max, character, password))
                {
                    numberOfValidPasswords += 1;
                }
            }

            Console.WriteLine(numberOfValidPasswords);
        }

        private static bool PasswordIsValid(int min, int max, string character, string password)
        {
            var timesInString = password.Count(x => x.Equals(Char.Parse(character)));

            if (timesInString >= min && timesInString <= max)
            {
                return true;
            }

            return false;
        }
    }
}
