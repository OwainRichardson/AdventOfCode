using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_11_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day11.txt");

            List<Octopus> octopii = ParseOctopii(inputs);

            int maxNumberOfTurns = 100;
            int numberOfFlashes = 0;

            for (int numberOfTurns = 1; numberOfTurns <= maxNumberOfTurns; numberOfTurns++)
            {
                StepOctopii(octopii);

                octopii.ForEach(x =>
                {
                    if (x.Flashed)
                    {
                        x.Flashed = false;
                        numberOfFlashes += 1;
                    }
                });
            }

            Console.WriteLine(numberOfFlashes);
        }

        private static void StepOctopii(List<Octopus> octopii)
        {
            octopii.ForEach(octopus => octopus.Power += 1);

            bool anyFlashes = CheckPowerFlashes(octopii);

            while (anyFlashes)
            {
                FlashOctopii(octopii);
                anyFlashes = CheckPowerFlashes(octopii);
            }
        }

        private static void FlashOctopii(List<Octopus> octopii)
        {
            List<Octopus> octopiiReadyToFlash = octopii.Where(octopus => octopus.Power > 9).ToList();
            foreach (Octopus octopus in octopiiReadyToFlash)
            {
                octopus.Flashed = true;
                octopus.Power = 0;
                PowerUpNearbyOctopii(octopus, octopii);
            }
        }

        private static void PowerUpNearbyOctopii(Octopus octopus, List<Octopus> octopii)
        {
            List<Octopus> nearbyOctopii = octopii.Where(o => o.X >= octopus.X - 1 && o.X <= octopus.X + 1
                                                                && o.Y >= octopus.Y - 1 && o.Y <= octopus.Y + 1
                                                                && !o.Flashed
                                                                && $"{o.X},{o.Y}" != $"{octopus.X},{octopus.Y}")
                                                  .ToList();

            nearbyOctopii.ForEach(nearby => nearby.Power += 1);
        }

        private static bool CheckPowerFlashes(List<Octopus> octopii)
        {
            return octopii.Any(octopus => octopus.Power > 9);
        }

        private static List<Octopus> ParseOctopii(string[] inputs)
        {
            List<Octopus> octopii = new List<Octopus>();

            for (int y = 0; y < inputs.Length; y++)
            {
                for (int x = 0; x < inputs[y].Length; x++)
                {
                    Octopus octopus = new Octopus
                    {
                        X = x,
                        Y = y,
                        Flashed = false,
                        Power = int.Parse(inputs[y][x].ToString())
                    };

                    octopii.Add(octopus);
                }
            }

            return octopii;
        }
    }
}
