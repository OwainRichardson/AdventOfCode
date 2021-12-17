using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_10_1
    {
        public static void Execute()
        {
            long[] inputs = File.ReadAllLines(@"2020\Data\day10.txt").Select(x => long.Parse(x)).OrderBy(x => x).ToArray();
            int oneStepJumps = 1;
            int threeStepJumps = 1;

            for (int index = 1; index < inputs.Length; index++)
            {
                if (inputs[index] - inputs[index - 1] == 1)
                {
                    oneStepJumps += 1;
                }
                else if (inputs[index] - inputs[index - 1] == 3)
                {
                    threeStepJumps += 1;
                }
            }

            Console.WriteLine(oneStepJumps * threeStepJumps);
        }
    }
}
