using AdventOfCode._2015.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2015
{
    public class D_23_Computer
    {
        internal List<Register> Execute(List<Register> registers)
        {
            List<Instruction> instructions = ParseInputs();

            for (int index = 0; index <= instructions.Count - 1;)
            {
                Register register;

                if (registers.Any(x => x.Name == instructions[index].Register))
                {
                    register = registers.First(x => x.Name == instructions[index].Register);
                }
                else
                {
                    register = new Register
                    {
                        Name = instructions[index].Register
                    };

                    registers.Add(register);
                }

                switch (instructions[index].Code)
                {
                    case "inc":
                        register.Value += 1;
                        index++;
                        continue;
                    case "tpl":
                        register.Value *= 3;
                        index++;
                        continue;
                    case "hlf":
                        register.Value /= 2;
                        index++;
                        continue;
                    case "jmp":
                        index += instructions[index].JumpValue;
                        continue;
                    case "jie":
                        if (register.Value % 2 == 0)
                        {
                            index += instructions[index].JumpValue;
                        }
                        else
                        {
                            index++;
                        }
                        continue;
                    case "jio":
                        if (register.Value == 1)
                        {
                            index += instructions[index].JumpValue;
                        }
                        else
                        {
                            index++;
                        }
                        continue;
                    default:
                        continue;
                }
            }

            return registers;
        }

        private List<Instruction> ParseInputs()
        {
            List<Instruction> instructions = new List<Instruction>();
            var inputs = File.ReadAllLines("C:\\AdventOfCode\\2015\\Day 23\\full_input.txt");

            // Group 1 match is action code
            // Group 2 match is register to perform action on
            // Group 3 match is number to jump
            var regexPattern = @"([a-z]{3})\s{1}([a-z]{1})?[\,]?\s{0,1}([\+|\-]{0,1}[\d]*)";

            foreach (var input in inputs)
            {
                Match match = Regex.Match(input, regexPattern);
                Instruction instruction = new Instruction
                {
                    Code = match.Groups[1].Value,
                    Register = match.Groups[2].Value,
                    JumpValue = !string.IsNullOrWhiteSpace(match.Groups[3].Value) ? int.Parse(match.Groups[3].Value) : 0
                };

                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}