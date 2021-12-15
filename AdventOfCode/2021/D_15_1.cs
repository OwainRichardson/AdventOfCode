using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_15_1
    {
        private static int _maxX = 0;
        private static int _maxY = 0;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day15.txt");

            Dictionary<string, Tuple<int, int>> map = ParseInputs(inputs);
            _maxX = inputs[0].Length - 1;
            _maxY = inputs.Length - 1;

            FindPaths(map);

            Console.WriteLine(map.Last().Value.Item2);
        }

        private static void FindPaths(Dictionary<string, Tuple<int, int>> map)
        {
            for (int y = 0; y <= _maxY; y++)
            {
                for (int x = 0; x <= _maxX; x++)
                {
                    string currentCoordKey = $"{x},{y}";
                    var currentCoord = map[currentCoordKey];

                    string upCoordKey = $"{x},{y - 1}";
                    int upCoordRisk = map.ContainsKey(upCoordKey) ? map[upCoordKey]?.Item2 ?? 0 : 0;
                    string leftCoordkey = $"{x - 1},{y}";
                    int leftCoordRisk = map.ContainsKey(leftCoordkey) ? map[leftCoordkey]?.Item2 ?? 0 : 0;

                    var lowestRisk = 0;

                    if (x == 0 && y == 0) lowestRisk = 0;
                    else if (x == 0) lowestRisk = upCoordRisk + currentCoord.Item1;
                    else if (y == 0) lowestRisk = leftCoordRisk + currentCoord.Item1;
                    else lowestRisk = upCoordRisk < leftCoordRisk ? upCoordRisk + currentCoord.Item1 : leftCoordRisk + currentCoord.Item1;

                    map[currentCoordKey] = new Tuple<int, int>(currentCoord.Item1, lowestRisk);
                }
            }
        }

        private static Dictionary<string, Tuple<int, int>> ParseInputs(string[] inputs)
        {
            Dictionary<string, Tuple<int, int>> coords = new Dictionary<string, Tuple<int, int>>();

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[0].Length; x++)
                {
                    int value = int.Parse(inputs[y][x].ToString());

                    coords.Add($"{x},{y}", new Tuple<int, int>(value, 0));
                }
            }

            return coords;
        }
    }
}