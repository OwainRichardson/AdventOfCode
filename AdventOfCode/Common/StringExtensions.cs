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
    }
}
