using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    public static class D_22_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day22_full.txt");
            string input = string.Join("", inputs);

            int width = inputs[0].Length;

            int half = (int)Math.Floor((double)width / 2);

            List<VirusCoord> virusCoords = PopulateVirusCoords(input, half, inputs);

            //PrintMap(virusCoords);

            int direction = ScannerDirections.Up;
            int x = 0;
            int y = 0;
            int count = 0;
            for (int step = 1; step <= 10000; step++)
            {
                VirusCoord virusCoord = virusCoords.FirstOrDefault(v => v.X == x && v.Y == y);
                if (virusCoord == null)
                {
                    virusCoord = new VirusCoord
                    {
                        X = x,
                        Y = y,
                        Infected = false
                    };
                    virusCoords.Add(virusCoord);
                }

                direction = ChangeDirection(virusCoord, direction);

                bool infected = !virusCoord.Infected;
                if (infected)
                {
                    count++;
                }
                virusCoord.Infected = infected;
                Move(direction, ref x, ref y);
                //PrintMap(virusCoords);
            }

            Console.WriteLine(count);
        }

        private static void Move(int direction, ref int x, ref int y)
        {
            switch (direction)
            {
                case ScannerDirections.Up:
                    y = y - 1;
                    break;
                case ScannerDirections.Right:
                    x = x + 1;
                    break;
                case ScannerDirections.Down:
                    y = y + 1;
                    break;
                case ScannerDirections.Left:
                    x = x - 1;
                    break;
                default:
                    throw new ArithmeticException();
            }
        }

        private static int ChangeDirection(VirusCoord virusCoord, int direction)
        {
            if (virusCoord.Infected)
            {
                switch (direction)
                {
                    case ScannerDirections.Up:
                        return ScannerDirections.Right;
                    case ScannerDirections.Right:
                        return ScannerDirections.Down;
                    case ScannerDirections.Down:
                        return ScannerDirections.Left;
                    case ScannerDirections.Left:
                        return ScannerDirections.Up;
                    default:
                        throw new ArithmeticException();
                }
            }
            else
            {
                switch (direction)
                {
                    case ScannerDirections.Up:
                        return ScannerDirections.Left;
                    case ScannerDirections.Right:
                        return ScannerDirections.Up;
                    case ScannerDirections.Down:
                        return ScannerDirections.Right;
                    case ScannerDirections.Left:
                        return ScannerDirections.Down;
                    default:
                        throw new ArithmeticException();
                }
            }
        }

        private static List<VirusCoord> PopulateVirusCoords(string input, int half, string[] inputs)
        {
            List<VirusCoord> virusCoords = new List<VirusCoord>();
            int index = 0;
            for (int y = -half; y <= half; y++)
            {
                for (int x = -half; x <= half; x++)
                {
                    VirusCoord virusCoord = new VirusCoord
                    {
                        X = x,
                        Y = y,
                        Infected = input[index].ToString().Equals("#")
                    };

                    virusCoords.Add(virusCoord);
                    index++;
                }
            }

            return virusCoords;
        }

        private static void PrintMap(List<VirusCoord> virusCoords)
        {
            int minX = virusCoords.Min(x => x.X);
            int minY = virusCoords.Min(x => x.Y);
            int maxX = virusCoords.Max(x => x.X);
            int maxY = virusCoords.Max(x => x.Y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    VirusCoord virusCoord = virusCoords.FirstOrDefault(v => v.X == x && v.Y == y);

                    if (virusCoord != null && virusCoord.Infected)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
