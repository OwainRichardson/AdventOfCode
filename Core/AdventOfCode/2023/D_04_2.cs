using AdventOfCode._2023.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_04_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day04.txt").ToArray();

            List<Scratchcard> scratchcards = ParseInputsToScratchcards(inputs);

            int scratchCardIndex = 1;

            foreach (Scratchcard scratchcard in scratchcards.OrderBy(s => s.Id))
            {
                List<int> intersect = scratchcard.YourNumbers.Intersect(scratchcard.WinningNumbers).ToList();

                for (int cardToUpdateIndex = 1; cardToUpdateIndex <= intersect.Count; cardToUpdateIndex++)
                {
                    var cardToUpdate = scratchcards.First(s => s.Id == scratchCardIndex + cardToUpdateIndex);
                    cardToUpdate.NumberOfCards += scratchcard.NumberOfCards;
                }

                scratchCardIndex++;
            }

            Console.WriteLine(scratchcards.Sum(s => s.NumberOfCards));
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