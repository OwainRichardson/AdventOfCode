using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_19_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day19.txt");

            Dictionary<int, string> rules = ParseRules(inputs);
            List<string> messages = ParseMessages(inputs);

            rules = SimplifyRules(rules);
            List<string> possibleMessages = SubstituteRules(rules).Select(x => x.Replace(" ", "")).Distinct().ToList();

            Console.WriteLine($"\r{messages.Count(x => possibleMessages.Contains(x))}                        ");
        }

        private static Dictionary<int, string> SimplifyRules(Dictionary<int, string> rules)
        {
            int changes = -1;

            while (changes != 0)
            {
                changes = 0;

                Dictionary<int, string> calculatedRules = rules.Where(x => !x.Value.Any(char.IsDigit) && !x.Value.Contains("|")).ToDictionary(x => x.Key, x => x.Value);

                for (int index = 0; index < rules.Count; index++)
                {
                    //string[] ruleSplit = rules[index].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    //int arrayIndex = 0;
                    //Array.ForEach(ruleSplit, (x) =>
                    //{
                    //    if (x.All(char.IsDigit) && calculatedRules.Keys.Contains(int.Parse(x)))
                    //    {
                    //        ruleSplit[arrayIndex] = calculatedRules.First(z => z.Key == int.Parse(x)).Value;
                    //        changes += 1;
                    //    }

                    //    arrayIndex += 1;
                    //});

                    foreach (var calc in calculatedRules)
                    {
                        string value = rules[index];

                        rules[index] = value.Replace($"{calc.Key}", $" {calc.Value} ");
                    }
                }
            }

            return rules;
        }

        private static List<string> SubstituteRules(Dictionary<int, string> rules)
        {
            Dictionary<int, string> substitutedRules = new Dictionary<int, string> { };
            substitutedRules.Add(0, rules[0]);
            var straightRules = rules.Where(x => !x.Value.Contains("|")).ToList();
            var splitRules = rules.Where(x => x.Value.Contains("|")).ToList();

            while (substitutedRules.Any(x => x.Value.Any(char.IsDigit)))
            {
                Console.Write($"\r{substitutedRules.Count}     ");

                int changes = -1;
                while (changes != 0)
                {
                    Console.Write($"\r{substitutedRules.Count}     ");

                    changes = 0;

                    for (int index = 0; index < substitutedRules.Count; index++)
                    {
                        foreach (var splitRule in splitRules)
                        {
                            if (substitutedRules[index].Contains($" {splitRule.Key} "))
                            {
                                string[] splitValues = splitRule.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                                substitutedRules.Add(substitutedRules.Keys.Max() + 1, substitutedRules[index].Replace($" {splitRule.Key }", splitValues[0]));
                                substitutedRules[index] = substitutedRules[index].Replace($" {splitRule.Key} ", splitValues[1]);

                                changes += 1;
                            }
                        }
                    }
                }

                changes = -1;
                while (changes != 0)
                {
                    Console.Write($"\r{substitutedRules.Count(x => !x.Value.Any(char.IsDigit))}     ");

                    changes = 0;

                    for (int index = 0; index < substitutedRules.Count; index++)
                    {
                        foreach (var straightRule in straightRules)
                        {
                            if (substitutedRules[index].Contains($" {straightRule.Key} "))
                            {
                                substitutedRules[index] = substitutedRules[index].Replace($" {straightRule.Key} ", $" {straightRule.Value} ");
                                changes += 1;
                            }
                        }
                    }
                }
            }

            return substitutedRules.Values.ToList();
        }

        //private static List<string> SubstituteRules(Dictionary<int, string> rules)
        //{
        //    Dictionary<int, string> substitutedRules = new Dictionary<int, string> { };
        //    substitutedRules.Add(0, rules[0]);

        //    while (substitutedRules.Values.Any(x => x.Any(char.IsDigit)))
        //    {
        //        for (int index = 0; index < substitutedRules.Count; index++)
        //        {
        //            Console.Write($"\r{substitutedRules.Values.Count(x => !x.Any(char.IsDigit))}\t / \t{substitutedRules.Count}                ");

        //            string currentRule = substitutedRules[index];

        //            if (!currentRule.Any(char.IsDigit)) continue;

        //            foreach (string s in currentRule.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
        //            {
        //                if (int.TryParse(s, out int result))
        //                {
        //                    var rule = rules[result];

        //                    if (rule.Contains("|"))
        //                    {
        //                        //var ruleSplit = rule.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

        //                        //string[] currentRuleSplit = currentRule.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        //                        //string[] currentRuleSplit2 = (string[])currentRuleSplit.Clone();

        //                        //int arrayIndex = 0;
        //                        //Array.ForEach(currentRuleSplit2, (x) =>
        //                        //{
        //                        //    if (x == result.ToString())
        //                        //    {
        //                        //        currentRuleSplit2[arrayIndex] = ruleSplit[0].TrimStart().TrimEnd();
        //                        //    }

        //                        //    arrayIndex += 1;
        //                        //});

        //                        //substitutedRules.Add(substitutedRules.Keys.Max() + 1, string.Join(" ", currentRuleSplit2));

        //                        //arrayIndex = 0;
        //                        //Array.ForEach(currentRuleSplit, (x) =>
        //                        //{
        //                        //    if (x == result.ToString())
        //                        //    {
        //                        //        currentRuleSplit[arrayIndex] = ruleSplit[1].TrimStart().TrimEnd();
        //                        //    }

        //                        //    arrayIndex += 1;
        //                        //});
        //                        //currentRule = string.Join(" ", currentRuleSplit);
        //                        //substitutedRules[index] = currentRule;



        //                        index = -1;
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        string[] currentRuleSplit = currentRule.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

        //                        int arrayIndex = 0;
        //                        Array.ForEach(currentRuleSplit, (x) =>
        //                        {
        //                            if (x == result.ToString())
        //                            {
        //                                currentRuleSplit[arrayIndex] = rule.TrimStart().TrimEnd();
        //                            }

        //                            arrayIndex += 1;
        //                        });
        //                        currentRule = string.Join(" ", currentRuleSplit);
        //                        substitutedRules[index] = currentRule;
        //                        index = -1;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return substitutedRules.Values.ToList();
        //}

        private static List<string> ParseMessages(string[] inputs)
        {
            List<string> messages = new List<string>();
            int index = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(inputs[i]))
                {
                    index = i + 1;
                }
            }

            for (int i = index; i < inputs.Length; i++)
            {
                messages.Add(inputs[i]);
            }

            return messages;
        }

        private static Dictionary<int, string> ParseRules(string[] inputs)
        {
            Dictionary<int, string> rules = new Dictionary<int, string>();
            string pattern = @"^(\d+):\s(.+)$";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input)) break;

                Match match = regex.Match(input);
                rules.Add(int.Parse(match.Groups[1].Value), $" {match.Groups[2].Value.Replace("\"", "")} ");
            }

            return rules;
        }
    }
}