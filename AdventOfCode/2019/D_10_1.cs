using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class D_10_1
    {
        public static void Execute()
        {
            int width = 26;
            List<string> rows = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day10_full.txt").ToList();
            List<MapCoord> mapCoords = new List<MapCoord>();

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

            int canSeeMost = 0;
            var asteroids = mapCoords.Where(x => x.IsAsteroid).ToList();
            foreach (var asteroid in asteroids)
            {
                int count = CalculateHowManyAsteroidsCanBeSeen(asteroid, asteroids);

                if (count > canSeeMost)
                {
                    canSeeMost = count;
                }
            }

            Console.WriteLine(canSeeMost);
        }

        private static int CalculateHowManyAsteroidsCanBeSeen(MapCoord asteroid, List<MapCoord> asteroids)
        {
            List<MapCoord> asteroidDistances = new List<MapCoord>();
            List<MapCoord> invalidAsteroids = new List<MapCoord>();

            foreach (var a in asteroids)
            {
                if (a.X == asteroid.X && a.Y == asteroid.Y)
                {
                    continue;
                }

                asteroidDistances.Add(new MapCoord
                {
                    X = a.X - asteroid.X,
                    Y = a.Y - asteroid.Y
                });
            }

            foreach (var distance in asteroidDistances)
            {
                if (distance.Ratio == 0)
                {
                    if (distance.X == 0)
                    {
                        // Vertical 
                        if (asteroidDistances.Count(x => x.Ratio == 0 && x.X == 0) > 1)
                        {
                            var asteroidsBelow = asteroidDistances.Where(x => x.Ratio == 0 && x.X == 0 && x.Y > 0);
                            foreach (var ab in asteroidsBelow.Skip(1))
                            {
                                if (!invalidAsteroids.Any(x => x.X == ab.X && x.Y == ab.Y))
                                {
                                    invalidAsteroids.Add(ab);
                                }
                            }

                            var asteroidsAbove = asteroidDistances.Where(x => x.Ratio == 0 && x.X == 0 && x.Y < 0);
                            foreach (var aa in asteroidsAbove.Skip(1))
                            {
                                if (!invalidAsteroids.Any(x => x.X == aa.X && x.Y == aa.Y))
                                {
                                    invalidAsteroids.Add(aa);
                                }
                            }
                        }
                    }
                    if (distance.Y == 0)
                    {
                        // Horizontal
                        if (asteroidDistances.Count(x => x.Ratio == 0 && x.Y == 0) > 1)
                        {
                            var asteroidsBelow = asteroidDistances.Where(x => x.Ratio == 0 && x.Y == 0 && x.X > 0);
                            foreach (var ab in asteroidsBelow.OrderBy(x => x.X).Skip(1))
                            {
                                if (!invalidAsteroids.Any(x => x.X == ab.X && x.Y == ab.Y))
                                {
                                    invalidAsteroids.Add(ab);
                                }
                            }

                            var asteroidsAbove = asteroidDistances.Where(x => x.Ratio == 0 && x.Y == 0 && x.X < 0);
                            foreach (var aa in asteroidsAbove.OrderByDescending(x => x.X).Skip(1))
                            {
                                if (!invalidAsteroids.Any(x => x.X == aa.X && x.Y == aa.Y))
                                {
                                    invalidAsteroids.Add(aa);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (asteroidDistances.Count(x => x.Ratio == distance.Ratio) > 1)
                    {
                        if (distance.Ratio > 0)
                        {
                            if (asteroidDistances.Count(x => x.Ratio == distance.Ratio && x.X > 0 && x.Y > 0) > 1)
                            {
                                foreach (var ast in asteroidDistances.Where(x => x.Ratio == distance.Ratio && x.X > 0 && x.Y > 0).OrderBy(x => x.ManhattanDistance).Skip(1))
                                {
                                    if (!invalidAsteroids.Any(x => x.X == ast.X && x.Y == ast.Y))
                                    {
                                        invalidAsteroids.Add(ast);
                                    }
                                }
                            }
                            if (asteroidDistances.Count(x => x.Ratio == distance.Ratio && x.X < 0 && x.Y < 0) > 1)
                            {
                                foreach (var ast in asteroidDistances.Where(x => x.Ratio == distance.Ratio && x.X < 0 && x.Y < 0).OrderBy(x => x.ManhattanDistance).Skip(1))
                                {
                                    if (!invalidAsteroids.Any(x => x.X == ast.X && x.Y == ast.Y))
                                    {
                                        invalidAsteroids.Add(ast);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (asteroidDistances.Count(x => x.Ratio == distance.Ratio && x.X > 0 && x.Y < 0) > 1)
                            {
                                foreach (var ast in asteroidDistances.Where(x => x.Ratio == distance.Ratio && x.X > 0 && x.Y < 0).OrderBy(x => x.ManhattanDistance).Skip(1))
                                {
                                    if (!invalidAsteroids.Any(x => x.X == ast.X && x.Y == ast.Y))
                                    {
                                        invalidAsteroids.Add(ast);
                                    }
                                }
                            }
                            if (asteroidDistances.Count(x => x.Ratio == distance.Ratio && x.X < 0 && x.Y > 0) > 1)
                            {
                                foreach (var ast in asteroidDistances.Where(x => x.Ratio == distance.Ratio && x.X < 0 && x.Y > 0).OrderBy(x => x.ManhattanDistance).Skip(1))
                                {
                                    if (!invalidAsteroids.Any(x => x.X == ast.X && x.Y == ast.Y))
                                    {
                                        invalidAsteroids.Add(ast);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return asteroidDistances.Count() - invalidAsteroids.Count();
        }
    }
}