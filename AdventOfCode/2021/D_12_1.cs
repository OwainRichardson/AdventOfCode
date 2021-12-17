using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_12_1
    {
        private static List<string> paths = new List<string>();

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day12.txt");

            List<Cave> caves = ParseCaves(inputs);

            FindPaths(caves);
        }

        private static void FindPaths(List<Cave> caves)
        {
            Cave startCave = caves.Single(x => x.Name == "start");

            foreach (string nextCave in startCave.OtherCaves)
            {
                FollowPath(startCave.Name, nextCave, caves);
            }

            Console.WriteLine(paths.Count);
        }

        private static void FollowPath(string path, string nextCave, List<Cave> caves)
        {
            if (nextCave == "start")
            {
                return;
            }

            if (nextCave == "end")
            {
                paths.Add(path);
                return;
            }

            Cave currentCave = caves.Single(x => x.Name == nextCave);

            if (!currentCave.BigCave && path.Contains(nextCave))
            {
                return;
            }

            foreach (string c in currentCave.OtherCaves)
            {
                FollowPath($"{path}-{nextCave}", c, caves);
            }
        }

        private static List<Cave> ParseCaves(string[] inputs)
        {
            List<Cave> caves = new List<Cave>();

            foreach (string input in inputs)
            {
                string[] split = input.Split('-');
                for (int index = 0; index <= 1; index++)
                {
                    string cave = split[index];

                    if (!caves.Any(x => x.Name == cave))
                    {
                        bool bigCave = false;
                        if (cave == "start" || cave == "end")
                        {
                            bigCave = false;
                        }
                        else
                        {
                            bigCave = cave.All(x => char.IsUpper(x));
                        }

                        Cave newCave = new Cave { Name = cave, BigCave = bigCave };
                        newCave.OtherCaves.Add(split[(index + 1) % 2]);

                        caves.Add(newCave);
                    }
                    else
                    {
                        Cave c = caves.Single(x => x.Name == cave);
                        c.OtherCaves.Add(split[(index + 1) % 2]);
                    }
                }
            }

            return caves;
        }
    }
}
