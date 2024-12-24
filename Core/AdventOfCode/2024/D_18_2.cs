using AdventOfCode._2024.Models;

namespace AdventOfCode._2024
{
    public static class D_18_2
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day18.txt");

            int lastTake = -1;

            for (int take = 1024; take <= inputs.Length; take++)
            {
                lastTake = take;

                List<MazeCoord> map = ParseInputs(inputs.Take(take).ToArray());
                map = PadMap(map);

                map.First(m => m.X == 0 && m.Y == 0).Distance = 0;
                int maxX = map.Max(m => m.X);
                int maxY = map.Max(m => m.Y);
                map.First(m => m.X == 0 && m.Y == 0).Distance = 0;
                map.First(m => m.X == maxX && m.Y == maxY).IsEnd = true;

                CalculateDistances(map);

                if (map.Single(m => m.IsEnd).Distance == int.MaxValue)
                {
                    break;
                }
            }

            return inputs[lastTake - 1];
        }

        private static void CalculateDistances(List<MazeCoord> map)
        {
            Queue<MazeCoord> queue = new Queue<MazeCoord>();
            queue.Enqueue(map.Single(m => m.X == 0 && m.Y == 0));

            while (queue.Count > 0)
            {
                MazeCoord current = queue.Dequeue();
                if (current.IsEnd) break;

                List<MazeCoord> localCoords = GetLocalCoords(current, map);

                foreach (MazeCoord local in localCoords)
                {
                    if (local.IsWall) continue;

                    if (current.Distance + 1 < local.Distance)
                    {
                        local.Distance = current.Distance + 1;

                        queue = new Queue<MazeCoord>(queue.Where(q => q.Id != $"{local.X}:{local.Y}"));
                        queue.Enqueue(local);
                    }
                }
            }
        }

        private static void PrintMap(List<MazeCoord> map)
        {
            Console.WriteLine();

            for (int y = 0; y <= map.Max(y => y.Y); y++)
            {
                for (int x = 0; x <= map.Max(x => x.X); x++)
                {
                    MazeCoord coord = map.First(w => w.X == x && w.Y == y);

                    if (coord.IsWall) Console.Write("  #  ");
                    else if (coord.Distance == int.MaxValue) Console.Write("  .  ");
                    else Console.Write($"  {coord.Distance} ");

                    if (coord.Distance <= 9 && !coord.IsWall) Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        private static List<MazeCoord> GetLocalCoords(MazeCoord currentCoord, List<MazeCoord> map)
        {
            List<MazeCoord> localCoords = new List<MazeCoord>();

            MazeCoord upCoord = map.FirstOrDefault(gc => gc.X == currentCoord.X && gc.Y == currentCoord.Y - 1);
            if (upCoord != null)
            {
                localCoords.Add(upCoord);
            }

            MazeCoord downCoord = map.FirstOrDefault(gc => gc.X == currentCoord.X && gc.Y == currentCoord.Y + 1);
            if (downCoord != null)
            {
                localCoords.Add(downCoord);
            }

            MazeCoord leftCoord = map.FirstOrDefault(gc => gc.X == currentCoord.X - 1 && gc.Y == currentCoord.Y);
            if (leftCoord != null)
            {
                localCoords.Add(leftCoord);
            }

            MazeCoord rightCoord = map.FirstOrDefault(gc => gc.X == currentCoord.X + 1 && gc.Y == currentCoord.Y);
            if (rightCoord != null)
            {
                localCoords.Add(rightCoord);
            }

            return localCoords;
        }

        private static List<MazeCoord> PadMap(List<MazeCoord> map)
        {
            for (int y = 0; y <= map.Max(m => m.Y); y++)
            {
                for (int x = 0; x <= map.Max(m => m.X); x++)
                {
                    MazeCoord coord = map.FirstOrDefault(m => m.X == x && m.Y == y);
                    if (coord == null)
                    {
                        map.Add(new MazeCoord { X = x, Y = y, IsWall = false, Distance = int.MaxValue, Value = '.', Id = $"{x}:{y}" });
                    }
                }
            }

            return map.OrderBy(m => m.Y).ThenBy(m => m.X).ToList();
        }

        private static List<MazeCoord> ParseInputs(string[] inputs)
        {
            List<MazeCoord> map = new List<MazeCoord>();

            foreach (string input in inputs)
            {
                int[] split = input.Split(',').Select(i => int.Parse(i)).ToArray();

                map.Add(new MazeCoord { X = split[0], Y = split[1], IsWall = true, Distance = int.MaxValue, Id = $"{split[0]}:{split[1]}" });
            }

            return map;
        }
    }
}