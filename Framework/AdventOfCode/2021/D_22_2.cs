using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_22_2
    {
        private static Dictionary<string, bool> _on = new Dictionary<string, bool>();
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day22.txt");

            foreach (string input in inputs)
            {
                if (input.StartsWith("on"))
                {
                    TurnLightsOn(input);
                }
                else
                {
                    TurnLightsOff(input);
                }
            }

            Console.WriteLine(_on.Count());
        }

        private static void TurnLightsOn(string input)
        {
            string pattern = @"^on\sx=(-?\d+)\.\.(-?\d+),y=(-?\d+)\.\.(-?\d+),z=(-?\d+)\.\.(-?\d+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);
            int minX = int.Parse(match.Groups[1].Value);
            int maxX = int.Parse(match.Groups[2].Value);
            int minY = int.Parse(match.Groups[3].Value);
            int maxY = int.Parse(match.Groups[4].Value);
            int minZ = int.Parse(match.Groups[5].Value);
            int maxZ = int.Parse(match.Groups[6].Value);

            for (int z = minZ; z <= maxZ; z++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        string lightKey = $"{z},{y},{x}";
                        if (!_on.ContainsKey(lightKey))
                        {
                            _on.Add(lightKey, true);
                        }
                    }
                }
            }
        }

        private static void TurnLightsOff(string input)
        {
            string pattern = @"^off\sx=(-?\d+)\.\.(-?\d+),y=(-?\d+)\.\.(-?\d+),z=(-?\d+)\.\.(-?\d+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);
            int minX = int.Parse(match.Groups[1].Value);
            int maxX = int.Parse(match.Groups[2].Value);
            int minY = int.Parse(match.Groups[3].Value);
            int maxY = int.Parse(match.Groups[4].Value);
            int minZ = int.Parse(match.Groups[5].Value);
            int maxZ = int.Parse(match.Groups[6].Value);

            for (int z = minZ; z <= maxZ; z++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    for (int x = minX; x <= maxX; x++)
                    {
                        string lightKey = $"{z},{y},{x}";
                        if (_on.ContainsKey(lightKey))
                        {
                            _on.Remove(lightKey);
                        }
                    }
                }
            }
        }
    }
}