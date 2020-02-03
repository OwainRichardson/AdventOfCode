using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_25_1
    {
        public static void Execute()
        {
            List<DiagonalCoord> coords = new List<DiagonalCoord>();
            var x = 1;
            var y = 1;

            while (true)
            {
                long previousValue = 20151125;
                if (coords.Any())
                {
                    previousValue = coords.Last().Presents;
                }

                if (!coords.Any())
                {
                    coords.Add(new DiagonalCoord { X = x, Y = y, Presents = previousValue });
                }
                else
                {
                    coords.Add(new DiagonalCoord { X = x, Y = y, Presents = CalculateValue(previousValue) });
                }

                if (x == 3083 && y == 2978)
                {
                    break;
                }

                IncrementXandY(ref x, ref y, coords);
            }

            //PrintCoords(coords);

            Console.Write($"Value at row 2978, column 3083 = ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(coords.First(c => c.X == 3083 && c.Y == 2978).Presents);
            Console.ResetColor();
            Console.WriteLine();
        }

        private static long CalculateValue(long previousValue)
        {
            long mult = previousValue * 252533;

            return mult % 33554393;
        }

        private static void PrintCoords(List<DiagonalCoord> coords)
        {
            for (int y = 1; y <= coords.Max(z => z.Y); y++)
            {
                for (int x = 1; x <= coords.Max(z => z.X); x++)
                {
                    var value = coords.FirstOrDefault(c => c.X == x && c.Y == y);

                    if (value != null)
                    {
                        //Console.Write(value.Presents);
                        //Console.Write("\t");
                    }
                }

                //Console.WriteLine();
            }
        }

        private static void IncrementXandY(ref int x, ref int y, List<DiagonalCoord> coords)
        {
            if (y - 1 < 1)
            {
                y = coords.Max(c => c.Y) + 1;
                //Console.WriteLine($"y = {y}");
                x = 1;
            }
            else
            {
                y -= 1;
                x++;
            }
        }
    }
}