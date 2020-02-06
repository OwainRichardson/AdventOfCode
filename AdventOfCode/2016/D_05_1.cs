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
    public static class D_05_1
    {
        public static void Execute()
        {
            var input = "wtnhxymk";

            int index = 1;
            string answer = string.Empty;

            for (int i = 1; i <= 8; i++)
            {
                string secretKey = string.Empty;

                while (!secretKey.StartsWith("00000"))
                {
                    index++;
                    secretKey = CalculateMD5Hash($"{input}{index}");
                }

                CustomConsoleColour.SetAnswerColour();
                answer = $"{answer}{secretKey.Substring(5, 1)}";
                Console.Write($"\r{answer}");
            }

            Console.ResetColor();
        }

        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
