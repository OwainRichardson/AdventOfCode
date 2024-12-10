namespace AdventOfCode._2024
{
    public static class D_01_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2024\Data\day01.txt").ToArray();

            int similarityScore = 0;

            (List<int> first, List<int> second) = ParseInputsToLists(inputs);

            for (int i = 0; i < first.Count; i++)
            {
                int countInSecond = second.Count(s => s == first[i]);

                similarityScore += (first[i] * countInSecond);
            }

            Console.WriteLine(similarityScore);
        }

        private static (List<int> first, List<int> second) ParseInputsToLists(string[] inputs)
        {
            List<int> first = new List<int>();
            List<int> second = new List<int>();

            foreach (string input in inputs)
            {
                int[] split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();
                first.Add(split[0]);
                second.Add(split[1]);
            }

            return (first, second);
        }
    }
}
