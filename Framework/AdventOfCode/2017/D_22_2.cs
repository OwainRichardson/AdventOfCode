using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    public static class D_22_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day22_full.txt");
            string input = string.Join("", inputs);

            int width = inputs[0].Length;

            int half = (int)Math.Floor((double)width / 2);

            Dictionary<string, char> virusCoords = PopulateVirusCoords(input, half, inputs);

            int direction = ScannerDirections.Up;
            int x = 0;
            int y = 0;
            int count = 0;
            for (int step = 1; step <= 10000000; step++)
            {
                if (step % 1000 == 0)
                {
                    Console.Write($"\r{step}     ");
                }

                if (!virusCoords.ContainsKey($"{x},{y}"))
                {
                    virusCoords.Add($"{x},{y}", 'c');
                }

                direction = ChangeDirection(virusCoords[$"{x},{y}"], direction);

                char state = CalcualteInfectedStatus(virusCoords[$"{x},{y}"]);
                if (state == 'i')
                {
                    count++;
                }
                virusCoords[$"{x},{y}"] = state;

                Move(direction, ref x, ref y);
            }

            Console.Write($"\r{count}       ");
            Console.WriteLine();
        }

        private static char CalcualteInfectedStatus(char state)
        {
            if (state == 'i')
            {
                return 'f';
            }
            else if (state == 'c')
            {
                return 'w';
            }
            else if (state == 'w')
            {
                return 'i';
            }
            else if (state == 'f')
            {
                return 'c';
            }

            throw new ArithmeticException();
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

        private static int ChangeDirection(char state, int direction)
        {
            if (state == 'i')
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
            else if (state == 'w')
            {
                return direction;
            }
            else if (state == 'f')
            {
                switch (direction)
                {
                    case ScannerDirections.Up:
                        return ScannerDirections.Down;
                    case ScannerDirections.Right:
                        return ScannerDirections.Left;
                    case ScannerDirections.Down:
                        return ScannerDirections.Up;
                    case ScannerDirections.Left:
                        return ScannerDirections.Right;
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

        private static Dictionary<string, char> PopulateVirusCoords(string input, int half, string[] inputs)
        {
            Dictionary<string, char> virusCoords = new Dictionary<string, char>();
            int index = 0;
            for (int y = -half; y <= half; y++)
            {
                for (int x = -half; x <= half; x++)
                {
                    char state = input[index].ToString().Equals("#") ? 'i' : 'c';

                    virusCoords.Add($"{x},{y}", state);
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
