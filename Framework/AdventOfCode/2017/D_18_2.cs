using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017
{
    public static class D_18_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day18_full.txt");

            D_18_Computer programZero = new D_18_Computer(0, inputs);
            D_18_Computer programOne = new D_18_Computer(1, inputs);

            programZero.OtherQueue = programOne.Queue;
            programOne.OtherQueue = programZero.Queue;

            do
            {
                programOne.Execute();
                programZero.Execute();
            }
            while (programOne.Queue.Count != 0);

            Console.WriteLine($"Program 1 sent {programOne.SendCounter} values");
        }
    }

    public class D_18_Computer
    {
        private int _registerPValue = 0;
        private string[] _inputs = new string[0];
        public int _computerId = -1;
        public bool stopped = false;
        public bool finished = false;
        public bool Waiting { get; set; } = false;
        long index = 0;
        List<Register> registers = new List<Register>();
        public Queue<long> Queue { get; }
        public Queue<long> OtherQueue { get; set; }
        public int SendCounter { get; set; }
        public bool Finished { get; set; }

        public D_18_Computer(int pValue, string[] inputs)
        {
            _registerPValue = pValue;
            _inputs = inputs;
            _computerId = pValue;

            registers.Add(new Register
            {
                Name = "p",
                Value = pValue
            });

            Queue = new Queue<long>();
        }

        public void Execute()
        {
            while (true)
            {
                string input = _inputs[index];

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
                    SendCounter++;
                    index++;
                }
                else if (input.StartsWith("rcv"))
                {
                    if (RcvValue(ref registers, input))
                    {
                        index++;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (input.StartsWith("jgz"))
                {
                    JgzRegister(registers, input, ref index);
                }
            }
        }

        private List<Register> ParseInput(List<Register> registers, string input, ref bool stopped, ref long index, out long? send)
        {
            send = null;



            return registers;
        }

        private void JgzRegister(List<Register> registers, string input, ref long index)
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

        private bool RcvValue(ref List<Register> registers, string input)
        {
            string pattern = @"rcv (\w+)";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(input);
            string registerName = match.Groups[1].Value;

            Register register = registers.FirstOrDefault(x => x.Name == registerName);
            if (register == null)
            {
                register = new Register
                {
                    Name = registerName
                };

                registers.Add(register);
            }

            if (Queue.Count > 0)
            {
                register.Value = Queue.Dequeue();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void SndValue(List<Register> registers, string input)
        {
            string pattern = @"snd (\w+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            OtherQueue.Enqueue(GetValue(registers, match, 1));
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
