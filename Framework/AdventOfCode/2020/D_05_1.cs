using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_05_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day05.txt");

            List<int> seatIds = new List<int>();

            foreach (string input in inputs)
            {
                seatIds.Add(CalculateSeatId(input.ToLower()));
            }

            Console.WriteLine(seatIds.Max());
        }

        private static int CalculateSeatId(string input)
        {
            int lowRow = 0;
            int highRow = 127;

            foreach (Char character in input.Take(7))
            {
                switch (character)
                {
                    case 'f':
                        highRow -= (int)Math.Ceiling((decimal)(highRow - lowRow) / 2);
                        break;
                    case 'b':
                        lowRow += (int)Math.Ceiling((decimal)(highRow - lowRow) / 2);
                        break;
                    default:
                        throw new Exception();
                }
            }

            if (highRow != lowRow)
            {
                throw new Exception();
            }

            int seatRow = highRow;

            int lowColumn = 0;
            int highColumn = 7;

            foreach (Char character in input.ToLower().Skip(7))
            {
                switch (character)
                {
                    case 'l':
                        highColumn -= (int)Math.Ceiling((decimal)(highColumn - lowColumn) / 2);
                        break;
                    case 'r':
                        lowColumn += (int)Math.Ceiling((decimal)(highColumn - lowColumn) / 2);
                        break;
                    default:
                        throw new Exception();
                }
            }

            if (highColumn != lowColumn)
            {
                throw new Exception();
            }

            int seatColumn = highColumn;

            return (seatRow * 8) + seatColumn;
        }
    }
}
