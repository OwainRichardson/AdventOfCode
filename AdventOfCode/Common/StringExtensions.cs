namespace AdventOfCode.Common
{
    public static class StringExtensions
    {
        public static int ToInt(this string input)
        {
            int parsedInteger;

            if (int.TryParse(input, out parsedInteger))
            {
                return parsedInteger;
            }

            return -999;
        }

        public static bool ContainsANumber(this string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
