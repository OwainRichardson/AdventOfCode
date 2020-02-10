using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_16_2
    {
        public static void Execute()
        {
            var input = "11100010111110100";
            int diskLength = 35651584;

            while (input.Length < diskLength)
            {
                input = DragonCurve(input);
            }

            FindCheckSum(input.Substring(0, diskLength));
        }

        private static string DragonCurve(string input)
        {
            string a = input;
            string b = InverseAndReverse(a);

            return $"{a}0{b}";

        }

        private static string Inverse(string v)
        {
                if (v == "1")
                {
                    return "0";
                }
                if (v == "0")
                {
                    return "1";
                }

            return "ERROR";
        }

        private static string InverseAndReverse(string a)
        {
            string result = string.Empty;

            char[] charArray = a.ToCharArray();
            Array.Reverse(charArray);

            result = new string(charArray).Replace("1", "2").Replace("0", "1").Replace("2", "0");

            return result;
        }

        private static void FindCheckSum(string input)
        {
            while (input.Length % 2 == 0)
            {
                StringBuilder checkSumBuilder = new StringBuilder(input.Length / 2 + 1);

                for (int i = 0; i < input.Length; i = i + 2)
                {
                    if (input[i] == input[i + 1])
                    {
                        checkSumBuilder.Append("1");
                    }
                    else
                    {
                        checkSumBuilder.Append("0");
                    }
                }

                input = checkSumBuilder.ToString();
            }

            Console.WriteLine($"Checksum = {input}");
        }
    }
}
