using AdventOfCode._2018.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2018
{
    public static class D_03_2
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"../../../AdventOfCode/2018/Data/day03_full.txt");

            List<Claim> claims = ParseInputs(inputs);

            int maxX = claims.Max(x => x.X + x.Width);

            int maxY = claims.Max(x => x.Y + x.Height);

            char[,] cloth = new char[maxX + 1, maxY + 1];

            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    cloth[x, y] = '.';
                }
            }

            MapClaims(cloth, claims, maxX, maxY);
        }

        private static void MapClaims(char[,] cloth, List<Claim> claims, int maxX, int maxY)
        {
            foreach (Claim claim in claims)
            {
                bool overlaps = false;

                for (int y = claim.Y; y < claim.Y + claim.Height; y++)
                {
                    for (int x = claim.X; x < claim.X + claim.Width; x++)
                    {
                        claim.Coords.Add($"{x}-{y}", false);

                        if (cloth[x, y] == 'X')
                        {
                            claim.Overlapped = true;
                            FindOtherClaim(claims, $"{x}-{y}");
                        }
                        else if (cloth[x, y] != '.')
                        {
                            cloth[x, y] = 'X';
                            claim.Overlapped = true;
                            FindOtherClaim(claims, $"{x}-{y}");
                        }
                        else
                        {
                            cloth[x, y] = 'O';
                        }
                    }
                }

                //PrintCloth(cloth, maxX, maxY);
            }

            Console.WriteLine(claims.Single(x => !x.Overlapped).Id);
        }

        private static void FindOtherClaim(List<Claim> claims, string v)
        {
            var overlappingClaims = claims.Where(x => x.Coords.ContainsKey(v));

            foreach (Claim claim in overlappingClaims)
            {
                claim.Overlapped = true;
            }
        }

        private static void PrintCloth(char[,] cloth, int maxX, int maxY)
        {
            for (int y = 0; y <= maxY; y++)
            {
                for (int x = 0; x <= maxX; x++)
                {
                    Console.Write(cloth[x, y]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static List<Claim> ParseInputs(string[] inputs)
        {
            List<Claim> claims = new List<Claim>();
            string pattern = @"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);
                Claim claim = new Claim();

                claim.Id = int.Parse(match.Groups[1].Value);

                claim.X = int.Parse(match.Groups[2].Value);
                claim.Y = int.Parse(match.Groups[3].Value);

                claim.Width = int.Parse(match.Groups[4].Value);
                claim.Height = int.Parse(match.Groups[5].Value);

                claims.Add(claim);
            }

            return claims;
        }
    }
}
