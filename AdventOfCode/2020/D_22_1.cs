using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_22_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day22.txt");

            List<int> player1 = ParsePlayer1(inputs);
            List<int> player2 = ParsePlayer2(inputs);

            PlayCrabCards(player1, player2);
        }

        private static void PlayCrabCards(List<int> player1, List<int> player2)
        {
            int index = 0;

            while (true)
            {
                if (!player1.Any() || !player2.Any()) break;

                int player1DeckSize = player1.Count;
                int player2DeckSize = player2.Count;

                int player1Card = player1[index % player1DeckSize];
                int player2Card = player2[index % player2DeckSize];

                if (player1Card > player2Card)
                {
                    player1.Remove(player1Card);
                    player1.Add(player1Card);

                    player2.Remove(player2Card);
                    player1.Add(player2Card);
                }
                else
                {
                    player2.Remove(player2Card);
                    player2.Add(player2Card);

                    player1.Remove(player1Card);
                    player2.Add(player1Card);
                }
            }

            long score = 0;

            if (player1.Any())
            {
                for (int i = player1.Count; i > 0; i--)
                {
                    score += (i * player1[player1.Count - i]);
                }
            }

            if (player2.Any())
            {
                for (int i = player2.Count; i > 0; i--)
                {
                    score += (i * player2[player2.Count - i]);
                }
            }

            Console.WriteLine(score);
        }

        private static List<int> ParsePlayer1(string[] inputs)
        {
            List<int> cards = new List<int>();

            foreach (string input in inputs.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(input)) break;

                cards.Add(int.Parse(input));
            }

            return cards;
        }

        private static List<int> ParsePlayer2(string[] inputs)
        {
            List<int> cards = new List<int>();
            bool player2 = false;

            foreach (string input in inputs.Skip(1))
            {
                if (input.Equals("Player 2:"))
                {
                    player2 = true;
                    continue;
                };

                if (player2)
                {
                    cards.Add(int.Parse(input));
                }
            }

            return cards;
        }
    }
}