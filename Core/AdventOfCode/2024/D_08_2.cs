﻿using AdventOfCode._2024.Models;
using AdventOfCode._2024.Models.Enums;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http.Headers;

namespace AdventOfCode._2024
{
    public static class D_08_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day08.txt").ToArray();

            List<Coord> map = ParseInputs(inputs);

            List<string> uniqueCharacters = map.Where(c => c.Value != ".").Select(c => c.Value).Distinct().ToList();

            foreach (string character in uniqueCharacters)
            {
                List<Coord> characterCoords = map.Where(c => c.Value == character).ToList();

                foreach (Coord characterCoord in characterCoords)
                {
                    List<Coord> otherCoords = characterCoords.Where(c => c.Id != characterCoord.Id).ToList();

                    foreach (Coord otherCoord in otherCoords)
                    {
                        int xDistance = characterCoord.X - otherCoord.X;
                        int yDistance = characterCoord.Y - otherCoord.Y;

                        Coord antinode1 = map.FirstOrDefault(c => c.X == otherCoord.X - xDistance && c.Y == otherCoord.Y - yDistance);
                        if (antinode1 != null)
                        {
                            antinode1.IsAntinode = true;
                        }

                        while (antinode1 != null)
                        {
                            antinode1 = map.FirstOrDefault(c => c.X == antinode1.X - xDistance && c.Y == antinode1.Y - yDistance);
                            if (antinode1 != null)
                            {
                                antinode1.IsAntinode = true;
                            }
                        }

                        Coord antinode2 = map.FirstOrDefault(c => c.X == characterCoord.X + xDistance && c.Y == characterCoord.Y + yDistance);
                        if (antinode2 != null)
                        {
                            antinode2.IsAntinode = true;
                        }

                        while (antinode2 != null)
                        {
                            antinode2 = map.FirstOrDefault(c => c.X == antinode2.X + xDistance && c.Y == antinode2.Y + yDistance);
                            if (antinode2 != null)
                            {
                                antinode2.IsAntinode = true;
                            }
                        }
                    }
                }
            }

            int total = map.Count(c => c.IsAntinode);

            foreach (string unique in uniqueCharacters)
            {
                List<Coord> coords = map.Where(c => c.Value == unique).ToList();
                if (coords.Count > 1) 
                {
                    total += coords.Count(c => !c.IsAntinode);
                }
            }

            Console.WriteLine(total);
        }

        private static void DrawMap(List<Coord> map, int length)
        {
            Console.WriteLine();

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Coord mapCoord = map.First(c => c.X == x && c.Y == y);
                    if (mapCoord.Value != ".") Console.Write(mapCoord.Value);
                    else if (mapCoord.IsAntinode) Console.Write("#");
                    else Console.Write(mapCoord.Value);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static List<Coord> ParseInputs(string[] inputs)
        {
            List<Coord> map = new List<Coord>();

            int y = 0;
            int id = 0;

            foreach (string input in inputs)
            {
                int x = 0;

                foreach (char c in input)
                {
                    map.Add(new Coord { X = x, Y = y, Value = c.ToString(), IsAntinode = false, Id = id });

                    id++;
                    x++;
                }

                y++;
            }

            return map;
        }
    }
}