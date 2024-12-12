using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;

namespace AdventOfCode._2024
{
    public static class D_12_1
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day12.txt");

            List<GardenCoord> garden = ParseInputs(inputs);

            int groupId = 0;
            foreach (GardenCoord coord in garden)
            {
                List<GardenCoord> localCoords = GetLocalCoords(garden, coord);
                coord.NumberOfFences = (4 - localCoords.Count) + localCoords.Count(lc => lc.Value != coord.Value);

                // ToDo: fix this bit
                if (coord.Group != -1)
                {
                    foreach (var localCoord in localCoords.Where(lc => lc.Value == coord.Value))
                    {
                        localCoord.Group = coord.Group;
                    }
                }
                else if (localCoords.Any(lc => lc.Value == coord.Value && lc.Group != -1))
                {
                    coord.Group = localCoords.First(lc => lc.Value == coord.Value && lc.Group != -1).Group;
                    foreach (var localCoord in localCoords.Where(lc => lc.Value == coord.Value))
                    {
                        localCoord.Group = coord.Group;
                    }
                }
                else
                {
                    coord.Group = groupId;
                    groupId++;
                }
            }

            if (garden.Any(c => c.Group == -1))
            {
                throw new InvalidOperationException();
            }

            long total = 0;

            List<int> groupIds = garden.Select(g => g.Group).Distinct().ToList();

            foreach (int group in groupIds)
            {
                Console.WriteLine(garden.Where(g => g.Group == group).Sum(g => g.NumberOfFences) + "*" + garden.Count(g => g.Group == group));

                total += (garden.Where(g => g.Group == group).Sum(g => g.NumberOfFences) * garden.Count(g => g.Group == group));
            }

            return total.ToString();
        }

        private static List<GardenCoord> GetLocalCoords(List<GardenCoord> garden, GardenCoord currentCoord)
        {
            List<GardenCoord> gardenCoords = new List<GardenCoord>();

            GardenCoord upCoord = garden.FirstOrDefault(gc => gc.X == currentCoord.X && gc.Y == currentCoord.Y - 1);
            if (upCoord != null)
            {
                gardenCoords.Add(upCoord);
            }

            GardenCoord downCoord = garden.FirstOrDefault(gc => gc.X == currentCoord.X && gc.Y == currentCoord.Y + 1);
            if (downCoord != null)
            {
                gardenCoords.Add(downCoord);
            }

            GardenCoord leftCoord = garden.FirstOrDefault(gc => gc.X == currentCoord.X - 1 && gc.Y == currentCoord.Y);
            if (leftCoord != null)
            {
                gardenCoords.Add(leftCoord);
            }

            GardenCoord rightCoord = garden.FirstOrDefault(gc => gc.X == currentCoord.X + 1 && gc.Y == currentCoord.Y);
            if (rightCoord != null)
            {
                gardenCoords.Add(rightCoord);
            }

            return gardenCoords;
        }

        private static List<GardenCoord> ParseInputs(string[] inputs)
        {
            List<GardenCoord> garden = new List<GardenCoord>();

            int y = 0;

            foreach (string input in inputs)
            {
                int x = 0;

                foreach (char c in input)
                {
                    garden.Add(new GardenCoord { X = x, Y = y, Value = c.ToString(), Group = -1 });

                    x++;
                }

                y++;
            }

            return garden;
        }
    }
}