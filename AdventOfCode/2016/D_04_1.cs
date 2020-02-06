using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_04_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day04_full.txt");

            int total = 0;
            foreach (var input in inputs)
            {
                total += ParseInput(input);
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine($"{total}");
            Console.ResetColor();
        }

        private static int ParseInput(string input)
        {
            List<CharacterCount> characterCounts = new List<CharacterCount>();

            Match valueRegex = Regex.Match(input, @"(\d+)");
            int value = int.Parse(valueRegex.Value);
            string checksum = Regex.Match(input, @"\[([a-z]*)\]").Groups[1].Value;

            string jumble = input.Substring(0, valueRegex.Index);

            foreach (Char c in jumble)
            {
                if (Char.IsLetter(c))
                {
                    AddOrUpdateToList(c.ToString(), ref characterCounts);
                }
            }
            characterCounts = characterCounts.OrderByDescending(x => x.Count).ThenBy(x => x.Character).ToList();

            if (characterCounts.Count < 5)
            {
                return 0;
            }

            string calculatedChecksum = string.Empty;
            for (int i = 1; i <= 5; i++)
            {
                calculatedChecksum = $"{calculatedChecksum}{characterCounts[i - 1].Character}";
            }

            if (calculatedChecksum.Equals(checksum))
            {
                return value;
            }

            return 0;
        }

        private static void AddOrUpdateToList(string character, ref List<CharacterCount> characterCounts)
        {
            if (characterCounts.Any(x => x.Character == character))
            {
                var charCount = characterCounts.First(x => x.Character == character);
                charCount.Count++;
            }
            else
            {
                characterCounts.Add(
                    new CharacterCount
                    {
                        Character = character,
                        Count = 1
                    });
            }
        }
    }
}
