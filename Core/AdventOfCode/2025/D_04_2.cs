using AdventOfCode._2025.Models;

namespace AdventOfCode._2025;

public static class D_04_2
{
    public static void Execute()
    {
        int maxRolls = 4;
        int totalAccessibleRolls = 0;
        string[] inputs = File.ReadAllLines(@"2025\Data\day04.txt").ToArray();

        List<Coord> map = ParseInputs(inputs);
        List<Coord> coordsToRemove = new();
        bool changes = true;

        while (changes)
        {
            changes = false;

            foreach (Coord coord in map.Where(c => c.Value == '@'))
            {
                List<int> yCoords = [coord.Y - 1, coord.Y, coord.Y + 1];
                List<int> xCoords = [coord.X - 1, coord.X, coord.X + 1];

                List<Coord> adjacentCoords = map.Where(c => xCoords.Contains(c.X)
                                                        && yCoords.Contains(c.Y)
                                                        && c.Value == '@')
                                                .ToList();

                adjacentCoords.Remove(coord);

                if (adjacentCoords.Count < maxRolls)
                {
                    totalAccessibleRolls += 1;
                    coordsToRemove.Add(coord);
                    changes = true;
                }
            }

            coordsToRemove.ForEach(coord =>
            {
                Coord mapCoord = map.First(c => c.X == coord.X && c.Y == coord.Y);
                mapCoord.Value = '.';
            });
        }

        Console.WriteLine(totalAccessibleRolls);
    }

    private static List<Coord> ParseInputs(string[] inputs)
    {
        List<Coord> map = new();

        for (int y = 0; y < inputs.Length; y++)
        {
            for (int x = 0; x < inputs[y].Length; x++)
            {
                Coord coord = new()
                {
                    X = x,
                    Y = y,
                    Value = inputs[y][x]
                };

                map.Add(coord);
            }
        }

        return map;
    }
}