namespace AdventOfCode._2025;

public static class D_02_2
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
        string productNumber = productId.ToString();

        int productIdLength = productNumber.Length;
        if (productIdLength <= 1) return false;

        if (productNumber.All(x => x == productNumber[0]))
        {
            return true;
        }

        List<int> divisors = GetDivisors(productIdLength);
        divisors.RemoveAt(0);
        divisors.RemoveAt(divisors.Count - 1);

        foreach (int divisor in divisors)
        {
            HashSet<string> chunks = new HashSet<string>();

            for (int i = 0; i < productNumber.Length / divisor; i++)
            {
                chunks.Add(productNumber.Substring(i * divisor, divisor));
            }

            if (chunks.All(c => c.Equals(chunks.First())))
            {
                return true;
            }    
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

    private static List<int> GetDivisors(int n)
    {
        var divisors = new List<int>();

        for (int i = 1; i * i <= n; i++)
        {
            if (n % i == 0)
            {
                divisors.Add(i);
                if (i != n / i)
                    divisors.Add(n / i);
            }
        }

        divisors.Sort();
        return divisors;
    }
}
