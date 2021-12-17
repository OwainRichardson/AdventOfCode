using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            Dictionary<string, long> pairs = CompilePairs(template);

            for (int cycle = 1; cycle <= _cycles; cycle++)
            {
                pairs = Step(pairs, rules);
            }

            foreach (var pair in pairs)
            {
                _combined[pair.Key[0].ToString()] += pair.Value;
            }

            _combined[template.Last().ToString()] += 1;

            Console.WriteLine(_combined.Values.Max() - _combined.Values.Min());
        }

        private static Dictionary<string, long> Step(Dictionary<string, long> pairs, Dictionary<string, string> rules)
        {
            Dictionary<string, long> newPairs = rules.Keys.ToDictionary(x => x, x => (long)0);

            foreach (var pair in pairs)
            {
                string[] split = new string[] { rules[pair.Key], $"{rules[pair.Key][1]}{pair.Key[1]}" };

                foreach (string key in split)
                {
                    newPairs[key] += pair.Value;
                }
            }

            return newPairs;
        }

        private static Dictionary<string, long> CompilePairs(string template)
        {
            Dictionary<string, long> pairs = new Dictionary<string, long>();
            for (int i = 0; i < template.Length - 1; i++)
            {
                pairs.Add(template.Substring(i, 2), 1);
            }

            return pairs;
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
