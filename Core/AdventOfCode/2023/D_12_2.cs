using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Runtime;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_12_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day12.txt").ToArray();

            List<SpringMap> springMaps = ParseInputsToSpringMaps(inputs);

            ExpandMaps(springMaps);

            CalculatePermutations(springMaps);
        }

        private static void ExpandMaps(List<SpringMap> springMaps)
        {
            int number = 5;

            foreach (var springMap in springMaps)
            {
                List<int> initialNumberMap = new List<int>(springMap.NumberMap);
                string initialMap = springMap.DamagedMap;

                for (int index = 1; index < number; index++)
                {
                    springMap.DamagedMap = $"{springMap.DamagedMap}?{initialMap}";
                    springMap.NumberMap.AddRange(initialNumberMap);
                }
            }
        }

        private static void CalculatePermutations(List<SpringMap> springMaps)
        {
            int matches = 0;

            foreach (SpringMap springMap in springMaps)
            {
                List<string> replacementCombinations = CalculateReplacementCombinations(springMap.DamagedMap);

                foreach (string combo in replacementCombinations)
                {
                    StringBuilder replacedString = new StringBuilder();
                    int replacementIndex = 0;

                    for (int index = 0; index < springMap.DamagedMap.Length; index++)
                    {
                        if (springMap.DamagedMap[index] != '?')
                        {
                            replacedString.Append(springMap.DamagedMap[index]);
                            continue;
                        }

                        replacedString.Append(combo[replacementIndex] == '0' ? "." : "#");

                        replacementIndex++;
                    }

                    if (MatchesRule(replacedString.ToString(), springMap.NumberMap))
                    {
                        matches++;
                    }
                }
            }

            Console.WriteLine(matches);
        }

        private static bool MatchesRule(string replacedString, List<int> numberMap)
        {
            string[] split = replacedString.Split('.', StringSplitOptions.RemoveEmptyEntries);

            if (split.Length != numberMap.Count) return false;

            for (int index = 0; index < split.Length; index++)
            {
                if (split[index].Length != numberMap[index])
                {
                    return false;
                }
            }

            return true;
        }

        private static List<string> CalculateReplacementCombinations(string damagedMap)
        {
            int numberOfReplacementsToMake = damagedMap.Count(m => m.Equals('?'));

            return CreateReplacementCombinations(numberOfReplacementsToMake);
        }

        private static List<string> CreateReplacementCombinations(int numberOfReplacementsToMake)
        {
            List<string> combinations = new List<string>();

            int number = 0;
            while (true)
            {
                string result = Convert.ToString(number, 2);
                while (result.Length < numberOfReplacementsToMake)
                {
                    result = $"0{result}";
                }

                if (result.Length > numberOfReplacementsToMake)
                {
                    break;
                }

                combinations.Add(result);

                number++;
            }

            return combinations;
        }

        private static List<SpringMap> ParseInputsToSpringMaps(string[] inputs)
        {
            List<SpringMap> springMaps = new List<SpringMap>();

            foreach (string input in inputs)
            {
                string[] inputSplit = input.Split(' ');
                SpringMap springMap = new SpringMap
                {
                    DamagedMap = inputSplit[0],
                    NumberMap = inputSplit[1].Split(',').Select(x => int.Parse(x)).ToList()
                };
                springMaps.Add(springMap);
            }

            return springMaps;
        }
    }
}