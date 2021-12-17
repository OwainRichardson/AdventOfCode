using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2016
{
    public static class D_21_2
    {
        public static void Execute()
        {
            var instructions = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day21_full.txt");
            D_21_2_Scrambler scrambler = new D_21_2_Scrambler();
            string input = "fbgdceah";

            foreach (string instruction in instructions.Reverse())
            {
                input = scrambler.Execute(input, instruction);
            }

            Console.WriteLine($"{input}");
        }
    }
}
