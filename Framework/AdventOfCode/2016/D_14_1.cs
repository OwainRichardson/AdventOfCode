using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode._2016
{
    public static class D_14_1
    {
        public static void Execute()
        {
            string input = "ngcjuoqr";
            string[] keys = new string[64];
            int index = 1;

            while (keys.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                string hash = CalculateMD5Hash($"{input}{index}");
                string repeat = string.Empty;

                if (ContainsRepeatedLetter(hash, 3, null, out repeat))
                {
                    if (AnyOfTheNextThousandContainFiveRepeats(index, input, repeat))
                    {
                        var arrayIndex = Array.IndexOf(keys, null);
                        keys[arrayIndex] = index.ToString();
                    }
                }

                index++;
            }

            Console.WriteLine(keys.Last());
        }

        private static bool AnyOfTheNextThousandContainFiveRepeats(int index, string input, string toCheck)
        {
            for (int i = index + 1; i <= index + 1000; i++)
            {
                string hash = CalculateMD5Hash($"{input}{i}");
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
