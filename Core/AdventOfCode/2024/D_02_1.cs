namespace AdventOfCode._2024
{
    public static class D_02_1
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
            bool isIncreasing = false;

            if (report[0] < report[1])
                isIncreasing = true;

            for (int index = 0; index < report.Count - 1; index++)
            {
                if (isIncreasing)
                {
                    if (report[index] >= report[index + 1] || report[index] + 3 < report[index + 1])
                    {
                        return false;
                    }
                }
                else
                {
                    if (report[index] <= report[index + 1] || report[index] - 3 > report[index + 1])
                    {
                        return false;
                    }
                }
            }

            return true;
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
