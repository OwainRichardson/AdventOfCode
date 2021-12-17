using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class D_10_2
    {
        public static void Execute()
        {
            int width = 26;
            List<string> rows = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day10_full.txt").ToList();
            List<MapCoord> mapCoords = new List<MapCoord>();
            MapCoord gun = new MapCoord { IsAsteroid = true, X = 19, Y = 14 };

            int y = 0;
            foreach (var row in rows)
            {
                for (int x = 0; x < width; x++)
                {
                    mapCoords.Add(new MapCoord
                    {
                        Y = y,
                        X = x,
                        IsAsteroid = row[x].ToString() == "#"
                    });
                }

                y++;
            }

            var asteroids = mapCoords.Where(x => x.IsAsteroid).ToList();

            FindAndDestroyAsteroids(gun, asteroids);
        }

        private static void FindAndDestroyAsteroids(MapCoord asteroid, List<MapCoord> asteroids)
        {
            List<MapCoord> asteroidDistances = new List<MapCoord>();

            foreach (var a in asteroids)
            {
                if (a.X == asteroid.X && a.Y == asteroid.Y)
                {
                    continue;
                }

                asteroidDistances.Add(new MapCoord
                {
                    X = a.X - asteroid.X,
                    Y = a.Y - asteroid.Y,
                    OriginalX = a.X,
                    OriginalY = a.Y
                });
            }

            List<double> degrees = asteroidDistances.Select(x => x.Degrees).Distinct().OrderBy(x => x).ToList();

            int index = 1;
            while (!asteroidDistances.All(x => x.Destroyed))
            {
                foreach (var degree in degrees)
                {
                    var asteroidsWithDegree = asteroidDistances.Where(x => x.Degrees == degree && !x.Destroyed);

                    if (asteroidsWithDegree != null && asteroidsWithDegree.Any())
                    {
                        var closestAsteroidDistance = asteroidsWithDegree.Min(x => x.ManhattanDistance);
                        var asteroidToDestroy = asteroidsWithDegree.First(x => x.ManhattanDistance == closestAsteroidDistance);
                        asteroidToDestroy.Destroyed = true;
                        if (index == 200)
                        {
                            Console.WriteLine($"#{index} - Destroyed asteroid at ({asteroidToDestroy.OriginalX}, {asteroidToDestroy.OriginalY}) - value: {(asteroidToDestroy.OriginalX * 100) + asteroidToDestroy.OriginalY}");
                        }
                        index++;
                    }
                }
            }
        }
    }
}