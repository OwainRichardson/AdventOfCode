using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_10_1
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day10_full.txt")[0];
            int[] cycle = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

            int numberOfNumbers = 4;
            int[] numbers = new int[numberOfNumbers + 1];
            numbers = PopulateNumbers(numbers, numberOfNumbers);

            ApplyTwist(numbers, cycle);

            Console.WriteLine(numbers[0] * numbers[1]);
        }

        private static void ApplyTwist(int[] numbers, int[] cycle)
        {
            int index = 0;
            int skipSize = 0;
            int loop = 1;

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
