using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Runtime;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2023
{
    public static class D_13_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day13.txt").ToArray();

            List<Pattern> patterns = ParseInputsToPatterns(inputs);
            int total = 0;

            foreach (Pattern pattern in patterns)
            {
                int score = FindLineOfReflection(pattern);

                total += score;
            }

            Console.WriteLine(total);
        }

        private static int FindLineOfReflection(Pattern pattern)
        {
            (bool horizontal, int rows) = CheckHorizontal(pattern);
            int columns = 0;

            if (!horizontal)
            {
                columns = CheckVertical(pattern);
            }

            if (horizontal)
            {
                return rows * 100;
            }
            else
            {
                return columns;
            }
        }

        private static int CheckVertical(Pattern pattern)
        {
            for (int index = 1; index < pattern.Rocks[0].Length; index++)
            {
                bool symmetryFound = true;

                int columnComparisonIndex = 0;
                bool columnsMatch = true;

                while (index + columnComparisonIndex < pattern.Rocks[0].Length && index - 1 - columnComparisonIndex >= 0)
                {
                    columnsMatch = DoColumnsMatch(pattern.Rocks, index, columnComparisonIndex);
                    if (!columnsMatch)
                    {
                        symmetryFound = false;
                        break;
                    }

                    columnComparisonIndex += 1;
                }

                if (!symmetryFound) continue;

                return index;
            }

            return 0;
        }

        private static bool DoColumnsMatch(List<string> rocks, int index, int columnComparisonIndex)
        {
            for (int i = 0; i < rocks.Count; i++)
            {
                if (rocks[i][index + columnComparisonIndex] != rocks[i][index - 1 - columnComparisonIndex])
                {
                    return false;
                }
            }

            return true;
        }

        private static (bool horizontal, int rows) CheckHorizontal(Pattern pattern)
        {

            for (int index = 1; index < pattern.Rocks.Count; index++)
            {
                bool symmetryFound = true;

                int rowComparisonIndex = 0;

                while (rowComparisonIndex < pattern.Rocks.Count - index && (index - 1 - rowComparisonIndex) >= 0)
                {
                    if (pattern.Rocks[index - 1 - rowComparisonIndex] != pattern.Rocks[index + rowComparisonIndex])
                    {
                        symmetryFound = false;
                        break;
                    }

                    rowComparisonIndex += 1;
                }

                if (!symmetryFound) continue;

                return (true, index);
            }

            return (false, 0);
        }

        private static List<Pattern> ParseInputsToPatterns(string[] inputs)
        {
            List<Pattern> patterns = new List<Pattern>();
            Pattern pattern = new Pattern();

            for (int i = 0; i < inputs.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(inputs[i]))
                {
                    pattern.Rocks.Add(inputs[i]);
                }
                else
                {
                    patterns.Add(pattern);
                    pattern = new Pattern();
                }
            }

            patterns.Add(pattern);

            return patterns;
        }
    }
}