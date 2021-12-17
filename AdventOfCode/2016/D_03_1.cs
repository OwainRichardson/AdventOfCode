using AdventOfCode.Common;
using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2016
{
    public static class D_03_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day03_full.txt");

            int validTriangles = 0;
            foreach (var input in inputs)
            {
                int[] sides = ParseSides(input);

                var longestSide = sides.Max();

                int numberOfLongestSides = sides.Count(x => x == longestSide);
                if (numberOfLongestSides > 1)
                {
                    validTriangles++;
                }
                else
                {
                    var otherSides = sides.Where(x => x != longestSide);

                    if (otherSides.Sum() > longestSide)
                    {
                        validTriangles++;
                    }
                }
            }

            Console.Write($"Number of valid triangles: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(validTriangles);
            Console.ResetColor();
            Console.WriteLine();
        }

        private static int[] ParseSides(string input)
        {
            return input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        }
    }
}
