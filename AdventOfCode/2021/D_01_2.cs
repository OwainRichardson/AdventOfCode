using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_01_2
    {
        public static void Execute()
        {
            int[] inputs = File.ReadAllLines(@"2021\Data\day01.txt").Select(x => int.Parse(x)).ToArray();
            int numberOfLargerMeasurements = 0;

            for (int index = 3; index < inputs.Length; index++)
            {
                if (AddInputs(inputs[index - 3], inputs[index - 2], inputs[index - 1]) < AddInputs(inputs[index - 2], inputs[index - 1], inputs[index]))
                {
                    numberOfLargerMeasurements++;
                }
            }

            Console.WriteLine(numberOfLargerMeasurements);
        }

        private static int AddInputs(int v1, int v2, int v3)
        {
            return v1 + v2 + v3;
        }
    }
}
