using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_01_1
    {
        public static void Execute()
        {
            int[] inputs = File.ReadAllLines(@"2021\Data\day01.txt").Select(x => int.Parse(x)).ToArray();
            int numberOfLargerMeasurements = 0;

            for (int index = 1; index < inputs.Length; index++)
            {
                if (inputs[index - 1] < inputs[index])
                {
                    numberOfLargerMeasurements++;
                }
            }

            Console.WriteLine(numberOfLargerMeasurements);
        }
    }
}
