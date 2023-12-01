using System;
using System.IO;

namespace AdventOfCode._2017
{
    public static class D_01_2
    {
        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day01_full.txt")[0];

            ReadInput(input);
        }

        private static void ReadInput(string input)
        {
            int total = 0;

            int increment = input.Length / 2;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + increment) % input.Length])
                {
                    total += int.Parse(input[i].ToString());
                }
            }

            Console.WriteLine(total);
        }
    }
}
