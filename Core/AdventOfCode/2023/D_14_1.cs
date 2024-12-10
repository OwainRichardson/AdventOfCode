using AdventOfCode._2023.Models;

namespace AdventOfCode._2023
{
    public static class D_14_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day14.txt").ToArray();

            List<Rock> rocks = ParseInputsToRocks(inputs);

            //DrawRocks(rocks);

            RollNorth(rocks);

            //DrawRocks(rocks);

            Console.WriteLine(CalculateLoad(rocks));
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
            bool aRockMoved = true;

            while (aRockMoved)
            {
                aRockMoved = false;

                foreach (Rock rock in rocks.Where(r => r.Type == "O"))
                {
                    if (rock.CanMoveNorth(rocks))
                    {
                        rock.Y -= 1;
                        aRockMoved = true;
                    }
                }

                //DrawRocks(rocks);
            }
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