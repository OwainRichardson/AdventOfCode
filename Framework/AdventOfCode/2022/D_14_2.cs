using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_14_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day14.txt").ToArray();

            List<Coordinate> rocks = ParseInputs(inputs);

            DropSand(rocks);
        }

        private static void DropSand(List<Coordinate> rocks)
        {
            int sandX = 500;
            int sandY = 0;

            List<Coordinate> sand = new List<Coordinate>();
            bool sandAtSource = false;
            int maxRockY = rocks.Max(r => r.Y);

            while (!sandAtSource)
            { 
                bool sandSettled = false;
                Coordinate currentSand = new Coordinate { X = sandX, Y = sandY };

                while (!sandSettled)
                {
                    if (currentSand.Y == maxRockY + 1)
                    {
                        sand.Add(currentSand);
                        sandSettled = true;

                        continue;
                    }

                    if (!rocks.Any(r => r.X == currentSand.X && r.Y == currentSand.Y + 1) && !sand.Any(s => s.X == currentSand.X && s.Y == currentSand.Y + 1))
                    {
                        currentSand.Y += 1;
                        
                        continue;
                    }
                    else
                    {
                        // down left
                        if (!rocks.Any(r => r.X == currentSand.X - 1 && r.Y == currentSand.Y + 1) && !sand.Any(s => s.X == currentSand.X - 1 && s.Y == currentSand.Y + 1))
                        {
                            currentSand.X -= 1;
                            currentSand.Y += 1;

                            continue;
                        }

                        // down right
                        if (!rocks.Any(r => r.X == currentSand.X + 1 && r.Y == currentSand.Y + 1) && !sand.Any(s => s.X == currentSand.X + 1 && s.Y == currentSand.Y + 1))
                        {
                            currentSand.X += 1;
                            currentSand.Y += 1;

                            continue;
                        }

                        sand.Add(currentSand);
                        sandSettled = true;
                    }
                }

                if (sand.Any(s => s.X == sandX && s.Y == sandY))
                {
                    sandAtSource = true;
                }
            }

            //PrintCave(rocks, sand);

            Console.WriteLine(sand.Count);
        }

        private static void PrintCave(List<Coordinate> rocks, List<Coordinate> sand)
        {
            Console.WriteLine();

            int minX = rocks.Min(r => r.X);
            if (sand.Min(s => s.X) < minX)
            {
                minX = sand.Min(s => s.X);
            }
            int maxX = rocks.Max(r => r.X);
            if (sand.Max(s => s.X) > maxX)
            {
                maxX = sand.Max(s => s.X);
            }

            int minY = rocks.Min(r => r.Y);
            if (sand.Min(s => s.Y) < minY)
            {
                minY = sand.Min(s => s.Y);
            }
            int maxY = 10;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (rocks.Any(r => r.X == x && r.Y == y))
                    {
                        Console.Write("#");
                    }
                    else if (sand.Any(r => r.X == x && r.Y == y))
                    {
                        Console.Write("o");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }

        private static List<Coordinate> ParseInputs(string[] inputs)
        {
            List<Coordinate> coords = new List<Coordinate>();

            string pattern = @"(\d+)\,(\d+)";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                MatchCollection matches = regex.Matches(input);

                for (int index = 1; index < matches.Count; index++)
                {
                    Match first = matches[index - 1];
                    Match second = matches[index];

                    Coordinate firstCoord = new Coordinate { X = int.Parse(first.Groups[1].Value), Y = int.Parse(first.Groups[2].Value) };
                    Coordinate secondCoord = new Coordinate { X = int.Parse(second.Groups[1].Value), Y = int.Parse(second.Groups[2].Value) };

                    if (firstCoord.X == secondCoord.X)
                    {
                        if (firstCoord.Y > secondCoord.Y)
                        {
                            for (int y = secondCoord.Y; y <= firstCoord.Y; y++)
                            {
                                coords.Add(new Coordinate { X = firstCoord.X, Y = y });
                            }
                        }
                        else
                        {
                            for (int y = firstCoord.Y; y <= secondCoord.Y; y++)
                            {
                                coords.Add(new Coordinate { X = firstCoord.X, Y = y });
                            }
                        }
                    }
                    else
                    {
                        if (firstCoord.X > secondCoord.X)
                        {
                            for (int x = secondCoord.X; x <= firstCoord.X; x++)
                            {
                                coords.Add(new Coordinate { X = x, Y = firstCoord.Y });
                            }
                        }
                        else
                        {
                            for (int x = firstCoord.X; x <= secondCoord.X; x++)
                            {
                                coords.Add(new Coordinate { X = x, Y = firstCoord.Y });
                            }
                        }
                    }
                }
            }

            return coords;
        }
    }
}