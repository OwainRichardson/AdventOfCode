using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2017
{
    public static class D_20_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day20_full.txt");

            List<Particle> particles = ParseParticles(inputs);

            int count = 1;
            bool collisions = false;

            while (count < 1000)
            {
                collisions = false;

                particles = Step(particles);
                particles = CheckColisions(particles, out collisions);

                if (collisions)
                {
                    count = 1;
                }
                else
                {
                    count++;
                }
            }

            Console.WriteLine($"{particles.Count} particles left");
        }

        private static List<Particle> CheckColisions(List<Particle> particles, out bool collisions)
        {
            List<Particle> particlesToKeep = new List<Particle>();
            collisions = false;

            foreach (Particle particle in particles)
            {
                if (!particles.Any(x => x.XPos == particle.XPos && x.YPos == particle.YPos && x.ZPos == particle.ZPos && x.Id != particle.Id))
                {
                    particlesToKeep.Add(particle);
                }
                else
                {
                    collisions = true;
                }
            }

            return particlesToKeep;
        }

        private static List<Particle> Step(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                particle.XVel += particle.XAcc;
                particle.YVel += particle.YAcc;
                particle.ZVel += particle.ZAcc;

                particle.XPos += particle.XVel;
                particle.YPos += particle.YVel;
                particle.ZPos += particle.ZVel;
            }

            return particles;
        }

        private static List<Particle> ParseParticles(string[] inputs)
        {
            List<Particle> particles = new List<Particle>();
            string positionPattern = @"p=<(-?\d+),(-?\d+),(-?\d+)>";
            string velocityPattern = @"v=<(-?\d+),(-?\d+),(-?\d+)>";
            string accelerationPattern = @"a=<(-?\d+),(-?\d+),(-?\d+)>";
            int index = 0;

            foreach (string input in inputs)
            {
                Particle particle = new Particle
                {
                    Id = index
                };

                Match positionMatch = Regex.Match(input, positionPattern);
                particle.XPos = int.Parse(positionMatch.Groups[1].Value);
                particle.YPos = int.Parse(positionMatch.Groups[2].Value);
                particle.ZPos = int.Parse(positionMatch.Groups[3].Value);

                Match velocityMatch = Regex.Match(input, velocityPattern);
                particle.XVel = int.Parse(velocityMatch.Groups[1].Value);
                particle.YVel = int.Parse(velocityMatch.Groups[2].Value);
                particle.ZVel = int.Parse(velocityMatch.Groups[3].Value);

                Match accelerationMatch = Regex.Match(input, accelerationPattern);
                particle.XAcc = int.Parse(accelerationMatch.Groups[1].Value);
                particle.YAcc = int.Parse(accelerationMatch.Groups[2].Value);
                particle.ZAcc = int.Parse(accelerationMatch.Groups[3].Value);

                particles.Add(particle);
                index++;
            }

            return particles;
        }
    }
}
