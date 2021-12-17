using AdventOfCode._2020.Models;
using System;
using System.IO;

namespace AdventOfCode._2020
{
    public static class D_12_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day12.txt");

            int x = 0;
            int y = 0;
            int direction = Directions.East;

            foreach (string input in inputs)
            {
                int amount = 0;
                switch (input.Substring(0, 1).ToLower())
                {
                    case "n":
                        amount = int.Parse(input.Substring(1));
                        y += amount;
                        break;
                    case "e":
                        amount = int.Parse(input.Substring(1));
                        x += amount;
                        break;
                    case "s":
                        amount = int.Parse(input.Substring(1));
                        y -= amount;
                        break;
                    case "w":
                        amount = int.Parse(input.Substring(1));
                        x -= amount;
                        break;
                    case "l":
                        amount = int.Parse(input.Substring(1)) / 90;
                        direction -= amount;

                        if (direction < 0) direction += 4;
                        break;
                    case "r":
                        amount = int.Parse(input.Substring(1)) / 90;
                        direction += amount;

                        if (direction > 3) direction -= 4;
                        break;
                    case "f":
                        amount = int.Parse(input.Substring(1));
                        switch (direction)
                        {
                            case Directions.North:
                                y += amount;
                                break;
                            case Directions.East:
                                x += amount;
                                break;
                            case Directions.South:
                                y -= amount;
                                break;
                            case Directions.West:
                                x -= amount;
                                break;
                            default:
                                throw new Exception();
                        }
                        break;
                    default:
                        throw new Exception();
                }
            }

            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }
    }
}
