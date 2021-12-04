using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2021
{
    public static class D_02_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day02.txt").ToArray();

            int x = 0;
            int y = 0;

            foreach (string input in inputs)
            {
                if (input.StartsWith("forward"))
                {
                    int value = int.Parse(input.Split(new string[] { "forward " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    x += value;
                }
                else if (input.StartsWith("down"))
                {
                    int value = int.Parse(input.Split(new string[] { "down " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    y += value;
                }
                else if (input.StartsWith("up"))
                {
                    int value = int.Parse(input.Split(new string[] { "up " }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    y -= value;
                }
            }

            Console.WriteLine(x * y);
        }
    }
}
