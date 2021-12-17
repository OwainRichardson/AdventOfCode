using AdventOfCode.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2015
{
    public static class D_12_2
    {
        private static List<int> _numbers = new List<int>();

        public static void Execute()
        {
            var input = string.Join(" ", File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day12_full.txt"));

            dynamic o = JsonConvert.DeserializeObject(input);

            CustomConsoleColour.SetAnswerColour();
            Console.WriteLine(GetSum(o, "red"));
            Console.ResetColor();
        }

        public static long GetSum(JObject o, string avoid = null)
        {
            bool shouldAvoid = o.Properties()
                .Select(a => a.Value).OfType<JValue>()
                .Select(v => v.Value).Contains(avoid);
            if (shouldAvoid) return 0;

            return o.Properties().Sum((dynamic a) => (long)GetSum(a.Value, avoid));
        }

        static long GetSum(JArray arr, string avoid = null) => arr.Sum((dynamic a) => (long)GetSum(a, avoid));

        static long GetSum(JValue val, string avoid = null) => val.Type == JTokenType.Integer ? (long)val.Value : 0;
    }
}
