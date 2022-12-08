using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2022
{
    public class D_08_2
    {
        public static void Execute()
        { 
            string[] inputs = File.ReadAllLines(@"2022\Data\day08.txt").ToArray();

            List<Tree> trees = ParseInputs(inputs);

            CalculateScenicScore(trees);
        }

        private static void CalculateScenicScore(List<Tree> trees)
        {
            int maxY = trees.Max(t => t.Y);
            int maxX = trees.Max(y => y.X);

            foreach (Tree tree in trees)
            {
                if (tree.Y == 0)
                {
                    tree.ScenicScore = 0;
                }
                else if (tree.X == 0)
                {
                    tree.ScenicScore = 0;
                }
                else if (tree.Y == maxY)
                {
                    tree.ScenicScore = 0;
                }
                else if (tree.X == maxX)
                {
                    tree.ScenicScore = 0;
                }
                else
                {
                    int up = 0;
                    int down = 0;
                    int right = 0;
                    int left = 0;

                    for (int y = tree.Y - 1; y >= 0; y--)
                    {
                        Tree nextTree = trees.Single(t => t.Y == y && t.X == tree.X);
                        if (nextTree.Height < tree.Height)
                        {
                            up += 1;
                        }
                        else
                        {
                            up += 1;
                            break;
                        }
                    }
                    for (int y = tree.Y + 1; y <= maxY; y++)
                    {
                        Tree nextTree = trees.Single(t => t.Y == y && t.X == tree.X);
                        if (nextTree.Height < tree.Height)
                        {
                            down += 1;
                        }
                        else
                        {
                            down += 1;
                            break;
                        }
                    }
                    for (int x = tree.X + 1; x <= maxX; x++)
                    {
                        Tree nextTree = trees.Single(t => t.X == x && t.Y == tree.Y);
                        if (nextTree.Height < tree.Height)
                        {
                            right += 1;
                        }
                        else
                        {
                            right += 1;
                            break;
                        }
                    }
                    for (int x = tree.X - 1; x >= 0; x--)
                    {
                        Tree nextTree = trees.Single(t => t.X == x && t.Y == tree.Y);
                        if (nextTree.Height < tree.Height)
                        {
                            left += 1;
                        }
                        else
                        {
                            left += 1;
                            break;
                        }
                    }

                    tree.ScenicScore = up * down * right * left;
                }
            }

            Console.Write(trees.Max(t => t.ScenicScore));
        }

        private static List<Tree> ParseInputs(string[] inputs)
        {
            List<Tree> trees = new List<Tree>();

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[0].Length; x++)
                {
                    Tree tree = new Tree
                    {
                        X = x,
                        Y = y,
                        Height = int.Parse(inputs[y][x].ToString())
                    };

                    trees.Add(tree);
                }
            }

            return trees;
        }
    }
}
