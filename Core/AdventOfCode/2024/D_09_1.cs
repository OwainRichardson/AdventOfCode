namespace AdventOfCode._2024
{
    public static class D_09_1
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"2024\Data\day09.txt")[0];

            List<long> memoryBlocks = CreateMemoryBlocks(input);

            CondenseMemory(memoryBlocks);

            long total = 0;
            for (int index = 0; index < memoryBlocks.Count; index++)
            {
                if (memoryBlocks[index] == -1) break;

                total += (index * memoryBlocks[index]);
            }
            Console.WriteLine(total);
        }

        private static void CondenseMemory(List<long> memoryBlocks)
        {
            int indexOfFirstDot = memoryBlocks.IndexOf(-1);
            long lastValue = memoryBlocks.Last(m => m != -1);
            int lastIndexOfValue = memoryBlocks.LastIndexOf(lastValue);

            while (indexOfFirstDot < lastIndexOfValue)
            {
                memoryBlocks[indexOfFirstDot] = lastValue;
                memoryBlocks.RemoveAt(lastIndexOfValue);

                indexOfFirstDot = memoryBlocks.IndexOf(-1);
                lastValue = memoryBlocks.Last(m => m != -1);
                lastIndexOfValue = memoryBlocks.LastIndexOf(lastValue);
            }
        }

        private static List<long> CreateMemoryBlocks(string input)
        {
            int number = 0;
            List<long> memoryBlocks = new List<long>();

            for (int index = 0; index < input.Length; index += 2)
            {
                int numberOfBlocks = int.Parse(input[index].ToString());
                for (int n = 1; n <= numberOfBlocks; n++)
                {
                    memoryBlocks.Add(number);
                }

                if (index + 1 < input.Length)
                {
                    int gap = int.Parse(input[index + 1].ToString());
                    if (gap > 0)
                    {
                        for (int g = 1; g <= gap; g++)
                        {
                            memoryBlocks.Add(-1);
                        }
                    }
                }

                number++;
            }

            return memoryBlocks;
        }
    }
}