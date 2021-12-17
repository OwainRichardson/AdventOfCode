using AdventOfCode._2016.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    public static class D_20_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day20_full.txt");
            List<BlockedIp> blockedIps = new List<BlockedIp>();

            ParseInputsToBlockedIps(inputs, ref blockedIps);

            blockedIps = blockedIps.OrderBy(x => x.End).ToList();

            int count = 0;
            for (int i = 1; i < blockedIps.Count; i++)
            {
                if (blockedIps[i - 1].End + 1 < blockedIps[i].Start)
                {
                    long possible = blockedIps[i - 1].End + 1;

                    if (!blockedIps.Any(x => x.Start <= possible && x.End >= possible))
                    {
                        while (!blockedIps.Any(x => x.Start <= possible && x.End >= possible))
                        {
                            possible++;
                            count++;

                            Console.Write($"\r{count}");
                        }
                    }
                }
            }

            Console.WriteLine();
        }

        private static void ParseInputsToBlockedIps(string[] inputs, ref List<BlockedIp> blockedIps)
        {
            string pattern = @"(\d+)-(\d+)";
            Regex regex = new Regex(pattern);

            foreach (var input in inputs)
            {
                BlockedIp blockedIp = new BlockedIp();

                Match match = regex.Match(input);
                blockedIp.Start = long.Parse(match.Groups[1].Value);
                blockedIp.End = long.Parse(match.Groups[2].Value);

                blockedIps.Add(blockedIp);
            }
        }
    }
}
