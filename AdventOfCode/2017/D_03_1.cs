using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_03_1
    {
        public static void Execute()
        {
            int input = 361527;

            List<MemoryLocation> memoryGrid = GenerateMemory(input);

            //PrintGrid(memoryGrid);

            var requiredMemory = memoryGrid.First(x => x.Value == input);

            Console.WriteLine(requiredMemory.ManhattanDistance);
        }

        private static void PrintGrid(List<MemoryLocation> memoryGrid)
        {
            int minX = memoryGrid.Min(m => m.X);
            int maxX = memoryGrid.Max(m => m.X);

            int minY = memoryGrid.Min(m => m.Y);
            int maxY = memoryGrid.Max(m => m.Y);

            for (int y = maxY; y >= minY; y--)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    string value = memoryGrid.FirstOrDefault(m => m.X == x && m.Y == y)?.Value.ToString();

                    Console.Write($"{value}\t");
                }
                Console.WriteLine();
            }
        }

        private static List<MemoryLocation> GenerateMemory(int input)
        {
            List<MemoryLocation> memoryStore = new List<MemoryLocation>();

            int x = 0;
            int y = 0;
            int direction = 1;

            for (int i = 1; i <= input; i++)
            {
                memoryStore.Add(new MemoryLocation
                {
                    X = x,
                    Y = y,
                    Value = i
                });

                if (direction == 0)
                {
                    y++;
                }
                else if (direction == 1)
                {
                    x++;
                }
                else if (direction == 2)
                {
                    y--;
                }
                else if (direction == 3)
                {
                    x--;
                }

                CalculateDirection(memoryStore, ref direction, x, y);

            }

            return memoryStore;
        }

        private static void CalculateDirection(List<MemoryLocation> memoryStore, ref int direction, int x, int y)
        {
            if (direction == 0)
            {
                if (!memoryStore.Any(m => m.Y == y && m.X == x - 1))
                {
                    direction = 3;
                }

            }
            else if (direction == 1)
            {
                if (!memoryStore.Any(m => m.Y == y + 1 && m.X == x))
                {
                    direction -= 1;
                }
            }
            else if (direction == 2)
            {
                if (!memoryStore.Any(m => m.Y == y && m.X == x + 1))
                {
                    direction -= 1;
                }
            }
            else if (direction == 3)
            {
                if (!memoryStore.Any(m => m.Y == y - 1 && m.X == x))
                {
                    direction -= 1;
                }
            }
        }
    }
}
