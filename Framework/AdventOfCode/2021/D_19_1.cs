using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_19_1
    {
        private static List<int> _matchingIds = new List<int>();
        private static List<long> _lengths = new List<long>();

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day19.txt");

            List<Scanner> scanners = ParseInputs(inputs);

            CalculateDistancesBetweenBeacons(scanners);

            CheckForEquals(scanners);

            _matchingIds = _matchingIds.OrderBy(x => x).ToList();

            Console.WriteLine(scanners.SelectMany(x => x.Beacons).Count() - _matchingIds.Distinct().Count());
        }

        private static void CheckForEquals(List<Scanner> scanners)
        {
            int scannerIndex = 0;
            foreach (Scanner scanner in scanners)
            {
                for (int scan = scannerIndex; scan < scanners.Count; scan++)
                {
                    if (scan == scannerIndex) continue;

                    int beaconCount = 0;
                    foreach (var beacon in scanner.Beacons)
                    {
                        foreach (var beaconDistance in beacon.AbsoluteDistanceToOtherBeacons)
                        {
                            if (scanners[scan].Beacons.Any(x => x.AbsoluteDistanceToOtherBeacons.Contains(beaconDistance)))
                            {
                                var matchingPoint = scanners[scan].Beacons.SingleOrDefault(x => x.AbsoluteDistanceToOtherBeacons.Contains(beaconDistance));

                                if (!_lengths.Contains(beaconDistance))
                                {
                                    _matchingIds.TryAdd(matchingPoint.Id);
                                    _matchingIds.TryAdd(beacon.Id);
                                    _lengths.Add(beaconDistance);
                                }

                                beaconCount++;

                                break;
                            }
                        }
                    }

                    Console.WriteLine($"{scannerIndex} - {scan}: {beaconCount}");
                }

                scannerIndex++;
            }
        }

        private static void CalculateDistancesBetweenBeacons(List<Scanner> scanners)
        {
            foreach (Scanner scanner in scanners)
            {
                int beaconIndex = 0;
                foreach (var beacon in scanner.Beacons)
                {
                    for (int index = beaconIndex; index < scanner.Beacons.Count; index++)
                    {
                        if (index == beaconIndex) continue;

                        var distanceBeacon = scanner.Beacons[index];

                        long firstPoint = (long)Math.Pow(Math.Abs(beacon.Coord1) - Math.Abs(distanceBeacon.Coord1), 2) + (long)Math.Pow(Math.Abs(beacon.Coord2) - Math.Abs(distanceBeacon.Coord2), 2) + (long)Math.Pow(Math.Abs(beacon.Coord3) - Math.Abs(distanceBeacon.Coord3), 2);

                        beacon.AbsoluteDistanceToOtherBeacons.Add(Math.Abs(firstPoint));
                    }

                    beaconIndex++;
                }
            }
        }

        private static List<Scanner> ParseInputs(string[] inputs)
        {
            int scannerId = -1;
            string pattern = @"^---\sscanner\s(\d+)\s---$";
            Regex regex = new Regex(pattern);
            List<Scanner> scanners = new List<Scanner>();
            int beaconId = 0;

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input)) continue;

                if (regex.IsMatch(input))
                {
                    Match match = regex.Match(input);
                    scannerId = int.Parse(match.Groups[1].Value);

                    Scanner newScanner = new Scanner { Id = scannerId };
                    scanners.Add(newScanner);

                    continue;
                }

                int[] split = input.Split(',').Select(x => int.Parse(x)).ToArray();

                Scanner scanner = scanners.Single(x => x.Id == scannerId);
                scanner.Beacons.Add(new BeaconCoords { Id = beaconId, Coord1 = split[0], Coord2 = split[1], Coord3 = split[2] });
                beaconId++;
            }

            return scanners;
        }
    }
}