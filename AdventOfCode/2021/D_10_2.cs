using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public static class D_10_2
    {
        public static void Execute()
        {
            List<string> inputs = File.ReadAllLines(@"2021\Data\day10.txt").ToList();

            List<string> chunksToRemove = new List<string>();
            foreach (string input in inputs)
            {
                bool chunkIsCorrupted = ParseChunks(input);

                if (chunkIsCorrupted)
                {
                    chunksToRemove.Add(input);
                }
            }

            foreach (string chunkToRemove in chunksToRemove)
            {
                inputs.Remove(chunkToRemove);
            }

            List<long> scores = new List<long>();
            foreach (string input in inputs)
            {
                scores.Add(CompleteChunks(input));
            }

            long score = scores.OrderBy(c => c).Skip(scores.Count / 2).Take(1).First();

            Console.WriteLine(score);
        }

        private static long CompleteChunks(string input)
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
                            if (digit == ")")
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "[":
                            if (digit == "]")
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "{":
                            if (digit == "}")
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "<":
                            if (digit == ">")
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return CompleteChunk(openedChunks);
        }

        private static long CompleteChunk(List<string> openedChunks)
        {
            long score = 0;

            for (int index = openedChunks.Count - 1; index >= 0; index--)
            {
                score *= 5;

                if (openedChunks[index] == "(") score += 1;
                if (openedChunks[index] == "[") score += 2;
                if (openedChunks[index] == "{") score += 3;
                if (openedChunks[index] == "<") score += 4;
            }

            return score;
        }

        private static bool ParseChunks(string input)
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
                                return true;
                            }
                            else
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "[":
                            if (digit != "]")
                            {
                                return true;
                            }
                            else
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "{":
                            if (digit != "}")
                            {
                                return true;
                            }
                            else
                            {
                                openedChunks.RemoveAt(openedChunks.Count - 1);
                            }
                            break;
                        case "<":
                            if (digit != ">")
                            {
                                return true;
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

            return false;
        }
    }
}
