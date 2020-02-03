using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_03_2
    {
        public static List<Coord> coords = new List<Coord>();
        public static int currentSantaX = 0;
        public static int currentSantaY = 0;
        public static int currentRoboX = 0;
        public static int currentRoboY = 0;

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day03_full.txt")[0];
            coords.Add(new Coord { X = 0, Y = 0, Presents = 1 });

            for (int i = 0; i < input.Length; i += 2)
            {
                MoveSanta(input[i]);
                MoveRoboSanta(input[i + 1]);
            }

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(coords.Count());
            Console.ResetColor();
        }

        private static void MoveRoboSanta(char v)
        {
            int x, y;

            if (v.ToString() == "<")
            {
                x = currentRoboX - 1;
                y = currentRoboY;

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

                currentRoboX = x;
                currentRoboY = y;
            }
            if (v.ToString() == ">")
            {
                x = currentRoboX + 1;
                y = currentRoboY;

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

                currentRoboX = x;
                currentRoboY = y;
            }
            if (v.ToString() == "^")
            {
                x = currentRoboX;
                y = currentRoboY + 1;

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

                currentRoboX = x;
                currentRoboY = y;
            }
            if (v.ToString() == "v")
            {
                x = currentRoboX;
                y = currentRoboY - 1;

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

                currentRoboX = x;
                currentRoboY = y;
            }
        }

        public static void MoveSanta(Char c)
        {
            int x, y;
            if (c.ToString() == "<")
            {
                x = currentSantaX - 1;
                y = currentSantaY;

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

                currentSantaX = x;
                currentSantaY = y;
            }
            if (c.ToString() == ">")
            {
                x = currentSantaX + 1;
                y = currentSantaY;

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

                currentSantaX = x;
                currentSantaY = y;
            }
            if (c.ToString() == "^")
            {
                x = currentSantaX;
                y = currentSantaY + 1;

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

                currentSantaX = x;
                currentSantaY = y;
            }
            if (c.ToString() == "v")
            {
                x = currentSantaX;
                y = currentSantaY - 1;

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

                currentSantaX = x;
                currentSantaY = y;
            }
        }
    }
}
