using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;

namespace AdventOfCode._2024
{
    public static class D_11_1
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"2024\Data\day11.txt")[0];

            Dictionary<long, long> stones = ParseInputs(input);

            int maxLoops = 25;
            int loops = 0;

            while (loops < maxLoops)
            {
                Dictionary<long, long> newStones = new Dictionary<long, long>();
                foreach (var stone in stones)
                {
                    if (stone.Key == 0)
                    {
                        newStones.TryAddStone(1, stone.Value);
                    }
                    else if (stone.Key.ToString().Length % 2 == 0)
                    {
                        string key = stone.Key.ToString();
                        int length = key.Length / 2;

                        newStones.TryAddStone(long.Parse(key.Substring(0, length)), stone.Value);
                        newStones.TryAddStone(long.Parse(key.Substring(length)), stone.Value);
                    }
                    else
                    {
                        newStones.TryAddStone(stone.Key * 2024, stone.Value);
                    }
                }

                stones = newStones;

                loops++;
            }

            Console.WriteLine(stones.Sum(s => s.Value));
        }

        private static Dictionary<long, long> ParseInputs(string input)
        {
            Dictionary<long, long> stones = new Dictionary<long, long>();

            long[] split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();

            foreach (long s in split)
            {
                stones.TryAddStone(s, 1);
            }

            return stones;
        }
    }
}