using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class D_06_1
    {
        public static void Execute()
        {
            var orbitMaps = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day06_full.txt");

            var orbits = ParseDirectOrbitMaps(orbitMaps);
            orbits = ParseIndirectOrbitMaps(orbits);

            var totalOrbits = 0;

            foreach (var orbit in orbits)
            {
                totalOrbits += orbit.IndirectMaps.Count();
                totalOrbits += 1; // Direct Orbit
            }

            Console.WriteLine($"Total Orbits: {totalOrbits}");
        }

        private static List<Orbit> ParseIndirectOrbitMaps(List<Orbit> orbits)
        {
            foreach (var map in orbits)
            {
                List<string> indirectMaps = new List<string>();

                var currentNode = map.DirectMap;
                while (!currentNode.Equals("COM"))
                {
                    var indirectOrbit = orbits.FirstOrDefault(x => x.StartPoint == currentNode);
                    if (indirectOrbit != null)
                    {
                        indirectMaps.Add(indirectOrbit.DirectMap);
                        currentNode = indirectOrbit.DirectMap;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                map.IndirectMaps.AddRange(indirectMaps);
            }

            return orbits;
        }

        public static List<Orbit> ParseDirectOrbitMaps(string[] orbitMaps)
        {
            List<Orbit> orbits = new List<Orbit>();

            foreach (var map in orbitMaps)
            {
                var directMap = map.Substring(0, map.IndexOf(')'));
                var startPoint = map.Substring(map.IndexOf(')') + 1);

                if (!orbits.Any(x => x.StartPoint == startPoint))
                {
                    orbits.Add(new Orbit
                    {
                        StartPoint = startPoint,
                        DirectMap = directMap
                    });
                }
            }

            return orbits;
        }
    }
}