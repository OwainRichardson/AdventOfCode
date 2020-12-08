using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_08_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day08.txt");

            List<Instruction> instructions = ParseInputs(inputs);

            int accumulator = 0;
            bool success = false;
            int changeIndex = 0;
            int previousIndex = 0;

            while (!success)
            {
                instructions.ForEach(x => x.Run = false);

                instructions = ChangeInstruction(instructions, true, ref changeIndex, ref previousIndex);

                success = Run(instructions, out accumulator);
            }

            Console.WriteLine(accumulator);
        }

        private static List<Instruction> ChangeInstruction(List<Instruction> instructions, bool correctPrevious, ref int changeIndex, ref int previousIndex)
        {
            if (correctPrevious && changeIndex > 0)
            {
                if (instructions[previousIndex].Operation == "nop")
                {
                    instructions[previousIndex].Operation = "jmp";
                }
                else if (instructions[previousIndex].Operation == "jmp")
                {
                    instructions[previousIndex].Operation = "nop";
                }
            }

            if (instructions[changeIndex].Operation == "nop")
            {
                instructions[changeIndex].Operation = "jmp";
                previousIndex = changeIndex;
                changeIndex += 1;
            }
            else if (instructions[changeIndex].Operation == "jmp")
            {
                instructions[changeIndex].Operation = "nop";
                previousIndex = changeIndex;
                changeIndex += 1;
            }
            else if (instructions[changeIndex].Operation == "acc")
            {
                changeIndex += 1;
                ChangeInstruction(instructions, false, ref changeIndex, ref previousIndex);
            }

            return instructions;
        }

        private static bool Run(List<Instruction> instructions, out int accumulator)
        {
            int index = 0;
            bool infiniteLoop = false;
            int internalAccumulator = 0;

            while (index < instructions.Count)
            {
                var currentInstruction = instructions[index];

                if (currentInstruction.Run)
                {
                    infiniteLoop = true;
                    break;
                }

                switch (currentInstruction.Operation)
                {
                    case "nop":
                        index += 1;
                        currentInstruction.Run = true;
                        break;
                    case "acc":
                        index += 1;
                        currentInstruction.Run = true;
                        internalAccumulator += currentInstruction.Number;
                        break;
                    case "jmp":
                        index += currentInstruction.Number;
                        currentInstruction.Run = true;
                        break;
                    default:
                        throw new Exception();
                }
            }

            if (infiniteLoop)
            {
                accumulator = -99999;
                return false;
            }
            else
            {
                accumulator = internalAccumulator;
                return true;
            }
        }

        private static List<Instruction> ParseInputs(string[] inputs)
        {
            List<Instruction> instructions = new List<Instruction>();
            string pattern = @"^(\w{3})\s([+|-]\d+)$";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                Instruction instruction = new Instruction();

                Match match = regex.Match(input);

                instruction.Operation = match.Groups[1].Value;
                instruction.Number = int.Parse(match.Groups[2].Value);

                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}
