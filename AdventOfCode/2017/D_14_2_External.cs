using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2017
{
    public static class D_14_2_External
    {
        public static void Execute()
        {
            string input = "stpzcrnm";
            Console.WriteLine(PartTwo(input));
        }

        public static string PartOne(string input)
        {
            var diskBits = GenerateGrid(input);
            var used = diskBits.ToList().Count(x => x);
            return used.ToString();
        }

        public static bool[,] GenerateGrid(string input)
        {
            var diskBits = new bool[128, 128];

            for (int row = 0; row < 128; row++)
            {
                var hash = D_10_2_External.KnotHashPartTwo($"{input}-{row}");
                var hashBits = hash.HexToBinary();

                for (var col = 0; col < 128; col++)
                {
                    diskBits[row, col] = hashBits[col] == '1';
                }
            }

            return diskBits;
        }

        public static string PartTwo(string input)
        {
            var diskBits = GenerateGrid(input);

            var groupCount = 0;

            diskBits.ForEach((row, col) =>
            {
                if (diskBits[row, col])
                {
                    var location = new Point(row, col);
                    RemoveBitGroup(location, diskBits);
                    groupCount++;
                }
            });

            return groupCount.ToString();
        }

        private static void RemoveBitGroup(Point location, bool[,] diskBits)
        {
            diskBits[location.X, location.Y] = false;

            foreach (var adjacent in location.GetNeighbors(includeDiagonals: false))
            {
                if (adjacent.X >= 0 && adjacent.X < 128 && adjacent.Y >= 0 && adjacent.Y < 128 && diskBits[adjacent.X, adjacent.Y])
                {
                    RemoveBitGroup(adjacent, diskBits);
                }
            }
        }

        public static IEnumerable<Point> GetNeighbors(this Point point, bool includeDiagonals)
        {
            var adjacentPoints = new List<Point>(8);

            adjacentPoints.Add(new Point(point.X - 1, point.Y));
            adjacentPoints.Add(new Point(point.X + 1, point.Y));
            adjacentPoints.Add(new Point(point.X, point.Y + 1));
            adjacentPoints.Add(new Point(point.X, point.Y - 1));

            if (includeDiagonals)
            {
                adjacentPoints.Add(new Point(point.X - 1, point.Y - 1));
                adjacentPoints.Add(new Point(point.X + 1, point.Y - 1));
                adjacentPoints.Add(new Point(point.X + 1, point.Y + 1));
                adjacentPoints.Add(new Point(point.X - 1, point.Y + 1));
            }

            return adjacentPoints;
        }

        public static void ForEach<T>(this T[,] a, Action<int, int> action)
        {
            for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
            {
                for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
                {
                    action(x, y);
                }
            }
        }

        public static IEnumerable<T> ToList<T>(this T[,] a)
        {
            for (var x = a.GetLowerBound(0); x <= a.GetUpperBound(0); x++)
            {
                for (var y = a.GetLowerBound(1); y <= a.GetUpperBound(1); y++)
                {
                    yield return a[x, y];
                }
            }
        }

        public static string HexToBinary(this string hex)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var c in hex.ToCharArray())
            {
                var intValue = int.Parse(c.ToString(), System.Globalization.NumberStyles.HexNumber);
                sb.Append(Convert.ToString(intValue, 2).PadLeft(4, '0'));
            }

            return sb.ToString();
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
