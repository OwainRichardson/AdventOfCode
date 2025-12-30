using AdventOfCode._2025.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2025;

public static class D_12_1
{
    private static readonly List<ConsoleColor> Colours = [ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Magenta, ConsoleColor.DarkYellow];

    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day12.txt").ToArray();

        var (presents, presentInstructions) = ParseInputs(inputs);

        foreach (PresentInstruction instruction in presentInstructions)
        {
            bool triedEverything = false;
            char[,] grid = new char[instruction.Long, instruction.Wide];
            DefaultGrid(grid, instruction);

            while (!triedEverything)
            {
                for (int index = 0; index <= 5; index++)
                {
                    int required = instruction.RequiredShapes[index];

                    if (required == 0) continue;
                    int added = 0;

                    while (added < required)
                    {
                        bool addedSuccessfully = AddShapeToGrid(grid, presents.First(p => p.Id == index), instruction);
                        if (addedSuccessfully)
                        {
                            added++;

                            //PrintGrid(grid, instruction);
                        }
                        else
                        {
                            var o = 0;
                        }
                    }
                }
            }
        }
    }

    private static void DefaultGrid(char[,] grid, PresentInstruction instruction)
    {
        for (int y = 0; y <= instruction.Long - 1; y++)
        {
            for (int x = 0; x <= instruction.Wide - 1; x++)
            {
                grid[y, x] = '.';
            }
        }
    }

    private static void PrintGrid(char[,] grid, PresentInstruction instruction)
    {
        Console.WriteLine();

        for (int y = 0; y <= instruction.Long - 1; y++)
        {
            for (int x = 0; x <= instruction.Wide - 1; x++)
            {
                Console.Write(grid[y, x]);
            }

            Console.WriteLine();
        }
    }

    private static bool AddShapeToGrid(char[,] grid, Present present, PresentInstruction instruction)
    {
        for (int y = 0; y < instruction.Long - 2; y++)
        {
            for (int x = 0; x < instruction.Wide - 2; x++)
            {
                foreach (Coord coord in present.PositionOne)
                {
                    if (present.PositionOne.All(po => grid[po.Y + y, po.X + x] != '#'))
                    {
                        present.PositionOne.ForEach(po =>
                        {
                            grid[po.Y + y, po.X + x] = '#';
                        });

                        return true;
                    }
                    else if (present.PositionTwo.All(po => grid[po.Y + y, po.X + x] != '#'))
                    {
                        present.PositionTwo.ForEach(po =>
                        {
                            grid[po.Y + y, po.X + x] = '#';
                        });

                        return true;
                    }
                    else if (present.PositionThree.All(po => grid[po.Y + y, po.X + x] != '#'))
                    {
                        present.PositionThree.ForEach(po =>
                        {
                            grid[po.Y + y, po.X + x] = '#';
                        });

                        return true;
                    }
                    else if (present.PositionFour.All(po => grid[po.Y + y, po.X + x] != '#'))
                    {
                        present.PositionFour.ForEach(po =>
                        {
                            grid[po.Y + y, po.X + x] = '#';
                        });

                        return true;
                    }
                }
            }
        }

        return false;
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

    private static (List<Present> Presents, List<PresentInstruction> PresentInstructions) ParseInputs(string[] inputs)
    {
        List<Present> presents = new();
        List<PresentInstruction> presentInstructions = new();

        string presentsPattern = @"^(\d{1})\:$";
        Regex presentsRegex = new(presentsPattern);

        string instructionPattern = @"^(\d+)x(\d+)\: (\d+) (\d+) (\d+) (\d+) (\d+) (\d+)$";
        Regex instructionsRegex = new(instructionPattern);

        bool parsePresents = true;

        for (int i = 0; i < inputs.Length; i++)
        {
            if (string.IsNullOrEmpty(inputs[i])) continue;

            if (parsePresents)
            {
                if (presentsRegex.IsMatch(inputs[i]))
                {
                    Match match = presentsRegex.Match(inputs[i]);

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
            else
            {
                Match match = instructionsRegex.Match(inputs[i]);
                presentInstructions.Add(new()
                {
                    Wide = int.Parse(match.Groups[1].Value),
                    Long = int.Parse(match.Groups[2].Value),
                    RequiredShapes = [
                        int.Parse(match.Groups[3].Value),
                        int.Parse(match.Groups[4].Value),
                        int.Parse(match.Groups[5].Value),
                        int.Parse(match.Groups[6].Value),
                        int.Parse(match.Groups[7].Value),
                        int.Parse(match.Groups[8].Value),
                        ]
                });
            }

            if (presents.Count == 6 && parsePresents)
            {
                parsePresents = false;
                i += 3;
            }
        }

        return (presents, presentInstructions);
    }
}