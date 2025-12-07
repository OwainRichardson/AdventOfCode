

using AdventOfCode._2025.Models;
using System.Text;

namespace AdventOfCode._2025;

public static class D_07_2
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day07.txt").ToArray();

        List<Coord> map = ParseInputs(inputs);
        Coord start = map.Single(m => m.IsStart);
        Dictionary<int, long> paths = new Dictionary<int, long>() { { start.X, 1 } };

        for (int y = 0; y < map.Max(m => m.Y); y++)
        {
            Dictionary<int, long> nextPaths = paths.ToDictionary(
                                                        p => p.Key,
                                                        p => p.Value);

            foreach (var path in paths)
            {
                Coord coordBelowBeam = map.First(m => m.Y == y + 1 && m.X == path.Key);

                if (coordBelowBeam == null)
                {
                    break;
                }

                if (coordBelowBeam.Value == '.')
                {
                    continue;
                }

                if (coordBelowBeam.Value == '^')
                {
                    nextPaths[path.Key] -= path.Value;

                    if (nextPaths.ContainsKey(path.Key - 1))
                    {
                        nextPaths[path.Key - 1] += path.Value;
                    }
                    else
                    {
                        nextPaths.Add(path.Key - 1, 1);
                    }

                    if (nextPaths.ContainsKey(path.Key + 1))
                    {
                        nextPaths[path.Key + 1] += path.Value;
                    }
                    else
                    {
                        nextPaths.Add(path.Key + 1, 1);
                    }
                }

                paths = nextPaths;
            }
        }

        Console.WriteLine(paths.Sum(p => p.Value));
    }

    private static List<Coord> ParseInputs(string[] inputs)
    {
        List<Coord> map = new();

        for (int y = 0; y < inputs.Length; y++)
        {
            for (int x = 0; x < inputs[0].Length; x++)
            {
                Coord coord = new Coord
                {
                    X = x,
                    Y = y,
                    Value = inputs[y][x],
                    IsStart = inputs[y][x] == 'S'
                };

                map.Add(coord);
            }
        }

        return map;
    }
}