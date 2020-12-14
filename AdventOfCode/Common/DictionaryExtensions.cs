using System.Collections.Generic;

namespace AdventOfCode.Common
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate(this Dictionary<int, string> dictionary, int index, string value)
        {
            if (dictionary.ContainsKey(index))
            {
                dictionary[index] = value;
            }
            else
            {
                dictionary.Add(index, value);
            }    
        }

        public static void AddOrUpdate(this Dictionary<long, long> dictionary, long index, long value)
        {
            if (dictionary.ContainsKey(index))
            {
                dictionary[index] = value;
            }
            else
            {
                dictionary.Add(index, value);
            }
        }
    }
}
