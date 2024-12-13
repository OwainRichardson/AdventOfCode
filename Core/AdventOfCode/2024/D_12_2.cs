using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;

namespace AdventOfCode._2024
{
    public static class D_12_2
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day12.txt");

            List<GardenCoord> garden = ParseInputs(inputs);

            CountSides(garden);
            CalculateGroups(garden);

            if (garden.Any(c => c.Group == -1))
            {
                throw new InvalidOperationException();
            }

            long total = 0;

            List<int> groupIds = garden.Select(g => g.Group).Distinct().ToList();

            foreach (int group in groupIds)
            {
                total += (AddUpSides(garden, group, inputs.Length) * garden.Count(g => g.Group == group));
            }

            return total.ToString();
        }

        private static long AddUpSides(List<GardenCoord> garden, int group, int inputsLength)
        {
            long numberOfSides = 0;

            List<Side> allSides = garden.Where(g => g.Group == group).SelectMany(g => g.Sides).ToList();

            List<Side> topSides = allSides.Where(s => s.Direction == "top").ToList();
            for (int y = 0; y < inputsLength; y++)
            {
                var topSidesAtY = topSides.Where(s => s.YSide == y).ToList();

                if (!topSidesAtY.Any()) continue;

                numberOfSides += 1;
                for (int x = 1; x < topSidesAtY.Count; x++)
                {
                    if (topSidesAtY[x].XSide == topSidesAtY[x - 1].XSide + 1) continue;

                    numberOfSides += 1;
                }
            }

            List<Side> bottomSides = allSides.Where(s => s.Direction == "bottom").ToList();
            for (int y = 0; y < inputsLength; y++)
            {
                var bottomSidesAtY = bottomSides.Where(s => s.YSide == y).ToList();

                if (!bottomSidesAtY.Any()) continue;

                numberOfSides += 1;
                for (int x = 1; x < bottomSidesAtY.Count; x++)
                {
                    if (bottomSidesAtY[x].XSide == bottomSidesAtY[x - 1].XSide + 1) continue;

                    numberOfSides += 1;
                }
            }

            List<Side> leftSides = allSides.Where(s => s.Direction == "left").ToList();
            for (int x = 0; x < inputsLength; x++)
            {
                var leftSidesAtX = leftSides.Where(s => s.XSide == x).ToList();

                if (!leftSidesAtX.Any()) continue;

                numberOfSides += 1;
                for (int y = 1; y < leftSidesAtX.Count; y++)
                {
                    if (leftSidesAtX[y].YSide == leftSidesAtX[y - 1].YSide + 1) continue;

                    numberOfSides += 1;
                }
            }

            List<Side> rightSides = allSides.Where(s => s.Direction == "right").ToList();
            for (int x = 0; x < inputsLength; x++)
            {
                var leftSidesAtX = rightSides.Where(s => s.XSide == x).ToList();

                if (!leftSidesAtX.Any()) continue;

                numberOfSides += 1;
                for (int y = 1; y < leftSidesAtX.Count; y++)
                {
                    if (leftSidesAtX[y].YSide == leftSidesAtX[y - 1].YSide + 1) continue;

                    numberOfSides += 1;
                }
            }

            return numberOfSides;
        }

        private static void CalculateGroups(List<GardenCoord> garden)
        {
            int group = 0;
            foreach (GardenCoord coord in garden)
            {
                if (coord.Group == -1)
                {
                    coord.Group = group;
                    PopulateGroups(garden, coord, group);

                    group++;
                }
            }
        }

        private static void PopulateGroups(List<GardenCoord> garden, GardenCoord coord, int group)
        {
            bool changesMade = true;

            while (changesMade)
            {
                changesMade = false;

                List<GardenCoord> localCoords = GetLocalCoords(garden, coord).Where(lc => lc.Value == coord.Value).ToList();

                foreach (GardenCoord localCoord in localCoords)
                {
                    if (localCoord.Group == -1)
                    {
                        localCoord.Group = group;

                        PopulateGroups(garden, localCoord, group);
                        changesMade = true;
                    }
                }
            }
        }

        private static void CountSides(List<GardenCoord> garden)
        {
            foreach (GardenCoord coord in garden)
            {
                coord.Sides = new List<Side>
                {
                    new Side { Direction = "top", XSide = coord.X, YSide = coord.Y },
                    new Side { Direction = "bottom", XSide = coord.X, YSide = coord.Y },
                    new Side { Direction = "left", XSide = coord.X, YSide = coord.Y },
                    new Side { Direction = "right", XSide = coord.X, YSide = coord.Y }
                };

                List<GardenCoord> localCoords = GetLocalCoords(garden, coord).Where(c => c.Value == coord.Value).ToList();
                
                foreach (GardenCoord localCoord in localCoords)
                {
                    if (localCoord.X < coord.X)
                    {
                        var leftSide = coord.Sides.Single(c => c.Direction == "left");
                        coord.Sides.Remove(leftSide);
                    }
                    else if (localCoord.X > coord.X)
                    {
                        var rightSide = coord.Sides.Single(c => c.Direction == "right");
                        coord.Sides.Remove(rightSide);
                    }
                    else if (localCoord.Y < coord.Y)
                    {
                        var topSide = coord.Sides.Single(c => c.Direction == "top");
                        coord.Sides.Remove(topSide);
                    }
                    else if (localCoord.Y > coord.Y)
                    {
                        var bottomSide = coord.Sides.Single(c => c.Direction == "bottom");
                        coord.Sides.Remove(bottomSide);
                    }
                }
            }
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