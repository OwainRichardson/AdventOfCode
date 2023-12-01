using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_15_2
    {
        private static int _maxX = 0;
        private static int _maxY = 0;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day15.txt");

            Dictionary<string, Tuple<int, int>> map = ParseInputs(inputs);
            _maxX = (inputs[0].Length * 5) - 1;
            _maxY = (inputs.Length * 5) - 1;

            FindPaths(map);

            bool changes = true;
            while (changes)
            {
                changes = CheckMap(map);
            }

            Console.WriteLine(map.Last().Value.Item2);
        }

        private static bool CheckMap(Dictionary<string, Tuple<int, int>> map)
        {
            bool changes = false;

            for (int y = 0; y <= _maxY; y++)
            {
                for (int x = 0; x <= _maxX; x++)
                {
                    string currentCoordKey = $"{x},{y}";
                    var currentCoord = map[currentCoordKey];

                    string upCoordKey = $"{x},{y - 1}";
                    int upCoordRisk = map.ContainsKey(upCoordKey) ? map[upCoordKey]?.Item2 ?? 999999999 : 999999999;
                    string leftCoordKey = $"{x - 1},{y}";
                    int leftCoordRisk = map.ContainsKey(leftCoordKey) ? map[leftCoordKey]?.Item2 ?? 999999999 : 999999999;
                    string downCoordKey = $"{x},{y + 1}";
                    int downCoordRisk = map.ContainsKey(downCoordKey) ? map[downCoordKey]?.Item2 ?? 999999999 : 999999999;
                    string rightCoordKey = $"{x + 1},{y}";
                    int rightCoordRisk = map.ContainsKey(rightCoordKey) ? map[rightCoordKey]?.Item2 ?? 999999999 : 999999999;

                    var lowestRisk = 0;
                    if (x == 0 && y == 0) lowestRisk = 0;
                    else if (x == 0) lowestRisk = upCoordRisk < rightCoordRisk ? upCoordRisk + currentCoord.Item1 : rightCoordRisk;
                    else if (y == 0) lowestRisk = leftCoordRisk < downCoordRisk ? leftCoordRisk + currentCoord.Item1 : downCoordRisk;
                    else lowestRisk = new List<int> { upCoordRisk, leftCoordRisk, downCoordRisk, rightCoordRisk }.Min() + currentCoord.Item1;

                    if (lowestRisk != currentCoord.Item2)
                    {
                        map[currentCoordKey] = new Tuple<int, int>(currentCoord.Item1, lowestRisk);
                        changes = true;
                    }
                }
            }

            return changes;
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

                    map[currentCoordKey]  = new Tuple<int, int>(currentCoord.Item1, lowestRisk);
                }
            }
        }

        private static Dictionary<string, Tuple<int, int>> ParseInputs(string[] inputs)
        {
            Dictionary<string, Tuple<int, int>> coords = new Dictionary<string, Tuple<int, int>>();

            for (int yBoard = 0; yBoard < 5; yBoard++)
            {
                for (int xBoard = 0; xBoard < 5; xBoard++)
                {
                    int extra = yBoard + xBoard;

                    for (int y = 0; y < inputs.Length; y++)
                    {
                        for (int x = 0; x < inputs[0].Length; x++)
                        {
                            int value = int.Parse(inputs[y][x].ToString()) + extra > 9 ? int.Parse(inputs[y][x].ToString()) + extra - 9 : int.Parse(inputs[y][x].ToString()) + extra;

                            coords.Add($"{x + (xBoard * inputs[0].Length)},{y + (yBoard * inputs.Length)}", new Tuple<int, int>(value, 0));
                        }
                    }
                }
            }

            return coords;
        }
    }
}