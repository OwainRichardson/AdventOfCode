using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class D_06_2
    {
        public static void Execute()
        {
            var orbitMaps = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day06_full.txt");

            var orbits = ParseDirectOrbitMaps(orbitMaps);
            orbits = ParseIndirectOrbitMaps(orbits);

            var myOrbit = orbits.FirstOrDefault(x => x.StartPoint == "YOU");
            var santasOrbit = orbits.FirstOrDefault(x => x.StartPoint == "SAN");

            List<string> myOrbits = new List<string> { myOrbit.DirectMap };
            myOrbits.AddRange(myOrbit.IndirectMaps);

            List<string> santasOrbits = new List<string> { santasOrbit.DirectMap };
            santasOrbits.AddRange(santasOrbit.IndirectMaps);

            //ToDo: Find common orbits
            var commonOrbits = myOrbits.Intersect(santasOrbits);

            //ToDo: Find first common orbit
            var closestOrbit = commonOrbits.First();

            //ToDo: Work out path length to common orbit
            var myDistanceToClosestOrbit = myOrbits.IndexOf(closestOrbit);
            var santasDistanceToClosestOrbit = santasOrbits.IndexOf(closestOrbit);

            //Console.WriteLine($"My distance: {myDistanceToClosestOrbit}");
            //Console.WriteLine($"Santas distance: {santasDistanceToClosestOrbit}");
            Console.WriteLine($"Total distance: {myDistanceToClosestOrbit + santasDistanceToClosestOrbit}");
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