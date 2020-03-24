using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2019
{
    public static class D_12_2
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

            List<Moon> originalPosition = PopulateMoons(moonsData);

            int numberOfSteps = 1;

            while (moons.Any(x => x.XCyclePattern.Count <= 10) || moons.Any(x => x.YCyclePattern.Count <= 10) || moons.Any(x => x.ZCyclePattern.Count <= 10))
            {
                moons = Step(moons);

                CheckMoonCycles(moons, numberOfSteps, originalPosition);

                numberOfSteps++;
            }

            long xCycleLength = 0;
            long yCycleLength = 0;
            long zCycleLength = 0;

            if (moons.Any(x => x.XCyclePattern.All(y => y == x.XCyclePattern[0])))
            {
                var consistentMoon = moons.First(x => x.XCyclePattern.All(y => y == x.XCyclePattern[0]));

                xCycleLength = consistentMoon.XCyclePattern[0];
            }
            else
            {
                xCycleLength = CalculateCycleLength(moons[1].XCyclePattern);
            }

            if (moons.Any(x => x.YCyclePattern.All(y => y == x.YCyclePattern[0])))
            {
                var consistentMoon = moons.First(x => x.YCyclePattern.All(y => y == x.YCyclePattern[0]));

                yCycleLength = consistentMoon.YCyclePattern[0];
            }
            else
            {
                yCycleLength = CalculateCycleLength(moons[1].YCyclePattern);
            }

            if (moons.Any(x => x.ZCyclePattern.All(y => y == x.ZCyclePattern[0])))
            {
                var consistentMoon = moons.First(x => x.ZCyclePattern.All(y => y == x.ZCyclePattern[0]));

                zCycleLength = consistentMoon.ZCyclePattern[0];
            }
            else
            {
                zCycleLength = CalculateCycleLength(moons[1].ZCyclePattern);
            }

            //Console.WriteLine(xCycleLength);
            //Console.WriteLine(yCycleLength);
            //Console.WriteLine(zCycleLength);

            Console.WriteLine($"Cycles until back to the start: {CalculateLcmForArray(new long[] { xCycleLength, yCycleLength, zCycleLength })}");
        }

        private static long CalculateCycleLength(List<long> cyclePattern)
        {
            for (int i = 1; i < cyclePattern.Count; i++)
            {
                if (cyclePattern.Take(i).Contains(cyclePattern[i]))
                {
                    // Found a cycle
                    var centreOfCycle = i - 1;
                    var finalCycleIndex = ((i - 1) * 2) + 1;
                    var total = cyclePattern.Take(finalCycleIndex).Sum();

                    return total;
                }
            }

            return 0;
        }

        public static long CalculateLcmForArray(long[] element_array)
        {
            long lcm_of_array_elements = 1;
            int divisor = 2;

            while (true)
            {

                int counter = 0;
                bool divisible = false;
                for (int i = 0; i < element_array.Length; i++)
                {
                    // lcm_of_array_elements (n1, n2, ... 0) = 0. 
                    // For negative number we convert into 
                    // positive and calculate lcm_of_array_elements. 
                    if (element_array[i] == 0)
                    {
                        return 0;
                    }
                    else if (element_array[i] < 0)
                    {
                        element_array[i] = element_array[i] * (-1);
                    }
                    if (element_array[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by devisor if complete 
                    // division i.e. without remainder then replace 
                    // number with quotient; used for find next factor 
                    if (element_array[i] % divisor == 0)
                    {
                        divisible = true;
                        element_array[i] = element_array[i] / divisor;
                    }
                }

                // If divisor able to completely divide any number 
                // from array multiply with lcm_of_array_elements 
                // and store into lcm_of_array_elements and continue 
                // to same divisor for next factor finding. 
                // else increment divisor 
                if (divisible)
                {
                    lcm_of_array_elements = lcm_of_array_elements * divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate  
                // we found all factors and terminate while loop. 
                if (counter == element_array.Length)
                {
                    return lcm_of_array_elements;
                }
            }
        }

        private static void CheckMoonCycles(List<Moon> moons, int numberOfSteps, List<Moon> originalPosition)
        {
            long pattern = 0;

            foreach (var moon in moons.Where(x => x.XCycleLength == 0))
            {
                if (moon.Position.X == originalPosition.First(x => x.Id == moon.Id).Position.X
                        && moon.Velocity.X == originalPosition.First(x => x.Id == moon.Id).Velocity.X
                        )
                {
                    if (!moon.XCyclePattern.Any())
                    {
                        pattern = numberOfSteps;
                    }
                    else
                    {
                        pattern = numberOfSteps - moon.XCyclePattern.Sum();
                    }

                    moon.XCyclePattern.Add(pattern);
                }
            }

            foreach (var moon in moons.Where(x => x.YCycleLength == 0))
            {
                if (moon.Position.Y == originalPosition.First(x => x.Id == moon.Id).Position.Y
                            && moon.Velocity.Y == originalPosition.First(x => x.Id == moon.Id).Velocity.Y
                            )
                {
                    if (!moon.YCyclePattern.Any())
                    {
                        pattern = numberOfSteps;
                    }
                    else
                    {
                        pattern = numberOfSteps - moon.YCyclePattern.Sum();
                    }

                    moon.YCyclePattern.Add(pattern);
                }
            }

            foreach (var moon in moons.Where(x => x.ZCycleLength == 0))
            {
                if (moon.Position.Z == originalPosition.First(x => x.Id == moon.Id).Position.Z
                        && moon.Velocity.Z == originalPosition.First(x => x.Id == moon.Id).Velocity.Z
                        )
                {
                    if (!moon.ZCyclePattern.Any())
                    {
                        pattern = numberOfSteps;
                    }
                    else
                    {
                        pattern = numberOfSteps - moon.ZCyclePattern.Sum();
                    }

                    moon.ZCyclePattern.Add(pattern);
                }
            }
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