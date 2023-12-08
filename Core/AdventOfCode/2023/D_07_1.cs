using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_07_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day07.txt").ToArray();

            List<CamelCardHand> hands = ParseInputsToHands(inputs);

            hands = OrderHands(hands);
            hands.Reverse();

            long total = 0;
            long index = 1;
            foreach (var hand in hands)
            {
                total += (hand.Bet * index);
                index++;

                //Console.WriteLine(hand.Hand);
            }

            Console.WriteLine(total);
        }

        private static List<CamelCardHand> OrderHands(List<CamelCardHand> hands)
        {
            List<CamelCardHand> orderedHands = new List<CamelCardHand>();

            OrderHandType(orderedHands, hands.Where(h => h.HandType == CamelCardHandType.FiveOfAKind).ToList());
            OrderHandType(orderedHands, hands.Where(h => h.HandType == CamelCardHandType.FourOfAKind).ToList());
            OrderHandType(orderedHands, hands.Where(h => h.HandType == CamelCardHandType.FullHouse).ToList());
            OrderHandType(orderedHands, hands.Where(h => h.HandType == CamelCardHandType.ThreeOfAKind).ToList());
            OrderHandType(orderedHands, hands.Where(h => h.HandType == CamelCardHandType.TwoPair).ToList());
            OrderHandType(orderedHands, hands.Where(h => h.HandType == CamelCardHandType.OnePair).ToList());
            OrderHandType(orderedHands, hands.Where(h => h.HandType == CamelCardHandType.HighCard).ToList());

            return orderedHands;
        }

        private static void OrderHandType(List<CamelCardHand> orderedHands, List<CamelCardHand> hands)
        {
            hands.ForEach(hand =>
            {
                hand.Order = PadToTwoValues(CardValues[hand.Hand[0].ToString()].ToString());
            });

            var jointHands = hands.GroupBy(x => x.Order, (key, value) => new { Order = key, Number = value.Count() }).ToList();

            int index = 1;

            while (jointHands.Any(h => h.Number > 1))
            {
                foreach (var jointValue in jointHands)
                {
                    foreach (var hand in hands)
                    {
                        hand.Order = $"{hand.Order}{PadToTwoValues(CardValues[hand.Hand[index].ToString()].ToString())}";
                    }
                }

                jointHands = hands.GroupBy(x => x.Order, (key, value) => new { Order = key, Number = value.Count() }).ToList();
                index++;
            }

            foreach (var hand in hands.OrderByDescending(h => h.Order))
            {
                orderedHands.Add(hand);
            }
        }

        private static string PadToTwoValues(string value)
        {
            if (int.Parse(value) < 10)
            {
                return $"0{value}";
            }

            return value;
        }

        private static List<CamelCardHand> ParseInputsToHands(string[] inputs)
        {
            List<CamelCardHand> hands = new List<CamelCardHand>();

            foreach (string input in inputs)
            {
                CamelCardHand hand = new CamelCardHand();

                string[] inputSplit = input.Split(' ').ToArray();
                hand.Hand = inputSplit[0];
                hand.Bet = int.Parse(inputSplit[1]);
                hand.HandType = CalculateHandType(inputSplit[0]);

                hands.Add(hand);
            }

            return hands;
        }

        private static CamelCardHandType CalculateHandType(string hand)
        {
            var cards = hand.GroupBy(x => x, (key, value) => new { Card = key, Number = value.Count() }).ToList();

            if (cards.Exists(c => c.Number == 5))
            {
                return CamelCardHandType.FiveOfAKind;
            }

            if (cards.Exists(c => c.Number == 4))
            {
                return CamelCardHandType.FourOfAKind;
            }

            if (cards.Exists(c => c.Number == 2) && cards.Exists(c => c.Number == 3))
            {
                return CamelCardHandType.FullHouse;
            }

            if (cards.Exists(c => c.Number == 3))
            {
                return CamelCardHandType.ThreeOfAKind;
            }

            if (cards.Count(c => c.Number == 2) == 2)
            {
                return CamelCardHandType.TwoPair;
            }

            if (cards.Count(c => c.Number == 2) == 1)
            {
                return CamelCardHandType.OnePair;
            }

            return CamelCardHandType.HighCard;
        }

        private static Dictionary<string, int> CardValues = new Dictionary<string, int>
        {
            { "A", 14 },
            { "K", 13 },
            { "Q", 12 },
            { "J", 11 },
            { "T", 10 },
            { "9", 9 },
            { "8", 8 },
            { "7", 7 },
            { "6", 6 },
            { "5", 5 },
            { "4", 4 },
            { "3", 3 },
            { "2", 2 }
        };
    }
}