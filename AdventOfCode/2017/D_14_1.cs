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
    public static class D_14_1
    {
        public static void Execute()
        {
            string input = "stpzcrnm";

            int count = 0;
            for (int i = 0; i < 128; i++)
            {
                string row = CalculateRow(input, i);

                count += row.Count(x => x.ToString().Equals("1"));
            }

            Console.WriteLine(count);
        }

        private static string CalculateRow(string input, int rowNumber)
        {
            string key = $"{input}-{rowNumber}";

            string hash = D_10_2_External.KnotHashPartTwo(key);

            StringBuilder binary = new StringBuilder();
            foreach (Char c in hash)
            {
                string bin = Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2);

                if (bin.Length < 4)
                {
                    while (bin.Length < 4)
                    {
                        bin = $"0{bin}";
                    }
                }

                binary.Append(bin);
            }

            return binary.ToString();
        }
    }
}
