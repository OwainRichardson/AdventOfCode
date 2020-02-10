using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_18_2
    {
        private const string _safe = ".";
        private const string _trap = "^";

        public static void Execute()
        {
            var input = ".^.^..^......^^^^^...^^^...^...^....^^.^...^.^^^^....^...^^.^^^...^^^^.^^.^.^^..^.^^^..^^^^^^.^^^..^";
            int steps = 400000;
            string[,] map = new string[steps, input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                map[0, i] = input[i].ToString();
            }

            for (int i = 1; i < steps; i++)
            {
                GenerateNextRow(ref map, i, input.Length);
            }

            int total = 0;
            for (int r = 0; r < steps; r++)
            {
                for (int c = 0; c < input.Length; c++)
                {
                    if (map[r, c] == _safe)
                    {
                        total++;
                    }
                }
            }

            Console.WriteLine(total);
        }

        private static void GenerateNextRow(ref string[,] map, int row, int length)
        {
            for (int i = 0; i < length; i++)
            {
                string tileLeft = i == 0 ? "." : map[row - 1, i - 1];
                string tileCentre = map[row - 1, i];
                string tileRight = i == length - 1 ? "." : map[row - 1, i + 1];

                if (tileLeft == _trap && tileCentre == _trap && tileRight == _safe)
                {
                    map[row, i] = _trap;
                }
                else if (tileLeft == _safe && tileCentre == _trap && tileRight == _trap)
                {
                    map[row, i] = _trap;
                }
                else if (tileLeft == _trap && tileCentre == _safe && tileRight == _safe)
                {
                    map[row, i] = _trap;
                }
                else if (tileLeft == _safe && tileCentre == _safe && tileRight == _trap)
                {
                    map[row, i] = _trap;
                }
                else
                {
                    map[row, i] = _safe;
                }
            }
        }

        private static void PrintRow(string[,] map, int row, int rowLength)
        {
            for (int i = 0; i < rowLength; i++)
            {
                Console.Write(map[row, i]);
            }

            Console.WriteLine();
        }
    }
}
