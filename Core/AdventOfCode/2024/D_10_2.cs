using AdventOfCode._2024.Models;

namespace AdventOfCode._2024
{
    public static class D_10_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day10.txt");

            List<TrailheadCoord> map = MapInputs(inputs);
            int total = 0;

            foreach (TrailheadCoord startCoord in map.Where(m => m.Value == 0))
            {
                StepThroughMap(map, startCoord);

                total += map.Sum(m => m.NumberOfRoutesThatGetHere);

                map.ForEach(m =>
                {
                    m.NumberOfRoutesThatGetHere = 0;
                });
            }

            Console.WriteLine(total);
        }

        private static void StepThroughMap(List<TrailheadCoord> map, TrailheadCoord currentCoord)
        {
            if (currentCoord.Value == 9)
            {
                currentCoord.NumberOfRoutesThatGetHere += 1;
            }

            List<TrailheadCoord> nextSteps = map.Where(m =>
                                        ((m.X == currentCoord.X && m.Y == currentCoord.Y - 1)
                                        || (m.X == currentCoord.X && m.Y == currentCoord.Y + 1)
                                        || (m.X == currentCoord.X - 1 && m.Y == currentCoord.Y)
                                        || (m.X == currentCoord.X + 1 && m.Y == currentCoord.Y))
                                        && m.Value == currentCoord.Value + 1
                                        ).ToList();

            foreach (TrailheadCoord nextStep in nextSteps)
            {
                StepThroughMap(map, nextStep);
            }
        }

        private static List<TrailheadCoord> MapInputs(string[] inputs)
        {
            List<TrailheadCoord> coords = new List<TrailheadCoord>();
            int length = inputs.Length;
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    int value;
                    if (!int.TryParse(inputs[y][x].ToString(), out value))
                    {
                        value = -1;
                    }

                    coords.Add(new TrailheadCoord { X = x, Y = y, Value = value });
                }
            }

            return coords;
        }
    }
}