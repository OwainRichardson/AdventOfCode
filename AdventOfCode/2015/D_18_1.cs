using AdventOfCode._2015.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    public class D_18_1
    {
        public static string[,] _lights = new string[100, 100];
        public static string[,] _nextLights = new string[100, 100];
        public static int _lineLength = 0;

        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day18_full.txt");

            ParseInput(inputs);

            Array.Copy(_lights, _nextLights, _lights.Length);

            //PrintLights();

            for (int i = 1; i <= 100; i++)
            {
                StepLights();

                Array.Copy(_nextLights, _lights, _nextLights.Length);

                //PrintLights();
            }

            int lightsOn = 0;

            for (int i = 0; i < _lineLength; i++)
            {
                for (int j = 0; j < _lineLength; j++)
                {
                    if (IsOn(_nextLights[i, j]))
                    {
                        lightsOn++;
                    }
                }
            }


            Console.Write($"Number of lights on is ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(lightsOn);
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void StepLights()
        {
            for (int i = 0; i < _lineLength; i++)
            {
                for (int j = 0; j < _lineLength; j++)
                {
                    var neighbouringLights = GetNeighbouringLights(i, j);

                    var currentLight = _lights[i, j];

                    if (IsOn(currentLight))
                    {
                        int onCount = 0;

                        foreach (var light in neighbouringLights)
                        {
                            if (IsOn(_lights[light.Y, light.X]))
                            {
                                onCount++;
                            }
                        }

                        if (onCount == 2 || onCount == 3)
                        {
                            _nextLights[i, j] = "#";
                        }
                        else
                        {
                            _nextLights[i, j] = ".";
                        }
                    }
                    else
                    {
                        int onCount = 0;

                        foreach (var light in neighbouringLights)
                        {
                            if (IsOn(_lights[light.Y, light.X]))
                            {
                                onCount++;
                            }
                        }

                        if (onCount == 3)
                        {
                            _nextLights[i, j] = "#";
                        }
                        else
                        {
                            _nextLights[i, j] = ".";
                        }
                    }
                }
            }
        }

        private static bool IsOn(string currentLight)
        {
            return currentLight == "#";
        }

        private static List<LightCoords> GetNeighbouringLights(int i, int j)
        {
            List<LightCoords> neighbouringLights = new List<LightCoords>();

            for (int y = i - 1; y <= i + 1; y++)
            {
                for (int x = j - 1; x <= j + 1; x++)
                {
                    if (y < 0 || x < 0 || y > _lineLength - 1 || x > _lineLength - 1 || (y == i && x == j))
                    {
                        continue;
                    }

                    neighbouringLights.Add(new LightCoords
                    {
                        X = x,
                        Y = y
                    });
                }
            }

            return neighbouringLights;

        }

        private static void PrintLights()
        {
            for (int i = 0; i < _lineLength; i++)
            {
                for (int j = 0; j < _lineLength; j++)
                {
                    Console.Write($"{_nextLights[i, j]}");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static void ParseInput(string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                var line = inputs[i];

                if (_lineLength == 0)
                {
                    _lineLength = line.Length;
                }

                for (int j = 0; j < line.Length; j++)
                {
                    _lights[i, j] = line[j].ToString();
                }
            }
        }
    }
}