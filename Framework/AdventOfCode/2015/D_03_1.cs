using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2015
{
    public class D_03_1
    {
        public static List<Coord> coords = new List<Coord>();
        public static int currentX = 0;
        public static int currentY = 0;

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day03_full.txt")[0];
            coords.Add(new Coord { X = 0, Y = 0, Presents = 1 });

            foreach (Char c in input)
            {
                int x, y;

                if (c.ToString() == "<")
                {
                    x = currentX - 1;
                    y = currentY;

                    var existing = coords.FirstOrDefault(co => co.X == x && co.Y == y);
                    if (existing == null)
                    {
                        coords.Add(new Coord
                        {
                            X = x,
                            Y = y,
                            Presents = 1
                        });
                    }
                    else
                    {
                        existing.Presents += 1;
                    }

                    currentX = x;
                    currentY = y;
                }
                if (c.ToString() == ">")
                {
                    x = currentX + 1;
                    y = currentY;

                    var existing = coords.FirstOrDefault(co => co.X == x && co.Y == y);
                    if (existing == null)
                    {
                        coords.Add(new Coord
                        {
                            X = x,
                            Y = y,
                            Presents = 1
                        });
                    }
                    else
                    {
                        existing.Presents += 1;
                    }

                    currentX = x;
                    currentY = y;
                }
                if (c.ToString() == "^")
                {
                    x = currentX;
                    y = currentY + 1;

                    var existing = coords.FirstOrDefault(co => co.X == x && co.Y == y);
                    if (existing == null)
                    {
                        coords.Add(new Coord
                        {
                            X = x,
                            Y = y,
                            Presents = 1
                        });
                    }
                    else
                    {
                        existing.Presents += 1;
                    }

                    currentX = x;
                    currentY = y;
                }
                if (c.ToString() == "v")
                {
                    x = currentX;
                    y = currentY - 1;

                    var existing = coords.FirstOrDefault(co => co.X == x && co.Y == y);
                    if (existing == null)
                    {
                        coords.Add(new Coord
                        {
                            X = x,
                            Y = y,
                            Presents = 1
                        });
                    }
                    else
                    {
                        existing.Presents += 1;
                    }

                    currentX = x;
                    currentY = y;
                }
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(coords.Count());
            Console.ResetColor();
        }
    }
}