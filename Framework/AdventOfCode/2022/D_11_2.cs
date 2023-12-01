using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_11_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day11.txt").ToArray();
            List<Monkey> monkeys = ParseMonkeys(inputs);

            for (int turn = 1; turn <= 10000; turn++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    while (monkey.Items.Any())
                    {
                        monkey.ItemsInspected += 1;

                        BigInteger item = monkey.Items.Dequeue();

                        if (monkey.Operation == "*")
                        {
                            int operationValue;
                            if (int.TryParse(monkey.OperationValue, out operationValue))
                            {
                                item = item * operationValue;
                            }
                            else
                            {
                                item = item * item;
                            }
                        }
                        else
                        {
                            int operationValue;
                            if (int.TryParse(monkey.OperationValue, out operationValue))
                            {
                                item = item + operationValue;
                            }
                            else
                            {
                                item = item + item;
                            }
                        }

                        if (item % monkey.Divisible == 0)
                        {
                            monkeys.Single(m => m.Id == monkey.TrueMonkey).Items.Enqueue(monkey.Divisible);
                        }
                        else
                        {
                            monkeys.Single(m => m.Id == monkey.FalseMonkey).Items.Enqueue(item);
                        }
                    }
                }
            }

            List<int> topTwoMonkeys = monkeys.OrderByDescending(m => m.ItemsInspected).Take(2).Select(m => m.ItemsInspected).ToList();
            int monkeyBusiness = 1;
            topTwoMonkeys.ForEach(m => { monkeyBusiness *= m; });
            
            Console.WriteLine(monkeyBusiness);
        }

        private static List<Monkey> ParseMonkeys(string[] inputs)
        {
            List<Monkey> monkeys = new List<Monkey>();
            Monkey currentMonkey = new Monkey();

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    monkeys.Add(currentMonkey);
                    currentMonkey = new Monkey();

                    continue;
                }

                if (input.StartsWith("Monkey"))
                {
                    string pattern = @"Monkey (\d+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);

                    currentMonkey.Id = int.Parse(match.Groups[1].Value);
                }
                else if (input.Trim().StartsWith("Starting items"))
                {
                    string pattern = @"Starting items: (.+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input.Trim());

                    List<int> items = match.Groups[1].Value.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                    foreach (int item in items)
                    {
                        currentMonkey.Items.Enqueue(item);
                    }
                }
                else if (input.Trim().StartsWith("Operation"))
                {
                    string pattern = @"Operation: new = old ([\+|\*]) (.+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input.Trim());

                    currentMonkey.Operation = match.Groups[1].Value;
                    currentMonkey.OperationValue = match.Groups[2].Value;
                }
                else if (input.Trim().StartsWith("Test"))
                {
                    string pattern = @"Test: divisible by (\d+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input.Trim());

                    currentMonkey.Divisible = int.Parse(match.Groups[1].Value);
                }
                else if (input.Trim().StartsWith("If true"))
                {
                    string pattern = @"If true: throw to monkey (\d+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input.Trim());

                    currentMonkey.TrueMonkey = int.Parse(match.Groups[1].Value);
                }
                else if (input.Trim().StartsWith("If false"))
                {
                    string pattern = @"If false: throw to monkey (\d+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input.Trim());

                    currentMonkey.FalseMonkey = int.Parse(match.Groups[1].Value);
                }
            }

            monkeys.Add(currentMonkey);

            return monkeys;
        }
    }
}
