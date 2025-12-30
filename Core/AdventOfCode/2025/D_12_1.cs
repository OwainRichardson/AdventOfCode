

using AdventOfCode._2025.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2025;

public static class D_12_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day12.txt").ToArray();

        List<Present> presents = ParseInputs(inputs);

        PrintPresent(presents[0].PositionOne);
    }

    private static void PrintPresent(List<Coord> present)
    {
        Console.WriteLine();

        for (int y = 0; y <= 2; y++)
        {
            for (int x = 0; x <= 2; x++)
            {
                if (present.Any(p => p.X == x && p.Y == y))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }
    }

    private static List<Present> ParseInputs(string[] inputs)
    {
        List<Present> presents = new();

        string pattern = @"^(\d{1})\:$";
        Regex regex = new(pattern);

        for (int i = 0; i < inputs.Length; i++)
        {
            if (regex.IsMatch(inputs[i]))
            {
                Match match = regex.Match(inputs[i]);

                int id = int.Parse(match.Groups[1].Value);

                string[] map = [
                    inputs[i + 1],
                    inputs[i + 2],
                    inputs[i + 3]
                    ];

                Present present = new(map);
                present.Id = id;

                presents.Add(present);
            }
        }

        return presents;
    }
}