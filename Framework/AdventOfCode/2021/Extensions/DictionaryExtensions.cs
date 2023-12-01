using System.Collections.Generic;

namespace AdventOfCode._2021.Extensions
{
    public static class DictionaryExtensions
    {
        public static void TryAdd(this Dictionary<string, int> dict, string coord)
        {
            if (dict.ContainsKey(coord))
            {
                dict[coord] += 1;
            }
            else
            {
                dict.Add(coord, 1);
            }
        }

        public static void TryAdd(this Dictionary<string, long> dict, string value)
        {
            if (dict.ContainsKey(value))
            {
                dict[value] += 1;
            }
            else
            {
                dict.Add(value, 1);
            }
        }
    }
}
