using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_16_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day16.txt").ToArray();

            List<Valve> valves = ParseValves(inputs);

            Valve currentValve = valves.Single(v => v.Name == "AA");

            int minutes = 30;
            while (minutes > 0)
            {
                if (currentValve.FlowRate == 0)
                {
                    // ToDo: move
                }
                else
                {
                    // ToDo: open
                }

                minutes -= 1;
            }
        }

        private static List<Valve> ParseValves(string[] inputs)
        {
            List<Valve> valves = new List<Valve>();

            string pattern = @"^Valve (\w+) has flow rate=(\d+); tunnels? leads? to valves? (.+)$";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                Valve valve = new Valve();
                Match match = regex.Match(input);

                valve.Name = match.Groups[1].Value;
                valve.FlowRate = int.Parse(match.Groups[2].Value);

                string tunnels = match.Groups[3].Value;
                valve.Tunnels = tunnels.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

                valves.Add(valve);
            }

            return valves;
        }
    }
}