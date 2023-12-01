using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_13_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day13.txt").ToArray();

            List<Pair> pairs = ParseInputs(inputs);

            List<int> indeces = new List<int>();
            int index = 1;
            foreach (Pair pair in pairs)
            {
                if (CheckPair(pair).Value)
                {
                    indeces.Add(index);
                }

                index += 1;
            }

            Console.WriteLine(indeces.Sum());
        }

        private static bool? CheckPair(Pair pair)
        {
            pair.Left = pair.Left.Substring(1, pair.Left.Length - 2);
            pair.Right = pair.Right.Substring(1, pair.Right.Length - 2);

            while (true)
            {
                if (string.IsNullOrWhiteSpace(pair.Left)) return true;
                if (string.IsNullOrWhiteSpace(pair.Right)) return false;

                string leftCharacter = GetCompareCharacter(pair.Left);
                string rightCharacter = GetCompareCharacter(pair.Right);

                if (leftCharacter.StartsWith("[") && rightCharacter.StartsWith("["))
                {
                    bool? checkResult = CheckPair(new Pair { Left = leftCharacter, Right = rightCharacter });
                    if (checkResult != null)
                    {
                        return checkResult;
                    }
                }
                else if (leftCharacter.StartsWith("[") || rightCharacter.StartsWith("["))
                {
                    if (leftCharacter.StartsWith("["))
                    {
                        rightCharacter = $"[{rightCharacter}]";
                    }
                    else
                    {
                        leftCharacter = $"[{leftCharacter}]";
                    }

                    bool? checkResult = CheckPair(new Pair { Left = leftCharacter, Right = rightCharacter });
                    if (checkResult != null)
                    {
                        return checkResult;
                    }
                }
                else
                {
                    if (leftCharacter.Contains(',') && rightCharacter.Contains(','))
                    {
                        int[] left = leftCharacter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                        int[] right = rightCharacter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                        for (int i = 0; i < left.Length; i++)
                        {
                            if (i == right.Length) return false;
                            if (left[i] < right[i]) return true;
                            if (left[i] > right[i]) return false;
                        }

                        if (left.Length < right.Length) return true;
                        if (left.Length > right.Length) return false;
                        return null;
                    }
                    else if (leftCharacter.Contains(',') || rightCharacter.Contains(','))
                    {
                        if (leftCharacter.Contains(','))
                        {
                            int[] left = leftCharacter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                            if (left[0] < int.Parse(rightCharacter)) return true;

                            return false;
                        }
                        else
                        {
                            int[] right = rightCharacter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                            if (int.Parse(leftCharacter) > right[0]) return false;

                            return true;
                        }
                    }
                    else
                    {
                        int left = int.Parse(leftCharacter);
                        int right = int.Parse(rightCharacter);

                        if (right < left) return false;
                        if (left < right) return true;
                    }
                }

                if (pair.Left.Length == 1) return null;
                if (pair.Right.Length == 1) return false;

                if (pair.Left.StartsWith(leftCharacter))
                {
                    pair.Left = pair.Left.Substring(leftCharacter.Length + 1);
                }
                else if (pair.Left.StartsWith(leftCharacter.Substring(1, leftCharacter.Length - 2)))
                {
                    pair.Left = pair.Left.Substring(leftCharacter.Substring(1, leftCharacter.Length - 2).Length + 1);
                }
                if (pair.Right.StartsWith(rightCharacter))
                {
                    pair.Right = pair.Right.Substring(rightCharacter.Length + 1);
                }
                else if (pair.Right.StartsWith(rightCharacter.Substring(1, rightCharacter.Length - 2)))
                {
                    pair.Right = pair.Right.Substring(rightCharacter.Substring(1, rightCharacter.Length - 2).Length + 1);
                }
            }
        }

        private static string GetCompareCharacter(string input)
        {
            if (char.IsDigit(input[0]))
            {
                if (!input.Any(c => c == '['))
                {
                    return input;
                }

                string pattern = @"^(\d+)";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(input);
                return match.Groups[1].Value;
            }
            else if (input[0] == '[')
            {
                int indexOfEndBracket = -1;
                int openBrackets = 1;
                for (int i = 1; i < input.Length; i++)
                {
                    if (input[i] == '[') openBrackets += 1;
                    if (input[i] == ']') openBrackets -= 1;
                    if (openBrackets == 0)
                    {
                        indexOfEndBracket = i;
                        break;
                    }
                }

                return input.Substring(0, indexOfEndBracket + 1);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static List<Pair> ParseInputs(string[] inputs)
        {
            List<Pair> pairs = new List<Pair>();
            Pair currentPair = new Pair();

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    pairs.Add(currentPair);
                    currentPair = new Pair();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(currentPair.Left))
                    {
                        currentPair.Left = input;
                    }
                    else
                    {
                        currentPair.Right = input;
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(currentPair.Right))
            {
                pairs.Add(currentPair);
            }

            return pairs;
        }
    }
}