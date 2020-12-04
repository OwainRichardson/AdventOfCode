using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_04_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day04.txt");

            Console.WriteLine(NumberOfValidPassports(inputs));
        }

        private static int NumberOfValidPassports(string[] inputs)
        {
            int index = 0;
            List<string> passportDetails = new List<string>();
            string current = string.Empty;

            while (index <= inputs.Length)
            {
                if (index == inputs.Length || string.IsNullOrWhiteSpace(inputs[index]))
                {
                    passportDetails.Add(current);
                    current = string.Empty;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(current))
                    {
                        current = $"{current} {inputs[index]}";
                    }
                    else
                    {
                        current = inputs[index];
                    }
                }

                index += 1;
            }

            int validPassports = 0;
            foreach (string passport in passportDetails)
            {
                if (passport.Contains("byr") && passport.Contains("iyr") && passport.Contains("eyr") && passport.Contains("hgt") && passport.Contains("hcl") && passport.Contains("ecl") && passport.Contains("pid"))
                {
                    validPassports += 1;
                }
            }

            return validPassports;
        }
    }
}
