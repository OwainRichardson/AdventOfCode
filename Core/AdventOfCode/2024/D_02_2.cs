namespace AdventOfCode._2024
{
    public static class D_02_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day02.txt").ToArray();

            int safeReports = 0;

            List<List<int>> reports = ParseInputsToReports(inputs);

            foreach (List<int> report in reports)
            {
                if (IsSafe(report))
                {
                    safeReports += 1;
                }
            }

            Console.WriteLine(safeReports);
        }

        private static bool IsSafe(List<int> report)
        {
            for (int removeIndex = 0; removeIndex < report.Count; removeIndex++)
            {
                List<int> currentReport = new List<int>(report);

                currentReport.RemoveAt(removeIndex);
                bool isIncreasing = false;
                bool isSafe = true;

                if (currentReport[0] < currentReport[1])
                    isIncreasing = true;

                for (int index = 0; index < currentReport.Count - 1; index++)
                {
                    if (isIncreasing)
                    {
                        if (currentReport[index] >= currentReport[index + 1] || currentReport[index] + 3 < currentReport[index + 1])
                        {
                            isSafe = false;
                            break;
                        }
                    }
                    else
                    {
                        if (currentReport[index] <= currentReport[index + 1] || currentReport[index] - 3 > currentReport[index + 1])
                        {
                            isSafe = false;
                            break;
                        }
                    }
                }

                if (isSafe)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<List<int>> ParseInputsToReports(string[] inputs)
        {
            List<List<int>> reports = new List<List<int>>();

            foreach (string input in inputs)
            {
                List<int> report = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToList();

                reports.Add(report);
            }

            return reports;
        }
    }
}
