using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_13_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day13_full.txt");

            List<Firewall> firewalls = ParseFirewalls(inputs);
            int picoSecondDelay = 0;
            bool success = false;

            while (!success)
            {
                success = true;
                foreach (var firewall in firewalls)
                {
                    if ((firewall.Id + picoSecondDelay) % (2 * firewall.Depth - 2) == 0)
                    {
                        success = false;
                        picoSecondDelay++;
                        break;
                    }
                }
            }

            Console.WriteLine(picoSecondDelay);
        }

        private static List<Firewall> ResetScanners(List<Firewall> firewalls)
        {
            foreach (var firewall in firewalls)
            {
                firewall.ScannerIndex = 1;
                firewall.ScannerDirection = ScannerDirections.Down;
            }

            return firewalls;
        }

        private static bool StepPackage(List<Firewall> firewalls, Package package, int picoSecondDelay, int picoSecond)
        {
            if (picoSecond < picoSecondDelay)
            {
                return false;
            }

            package.ColumnIndex++;

            if (firewalls.Any(x => x.Id == package.ColumnIndex))
            {
                Firewall firewall = firewalls.First(x => x.Id == package.ColumnIndex);

                if (firewall.ScannerIndex == 1)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<Firewall> StepScanners(List<Firewall> firewalls)
        {
            foreach (var firewall in firewalls)
            {
                if (firewall.ScannerDirection == ScannerDirections.Down)
                {
                    if (firewall.ScannerIndex + 1 > firewall.Depth)
                    {
                        firewall.ScannerIndex--;
                        firewall.ScannerDirection = ScannerDirections.Up;
                    }
                    else
                    {
                        firewall.ScannerIndex++;
                    }
                }
                else if (firewall.ScannerDirection == ScannerDirections.Up)
                {
                    if (firewall.ScannerIndex - 1 == 0)
                    {
                        firewall.ScannerIndex++;
                        firewall.ScannerDirection = ScannerDirections.Down;
                    }
                    else
                    {
                        firewall.ScannerIndex--;
                    }
                }
            }

            return firewalls;
        }

        private static void PrintFirewalls(List<Firewall> firewalls, Package package)
        {
            for (int i = 0; i <= firewalls.Max(x => x.Id); i++)
            {
                Console.Write($" {i}\t");
            }
            Console.WriteLine();

            int depth = 1;

            while (depth <= firewalls.Max(x => x.Depth))
            {
                for (int i = 0; i <= firewalls.Max(x => x.Id); i++)
                {
                    if (firewalls.Any(x => x.Id == i) && firewalls.First(x => x.Id == i).Depth >= depth)
                    {
                        if (firewalls.First(x => x.Id == i).ScannerIndex == depth)
                        {
                            if (package.ColumnIndex == i && depth == 1)
                            {
                                Console.Write($"(S)\t");
                            }
                            else
                            {
                                Console.Write($"[S]\t");
                            }
                        }
                        else
                        {
                            if (package.ColumnIndex == i && depth == 1)
                            {
                                Console.Write($"( )\t");
                            }
                            else
                            {
                                Console.Write($"[ ]\t");
                            }
                        }
                    }
                    else
                    {
                        Console.Write($"   \t");
                    }
                }
                Console.WriteLine();

                depth++;
            }

            Console.WriteLine();

        }

        private static List<Firewall> ParseFirewalls(string[] inputs)
        {
            List<Firewall> firewalls = new List<Firewall>();
            string pattern = @"(\d+): (\d+)";
            Regex regex = new Regex(pattern);

            foreach (var input in inputs)
            {
                Match match = regex.Match(input);
                Firewall firewall = new Firewall
                {
                    Id = int.Parse(match.Groups[1].Value),
                    Depth = int.Parse(match.Groups[2].Value),
                    ScannerIndex = 1,
                    ScannerDirection = ScannerDirections.Down
                };

                firewalls.Add(firewall);
            }

            return firewalls;
        }
    }
}
