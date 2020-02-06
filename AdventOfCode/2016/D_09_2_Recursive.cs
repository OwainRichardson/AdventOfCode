using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_09_2
    {
        public static void Execute()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day09_full.txt");
            Dictionary<string, int?> memo = new Dictionary<string, int?>();

            long decompressedSize = 0;

            foreach (var input in inputs)
            {
                decompressedSize += Decompress(input, memo);
            }

            stopwatch.Stop();

            Console.Write($"\rThe decompressed of the file is ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(decompressedSize);
            Console.ResetColor();
            Console.Write($" chars");
            Console.WriteLine($"calculated in {stopwatch.Elapsed.TotalSeconds}");
            Console.WriteLine();
        }

        private static long Decompress(string input, Dictionary<string, int?> memo)
        {
            string pattern = @"\((\d+)x(\d+)\)";
            Regex regex = new Regex(pattern);

            long total = 0;

            // Do Regex
            Match match = regex.Match(input, 0);
            if (match.Success)
            {
                total += match.Index;

                int numberOfChars = int.Parse(match.Groups[1].Value);
                int repeatNumberOfTimes = int.Parse(match.Groups[2].Value);

                //updatedInput = regex.Replace(updatedInput, "", 1);
                string textToRepeat = input.Substring(match.Index, numberOfChars + match.Length);
                string theRest = input.Substring(match.Length + match.Index + numberOfChars);

                if (memo.ContainsKey(textToRepeat) && memo[textToRepeat] != null)
                {
                    total += memo[textToRepeat].Value + Decompress(theRest, memo);
                }
                else
                {
                    var expandedTextToRepeat = Expand(textToRepeat);

                    if (!regex.Match(expandedTextToRepeat).Success)
                    {
                        memo.Add(textToRepeat, expandedTextToRepeat.Count());
                        total += expandedTextToRepeat.Count() + Decompress(theRest, memo);
                    }
                    else
                    {
                        total += Decompress(Expand(textToRepeat), memo) + Decompress(theRest, memo);
                    }
                }
            }
            else
            {
                // ToDo: Something
                total = input.Count();
            }

            return total;
        }

        private static string Expand(string input)
        {
            string pattern = @"\((\d+)x(\d+)\)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input, 0);
            string updatedInput = string.Empty;

            int numberOfChars = int.Parse(match.Groups[1].Value);
            int repeatNumberOfTimes = int.Parse(match.Groups[2].Value);

            string textToRepeat = input.Substring(match.Index + match.Length, numberOfChars);

            for (int repeat = 1; repeat <= repeatNumberOfTimes; repeat++)
            {
                updatedInput = updatedInput.Insert(match.Index, textToRepeat);
            }

            return updatedInput;
        }
    }
}
