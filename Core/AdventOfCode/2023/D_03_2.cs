using AdventOfCode._2023.Models;

namespace AdventOfCode._2023
{
    public static class D_03_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day03.txt").ToArray();

            List<EnginePart> engineParts = ParseInputsToEngineParts(inputs);

            long total = 0;

            foreach (EnginePart part in engineParts.Where(enginePart => enginePart.IsSymbol && enginePart.Value == "*"))
            {
                List<EnginePart> adjacentParts = engineParts.Where(enginePart => !enginePart.IsSymbol
                                                            && (enginePart.Y >= part.Y - 1 && enginePart.Y <= part.Y + 1)
                                                            && (enginePart.EndX >= part.StartX - 1 && enginePart.StartX <= part.EndX + 1)
                ).ToList();

                if (adjacentParts.Count == 2)
                {
                    total += (int.Parse(adjacentParts[0].Value) * int.Parse(adjacentParts[1].Value));
                }
            }

            Console.Write(total);
        }

        private static List<EnginePart> ParseInputsToEngineParts(string[] inputs)
        {
            List<EnginePart> engineParts = new List<EnginePart>();

            int y = 0;
            foreach (string input in inputs)
            {
                int x = 0;
                string value = string.Empty;

                foreach (char c in input)
                {
                    if (!char.IsDigit(c))
                    {
                        if (!string.IsNullOrEmpty(value))
                        {
                            EnginePart enginePart = new EnginePart
                            {
                                StartX = x - value.Length,
                                EndX = x - 1,
                                IsSymbol = false,
                                Y = y,
                                Value = value.ToString()
                            };
                            engineParts.Add(enginePart);
                            value = string.Empty;
                        }
                    }
                    
                    if (c != '.')
                    {
                        if (!char.IsDigit(c))
                        {
                            EnginePart enginePart = new EnginePart
                            {
                                StartX = x,
                                EndX = x,
                                IsSymbol = true,
                                Y = y,
                                Value = c.ToString()
                            };
                            engineParts.Add(enginePart);
                            value = string.Empty;
                        }
                        else
                        {
                            value = $"{value}{c.ToString()}";
                        }
                    }

                    x++;
                }

                if (!string.IsNullOrWhiteSpace(value))
                {
                    EnginePart lineEndEnginePart = new EnginePart
                    {
                        StartX = x - value.Length,
                        EndX = x,
                        IsSymbol = !int.TryParse(value, out int _),
                        Y = y,
                        Value = value
                    };
                    engineParts.Add(lineEndEnginePart);
                    value = string.Empty;
                }

                y++;
            }

            return engineParts;
        }
    }
}