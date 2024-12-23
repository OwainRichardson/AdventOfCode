using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public static class D_17_2
    {
        public static string Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day17.txt");

            int registerA = 766369050;
            int max = 1500000000;

            while (registerA <= max)
            {
                (Dictionary<string, long> registers, string instructions) = ParseInputs(inputs);
                int[] ops = instructions.Split(',').Select(i => int.Parse(i)).ToArray();

                registers["A"] = registerA;

                string output = RunProgram(registers, ops);

                if (output == instructions) break;

                registerA++;
            }

            return registerA.ToString();
        }

        private static string RunProgram(Dictionary<string, long> registers, int[] ops)
        {
            StringBuilder values = new StringBuilder();

            int index = 0;
            while (index < ops.Length)
            {
                switch (ops[index])
                {
                    case 0:
                        long numerator1 = registers["A"];
                        long denominator1 = (long)Math.Pow(2, GetComboOperand(ops[index + 1], registers));

                        registers["A"] = numerator1 / denominator1;

                        index += 2;
                        break;
                    case 1:
                        registers["B"] = registers["B"] ^ ops[index + 1];

                        index += 2;
                        break;
                    case 2:
                        long comboValue2 = (long)GetComboOperand(ops[index + 1], registers);
                        registers["B"] = comboValue2 % 8;

                        index += 2;
                        break;
                    case 3:
                        if (registers["A"] == 0)
                        {
                            index += 2;
                        }
                        else
                        {
                            index = ops[index + 1];
                        }
                        break;
                    case 4:
                        registers["B"] = registers["B"] ^ registers["C"];

                        index += 2;
                        break;
                    case 5:
                        long comboValue5 = (long)GetComboOperand(ops[index + 1], registers);
                        values.Append($"{comboValue5 % 8},");

                        index += 2;
                        break;
                    case 6:
                        long numerator6 = registers["A"];
                        long denominator6 = (long)Math.Pow(2, GetComboOperand(ops[index + 1], registers));

                        registers["B"] = numerator6 / denominator6;

                        index += 2;
                        break;
                    case 7:
                        long numerator7 = registers["A"];
                        long denominator7 = (long)Math.Pow(2, GetComboOperand(ops[index + 1], registers));

                        registers["C"] = numerator7 / denominator7;

                        index += 2;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return values.ToString().TrimEnd(',');
        }

        private static double GetComboOperand(int opValue, Dictionary<string, long> registers)
        {
            if (opValue <= 3)
            {
                return opValue;
            }

            if (opValue == 4)
            {
                return registers["A"];
            }

            if (opValue == 5)
            {
                return registers["B"];
            }

            if (opValue == 6)
            {
                return registers["C"];
            }

            throw new InvalidOperationException();
        }

        private static (Dictionary<string, long> registers, string instructions) ParseInputs(string[] inputs)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            string registerPattern = @"^Register\W(\D{1}):\W(\d+)$";
            Regex registerRegex = new Regex(registerPattern);

            string programPattern = @"^Program:\W(.+)$";
            Regex programRegex = new Regex(programPattern);

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input)) continue;

                if (registerRegex.IsMatch(input))
                {
                    Match match = registerRegex.Match(input);
                    registers.Add(match.Groups[1].Value, long.Parse(match.Groups[2].Value));
                }

                if (programRegex.IsMatch(input))
                {
                    return (registers, programRegex.Match(input).Groups[1].Value);
                }
            }

            throw new InvalidOperationException();
        }
    }
}