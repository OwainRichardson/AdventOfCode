using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;

namespace AdventOfCode._2023
{
    public static class D_16_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day16.txt").ToArray();

            List<Mirror> mirrors = ParseInputsToMirrors(inputs);
            List<Mirror> energisedTiles = new List<Mirror>
            {
                new Mirror { X = 0, Y = 0 }
            };
            List<Beam> beams = new List<Beam>
            {
                new Beam { X = -1, Y = 0, Direction = Directions.Right, Id = "origin" }
            };

            bool firstTime = true;

            while (beams.All(b => b.TurnsSinceNonEnergisedTile < 10))
            {
                List<Beam> newBeams = new List<Beam>();
                List<Beam> beamsToRemove = new List<Beam>();

                foreach (Beam beam in beams)
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
                        beamsToRemove.Add(beam);
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
                        beam.TurnsSinceNonEnergisedTile = 0;
                    }
                    else
                    {
                        beam.TurnsSinceNonEnergisedTile += 1;
                    }
                }

                beams.AddRange(newBeams);
                foreach (Beam beam in beamsToRemove)
                {
                    beams.Remove(beam);
                }

                firstTime = false;
            }

            //DrawEnergisedTiles(energisedTiles);

            Console.WriteLine(energisedTiles.Count);
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