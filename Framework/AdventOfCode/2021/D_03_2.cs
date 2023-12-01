using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_03_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day03.txt").ToArray();

            string oxyGeneratorRating = CalculateOxyGeneratorRating(inputs);
            string co2ScrubberRating = CalculateCO2ScrubberRating(inputs);

            Console.WriteLine(ConvertBinaryToNumber(oxyGeneratorRating) * ConvertBinaryToNumber(co2ScrubberRating));
        }

        private static string CalculateCO2ScrubberRating(string[] inputs)
        {
            string[] filteredInputs = inputs;

            for (int index = 0; index < inputs[0].Length; index++)
            {
                if (filteredInputs.Length == 1) break;

                double averageValue = filteredInputs.Average(x => int.Parse(x[index].ToString()));

                if (averageValue >= 0.5)
                {
                    filteredInputs = RemoveFromInputs(filteredInputs, index, "1");
                }
                else
                {
                    filteredInputs = RemoveFromInputs(filteredInputs, index, "0");

                }
            }

            return filteredInputs.Single().ToString();
        }

        private static string CalculateOxyGeneratorRating(string[] inputs)
        {
            string[] filteredInputs = inputs;

            for (int index = 0; index < inputs[0].Length; index++)
            {
                double averageValue = filteredInputs.Average(x => int.Parse(x[index].ToString()));

                if (averageValue >= 0.5)
                {
                    filteredInputs = RemoveFromInputs(filteredInputs, index, "0");
                }
                else
                {
                    filteredInputs = RemoveFromInputs(filteredInputs, index, "1");

                }
            }

            return filteredInputs.Single().ToString();
        }

        private static string[] RemoveFromInputs(string[] inputs, int index, string valueToRemove)
        {
            List<string> newInputs = new List<string>();

            foreach (string input in inputs)
            {
                if (input[index].ToString() != valueToRemove)
                {
                    newInputs.Add(input);
                }
            }

            return newInputs.ToArray();
        }

        private static int ConvertBinaryToNumber(string input)
        {
            return Convert.ToInt32(input, 2);
        }

        
    }
}
