using AdventOfCode._2022.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2022
{
    public class D_05_2
    {
        public static void Execute()
        {
            string[] stacksAndInstructions = File.ReadAllLines(@"2022\Data\day05.txt").ToArray();

            Stack<string>[] stacks = Day5.ParseStacks(stacksAndInstructions);
            Tuple<int, int, int>[] instructions = Day5.ParseInstructions(stacksAndInstructions);

            OrganiseStacks(instructions, stacks);
        }

        private static void OrganiseStacks(Tuple<int, int, int>[] instructions, Stack<string>[] stacks)
        {
            foreach (var instruction in instructions)
            {
                int number = 0;
                Stack<string> newStack = new Stack<string>();
                while (number < instruction.Item1)
                {
                    string crate = stacks[instruction.Item2 - 1].Pop().ToString();
                    newStack.Push(crate);
                    
                    number += 1;
                }

                while (newStack.Count > 0)
                {
                    stacks[instruction.Item3 - 1].Push(newStack.Pop());
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (Stack<string> stack in stacks)
            {
                sb.Append(stack.Pop());
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
