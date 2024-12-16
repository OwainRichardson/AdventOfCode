using AdventOfCode._2024.Extensions;
using AdventOfCode._2024.Models;
using System.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace AdventOfCode._2024
{
    public static class D_13_2
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day13.txt");

            List<ClawMachine> clawMachines = ParseInputs(inputs);

            long tokens = 0;

            foreach (ClawMachine clawMachine in clawMachines)
            {
                tokens += FindPrize(clawMachine);
            }

            return tokens.ToString();
        }

        private static long FindPrize(ClawMachine clawMachine)
        {
            double[,] eliminator = new double[2, 2];

            eliminator[0, 0] = clawMachine.ButtonB.YMove * clawMachine.ButtonA.XMove;
            eliminator[0, 1] = clawMachine.ButtonB.YMove * clawMachine.Prize.X;
            // STEP 3:
            eliminator[1, 0] = clawMachine.ButtonB.XMove * clawMachine.ButtonA.YMove;
            eliminator[1, 1] = clawMachine.ButtonB.XMove * clawMachine.Prize.Y;

            // STEPS 4, 5:
            if (!long.TryParse(((eliminator[0, 1] - eliminator[1, 1]) / (eliminator[0, 0] - eliminator[1, 0])).ToString(), out long x))
            {
                return 0;
            }
            // STEP 6:
            if (!long.TryParse(((clawMachine.Prize.X - clawMachine.ButtonA.XMove * x) / clawMachine.ButtonB.XMove).ToString(), out long y))
            {
                return 0;
            }

            return CalculateTokens(x, y);
        }

        private static long CalculateTokens(long ax, long bx)
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
                        X = long.Parse(prizeMatch.Groups[1].Value) + 10000000000000,
                        Y = long.Parse(prizeMatch.Groups[2].Value) + 10000000000000
                    };
                }
            }

            clawMachines.Add(clawMachine);

            return clawMachines;
        }
    }
}