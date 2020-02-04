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
    public static class D_03_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day03_full.txt");

            var possibleTriangles = ParseInputsToTriangles(inputs);

            int validTriangles = 0;
            foreach (var input in possibleTriangles)
            {
                int[] sides = input;

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

        private static List<int[]> ParseInputsToTriangles(string[] inputs)
        {
            List<int[]> triangles = new List<int[]>();

            for (int i = 0; i < inputs.Length; i = i + 3)
            {
                int[] row1 = inputs[i].Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                int[] row2 = inputs[i + 1].Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                int[] row3 = inputs[i + 2].Trim().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                triangles.Add(new int[]
                {
                    row1[0],
                    row2[0],
                    row3[0]
                });
                triangles.Add(new int[]
                {
                    row1[1],
                    row2[1],
                    row3[1]
                });
                triangles.Add(new int[]
                {
                    row1[2],
                    row2[2],
                    row3[2]
                });

            }

            return triangles;
        }

        private static int[] ParseSides(string input)
        {
            return input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
        }
    }
}
