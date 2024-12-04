using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public static class D_04_2
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
                    if (wordsearch[x, y] == 'M')
                    {
                        countOfXmas += IsX_Mas(wordsearch, x, y, inputs.Length);
                    }
                }
            }

            Console.WriteLine(countOfXmas);
        }

        private static int IsX_Mas(char[,] wordsearch, int x, int y, int length)
        {
            int total = 0;

            // M.M
            // .A.
            // S.S
            if (x < length - 2 && y < length - 2 && wordsearch[x + 2, y] == 'M'
                    && $"{wordsearch[x, y]}{wordsearch[x + 1, y + 1]}{wordsearch[x + 2, y + 2]}" == "MAS"
                        && $"{wordsearch[x + 2, y]}{wordsearch[x + 1, y + 1]}{wordsearch[x, y + 2]}" == "MAS")
            {
                total += 1;
            }

            // M.S
            // .A.
            // M.S
            if (x < length - 2 && y < length - 2 && wordsearch[x, y + 2] == 'M'
                    && $"{wordsearch[x, y]}{wordsearch[x + 1, y + 1]}{wordsearch[x + 2, y + 2]}" == "MAS"
                        && $"{wordsearch[x, y + 2]}{wordsearch[x + 1, y + 1]}{wordsearch[x + 2, y]}" == "MAS")
            {
                total += 1;
            }

            // S.S
            // .A.
            // M.M
            if (x < length - 2 && y >= 2 && wordsearch[x + 2, y] == 'M'
                    && $"{wordsearch[x, y]}{wordsearch[x + 1, y - 1]}{wordsearch[x + 2, y - 2]}" == "MAS"
                        && $"{wordsearch[x + 2, y]}{wordsearch[x + 1, y - 1]}{wordsearch[x, y - 2]}" == "MAS")
            {
                total += 1;
            }

            // S.M
            // .A.
            // S.M
            if (x >= 2 && y < length - 2 && wordsearch[x, y + 2] == 'M'
                    && $"{wordsearch[x, y]}{wordsearch[x - 1, y + 1]}{wordsearch[x - 2, y + 2]}" == "MAS"
                        && $"{wordsearch[x, y + 2]}{wordsearch[x - 1, y + 1]}{wordsearch[x - 2, y]}" == "MAS")
            {
                total += 1;
            }

            return total;
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