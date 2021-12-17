using System;
using System.IO;

namespace AdventOfCode._2016
{
    public static class D_21_1
    {
        public static void Execute()
        {
            var instructions = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day21_full.txt");
            D_21_1_Scrambler scrambler = new D_21_1_Scrambler();
            string input = "abcdefgh";

            foreach (string instruction in instructions)
            {
                input = scrambler.Execute(input, instruction);
            }

            Console.WriteLine($"{input}");
        }
    }
}
