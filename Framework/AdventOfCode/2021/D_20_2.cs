using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_20_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day20.txt");

            (string iea, int[,] map) = ParseInputs(inputs);

            //PrintMap(map);

            int emptyBit = 0;

            for (int step = 1; step <= 50; step++)
            {
                map = StepMap(map, iea, emptyBit);

                emptyBit = GetInfiniteVoidAlgoBit(emptyBit, iea);

                //PrintMap(map);
            }

            Console.WriteLine(CountLightBits(map));
        }

        private static int CountLightBits(int[,] map)
        {
            int on = 0;

            int maxY = map.GetLength(0);
            int maxX = map.GetLength(1);

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    on += map[y, x];
                }
            }

            return on;
        }

        private static void PrintMap(List<BoardCoord> map)
        {
            int maxY = map.Max(c => c.Y);
            int maxX = map.Max(c => c.X);
            int minY = map.Min(c => c.Y);
            int minX = map.Min(c => c.X);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Console.Write(map.Single(m => m.X == x && m.Y == y).Value == 1 ? "#" : ".");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
        }

        private static int[,] StepMap(int[,] map, string iea, int emptyBit)
        {
            int[,] newMap = new int[map.GetLength(0) + 2, map.GetLength(1) + 2];

            for (int y = 0; y < newMap.GetLength(0); y++)
            {
                for (int x = 0; x < newMap.GetLength(1); x++)
                {
                    newMap[y, x] = CalculateValue(map, x - 1, y - 1, iea, emptyBit);
                }
            }

            return newMap;
        }

        static int GetInfiniteVoidAlgoBit(int emptyBit, string iea)
        {
            int algoId = emptyBit == 0 ? 0 : (emptyBit << 9) - 1;
            return iea[algoId] == '.' ? 0 : 1;
        }

        private static int CalculateValue(int[,] map, int x, int y, string iea, int emptyBit)
        {
            int maxY = map.GetLength(0);
            int maxX = map.GetLength(1);

            int topLeftValue = y - 1 < 0 || x - 1 < 0 || y - 1 >= maxY || x - 1 >= maxX ? emptyBit : map[y - 1, x - 1];
            int topCentreValue = y - 1 < 0 || x < 0 || y - 1 >= maxY || x >= maxX ? emptyBit : map[y - 1, x];
            int topRightValue = y - 1 < 0 || x + 1 < 0 || y - 1 >= maxY || x + 1 >= maxX ? emptyBit : map[y - 1, x + 1];
            int leftValue = y < 0 || x - 1 < 0 || y >= maxY || x - 1 >= maxX ? emptyBit : map[y, x - 1];
            int centreValue = y < 0 || x < 0 || y >= maxY || x >= maxX ? emptyBit : map[y, x];
            int rightValue = y < 0 || x + 1 < 0 || y >= maxY || x + 1 >= maxX ? emptyBit : map[y, x + 1];
            int bottomLeftValue = y + 1 < 0 || x - 1 < 0 || y + 1 >= maxY || x - 1 >= maxX ? emptyBit : map[y + 1, x - 1];
            int bottomCentreValue = y + 1 < 0 || x < 0 || y + 1 >= maxY || x >= maxX ? emptyBit : map[y + 1, x];
            int bottomRightValue = y + 1 < 0 || x + 1 < 0 || y + 1 >= maxY || x + 1 >= maxX ? emptyBit : map[y + 1, x + 1];

            string binary = $"{topLeftValue}{topCentreValue}{topRightValue}{leftValue}{centreValue}{rightValue}{bottomLeftValue}{bottomCentreValue}{bottomRightValue}";
            int index = Convert.ToInt32(binary, 2);

            return iea[index] == '.' ? 0 : 1;
        }

        private static (string, int[,]) ParseInputs(string[] inputs)
        {
            int[,] map = new int[inputs.Length, inputs[2].Length];

            for (int y = 2; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[3].Length; x++)
                {
                    map[y, x] = inputs[y][x] == '.' ? 0 : 1;
                }
            }

            return (inputs[0], map);
        }
    }
}