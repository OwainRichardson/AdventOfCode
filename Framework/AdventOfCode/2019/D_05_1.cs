using System;

namespace AdventOfCode._2019
{
    public static class D_05_1
    {
        public const int _globalInput = 1;

        public static void Execute()
        {
            int[] input = new int[] { 3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 101, 67, 166, 224, 1001, 224, -110, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 4, 224, 1, 224, 223, 223, 2, 62, 66, 224, 101, -406, 224, 224, 4, 224, 102, 8, 223, 223, 101, 3, 224, 224, 1, 224, 223, 223, 1101, 76, 51, 225, 1101, 51, 29, 225, 1102, 57, 14, 225, 1102, 64, 48, 224, 1001, 224, -3072, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 1, 224, 1, 224, 223, 223, 1001, 217, 90, 224, 1001, 224, -101, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 2, 224, 1, 223, 224, 223, 1101, 57, 55, 224, 1001, 224, -112, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 7, 224, 1, 223, 224, 223, 1102, 5, 62, 225, 1102, 49, 68, 225, 102, 40, 140, 224, 101, -2720, 224, 224, 4, 224, 1002, 223, 8, 223, 1001, 224, 4, 224, 1, 223, 224, 223, 1101, 92, 43, 225, 1101, 93, 21, 225, 1002, 170, 31, 224, 101, -651, 224, 224, 4, 224, 102, 8, 223, 223, 101, 4, 224, 224, 1, 223, 224, 223, 1, 136, 57, 224, 1001, 224, -138, 224, 4, 224, 102, 8, 223, 223, 101, 2, 224, 224, 1, 223, 224, 223, 1102, 11, 85, 225, 4, 223, 99, 0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005, 227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0, 99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0, 105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0, 1105, 1, 99999, 1107, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 329, 1001, 223, 1, 223, 1007, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 344, 101, 1, 223, 223, 108, 677, 677, 224, 1002, 223, 2, 223, 1006, 224, 359, 101, 1, 223, 223, 1008, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 374, 1001, 223, 1, 223, 108, 677, 226, 224, 1002, 223, 2, 223, 1006, 224, 389, 101, 1, 223, 223, 7, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 404, 101, 1, 223, 223, 7, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 419, 101, 1, 223, 223, 107, 226, 226, 224, 102, 2, 223, 223, 1006, 224, 434, 1001, 223, 1, 223, 1008, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 449, 101, 1, 223, 223, 108, 226, 226, 224, 102, 2, 223, 223, 1005, 224, 464, 1001, 223, 1, 223, 1108, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 479, 1001, 223, 1, 223, 8, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 494, 1001, 223, 1, 223, 1108, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 509, 1001, 223, 1, 223, 1007, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 524, 1001, 223, 1, 223, 7, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 539, 1001, 223, 1, 223, 8, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 554, 1001, 223, 1, 223, 107, 226, 677, 224, 1002, 223, 2, 223, 1006, 224, 569, 101, 1, 223, 223, 1107, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 584, 1001, 223, 1, 223, 1108, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 599, 1001, 223, 1, 223, 1008, 677, 226, 224, 102, 2, 223, 223, 1006, 224, 614, 101, 1, 223, 223, 107, 677, 677, 224, 102, 2, 223, 223, 1006, 224, 629, 1001, 223, 1, 223, 1107, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 644, 101, 1, 223, 223, 8, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 659, 1001, 223, 1, 223, 1007, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 674, 1001, 223, 1, 223, 4, 223, 99, 226 };

            ParseOpcode(input);
        }

        private static void ParseOpcode(int[] input)
        {
            int indexOfOpcode = 0;
            while (input[indexOfOpcode] != 99)
            {

                // Parameter mode
                var parameterCode = input[indexOfOpcode].ToString();
                var opcode = parameterCode.Length > 1 ? int.Parse(parameterCode.Substring(parameterCode.Length - 2)) : int.Parse(parameterCode);
                var param1operator = parameterCode.Length > 2 ? int.Parse(parameterCode.Substring(parameterCode.Length - 3, 1)) : 0;
                var param2operator = parameterCode.Length > 3 ? int.Parse(parameterCode.Substring(parameterCode.Length - 4, 1)) : 0;

                if (opcode == 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (opcode == 1)
                {
                    var param1 = input[indexOfOpcode + 1];
                    var value1 = CalculateParameterMode(param1, param1operator, input);

                    var param2 = input[indexOfOpcode + 2];
                    var value2 = CalculateParameterMode(param2, param2operator, input);

                    var param3 = input[indexOfOpcode + 3];

                    input[param3] = value1 + value2;
                    indexOfOpcode += 4;
                }
                else if (opcode == 2)
                {
                    var param1 = input[indexOfOpcode + 1];
                    var value1 = CalculateParameterMode(param1, param1operator, input);

                    var param2 = input[indexOfOpcode + 2];
                    var value2 = CalculateParameterMode(param2, param2operator, input);

                    var param3 = input[indexOfOpcode + 3];

                    input[param3] = value1 * value2;
                    indexOfOpcode += 4;
                }
                else if (opcode == 3)
                {
                    ParseOpcodeThree(input, indexOfOpcode);
                    indexOfOpcode += 2;
                }
                else if (opcode == 4)
                {
                    ParseOpcodeFour(input, indexOfOpcode);
                    indexOfOpcode += 2;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();

                }
            }
        }

        private static int CalculateParameterMode(int param1, int param1operator, int[] input)
        {
            if (param1operator == 0)
            {
                return input[param1];
            }

            return param1;
        }

        private static void ParseOpcodeOne(int[] input, int index)
        {
            var indexOfNumber1 = input[index + 1];
            var indexOfNumber2 = input[index + 2];
            var number1 = input[indexOfNumber1];
            var number2 = input[indexOfNumber2];
            var indexToStoreResult = input[index + 3];

            var total = number1 + number2;

            if (indexToStoreResult > input.Length)
            {
                throw new IndexOutOfRangeException();
            }

            input[indexToStoreResult] = total;
        }

        private static void ParseOpcodeTwo(int[] input, int index)
        {
            var indexOfNumber1 = input[index + 1];
            var indexOfNumber2 = input[index + 2];
            var number1 = input[indexOfNumber1];
            var number2 = input[indexOfNumber2];
            var indexToStoreResult = input[index + 3];

            var total = number1 * number2;

            if (indexToStoreResult > input.Length)
            {
                throw new IndexOutOfRangeException();
            }

            input[indexToStoreResult] = total;
        }

        private static void ParseOpcodeThree(int[] input, int index)
        {
            var indexOfNumber1 = input[index + 1];

            if (indexOfNumber1 > input.Length)
            {
                throw new IndexOutOfRangeException();
            }

            input[indexOfNumber1] = _globalInput;
        }

        private static void ParseOpcodeFour(int[] input, int index)
        {
            var indexOfNumber1 = input[index + 1];

            if (input[indexOfNumber1] != 0)
            {
                Console.WriteLine($"{input[indexOfNumber1]}");
            }
        }
    }
}