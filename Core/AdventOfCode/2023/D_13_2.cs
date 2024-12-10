using AdventOfCode._2023.Models;

namespace AdventOfCode._2023
{
    public static class D_13_2
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
                bool differencePassUsed = false;

                while (index + columnComparisonIndex < pattern.Rocks[0].Length && index - 1 - columnComparisonIndex >= 0)
                {
                    int differences = FindDifferencesInColumns(pattern.Rocks, index, columnComparisonIndex);

                    if (differences == 1 && !differencePassUsed)
                    {
                        differencePassUsed = true;
                    }
                    else if (differences > 0)
                    {
                        symmetryFound = false;
                        break;
                    }

                    columnComparisonIndex += 1;
                }

                if (!differencePassUsed)
                {
                    symmetryFound = false;
                }

                if (!symmetryFound) continue;

                return index;
            }

            return 0;
        }

        private static int FindDifferencesInColumns(List<string> rocks, int index, int columnComparisonIndex)
        {
            int differences = 0;

            for (int i = 0; i < rocks.Count; i++)
            {
                if (rocks[i][index + columnComparisonIndex] != rocks[i][index - 1 - columnComparisonIndex])
                {
                    differences += 1;
                }
            }

            return differences;
        }

        private static (bool horizontal, int rows) CheckHorizontal(Pattern pattern)
        {
            for (int index = 1; index < pattern.Rocks.Count; index++)
            {
                bool symmetryFound = true;
                int rowComparisonIndex = 0;
                bool differencePassUsed = false;

                while (rowComparisonIndex < pattern.Rocks.Count - index && (index - 1 - rowComparisonIndex) >= 0)
                {
                    int differences = FindDifferencesInRows(pattern.Rocks, index, rowComparisonIndex);

                    if (differences == 1 && !differencePassUsed)
                    {
                        differencePassUsed = true;
                    }
                    else if (differences > 0)
                    {
                        symmetryFound = false;
                        break;
                    }

                    rowComparisonIndex += 1;
                }

                if (!differencePassUsed)
                {
                    symmetryFound = false;
                }

                if (!symmetryFound) continue;

                return (true, index);
            }

            return (false, 0);
        }

        private static int FindDifferencesInRows(List<string> rocks, int index, int rowComparisonIndex)
        {
            int differences = 0;

            for (int i = 0; i < rocks[index].Length; i++)
            {
                if (rocks[index + rowComparisonIndex][i] != rocks[index - 1 - rowComparisonIndex][i])
                {
                    differences += 1;
                }
            }

            return differences;
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