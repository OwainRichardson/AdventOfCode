using AdventOfCode._2024.Models;
using System.Xml.Schema;

namespace AdventOfCode._2024
{
    public static class D_19_1
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day19.txt");

            (List<string> available, List<string> wanted) = ParseInputs(inputs);

            int total = 0;
            foreach (string design in wanted)
            {
                if (CheckIfPossible(design, available))
                {
                    total += 1;
                }
            }

            return $"{total}";
        }

        private static bool CheckIfPossible(string design, List<string> available, int index = 0, string soFar = "")
        {
            if (soFar == design) return true;

            if (!design.StartsWith(soFar)) return false;

            List<string> nextAvailable = available.Where(a => a.StartsWith(design[index])).ToList();
            if (!nextAvailable.Any()) return false;

            foreach (string next in nextAvailable)
            {
                bool isPossible = CheckIfPossible(design, available, index + next.Length, soFar + next);
                if (isPossible)
                {
                    return true;
                }
            }

            return false;
        }

        private static (List<string> available, List<string> wanted) ParseInputs(string[] inputs)
        {
            List<string> available = inputs[0].Split(',').Select(i => i.Trim()).ToList();

            List<string> wanted = new List<string>();

            for (int i = 2; i < inputs.Length; i++)
            {
                wanted.Add(inputs[i]);
            }

            return (available, wanted);
        }
    }
}