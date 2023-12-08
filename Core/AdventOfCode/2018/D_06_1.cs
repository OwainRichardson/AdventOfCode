using AdventOfCode._2018.Models;
using AdventOfCode._2018.Models.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    public static class D_06_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2018\Data\day06.txt").ToArray();

            List<Coord> coords = ParseInputsToCoords(inputs);

            //PlotCoords(coords);

            AssignCoords(coords);
        }

        private static void AssignCoords(List<Coord> coords)
        {
            int minX = coords.Min(c => c.X) - 1;
            int minY = coords.Min(c => c.Y) - 1;

            int maxX = coords.Max(c => c.X) + 1;
            int maxY = coords.Max(c => c.Y) + 1;

            List<Coord> assignedCoords = new List<Coord>();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    int smallestDistance = 999;
                    List<int> closestIds = new List<int>();
                    foreach (Coord coord in coords)
                    {
                        int manhattanDistance = Math.Abs(coord.X - x) + Math.Abs(coord.Y - y);
                        if (manhattanDistance < smallestDistance)
                        {
                            smallestDistance = manhattanDistance;
                            closestIds = new List<int> { coord.Id };
                        }
                        else if (manhattanDistance == smallestDistance)
                        {
                            closestIds.Add(coord.Id);
                        }
                    }

                    if (x == minX || x == maxX || y == minY || y == maxY)
                    {
                        if (closestIds.Count == 1)
                        {
                            Coord closestCoord = coords.First(c => c.Id == closestIds.Single());
                            closestCoord.IsInfinite = true;
                        }
                    }

                    if (closestIds.Count == 1)
                    {
                        Coord closestCoord = coords.First(c => c.Id == closestIds.Single());
                        assignedCoords.Add(new Coord { X = x, Y = y, Id = closestCoord.Id });
                    }
                }
            }

            List<Coord> nonInfiniteCoords = coords.Where(c => !c.IsInfinite).ToList();

            int largestNonInfiniteCoords = 0;
            foreach (Coord coord in nonInfiniteCoords)
            {
                int numberOfCoords = assignedCoords.Count(c => c.Id == coord.Id);
                if (numberOfCoords > largestNonInfiniteCoords)
                {
                    largestNonInfiniteCoords = numberOfCoords;
                }
            }

            Console.WriteLine(largestNonInfiniteCoords);
        }

        private static void PlotCoords(List<Coord> coords)
        {
            int minX = coords.Min(c => c.X) - 1;
            int minY = coords.Min(c => c.Y) - 1;

            int maxX = coords.Max(c => c.X) + 1;
            int maxY = coords.Max(c => c.Y) + 1;

            Console.WriteLine();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Coord coord = coords.FirstOrDefault(c => c.X == x && c.Y == y);
                    if (coord != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(coord.Id);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static List<Coord> ParseInputsToCoords(string[] inputs)
        {
            List<Coord> coords = new List<Coord>();

            int index = 0;
            foreach (string input in inputs)
            {
                int[] splitCoords = input.Split(", ").Select(i => int.Parse(i)).ToArray();

                Coord coord = new Coord
                {
                    Id = index,
                    X = splitCoords[0],
                    Y = splitCoords[1]
                };

                coords.Add(coord);

                index++;
            }

            return coords;
        }
    }
}