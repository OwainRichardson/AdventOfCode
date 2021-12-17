using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_17_2
    {
        private static string _binary = "";
        private static int _versionSum = 0;

        public static void Execute()
        {
            string input = File.ReadAllLines(@"2021\Data\day17.txt").Single();

            string pattern = @"x=(\d+)\.\.(\d+), y=(-?\d+)\.\.(-?\d+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            int maxX = 0;
            int minX = 0;
            int maxY = 0;
            int minY = 0;

            if (match.Success)
            {
                (maxX, minX) = ParseXCoords(match);
                (maxY, minY) = ParseYCoords(match);                
            }

            Fire(minY, maxY, minX, maxX);
        }

        private static void Fire(int minY, int maxY, int minX, int maxX)
        {
            List<string> successfulInitialVelocities = new List<string>();

            for (int y = minY; y <= 1000; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    int currentY = 0;
                    int currentX = 0;
                    int currentYVelocity = y;
                    int currentXVelocity = x;

                    while (currentY >= minY && currentX <= maxX)
                    {
                        currentY += currentYVelocity;
                        currentX += currentXVelocity;

                        if (currentY >= minY && currentY <= maxY && currentX >= minX && currentX <= maxX)
                        {
                            successfulInitialVelocities.Add($"{x},{y}");
                            break;
                        }

                        currentYVelocity -= 1;
                        currentXVelocity = currentXVelocity > 0 ? currentXVelocity - 1 : currentXVelocity == 0 ? 0 : currentXVelocity + 1;
                    }
                }
            }

            Console.WriteLine(successfulInitialVelocities.Distinct().Count());
        }

            private static (int maxX, int minX) ParseXCoords(Match match)
        {
            int maxX;
            int minX;

            if (int.Parse(match.Groups[1].Value) > int.Parse(match.Groups[2].Value))
            {
                maxX = int.Parse(match.Groups[1].Value);
                minX = int.Parse(match.Groups[2].Value);
            }
            else
            {
                maxX = int.Parse(match.Groups[2].Value);
                minX = int.Parse(match.Groups[1].Value);
            }

            return (maxX, minX);
        }

        private static (int maxY, int minY) ParseYCoords(Match match)
        {
            int maxY;
            int minY;

            if (int.Parse(match.Groups[3].Value) > int.Parse(match.Groups[4].Value))
            {
                maxY = int.Parse(match.Groups[3].Value);
                minY = int.Parse(match.Groups[4].Value);
            }
            else
            {
                maxY = int.Parse(match.Groups[4].Value);
                minY = int.Parse(match.Groups[3].Value);
            }

            return (maxY, minY);
        }
    }
}