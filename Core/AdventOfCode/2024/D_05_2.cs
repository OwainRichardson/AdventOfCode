using AdventOfCode._2024.Models;

namespace AdventOfCode._2024
{
    public static class D_05_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day05.txt").ToArray();

            (List<Page> pageRules, List<List<int>> updates) = ParseInputs(inputs);

            int total = 0;

            foreach (List<int> update in updates)
            {
                (bool valid, int currentIndexOfError, int indexOfWrongPage) = UpdateIsValid(update, pageRules);

                if (!valid)
                {
                    while (!valid)
                    {
                        int current = update[currentIndexOfError];
                        int toSwap = update[indexOfWrongPage];

                        update[currentIndexOfError] = toSwap;
                        update[indexOfWrongPage] = current;

                        (valid, currentIndexOfError, indexOfWrongPage) = UpdateIsValid(update, pageRules);
                    }

                    int middleNumber = update[(int)Math.Floor((double)update.Count / 2)];
                    total += middleNumber;
                }
            }

            Console.WriteLine(total);
        }

        private static (bool valid, int currentIndexOfError, int indexOfWrongPage) UpdateIsValid(List<int> update, List<Page> pageRules)
        {
            foreach (int page in update)
            {
                Page rules = pageRules.SingleOrDefault(pr => pr.PageNumber == page);

                if (rules == null) continue;

                int indexOfCurrentPage = update.IndexOf(page);

                foreach (int otherPage in rules.MustBeBeforePages)
                {
                    int indexOfOtherPage = update.IndexOf(otherPage);
                    if (indexOfOtherPage > -1 && indexOfOtherPage < indexOfCurrentPage)
                    {
                        return (false, indexOfCurrentPage, indexOfOtherPage);
                    }
                }
            }

            return (true, -1, -1);
        }

        private static (List<Page> pageRules, List<List<int>> updates) ParseInputs(string[] inputs)
        {
            List<Page> pageRules = new List<Page>();
            List<List<int>> updates = new List<List<int>>();

            bool parsePages = true;
            bool parseUpdates = false;

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    parsePages = false;
                    parseUpdates = true;

                    continue;
                }

                if (parsePages)
                {
                    int[] split = input.Split('|', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();
                    Page existingPage = pageRules.Find(p => p.PageNumber == split[0]);

                    if (existingPage != null)
                    {
                        existingPage.MustBeBeforePages.Add(split[1]);
                    }
                    else
                    {
                        pageRules.Add(new Page { PageNumber = split[0], MustBeBeforePages = new List<int> { split[1] } });
                    }
                }

                if (parseUpdates)
                {
                    List<int> split = input.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToList();
                    updates.Add(split);
                }
            }

            return (pageRules, updates);
        }
    }
}