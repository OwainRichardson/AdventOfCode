using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_06_2
    {
        public static int[,] _lights = new int[1000, 1000];
        private static string TurnOff = "turn off";
        private static string TurnOn = "turn on";
        private static string Toggle = "toggle";
        public static void Execute()
        {
            var instructions = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day6_full.txt");

            foreach (var instruction in instructions)
            {
                if (instruction.StartsWith(TurnOff))
                {
                    TurnOffLights(instruction);
                }
                else if (instruction.StartsWith(TurnOn))
                {
                    TurnOnLights(instruction);
                }
                else if (instruction.StartsWith(Toggle))
                {
                    ToggleLights(instruction);
                }
            }

            int total = 0;

            for (int y = 0; y <= 999; y++)
            {
                for (int x = 0; x <= 999; x++)
                {
                    total += _lights[x, y];
                }
            }

            Console.Write($"The total brightness is: ");

            CustomConsoleColour.SetAnswerColour();
            Console.Write(total);
            Console.ResetColor();

            Console.WriteLine();
        }

        private static void TurnOffLights(string instruction)
        {
            instruction = instruction
                            .Replace(TurnOff, "")
                            .Replace("through", "")
                            .Trim();

            var coords = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var startCoords = GetCoords(coords[0]);
            var endCoords = GetCoords(coords[1]);

            for (int y = startCoords.Y; y <= endCoords.Y; y++)
            {
                for (int x = startCoords.X; x <= endCoords.X; x++)
                {
                    if (_lights[x, y] > 0)
                    {
                        _lights[x, y] -= 1;
                    }
                }
            }
        }

        private static void TurnOnLights(string instruction)
        {
            instruction = instruction
                            .Replace(TurnOn, "")
                            .Replace("through", "")
                            .Trim();

            var coords = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var startCoords = GetCoords(coords[0]);
            var endCoords = GetCoords(coords[1]);

            for (int y = startCoords.Y; y <= endCoords.Y; y++)
            {
                for (int x = startCoords.X; x <= endCoords.X; x++)
                {
                    _lights[x, y] += 1;
                }
            }
        }

        private static void ToggleLights(string instruction)
        {
            instruction = instruction
                            .Replace(Toggle, "")
                            .Replace("through", "")
                            .Trim();

            var coords = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var startCoords = GetCoords(coords[0]);
            var endCoords = GetCoords(coords[1]);

            for (int y = startCoords.Y; y <= endCoords.Y; y++)
            {
                for (int x = startCoords.X; x <= endCoords.X; x++)
                {
                    _lights[x, y] += 2;
                }
            }
        }

        private static LightCoords GetCoords(string coords)
        {
            return new LightCoords { X = int.Parse(coords.Substring(0, coords.IndexOf(','))), Y = int.Parse(coords.Substring(coords.IndexOf(',') + 1)) };
        }
    }
}
