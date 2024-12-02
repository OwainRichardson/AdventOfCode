using AdventOfCode._2018.Models;
using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Net.WebSockets;
using System.Runtime;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2023
{
    public static class D_18_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day18.txt").ToArray();

            List<DigInstruction> instructions = ParseInputToInstructions(inputs);
            HashSet<string> lines = new HashSet<string>
            {
                "0:0"
            };

            DrawInstructions(instructions, lines);

            int total = 0;



            bool coordCounted = true;

            int y = lines.Select(l => int.Parse(l.Split(':')[0])).Min(y => y) + 1;
            int x = lines.Where(l => int.Parse(l.Split(':')[0]) == y).Min(l => int.Parse(l.Split(':')[1])) + 1;

            HashSet<string> inners = new HashSet<string>
            {
                $"{y}:{x}"
            };

            while (coordCounted)
            {
                coordCounted = false;
                HashSet<string> coordsToAdd = new HashSet<string>();

                foreach (var coord in inners.TakeLast(1000))
                {
                    int[] split = coord.Split(':').Select(c => int.Parse(c)).ToArray();
                    y = split[0];
                    x = split[1];

                    bool upCoord = lines.Contains($"{y - 1}:{x}") || inners.Contains($"{y - 1}:{x}");
                    var downCoord = lines.Contains($"{y + 1}:{x}") || inners.Contains($"{y + 1}:{x}");
                    var leftCoord = lines.Contains($"{y}:{x - 1}") || inners.Contains($"{y}:{x - 1}");
                    var rightCoord = lines.Contains($"{y}:{x + 1}") || inners.Contains($"{y}:{x + 1}");

                    if (!upCoord && !coordsToAdd.Contains($"{y - 1}:{x}")) coordsToAdd.Add($"{y - 1}:{x}");
                    if (!downCoord && !coordsToAdd.Contains($"{y + 1}:{x}")) coordsToAdd.Add($"{y + 1}:{x}");
                    if (!leftCoord && !coordsToAdd.Contains($"{y}:{x - 1}")) coordsToAdd.Add($"{y}:{x - 1}");
                    if (!rightCoord && !coordsToAdd.Contains($"{y}:{x + 1}")) coordsToAdd.Add($"{y}:{x + 1}");
                }

                inners = inners.Concat(coordsToAdd).ToHashSet();

                if (coordsToAdd.Any()) coordCounted = true;
            }

            total = lines.Count;
            total += inners.Count;

            Console.WriteLine(total);

            //DrawCoords(lines, inners);
        }

        private static void DrawInstructions(List<DigInstruction> instructions, HashSet<string> lines)
        {
            int x = 0;
            int y = 0;

            foreach (DigInstruction instruction in instructions)
            {
                int number = 1;

                while (number <= instruction.Number)
                {
                    switch (instruction.Direction)
                    {
                        case Directions.Up:
                            y -= 1;
                            break;
                        case Directions.Down:
                            y += 1;
                            break;
                        case Directions.Right:
                            x += 1;
                            break;
                        case Directions.Left:
                            x -= 1;
                            break;
                        default:
                            throw new InvalidOperationException();
                    }

                    if (!lines.Contains($"{y}:{x}"))
                    {
                        lines.Add($"{y}:{x}");
                    }
                    number++;
                }
            }
        }

        private static void DrawCoords(HashSet<string> lines, HashSet<string> inners)
        {
            Console.WriteLine();

            int minY = lines.Select(l => int.Parse(l.Split(':')[0])).Min(y => y);
            int maxY = lines.Select(l => int.Parse(l.Split(':')[0])).Max(y => y);

            int minX = lines.Select(l => int.Parse(l.Split(':')[1])).Min(x => x);
            int maxX = lines.Select(l => int.Parse(l.Split(':')[1])).Max(x => x);

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (lines.Contains($"{y}:{x}"))
                    {
                        Console.Write("#");
                    }
                    else if (inners.Contains($"{y}:{x}"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("#");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static List<DigInstruction> ParseInputToInstructions(string[] inputs)
        {
            string pattern = @"^(\w{1})\W(\d+)\W\((.+)\)$";
            Regex regex = new Regex(pattern);

            List<DigInstruction> instructions = new List<DigInstruction>();

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);

                DigInstruction instruction = new DigInstruction
                {
                    Direction = ParseDirection(match.Groups[1].Value),
                    Number = int.Parse(match.Groups[2].Value),
                    HexCode = match.Groups[3].Value
                };

                instructions.Add(instruction);
            }

            return instructions;
        }

        private static Directions ParseDirection(string value)
        {
            switch (value)
            {
                case "R":
                    return Directions.Right;
                case "L":
                    return Directions.Left;
                case "U":
                    return Directions.Up;
                case "D":
                    return Directions.Down;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}