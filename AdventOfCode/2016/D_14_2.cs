using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_14_2
    {
        public static void Execute()
        {
            string input = "ngcjuoqr";
            string[] keys = new string[64];
            int index = 0;
            Dictionary<int, string> memo = new Dictionary<int, string>();

            while (keys.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                for (int key = index - 10; key < index; key++)
                {
                    if (memo.ContainsKey(key))
                    {
                        memo.Remove(key);
                    }
                }

                string hash = string.Empty;

                if (memo.ContainsKey(index))
                {
                    hash = memo[index];
                }
                else
                {
                    hash = CalculateMD5Hash($"{input}{index}");

                    for (int i = 1; i <= 2016; i++)
                    {
                        hash = CalculateMD5Hash(hash);
                    }

                    memo.Add(index, hash);
                }

                string repeat = string.Empty;

                if (ContainsRepeatedLetter(hash, 3, null, out repeat))
                {
                    if (AnyOfTheNextThousandContainFiveRepeats(index, input, repeat, ref memo))
                    {
                        var arrayIndex = Array.IndexOf(keys, null);
                        keys[arrayIndex] = index.ToString();
                    }
                }

                index++;

                if (index % 100 == 0)
                {
                    Console.Write($"\r{index}");
                }
            }
            Console.Write($"\r{keys.Last()}     ");
        }

        private static bool AnyOfTheNextThousandContainFiveRepeats(int index, string input, string toCheck, ref Dictionary<int, string> memo)
        {
            for (int i = index + 1; i <= index + 1000; i++)
            {
                string hash = string.Empty;

                if (memo.ContainsKey(i))
                {
                    hash = memo[i];
                }
                else
                {
                    hash = CalculateMD5Hash($"{input}{i}");
                    for (int h = 1; h <= 2016; h++)
                    {
                        hash = CalculateMD5Hash(hash);
                    }

                    memo.Add(i, hash);
                }

                string repeat;
                if (ContainsRepeatedLetter(hash, 5, toCheck, out repeat))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool ContainsRepeatedLetter(string hash, int sequenceLength, string toCheck, out string repeat)
        {
            int count = 1;
            var charEnumerator = StringInfo.GetTextElementEnumerator(hash);
            var currentElement = string.Empty;

            while (charEnumerator.MoveNext())
            {
                if (((!string.IsNullOrWhiteSpace(toCheck) && currentElement == toCheck) || string.IsNullOrWhiteSpace(toCheck)) && currentElement == charEnumerator.GetTextElement())
                {
                    count++;
                    if (count >= sequenceLength)
                    {
                        repeat = currentElement;
                        return true;
                    }
                }
                else
                {
                    count = 1;
                    currentElement = charEnumerator.GetTextElement();
                }
            }

            repeat = "";
            return false;
        }

        public static string CalculateMD5Hash(string input)
        {
            var Md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = Md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
