using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public static class D_22_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day22.txt");

            Queue<int> player1 = ParsePlayer1(inputs);
            Queue<int> player2 = ParsePlayer2(inputs);

            _ = PlayCrabCards(player1, player2, out Queue<int> pack);


            long score = 0;

            for (int i = pack.Count; i > 0; i--)
            {
                int value = pack.Dequeue();
                score += (i * value);
            }

            Console.WriteLine(score);
        }

        private static bool PlayCrabCards(Queue<int> player1Deck, Queue<int> player2Deck, out Queue<int> pack)
        {
            HashSet<string> previousTurnsPlayer1 = new HashSet<string>();
            HashSet<string> previousTurnsPlayer2 = new HashSet<string>();

            while (true)
            {
                if (!player1Deck.Any())
                {
                    pack = player2Deck;
                    return false;
                }
                else if (!player2Deck.Any())
                {
                    pack = player1Deck;
                    return true;
                }

                if (previousTurnsPlayer1.Contains(JoinDeck(player1Deck)) || previousTurnsPlayer2.Contains(JoinDeck(player2Deck)))
                {
                    pack = player1Deck;
                    return true;
                }
                previousTurnsPlayer1.Add(JoinDeck(player1Deck));
                previousTurnsPlayer2.Add(JoinDeck(player2Deck));


                int player1DeckSize = player1Deck.Count;
                int player2DeckSize = player2Deck.Count;

                int player1Card = player1Deck.Dequeue();
                int player2Card = player2Deck.Dequeue();

                if (player1DeckSize - 1 >= player1Card && player2DeckSize - 1 >= player2Card)
                {
                    if (PlayCrabCards(new Queue<int>(player1Deck.ToArray().Take(player1Card)), new Queue<int>(player2Deck.ToArray().Take(player2Card)), out Queue<int> subPack))
                    {
                        player1Deck.Enqueue(player1Card);
                        player1Deck.Enqueue(player2Card);
                    }
                    else
                    {
                        player2Deck.Enqueue(player2Card);
                        player2Deck.Enqueue(player1Card);
                    }
                }
                else
                {
                    if (player1Card > player2Card)
                    {
                        player1Deck.Enqueue(player1Card);
                        player1Deck.Enqueue(player2Card);
                    }
                    else
                    {
                        player2Deck.Enqueue(player2Card);
                        player2Deck.Enqueue(player1Card);
                    }
                }
            }
        }

        private static string JoinDeck(Queue<int> deck)
        {
            return $"{string.Join("-", deck)}";
        }

        private static Queue<int> ParsePlayer1(string[] inputs)
        {
            Queue<int> cards = new Queue<int>();

            foreach (string input in inputs.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(input)) break;

                cards.Enqueue(int.Parse(input));
            }

            return cards;
        }

        private static Queue<int> ParsePlayer2(string[] inputs)
        {
            Queue<int> cards = new Queue<int>();
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
                    cards.Enqueue(int.Parse(input));
                }
            }

            return cards;
        }
    }
}