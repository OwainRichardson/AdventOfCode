using AdventOfCode._2024.Models;
using AdventOfCode._2024.Models.Enums;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http.Headers;

namespace AdventOfCode._2024
{
    public static class D_07_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day07.txt").ToArray();
            long answer = 0;

            foreach (string input in inputs)
            {
                string[] split = input.Split(':', StringSplitOptions.RemoveEmptyEntries);
                long target = long.Parse(split[0]);

                List<int> values = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList();

                int combinationLength = values.Count - 1;
                string combination = "".PadLeft(combinationLength, '0');

                while (!string.IsNullOrEmpty(combination))
                {
                    int combinationIndex = 0;
                    long total = values[0];
                    for (int index = 1; index <= values.Count - 1; index++)
                    {
                        if (combination[combinationIndex] == '0')
                        {
                            total += values[index];
                        }
                        else if (combination[combinationIndex] == '1')
                        {
                            total *= values[index];
                        }
                        else if (combination[combinationIndex] == '2')
                        {
                            total = long.Parse($"{total}{values[index]}");
                        }

                        combinationIndex++;
                    }

                    if (total == target)
                    {
                        answer += target;
                        break;
                    }

                    combination = CalculateCombination(combination, 0, 2, combination.Length - 1);
                }
            }

            Console.WriteLine(answer);
        }

        private static string CalculateCombination(string combination, int min, int max, int startIndex)
        {
            if (startIndex < 0) return null;

            int[] split = combination.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();

            split[startIndex] += 1;

            if (split[startIndex] > max)
            {
                split[startIndex] = 0;

                return CalculateCombination(string.Join(null, split), min, max, startIndex - 1);
            }

            return string.Join(null, split);
        }
    }
}