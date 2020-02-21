using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_05_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day05_full.txt");

            int[] instructions = ParseInstructions(inputs);

            PlayGame(instructions);
        }

        private static void PlayGame(int[] instructions)
        {
            int index = 0;
            int steps = 0;
            while (index < instructions.Length)
            {
                int nextIndex = index + instructions[index];

                instructions[index]++;

                index = nextIndex;
                steps++;
            }

            Console.WriteLine(steps);
        }

        private static int[] ParseInstructions(string[] inputs)
        {
            int[] instructions = new int[inputs.Length];

            for (int index = 0; index < inputs.Length; index++)
            {
                instructions[index] = int.Parse(inputs[index]);
            }

            return instructions;
        }
    }
}
