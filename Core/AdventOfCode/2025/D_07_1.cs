

using AdventOfCode._2025.Models;

namespace AdventOfCode._2025;

public static class D_07_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day07.txt").ToArray();

        List<Coord> map = ParseInputs(inputs);

        int currentBeamId = 1;
        List<Coord> beams = map.Where(m => m.IsStart).ToList();
        beams.Single().Id = currentBeamId;

        int numberOfSplits = 0;

        for (int y = 0; y < map.Max(m => m.Y); y++)
        {
            List<Coord> beamsToAdd = new();
            List<Coord> beamsToRemove = new();

            foreach (Coord beam in beams)
            {
                Coord coordBelowBeam = map.First(m => m.Y == beam.Y + 1 && m.X == beam.X);

                if (coordBelowBeam == null)
                {
                    continue;
                }

                if (coordBelowBeam.Value == '.')
                {
                    if (!beams.Any(b => b.X == beam.X && b.Y == beam.Y + 1) && !beamsToAdd.Any(b => b.X == beam.X && b.Y == beam.Y + 1))
                    {
                        beam.Y += 1;
                    }
                    else
                    {
                        beamsToRemove.Add(beam);
                    }

                    continue;
                }

                if (coordBelowBeam.Value == '^')
                {
                    numberOfSplits++;
                    beamsToRemove.Add(beam);

                    if (!beams.Any(b => b.X == beam.X - 1 && b.Y == beam.Y + 1) && !beamsToAdd.Any(b => b.X == beam.X - 1 && b.Y == beam.Y + 1))
                    {
                        currentBeamId += 1;
                        beamsToAdd.Add(new() { Id = currentBeamId, X = beam.X - 1, Y = beam.Y + 1 });
                    }
                    if (!beams.Any(b => b.X == beam.X + 1 && b.Y == beam.Y + 1) && !beamsToAdd.Any(b => b.X == beam.X + 1 && b.Y == beam.Y + 1))
                    {
                        currentBeamId += 1;
                        beamsToAdd.Add(new() { Id = currentBeamId, X = beam.X + 1, Y = beam.Y + 1 });
                    }
                }
            }

            beams = beams.Where(b => !beamsToRemove.Select(btr => btr.Id).Contains(b.Id)).ToList();
            beams.AddRange(beamsToAdd);
        }

        Console.WriteLine(numberOfSplits);
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