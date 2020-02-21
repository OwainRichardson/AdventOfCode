using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_08_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day08_full.txt");
            List<Register> registers = new List<Register>();

            registers = CreateRequiredRegisters(registers, inputs);

            int total = RunInstructions(registers, inputs);

            Console.WriteLine(total);
        }

        private static int RunInstructions(List<Register> registers, string[] inputs)
        {
            string pattern = string.Empty;
            Regex regex;
            int maxRegValue = 0;

            foreach (string input in inputs)
            {
                if (input.Contains(" inc "))
                {
                    pattern = @"(\w+) inc (-?\d+) if (\w+) (.+) (-?\d+)";
                    regex = new Regex(pattern);
                    Match incMatch = regex.Match(input);
                    string register = incMatch.Groups[1].Value;
                    int increase = int.Parse(incMatch.Groups[2].Value);
                    string registerToCheck = incMatch.Groups[3].Value;
                    string operation = incMatch.Groups[4].Value;
                    int valueToCheck = int.Parse(incMatch.Groups[5].Value);
                    int registerValue = registers.First(x => x.Name == registerToCheck).Value;

                    if (CheckValue(operation, registerValue, valueToCheck))
                    {
                        Register reg = registers.First(x => x.Name == register);
                        reg.Value += increase;
                    }
                }
                else if (input.Contains(" dec "))
                {
                    pattern = @"(\w+) dec (-?\d+) if (\w+) (.+) (-?\d+)";
                    regex = new Regex(pattern);
                    Match incMatch = regex.Match(input);
                    string register = incMatch.Groups[1].Value;
                    int decrease = int.Parse(incMatch.Groups[2].Value);
                    string registerToCheck = incMatch.Groups[3].Value;
                    string operation = incMatch.Groups[4].Value;
                    int valueToCheck = int.Parse(incMatch.Groups[5].Value);
                    int registerValue = registers.First(x => x.Name == registerToCheck).Value;

                    if (CheckValue(operation, registerValue, valueToCheck))
                    {
                        Register reg = registers.First(x => x.Name == register);
                        reg.Value -= decrease;
                    }
                }

                int maxReg = registers.Max(x => x.Value);
                maxRegValue = maxReg > maxRegValue ? maxReg : maxRegValue;
            }


            return maxRegValue;
        }

        private static bool CheckValue(string operation, int registerValue, int valueToCheck)
        {
            if (operation == "<" && registerValue < valueToCheck)
            {
                return true;
            }
            else if (operation == "<=" && registerValue <= valueToCheck)
            {
                return true;
            }
            else if (operation == ">" && registerValue > valueToCheck)
            {
                return true;
            }
            else if (operation == ">=" && registerValue >= valueToCheck)
            {
                return true;
            }
            else if (operation == "==" && registerValue == valueToCheck)
            {
                return true;
            }
            else if (operation == "!=" && registerValue != valueToCheck)
            {
                return true;
            }

            return false;
        }

        private static List<Register> CreateRequiredRegisters(List<Register> registers, string[] inputs)
        {
            string pattern = @"^(\w+)";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                string register = regex.Match(input).Value;

                if (!registers.Any(x => x.Name == register))
                {
                    registers.Add(new Register { Name = register, Value = 0 });
                }
            }

            return registers;
        }
    }
}
