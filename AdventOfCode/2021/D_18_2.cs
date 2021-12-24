using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_18_2
    {
        private static int _largestSum = 0;
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day18.txt");

            for (int i = 0; i < inputs.Length; i++)
            {
                string input = inputs[i];

                for (int y = 0; y < inputs.Length; y++)
                {
                    if (i == y) continue;

                    ReduceInput(input, inputs[y]);
                }
            }

            Console.WriteLine(_largestSum);
        }

        private static int CalculateMagnitude(string input)
        {
            while (input.Contains("["))
            {
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == ']')
                    {
                        int openingBracketIndex = input.LastIndexOf('[', i);

                        string toSum = input.Substring(openingBracketIndex + 1, i - openingBracketIndex - 1);
                        int[] split = toSum.Split(',').Select(x => int.Parse(x)).ToArray();

                        input = input.Remove(openingBracketIndex, i - openingBracketIndex + 1).Insert(openingBracketIndex, $"{(3 * split[0]) + (2 * split[1])}");
                        break;
                    }
                }
            }

            return int.Parse(input);
        }

        private static void ReduceInput(string input, string addition)
        {
            input = AddNewString(input, addition);

            bool exploding = true;
            bool splitting = true;
            while (exploding || splitting)
            {
                exploding = false;
                splitting = false;

                int indentCount = 0;
                for (int index = 0; index < input.Length; index++)
                {
                    if (input[index] == '[')
                    {
                        indentCount++;
                    }
                    if (input[index] == ']')
                    {
                        indentCount--;
                    }

                    if (indentCount == 5)
                    {
                        input = ExplodePair(input, index);

                        exploding = true;
                        break;
                    }
                }

                if (!exploding)
                {
                    for (int index = 0; index < input.Length; index++)
                    {
                        if (char.IsDigit(input[index]) && char.IsDigit(input[index + 1]))
                        {
                            input = SplitNumber(input, index);
                            splitting = true;
                            break;
                        }
                    }
                }
            }

            int total = CalculateMagnitude(input);
            if (total > _largestSum) _largestSum = total;
        }

        private static string SplitNumber(string input, int index)
        {
            int numberToSplit = int.Parse($"{input[index]}{input[index + 1]}");

            string insert = $"[{Math.Floor((double)numberToSplit / 2)},{Math.Ceiling((double)numberToSplit / 2)}]";

            return input.Remove(index, 2).Insert(index, insert);
        }

        private static string AddNewString(string input, string addition)
        {
            return $"[{input},{addition}]";
        }

        private static string ExplodePair(string input, int index)
        {
            int indexOfNextClose = input.IndexOf(']', index);

            string exploding = input.Substring(index + 1, indexOfNextClose - index - 1);
            int[] split = exploding.Split(',').Select(x => int.Parse(x)).ToArray();

            for (int backIndex = index - 1; backIndex >= 0; backIndex--)
            {
                if (char.IsDigit(input[backIndex]))
                {
                    if (char.IsDigit(input[backIndex - 1]))
                    {
                        int newNumber = int.Parse($"{input[backIndex - 1]}{input[backIndex]}") + split[0];
                        input = input.Remove(backIndex - 1, 2).Insert(backIndex - 1, $"{newNumber}");
                    }
                    else
                    {
                        int newNumber = int.Parse(input[backIndex].ToString()) + split[0];
                        input = input.Remove(backIndex, 1).Insert(backIndex, $"{newNumber}");

                        if (newNumber > 9)
                        {
                            index += 1;
                            indexOfNextClose += 1;
                        }
                    }

                    break;
                }
            }

            for (int nextIndex = indexOfNextClose + 1; nextIndex < input.Length; nextIndex++)
            {
                if (char.IsDigit(input[nextIndex]))
                {
                    if (char.IsDigit(input[nextIndex + 1]))
                    {
                        int newNumber = int.Parse($"{input[nextIndex]}{input[nextIndex + 1]}") + split[1];
                        input = input.Remove(nextIndex, 2).Insert(nextIndex, $"{newNumber}");
                    }
                    else
                    {
                        int newNumber = int.Parse(input[nextIndex].ToString()) + split[1];
                        input = input.Remove(nextIndex, 1).Insert(nextIndex, $"{newNumber}");
                    }

                    break;
                }
            }

            input = input.Remove(index, indexOfNextClose - index + 1).Insert(index, "0");

            return input;
        }
    }
}