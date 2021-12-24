using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_24_1
    {
        private static int _inputIndex = 0;
        private static long _number = 99999999999999;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day24.txt");

            bool found = false;
            while (!found)
            {
                Dictionary<string, long> variables = new Dictionary<string, long>();

                foreach (string input in inputs)
                {
                    variables = ParseInput(input, variables);
                }

                _inputIndex = 0;
                if (variables["z"] == 0)
                {
                    break;
                }

                _number -= 1;

                while (_number.ToString().Contains("0")) _number -= 1;
            }

            Console.WriteLine(_number);
        }

        private static Dictionary<string, long> ParseInput(string input, Dictionary<string, long> variables)
        {
            if (input.StartsWith("inp"))
            {
                HandleInp(variables, input);
            }
            else if (input.StartsWith("add"))
            {
                HandleAdd(variables, input);
            }
            else if (input.StartsWith("mul"))
            {
                HandleMul(variables, input);
            }
            else if (input.StartsWith("div"))
            {
                HandleDiv(variables, input);
            }
            else if (input.StartsWith("mod"))
            {
                HandleMod(variables, input);
            }
            else if (input.StartsWith("eql"))
            {
                HandleEql(variables, input);
            }

            return variables;
        }

        private static void HandleInp(Dictionary<string, long> variables, string input)
        {
            int number = int.Parse(_number.ToString()[_inputIndex].ToString());
            string binary = Convert.ToString(number, 2);

            while (binary.Length < 4)
            {
                binary = $"0{binary}";
            }

            variables["z"] = int.Parse(binary[3].ToString());
            variables["y"] = int.Parse(binary[2].ToString());
            variables["x"] = int.Parse(binary[1].ToString());
            variables["w"] = int.Parse(binary[0].ToString());

            _inputIndex += 1;
        }

        private static void HandleEql(Dictionary<string, long> variables, string input)
        {
            string pattern = @"^eql\s(\S+)\s(\S+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            long first = GetValue(match.Groups[1].Value, variables);
            long second = GetValue(match.Groups[2].Value, variables);

            variables[match.Groups[1].Value] = first == second ? 1 : 0;
        }

        private static void HandleMod(Dictionary<string, long> variables, string input)
        {
            string pattern = @"^mod\s(\S+)\s(\S+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            long first = GetValue(match.Groups[1].Value, variables);
            long second = GetValue(match.Groups[2].Value, variables);

            variables[match.Groups[1].Value] = first % second;
        }

        private static void HandleDiv(Dictionary<string, long> variables, string input)
        {
            string pattern = @"^div\s(\S+)\s(\S+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            long first = GetValue(match.Groups[1].Value, variables);
            long second = GetValue(match.Groups[2].Value, variables);

            variables[match.Groups[1].Value] = first / second;
        }

        private static void HandleMul(Dictionary<string, long> variables, string input)
        {
            string pattern = @"^mul\s(\S+)\s(\S+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            long first = GetValue(match.Groups[1].Value, variables);
            long second = GetValue(match.Groups[2].Value, variables);

            variables[match.Groups[1].Value] = first * second;
        }

        private static void HandleAdd(Dictionary<string, long> variables, string input)
        {
            string pattern = @"^add\s(\S+)\s(\S+)$";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            long first = GetValue(match.Groups[1].Value, variables);
            long second = GetValue(match.Groups[2].Value, variables);

            variables[match.Groups[1].Value] = first + second;
        }

        private static long GetValue(string value, Dictionary<string, long> variables)
        {
            long returnValue;
            if (long.TryParse(value, out returnValue))
            {
                return returnValue;
            }

            return variables[value];
        }
    }
}