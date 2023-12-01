using AdventOfCode.Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    public static class D_09_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day09_full.txt");

            int decompressedSize = 0;

            foreach (var input in inputs)
            {
                decompressedSize += Decompress(input);
            }

            Console.Write($"The decompressed of the file is ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(decompressedSize);
            Console.ResetColor();
            Console.Write($" chars");
            Console.WriteLine();
        }

        private static int Decompress(string input)
        {
            string pattern = @"\((\d+)x(\d+)\)";
            Regex regex = new Regex(pattern);
            int total = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].ToString() == "(")
                {
                    // Do Regex
                    Match match = regex.Match(input, i);
                    int numberOfChars = int.Parse(match.Groups[1].Value);
                    int repeatNumberOfTimes = int.Parse(match.Groups[2].Value);

                    total += (numberOfChars * repeatNumberOfTimes);

                    i = input.IndexOf(")", i + 1) + numberOfChars;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(input[i].ToString()))
                    {
                        total++;
                    }
                }
            }

            return total;
        }
    }
}
