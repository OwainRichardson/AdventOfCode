using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class D_07_1
    {
        public static int _globalInput = 0;
        public static bool phaseSettingUsed = false;
        public static int[] phaseSettings = new int[] { 0, 1, 2, 3, 4 };
        public static List<string> listOfPhaseSettings = new List<string>();

        public static void Execute()
        {
            int[] input = new int[] { 3, 8, 1001, 8, 10, 8, 105, 1, 0, 0, 21, 42, 67, 88, 101, 114, 195, 276, 357, 438, 99999, 3, 9, 101, 3, 9, 9, 1002, 9, 4, 9, 1001, 9, 5, 9, 102, 4, 9, 9, 4, 9, 99, 3, 9, 1001, 9, 3, 9, 1002, 9, 2, 9, 101, 2, 9, 9, 102, 2, 9, 9, 1001, 9, 5, 9, 4, 9, 99, 3, 9, 102, 4, 9, 9, 1001, 9, 3, 9, 102, 4, 9, 9, 101, 4, 9, 9, 4, 9, 99, 3, 9, 101, 2, 9, 9, 1002, 9, 3, 9, 4, 9, 99, 3, 9, 101, 4, 9, 9, 1002, 9, 5, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 101, 1, 9, 9, 4, 9, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1001, 9, 1, 9, 4, 9, 99, 3, 9, 1001, 9, 2, 9, 4, 9, 3, 9, 102, 2, 9, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 1002, 9, 2, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 3, 9, 101, 2, 9, 9, 4, 9, 99 };

            GetPhaseSettings(phaseSettings);

            foreach (var phaseSetting in listOfPhaseSettings)
            {
                var amplifierOutput = 0;

                amplifierOutput = RunAmp(input, phaseSetting[0].ToString(), amplifierOutput);
                amplifierOutput = RunAmp(input, phaseSetting[1].ToString(), amplifierOutput);
                amplifierOutput = RunAmp(input, phaseSetting[2].ToString(), amplifierOutput);
                amplifierOutput = RunAmp(input, phaseSetting[3].ToString(), amplifierOutput);
                amplifierOutput = RunAmp(input, phaseSetting[4].ToString(), amplifierOutput);

                Console.WriteLine($"Output From Amp E for code {phaseSetting}: {amplifierOutput}");
            }

            Console.WriteLine();
        }

        private static int RunAmp(int[] input, string phaseSetting, int amplifierOutput)
        {
            var temp = input;

            return ParseOpcode(temp, int.Parse(phaseSetting), amplifierOutput);
        }

        private static void Swap(ref int a, ref int b)
        {
            if (a == b) return;

            var temp = a;
            a = b;
            b = temp;
        }

        public static void GetPhaseSettings(int[] list)
        {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }

        private static void GetPer(int[] list, int k, int m)
        {
            if (k == m)
            {
                listOfPhaseSettings.Add($"{list[0]}{list[1]}{list[2]}{list[3]}{list[4]}");
            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }

        private static int ParseOpcode(int[] input, int phaseSetting, int amplifierOutput)
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
                    ParseOpcodeThree(input, indexOfOpcode, phaseSetting, amplifierOutput);
                    indexOfOpcode += 2;
                }
                else if (opcode == 4)
                {
                    var param1 = input[indexOfOpcode + 1];
                    var value1 = CalculateParameterMode(param1, param1operator, input);

                    return value1;

                    //indexOfOpcode += 2;
                }
                else if (opcode == 5)
                {
                    var param1 = input[indexOfOpcode + 1];
                    var value1 = CalculateParameterMode(param1, param1operator, input);

                    var param2 = input[indexOfOpcode + 2];
                    var value2 = CalculateParameterMode(param2, param2operator, input);

                    if (value1 != 0)
                    {
                        indexOfOpcode = value2;
                    }
                    else
                    {
                        indexOfOpcode += 3;
                    }
                }

                else if (opcode == 6)
                {
                    var param1 = input[indexOfOpcode + 1];
                    var value1 = CalculateParameterMode(param1, param1operator, input);

                    var param2 = input[indexOfOpcode + 2];
                    var value2 = CalculateParameterMode(param2, param2operator, input);

                    if (value1 == 0)
                    {
                        indexOfOpcode = value2;
                    }
                    else
                    {
                        indexOfOpcode += 3;
                    }
                }
                else if (opcode == 7)
                {
                    var param1 = input[indexOfOpcode + 1];
                    var value1 = CalculateParameterMode(param1, param1operator, input);

                    var param2 = input[indexOfOpcode + 2];
                    var value2 = CalculateParameterMode(param2, param2operator, input);

                    var indexToStore = input[indexOfOpcode + 3];

                    if (value1 < value2)
                    {
                        input[indexToStore] = 1;
                    }
                    else
                    {
                        input[indexToStore] = 0;
                    }

                    indexOfOpcode += 4;
                }

                else if (opcode == 8)
                {
                    var param1 = input[indexOfOpcode + 1];
                    var value1 = CalculateParameterMode(param1, param1operator, input);

                    var param2 = input[indexOfOpcode + 2];
                    var value2 = CalculateParameterMode(param2, param2operator, input);

                    var indexToStore = input[indexOfOpcode + 3];

                    if (value1 == value2)
                    {
                        input[indexToStore] = 1;
                    }
                    else
                    {
                        input[indexToStore] = 0;
                    }

                    indexOfOpcode += 4;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();

                }
            }

            return 9999999;
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

        private static void ParseOpcodeThree(int[] input, int index, int phaseSetting, int amplifierOutput)
        {
            var indexOfNumber1 = input[index + 1];

            if (indexOfNumber1 > input.Length)
            {
                throw new IndexOutOfRangeException();
            }

            if (!phaseSettingUsed)
            {
                input[indexOfNumber1] = phaseSetting;
                phaseSettingUsed = true;
            }
            else
            {
                input[indexOfNumber1] = amplifierOutput;
                phaseSettingUsed = false;
            }
        }

        private static void ParseOpcodeFour(int[] input, int index)
        {
            var indexOfNumber1 = input[index + 1];

            Console.WriteLine($"{input[indexOfNumber1]}");
        }

        private static int ParseOpcodeFive(int[] input, int index)
        {
            var number1 = input[index + 1];
            var number2 = input[index + 2];

            if (input[number1] != 0)
            {
                return number2;
            }

            return 3;
        }

        private static int ParseOpcodeSix(int[] input, int index)
        {
            var number1 = input[index + 1];
            var number2 = input[index + 2];

            if (input[number1] == 0)
            {
                return number2;
            }

            return 3;
        }
    }
}