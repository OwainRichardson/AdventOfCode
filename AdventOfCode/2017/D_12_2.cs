using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017
{
    public static class D_12_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day12_full.txt");

            List<Pipe> pipes = ParseInputs(inputs);
            List<int> pipeIds = new List<int> { 0 };

            int groups = 0;
            FindPathsToPipe(0, pipes, pipeIds);
            groups++;

            while (pipes.Select(x => x.Id).Count() > pipeIds.Count)
            {
                var firstPipeNotIncluded = pipes.Select(x => x.Id).First(x => !pipeIds.Contains(x));

                FindPathsToPipe(firstPipeNotIncluded, pipes, pipeIds);
                groups++;
            }

            Console.WriteLine(groups);
        }

        private static void FindPathsToPipe(int id, List<Pipe> pipes, List<int> pipeIds)
        {
            var selectedPipe = pipes.First(x => x.Id == id);

            foreach (var dependent in selectedPipe.DirectPipes)
            {
                if (!pipeIds.Contains(dependent))
                {
                    pipeIds.Add(dependent);

                    FindPathsToPipe(dependent, pipes, pipeIds);
                }
            }
        }

        private static List<Pipe> ParseInputs(string[] inputs)
        {
            string pattern = @"(\d+) <-> (.+)";
            Regex regex = new Regex(pattern);
            List<Pipe> pipes = new List<Pipe>();

            foreach (var input in inputs)
            {
                Pipe pipe = new Pipe();
                Match match = regex.Match(input);

                pipe.Id = int.Parse(match.Groups[1].Value);

                var directPipeMatches = match.Groups[2].Value;
                var directPipes = directPipeMatches.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var direct in directPipes)
                {
                    pipe.DirectPipes.Add(int.Parse(direct));
                }

                pipes.Add(pipe);
            }

            return pipes;
        }
    }
}
