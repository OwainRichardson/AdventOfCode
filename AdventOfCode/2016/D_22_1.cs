using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_22_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day22_full.txt");

            List<Node> nodes = ParseInputs(inputs);

            FindValidPairs(nodes);
        }

        private static void FindValidPairs(List<Node> nodes)
        {
            List<string> viablePairs = new List<string>();

            foreach (Node node in nodes)
            {
                foreach (Node otherNode in nodes)
                {
                    if (otherNode.X == node.X && otherNode.Y == node.Y)
                    {
                        continue;
                    }

                    if (node.Used != 0)
                    {
                        if (node.Used <= otherNode.Avail)
                        {
                            if (node.Id < otherNode.Id)
                            {
                                viablePairs.Add($"{node.Id}-{otherNode.Id}");
                            }
                            else
                            {
                                viablePairs.Add($"{otherNode.Id}-{node.Id}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine(viablePairs.Distinct().Count());
        }

        private static List<Node> ParseInputs(string[] inputs)
        {
            string pattern = @"\/dev\/grid\/node-x(\d+)-y(\d+)\s+(\d+)T\s+(\d+)T\s+(\d+)T\s+(\d+)%";
            Regex regex = new Regex(pattern);
            List<Node> nodes = new List<Node>();
            int id = 1;

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);

                if (match.Success)
                {
                    Node node = new Node();

                    node.X = int.Parse(match.Groups[1].Value);
                    node.Y = int.Parse(match.Groups[2].Value);
                    node.Size = int.Parse(match.Groups[3].Value);
                    node.Used = int.Parse(match.Groups[4].Value);
                    node.Avail = int.Parse(match.Groups[5].Value);
                    node.UsedPerc = int.Parse(match.Groups[6].Value);
                    node.Id = id;

                    nodes.Add(node);

                    id++;
                }
            }

            return nodes;
        }
    }
}