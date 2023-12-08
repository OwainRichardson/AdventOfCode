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
    public static class D_06_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2018\Data\day06.txt").ToArray();

            List<Coord> coords = ParseInputsToCoords(inputs);

            AssignCoords(coords);
        }

        private static void AssignCoords(List<Coord> coords)
        {
            int minX = coords.Min(c => c.X) - 1;
            int minY = coords.Min(c => c.Y) - 1;

            int maxX = coords.Max(c => c.X) + 1;
            int maxY = coords.Max(c => c.Y) + 1;

            List<Coord> targetCoords = new List<Coord>();
            int maxTotalDistance = 10000;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    int totalDistance = 0;
                    foreach (Coord coord in coords)
                    {
                        int manhattanDistance = Math.Abs(coord.X - x) + Math.Abs(coord.Y - y);
                        totalDistance += manhattanDistance;
                    }

                    if (totalDistance < maxTotalDistance)
                    {
                        targetCoords.Add(new Coord { X = x, Y = y });
                    }
                }
            }

            Console.WriteLine(targetCoords.Count);
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