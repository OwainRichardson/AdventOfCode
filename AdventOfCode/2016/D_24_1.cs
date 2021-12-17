using AdventOfCode._2016.Models;
using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2016
{
    public static class D_24_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day24_full.txt");

            string[,] map = ParseInputs(inputs);
            int maxY = inputs.Count();
            int maxX = inputs[0].Length;
            PrintMap(map, maxX, maxY);
            int x = -1;
            int y = -1;
            FindStartPosition(map, maxX, maxY, out x, out y);
            List<int> paths = new List<int>();

            while (MapHasNumbers(map, maxX, maxY))
            {
                FindPaths(map, paths, x, y, maxX, maxY);
            }
        }

        private static void FindPaths(string[,] map, List<int> paths, int x, int y, int maxX, int maxY)
        {
            map[y, x] = ".";

            List<MapCoord> closestLetters = new List<MapCoord>();

            FindClosestLetters(map, x, y, maxX, maxY, ref closestLetters, 0);

            closestLetters = closestLetters.Where(c => c != null).ToList();

            foreach (MapCoord closestLetter in closestLetters)
            {
                FindPaths(map, paths, closestLetter.X, closestLetter.Y, maxX, maxY);
            }
        }

        private static MapCoord FindClosestLetters(string[,] map, int x, int y, int maxX, int maxY, ref List<MapCoord> coords, int distance, int direction = -1)
        {
            if (x < 0 || y < 0 || x >= maxX || y >= maxY || map[y, x] == "#")
            {
                return null;
            }

            if (map[y, x] != "#" && map[y, x] != "." && map[y, x] != "0")
            {
                return new MapCoord { X = x, Y = y, Distance = distance };
            }

            if (direction != ScannerDirections.Down)
            {
                coords.Add(FindClosestLetters(map, x, y - 1, maxX, maxY, ref coords, distance + 1, ScannerDirections.Up));
            }
            if (direction != ScannerDirections.Up)
            {
                coords.Add(FindClosestLetters(map, x, y + 1, maxX, maxY, ref coords, distance + 1, ScannerDirections.Down));
            }
            if (direction != ScannerDirections.Left)
            {
                coords.Add(FindClosestLetters(map, x - 1, y, maxX, maxY, ref coords, distance + 1, ScannerDirections.Right));
            }
            if (direction != ScannerDirections.Right)
            {
                coords.Add(FindClosestLetters(map, x + 1, y, maxX, maxY, ref coords, distance + 1, ScannerDirections.Left));
            }

            return null;
        }

        private static bool MapHasNumbers(string[,] map, int maxX, int maxY)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (map[y, x] != "." && map[y, x] != "#")
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void FindStartPosition(string[,] map, int maxX, int maxY, out int startX, out int startY)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    if (map[y, x] == "0")
                    {
                        startX = x;
                        startY = y;

                        return;
                    }
                }
            }

            startX = -1;
            startY = -1;
            return;
        }

        private static void PrintMap(string[,] map, int maxX, int maxY)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Console.Write(map[y, x]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static string[,] ParseInputs(string[] inputs)
        {
            int maxY = inputs.Count();
            int maxX = inputs[0].Length;

            string[,] map = new string[maxY, maxX];

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    map[y, x] = inputs[y][x].ToString();
                }
            }

            return map;
        }
    }
}
