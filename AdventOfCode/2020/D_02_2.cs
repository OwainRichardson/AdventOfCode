using AdventOfCode.Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_02_2
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
                int index1 = match.Groups[1].Value.ToInt();
                int index2 = match.Groups[2].Value.ToInt();
                string character = match.Groups[3].Value;
                string password = match.Groups[4].Value;

                if (PasswordIsValid(index1, index2, character, password))
                {
                    numberOfValidPasswords += 1;
                }
            }

            Console.WriteLine(numberOfValidPasswords);
        }

        private static bool PasswordIsValid(int index1, int index2, string character, string password)
        {
            int matches = 0;

            if (password[index1 - 1] == char.Parse(character))
            {
                matches += 1;
            }

            if (password[index2 - 1] == char.Parse(character))
            {
                matches += 1;
            }

            if (matches == 1)
            {
                return true;
            }

            return false;
        }
    }
}
