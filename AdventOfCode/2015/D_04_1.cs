using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public static class D_04_1
    {
        public static void Execute()
        {
            string input = "yzbqklnj";
            int index = 0;
            string secretKey = "";

            while (!secretKey.StartsWith("00000"))
            {
                index++;
                secretKey = CalculateMD5Hash($"{input}{index}");
            }

            Console.WriteLine(index);
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
