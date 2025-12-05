using AdventOfCode._2025.Models;

namespace AdventOfCode._2025;

public static class D_05_2
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day05.txt").ToArray();

        List<IngredientRange> freshIngredientRanges = ParseInputs(inputs);

        List<IngredientRange> ranges = SimplifyRanges(freshIngredientRanges);

        long count = 0;

        foreach (var range in ranges)
        {
            count += range.EndOfRange - range.StartOfRange + 1;
        }

        Console.WriteLine(count);
    }

    private static List<IngredientRange> SimplifyRanges(List<IngredientRange> freshIngredientRanges)
    {
        foreach (var range in freshIngredientRanges)
        {
            foreach (var innerRange in freshIngredientRanges.Where(fir => fir.Id != range.Id))
            {
                if (IsContained(range, innerRange))
                {
                    freshIngredientRanges.Remove(range);

                    return SimplifyRanges(freshIngredientRanges);
                }

                if (IsIntersect(range, innerRange))
                {
                    ResolveIntersect(range, innerRange);

                    freshIngredientRanges.Remove(range);

                    return SimplifyRanges(freshIngredientRanges);
                }
            }
        }

        if (AnyIntersects(freshIngredientRanges))
        {
            return SimplifyRanges(freshIngredientRanges);
        }

        return freshIngredientRanges;
    }

    private static bool IsContained(IngredientRange range, IngredientRange innerRange)
    {
        if (range.StartOfRange >= innerRange.StartOfRange && range.EndOfRange <= innerRange.EndOfRange)
        {
            return true;
        }

        return false;
    }

    private static void ResolveIntersect(IngredientRange range, IngredientRange innerRange)
    {
        if (range.StartOfRange >= innerRange.StartOfRange && range.StartOfRange <= innerRange.EndOfRange)
        {
            innerRange.EndOfRange = range.EndOfRange;
        }
        else if (range.EndOfRange >= innerRange.StartOfRange && range.EndOfRange <= innerRange.EndOfRange)
        {
            innerRange.StartOfRange = range.StartOfRange;
        }
    }

    private static bool IsIntersect(IngredientRange range, IngredientRange innerRange)
    {
        if (range.StartOfRange >= innerRange.StartOfRange && range.StartOfRange <= innerRange.EndOfRange)
        {
            return true;
        }

        if (range.EndOfRange >= innerRange.StartOfRange && range.EndOfRange <= innerRange.EndOfRange)
        {
            return true;
        }

        return false;
    }

    private static bool AnyIntersects(List<IngredientRange> parsedIngredientRanges)
    {
        foreach (var range in parsedIngredientRanges)
        {
            if (parsedIngredientRanges.Any(pir => range.StartOfRange >= pir.StartOfRange && range.StartOfRange <= pir.EndOfRange && range.Id != pir.Id))
            {
                return true;
            }

            if (parsedIngredientRanges.Any(pir => range.EndOfRange >= pir.StartOfRange && range.EndOfRange <= pir.EndOfRange && range.Id != pir.Id))
            {
                return true;
            }
        }

        return false;
    }

    private static List<IngredientRange> ParseInputs(string[] inputs)
    {
        List<IngredientRange> freshIngredientRanges = new();
        long index = 1;

        foreach (string input in inputs)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                break;
            }

            string[] ingredientRangesEnds = input.Split('-').ToArray();

            freshIngredientRanges.Add(new() { StartOfRange = long.Parse(ingredientRangesEnds[0]), EndOfRange = long.Parse(ingredientRangesEnds[1]), Id = index });
            index++;
        }

        return freshIngredientRanges;
    }
}