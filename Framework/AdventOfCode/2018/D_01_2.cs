using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode._2018
{
    public static class D_01_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2018\Data\day01_full.txt");

            Dictionary<int, bool> values = new Dictionary<int, bool>();

            int total = 0;
            values.Add(total, false);
            bool finished = false;
            while (!finished)
            {
                foreach (string input in inputs)
                {
                    total += int.Parse(input);

                    if (!values.ContainsKey(total))
                    {
                        values.Add(total, false);
                    }
                    else
                    {
                        Console.WriteLine(total);
                        finished = true;
                        break;
                    }
                }
            }
        }
    }
}
