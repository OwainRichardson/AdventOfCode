using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class D_11_1
    {
        public static int _globalInput = 0;
        public static long _relativeBase = 0;
        public static long[] input = new long[] { 3, 8, 1005, 8, 304, 1106, 0, 11, 0, 0, 0, 104, 1, 104, 0, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 1002, 8, 1, 29, 2, 103, 1, 10, 1, 106, 18, 10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 102, 1, 8, 59, 2, 102, 3, 10, 2, 1101, 12, 10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 101, 0, 8, 88, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 1, 8, 10, 4, 10, 101, 0, 8, 110, 2, 108, 9, 10, 1006, 0, 56, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 101, 0, 8, 139, 1, 108, 20, 10, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10, 108, 0, 8, 10, 4, 10, 102, 1, 8, 165, 1, 104, 9, 10, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10, 1008, 8, 0, 10, 4, 10, 1001, 8, 0, 192, 2, 9, 14, 10, 2, 1103, 5, 10, 1, 1108, 5, 10, 3, 8, 1002, 8, -1, 10, 101, 1, 10, 10, 4, 10, 1008, 8, 1, 10, 4, 10, 102, 1, 8, 226, 1006, 0, 73, 1006, 0, 20, 1, 1106, 11, 10, 1, 1105, 7, 10, 3, 8, 102, -1, 8, 10, 1001, 10, 1, 10, 4, 10, 108, 0, 8, 10, 4, 10, 1001, 8, 0, 261, 3, 8, 102, -1, 8, 10, 101, 1, 10, 10, 4, 10, 108, 1, 8, 10, 4, 10, 1002, 8, 1, 283, 101, 1, 9, 9, 1007, 9, 1052, 10, 1005, 10, 15, 99, 109, 626, 104, 0, 104, 1, 21101, 48062899092, 0, 1, 21101, 0, 321, 0, 1105, 1, 425, 21101, 936995300108, 0, 1, 21101, 0, 332, 0, 1106, 0, 425, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 1, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 1, 21102, 209382902951, 1, 1, 21101, 379, 0, 0, 1106, 0, 425, 21102, 179544747200, 1, 1, 21102, 390, 1, 0, 1106, 0, 425, 3, 10, 104, 0, 104, 0, 3, 10, 104, 0, 104, 0, 21102, 1, 709488292628, 1, 21102, 1, 413, 0, 1106, 0, 425, 21101, 0, 983929868648, 1, 21101, 424, 0, 0, 1105, 1, 425, 99, 109, 2, 22101, 0, -1, 1, 21102, 40, 1, 2, 21102, 456, 1, 3, 21101, 446, 0, 0, 1106, 0, 489, 109, -2, 2106, 0, 0, 0, 1, 0, 0, 1, 109, 2, 3, 10, 204, -1, 1001, 451, 452, 467, 4, 0, 1001, 451, 1, 451, 108, 4, 451, 10, 1006, 10, 483, 1102, 0, 1, 451, 109, -2, 2105, 1, 0, 0, 109, 4, 1201, -1, 0, 488, 1207, -3, 0, 10, 1006, 10, 506, 21102, 1, 0, -3, 21202, -3, 1, 1, 21201, -2, 0, 2, 21101, 0, 1, 3, 21101, 525, 0, 0, 1105, 1, 530, 109, -4, 2105, 1, 0, 109, 5, 1207, -3, 1, 10, 1006, 10, 553, 2207, -4, -2, 10, 1006, 10, 553, 21202, -4, 1, -4, 1105, 1, 621, 21201, -4, 0, 1, 21201, -3, -1, 2, 21202, -2, 2, 3, 21102, 1, 572, 0, 1106, 0, 530, 21201, 1, 0, -4, 21101, 0, 1, -1, 2207, -4, -2, 10, 1006, 10, 591, 21102, 0, 1, -1, 22202, -2, -1, -2, 2107, 0, -3, 10, 1006, 10, 613, 22101, 0, -1, 1, 21101, 0, 613, 0, 106, 0, 488, 21202, -2, -1, -2, 22201, -4, -2, -4, 109, -5, 2106, 0, 0 };
        public static List<RobotCoord> coordsVisited = new List<RobotCoord>();

        public static void Execute()
        {
            RobotCoord startPoint = new RobotCoord { X = 0, Y = 0, Current = true };
            coordsVisited.Add(startPoint);
            long executionPointer = 0;
            long colour = 0;
            long direction = 0;
            int currentDirection = Direction.Up;
            int i = 1;
            while (input[executionPointer] != OpcodeActions.End)
            {
                RobotCoord currentCoord = coordsVisited.First(x => x.Current);
                _globalInput = currentCoord.Colour == 0 ? 0 : 1;

                colour = ParseOpcode(input, ref executionPointer);
                if (colour == -1)
                {
                    break;
                }

                if ((int)colour != currentCoord.Colour)
                {
                    currentCoord.Colour = (int)colour;

                    currentCoord.ColourChanged = true;
                }
                executionPointer += 2;

                direction = ParseOpcode(input, ref executionPointer);
                CalculateDirection(direction, ref currentDirection, currentCoord);

                executionPointer += 2;
            }

            Console.WriteLine($"Number of painted squares: {coordsVisited.Count(x => x.ColourChanged)}");
        }

        private static void CalculateDirection(long direction, ref int currentDirection, RobotCoord currentCoord)
        {
            switch (direction)
            {
                case 0:
                    if (currentDirection == Direction.Up)
                    {
                        currentDirection = Direction.Left;
                    }
                    else
                    {
                        currentDirection -= 1;
                    }

                    WalkForwardOne(currentDirection, currentCoord);

                    break;
                case 1:
                    if (currentDirection == Direction.Left)
                    {
                        currentDirection = Direction.Up;
                    }
                    else
                    {
                        currentDirection += 1;
                    }

                    WalkForwardOne(currentDirection, currentCoord);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }

        private static void WalkForwardOne(int currentDirection, RobotCoord currentCoord)
        {
            coordsVisited.ForEach(x => x.Current = false);

            RobotCoord newCoord = new RobotCoord();

            switch (currentDirection)
            {
                case 1:
                    newCoord = new RobotCoord
                    {
                        X = currentCoord.X,
                        Y = currentCoord.Y + 1,
                        Current = true
                    };

                    if (coordsVisited.Any(x => x.X == newCoord.X && x.Y == newCoord.Y))
                    {
                        var coord = coordsVisited.First(x => x.X == newCoord.X && x.Y == newCoord.Y);
                        coord.Current = true;
                    }
                    else
                    {
                        coordsVisited.Add(newCoord);
                    }

                    break;
                case 2:
                    newCoord = new RobotCoord
                    {
                        X = currentCoord.X + 1,
                        Y = currentCoord.Y,
                        Current = true
                    };

                    if (coordsVisited.Any(x => x.X == newCoord.X && x.Y == newCoord.Y))
                    {
                        var coord = coordsVisited.First(x => x.X == newCoord.X && x.Y == newCoord.Y);
                        coord.Current = true;
                    }
                    else
                    {
                        coordsVisited.Add(newCoord);
                    }
                    break;
                case 3:
                    newCoord = new RobotCoord
                    {
                        X = currentCoord.X,
                        Y = currentCoord.Y - 1,
                        Current = true
                    };

                    if (coordsVisited.Any(x => x.X == newCoord.X && x.Y == newCoord.Y))
                    {
                        var coord = coordsVisited.First(x => x.X == newCoord.X && x.Y == newCoord.Y);
                        coord.Current = true;
                    }
                    else
                    {
                        coordsVisited.Add(newCoord);
                    }
                    break;
                case 4:
                    newCoord = new RobotCoord
                    {
                        X = currentCoord.X - 1,
                        Y = currentCoord.Y,
                        Current = true
                    };
                    if (coordsVisited.Any(x => x.X == newCoord.X && x.Y == newCoord.Y))
                    {
                        var coord = coordsVisited.First(x => x.X == newCoord.X && x.Y == newCoord.Y);
                        coord.Current = true;
                    }
                    else
                    {
                        coordsVisited.Add(newCoord);
                    }
                    break;
            }
        }

        private static int ParseOpcode(long[] input, ref long indexOfOpcode)
        {
            while (input[indexOfOpcode] != OpcodeActions.End)
            {
                // Parameter mode
                var parameterCode = input[indexOfOpcode].ToString();
                var opcode = parameterCode.Length > 1 ? int.Parse(parameterCode.Substring(parameterCode.Length - 2)) : int.Parse(parameterCode);
                var param1operator = parameterCode.Length > 2 ? int.Parse(parameterCode.Substring(parameterCode.Length - 3, 1)) : 0;
                var param2operator = parameterCode.Length > 3 ? int.Parse(parameterCode.Substring(parameterCode.Length - 4, 1)) : 0;
                var param3operator = parameterCode.Length > 4 ? int.Parse(parameterCode.Substring(parameterCode.Length - 5, 1)) : 0;

                long param1, param2;

                switch (opcode)
                {
                    case OpcodeActions.Add:
                        param1 = GetValue(indexOfOpcode + 1, param1operator);
                        param2 = GetValue(indexOfOpcode + 2, param2operator);
                        Write(indexOfOpcode + 3, param1 + param2, param3operator);
                        indexOfOpcode += 4;
                        break;
                    case OpcodeActions.Multiply:
                        param1 = GetValue(indexOfOpcode + 1, param1operator);
                        param2 = GetValue(indexOfOpcode + 2, param2operator);
                        Write(indexOfOpcode + 3, param1 * param2, param3operator);
                        indexOfOpcode += 4;
                        break;

                    case OpcodeActions.Insert:
                        Write(indexOfOpcode + 1, _globalInput, param1operator);

                        indexOfOpcode += 2;

                        break;
                    case OpcodeActions.Write:
                        return Convert.ToInt32(GetValue(indexOfOpcode + 1, param1operator));
                    case OpcodeActions.JumpIfNonZero:
                        param1 = GetValue(indexOfOpcode + 1, param1operator);
                        param2 = GetValue(indexOfOpcode + 2, param2operator);

                        if (param1 != 0)
                        {
                            indexOfOpcode = param2;
                        }
                        else
                        {
                            indexOfOpcode += 3;
                        }

                        break;
                    case OpcodeActions.JumpIfZero:
                        param1 = GetValue(indexOfOpcode + 1, param1operator);
                        param2 = GetValue(indexOfOpcode + 2, param2operator);

                        if (param1 == 0)
                        {
                            indexOfOpcode = param2;
                        }
                        else
                        {
                            indexOfOpcode += 3;
                        }

                        break;
                    case OpcodeActions.LessThan:
                        param1 = GetValue(indexOfOpcode + 1, param1operator);
                        param2 = GetValue(indexOfOpcode + 2, param2operator);

                        if (param1 < param2)
                        {
                            Write(indexOfOpcode + 3, 1, param3operator);
                        }
                        else
                        {
                            Write(indexOfOpcode + 3, 0, param3operator);
                        }

                        indexOfOpcode += 4;

                        break;
                    case OpcodeActions.Equal:
                        param1 = GetValue(indexOfOpcode + 1, param1operator);
                        param2 = GetValue(indexOfOpcode + 2, param2operator);

                        if (param1 == param2)
                        {
                            Write(indexOfOpcode + 3, 1, param3operator);
                        }
                        else
                        {
                            Write(indexOfOpcode + 3, 0, param3operator);
                        }

                        indexOfOpcode += 4;

                        break;
                    case OpcodeActions.UpdateRelativeBase:
                        param1 = GetValue(indexOfOpcode + 1, param1operator);

                        _relativeBase += param1;

                        indexOfOpcode += 2;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return -1;
        }

        private static long GetValue(long index, int operation)
        {
            long readIndex = 0;
            switch (operation)
            {
                case ReadOperations.Position:
                    readIndex = input[index];
                    if (readIndex > input.Length - 1)
                    {
                        Array.Resize(ref input, Convert.ToInt32(readIndex) + 1);
                    }
                    return input[input[index]];
                case ReadOperations.Immediate:
                    return input[index];
                case ReadOperations.Relative:
                    return input[_relativeBase + input[index]];
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void Write(long indexToWriteTo, long value, int operation)
        {
            long index = 0;
            switch (operation)
            {
                case ReadOperations.Position:
                    index = input[indexToWriteTo];
                    if (index > input.Length - 1)
                    {
                        Array.Resize(ref input, Convert.ToInt32(index) + 1);
                    }

                    input[index] = value;
                    break;
                case ReadOperations.Immediate:
                    throw new ArgumentOutOfRangeException();
                case ReadOperations.Relative:

                    index = _relativeBase + input[indexToWriteTo];
                    if (index > input.Length - 1)
                    {
                        Array.Resize(ref input, Convert.ToInt32(index) + 1);
                    }

                    input[index] = value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}