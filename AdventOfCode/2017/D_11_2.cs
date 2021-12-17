using System;
using System.IO;

namespace AdventOfCode._2017
{
    public static class D_11_2
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day11_full.txt")[0];

            string[] steps = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            int x = 0;
            int y = 0;
            int maxDistance = 0;

            foreach (string step in steps)
            {
                if (step.Contains("n"))
                {
                    y++;
                }
                if (step.Contains("e"))
                {
                    x++;
                }
                if (step.Contains("s"))
                {
                    y--;
                }
                if (step.Contains("w"))
                {
                    x--;
                }

                int distance = CalculateDistance(x, y);

                maxDistance = distance > maxDistance ? distance : maxDistance;
            }            

            Console.WriteLine(maxDistance);
        }

        private static int CalculateDistance(int x, int y)
        {
            int numberOfSteps = 0;
            while (x != 0 || y != 0)
            {
                if (x != 0 && y != 0)
                {
                    if (x < 0)
                    {
                        x++;
                    }
                    else
                    {
                        x--;
                    }

                    if (y < 0)
                    {
                        y++;
                    }
                    else
                    {
                        y--;
                    }
                }
                else if (x == 0 && y != 0)
                {
                    if (y < 0)
                    {
                        y++;
                    }
                    else
                    {
                        y--;
                    }
                }
                else if (x != 0 && y == 0)
                {
                    if (x < 0)
                    {
                        x++;
                    }
                    else
                    {
                        x--;
                    }
                }

                numberOfSteps++;
            }

            return numberOfSteps;
        }
    }
}
