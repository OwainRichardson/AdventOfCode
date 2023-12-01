using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_05_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day05.txt").ToArray();

            List<LineCoords> lineCoords = ParseInputs(inputs);

            CalculateCoords(lineCoords);
        }

        private static void CalculateCoords(List<LineCoords> lineCoords)
        {
            Dictionary<string, int> coords = new Dictionary<string, int>();

            foreach (LineCoords coord in lineCoords)
            {
                if (coord.IsHorizontal())
                {
                    if (coord.StartX > coord.EndX)
                    {
                        for (int index = coord.StartX; index >= coord.EndX; index--)
                        {
                            coords.TryAdd($"{index},{coord.StartY}");
                        }
                    }
                    else
                    {
                        for (int index = coord.StartX; index <= coord.EndX; index++)
                        {
                            coords.TryAdd($"{index},{coord.StartY}");
                        }
                    }
                }    
                else if (coord.IsVertical())
                {
                    if (coord.StartY > coord.EndY)
                    {
                        for (int index = coord.StartY; index >= coord.EndY; index--)
                        {
                            coords.TryAdd($"{coord.StartX},{index}");
                        }
                    }
                    else
                    {
                        for (int index = coord.StartY; index <= coord.EndY; index++)
                        {
                            coords.TryAdd($"{coord.StartX},{index}");
                        }
                    }
                }
                else
                {
                    if (coord.StartX > coord.EndX)
                    {
                        if (coord.StartY > coord.EndY)
                        {
                            int x = coord.StartX;
                            int y = coord.StartY;

                            while (y >= coord.EndY)
                            {
                                coords.TryAdd($"{x},{y}");

                                x--;
                                y--;
                            }
                        }
                        else
                        {
                            int x = coord.StartX;
                            int y = coord.StartY;

                            while (y <= coord.EndY)
                            {
                                coords.TryAdd($"{x},{y}");

                                x--;
                                y++;
                            }
                        }
                    }
                    else
                    {
                        if (coord.StartY > coord.EndY)
                        {
                            int x = coord.StartX;
                            int y = coord.StartY;

                            while (y >= coord.EndY)
                            {
                                coords.TryAdd($"{x},{y}");

                                x++;
                                y--;
                            }
                        }
                        else
                        {
                            int x = coord.StartX;
                            int y = coord.StartY;

                            while (y <= coord.EndY)
                            {
                                coords.TryAdd($"{x},{y}");

                                x++;
                                y++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(coords.Count(c => c.Value > 1));
        }

        private static List<LineCoords> ParseInputs(string[] inputs)
        {
            List<LineCoords> lineCoords = new List<LineCoords>();

            foreach (string input in inputs)
            {
                LineCoords lineCoord = new LineCoords();
                string[] coords = input.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);

                string[] startCoords = ParseCoords(coords[0]);
                string[] endCoords = ParseCoords(coords[1]);

                lineCoord.StartX = int.Parse(startCoords[0]);
                lineCoord.StartY = int.Parse(startCoords[1]);
                lineCoord.EndX = int.Parse(endCoords[0]);
                lineCoord.EndY = int.Parse(endCoords[1]);

                lineCoords.Add(lineCoord);
            }

            return lineCoords;
        }

        private static string[] ParseCoords(string coords)
        {
            return coords.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
