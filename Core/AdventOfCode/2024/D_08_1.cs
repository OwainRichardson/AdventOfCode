using AdventOfCode._2024.Models;
using AdventOfCode._2024.Models.Enums;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http.Headers;

namespace AdventOfCode._2024
{
    public static class D_08_1
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

                        Coord antinode2 = map.FirstOrDefault(c => c.X == characterCoord.X + xDistance && c.Y == characterCoord.Y + yDistance);
                        if (antinode2 != null)
                        {
                            antinode2.IsAntinode = true;
                        }
                    }
                }
            }

            Console.WriteLine(map.Count(c => c.IsAntinode));
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