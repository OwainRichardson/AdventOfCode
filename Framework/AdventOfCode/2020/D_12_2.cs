using System;
using System.IO;

namespace AdventOfCode._2020
{
    public static class D_12_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day12.txt");

            int x = 0;
            int y = 0;
            int waypointDistanceX = 10;
            int waypointDistanceY = 1;

            foreach (string input in inputs)
            {
                int amount = 0;
                int newY = 0;
                int newX = 0;

                switch (input.Substring(0, 1).ToLower())
                {
                    case "n":
                        amount = int.Parse(input.Substring(1));
                        waypointDistanceY += amount;
                        break;
                    case "e":
                        amount = int.Parse(input.Substring(1));
                        waypointDistanceX += amount;
                        break;
                    case "s":
                        amount = int.Parse(input.Substring(1));
                        waypointDistanceY -= amount;
                        break;
                    case "w":
                        amount = int.Parse(input.Substring(1));
                        waypointDistanceX -= amount;
                        break;
                    case "l":
                        amount = int.Parse(input.Substring(1)) / 90;

                        
                        switch (amount)
                        {
                            case 1:
                                newY += waypointDistanceX;
                                newX -= waypointDistanceY;
                                break;
                            case 2:
                                newX -= waypointDistanceX;
                                newY -= waypointDistanceY;
                                break;
                            case 3:
                                newY -= waypointDistanceX;
                                newX += waypointDistanceY;
                                break;
                        }

                        waypointDistanceX = newX;
                        waypointDistanceY = newY;

                        break;
                    case "r":
                        amount = int.Parse(input.Substring(1)) / 90;

                        switch (amount)
                        {
                            case 1:
                                newY -= waypointDistanceX;
                                newX += waypointDistanceY;
                                break;
                            case 2:
                                newX -= waypointDistanceX;
                                newY -= waypointDistanceY;
                                break;
                            case 3:
                                newY += waypointDistanceX;
                                newX -= waypointDistanceY;
                                break;
                        }

                        waypointDistanceX = newX;
                        waypointDistanceY = newY;

                        break;
                    case "f":
                        amount = int.Parse(input.Substring(1));

                        x += (amount * waypointDistanceX);
                        y += (amount * waypointDistanceY);

                        break;
                    default:
                        throw new Exception();
                }
            }

            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }
    }
}
