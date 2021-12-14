using AdventOfCode._2021.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_14_2
    {
        private static Dictionary<string, long> _combined;
        private static int _cycles = 40;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day14.txt");

            string template = inputs[0];
            Dictionary<string, string> rules = ParsePolyRules(inputs);

            var joinedList = string.Join("", rules.Keys).Distinct().Select(x => x.ToString()).ToList();
            _combined = joinedList.ToDictionary(x => x, x => (long)0);

            List<string> pairs = CompilePairs(template);
            int pairIndex = 1;
            foreach (string pair in pairs)
            {
                Console.Write($"\r{pairIndex}/{pairs.Count}          ");

                Step(pair, rules);
                
                pairIndex++;
            }

            _combined[template.Last().ToString()] += 1;

            foreach(var combine in _combined)
            {
                Console.WriteLine($"\r {combine.Key} - {combine.Value}");
            }

            Console.WriteLine($"\r{_combined.Max(x => x.Value) - _combined.Min(x => x.Value)}                                       ");
        }

        private static List<string> CompilePairs(string template)
        {
            List<string> pairs = new List<string>();
            for (int i = 0; i < template.Length - 1; i++)
            {
                pairs.Add(template.Substring(i, 2));
            }

            return pairs;
        }

        private static void Step(string template, Dictionary<string, string> rules, int cycles = 1)
        {
            if (cycles > _cycles)
            {
                _combined[template[0].ToString()] += 1;
                return;
            }

            template = $"{rules[template]}{template.Last()}";

            for (int index = 0; index < template.Length - 1; index++)
            {
                Step(template.Substring(index, 2), rules, cycles + 1);
            }
        }

        private static List<string> SplitTemplate(string template)
        {
            List<string> pairs = new List<string>();

            for (int i = 0; i < template.Length - 1; i++)
            {
                pairs.Add(template.Substring(i, 2));
            }

            return pairs;
        }

        private static Dictionary<string, string> ParsePolyRules(string[] inputs)
        {
            Dictionary<string, string> rules = new Dictionary<string, string>();
            bool readyForRules = false;

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    readyForRules = true;
                    continue;
                }

                if (readyForRules)
                {
                    string[] parts = input.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                    rules.Add(parts[0], $"{parts[0][0]}{parts[1]}");
                }
            }

            return rules;
        }
    }
}
