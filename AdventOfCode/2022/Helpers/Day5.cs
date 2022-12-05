using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022.Helpers
{
    public static class Day5
    {
        public static Tuple<int, int, int>[] ParseInstructions(string[] stacksAndInstructions)
        {
            int indexOfFreeRow = Array.IndexOf(stacksAndInstructions, stacksAndInstructions.Single(x => string.IsNullOrWhiteSpace(x)));
            int numberOfInstructions = (stacksAndInstructions.Length - indexOfFreeRow) - 1;

            Tuple<int, int, int>[] instructions = new Tuple<int, int, int>[numberOfInstructions];
            int instructionsIndex = 0;

            for (int index = indexOfFreeRow + 1; index < stacksAndInstructions.Length; index++)
            {
                string pattern = @"^move\W(\d+)\Wfrom\W(\d+)\Wto\W(\d+)$";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(stacksAndInstructions[index]);

                instructions[instructionsIndex] = new Tuple<int, int, int>(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));

                instructionsIndex += 1;
            }

            return instructions;
        }

        public static Stack<string>[] ParseStacks(string[] stacksAndInstructions)
        {
            var stackNumbers = stacksAndInstructions.First(x => x.Replace(" ", "").All(y => char.IsDigit(y)));
            int indexOfStackNumbers = Array.IndexOf(stacksAndInstructions, stackNumbers);
            int maxStack = -1;

            foreach (char stack in stackNumbers)
            {
                if (char.IsWhiteSpace(stack)) continue;
                if (char.IsDigit(stack))
                {
                    maxStack = int.Parse(stack.ToString());
                }
            }

            Stack<string>[] stacks = new Stack<string>[maxStack];
            for (int i = 0; i < maxStack; i++)
            {
                stacks[i] = new Stack<string>();
            }

            for (int index = indexOfStackNumbers - 1; index >= 0; index--)
            {
                int currentStackIndex = 0;
                string currentStack = stacksAndInstructions[index];
                int stackNumber = 0;

                while (currentStackIndex < currentStack.Length)
                {
                    if (char.IsWhiteSpace(currentStack[currentStackIndex]))
                    {
                        currentStackIndex += 4;
                        stackNumber += 1;

                        continue;
                    }
                    if (currentStack[currentStackIndex].ToString() == "[")
                    {
                        stacks[stackNumber].Push(currentStack[currentStackIndex + 1].ToString());
                        currentStackIndex += 4;
                        stackNumber += 1;

                        continue;
                    }
                }
            }

            return stacks;
        }
    }
}
