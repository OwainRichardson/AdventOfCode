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
    }
}
