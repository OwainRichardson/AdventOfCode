using AdventOfCode._2018.Models;
using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2024
{
    public static class D_15_1
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day15.txt");

            (string instructions, List<WarehouseCoord> warehouse) = ParseInputs(inputs);

            FollowInstructions(warehouse, instructions);

            //PrintWarehouse(warehouse);

            long total = CalculateGpsTotal(warehouse);

            return total.ToString();
        }

        private static long CalculateGpsTotal(List<WarehouseCoord> warehouse)
        {
            long total = 0;

            foreach (WarehouseCoord pacakge in warehouse.Where(w => w.IsPackage))
            {
                total += ((100 * pacakge.Y) + pacakge.X);
            }

            return total;
        }

        private static void FollowInstructions(List<WarehouseCoord> warehouse, string instructions)
        {
            foreach (char c in instructions)
            {
                var robot = warehouse.Single(w => w.IsRobot);
                WarehouseCoord nextCoord = null;

                if (c == '<')
                {
                    nextCoord = warehouse.First(w => w.X == robot.X - 1 && w.Y == robot.Y);
                }
                else if (c == '^')
                {
                    nextCoord = warehouse.First(w => w.X == robot.X && w.Y == robot.Y - 1);
                }
                else if (c == '>')
                {
                    nextCoord = warehouse.First(w => w.X == robot.X + 1 && w.Y == robot.Y);
                }
                else if (c == 'v')
                {
                    nextCoord = warehouse.First(w => w.X == robot.X && w.Y == robot.Y + 1);
                }

                if (nextCoord == null) throw new InvalidOperationException();

                //PrintWarehouse(warehouse);

                if (nextCoord.IsWall) continue;
                if (nextCoord.IsEmpty)
                {
                    nextCoord.IsRobot = true;
                    nextCoord.IsEmpty = false;
                    robot.IsRobot = false;
                    robot.IsEmpty = true;
                }
                if (nextCoord.IsPackage && CanMovePackage(warehouse, nextCoord, c))
                {
                    MovePackageQueue(warehouse, nextCoord, c);
                    nextCoord.IsPackage = false;
                    nextCoord.IsEmpty = false;
                    nextCoord.IsRobot = true;
                    robot.IsRobot = false;
                    robot.IsEmpty = true;
                }

            }
        }

        private static void PrintWarehouse(List<WarehouseCoord> warehouse)
        {
            Console.WriteLine();

            for (int y = 0; y <= warehouse.Max(y => y.Y); y++)
            {
                for (int x = 0; x <= warehouse.Max(x => x.X); x++)
                {
                    WarehouseCoord coord = warehouse.First(w => w.X == x && w.Y == y);

                    if (coord.IsWall) Console.Write("#");
                    else if (coord.IsEmpty) Console.Write(".");
                    else if (coord.IsRobot) Console.Write("@");
                    else if (coord.IsPackage) Console.Write("O");
                }

                Console.WriteLine();
            }
        }

        private static void MovePackageQueue(List<WarehouseCoord> warehouse, WarehouseCoord coord, char c)
        {
            WarehouseCoord nextCoord = coord;
            bool stopMoving = false;
            while (!stopMoving)
            {

                if (c == '<')
                {
                    while (!stopMoving)
                    {
                        nextCoord = warehouse.First(w => w.X == coord.X - 1 && w.Y == coord.Y);
                        if (nextCoord.IsEmpty) stopMoving = true;
                        nextCoord.IsPackage = true;
                        nextCoord.IsEmpty = false;

                        coord = nextCoord;
                    }
                }
                else if (c == '^')
                {
                    while (!stopMoving)
                    {
                        nextCoord = warehouse.First(w => w.X == coord.X && w.Y == coord.Y - 1);
                        if (nextCoord.IsEmpty) stopMoving = true;
                        nextCoord.IsPackage = true;
                        nextCoord.IsEmpty = false;

                        coord = nextCoord;
                    }
                }
                else if (c == '>')
                {
                    while (!stopMoving)
                    {
                        nextCoord = warehouse.First(w => w.X == coord.X + 1 && w.Y == coord.Y);
                        if (nextCoord.IsEmpty) stopMoving = true;
                        nextCoord.IsPackage = true;
                        nextCoord.IsEmpty = false;

                        coord = nextCoord;
                    }
                }
                else if (c == 'v')
                {
                    while (!stopMoving)
                    {
                        nextCoord = warehouse.First(w => w.X == coord.X && w.Y == coord.Y + 1);
                        if (nextCoord.IsEmpty) stopMoving = true;
                        nextCoord.IsPackage = true;
                        nextCoord.IsEmpty = false;

                        coord = nextCoord;
                    }
                }
            }
        }

        private static bool CanMovePackage(List<WarehouseCoord> warehouse, WarehouseCoord coord, char c)
        {
            WarehouseCoord nextCoord = null;
            if (c == '<')
            {
                nextCoord = warehouse.First(w => w.X == coord.X - 1 && w.Y == coord.Y);
            }
            else if (c == '^')
            {
                nextCoord = warehouse.First(w => w.X == coord.X && w.Y == coord.Y - 1);
            }
            else if (c == '>')
            {
                nextCoord = warehouse.First(w => w.X == coord.X + 1 && w.Y == coord.Y);
            }
            else if (c == 'v')
            {
                nextCoord = warehouse.First(w => w.X == coord.X && w.Y == coord.Y + 1);
            }
            else
            {
                throw new InvalidOperationException();
            }

            if (nextCoord.IsPackage) return CanMovePackage(warehouse, nextCoord, c);

            if (nextCoord.IsWall) return false;
            if (nextCoord.IsEmpty) return true;

            throw new InvalidOperationException();
        }

        private static (string instructions, List<WarehouseCoord> warehouse) ParseInputs(string[] inputs)
        {
            List<WarehouseCoord> warehouse = new List<WarehouseCoord>();
            int y = 0;
            bool parseMap = true;
            StringBuilder instructions = new StringBuilder();

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    parseMap = false;
                }

                if (parseMap)
                {
                    int x = 0;
                    foreach (char c in input)
                    {
                        WarehouseCoord w = new WarehouseCoord
                        {
                            X = x,
                            Y = y,
                            Value = c.ToString()
                        };

                        if (c == '#')
                        {
                            w.IsWall = true;
                        }
                        else if (c == 'O')
                        {
                            w.IsPackage = true;
                        }
                        else if (c == '@')
                        {
                            w.IsRobot = true;
                        }
                        else if (c == '.')
                        {
                            w.IsEmpty = true;
                        }

                        warehouse.Add(w);

                        x++;
                    }

                    y++;
                }

                if (!parseMap)
                {
                    instructions.Append(input);
                }
            }

            return (instructions.ToString(), warehouse);
        }
    }
}