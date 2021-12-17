using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2017
{
    public static class D_10_2
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day10_full.txt")[0];
            string[] splitInputs = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            int[] cycle = new int[(splitInputs.Length * 2) - 1 + 5];
            int index = 0;
            foreach (string split in splitInputs)
            {
                cycle[index] = Encoding.Default.GetBytes(split.ToString())[0];
                cycle[index + 1] = Encoding.Default.GetBytes(",")[0];
                index += 2;
            }

            if (index > 0)
            {
                index--;
            }

            cycle[index] = 17;
            cycle[index + 1] = 31;
            cycle[index + 2] = 73;
            cycle[index + 3] = 47;
            cycle[index + 4] = 23;

            int numberOfNumbers = 255;
            int[] numbers = new int[numberOfNumbers + 1];
            numbers = PopulateNumbers(numbers, numberOfNumbers);

            ApplyTwist(numbers, cycle);

            string denseHash = ApplyXOr(numbers);

            Console.WriteLine(denseHash);
        }

        private static string ApplyXOr(int[] numbers)
        {
            string denseHash = string.Empty;

            for (int i = 0; i < 16; i++)
            {
                int[] temp = numbers.Skip(i * 16).Take(16).ToArray();

                int current = temp.Aggregate((x, y) => x ^ y);
                
                if (current.ToString().Length == 1)
                {
                    denseHash = $"{denseHash}{string.Format("0{0:X}", current)}";
                }
                else
                {
                    denseHash = $"{denseHash}{string.Format("{0:X}", current)}";
                }

            }

            return denseHash;
        }

        private static void ApplyTwist(int[] numbers, int[] cycle)
        {
            int index = 0;
            int skipSize = 0;

            for (int loop = 1; loop <= 64; loop++)
            {
                foreach (var length in cycle)
                {
                    int[] temp = new int[length];

                    for (int i = 0; i < length; i++)
                    {
                        temp[i] = numbers[(index + i) % numbers.Length];
                    }

                    temp = temp.Reverse().ToArray();

                    for (int i = 0; i < length; i++)
                    {
                        numbers[(index + i) % numbers.Length] = temp[i];
                    }

                    index = (index + skipSize + length) % numbers.Length;
                    skipSize++;
                }
            }
        }

        private static int[] PopulateNumbers(int[] numbers, int total)
        {
            for (int i = 0; i <= total; i++)
            {
                numbers[i] = i;
            }

            return numbers;
        }
    }
}
