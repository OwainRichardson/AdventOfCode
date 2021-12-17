using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_18_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day18.txt");

            long total = 0;
            foreach (string input in inputs)
            {
                total += EvaluateEquation(input);
            }

            Console.WriteLine(total);
        }

        private static long EvaluateEquation(string input)
        {
            while (input.Contains("("))
            {
                int indexOfStartBrackedToEvaluate = input.IndexOf("(");
                int indexOfEndBrackedToEvaluate = -1;

                for (int index = indexOfStartBrackedToEvaluate + 1; index < input.Length; index++)
                {
                    if (input[index].ToString() == ")")
                    {
                        indexOfEndBrackedToEvaluate = index;
                        break;
                    }
                    else if (input[index].ToString() == "(")
                    {
                        indexOfStartBrackedToEvaluate = index;
                    }
                }

                long result = EvaluateBrackets(input.Substring(indexOfStartBrackedToEvaluate, (indexOfEndBrackedToEvaluate - indexOfStartBrackedToEvaluate) + 1));

                string firstBitOfInput = input.Substring(0, indexOfStartBrackedToEvaluate);
                string lastBitOfInput = input.Substring(indexOfEndBrackedToEvaluate + 1);

                input = $"{firstBitOfInput}{result.ToString()}{lastBitOfInput}";
            }

            long equationResult = SolveEquation(input);

            //string pattern = @"^(\d+)\s([+|*]{1})\s(\d+)";
            //Regex regex = new Regex(pattern);
            //Match match = regex.Match(input);

            //string lastBitOfNewInput = input.Substring(match.Index + match.Length);

            //input = $"{equationResult.ToString()}{lastBitOfNewInput}";

            return equationResult;
        }

        private static long SolveEquation(string input)
        {
            string pattern = @"^(\d+)\s([+|*]{1})\s(\d+)";
            Regex regex = new Regex(pattern);

            long total = 0;

            while (input.Contains("+") || input.Contains("*"))
            {
                Match match = regex.Match(input);
                total = CalculateEquation(match.Groups[0].Value);

                string lastBitOfNewInput = input.Substring(match.Index + match.Length);

                input = $"{total.ToString()}{lastBitOfNewInput}";
            }

            return long.Parse(input);
        }

        private static long EvaluateBrackets(string equation)
        {
            equation = equation.Replace("(", "").Replace(")", "");

            return SolveEquation(equation);
        }

        private static long CalculateEquation(string equation)
        {
            string pattern = @"^(\d+)\s([+|*]{1})\s(\d+)$";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(equation);

            long firstNumber = long.Parse(match.Groups[1].Value);
            long secondNumber = long.Parse(match.Groups[3].Value);

            switch (match.Groups[2].Value)
            {
                case "*":
                    return firstNumber * secondNumber;
                case "+":
                    return firstNumber + secondNumber;
                default:
                    throw new ArgumentException();
            }
        }
    }
}