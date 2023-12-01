using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_09_2
    {
        public static void Execute()
        {
            long[] inputs = File.ReadAllLines(@"2020\Data\day09.txt").Select(x => long.Parse(x)).ToArray();
            const int preambleLength = 25;
            int index = preambleLength;

            long[] preamble = inputs.Take(preambleLength).ToArray();

            bool error = false;
            while (!error)
            {
                long nextNumber = inputs[index];

                if (NumberCanBeMadeFromPreamble(nextNumber, preamble))
                {
                    index++;
                    preamble = ResetPreamble(index, inputs, preambleLength);
                }
                else
                {
                    error = true;
                }
            }

            long erroneousNumber = inputs[index];

            FindContiguousSet(inputs, erroneousNumber);
        }

        private static void FindContiguousSet(long[] inputs, long erroneousNumber)
        {
            List<long> currentNumbers = new List<long>();

            for (int startIndex = 0; startIndex < inputs.Length; startIndex++)
            {
                int index = startIndex;
                while (currentNumbers.Sum() < erroneousNumber)
                {
                    currentNumbers.Add(inputs[index]);
                    index += 1;
                }

                if (currentNumbers.Sum() == erroneousNumber)
                {
                    break;
                }
                else
                {
                    currentNumbers = new List<long>();
                }
            }

            Console.WriteLine(currentNumbers.Min() + currentNumbers.Max());
        }

        private static long[] ResetPreamble(int index, long[] inputs, int preambleLength)
        {
            return inputs.Skip(index - preambleLength).Take(preambleLength).ToArray();
        }

        private static bool NumberCanBeMadeFromPreamble(long nextNumber, long[] preamble)
        {
            for (int i = 0; i < preamble.Length; i++)
            {
                for (int j = 0; j < preamble.Length; j++)
                {
                    if (i == j) continue;

                    if (preamble[i] + preamble[j] == nextNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
