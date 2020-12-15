using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_14_2
    {
        public static string Mask { get; set; }

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day14.txt");
            Dictionary<long, long> mem = new Dictionary<long, long>();
            string pattern = @"^mem\[(\d+)\]\s{1}=\s{1}(\d+)$";
            Regex regex = new Regex(pattern);


            foreach (string input in inputs)
            {
                Console.Write($"\r{Array.IndexOf(inputs, input)} / {inputs.Length}");

                if (input.StartsWith("mask"))
                {
                    SetMask(input);
                }
                else if (input.StartsWith("mem"))
                {
                    Match match = regex.Match(input);
                    int initialIndex = int.Parse(match.Groups[1].Value);
                    long number = long.Parse(match.Groups[2].Value);

                    List<long> indexes = GetIndexes(initialIndex);

                    foreach (var index in indexes)
                    {
                        mem.AddOrUpdate(index, number);
                    }
                }
            }

            long total = 0;

            foreach (var entry in mem)
            {
                total += entry.Value;
            }

            Console.WriteLine($"\r{total}              ");
        }

        private static List<long> GetIndexes(int initialIndex)
        {
            string indexBinary = Convert.ToString(initialIndex, 2).PadLeft(36, '0');

            string maskedIndexBinary = ApplyMask(indexBinary);

            return CalculateAddresses(maskedIndexBinary).Select(x => ConvertToInteger(x)).ToList();
        }

        private static List<string> CalculateAddresses(string maskedIndexBinary)
        {
            int numberOfXs = maskedIndexBinary.Count(x => x.ToString() == "X");
            List<string> perms = new List<string>();
            for (int i = 0; i <= numberOfXs; i++)
            {
                List<string> combination = new List<string>();

                for (int j = 0; j <= numberOfXs - 1; j++)
                {
                    if (i > j) combination.Add("1");
                    else combination.Add("0");
                }

                perms.AddRange(GetPermutations(combination));
            }

            List<string> distinctPerms = perms.Distinct().ToList();

            return ConvertBinaryWithPerms(distinctPerms, maskedIndexBinary);
        }

        private static List<string> ConvertBinaryWithPerms(List<string> perms, string maskedIndexBinary)
        {
            List<string> binaries = new List<string>();

            foreach (string perm in perms)
            {
                StringBuilder currentBinary = new StringBuilder(maskedIndexBinary);

                for (int index = 0; index < perm.Length; index++)
                {
                    int indexToReplace = currentBinary.ToString().IndexOf("X");
                    currentBinary[indexToReplace] = perm[index];
                }

                binaries.Add(currentBinary.ToString());
            }

            return binaries;
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
                if (Mask[index].ToString() == "X") builder.Append("X");
                else if (Mask[index].ToString() == "1") builder.Append("1");
                else if (Mask[index].ToString() == "0") builder.Append(binary[index]);
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

        private static List<string> GetPermutations(List<string> things, List<string> current = null)
        {
            List<string> res = new List<string>();
            if (current == null)
            {
                current = new List<string>();
            }
            if (things.Count > 0)
            {
                foreach (string t in things)
                {
                    List<string> newP = new List<string>(current);
                    newP.Add(t);

                    List<string> newThings = new List<string>(things);
                    newThings.Remove(t);
                    res.AddRange(GetPermutations(newThings, newP));
                }
            }
            else
            {
                string permut = string.Join("", current);
                if (!res.Contains(permut))
                {
                    res.Add(permut);
                }
            }

            return res;
        }
    }
}
