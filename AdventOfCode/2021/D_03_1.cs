using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_03_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day03.txt").ToArray();

            string gammaRate = CalculateGammaRate(inputs);
            string epsilonRate = CalculateEpsilonRate(gammaRate);

            Console.WriteLine(ConvertBinaryToNumber(gammaRate) * ConvertBinaryToNumber(epsilonRate));
        }

        private static int ConvertBinaryToNumber(string input)
        {
            return Convert.ToInt32(input, 2);
        }

        private static string CalculateEpsilonRate(string gammaRate)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in gammaRate)
            {
                if (c.ToString() == "1")
                {
                    sb.Append("0");
                }
                else
                {
                    sb.Append("1");
                }
            }

            return sb.ToString();
        }

        private static string CalculateGammaRate(string[] inputs)
        {
            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < inputs[0].Length; index++)
            {
                double averageValue = inputs.Average(x => int.Parse(x[index].ToString()));

                if (averageValue >= 0.5)
                {
                    sb.Append("1");
                }
                else
                {
                    sb.Append("0");
                }
            }

            return sb.ToString();
        }
    }
}
