using AdventOfCode._2020.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_17_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day17.txt");

            List<Dimension> dimensions = new List<Dimension>();
            Dimension startingDimension = ParseInputs(inputs);
            dimensions.Add(startingDimension);
            //PrintDimensions(dimensions);

            int cycles = 1;

            while (cycles <= 6)
            {
                dimensions = CalculateNextCycle(dimensions);

                //PrintDimensions(dimensions);

                cycles += 1;
            }

            Console.WriteLine(dimensions.Sum(x => x.Grid.Count(y => y.Value == "#")));
        }

        private static void PrintDimensions(List<Dimension> dimensions)
        {
            for (int z = dimensions.Min(x => x.Z); z <= dimensions.Max(x => x.Z); z++)
            {
                for (int w = dimensions.Min(x => x.W); w <= dimensions.Max(x => x.W); w++)
                {
                    PrintDimension(dimensions.Single(x => x.Z == z && x.W == w));
                }
            }
        }

        private static List<Dimension> CalculateNextCycle(List<Dimension> dimensions)
        {
            if (dimensions == null || !dimensions.Any()) return new List<Dimension>();

            List<Dimension> newDimensions = new List<Dimension>();

            int currentMinX = dimensions[0].Grid.Min(c => c.X);
            int currentMinY = dimensions[0].Grid.Min(c => c.Y);
            int newMinX = currentMinX - 1;
            int newMinY = currentMinY - 1;

            int currentMaxX = dimensions[0].Grid.Max(c => c.X);
            int currentMaxY = dimensions[0].Grid.Max(c => c.Y);
            int newMaxX = currentMaxX + 1;
            int newMaxY = currentMaxY + 1;

            foreach (var dimension in dimensions)
            {
                Dimension newDimension = new Dimension
                {
                    Z = dimension.Z,
                    W = dimension.W,
                    Grid = CalculateGrid(dimensions, dimension, newMinX, newMaxX, newMinY, newMaxY)
                };

                newDimensions.Add(newDimension);
            }

            for (int z = dimensions.Min(x => x.Z) - 1; z <= dimensions.Max(x => x.Z) + 1; z++)
            {
                for (int w = dimensions.Min(x => x.W) - 1; w <= dimensions.Max(x => x.W) + 1; w++)
                {
                    if (newDimensions.Any(d => d.Z == z && d.W == w)) continue;
                    
                    CheckNextDimension(newDimensions, dimensions, z, w);
                }
            }


            return newDimensions;
        }

        private static List<Coordinate> CalculateGrid(List<Dimension> dimensions, Dimension dimension, int newMinX, int newMaxX, int newMinY, int newMaxY)
        {
            List<Coordinate> newGrid = new List<Coordinate>();

            for (int y = newMinY; y <= newMaxY; y++)
            {
                for (int x = newMinX; x <= newMaxX; x++)
                {
                    string onOrOff = "";
                    Coordinate currentCoordinate = dimension?.Grid.SingleOrDefault(c => c.X == x && c.Y == y);
                    int numberOfNeighboursOn = dimensions.Where(d => d.Z >= dimension.Z - 1 && d.Z <= dimension.Z + 1 && d.W >= dimension.W - 1 && d.W <= dimension.W + 1).Sum(d => d.Grid.Count(c => c.X >= x - 1 && c.X <= x + 1 && c.Y >= y - 1 && c.Y <= y + 1 && c.Value == "#"));
                    if (currentCoordinate != null && currentCoordinate.Value == "#")
                    {
                        numberOfNeighboursOn -= 1;
                    }

                    if (currentCoordinate != null && currentCoordinate.Value == "#")
                    {
                        onOrOff = numberOfNeighboursOn == 2 ? "#" : numberOfNeighboursOn == 3 ? "#" : ".";
                    }
                    else
                    {
                        onOrOff = numberOfNeighboursOn == 3 ? "#" : ".";
                    }

                    newGrid.Add(new Coordinate { X = x, Y = y, Value = onOrOff });
                }
            }

            return newGrid;
        }

        private static void CheckNextDimension(List<Dimension> newDimensions, List<Dimension> dimensions, int z, int w)
        {
            int currentMinX = newDimensions[0].Grid.Min(c => c.X);
            int currentMinY = newDimensions[0].Grid.Min(c => c.Y);

            int currentMaxX = newDimensions[0].Grid.Max(c => c.X);
            int currentMaxY = newDimensions[0].Grid.Max(c => c.Y);

            Dimension newDimension = new Dimension { Z = z, W = w, Grid = CalculateGrid(dimensions, new Dimension { Z = z, W = w, Grid = new List<Coordinate>() }, currentMinX, currentMaxX, currentMinY, currentMaxY) };

            newDimensions.Add(newDimension);
        }

        private static void PrintDimension(Dimension dimension)
        {
            Console.WriteLine();
            Console.WriteLine($"z={dimension.Z} : w={dimension.W}");

            for (int y = dimension.Grid.Min(c => c.Y); y <= dimension.Grid.Max(c => c.Y); y++)
            {
                for (int x = dimension.Grid.Min(c => c.X); x <= dimension.Grid.Max(c => c.X); x++)
                {
                    Console.Write(dimension.Grid.Single(c => c.X == x && c.Y == y).Value);
                }

                Console.WriteLine();
            }
        }

        private static Dimension ParseInputs(string[] inputs)
        {
            Dimension dimension = new Dimension
            {
                Z = 0
            };

            int xLength = inputs[0].Length;
            int yLength = inputs.Count();

            dimension.Grid = new List<Coordinate>();

            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    dimension.Grid.Add(new Coordinate { Value = inputs[y][x].ToString(), X = x, Y = y });
                }
            }

            return dimension;
        }
    }
}