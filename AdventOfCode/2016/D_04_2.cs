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
    public static class D_04_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day04_full.txt");

            foreach (var input in inputs)
            {
                ParseInput(input);
            }
        }

        private static void ParseInput(string input)
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
                return;
            }

            string calculatedChecksum = string.Empty;
            for (int i = 1; i <= 5; i++)
            {
                calculatedChecksum = $"{calculatedChecksum}{characterCounts[i - 1].Character}";
            }

            if (calculatedChecksum.Equals(checksum))
            {
                ShiftInput(jumble, value);
            }
        }

        private static void ShiftInput(string jumble, int value)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string answer = string.Empty;

            foreach (Char c in jumble)
            {
                string newChar = "";
                if (!Char.IsLetter(c))
                {
                    newChar = " ";
                }
                else
                {
                    var index = alphabet.IndexOf(c.ToString());
                    newChar = alphabet[(index + value) % 26].ToString();
                }

                answer = $"{answer}{newChar}";
            }

            if (answer.Contains("north"))
            {
                Console.Write($"{answer} [");
                CustomConsoleColour.SetAnswerColour();
                Console.Write(value);
                Console.ResetColor();
                Console.Write($"]");
                Console.WriteLine();
            }
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
