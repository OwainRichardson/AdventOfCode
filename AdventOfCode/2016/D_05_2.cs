using AdventOfCode.Common;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2016
{
    public static class D_05_2
    {
        public static void Execute()
        {
            var input = "wtnhxymk";

            int index = 1;
            string[] answer = new string[8];

            while (answer.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                string secretKey = string.Empty;

                while (!secretKey.StartsWith("00000"))
                {
                    index++;
                    secretKey = CalculateMD5Hash($"{input}{index}");

                    if (index % 10000 == 0)
                    {
                        WriteAnswer(answer);
                    }
                }

                int position = 0;
                if (int.TryParse(secretKey.Substring(5, 1), out position))
                {
                    string value = secretKey.Substring(6, 1);
                    if (position < answer.Length)
                    {
                        if (string.IsNullOrWhiteSpace(answer[position]))
                        {
                            answer[position] = value;
                            WriteAnswer(answer);
                        }
                    }
                }
            }

            Console.WriteLine();
        }

        private static void WriteAnswer(string[] answer)
        {
            Console.Write("\r");

            foreach (string a in answer)
            {
                if (string.IsNullOrWhiteSpace(a))
                {
                    var rand = new Random();
                    Console.Write($"{rand.Next(0, 9)}");
                }
                else
                {
                    CustomConsoleColour.SetAnswerColour();
                    Console.Write(a);
                    Console.ResetColor();
                }
            }
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
