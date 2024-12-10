using AdventOfCode._2023.Models;
using System.Text;

namespace AdventOfCode._2023
{
    public static class D_11_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day11.txt").ToArray();

            List<Galaxy> galaxies = ParseInputsToGalaxies(inputs);

            galaxies = ExpandGalaxies(inputs, galaxies);

            CalculateDistances(galaxies);

            Console.WriteLine(galaxies.Sum(g => g.Distances));
        }

        private static void CalculateDistances(List<Galaxy> galaxies)
        {
            foreach (Galaxy galaxy in galaxies)
            {
                foreach (Galaxy otherGalaxy in galaxies.Where(g => g.Id > galaxy.Id))
                {
                    long distance = Math.Abs(galaxy.X - otherGalaxy.X) + Math.Abs(galaxy.Y - otherGalaxy.Y);
                    galaxy.Distances += distance;
                }
            }
        }

        private static List<Galaxy> ExpandGalaxies(string[] inputs, List<Galaxy> galaxies)
        {
            List<int> columnsWithoutGalaxy = new List<int>();
            for (int x = 0; x < inputs[0].Length; x++)
            {
                if (!galaxies.Any(g => g.X == x))
                {
                    columnsWithoutGalaxy.Add(x);
                }
            }
            List<string> newInputs = new List<string>();
            foreach (string input in inputs)
            {
                string newInput = input;
                foreach (int index in columnsWithoutGalaxy)
                {
                    newInput = newInput.Insert(index + columnsWithoutGalaxy.IndexOf(index), ".");
                }

                newInputs.Add(newInput);
                var i = 9;
            }

            List<int> rowsWithoutGalaxy = new List<int>();
            for (int y = 0; y < inputs.Length; y++)
            {
                if (!galaxies.Any(g => g.Y == y))
                {
                    rowsWithoutGalaxy.Add(y);
                }
            }
            string stringToInsert = CalculateEmptyRow(newInputs[0]);
            foreach (int index in rowsWithoutGalaxy)
            {
                newInputs.Insert(index + rowsWithoutGalaxy.IndexOf(index), stringToInsert);
            }

            galaxies = ParseInputsToGalaxies(newInputs);
            return galaxies;
        }

        private static string CalculateEmptyRow(string v)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < v.Length; i++)
            {
                sb.Append(".");
            }

            return sb.ToString();
        }

        private static List<Galaxy> ParseInputsToGalaxies(string[] inputs)
        {
            int index = 1;
            List<Galaxy> galaxies = new List<Galaxy>();

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[0].Length; x++)
                {
                    if (inputs[y][x] == '#')
                    {
                        Galaxy galaxy = new Galaxy
                        {
                            Id = index,
                            X = x,
                            Y = y
                        };
                        galaxies.Add(galaxy);
                        index++;
                    }
                }
            }

            return galaxies;
        }

        private static List<Galaxy> ParseInputsToGalaxies(List<string> inputs)
        {
            int index = 1;
            List<Galaxy> galaxies = new List<Galaxy>();

            for (int y = 0; y < inputs.Count; y++)
            {
                for (int x = 0; x < inputs[0].Length; x++)
                {
                    if (inputs[y][x] == '#')
                    {
                        Galaxy galaxy = new Galaxy
                        {
                            Id = index,
                            X = x,
                            Y = y
                        };
                        galaxies.Add(galaxy);
                        index++;
                    }
                }
            }

            return galaxies;
        }

        private static void DrawMap(List<Galaxy> galaxies)
        {
            Console.WriteLine();

            for (int y = galaxies.Min(p => p.Y); y <= galaxies.Max(p => p.Y); y++)
            {
                for (int x = galaxies.Min(p => p.X); x <= galaxies.Max(p => p.X); x++)
                {
                    if (galaxies.Any(p => p.X == x && p.Y == y))
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
    }
}