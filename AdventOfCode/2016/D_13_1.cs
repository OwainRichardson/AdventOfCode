using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public class D_13_1
    {
        public static void Execute()
        {
            int input = 1362;
            string[,] map = PopulateMap(input);

            //PrintMap(map, input, 2, 2, 6, 5);

            List<int> paths = new List<int>();
            FindShortestPath(map, 1, 1, 31, 39, ref paths, input, 0);

            Console.WriteLine(paths.Min());
        }

        private static void FindShortestPath(string[,] map, int x, int y, int targetX, int targetY, ref List<int> paths, int input, int pathValue)
        {
            Process proc = Process.GetCurrentProcess();
            //Console.Write($"\r{proc.PrivateMemorySize64}          ");

            if (x < 0 || y < 0 || x >= input || y >= input)
            {
            }
            else if (map[y, x] == "#")
            {
            }
            else if (map[y, x] == "O")
            {
            }
            else if (x == targetX && y == targetY)
            {
                paths.Add(pathValue);
                //PrintMap(map, input, 1, 1, targetX, targetY);
            }
            else
            {
                map[y, x] = "O";
                string[,] tempUp = new string[150, 150];
                Array.Copy(map, tempUp, 150 * 150);
                FindShortestPath(tempUp, x, y - 1, targetX, targetY, ref paths, input, pathValue + 1);
                tempUp = new string[0, 0];

                string[,] tempLeft = new string[150, 150];
                Array.Copy(map, tempLeft, 150 * 150);
                FindShortestPath(tempLeft, x - 1, y, targetX, targetY, ref paths, input, pathValue + 1);
                tempLeft = new string[0, 0];

                string[,] tempDown = new string[150, 150];
                Array.Copy(map, tempDown, 150 * 150);
                FindShortestPath(tempDown, x, y + 1, targetX, targetY, ref paths, input, pathValue + 1);
                tempDown = new string[0, 0];

                string[,] tempRight = new string[150, 150];
                Array.Copy(map, tempRight, 150 * 150);
                FindShortestPath(tempRight, x + 1, y, targetX, targetY, ref paths, input, pathValue + 1);
                tempRight = new string[0, 0];
            }
        }

        private static void PrintMap(string[,] map, int input, int startX, int startY, int targetX, int targetY)
        {
            for (int y = 0; y < input; y++)
            {
                for (int x = 0; x < input; x++)
                {
                    if (x == startX && y == startY)
                    {
                        Console.Write("O");
                    }
                    else if (x == targetX && y == targetY)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write(map[y, x]);
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static string[,] PopulateMap(int input)
        {
            string[,] map = new string[150, 150];

            for (int y = 0; y < 150; y++)
            {
                for (int x = 0; x < 150; x++)
                {
                    int number = (x * x) + (3 * x) + (2 * x * y) + y + (y * y);
                    number += input;

                    string binary = Convert.ToString(number, 2);

                    if (binary.Count(b => b.ToString() == "1") % 2 == 0)
                    {
                        map[y, x] = " ";
                    }
                    else
                    {
                        map[y, x] = "#";
                    }
                }
            }

            return map;
        }
    }
}
