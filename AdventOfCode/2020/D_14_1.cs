using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_14_1
    {
        public static string Mask { get; set; }

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day14.txt");
            Dictionary<int, string> mem = new Dictionary<int, string>();
            string pattern = @"^mem\[(\d+)\]\s{1}=\s{1}(\d+)$";
            Regex regex = new Regex(pattern);


            foreach (string input in inputs)
            {
                if (input.StartsWith("mask"))
                {
                    SetMask(input);
                }
                else if (input.StartsWith("mem"))
                {
                    Match match = regex.Match(input);
                    int index = int.Parse(match.Groups[1].Value);
                    int number = int.Parse(match.Groups[2].Value);

                    string binary = ParseNumberTo64BitBinary(number);

                    mem.AddOrUpdate(index, ApplyMask(binary));
                }
            }

            long total = 0;

            foreach (var entry in mem)
            {
                total += ConvertToInteger(entry.Value);
            }

            Console.WriteLine(total);
        }

        private static long ConvertToInteger(string value)
        {
            return Convert.ToInt64(value, 2);
        }

        private static string ApplyMask(string binary)
        {
            if (binary.Length != Mask.Length) throw new ArgumentException();
            StringBuilder builder = new StringBuilder();

            for (int index = 0; index < binary.Length; index++)
            {
                if (Mask[index].ToString() == "X") builder.Append(binary[index]);
                else builder.Append(Mask[index]);
            }

            return builder.ToString();
        }

        private static string ParseNumberTo64BitBinary(int number)
        {
            return Convert.ToString(number, 2).PadLeft(36, '0');
        }

        private static void SetMask(string input)
        {
            var split = input.Split(new string[] { "mask = " }, StringSplitOptions.RemoveEmptyEntries);

            Mask = split[0];
        }
    }
}
