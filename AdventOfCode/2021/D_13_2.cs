using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_13_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day13.txt");

            List<BoardCoord> coords = ParseCoords(inputs);

            List<FoldInstruction> foldInstructions = ParseFoldInstructions(inputs);

            coords = Fold(coords, foldInstructions);

            PrintCoords(coords);
        }

        private static void PrintCoords(List<BoardCoord> coords)
        {
            int minX = coords.Min(c => c.X);
            int maxX = coords.Max(c => c.X);
            int minY = coords.Min(c => c.Y);
            int maxY = coords.Max(c => c.Y);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (coords.Any(c => c.X == x && c.Y == y))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static List<BoardCoord> Fold(List<BoardCoord> coords, List<FoldInstruction> foldInstructions)
        {
            foreach (var foldInstruction in foldInstructions)
            {
                //PrintCoords(coords);

                switch (foldInstruction.Axis)
                {
                    case "y":
                        FoldY(coords, foldInstruction);
                        coords = DeDupeCoords(coords);
                        break;
                    case "x":
                        FoldX(coords, foldInstruction);
                        coords = DeDupeCoords(coords);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            return coords;
        }

        private static List<BoardCoord> DeDupeCoords(List<BoardCoord> coords)
        {
            List<BoardCoord> deDupedCoords = new List<BoardCoord>();

            foreach (BoardCoord coord in coords)
            {
                if (!deDupedCoords.Any(c => c.X == coord.X && c.Y == coord.Y))
                {
                    deDupedCoords.Add(coord);
                }
            }

            return deDupedCoords;
        }

        private static void FoldX(List<BoardCoord> coords, FoldInstruction foldInstruction)
        {
            List<BoardCoord> coordsBeforeLine = coords.Where(c => c.X > foldInstruction.Point).ToList();

            foreach (var coordBelowLine in coordsBeforeLine)
            {
                coordBelowLine.X = coordBelowLine.X - ((coordBelowLine.X - foldInstruction.Point) * 2);
            }
        }

        private static void FoldY(List<BoardCoord> coords, FoldInstruction foldInstruction)
        {
            List<BoardCoord> coordsBelowLine = coords.Where(c => c.Y > foldInstruction.Point).ToList();

            foreach (var coordBelowLine in coordsBelowLine)
            {
                coordBelowLine.Y = coordBelowLine.Y - ((coordBelowLine.Y - foldInstruction.Point) * 2);
            }
        }

        private static List<FoldInstruction> ParseFoldInstructions(string[] inputs)
        {
            List<FoldInstruction> foldInstructions = new List<FoldInstruction>();

            foreach (string input in inputs)
            {
                if (input.StartsWith("fold"))
                {
                    string instruction = input.Split(new string[] { "fold along " }, StringSplitOptions.RemoveEmptyEntries)[0];

                    string[] axisAndPoint = instruction.Split('=');

                    foldInstructions.Add(new FoldInstruction { Axis = axisAndPoint[0], Point = int.Parse(axisAndPoint[1]) });
                }
            }

            return foldInstructions;
        }

        private static List<BoardCoord> ParseCoords(string[] inputs)
        {
            List<BoardCoord> coords = new List<BoardCoord>();

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input)) break;

                string[] parts = input.Split(',');

                BoardCoord coord = new BoardCoord { X = int.Parse(parts[0]), Y = int.Parse(parts[1]) };

                coords.Add(coord);
            }

            return coords;
        }
    }
}
