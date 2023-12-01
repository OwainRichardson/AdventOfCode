using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Directory = AdventOfCode._2022.Models.Directory;

namespace AdventOfCode._2022
{
    public class D_07_1
    {
        public static void Execute()
        { 
            string[] inputs = File.ReadAllLines(@"2022\Data\day07.txt").ToArray();

            Directory root = new Directory { Name = "/", Id = 0, ParentId = -1 };
            List<Directory> directories = new List<Directory>() { root };
            Directory currentDirectory = root;

            foreach (string input in inputs)
            {
                if (input == "$ cd /") continue;

                if (input.StartsWith("dir"))
                {
                    string dirPattern = @"^dir\W(\w+)$";
                    Regex dirRegex = new Regex(dirPattern);
                    Match match = dirRegex.Match(input);

                    directories.Add(new Directory { Name = match.Groups[1].Value, ParentId = currentDirectory.Id, Id = directories.Max(d => d.Id) + 1 });
                }
                else if (input.StartsWith("$ cd .."))
                {
                    int parentDirectoryId = currentDirectory.ParentId;
                    currentDirectory = directories.Single(d => d.Id == parentDirectoryId);
                }
                else if (input.StartsWith("$ cd"))
                {
                    string pattern = @"^\$\Wcd\W(\w+)$";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);

                    Directory newDirectory = directories.Single(d => d.Name == match.Groups[1].Value && d.ParentId == currentDirectory.Id);
                    currentDirectory = newDirectory;
                }
                else if (input == "$ ls")
                {
                    continue;
                }
                else
                {
                    string filePattern = @"^(\d+)\W(.+)$";
                    Regex fileRegex = new Regex(filePattern);
                    Match match = fileRegex.Match(input);

                    currentDirectory.Files.Add(new DirectoryFile { Filename = match.Groups[2].Value, Size = long.Parse(match.Groups[1].Value) });
                    currentDirectory.Size += long.Parse(match.Groups[1].Value);

                    int nextParent = currentDirectory.ParentId;
                    while (nextParent >= 0)
                    {
                        Directory parent = directories.Single(d => d.Id == nextParent);
                        parent.Size += long.Parse(match.Groups[1].Value);

                        nextParent = parent.ParentId;
                    }
                }
            }

            Console.WriteLine(directories.Where(d => d.Size <= 100000).Sum(d => d.Size));
        }
    }
}
