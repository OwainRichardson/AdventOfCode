using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public static class D_04_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day04.txt").ToArray();

            char[,] wordsearch = ParseInputs(inputs);

            //PrintWordsearch(wordsearch, inputs.Length);

            int countOfXmas = 0;

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs.Length; x++)
                {
                    if (wordsearch[x, y] == 'X')
                    {
                        countOfXmas += IsXmas(wordsearch, x, y, inputs.Length);
                    }
                }
            }

            Console.WriteLine(countOfXmas);
        }

        private static int IsXmas(char[,] wordsearch, int x, int y, int length)
        {
            List<string> stringsToCheck = new List<string>();

            if (x >= 3)
            {
                // Left
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x - 1, y]}{wordsearch[x - 2, y]}{wordsearch[x - 3, y]}");
            }

            if (x >= 3 && y >= 3)
            {
                // Left Up Diagonal
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x - 1, y - 1]}{wordsearch[x - 2, y - 2]}{wordsearch[x - 3, y - 3]}");
            }

            if (y >= 3)
            {
                // Up
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x, y - 1]}{wordsearch[x, y - 2]}{wordsearch[x, y - 3]}");
            }

            if (y >= 3 && x < length - 3)
            {
                // Right Up Diagonal
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x + 1, y - 1]}{wordsearch[x + 2, y - 2]}{wordsearch[x + 3, y - 3]}");
            }

            if (x < length - 3)
            {
                // Right
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x + 1, y]}{wordsearch[x + 2, y]}{wordsearch[x + 3, y]}");
            }

            if (x < length - 3 && y < length - 3)
            {
                // Right Down Diagonal
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x + 1, y + 1]}{wordsearch[x + 2, y + 2]}{wordsearch[x + 3, y + 3]}");
            }

            if (y < length - 3)
            {
                // Down 
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x, y + 1]}{wordsearch[x, y + 2]}{wordsearch[x, y + 3]}");
            }

            if (x >= 3 && y < length - 3)
            {
                // Down Left Diagonal
                stringsToCheck.Add($"{wordsearch[x, y]}{wordsearch[x - 1, y + 1]}{wordsearch[x - 2, y + 2]}{wordsearch[x - 3, y + 3]}");
            }

            return stringsToCheck.Count(s => s == "XMAS");
        }

        private static void PrintWordsearch(char[,] wordsearch, int length)
        {
            Console.WriteLine();

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Console.Write(wordsearch[x, y]);
                }

                Console.WriteLine();
            }
        }

        private static char[,] ParseInputs(string[] inputs)
        {
            char[,] wordsearch = new char[inputs[0].Length, inputs.Length];

            int yIndex = 0;

            foreach (string input in inputs)
            {
                int xIndex = 0;

                foreach (char c in input)
                {
                    wordsearch[xIndex, yIndex] = c;

                    xIndex++;
                }

                yIndex++;
            }

            return wordsearch;
        }
    }
}