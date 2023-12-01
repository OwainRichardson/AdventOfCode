using AdventOfCode._2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022
{
    public class D_15_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2022\Data\day15.txt").ToArray();

            List<Sensor> sensors = ParseInputs(inputs);

            int y = 2000000;
            List<Range> unavailableRanges = new List<Range>();
            int unavailableCoordinates = 0;

            foreach (Sensor sensor in sensors)
            {
                if (sensor.Y > y && sensor.Y - sensor.DistanceToBeacon < y)
                {
                    int distanceFromY = sensor.Y - y;

                    unavailableRanges.Add(new Range { Min = sensor.X - (sensor.DistanceToBeacon - distanceFromY), Max = sensor.X + (sensor.DistanceToBeacon - distanceFromY) });
                }
                else if (sensor.Y < y && sensor.Y + sensor.DistanceToBeacon > y)
                {
                    int distanceFromY = y - sensor.Y;

                    unavailableRanges.Add(new Range { Min = sensor.X - (sensor.DistanceToBeacon - distanceFromY), Max = sensor.X + (sensor.DistanceToBeacon - distanceFromY) });
                }
            }

            for (int index = unavailableRanges.Min(r => r.Min); index <= unavailableRanges.Max(r => r.Max); index++)
            {
                if (unavailableRanges.Any(r => index >= r.Min && index <= r.Max) && !sensors.Any(s => s.BeaconY == y && s.BeaconX == index))
                {
                    unavailableCoordinates += 1;
                }
            }

            Console.WriteLine(unavailableCoordinates);
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