using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_08_1
    {
        public static void Execute()
        { 
            string[] inputs = File.ReadAllLines(@"2022\Data\day08.txt").ToArray();

            List<Tree> trees = ParseInputs(inputs);

            CalculateVisibleTrees(trees);
        }

        private static void CalculateVisibleTrees(List<Tree> trees)
        {
            int maxY = trees.Max(t => t.Y);
            int maxX = trees.Max(y => y.X);

            int visibleTrees = 0;

            foreach (Tree tree in trees)
            {
                if (tree.Y == 0)
                {
                    visibleTrees += 1;
                    continue;
                }
                else if (tree.X == 0)
                {
                    visibleTrees += 1;
                    continue;
                }
                else if (tree.Y == maxY)
                {
                    visibleTrees += 1;
                    continue;
                }
                else if (tree.X == maxX)
                {
                    visibleTrees += 1;
                    continue;
                }
                else
                {
                    if (trees.Where(t => t.X < tree.X && t.Y == tree.Y).All(t => t.Height < tree.Height))
                    {
                        visibleTrees += 1;
                        continue;
                    }
                    else if (trees.Where(t => t.X > tree.X && t.Y == tree.Y).All(t => t.Height < tree.Height))
                    {
                        visibleTrees += 1;
                        continue;
                    }
                    else if (trees.Where(t => t.Y < tree.Y && t.X == tree.X).All(t => t.Height < tree.Height))
                    {
                        visibleTrees += 1;
                        continue;
                    }
                    else if (trees.Where(t => t.Y > tree.Y && t.X == tree.X).All(t => t.Height < tree.Height))
                    {
                        visibleTrees += 1;
                        continue;
                    }
                }
            }

            Console.WriteLine(visibleTrees);
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
