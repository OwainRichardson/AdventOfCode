using AdventOfCode._2020.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_08_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day08.txt");

            List<Instruction> instructions = ParseInputs(inputs);

            int index = 0;
            int accumulator = 0;
            while (true)
            {
                var currentInstruction = instructions[index];

                if (currentInstruction.Run) break;

                switch (currentInstruction.Operation)
                {
                    case "nop":
                        index += 1;
                        currentInstruction.Run = true;
                        break;
                    case "acc":
                        index += 1;
                        currentInstruction.Run = true;
                        accumulator += currentInstruction.Number;
                        break;
                    case "jmp":
                        index += currentInstruction.Number;
                        currentInstruction.Run = true;
                        break;
                    default:
                        throw new Exception();
                }
            }

            Console.WriteLine(accumulator);
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
