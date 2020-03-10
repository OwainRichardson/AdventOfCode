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
    public static class D_18_2
    {
        //public static void Execute()
        //{
        //    string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day18_full.txt");

        //    D_18_Computer programZero = new D_18_Computer(0, inputs);
        //    D_18_Computer programOne = new D_18_Computer(1, inputs);
        //}
    }

    //public static class D_18_Computer
    //{
    //    private static int _registerPValue = 0;
    //    private static string[] _inputs = new string[0];
    //    public static int _computerId = -1;
    //    public static List<long> receiveQueue = new List<long>();
    //    public static bool stopped = false;
    //    public static bool finished = false;
    //    public static bool waiting = false;

    //    public static void Execute(int registerPValue, string[] inputs, D_18_Computer otherComputer)
    //    {
    //        _registerPValue = registerPValue;
    //        _inputs = inputs;
    //        _computerId = registerPValue;
    //        _otherComputer = otherComputer;

    //        List<Register> registers = new List<Register>
    //        {
    //            new Register
    //            {
    //                Name = "p",
    //                Value = _registerPValue
    //            }
    //        };
    //        long index = 0;

    //        while (!finished)
    //        {
    //            if (waiting)
    //            {

    //            }

    //            registers = ParseInput(registers, _inputs[index], ref stopped, ref index, otherComputer);
    //        }
    //    }

    //    private static List<Register> ParseInput(List<Register> registers, string input, ref bool stopped, ref long index, D_18_Computer otherComputer)
    //    {
    //        if (input.StartsWith("set"))
    //        {
    //            registers = SetRegister(registers, input);
    //            index++;
    //        }
    //        else if (input.StartsWith("add"))
    //        {
    //            registers = AddRegister(registers, input);
    //            index++;
    //        }
    //        else if (input.StartsWith("mul"))
    //        {
    //            registers = MulRegister(registers, input);
    //            index++;
    //        }
    //        else if (input.StartsWith("mod"))
    //        {
    //            registers = ModRegister(registers, input);
    //            index++;
    //        }
    //        else if (input.StartsWith("snd"))
    //        {
    //            SndValue(registers, input);
    //            index++;
    //        }
    //        else if (input.StartsWith("rcv"))
    //        {
    //            if (RcvValue(ref registers, input))
    //            {
    //                index++;
    //            }
    //            else
    //            {
    //                waiting = true;
    //            }
    //        }
    //        else if (input.StartsWith("jgz"))
    //        {
    //            JgzRegister(registers, input, ref index);
    //        }

    //        return registers;
    //    }

    //    private static void JgzRegister(List<Register> registers, string input, ref long index)
    //    {
    //        string pattern = @"jgz (\w+) (.+)";
    //        Regex regex = new Regex(pattern);
    //        Match match = regex.Match(input);

    //        long registerValue = GetValue(registers, match, 1);

    //        if (registerValue > 0)
    //        {
    //            long jumpValue = GetValue(registers, match, 2);

    //            index += jumpValue;
    //        }
    //        else
    //        {
    //            index++;
    //        }
    //    }

    //    private static bool RcvValue(ref List<Register> registers, string input)
    //    {
    //        string pattern = @"rcv (\w+)";
    //        Regex regex = new Regex(pattern);

    //        Match match = regex.Match(input);
    //        string registerName = match.Groups[1].Value;

    //        Register register = registers.First(x => x.Name == registerName);

    //        if (receiveQueue.Any())
    //        {
    //            register.Value = receiveQueue.First();
    //            receiveQueue = receiveQueue.Skip(1).ToList();

    //            return true;
    //        }
    //        else
    //        {
    //            waiting = true;
    //            return false;
    //        }
    //    }

    //    private static void SndValue(List<Register> registers, string input)
    //    {
    //        string pattern = @"snd (\w+)";
    //        Regex regex = new Regex(pattern);
    //        Match match = regex.Match(input);

    //        var value = GetValue(registers, match, 1);
    //    }

    //    private static List<Register> AddRegister(List<Register> registers, string input)
    //    {
    //        string pattern = @"add (\w+) (.+)";
    //        Regex regex = new Regex(pattern);

    //        Match match = regex.Match(input);

    //        if (registers.Any(x => x.Name == match.Groups[1].Value))
    //        {
    //            Register register = registers.First(x => x.Name == match.Groups[1].Value);

    //            long value = GetValue(registers, match);

    //            register.Value += value;
    //        }
    //        else
    //        {
    //            throw new ArgumentException();
    //        }

    //        return registers;
    //    }

    //    private static List<Register> ModRegister(List<Register> registers, string input)
    //    {
    //        string pattern = @"mod (\w+) (.+)";
    //        Regex regex = new Regex(pattern);

    //        Match match = regex.Match(input);

    //        if (registers.Any(x => x.Name == match.Groups[1].Value))
    //        {
    //            Register register = registers.First(x => x.Name == match.Groups[1].Value);

    //            long value = GetValue(registers, match);

    //            register.Value = register.Value % value;
    //        }
    //        else
    //        {
    //            throw new ArgumentException();
    //        }

    //        return registers;
    //    }

    //    private static List<Register> MulRegister(List<Register> registers, string input)
    //    {
    //        string pattern = @"mul (\w+) (.+)";
    //        Regex regex = new Regex(pattern);

    //        Match match = regex.Match(input);

    //        if (registers.Any(x => x.Name == match.Groups[1].Value))
    //        {
    //            Register register = registers.First(x => x.Name == match.Groups[1].Value);

    //            long value = GetValue(registers, match);

    //            register.Value *= value;
    //        }
    //        else
    //        {
    //            registers.Add(new Register
    //            {
    //                Name = match.Groups[1].Value,
    //                Value = 0
    //            });
    //        }

    //        return registers;
    //    }

    //    private static long GetValue(List<Register> registers, Match match, int index = 2)
    //    {
    //        int value = 0;
    //        if (int.TryParse(match.Groups[index].Value, out value))
    //        {
    //            return value;
    //        }
    //        else
    //        {
    //            Register multReg = registers.First(x => x.Name == match.Groups[index].Value);

    //            return multReg.Value;
    //        }
    //    }

    //    private static List<Register> SetRegister(List<Register> registers, string input)
    //    {
    //        string pattern = @"set (\w+) (.+)";
    //        Regex regex = new Regex(pattern);

    //        Match match = regex.Match(input);

    //        if (registers.Any(x => x.Name == match.Groups[1].Value))
    //        {
    //            Register register = registers.First(x => x.Name == match.Groups[1].Value);
    //            register.Value = GetValue(registers, match);
    //        }
    //        else
    //        {
    //            registers.Add(new Register
    //            {
    //                Name = match.Groups[1].Value,
    //                Value = GetValue(registers, match)
    //            });
    //        }

    //        return registers;
    //    }
    //}
}
