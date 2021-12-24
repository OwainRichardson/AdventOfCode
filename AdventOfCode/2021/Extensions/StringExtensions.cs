using System;
using System.Text;

namespace AdventOfCode._2021.Extensions
{
    public static class StringExtensions
    {
        public static string Rotate(this string input, int places)
        {
            string[] split = input.Split(',');

            StringBuilder sb = new StringBuilder();
            for (int i = places; i < places + split.Length; i++)
            {
                sb.Append($"{split[i % split.Length]},");
            }

            string s = sb.ToString();
            return s.Substring(0, s.Length - 1);
        }
    }
}
