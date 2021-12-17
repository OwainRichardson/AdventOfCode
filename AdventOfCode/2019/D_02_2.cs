using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class D_02_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day02_full.txt")[0];

            for (int i = 0; i <= 99; i++)
            {
                for (int j = 0; j <= 99; j++)
                {
                    int[] input = inputs.Split(',').Select(x => int.Parse(x)).ToArray();

                    input[1] = i;
                    input[2] = j;

                    ParseOpcode(input);

                    if (input[0] == 19690720)
                    {
                        Console.WriteLine($"noun: {i}, verb {j}");

                        Console.WriteLine($"total: {(i * 100) + j}");
                    }
                }
            }
        }

        private static void ParseOpcode(int[] input)
        {
            int indexOfOpcode = 0;
            while (input[indexOfOpcode] != 99)
            {
                try
                {
                    if (input[indexOfOpcode] == 1)
                    {
                        ParseOpcodeOne(input, indexOfOpcode);
                    }
                    else if (input[indexOfOpcode] == 2)
                    {
                        ParseOpcodeTwo(input, indexOfOpcode);
                    }

                    indexOfOpcode += 4;
                }
                catch
                {
                    break;
                }
            }
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
    }
}
