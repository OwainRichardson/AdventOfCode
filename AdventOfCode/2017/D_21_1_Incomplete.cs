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
    public static class D_21_1_Incomplete
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day21_full.txt");

            List<Rule> rules = ParseInputs(inputs);

            string input = ".#./..#/###";

            PrintInput(input);

            for (int i = 1; i <= 2; i++)
            {
                if (input.Replace("/", "").Length % 2 == 0)
                {
                    List<string> splits = GetSplits(input, 2);

                    if (input.Replace("/", "").Length / 4 == 1)
                    {
                        input = CheckTwoByTwoRules(input, rules.Where(x => x.Match.Replace("/", "").Length % 2 == 0).ToList());
                    }
                    else
                    {
                        int index = 1;
                        foreach (var split in splits)
                        {
                            string answer = CheckTwoByTwoRules(split, rules.Where(x => x.Match.Replace("/", "").Length % 2 == 0).ToList());
                        }
                    }
                }
                else if (input.Replace("/", "").Length % 3 == 0)
                {
                    if (input.Replace("/", "").Length / 3 == 3)
                    {
                        input = CheckThreeByThreeRules(input, rules.Where(x => x.Match.Replace("/", "").Length % 3 == 0).ToList());
                    }
                    else
                    {
                        // Split in to 3s
                    }

                    PrintInput(input);
                }
                else
                {
                    throw new ArithmeticException();
                }
            }
        }

        private static List<string> GetSplits(string input, int divisor)
        {
            List<string> splits = new List<string>();

            int lineLength = input.Replace("/", "").Length / (divisor * divisor);

            int row = 0;
            int lineIndex = 0;
            for (int index = 0; index < input.Replace("/", "").Length;)
            {
                string answer = string.Empty;
                if (row % divisor != 0 && splits.Any())
                {
                    answer = splits.First(x => x.Length == divisor + 1);
                }

                for (int d = 1; d <= divisor; d++)
                {
                    answer = $"{answer}{input.Replace("/", "")[index].ToString()}";

                    if (d != 1)
                    {
                        answer = $"{answer}/";
                    }
                    index++;
                    lineIndex++;
                }

                if (row % divisor == 0)
                {
                    splits.Add(answer);
                }
                else
                {
                    var firstSplit = splits.First(x => x.Length == divisor + 1);
                    int indexOfFirstSplit = splits.IndexOf(firstSplit);
                    splits[indexOfFirstSplit] = answer.Remove(answer.Length - 1);
                }

                if (lineIndex == lineLength)
                {
                    row++;
                    lineIndex = 0;
                }
            }

            return splits;
        }

        private static string CheckTwoByTwoRules(string input, List<Rule> rules, int count = 0)
        {
            if (count == 4)
            {
                return input;
            }

            if (rules.Any(x => x.Match == input))
            {
                return rules.First(x => x.Match == input).Replace;
            }

            // Mirror
            string mirror = MirrorInput(input);
            if (rules.Any(x => x.Match == mirror))
            {
                return rules.First(x => x.Match == mirror).Replace;
            }

            // Rotate and check
            return CheckTwoByTwoRules(RotateInput(input), rules, count + 1);
        }

        private static string RotateInput(string input)
        {
            input = input.Replace("/", "");
            string result = string.Empty;

            for (int index = 0; index < input.Length; index++)
            {
                result = $"{result}{input[(index + 1) % input.Length]}";
            }

            return result.Insert(input.Length / 2, "/");
        }

        private static string MirrorInput(string input)
        {
            string[] split = input.Split('/');
            string result = string.Empty;

            foreach (string s in split)
            {
                result = $"{result}{s.Reverse()}/";
            }

            return result.Substring(result.Length - 1);
        }

        private static void PrintInput(string input)
        {
            foreach (Char c in input)
            {
                if (c.ToString() == "/")
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(c.ToString());
                }
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static List<Rule> ParseInputs(string[] inputs)
        {
            List<Rule> rules = new List<Rule>();

            foreach (string input in inputs)
            {
                var split = input.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);

                Rule rule = new Rule
                {
                    Match = split[0],
                    Replace = split[1]
                };

                rules.Add(rule);
            }

            return rules;
        }

        private static string CheckThreeByThreeRules(string input, List<Rule> rules)
        {
            if (rules.Any(x => x.Match == input))
            {
                return rules.First(x => x.Match == input).Replace;
            }

            // Mirror

            // Rotate and check

            return "";
        }
    }
}
