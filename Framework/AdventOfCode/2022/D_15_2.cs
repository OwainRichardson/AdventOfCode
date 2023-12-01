using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_15_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day15.txt").ToArray();

            List<Sensor> sensors = ParseInputs(inputs);

            int minCoord = 0;
            int maxCoord = 4000000;

            List<Range> unavailableRanges = new List<Range>();

            foreach (Sensor sensor in sensors)
            {
                for (int y = sensor.Y - sensor.DistanceToBeacon; y <= sensor.Y + sensor.DistanceToBeacon; y++)
                {
                    int distanceFromY = Math.Abs(sensor.Y - y);

                    unavailableRanges.Add(new Range { Y = y, Min = sensor.X - (sensor.DistanceToBeacon - distanceFromY), Max = sensor.X + (sensor.DistanceToBeacon - distanceFromY) });
                }
            }

            for (int y = minCoord; y <= maxCoord; y++)
            {
                for (int x = minCoord; x <= maxCoord; x++)
                {
                    if (!unavailableRanges.Any(r => r.Y == y && x >= r.Min && x <= r.Max))
                    {
                        Console.WriteLine((x * 4000000) + y);
                        break;
                    }
                }

                unavailableRanges = unavailableRanges.Where(r => r.Y != y).ToList();
            }
        }

        private static List<Sensor> ParseInputs(string[] inputs)
        {
            string pattern = @"^Sensor at x=(-?\d+), y=(-?\d+): closest beacon is at x=(-?\d+), y=(-?\d+)$";
            Regex regex = new Regex(pattern);
            List<Sensor> sensors = new List<Sensor>();

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);

                Sensor sensor = new Sensor
                {
                    X = int.Parse(match.Groups[1].Value),
                    Y = int.Parse(match.Groups[2].Value),
                    BeaconX = int.Parse(match.Groups[3].Value),
                    BeaconY = int.Parse(match.Groups[4].Value)
                };

                sensor.DistanceToBeacon = Math.Abs(sensor.X - sensor.BeaconX) + Math.Abs(sensor.Y - sensor.BeaconY);

                sensors.Add(sensor);
            }

            return sensors;
        }
    }
}