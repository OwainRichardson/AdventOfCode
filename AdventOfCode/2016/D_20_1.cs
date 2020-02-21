﻿using AdventOfCode._2016.Models;
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
    public static class D_20_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day20_full.txt");
            List<BlockedIp> blockedIps = new List<BlockedIp>();

            ParseInputsToBlockedIps(inputs, ref blockedIps);

            blockedIps = blockedIps.OrderBy(x => x.End).ToList();

            for (int i = 1; i < blockedIps.Count; i++)
            {
                if (blockedIps[i - 1].End + 1 < blockedIps[i].Start)
                {
                    long possible = blockedIps[i - 1].End + 1;

                    if (!blockedIps.Any(x => x.Start <= possible && x.End >= possible))
                    {
                        Console.WriteLine(possible);
                        break;
                    }
                }
            }
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
