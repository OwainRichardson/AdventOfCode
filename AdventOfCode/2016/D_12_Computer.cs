using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    public static class D_12_Computer
    {
        public static void Execute(string[] inputs, ref Dictionary<string, int> registers)
        {
            int index = 0;

            while (index < inputs.Length)
            {
                var input = inputs[index];

                if (input.StartsWith("cpy"))
                {
                    string pattern = @"cpy (\w+) (\w+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);

                    string register = match.Groups[2].Value;
                    string value = match.Groups[1].Value;
                    int parsedValue = 0;
                    if (int.TryParse(value, out parsedValue))
                    {
                        registers[register] = parsedValue;
                    }
                    else
                    {
                        registers[register] = registers[value];
                    }
                    index++;
                }
                else if (input.StartsWith("inc"))
                {
                    string pattern = @"inc (\w)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);

                    string register = match.Groups[1].Value;
                    registers[register] += 1;
                    index++;
                }
                else if (input.StartsWith("dec"))
                {
                    string pattern = @"dec (\w)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);

                    string register = match.Groups[1].Value;
                    registers[register] -= 1;
                    index++;
                }
                else if (input.StartsWith("jnz"))
                {
                    string pattern = @"jnz (\w+) (-?\d+)";
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);

                    string register = match.Groups[1].Value;
                    int jump = int.Parse(match.Groups[2].Value);

                    if (registers.ContainsKey(register) && registers[register] != 0)
                    {
                        index += jump;
                    }
                    else
                    {
                        int parsedValue = 0;

                        if (int.TryParse(register, out parsedValue))
                        {
                            if (parsedValue != 0)
                            {
                                index += jump;
                            }
                        }
                        else
                        {
                            index++;
                        }
                    }
                }
            }
        }
    }
}
