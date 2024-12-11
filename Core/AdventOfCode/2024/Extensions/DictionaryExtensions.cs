namespace AdventOfCode._2024.Extensions
{
    public static class DictionaryExtensions
    {
        public static void TryAddStone(this Dictionary<long, long> dictionary, long key, long value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] += value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}
