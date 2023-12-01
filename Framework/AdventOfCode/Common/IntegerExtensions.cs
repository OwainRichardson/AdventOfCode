namespace AdventOfCode.Common
{
    public static class IntegerExtensions
    {
        public static string ToTwoFigures(this int input)
        {
            string convertedInput = input.ToString();

            while (convertedInput.Length < 2)
            {
                convertedInput = $"0{convertedInput}";
            }

            return convertedInput;
        }
    }
}
