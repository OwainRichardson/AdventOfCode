using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_04_2
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

            int validPassports = CheckValidPassports(passportDetails);

            return validPassports;
        }

        private static int CheckValidPassports(List<string> passportDetails)
        {
            int validPassports = 0;

            foreach (var passportDetail in passportDetails)
            {
                string[] details = passportDetail.Split(' ');

                if (details.Length <= 6 || (details.Length == 7 && passportDetail.Contains("cid"))) continue;
                bool invalid = false;

                foreach (string detail in details)
                {
                    if (detail.StartsWith("byr"))
                    {
                        var value = detail.Substring(detail.IndexOf(":") + 1);
                        if (value.Length != 4 || int.Parse(value) < 1920 || int.Parse(value) > 2002)
                        {
                            invalid = true;
                            break;
                        }
                        continue;
                    }
                    else if (detail.StartsWith("iyr"))
                    {
                        var value = detail.Substring(detail.IndexOf(":") + 1);
                        if (value.Length != 4 || int.Parse(value) < 2010 || int.Parse(value) > 2020)
                        {
                            invalid = true;
                            break;
                        }
                        continue;
                    }
                    else if (detail.StartsWith("eyr"))
                    {
                        var value = detail.Substring(detail.IndexOf(":") + 1);
                        if (value.Length != 4 || int.Parse(value) < 2020 || int.Parse(value) > 2030)
                        {
                            invalid = true;
                            break;
                        }
                        continue;
                    }
                    else if (detail.StartsWith("hgt"))
                    {
                        var value = detail.Substring(detail.IndexOf(":") + 1);
                        if (value.Contains("cm"))
                        {
                            int number = int.Parse(value.Substring(0, value.IndexOf("cm")));
                            if (number < 150 || number > 193)
                            {
                                invalid = true;
                                break;
                            }
                        }
                        else if (value.Contains("in"))
                        {
                            int number = int.Parse(value.Substring(0, value.IndexOf("in")));
                            if (number < 59 || number > 76)
                            {
                                invalid = true;
                                break;
                            }
                        }
                        else
                        {
                            invalid = true;
                            break;
                        }
                        continue;
                    }
                    else if (detail.StartsWith("hcl"))
                    {
                        var value = detail.Substring(detail.IndexOf(":") + 1);
                        string pattern = "^#([a-fA-F0-9]{6})$";
                        Regex regex = new Regex(pattern);

                        if (value.Length != 7 || !regex.IsMatch(value))
                        {
                            invalid = true;
                            break;
                        }
                        continue;
                    }
                    else if (detail.StartsWith("ecl"))
                    {
                        var value = detail.Substring(detail.IndexOf(":") + 1);
                        List<string> colours = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                        if (!colours.Contains(value))
                        {
                            invalid = true;
                            break;
                        }
                        continue;
                    }
                    else if (detail.StartsWith("pid"))
                    {
                        var value = detail.Substring(detail.IndexOf(":") + 1);
                        string pattern = "^[0-9]{9}$";
                        Regex regex = new Regex(pattern);

                        if (!regex.IsMatch(value))
                        {
                            invalid = true;
                            break;
                        }
                        continue;
                    }
                }

                if (!invalid)
                {
                    validPassports += 1;
                }
            }

            return validPassports;
        }
    }
}
