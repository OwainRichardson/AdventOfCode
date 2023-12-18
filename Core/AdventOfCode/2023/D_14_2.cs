using AdventOfCode._2023.Models;
using System.Text;

namespace AdventOfCode._2023
{
    public static class D_14_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day14.txt").ToArray();

            List<Rock> rocks = ParseInputsToRocks(inputs);

            List<RepeatedRock> patterns = new List<RepeatedRock>();

            //DrawRocks(rocks);

            for (int i = 1; i <= 1000000000; i++)
            {
                RollNorth(rocks);
                //DrawRocks(rocks);

                RollWest(rocks);
                //DrawRocks(rocks);

                RollSouth(rocks);
                //DrawRocks(rocks);

                RollEast(rocks);
                //DrawRocks(rocks);

                var orderedRocks = rocks.Where(r => r.Type == "O").OrderBy(r => r.Id).ToList();
                StringBuilder sb = new StringBuilder();
                foreach (Rock rock in orderedRocks)
                {
                    sb.Append($"{rock.Y}:{rock.X};");
                }

                RepeatedRock rr = new RepeatedRock
                {
                    Pattern = sb.ToString(),
                    Load = CalculateLoad(rocks)
                };

                if (patterns.Any(p => p.Pattern == rr.Pattern))
                {
                    int repeatLength = patterns.Count - 1;

                    int owain = 1000000000 % repeatLength;

                    Console.WriteLine(patterns[owain].Load);
                    break;
                }
                else
                {
                    patterns.Add(rr);
                }
            }

            Console.WriteLine(CalculateLoad(rocks));
        }

        private static void RollEast(List<Rock> rocks)
        {
            bool rockMoved = true;

            while (rockMoved)
            {
                rockMoved = false;

                var orderedRocks = rocks.Where(r => r.Type == "O").OrderByDescending(r => r.X).ToList();
                foreach (Rock rock in orderedRocks)
                {
                    if (rock.CanMoveEast(rocks))
                    {
                        List<Rock> eastRocks = rocks.Where(r => r.Id != rock.Id && r.X > rock.X && r.Y == rock.Y).ToList();

                        if (eastRocks != null && eastRocks.Any())
                        {
                            rock.X = eastRocks.Min(r => r.X) - 1;
                        }
                        else
                        {
                            rock.X = rocks.Where(r => r.Type == "#").Max(r => r.X);
                        }

                        rockMoved = true;
                    }
                }
            }
            //DrawRocks(rocks);
        }

        private static void RollSouth(List<Rock> rocks)
        {
            bool rockMoved = true;

            while (rockMoved)
            {
                rockMoved = false;

                var orderedRocks = rocks.Where(r => r.Type == "O").OrderByDescending(r => r.Y).ToList();
                foreach (Rock rock in orderedRocks)
                {
                    if (rock.CanMoveSouth(rocks))
                    {
                        List<Rock> southRocks = rocks.Where(r => r.Id != rock.Id && r.X == rock.X && r.Y > rock.Y).ToList();

                        if (southRocks != null && southRocks.Any())
                        {
                            rock.Y = southRocks.Min(r => r.Y) - 1;
                        }
                        else
                        {
                            rock.Y = rocks.Where(r => r.Type == "#").Max(r => r.Y);
                        }

                        rockMoved = true;
                    }

                    //DrawRocks(rocks);
                }
            }

            //DrawRocks(rocks);
        }

        private static void RollWest(List<Rock> rocks)
        {
            bool rockMoved = true;

            while (rockMoved)
            {
                rockMoved = false;

                var orderedRocks = rocks.Where(r => r.Type == "O").OrderBy(r => r.X).ToList();
                foreach (Rock rock in orderedRocks)
                {
                    if (rock.CanMoveWest(rocks))
                    {
                        List<Rock> westRocks = rocks.Where(r => r.Id != rock.Id && r.X < rock.X && r.Y == rock.Y).ToList();

                        if (westRocks != null && westRocks.Any())
                        {
                            rock.X = westRocks.Max(r => r.X) + 1;
                        }
                        else
                        {
                            rock.X = 0;
                        }

                        rockMoved = true;
                    }
                }
            }

            //DrawRocks(rocks);
        }

        private static int CalculateLoad(List<Rock> rocks)
        {
            int max = rocks.Max(r => r.Y) + 1;

            int total = 0;

            foreach (Rock rock in rocks.Where(r => r.Type == "O").OrderBy(r => r.Y))
            {
                total += (max - rock.Y);
            }

            return total;
        }

        private static void RollNorth(List<Rock> rocks)
        {
            bool rockMoved = true;

            while (rockMoved)
            {
                rockMoved = false;

                var orderedRocks = rocks.Where(r => r.Type == "O").OrderBy(r => r.Y).ToList();
                foreach (Rock rock in orderedRocks)
                {
                    if (rock.CanMoveNorth(rocks))
                    {
                        List<Rock> northRocks = rocks.Where(r => r.Id != rock.Id && r.X == rock.X && r.Y < rock.Y).ToList();

                        if (northRocks != null && northRocks.Any())
                        {
                            rock.Y = northRocks.Max(r => r.Y) + 1;
                        }
                        else
                        {
                            rock.Y = 0;
                        }

                        rockMoved = true;
                    }

                    //DrawRocks(rocks);
                }
            }

            //DrawRocks(rocks);
        }

        private static void DrawRocks(List<Rock> rocks)
        {
            Console.WriteLine();

            for (int y = rocks.Min(r => r.Y); y <= rocks.Max(r => r.Y); y++)
            {
                for (int x = rocks.Min(r => r.X); x <= rocks.Max(r => r.X); x++)
                {
                    Rock rock = rocks.Find(r => r.Y == y && r.X == x);

                    if (rock != null)
                    {
                        Console.Write(rock.Type);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }

        private static List<Rock> ParseInputsToRocks(string[] inputs)
        {
            List<Rock> rocks = new List<Rock>();
            int id = 1;

            foreach (string input in inputs)
            {
                for (int index = 0; index < input.Length; index++)
                {
                    if (input[index] == 'O' || input[index] == '#')
                    {
                        Rock rock = new Rock
                        {
                            X = index,
                            Y = Array.IndexOf(inputs, input),
                            Type = input[index].ToString(),
                            Id = id
                        };

                        rocks.Add(rock);

                        id += 1;
                    }
                }
            }

            return rocks;
        }
    }
}