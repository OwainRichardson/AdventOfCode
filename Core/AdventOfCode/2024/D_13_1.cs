using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2024
{
    public static class D_13_1
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day13.txt");

            List<ClawMachine> clawMachines = ParseInputs(inputs);

            int tokens = 0;

            foreach (ClawMachine clawMachine in clawMachines)
            {
                tokens += FindPrize(clawMachine);
            }

            return tokens.ToString();
        }

        private static int FindPrize(ClawMachine clawMachine)
        {
            int minTokens = 9999999;

            int numberToIterateTo = 0;
            if (clawMachine.ButtonA.XMove > clawMachine.ButtonB.XMove)
            {
                numberToIterateTo = (int)Math.Ceiling((double)clawMachine.Prize.X / clawMachine.ButtonB.XMove);
            }
            else
            {
                numberToIterateTo = (int)Math.Ceiling((double)clawMachine.Prize.X / clawMachine.ButtonA.XMove);
            }

            for (int ax = 0; ax <= numberToIterateTo; ax++)
            {
                for (int bx = 0; bx <= numberToIterateTo; bx++)
                {
                    int xTotal = (ax * clawMachine.ButtonA.XMove) + (bx * clawMachine.ButtonB.XMove);
                    if (xTotal == clawMachine.Prize.X)
                    {
                        int yTotal = (ax * clawMachine.ButtonA.YMove) + (bx * clawMachine.ButtonB.YMove);

                        if (yTotal == clawMachine.Prize.Y)
                        {
                            minTokens = CalculateTokens(ax, bx);
                        }
                    }
                }
            }

            return minTokens == 9999999 ? 0 : minTokens;
        }

        private static int CalculateTokens(int ax, int bx)
        {
            return (ax * 3) + bx;
        }

        private static List<ClawMachine> ParseInputs(string[] inputs)
        {
            string buttonPattern = @"^Button\W([A-Z])\:\W[X]([+|-]\d+),\W[Y]([+|-]\d+)$";
            Regex buttonRegex = new Regex(buttonPattern);
            
            string prizePattern = @"^Prize\:\WX\=([-]?\d+),\WY\=([-]?\d+)$";
            Regex prizeRegex = new Regex(prizePattern);

            List<ClawMachine> clawMachines = new List<ClawMachine>();
            ClawMachine clawMachine = new ClawMachine();

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    clawMachines.Add(clawMachine);
                    clawMachine = new ClawMachine();

                    continue;
                }

                Match buttonMatch = buttonRegex.Match(input);
                if (buttonMatch.Success)
                {
                    if (buttonMatch.Groups[1].Value == "A")
                    {
                        clawMachine.ButtonA = new ClawMachineButton
                        {
                            XMove = int.Parse(buttonMatch.Groups[2].Value),
                            YMove = int.Parse(buttonMatch.Groups[3].Value)
                        };
                    }
                    else if (buttonMatch.Groups[1].Value == "B")
                    {
                        clawMachine.ButtonB = new ClawMachineButton
                        {
                            XMove = int.Parse(buttonMatch.Groups[2].Value),
                            YMove = int.Parse(buttonMatch.Groups[3].Value)
                        };
                    }

                    continue;
                }

                Match prizeMatch = prizeRegex.Match(input);
                if (prizeMatch.Success)
                {
                    clawMachine.Prize = new ClawMachingPrize
                    {
                        X = int.Parse(prizeMatch.Groups[1].Value),
                        Y = int.Parse(prizeMatch.Groups[2].Value)
                    };
                }
            }

            clawMachines.Add(clawMachine);

            return clawMachines;
        }
    }
}