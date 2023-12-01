using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017
{
    public static class D_23_1
    {
        private static long _globalValue = 0;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day23_full.txt");
            List<Register> registers = new List<Register>
            {
                new Register { Name = "a", Value = 0},
                new Register { Name = "b", Value = 0},
                new Register { Name = "c", Value = 0},
                new Register { Name = "d", Value = 0},
                new Register { Name = "e", Value = 0},
                new Register { Name = "f", Value = 0},
                new Register { Name = "g", Value = 0},
                new Register { Name = "h", Value = 0},
            };
            long index = 0;
            int mulCount = 0;

            while (index < inputs.Length)
            {

                if (inputs[index].StartsWith("set"))
                {
                    registers = SetRegister(registers, inputs[index]);
                    index++;
                }
                else if (inputs[index].StartsWith("sub"))
                {
                    registers = SubRegister(registers, inputs[index]);
                    index++;
                }
                else if (inputs[index].StartsWith("mul"))
                {
                    registers = MulRegister(registers, inputs[index]);
                    mulCount++;
                    index++;
                }
                else if (inputs[index].StartsWith("jnz"))
                {
                    JnzRegister(registers, inputs[index], ref index);
                }
            }

            Console.WriteLine(mulCount);
        }

        private static void JnzRegister(List<Register> registers, string input, ref long index)
        {
            string pattern = @"jnz (\w+) (.+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            long registerValue = GetValue(registers, match, 1);

            if (registerValue != 0)
            {
                long jumpValue = GetValue(registers, match, 2);

                index += jumpValue;
            }
            else
            {
                index++;
            }
        }

        private static bool RcvValue()
        {
            if (_globalValue != 0)
            {
                Console.WriteLine(_globalValue);
                return true;
            }

            return false;
        }

        private static void SndValue(List<Register> registers, string input)
        {
            string pattern = @"snd (\w+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            var value = GetValue(registers, match, 1);

            _globalValue = value;
        }

        private static List<Register> SubRegister(List<Register> registers, string input)
        {
            string pattern = @"sub (\w+) (.+)";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(input);

            if (registers.Any(x => x.Name == match.Groups[1].Value))
            {
                Register register = registers.First(x => x.Name == match.Groups[1].Value);

                long value = GetValue(registers, match);

                register.Value -= value;
            }
            else
            {
                throw new ArgumentException();
            }

            return registers;
        }

        private static List<Register> ModRegister(List<Register> registers, string input)
        {
            string pattern = @"mod (\w+) (.+)";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(input);

            if (registers.Any(x => x.Name == match.Groups[1].Value))
            {
                Register register = registers.First(x => x.Name == match.Groups[1].Value);

                long value = GetValue(registers, match);

                register.Value = register.Value % value;
            }
            else
            {
                throw new ArgumentException();
            }

            return registers;
        }

        private static List<Register> MulRegister(List<Register> registers, string input)
        {
            string pattern = @"mul (\w+) (.+)";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(input);

            if (registers.Any(x => x.Name == match.Groups[1].Value))
            {
                Register register = registers.First(x => x.Name == match.Groups[1].Value);

                long value = GetValue(registers, match);

                register.Value *= value;
            }
            else
            {
                registers.Add(new Register
                {
                    Name = match.Groups[1].Value,
                    Value = 0
                });
            }

            return registers;
        }

        private static long GetValue(List<Register> registers, Match match, int index = 2)
        {
            int value = 0;
            if (int.TryParse(match.Groups[index].Value, out value))
            {
                return value;
            }
            else
            {
                Register multReg = registers.First(x => x.Name == match.Groups[index].Value);

                return multReg.Value;
            }
        }

        private static List<Register> SetRegister(List<Register> registers, string input)
        {
            string pattern = @"set (\w+) (.+)";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(input);

            if (registers.Any(x => x.Name == match.Groups[1].Value))
            {
                Register register = registers.First(x => x.Name == match.Groups[1].Value);
                register.Value = GetValue(registers, match);
            }
            else
            {
                registers.Add(new Register
                {
                    Name = match.Groups[1].Value,
                    Value = GetValue(registers, match)
                });
            }

            return registers;
        }
    }
}
