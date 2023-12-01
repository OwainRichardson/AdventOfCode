using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2016
{
    public static class D_17_1
    {
        private const int UP = 0;
        private const int DOWN = 1;
        private const int LEFT = 2;
        private const int RIGHT = 3;

        public static void Execute()
        {
            var input = "yjjvjgan";
            string path = string.Empty;
            List<string> paths = new List<string>();

            int[,] map = new int[4, 4];
            int x = 0;
            int y = 0;

            EscapeMaze(map, x, y, input, path, ref paths);

            Console.WriteLine(paths.Min(p => p.Length));
        }

        private static void EscapeMaze(int[,] map, int x, int y, string input, string path, ref List<string> paths)
        {
            if (paths.Any() && path.Length > paths.Min(p => p.Length))
            {
                return;
            }

            if (x == 3 && y == 3)
            {
                paths.Add(path);
                return;
            }

            string hash = CalculateMD5Hash($"{input}{path}");
            bool atLeastOneOpenDoor = false;

            for (int i = 0; i <= 3; i++)
            {
                if (char.IsLetter(hash[i]) && hash[i] != 'a' && hash[i] != 'A')
                {
                    atLeastOneOpenDoor = true;
                }
            }

            if (!atLeastOneOpenDoor)
            {
                return;
            }

            if (char.IsLetter(hash[UP]) && hash[UP] != 'a' && hash[UP] != 'A')
            {
                if (y - 1 >= 0)
                {
                    EscapeMaze(map, x, y - 1, input, $"{path}U", ref paths);
                }
            }
            if (char.IsLetter(hash[DOWN]) && hash[DOWN] != 'a' && hash[DOWN] != 'A')
            {
                if (y + 1 <= 3)
                {
                    EscapeMaze(map, x, y + 1, input, $"{path}D", ref paths);
                }
            }
            if (char.IsLetter(hash[LEFT]) && hash[LEFT] != 'a' && hash[LEFT] != 'A')
            {
                if (x - 1 >= 0)
                {
                    EscapeMaze(map, x - 1, y, input, $"{path}L", ref paths);
                }
            }
            if (char.IsLetter(hash[RIGHT]) && hash[RIGHT] != 'a' && hash[RIGHT] != 'A')
            {
                if (x + 1 <= 3)
                {
                    EscapeMaze(map, x + 1, y, input, $"{path}R", ref paths);
                }
            }
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
