namespace AdventOfCode._2024
{
    public static class D_09_2
    {
        public static void Execute()
        {
            string input = File.ReadAllLines(@"2024\Data\day09.txt")[0];

            List<long> memoryBlocks = CreateMemoryBlocks(input);

            CondenseMemory(memoryBlocks);

            long total = 0;
            for (int index = 0; index < memoryBlocks.Count; index++)
            {
                if (memoryBlocks[index] == -1) continue;

                total += index * memoryBlocks[index];
            }
            Console.WriteLine(total);
        }

        private static void CondenseMemory(List<long> memoryBlocks)
        {
            long valueToMove = memoryBlocks.Last(m => m != -1);

            while (valueToMove > 0)
            {
                int firstIndexOfValue = memoryBlocks.IndexOf(valueToMove);
                int lastIndexOfValue = memoryBlocks.LastIndexOf(valueToMove);
                int lengthOfBlock = lastIndexOfValue - firstIndexOfValue + 1;

                int indexToMoveTo = FindGap(memoryBlocks, lengthOfBlock, firstIndexOfValue);

                if (indexToMoveTo > -1)
                {
                    for (int i = 0; i < lengthOfBlock; i++)
                    {
                        memoryBlocks[indexToMoveTo + i] = valueToMove;
                    }

                    for (int i = 0; i < lengthOfBlock; i++)
                    {
                        memoryBlocks[firstIndexOfValue + i] = -1;
                    }
                }

                long lastValue = memoryBlocks.Last();
                while (lastValue == -1)
                {
                    memoryBlocks.RemoveAt(memoryBlocks.Count - 1);
                    lastValue = memoryBlocks.Last();
                }

                valueToMove--;
            }
        }

        private static int FindGap(List<long> memoryBlocks, int lengthOfBlock, int firstIndexOfValue)
        {
            int startIndexOfGap = -1;

            for (int i = 0; i < firstIndexOfValue; i++)
            {
                List<int> indexesToTest = new List<int>();
                for (int j = i; j < i + lengthOfBlock; j++)
                {
                    indexesToTest.Add(j);
                }

                var memoryIndexes = indexesToTest.Select(index => memoryBlocks[index]).ToList();
                if (memoryIndexes.All(mi => mi == -1))
                {
                    startIndexOfGap = i; 
                    break;
                }
            }

            return startIndexOfGap;
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