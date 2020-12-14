using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_13_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day13.txt");

            int earliestTimestemp = int.Parse(inputs[0]);
            List<string> busServicesBeforeParse = inputs[1].Split(',').ToList();

            _ = busServicesBeforeParse.RemoveAll(x => x == "x");

            List<int> busServices = busServicesBeforeParse.Select(x => int.Parse(x)).ToList();

            Dictionary<int, int> closestTimes = new Dictionary<int, int>();
            foreach(int busService in busServices)
            {
                int times = 0;

                while (times < earliestTimestemp)
                {
                    times += busService;
                }

                closestTimes.Add(busService, times);
            }

            var closest = closestTimes.Values.Min(x => x - earliestTimestemp);
            var closestKey = closestTimes.Where(x => x.Value - earliestTimestemp == closest).Single().Key;

            Console.WriteLine(closest * closestKey);            
        }
    }
}
