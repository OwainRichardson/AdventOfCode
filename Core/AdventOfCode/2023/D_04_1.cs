using AdventOfCode._2023.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_04_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day04.txt").ToArray();

            List<Scratchcard> scratchcards = ParseInputsToScratchcards(inputs);

            int score = 0;

            foreach (Scratchcard scratchcard in scratchcards)
            {
                List<int> intersect = scratchcard.YourNumbers.Intersect(scratchcard.WinningNumbers).ToList();

                score += (int)Math.Pow(2, intersect.Count - 1);
            }

            Console.WriteLine(score);
        }
        
        private static List<Scratchcard> ParseInputsToScratchcards(string[] inputs)
        {
            List<Scratchcard> scratchcards = new List<Scratchcard>();

            foreach (string input in inputs)
            {
                string cardPattern = @"^Card\W+(\d+)";
                Regex cardRegex = new Regex(cardPattern);

                Match cardMatch = cardRegex.Match(input);
                Scratchcard scratchcard = new Scratchcard
                {
                    Id = int.Parse(cardMatch.Groups[1].Value)
                };

                string allNumbers = input.Substring(input.IndexOf(':') + 1);
                string winningNumbersSet = allNumbers.Substring(0, allNumbers.IndexOf('|'));
                string yourNumbersSet = allNumbers.Substring(allNumbers.IndexOf('|') + 1);

                scratchcard.WinningNumbers = winningNumbersSet.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                scratchcard.YourNumbers = yourNumbersSet.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

                scratchcards.Add(scratchcard);
            }

            return scratchcards;
        }
    }
}