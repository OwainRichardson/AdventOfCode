using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_09_1
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

            Console.WriteLine(inputs[index]);
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
