using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using Microsoft.VisualBasic;
using System.Data.Common;
using System.Net.WebSockets;
using System.Runtime;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2023
{
    public static class D_16_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day16.txt").ToArray();

            int maxEnergisedTiles = 0;

            List<Mirror> mirrors = ParseInputsToMirrors(inputs);

            for (int y = 0; y <= mirrors.Max(m => m.Y); y++)
            {
                for (int x = 0; x <= mirrors.Max(m => m.X); x += mirrors.Max(m => m.X))
                {
                    Beam firstBeam = new Beam { X = x, Y = y, Direction = x == 0 ? Directions.Right : Directions.Left, Id = "origin" };

                    List<Mirror> energisedTiles = Run(mirrors, firstBeam);

                    if (energisedTiles.Count > maxEnergisedTiles)
                    {
                        maxEnergisedTiles = energisedTiles.Count;
                    }
                }
            }

            for (int x = 0; x <= mirrors.Max(m => m.X); x++)
            {
                for (int y = 0; y <= mirrors.Max(m => m.Y); y += mirrors.Max(m => m.Y))
                {
                    Beam firstBeam = new Beam { X = x, Y = y, Direction = y == 0 ? Directions.Down : Directions.Up, Id = "origin" };

                    List<Mirror> energisedTiles = Run(mirrors, firstBeam);

                    if (energisedTiles.Count > maxEnergisedTiles)
                    {
                        maxEnergisedTiles = energisedTiles.Count;
                    }
                }
            }

            //DrawEnergisedTiles(energisedTiles);

            Console.WriteLine(maxEnergisedTiles);
        }

        private static List<Mirror> Run(List<Mirror> mirrors, Beam firstBeam)
        {
            List<Mirror> energisedTiles = new List<Mirror>
            {
                new Mirror { X = firstBeam.X, Y = firstBeam.Y }
            };
            List<Beam> beams = new List<Beam>
            {
                firstBeam
            };

            bool firstTime = true;
            int turnsSinceNonEnergisedTile = 0;

            while (turnsSinceNonEnergisedTile < 1000)
            {
                List<Beam> newBeams = new List<Beam>();

                foreach (Beam beam in beams.Where(b => b.TurnsSinceNonEnergisedTile < int.MaxValue))
                {
                    int nextX = beam.X;
                    int nextY = beam.Y;

                    switch (beam.Direction)
                    {
                        case Directions.Up:
                            nextY -= 1;
                            break;
                        case Directions.Down:
                            nextY += 1;
                            break;
                        case Directions.Left:
                            nextX -= 1;
                            break;
                        case Directions.Right:
                            nextX += 1;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }

                    if ((nextX < 0 || nextY < 0 || nextX > mirrors.Max(m => m.X) || nextY > mirrors.Max(m => m.Y)) && !firstTime)
                    {
                        beam.TurnsSinceNonEnergisedTile = int.MaxValue;
                        continue;
                    }

                    Mirror mirror = mirrors.Find(m => m.Y == nextY && m.X == nextX);
                    if (mirror != null)
                    {
                        if (mirror.Type == '|' && (beam.Direction == Directions.Left || beam.Direction == Directions.Right))
                        {
                            beam.Direction = Directions.Down;
                            string id = $"{nextY}:{nextX}:up";
                            if (!beams.Exists(b => b.Id == id))
                            {
                                newBeams.Add(new Beam { Direction = Directions.Up, X = nextX, Y = nextY, Id = id });
                            }
                        }
                        else if (mirror.Type == '-' && (beam.Direction == Directions.Up || beam.Direction == Directions.Down))
                        {
                            beam.Direction = Directions.Left;
                            string id = $"{nextY}:{nextX}:right";
                            if (!beams.Exists(b => b.Id == id))
                            {
                                newBeams.Add(new Beam { Direction = Directions.Right, X = nextX, Y = nextY, Id = id });
                            }
                        }
                        else if (mirror.Type == '/')
                        {
                            switch (beam.Direction)
                            {
                                case Directions.Up:
                                    beam.Direction = Directions.Right;
                                    break;
                                case Directions.Down:
                                    beam.Direction = Directions.Left;
                                    break;
                                case Directions.Left:
                                    beam.Direction = Directions.Down;
                                    break;
                                case Directions.Right:
                                    beam.Direction = Directions.Up;
                                    break;
                                default:
                                    throw new InvalidOperationException();
                            }
                        }
                        else if (mirror.Type == '\\')
                        {
                            switch (beam.Direction)
                            {
                                case Directions.Up:
                                    beam.Direction = Directions.Left;
                                    break;
                                case Directions.Down:
                                    beam.Direction = Directions.Right;
                                    break;
                                case Directions.Left:
                                    beam.Direction = Directions.Up;
                                    break;
                                case Directions.Right:
                                    beam.Direction = Directions.Down;
                                    break;
                                default:
                                    throw new InvalidOperationException();
                            }
                        }
                    }

                    beam.X = nextX;
                    beam.Y = nextY;

                    if (!energisedTiles.Exists(m => m.Y == nextY && m.X == nextX))
                    {
                        energisedTiles.Add(new Mirror { X = nextX, Y = nextY });
                        turnsSinceNonEnergisedTile = 0;
                    }
                    else
                    {
                        turnsSinceNonEnergisedTile += 1;
                    }
                }

                if (beams.All(b => b.TurnsSinceNonEnergisedTile == int.MaxValue)) break;

                beams.AddRange(newBeams);

                firstTime = false;
            }

            return energisedTiles;
        }

        private static void DrawEnergisedTiles(List<Mirror> energisedTiles)
        {
            Console.WriteLine();

            for (int y = 0; y <= energisedTiles.Max(t => t.Y); y++)
            {
                for (int x = 0; x <= energisedTiles.Max(t => t.X); x++)
                {
                    if (energisedTiles.SingleOrDefault(t => t.Y == y && t.X == x) != null)
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }

        private static List<Mirror> ParseInputsToMirrors(string[] inputs)
        {
            List<Mirror> mirrors = new List<Mirror>();

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[0].Length; x++)
                {
                    if (!inputs[y][x].Equals('.'))
                    {
                        Mirror mirror = new Mirror { X = x, Y = y, Type = inputs[y][x] };
                        mirrors.Add(mirror);
                    }
                }
            }

            return mirrors;
        }
    }
}

//3271