using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2015
{
    public static class D_07_1
    {
        private static List<Wire> _wires = new List<Wire>();

        public static void Execute()
        {
            var circuit = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day07_full.txt");

            ParseInput(circuit);

            CalculateWireValues();

            foreach (var wire in _wires.OrderBy(x => x.Id))
            {
                if (wire.Id.ToLower().Equals("a"))
                {
                    Console.Write($"Wire {wire.Id}: ");
                    CustomConsoleColour.SetAnswerColour();
                    Console.Write(wire.Value);
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }

        private static void CalculateWireValues()
        {
            while (_wires.Any(x => x.Value == -999))
            {
                foreach (var wire in _wires.Where(x => x.Value == -999))
                {
                    long value = CalculateValue(wire.Instruction);
                    wire.Value = value;
                }
            }
        }

        private static void ParseInput(string[] input)
        {
            foreach (var instruction in input)
            {
                string[] splitInstruction = instruction.Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);

                long value = 0;

                if (long.TryParse(splitInstruction[0], out value))
                {
                    _wires.Add(new Wire
                    {
                        Id = splitInstruction[1],
                        Instruction = splitInstruction[0],
                        Value = value
                    });
                }
                else
                {
                    _wires.Add(new Wire
                    {
                        Id = splitInstruction[1],
                        Instruction = splitInstruction[0],
                        Value = -999
                    });
                }
            }
        }

        private static long CalculateValue(string input)
        {
            if (input.ToLower().Contains("and"))
            {
                string[] variables = input.Split(new string[] { " AND " }, StringSplitOptions.RemoveEmptyEntries);
                long value1 = CalculateVariableValue(variables[0]);
                long value2 = CalculateVariableValue(variables[1]);

                if (value1 == -999 || value2 == -999)
                {
                    return -999;
                }

                return value1 & value2;
            }

            if (input.ToLower().Contains("or"))
            {
                string[] variables = input.Split(new string[] { " OR " }, StringSplitOptions.RemoveEmptyEntries);
                long value1 = CalculateVariableValue(variables[0]);
                long value2 = CalculateVariableValue(variables[1]);

                if (value1 == -999 || value2 == -999)
                {
                    return -999;
                }

                return value1 | value2;
            }

            if (input.ToLower().Contains("lshift"))
            {
                string[] variables = input.Split(new string[] { " LSHIFT " }, StringSplitOptions.RemoveEmptyEntries);
                long value1 = CalculateVariableValue(variables[0]);

                if (value1 == -999)
                {
                    return -999;
                }

                return value1 << int.Parse(variables[1]);
            }

            if (input.ToLower().Contains("rshift"))
            {
                string[] variables = input.Split(new string[] { " RSHIFT " }, StringSplitOptions.RemoveEmptyEntries);
                long value1 = CalculateVariableValue(variables[0]);

                if (value1 == -999)
                {
                    return -999;
                }

                return value1 >> int.Parse(variables[1]);
            }

            if (input.ToLower().Contains("not"))
            {
                string variable = input.Replace("NOT ", "");
                long value = CalculateVariableValue(variable);

                if (value == -999)
                {
                    return -999;
                }

                return 65536 + ~value;
            }

            if (_wires.FirstOrDefault(x => x.Id == input) != null)
            {
                return _wires.First(x => x.Id == input).Value;
            }

            return -999;
        }

        private static long CalculateVariableValue(string v)
        {
            long value;
            if (long.TryParse(v, out value))
            {
                return value;
            }

            return _wires.First(x => x.Id.Equals(v)).Value;
        }
    }
}
