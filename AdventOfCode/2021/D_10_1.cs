using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_10_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day10.txt");

            int score = 0;
            foreach (string input in inputs)
            {
                score += ParseChunks(input);
            }

            Console.WriteLine(score);
        }

        private static int ParseChunks(string input)
        {
            List<string> openingChunks = new List<string> { "(", "[", "{", "<" };
            List<string> closingChunks = new List<string> { ")", "]", "}", ">" };

            List<string> openedChunks = new List<string>();

            for (int index = 0; index < input.Length; index++)
            {
                string digit = input[index].ToString();

                if (openingChunks.Contains(digit))
                {
                    openedChunks.Add(digit);
                }
                else
                {
                    string lastOpenedChunk = openedChunks.Last();

                    switch (lastOpenedChunk)
                    {
                        case "(":
                            if (digit != ")")
                            {
                                return CalculateScore(digit);
                            }
                            else
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "[":
                            if (digit != "]")
                            {
                                return CalculateScore(digit);
                            }
                            else
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "{":
                            if (digit != "}")
                            {
                                return CalculateScore(digit);
                            }
                            else
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "<":
                            if (digit != ">")
                            {
                                return CalculateScore(digit);
                            }
                            else
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return 0;
        }

        private static int CalculateScore(string digit)
        {
            switch (digit)
            {
                case ")":
                    return 3;
                    case "]":
                    return 57;
                case "}":
                    return 1197;
                case ">":
                    return 25137;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
