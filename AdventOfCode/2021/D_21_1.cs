using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_21_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day21.txt");

            List<Player> players = ParseInputs(inputs);

            PlayGame(players);
        }

        private static void PlayGame(List<Player> players)
        {
            int dieValue = 1;
            int playerTurn = 1;
            int dieRolls = 0;

            while (players.All(p => p.Score < 1000))
            {
                Player player = players.Single(p => p.Id == playerTurn);

                for (int i = 0; i <= 2; i++)
                {
                    player.Space += (dieValue % 10);
                    player.Space = player.Space > 10 ? player.Space - 10 : player.Space;
                    
                    dieRolls++;
                    dieValue = dieValue == 100 ? 1 : dieValue + 1;
                }

                player.Score += player.Space;

                playerTurn = playerTurn == 1 ? 2 : 1;
            }

            Console.WriteLine(players.Single(p => p.Score < 1000).Score * dieRolls);
        }

        private static List<Player> ParseInputs(string[] inputs)
        {
            List<Player> players = new List<Player>();

            string pattern = @"Player\s(\d)\sstarting\sposition:\s(\d)";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                Match match = regex.Match(input);
                Player player = new Player
                {
                    Id = int.Parse(match.Groups[1].Value),
                    Space = int.Parse(match.Groups[2].Value),
                    Score = 0
                };

                players.Add(player);
            }

            return players;
        }
    }
}