using AdventOfCode.Common;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_03_1
    {
        private const char TREE = '#';

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day03.txt");
            int x = 0;
            int y = 0;
            int width = inputs[0].Length;
            int height = inputs.Count();
            int movementX = 3;
            int movementY = 1;
            int numberOfTrees = 0;

            while (y < height)
            {
                if (x >= width)
                {
                    x -= width;
                }

                if (inputs[y][x] == TREE)
                {
                    numberOfTrees++;
                }

                x += movementX;
                y += movementY;
            }

            Console.WriteLine(numberOfTrees);
        }
    }
}
