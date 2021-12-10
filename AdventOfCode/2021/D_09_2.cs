using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_09_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day09.txt");

            List<HeightMapCoord> coords = ParseCoords(inputs);

            MarkLowPoints(coords);

            FindSizeOfBasins(coords);
        }

        private static void FindSizeOfBasins(List<HeightMapCoord> coords)
        {
            List<int> basinSize = new List<int>();

            foreach (HeightMapCoord lowPoint in coords.Where(x => x.lowPoint))
            {
                LinkedList<HeightMapCoord> basinCoords = new LinkedList<HeightMapCoord>();
                basinCoords.AddFirst(lowPoint);

                for (LinkedListNode<HeightMapCoord> node = basinCoords.First; node != null; node = node.Next)
                {
                    var currentNode = node.Value;

                    List<HeightMapCoord> higherCoords = CheckForHigherCoords(node.Value, coords);

                    foreach (HeightMapCoord higherCoord in higherCoords)
                    {
                        if (!basinCoords.Any(bc => bc.X == higherCoord.X && bc.Y == higherCoord.Y))
                        {
                            basinCoords.AddLast(higherCoord);
                        }
                    }
                }

                basinSize.Add(basinCoords.Count);
                basinCoords = new LinkedList<HeightMapCoord>();
            }

            Console.WriteLine(basinSize.OrderByDescending(bs => bs).Take(3).Product());
        }

        private static List<HeightMapCoord> CheckForHigherCoords(HeightMapCoord lowPoint, List<HeightMapCoord> coords)
        {
            List<HeightMapCoord> higherCoords = new List<HeightMapCoord>();

            HeightMapCoord upCoord = coords.FirstOrDefault(c => c.X == lowPoint.X && c.Y == lowPoint.Y - 1);
            if (upCoord != null && upCoord.Value != 9 && upCoord.Value > lowPoint.Value) higherCoords.Add(upCoord);

            HeightMapCoord downCoord = coords.FirstOrDefault(c => c.X == lowPoint.X && c.Y == lowPoint.Y + 1);
            if (downCoord != null && downCoord.Value != 9 && downCoord.Value > lowPoint.Value) higherCoords.Add(downCoord);

            HeightMapCoord rightCoord = coords.FirstOrDefault(c => c.X == lowPoint.X + 1 && c.Y == lowPoint.Y);
            if (rightCoord != null && rightCoord.Value != 9 && rightCoord.Value > lowPoint.Value) higherCoords.Add(rightCoord);

            HeightMapCoord leftCoord = coords.FirstOrDefault(c => c.X == lowPoint.X - 1 && c.Y == lowPoint.Y);
            if (leftCoord != null && leftCoord.Value != 9 && leftCoord.Value > lowPoint.Value) higherCoords.Add(leftCoord);

            return higherCoords;
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
