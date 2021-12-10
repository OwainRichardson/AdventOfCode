using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_09_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day09.txt");

            List<HeightMapCoord> coords = ParseCoords(inputs);

            MarkLowPoints(coords);

            Console.WriteLine(coords.Where(x => x.lowPoint).Sum(x => x.Value + 1));
        }

        private static void MarkLowPoints(List<HeightMapCoord> coords)
        {
            foreach (HeightMapCoord coord in coords)
            {
                HeightMapCoord upCoord = coords.FirstOrDefault(c => c.X == coord.X && c.Y == coord.Y - 1);
                if (upCoord == null || coord.Value < upCoord.Value)
                {
                    HeightMapCoord downCoord = coords.FirstOrDefault(c => c.X == coord.X && c.Y == coord.Y + 1);

                    if (downCoord == null || coord.Value < downCoord.Value)
                    {
                        HeightMapCoord rightCoord = coords.FirstOrDefault(c => c.X == coord.X + 1 && c.Y == coord.Y);

                        if (rightCoord == null || coord.Value < rightCoord.Value)
                        {
                            HeightMapCoord leftCoord = coords.FirstOrDefault(c => c.X == coord.X - 1 && c.Y == coord.Y);

                            if (leftCoord == null || coord.Value < leftCoord.Value)
                            {
                                coord.lowPoint = true;
                            }
                        }
                    }
                }
            }
        }

        private static List<HeightMapCoord> ParseCoords(string[] inputs)
        {
            List<HeightMapCoord> coords = new List<HeightMapCoord>();
            int y = 0;

            foreach (string input in inputs)
            {
                int x = 0;
                foreach (char c in input)
                {
                    HeightMapCoord coord = new HeightMapCoord
                    {
                        X = x,
                        Y = y,
                        Value = int.Parse(c.ToString())
                    };

                    coords.Add(coord);
                    x += 1;
                }

                y += 1;
            }

            return coords;
        }
    }
}
