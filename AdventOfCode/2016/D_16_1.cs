using System;

namespace AdventOfCode._2016
{
    public static class D_16_1
    {
        public static void Execute()
        {
            var input = "11100010111110100";
            int diskLength = 272;

            while (input.Length < diskLength)
            {
                input = DragonCurve(input);
            }

            FindCheckSum(input.Substring(0, diskLength));
        }

        private static string DragonCurve(string input)
        {
            string a = input;
            string b = Inverse(Reverse(a));

            return $"{a}0{b}";

        }

        private static string Inverse(string v)
        {
            string result = string.Empty;

            for (int i = 0; i < v.Length; i++)
            {
                if (v[i].ToString() == "1")
                {
                    result = $"{result}0";
                }
                if (v[i].ToString() == "0")
                {
                    result = $"{result}1";
                }
            }

            return result;
        }

        private static string Reverse(string a)
        {
            string result = string.Empty;

            for (int i = a.Length - 1; i >= 0; i--)
            {
                result = $"{result}{a[i]}";
            }

            return result;
        }

        private static void FindCheckSum(string input)
        {
            string checkSum = string.Empty;

            while (checkSum.Length % 2 == 0)
            {
                checkSum = string.Empty;

                for (int i = 0; i < input.Length; i = i + 2)
                {
                    if (input[i] == input[i + 1])
                    {
                        checkSum = $"{checkSum}{1}";
                    }
                    else
                    {
                        checkSum = $"{checkSum}{0}";
                    }
                }

                input = checkSum;
            }

            Console.WriteLine($"Checksum = {checkSum}");
        }
    }
}
