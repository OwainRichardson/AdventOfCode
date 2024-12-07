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
    public static class D_07_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day07.txt").ToArray();
            long answer = 0;

            foreach (string input in inputs)
            {
                string[] split = input.Split(':', StringSplitOptions.RemoveEmptyEntries);
                long target = long.Parse(split[0]);

                List<int> values = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToList();

                int numberOfOptions = (int)Math.Pow(2, values.Count - 1);
                int lengthOfMaxString = Convert.ToString(numberOfOptions - 1, 2).Length;

                for (int i = 0; i < numberOfOptions; i++)
                {
                    string binary =  Convert.ToString(i, 2).PadLeft(lengthOfMaxString, '0');

                    int binaryIndex = 0;
                    long total = values[0];
                    for (int index = 1; index <= values.Count - 1; index++)
                    {
                        if (binary[binaryIndex] == '0')
                        {
                            total += values[index];
                        }
                        else if (binary[binaryIndex] == '1')
                        {
                            total *= values[index];
                        }

                        binaryIndex++;
                    }

                    if (total == target)
                    {
                        answer += target;
                        break;
                    }

                }
            }

            Console.WriteLine(answer);
        }

        private static (int x, int y) Move(Direction direction, int x, int y) => direction switch
        {
            Direction.Up => (x, y - 1),
            Direction.Right => (x + 1, y),
            Direction.Down => (x, y + 1),
            Direction.Left => (x - 1, y),
            _ => throw new InvalidOperationException()
        };

        private static Direction TurnRight(Direction direction) => direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new InvalidOperationException()
        };

        private static bool ObjectInFront(char[,] map, int x, int y, Direction direction, int length)
        {
            if (direction == Direction.Up && y - 1 >= 0 && map[x, y - 1] == '#')
            {
                return true;
            }

            if (direction == Direction.Right && x + 1 < length && map[x + 1, y] == '#')
            {
                return true;
            }

            if (direction == Direction.Down && y + 1 < length && map[x, y + 1] == '#')
            {
                return true;
            }

            if (direction == Direction.Left && x - 1 >= 0 && map[x - 1, y] == '#')
            {
                return true;
            }

            return false;
        }

        private static void DrawMap(char[,] map, int length)
        {
            Console.WriteLine();

            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        private static (char[,] map, int x, int y) ParseInputs(string[] inputs)
        {
            char[,] map = new char[inputs[0].Length, inputs.Length];

            int startX = -1;
            int startY = -1;

            int y = 0;
            foreach (string input in inputs)
            {
                int x = 0;

                foreach (char c in input)
                {
                    map[x, y] = c;

                    if (c == '^')
                    {
                        startX = x;
                        startY = y;
                    }

                    x++;
                }

                y++;
            }

            return (map, startX, startY);
        }
    }
}