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
    public static class D_18_1
    {
        private static long _globalValue = 0;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day18_full.txt");
            List<Register> registers = new List<Register>();

            bool stopped = false;
            long index = 0;
            while (!stopped)
            {
                registers = ParseInput(registers, inputs[index], ref stopped, ref index);
            }
        }

        private static List<Register> ParseInput(List<Register> registers, string input, ref bool stopped, ref long index)
        {
            if (input.StartsWith("set"))
            {
                registers = SetRegister(registers, input);
                index++;
            }
            else if (input.StartsWith("add"))
            {
                registers = AddRegister(registers, input);
                index++;
            }
            else if (input.StartsWith("mul"))
            {
                registers = MulRegister(registers, input);
                index++;
            }
            else if (input.StartsWith("mod"))
            {
                registers = ModRegister(registers, input);
                index++;
            }
            else if (input.StartsWith("snd"))
            {
                SndValue(registers, input);
                index++;
            }
            else if (input.StartsWith("rcv"))
            {
                if (RcvValue())
                {
                    stopped = true;
                }

                index++;
            }
            else if (input.StartsWith("jgz"))
            {
                JgzRegister(registers, input, ref index);
            }

            return registers;
        }

        private static void JgzRegister(List<Register> registers, string input, ref long index)
        {
            string pattern = @"jgz (\w+) (.+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            long registerValue = GetValue(registers, match, 1);

            if (registerValue > 0)
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

        private static List<Register> AddRegister(List<Register> registers, string input)
        {
            string pattern = @"add (\w+) (.+)";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(input);

            if (registers.Any(x => x.Name == match.Groups[1].Value))
            {
                Register register = registers.First(x => x.Name == match.Groups[1].Value);

                long value = GetValue(registers, match);

                register.Value += value;
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
