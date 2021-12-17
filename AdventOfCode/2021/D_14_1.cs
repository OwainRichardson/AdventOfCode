using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_14_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day14.txt");

            StringBuilder template = new StringBuilder(inputs[0]);

            Dictionary<string, string> rules = ParsePolyRules(inputs);

            for (int step = 1; step <= 10; step++)
            {
                template = StepPoly(template, rules);
            }

            var groups = template.ToString().GroupBy(c => c, (key, g) => new { Element = key, Count = g.Count() }).ToList();

            var mostElement = groups.Max(y => y.Count);
            var minElement = groups.Min(y => y.Count);

            Console.WriteLine(mostElement - minElement);
        }

        private static StringBuilder StepPoly(StringBuilder template, Dictionary<string, string> rules)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < template.Length - 1; i++)
            {
                string matcher = $"{template[i]}{template[i + 1]}";
                sb.Append(rules[matcher]);
            }

            sb.Append(template[template.Length - 1]);

            return sb;
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
