using AdventOfCode._2019.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019
{
    public static class D_14_1
    {
        public static void Execute()
        {
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2019\Data\day14_full.txt");

            var recipes = ParseInstructionsIntoRequirements(inputs);

            FuelInstruction fuel = recipes.First(x => x.Creates.FuelType == "FUEL");

            Dictionary<string, int> requirements = new Dictionary<string, int>();
            Dictionary<string, int> dict = CalculateOreRequired(recipes.Where(x => x.Creates.FuelType != "FUEL"), fuel, requirements, null);

            int total = CalculateOreRequiredFromRequirements(dict, recipes.Where(x => x.Requires.Any(y => y.FuelType == "ORE")));
            Console.WriteLine(total);
        }

        private static int CalculateOreRequiredFromRequirements(Dictionary<string, int> dict, IEnumerable<FuelInstruction> recipes)
        {
            int total = 0;

            foreach (var entry in dict)
            {
                FuelInstruction recipe = recipes.First(x => x.Creates.FuelType == entry.Key);
                int value = dict[entry.Key];
                while (value > 0)
                {
                    total += recipe.Requires[0].Quantity;
                    value -= recipe.Creates.Quantity;
                }
            }

            return total;
        }

        private static Dictionary<string, int> CalculateOreRequired(IEnumerable<FuelInstruction> recipes, FuelInstruction recipe, Dictionary<string, int> requirements, Fuel previousRequirement, int mult = 1)
        {
            foreach (var requirement in recipe.Requires)
            {
                if (requirement.FuelType == "ORE")
                {
                    requirements.AddIfPossible(previousRequirement.FuelType, mult);
                }
                else
                {
                    CalculateOreRequired(recipes, recipes.First(x => x.Creates.FuelType == requirement.FuelType), requirements, requirement, mult * requirement.Quantity);
                }
            }

            return requirements;
        }

        private static List<FuelInstruction> ParseInstructionsIntoRequirements(string[] instructions)
        {
            List<FuelInstruction> fuelInstructions = new List<FuelInstruction>();

            foreach (var instruction in instructions)
            {
                var splitter = instruction.IndexOf("=>");
                var requirements = instruction.Substring(0, splitter);
                var requirementsSplit = requirements.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
                FuelInstruction fuelRequirement = new FuelInstruction();

                foreach (var requirement in requirementsSplit)
                {
                    var fuel = new Fuel();
                    fuel.Quantity = int.Parse(requirement.Substring(0, requirement.IndexOf(' ')));
                    fuel.FuelType = requirement.Substring(requirement.IndexOf(' ') + 1).Trim();

                    fuelRequirement.Requires.Add(fuel);
                }

                var produces = instruction.Substring(splitter + 2).Trim();
                var producesFuel = new Fuel();
                producesFuel.Quantity = int.Parse(produces.Substring(0, produces.IndexOf(' ')));
                producesFuel.FuelType = produces.Substring(produces.IndexOf(' ') + 1).Trim();

                fuelRequirement.Creates = producesFuel;

                fuelInstructions.Add(fuelRequirement);
            }

            return fuelInstructions;
        }

        private static Dictionary<string, int> AddIfPossible(this Dictionary<string, int> requirements, string key, int value)
        {
            if (requirements.ContainsKey(key))
            {
                requirements[key] += value;
            }
            else
            {
                requirements.Add(key, value);
            }

            return requirements;
        }
    }
}