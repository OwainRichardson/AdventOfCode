using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class D_12_1
    {
        public static void Execute()
        {
            var input = @"<x=6, y=10, z=10>
<x=-9, y=3, z=17>
<x=9, y=-4, z=14>
<x=4, y=14, z=4>";

            List<string> moonsData = input.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            List<Moon> moons = new List<Moon>();
            moons = PopulateMoons(moonsData);

            int numberOfSteps = 1000;

            for (int i = 1; i <= numberOfSteps; i++)
            {
                moons = Step(moons);
            }

            DisplayMoons(moons);
        }

        private static void DisplayMoons(List<Moon> moons)
        {
            int totalEnergy = 0;
            foreach (var moon in moons)
            {
                var potentialEnergy = Math.Abs(moon.Position.X) + Math.Abs(moon.Position.Y) + Math.Abs(moon.Position.Z);

                var kineticEnergy = Math.Abs(moon.Velocity.X) + Math.Abs(moon.Velocity.Y) + Math.Abs(moon.Velocity.Z);

                totalEnergy += (potentialEnergy * kineticEnergy);
            }

            Console.WriteLine($"Total energy: {totalEnergy}");
        }

        private static List<Moon> Step(List<Moon> moons)
        {
            moons = CalculateMoonVelocity(moons);
            moons = UpdateMoonsBasedOnVelocity(moons);

            return moons;
        }

        private static List<Moon> UpdateMoonsBasedOnVelocity(List<Moon> moons)
        {
            foreach (var moon in moons)
            {
                moon.Position.X += moon.Velocity.X;
                moon.Position.Y += moon.Velocity.Y;
                moon.Position.Z += moon.Velocity.Z;
            }

            return moons;
        }

        private static List<Moon> CalculateMoonVelocity(List<Moon> moons)
        {
            foreach (var moon in moons)
            {
                foreach (var otherMoon in moons.Where(x => x.Id != moon.Id))
                {
                    if (moon.Position.X > otherMoon.Position.X)
                    {
                        moon.Velocity.X -= 1;
                    }
                    else if (moon.Position.X < otherMoon.Position.X)
                    {
                        moon.Velocity.X += 1;
                    }
                    if (moon.Position.Y > otherMoon.Position.Y)
                    {
                        moon.Velocity.Y -= 1;
                    }
                    else if (moon.Position.Y < otherMoon.Position.Y)
                    {
                        moon.Velocity.Y += 1;
                    }
                    if (moon.Position.Z > otherMoon.Position.Z)
                    {
                        moon.Velocity.Z -= 1;
                    }
                    else if (moon.Position.Z < otherMoon.Position.Z)
                    {
                        moon.Velocity.Z += 1;
                    }
                }
            }

            return moons;
        }

        private static List<Moon> PopulateMoons(List<string> moonsData)
        {
            List<Moon> moons = new List<Moon>();
            int index = 1;

            foreach (var moon in moonsData)
            {
                int indexOfComma = moon.IndexOf(',');
                var x = moon.Substring(moon.IndexOf("x=") + 2, indexOfComma - (moon.IndexOf("x=") + 2));

                indexOfComma = indexOfComma + 1;
                var y = moon.Substring(moon.IndexOf("y=") + 2, moon.IndexOf(",", indexOfComma + 1) - (moon.IndexOf("y=") + 2));

                var z = moon.Substring(moon.IndexOf("z=") + 2, moon.IndexOf(">") - (moon.IndexOf("z=") + 2));

                moons.Add(new Moon
                {
                    Id = index,
                    Position = new Vector
                    {
                        X = int.Parse(x),
                        Y = int.Parse(y),
                        Z = int.Parse(z)
                    }
                });

                index++;
            }

            return moons;
        }
    }
}