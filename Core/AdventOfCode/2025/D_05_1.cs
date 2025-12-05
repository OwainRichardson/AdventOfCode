namespace AdventOfCode._2025;

public static class D_05_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day05.txt").ToArray();

        var (freshIngredientRanges, ingredients) = ParseInputs(inputs);

        int numberOfFreshIngredients = 0;

        foreach (long ingredient in ingredients.Where(ing => freshIngredientRanges.Any(fir => fir.StartOfRange <= ing && fir.EndOfRange >= ing)))
        {
            numberOfFreshIngredients++;
        }

        Console.WriteLine(numberOfFreshIngredients);
    }

    private static (List<(long StartOfRange, long EndOfRange)> freshIngredientRanges, List<long> ingredients) ParseInputs(string[] inputs)
    {
        bool parseFreshIngredientRanges = true;
        List<(long, long)> freshIngredientRanges = new();
        List<long> ingredients = new();

        foreach (string input in inputs)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                parseFreshIngredientRanges = false;
                continue;
            }

            if (parseFreshIngredientRanges)
            {
                string[] ingredientRangesEnds = input.Split('-').ToArray();

                freshIngredientRanges.Add((long.Parse(ingredientRangesEnds[0]), long.Parse(ingredientRangesEnds[1])));
            }
            else
            {
                ingredients.Add(long.Parse(input));
            }
        }

        return (freshIngredientRanges, ingredients);
    }
}