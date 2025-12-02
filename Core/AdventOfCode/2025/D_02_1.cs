namespace AdventOfCode._2025;

public static class D_02_1
{
    public static void Execute()
    {
        string input = File.ReadAllLines(@"2025\Data\day02.txt")[0];
        List<Tuple<long, long>> productRanges = ParseInputs(input);
        long total = 0;

        foreach (Tuple<long, long> range in productRanges)
        {
            var (start, finish) = range;

            for (long index = start; index <= finish; index++)
            {
                if (IsInvalidProductId(index))
                {
                    total += index;
                }
            }
        }

        Console.WriteLine(total);
    }

    private static bool IsInvalidProductId(long productId)
    {
        int productIdLength = productId.ToString().Length;

        if (productId.ToString().Substring(0, productIdLength / 2) == productId.ToString().Substring(productIdLength / 2))
        {
            return true;
        }

        return false;
    }

    private static List<Tuple<long, long>> ParseInputs(string input)
    {
        List<Tuple<long, long>> parsedRanges = new List<Tuple<long, long>>();

        string[] ranges = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string range in ranges)
        {
            long[] startAndEnd = range.Split('-').Select(r => long.Parse(r)).ToArray();

            parsedRanges.Add(new(startAndEnd[0], startAndEnd[1]));
        }

        return parsedRanges;
    }
}
