using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_14_2_Incomplete
    {
        public static void Execute()
        {
            string input = "flqrgnkx";
            string[,] map = new string[128, 128];

            for (int i = 0; i < 128; i++)
            {
                string row = CalculateRow(input, i).Replace("1", "#");

                int col = 0;
                foreach (Char c in row)
                {
                    map[i, col] = c.ToString();

                    col++;
                }
            }

            NumberGroups(map);

            var owain = 4;
        }

        private static void NumberGroups(string[,] map)
        {
            int groupNo = 1;

            // Take a row and make it a string
            for (int row = 0; row < 127; row++)
            {
                for (int col = 0; col < 127; col++)
                {
                    if (map[row, col] == "#")
                    {
                        NumberGroup(map, row, col, groupNo);

                        if (!CheckMap(map))
                        {
                            throw new Exception($"Map isn't right for group {groupNo}");
                        }

                        //PrintMap(map);

                        Console.WriteLine(groupNo);

                        groupNo++;
                    }
                }
            }

            PrintMap(map);

            Console.WriteLine(groupNo - 1);
        }

        private static bool CheckMap(string[,] map)
        {
            for (int row = 0; row < 127; row++)
            {
                for (int col = 0; col < 127; col++)
                {
                    if (map[row, col] == "#")
                    {
                        // Up
                        if (row - 1 >= 0)
                        {
                            if (map[row - 1, col] != "#" && map[row - 1, col] != "0")
                            {
                                return false;
                            }
                        }

                        // Left
                        if (col - 1 >= 0)
                        {
                            if (map[row, col - 1] != "#" && map[row, col - 1] != "0")
                            {
                                return false;
                            }
                        }

                        // Right
                        if (col + 1 <= 127)
                        {
                            if (map[row, col + 1] != "#" && map[row, col + 1] != "0")
                            {
                                return false;
                            }
                        }

                        // Down
                        if (row + 1 <= 127)
                        {
                            if (map[row + 1, col] != "#" && map[row + 1, col] != "0")
                            {
                                return false;
                            }
                        }
                    }

                }
            }

            return true;
        }

        private static void PrintMap(string[,] map)
        {
            for (int row = 0; row < 127; row++)
            {
                for (int col = 0; col < 127; col++)
                {
                    Console.Write(map[row, col].Replace("0", "."));
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void NumberGroup(string[,] map, int row, int col, int groupNo)
        {
            if (row == 128 || col == 128 || row == -1 || col == -1)
            {
                return;
            }

            if (map[row, col] == "0")
            {
                throw new ArgumentException();
            }

            map[row, col] = $"{groupNo}";

            // Up
            if (row - 1 >= 0)
            {
                if (map[row - 1, col] == "#")
                {
                    NumberGroup(map, row - 1, col, groupNo);
                }
            }

            // Left
            if (col - 1 >= 0)
            {
                if (map[row, col - 1] == "#")
                {
                    NumberGroup(map, row, col - 1, groupNo);
                }
            }

            // Right
            if (col + 1 <= 127)
            {
                if (map[row, col + 1] == "#")
                {
                    NumberGroup(map, row, col + 1, groupNo);
                }
            }

            // Down
            if (row + 1 <= 127)
            {
                if (map[row + 1, col] == "#")
                {
                    NumberGroup(map, row + 1, col, groupNo);
                }
            }
        }

        private static string CalculateRow(string input, int rowNumber)
        {
            string key = $"{input}-{rowNumber}";

            string hash = D_10_2_External.KnotHashPartTwo(key);

            StringBuilder binary = new StringBuilder();
            foreach (Char c in hash)
            {
                string bin = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2);

                if (bin.Length < 4)
                {
                    while (bin.Length < 4)
                    {
                        bin = $"0{bin}";
                    }
                }

                binary.Append(bin);
            }

            return binary.ToString();
        }
    }
}
