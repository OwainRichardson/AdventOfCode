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
    public class D_15_2
    {
        private static List<Ingredient> _ingredients = new List<Ingredient>();
        private static int _numberOfIngredients = 0;
        private static List<Score> _scores = new List<Score>();

        public static void Execute()
        {
            var input = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2015\Data\day15_full.txt");

            ParseIngredients(input);

            int[] combination = new int[_numberOfIngredients];

            while (combination[0] != 100)
            {
                StepCombination(ref combination);

                if (combination.Sum() == 100)
                {
                    //PrintCombination(combination);
                    CalculateScores(combination);
                }
            }

            var maxScore = _scores.Max(x => x.TotalScore);
            Console.Write($"Best combination is: ");
            CustomConsoleColour.SetAnswerColour();
            Console.Write(_scores.Max(x => x.TotalScore));
            Console.ResetColor();
            Console.WriteLine();

            //var ingredients = _scores.First(x => x.TotalScore == maxScore).Ingredients;
            //foreach (var ingredient in ingredients)
            //{
            //    Console.WriteLine($"    - {ingredient.Name} = {ingredient.Number} tsp");
            //}
        }

        private static void PrintCombination(int[] combination)
        {
            foreach (var comb in combination)
            {
                Console.Write($"{comb} ");
            }
            Console.WriteLine();
        }

        private static void StepCombination(ref int[] combination, int index = -1)
        {
            if (index == -1)
            {
                index = _numberOfIngredients - 1;
            }

            combination[index]++;

            if (combination[index] > 100)
            {
                combination[index] = 0;
                StepCombination(ref combination, index - 1);
            }
        }

        private static void CalculateScores(int[] combination)
        {
            var score = new Score();

            int capacity = 0;
            int durability = 0;
            int flavour = 0;
            int texture = 0;
            int calories = 0;

            for (int i = 0; i < combination.Length; i++)
            {
                score.Ingredients.Add(new IngredientScore
                {
                    Name = _ingredients[i].Name,
                    Number = combination[i]
                });

                capacity += _ingredients[i].Capacity * combination[i];
                durability += _ingredients[i].Durability * combination[i];
                flavour += _ingredients[i].Flavour * combination[i];
                texture += _ingredients[i].Texture * combination[i];
                calories += _ingredients[i].Calories * combination[i];
            }

            if (calories != 500)
            {
                return;
            }

            int total = 0;

            if (capacity < 0 || durability < 0 || flavour < 0 || texture < 0)
            {
                total = 0;
            }
            else
            {
                total = capacity * durability * flavour * texture;
            }

            score.TotalScore = total;

            _scores.Add(score);
        }

        private static void ParseIngredients(string[] input)
        {
            _numberOfIngredients = input.Length;

            foreach (var ingredient in input)
            {
                var ingred = new Ingredient
                {
                    Name = Regex.Match(ingredient, @"^[A-Za-z]+").Value,
                    Capacity = int.Parse(Regex.Match(ingredient, @"capacity ([-]*\d+)").Groups[1].Value),
                    Durability = int.Parse(Regex.Match(ingredient, @"durability ([-]*\d+)").Groups[1].Value),
                    Flavour = int.Parse(Regex.Match(ingredient, @"flavor ([-]*\d+)").Groups[1].Value),
                    Texture = int.Parse(Regex.Match(ingredient, @"texture ([-]*\d+)").Groups[1].Value),
                    Calories = int.Parse(Regex.Match(ingredient, @"calories ([-]*\d+)").Groups[1].Value)
                };

                _ingredients.Add(ingred);
            }
        }
    }
}