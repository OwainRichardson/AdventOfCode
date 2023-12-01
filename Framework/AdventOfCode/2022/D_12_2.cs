using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_12_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day12.txt").ToArray();

            List<Coordinate> map = ParseInput(inputs);

            FindShortestPath(map);

            List<Coordinate> aCoords = map.Where(c => c.Value == "a").ToList();
            int minPathLength = 0;

            Console.WriteLine(aCoords.Count);

            foreach(var aCoord in aCoords)
            {
                Console.Write($"\r{aCoords.IndexOf(aCoord)}");

                map.ForEach(x => x.PathLength = null);
                aCoord.PathLength = 0;

                FindShortestPath(map);

                int? pathLength = map.Single(c => c.Value == "E").PathLength;

                if (pathLength.HasValue && (pathLength.Value < minPathLength || minPathLength == 0))
                {
                    minPathLength = pathLength.Value;
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(minPathLength);
        }

        private static void FindShortestPath(List<Coordinate> map)
        {
            bool changes = true;

            while (changes)
            {
                changes = false;

                foreach (Coordinate coord in map)
                {
                    Coordinate upCoord = map.SingleOrDefault(m => m.X == coord.X && m.Y == coord.Y - 1);
                    Coordinate downCoord = map.SingleOrDefault(m => m.X == coord.X && m.Y == coord.Y + 1);
                    Coordinate leftCoord = map.SingleOrDefault(m => m.X == coord.X - 1 && m.Y == coord.Y);
                    Coordinate rightCoord = map.SingleOrDefault(m => m.X == coord.X + 1 && m.Y == coord.Y);

                    List<Coordinate> adjacentCoords = new List<Coordinate>();
                    if (upCoord != null && CanBeMovedTo(coord, upCoord)) adjacentCoords.Add(upCoord);
                    if (downCoord != null && CanBeMovedTo(coord, downCoord)) adjacentCoords.Add(downCoord);
                    if (leftCoord != null && CanBeMovedTo(coord, leftCoord)) adjacentCoords.Add(leftCoord);
                    if (rightCoord != null && CanBeMovedTo(coord, rightCoord)) adjacentCoords.Add(rightCoord);

                    if (adjacentCoords.All(ac => !ac.PathLength.HasValue)) continue;

                    int minPath = adjacentCoords.Where(c => c != null && c.PathLength.HasValue).Min(c => c.PathLength.Value) + 1;

                    if (coord.PathLength == null || minPath < coord.PathLength)
                    {
                        coord.PathLength = minPath;
                        changes = true;
                    }
                }

                //PrintMap(map);
            }
        }

        private static bool CanBeMovedTo(Coordinate coord, Coordinate coordToCheck)
        {
            if (coord.Value == "a" && coordToCheck.Value == "S") return true;
            if (coord.Value == "b" && coordToCheck.Value == "S") return true;
            if (coord.Value == "E" && coordToCheck.Value == "z") return true;
            if (coord.Value == "E" && coordToCheck.Value == "y") return true;
            if (coord.Value == "E" && coordToCheck.Value != "z") return false;

            int coordValue = coord.Value.ToCharArray().Single() % 32;
            int coordToCheckValue = coordToCheck.Value.ToCharArray().Single() % 32;

            if (coordValue <= coordToCheckValue + 1)
            {
                return true;
            }

            return false;
        }

        private static void PrintMap(List<Coordinate> map)
        {
            Console.WriteLine();

            int maxX = map.Max(c => c.X);
            int maxY = map.Max(c => c.Y);

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    Coordinate coord = map.Single(c => c.Y == y && c.X == x);

                    Console.Write(coord.PathLength.HasValue ? coord.PathLength.Value.ToString() : coord.Value);
                }

                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.WriteLine();
        }

        private static List<Coordinate> ParseInput(string[] inputs)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            int maxX = inputs[0].Length;
            int maxY = inputs.Length;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    Coordinate coord = new Coordinate
                    {
                        X = x,
                        Y = y,
                        Value = inputs[y][x].ToString()
                    };

                    if (coord.Value == "S") coord.Value = "a";

                    coordinates.Add(coord);
                }
            }

            return coordinates;
        }
    }
}