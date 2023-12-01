using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_03_2
    {
        private const char TREE = '#';

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day03.txt");
            int width = inputs[0].Length;
            int height = inputs.Count();

            List<int> totalNumberOfTrees = new List<int>();
            List<SledgeDirection> sledgeDirections = new List<SledgeDirection>
            {
                new SledgeDirection { Right = 1, Down = 1 },
                new SledgeDirection { Right = 3, Down = 1 },
                new SledgeDirection { Right = 5, Down = 1 },
                new SledgeDirection { Right = 7, Down = 1 },
                new SledgeDirection { Right = 1, Down = 2}
            };

            foreach (var direction in sledgeDirections)
            {
                totalNumberOfTrees.Add(NumberOfTreesOnRoute(inputs, 0, 0, width, height, direction.Right, direction.Down));
            }

            Console.WriteLine(totalNumberOfTrees.Mult());
        }

        private static int NumberOfTreesOnRoute(string[] inputs, int x, int y, int width, int height, int movementX, int movementY)
        {
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

            return numberOfTrees;
        }
    }
}
