using AdventOfCode._2018.Models;
using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;
using AdventOfCode._2024.Models.Enums;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2024
{
    public static class D_15_2
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

            for (int y = 0; y <= warehouse.Max(w => w.Y); y++)
            {
                for (int x = 0; x <= warehouse.Max(w => w.X); x++)
                {
                    var coord = warehouse.First(w => w.X == x && w.Y == y);
                    if (coord.IsPackage)
                    {
                        int distanceToSide = 999999999;
                        if (coord.Side == PackageSide.Left)
                        {
                            if (coord.X < distanceToSide)
                            {
                                distanceToSide = coord.X;
                            }
                        }
                        else
                        {
                            if (warehouse.Max(w => w.X) - (coord.X + 1) < distanceToSide)
                            {
                                distanceToSide = warehouse.Max(w => w.X) - (coord.X + 1);
                            }
                        }

                        total += ((coord.Y * 100) + distanceToSide);

                        x++;
                    }
                }
            }

            return total;
        }

        private static void FollowInstructions(List<WarehouseCoord> warehouse, string instructions)
        {
            int index = 0;
            try
            {
                foreach (char c in instructions)
                {
                    //PrintWarehouse(warehouse);

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

                    if (nextCoord.IsEmpty)
                    {
                        nextCoord.IsRobot = true;
                        nextCoord.IsEmpty = false;
                        nextCoord.Value = "@";
                        robot.IsRobot = false;
                        robot.IsEmpty = true;
                        robot.Value = ".";
                    }
                    else if (nextCoord.IsPackage && CanMovePackage(warehouse, robot, c))
                    {
                        MovePackageQueue(warehouse, robot, c);

                        nextCoord.IsPackage = false;
                        nextCoord.IsEmpty = false;
                        nextCoord.IsRobot = true;
                        nextCoord.Value = "@";

                        robot.IsRobot = false;
                        robot.IsEmpty = true;
                        robot.Value = ".";
                    }

                    //PrintWarehouse(warehouse);
                    if (warehouse.Count(w => w.Value == "@") > 1 || warehouse.Count(w => w.IsRobot) > 1)
                    {
                        var o = 0;
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                var b = 9;
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

                    Console.Write(coord.Value);
                }

                Console.WriteLine();
            }
        }

        private static void MovePackageQueue(List<WarehouseCoord> warehouse, WarehouseCoord robot, char c)
        {
            if (c == '<')
            {
                List<WarehouseCoord> packagesToMove = warehouse.Where(w => w.X < robot.X && w.Y == robot.Y).OrderByDescending(w => w.X).ToList();

                int indexOfSpace = packagesToMove.FindIndex(p => p.Value == ".");

                if (indexOfSpace > -1)
                {
                    packagesToMove.RemoveRange(indexOfSpace, packagesToMove.Count - indexOfSpace);
                }

                foreach (WarehouseCoord package in packagesToMove.OrderBy(p => p.X))
                {
                    var next = warehouse.First(w => w.X == package.X - 1 && w.Y == package.Y);
                    if (next.IsEmpty)
                    {
                        next.Value = "[";
                        next.Side = PackageSide.Left;
                        next.IsEmpty = false;
                    }
                    else
                    {
                        next.Side = next.Side == PackageSide.Left ? PackageSide.Right : PackageSide.Left;
                        next.Value = next.Side == PackageSide.Left ? "[" : "]";
                    }

                    next.IsPackage = true;
                }
            }
            else if (c == '^')
            {
                MoveUp(warehouse, robot);
            }
            else if (c == '>')
            {
                List<WarehouseCoord> packagesToMove = warehouse.Where(w => w.X > robot.X && w.Y == robot.Y).OrderBy(w => w.X).ToList();

                int indexOfSpace = packagesToMove.FindIndex(p => p.Value == ".");

                if (indexOfSpace > -1)
                {
                    packagesToMove.RemoveRange(indexOfSpace, packagesToMove.Count - indexOfSpace);
                }

                foreach (WarehouseCoord package in packagesToMove.OrderByDescending(p => p.X))
                {
                    var next = warehouse.First(w => w.X == package.X + 1 && w.Y == package.Y);
                    if (next.IsEmpty)
                    {
                        next.Value = "]";
                        next.Side = PackageSide.Right;
                        next.IsEmpty = false;
                    }
                    else
                    {
                        next.Side = next.Side == PackageSide.Left ? PackageSide.Right : PackageSide.Left;
                        next.Value = next.Side == PackageSide.Left ? "[" : "]";
                    }

                    next.IsPackage = true;
                }
            }
            else if (c == 'v')
            {
                MoveDown(warehouse, robot);
            }
        }

        private static void MoveDown(List<WarehouseCoord> warehouse, WarehouseCoord start)
        {
            WarehouseCoord right;
            WarehouseCoord left;

            WarehouseCoord downCoord = warehouse.First(w => w.X == start.X && w.Y == start.Y + 1);
            right = downCoord.Side == PackageSide.Left ? warehouse.First(w => w.X == start.X + 1 && w.Y == start.Y + 1) : downCoord;
            left = downCoord.Side == PackageSide.Left ? downCoord : warehouse.First(w => w.X == start.X - 1 && w.Y == start.Y + 1);

            var rightDown = warehouse.FirstOrDefault(w => w.X == right.X && w.Y == right.Y + 1);
            var leftDown = warehouse.FirstOrDefault(w => w.X == left.X && w.Y == left.Y + 1);

            if (rightDown.IsPackage)
            {
                MoveDown(warehouse, right);
            }
            if (leftDown.IsPackage)
            {
                MoveDown(warehouse, left);
            }

            rightDown.IsPackage = true;
            rightDown.Side = right.Side;
            rightDown.Value = right.Value;
            rightDown.IsEmpty = false;
            leftDown.IsPackage = true;
            leftDown.Side = left.Side;
            leftDown.Value = left.Value;
            leftDown.IsEmpty = false;

            right.IsPackage = false;
            right.IsEmpty = true;
            right.Value = ".";
            left.IsPackage = false;
            left.IsEmpty = true;
            left.Value = ".";
        }

        private static void MoveUp(List<WarehouseCoord> warehouse, WarehouseCoord start)
        {
            WarehouseCoord right;
            WarehouseCoord left;

            WarehouseCoord upCoord = warehouse.First(w => w.X == start.X && w.Y == start.Y - 1);
            right = upCoord.Side == PackageSide.Left ? warehouse.First(w => w.X == start.X + 1 && w.Y == start.Y - 1) : upCoord;
            left = upCoord.Side == PackageSide.Left ? upCoord : warehouse.First(w => w.X == start.X - 1 && w.Y == start.Y - 1);

            var rightUp = warehouse.FirstOrDefault(w => w.X == right.X && w.Y == right.Y - 1);
            var leftUp = warehouse.FirstOrDefault(w => w.X == left.X && w.Y == left.Y - 1);

            if (rightUp.IsPackage)
            {
                MoveUp(warehouse, right);
            }
            if (leftUp.IsPackage)
            {
                MoveUp(warehouse, left);
            }

            rightUp.IsPackage = true;
            rightUp.Side = right.Side;
            rightUp.Value = right.Value;
            rightUp.IsEmpty = false;
            leftUp.IsPackage = true;
            leftUp.Side = left.Side;
            leftUp.Value = left.Value;
            leftUp.IsEmpty = false;

            right.IsPackage = false;
            right.IsEmpty = true;
            right.Value = ".";
            left.IsPackage = false;
            left.IsEmpty = true;
            left.Value = ".";
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

            if (nextCoord.IsPackage && (c == '<' || c == '>')) return CanMovePackage(warehouse, nextCoord, c);

            if (nextCoord.IsPackage && (c == '^' || c == 'v'))
            {
                if (nextCoord.Side == PackageSide.Left)
                {
                    var rightCoord = warehouse.First(w => w.Y == nextCoord.Y && w.X == nextCoord.X + 1);
                    return CanMovePackage(warehouse, nextCoord, c) && CanMovePackage(warehouse, rightCoord, c);
                }
                else
                {
                    var leftCoord = warehouse.First(w => w.Y == nextCoord.Y && w.X == nextCoord.X - 1);
                    return CanMovePackage(warehouse, nextCoord, c) && CanMovePackage(warehouse, leftCoord, c);
                }
            }

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
                        WarehouseCoord w2 = new WarehouseCoord
                        {
                            X = x + 1,
                            Y = y,
                            Value = c.ToString(),
                        };

                        if (c == '#')
                        {
                            w.IsWall = true;
                            w2.IsWall = true;
                        }
                        else if (c == 'O')
                        {
                            w.Value = "[";
                            w.IsPackage = true;
                            w.Side = PackageSide.Left;
                            w2.Value = "]";
                            w2.IsPackage = true;
                            w2.Side = PackageSide.Right;
                        }
                        else if (c == '@')
                        {
                            w.IsRobot = true;
                            w2.Value = ".";
                            w2.IsEmpty = true;
                        }
                        else if (c == '.')
                        {
                            w.IsEmpty = true;
                            w2.IsEmpty = true;
                        }

                        warehouse.Add(w);
                        warehouse.Add(w2);

                        x += 2;
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