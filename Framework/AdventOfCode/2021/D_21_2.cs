using AdventOfCode._2021.Extensions;
using AdventOfCode._2021.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2021
{
    public static class D_21_2
    {
        private static long _player1Wins = 0;
        private static long _player2Wins = 0;
        private static int _winScore = 21;

        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2021\Data\day21.txt");

            List<Player> players = ParseInputs(inputs);

            PlayGame(players);

            if (_player1Wins > _player2Wins)
                Console.WriteLine(_player1Wins);
            else
                Console.WriteLine(_player2Wins);
        }

        private static void PlayGame(List<Player> players, int playerTurn = 1, int turn = 0)
        {
            if (turn == 0 && players.Any(x => x.Score >= _winScore))
            {
                int winner = players.Single(x => x.Score >= _winScore).Id;
                if (winner == 1)
                {
                    _player1Wins++;
                    Console.Write($"\rP1: {_player1Wins} - P2: {_player2Wins}");
                }
                else
                {
                    _player2Wins++;
                    Console.Write($"\rP1: {_player1Wins} - P2: {_player2Wins}");
                }


                return;
            }

            for (int i = turn; i <= 2; i++)
            {
                List<Player> rolled1 = CloneList(players);
                Player playerR1 = rolled1.Single(p => p.Id == playerTurn);
                playerR1.Space += 1;
                playerR1.Space = playerR1.Space > 10 ? playerR1.Space - 10 : playerR1.Space;
                if (i == 2) playerR1.Score += playerR1.Space;
                PlayGame(rolled1, i != 2 ? playerTurn : playerTurn == 1 ? 2 : 1, i == 2 ? 0 : i + 1);

                List<Player> rolled2 = CloneList(players);
                Player playerR2 = rolled1.Single(p => p.Id == playerTurn);
                playerR2.Space += 1;
                playerR2.Space = playerR2.Space > 10 ? playerR2.Space - 10 : playerR2.Space;
                if (i == 2) playerR2.Score += playerR2.Space;
                PlayGame(rolled2, i != 2 ? playerTurn : playerTurn == 1 ? 2 : 1, i == 2 ? 0 : i + 1);

                List<Player> rolled3 = CloneList(players);
                Player playerR3 = rolled1.Single(p => p.Id == playerTurn);
                playerR3.Space += 1;
                playerR3.Space = playerR3.Space > 10 ? playerR3.Space - 10 : playerR3.Space;
                if (i == 2) playerR3.Score += playerR3.Space;
                PlayGame(rolled3, i != 2 ? playerTurn : playerTurn == 1 ? 2 : 1, i == 2 ? 0 : i + 1);
            }
        }

        private static List<Player> CloneList(List<Player> players)
        {
            List<Player> newPlayers = new List<Player>();

            foreach (Player p in players)
            {
                newPlayers.Add(new Player
                {
                    Id = p.Id,
                    Score = p.Score,
                    Space = p.Space
                });
            }

            return newPlayers;
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