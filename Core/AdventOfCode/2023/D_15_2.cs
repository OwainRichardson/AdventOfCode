using AdventOfCode._2023.Models;
using System.Text;

namespace AdventOfCode._2023
{
    public static class D_15_2
    {
        public static void Execute()
        {
            string inputs = File.ReadAllLines(@"2023\Data\day15.txt")[0];

            List<string> splitInputs = inputs.Split(',').ToList();
            List<LensBox> boxes = new List<LensBox>();

            foreach (string split in splitInputs)
            {
                if (split.Contains('='))
                {
                    string[] splitSplit = split.Split('=');
                    string label = splitSplit[0];
                    int focalLength = int.Parse(splitSplit[1]);

                    int boxNumber = HashAlgorithm(split.Substring(0, split.IndexOf('=')));
                    if (!boxes.Exists(b => b.Id == boxNumber))
                    {
                        boxes.Add(new LensBox { Id = boxNumber });
                    }

                    LensBox box = boxes.First(b => b.Id == boxNumber);

                    if (box.Lenses.Exists(l => l.Label == label))
                    {
                        Lens lens = box.Lenses.First(l => l.Label == label);
                        lens.FocalLength = focalLength;
                    }
                    else
                    {
                        box.Lenses.Add(new Lens { Label = label, FocalLength = focalLength });
                    }
                }
                else if (split.Contains('-'))
                {
                    int boxNumber = HashAlgorithm(split.Replace("-", ""));

                    if (!boxes.Exists(b => b.Id == boxNumber))
                    {
                        boxes.Add(new LensBox { Id = boxNumber });
                    }

                    LensBox box = boxes.First(b => b.Id == boxNumber);

                    if (box.Lenses.Exists(l => l.Label == split.Replace("-", "")))
                    {
                        Lens lens = box.Lenses.First(l => l.Label == split.Replace("-", ""));
                        box.Lenses.Remove(lens);
                    }
                }
                else
                    throw new InvalidOperationException();
            }

            long total = 0;

            foreach (LensBox box in boxes)
            {
                foreach (Lens lens in box.Lenses)
                {
                    total += (box.Id + 1) * lens.FocalLength * (box.Lenses.IndexOf(lens) + 1);
                }
            }

            Console.WriteLine(total);
        }

        private static int HashAlgorithm(string split)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(split);
            int subtotal = 0;

            foreach (var b in asciiBytes)
            {
                subtotal += b;
                subtotal *= 17;
                subtotal = subtotal % 256;
            }

            return subtotal;
        }
    }
}