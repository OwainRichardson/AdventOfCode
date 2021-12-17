﻿using System;

namespace AdventOfCode._2019
{
    public static class D_09_1
    {
        public const int _globalInput = 0;
        public static long _relativeBase = 0;
        public static long[] input = new long[] { 1102, 34463338, 34463338, 63, 1007, 63, 34463338, 63, 1005, 63, 53, 1101, 0, 3, 1000, 109, 988, 209, 12, 9, 1000, 209, 6, 209, 3, 203, 0, 1008, 1000, 1, 63, 1005, 63, 65, 1008, 1000, 2, 63, 1005, 63, 904, 1008, 1000, 0, 63, 1005, 63, 58, 4, 25, 104, 0, 99, 4, 0, 104, 0, 99, 4, 17, 104, 0, 99, 0, 0, 1102, 32, 1, 1016, 1101, 38, 0, 1012, 1102, 1, 693, 1022, 1102, 1, 27, 1007, 1101, 0, 190, 1025, 1102, 20, 1, 1019, 1102, 1, 33, 1008, 1102, 28, 1, 1013, 1101, 0, 1, 1021, 1102, 1, 851, 1026, 1102, 22, 1, 1018, 1101, 29, 0, 1005, 1101, 0, 21, 1004, 1101, 36, 0, 1009, 1101, 0, 195, 1024, 1101, 0, 39, 1002, 1101, 0, 848, 1027, 1102, 1, 34, 1003, 1102, 23, 1, 1015, 1102, 1, 30, 1010, 1101, 0, 26, 1017, 1102, 1, 35, 1001, 1102, 1, 489, 1028, 1102, 1, 484, 1029, 1102, 1, 686, 1023, 1102, 0, 1, 1020, 1102, 25, 1, 1006, 1102, 1, 31, 1014, 1101, 37, 0, 1000, 1102, 1, 24, 1011, 109, 23, 2105, 1, 1, 4, 187, 1105, 1, 199, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -18, 2102, 1, -3, 63, 1008, 63, 41, 63, 1005, 63, 223, 1001, 64, 1, 64, 1106, 0, 225, 4, 205, 1002, 64, 2, 64, 109, 2, 2101, 0, 1, 63, 1008, 63, 33, 63, 1005, 63, 251, 4, 231, 1001, 64, 1, 64, 1105, 1, 251, 1002, 64, 2, 64, 109, 13, 21107, 40, 41, -8, 1005, 1012, 269, 4, 257, 1106, 0, 273, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 9, 1205, -8, 287, 4, 279, 1105, 1, 291, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -38, 2101, 0, 9, 63, 1008, 63, 34, 63, 1005, 63, 315, 1001, 64, 1, 64, 1106, 0, 317, 4, 297, 1002, 64, 2, 64, 109, 18, 21108, 41, 38, 1, 1005, 1010, 337, 1001, 64, 1, 64, 1106, 0, 339, 4, 323, 1002, 64, 2, 64, 109, -5, 2107, 30, 1, 63, 1005, 63, 359, 1001, 64, 1, 64, 1106, 0, 361, 4, 345, 1002, 64, 2, 64, 109, 14, 21101, 42, 0, -7, 1008, 1011, 42, 63, 1005, 63, 387, 4, 367, 1001, 64, 1, 64, 1106, 0, 387, 1002, 64, 2, 64, 109, -18, 1208, 0, 39, 63, 1005, 63, 403, 1106, 0, 409, 4, 393, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 3, 1207, -1, 38, 63, 1005, 63, 425, 1106, 0, 431, 4, 415, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -8, 1201, 5, 0, 63, 1008, 63, 35, 63, 1005, 63, 455, 1001, 64, 1, 64, 1106, 0, 457, 4, 437, 1002, 64, 2, 64, 109, 30, 1206, -4, 469, 1106, 0, 475, 4, 463, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 10, 2106, 0, -7, 4, 481, 1106, 0, 493, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -24, 21102, 43, 1, 3, 1008, 1014, 40, 63, 1005, 63, 517, 1001, 64, 1, 64, 1105, 1, 519, 4, 499, 1002, 64, 2, 64, 109, -4, 2108, 41, -5, 63, 1005, 63, 539, 1001, 64, 1, 64, 1106, 0, 541, 4, 525, 1002, 64, 2, 64, 109, 18, 21101, 44, 0, -8, 1008, 1017, 47, 63, 1005, 63, 561, 1105, 1, 567, 4, 547, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -24, 1202, 6, 1, 63, 1008, 63, 27, 63, 1005, 63, 589, 4, 573, 1106, 0, 593, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 7, 1208, -5, 34, 63, 1005, 63, 611, 4, 599, 1106, 0, 615, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -5, 1207, 6, 37, 63, 1005, 63, 637, 4, 621, 1001, 64, 1, 64, 1106, 0, 637, 1002, 64, 2, 64, 109, 23, 1206, -6, 655, 4, 643, 1001, 64, 1, 64, 1105, 1, 655, 1002, 64, 2, 64, 109, -10, 2107, 32, -8, 63, 1005, 63, 673, 4, 661, 1105, 1, 677, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 5, 2105, 1, 2, 1001, 64, 1, 64, 1106, 0, 695, 4, 683, 1002, 64, 2, 64, 109, -17, 1202, 0, 1, 63, 1008, 63, 20, 63, 1005, 63, 715, 1106, 0, 721, 4, 701, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -4, 1201, 4, 0, 63, 1008, 63, 21, 63, 1005, 63, 743, 4, 727, 1106, 0, 747, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 10, 1205, 10, 763, 1001, 64, 1, 64, 1105, 1, 765, 4, 753, 1002, 64, 2, 64, 109, 1, 21102, 45, 1, 1, 1008, 1012, 45, 63, 1005, 63, 787, 4, 771, 1105, 1, 791, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -4, 2102, 1, -2, 63, 1008, 63, 29, 63, 1005, 63, 813, 4, 797, 1105, 1, 817, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -4, 2108, 33, 5, 63, 1005, 63, 835, 4, 823, 1105, 1, 839, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 23, 2106, 0, 1, 1106, 0, 857, 4, 845, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -12, 21108, 46, 46, 1, 1005, 1015, 879, 4, 863, 1001, 64, 1, 64, 1106, 0, 879, 1002, 64, 2, 64, 109, 10, 21107, 47, 46, -5, 1005, 1019, 899, 1001, 64, 1, 64, 1105, 1, 901, 4, 885, 4, 64, 99, 21101, 27, 0, 1, 21101, 915, 0, 0, 1105, 1, 922, 21201, 1, 52134, 1, 204, 1, 99, 109, 3, 1207, -2, 3, 63, 1005, 63, 964, 21201, -2, -1, 1, 21101, 0, 942, 0, 1105, 1, 922, 22101, 0, 1, -1, 21201, -2, -3, 1, 21101, 0, 957, 0, 1105, 1, 922, 22201, 1, -1, -2, 1106, 0, 968, 21201, -2, 0, -2, 109, -3, 2106, 0, 0 };

        public static void Execute()
        {
            ParseOpcode(input);
        }

        private static void ParseOpcode(long[] input)
        {
            long indexOfOpcode = 0;
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
                        Console.WriteLine(GetValue(indexOfOpcode + 1, param1operator));

                        indexOfOpcode += 2;
                        break;
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
        }

        private static long GetValue(long index, int operation)
        {
            switch (operation)
            {
                case ReadOperations.Position:
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

    public static class OpcodeActions
    {
        public const int Add = 1;
        public const int Multiply = 2;
        public const int Insert = 3;
        public const int Write = 4;
        public const int JumpIfNonZero = 5;
        public const int JumpIfZero = 6;
        public const int LessThan = 7;
        public const int Equal = 8;
        public const int UpdateRelativeBase = 9;
        public const int End = 99;
    }

    public static class ReadOperations
    {
        public const int Position = 0;
        public const int Immediate = 1;
        public const int Relative = 2;
    }
}