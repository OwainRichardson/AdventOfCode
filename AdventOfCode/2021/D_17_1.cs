using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_17_1
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

            FireY(minY, maxY);
        }

        private static void FireY(int minY, int maxY)
        {
            int highestY = 0;
            for (int y = 0; y <= 100; y++)
            {
                int currentY = 0;
                int highestYThisRun = 0;
                int currentVelocity = y;
                while (currentY > minY)
                {
                    if (currentY > highestYThisRun) highestYThisRun = currentY;

                    currentY += currentVelocity;

                    if (currentY >= minY && currentY <= maxY)
                    {
                        if (highestYThisRun > highestY) highestY = highestYThisRun;
                        break;
                    }

                    currentVelocity -= 1;
                }
            }

            Console.WriteLine(highestY);
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