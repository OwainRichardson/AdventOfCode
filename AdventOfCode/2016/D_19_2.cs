using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2016
{
    public static class D_19_2
    {
        public static void Execute()
        {
            int numberOfElves = 3014603;
            int[] elves = new int[numberOfElves];

            SetupElves(ref elves, numberOfElves);

            PlayGame(elves);
        }

        private static void PlayGame(int[] elves)
        {
            int index = 0;

            while (elves.Length > 1)
            {
                Console.Write($"\r{elves.Length}");

                var nodeToRemove = CalculateNodeToRemove(elves, index % elves.Length);

                elves = elves.RemoveAt(nodeToRemove);

                if (nodeToRemove > index)
                {
                    index++;
                }
            }

            Console.Write($"\rElf with all the presents: {elves.Single()}");
            Console.WriteLine();
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }

        private static int GetIndexOfNode(int value, LinkedList<int> elves)
        {
            int index = 0;

            foreach (var elf in elves)
            {
                if (elf == value)
                {
                    break;
                }

                index++;
            }

            return index;
        }

        private static int CalculateNodeToRemove(int[] elves, int index)
        {
            int indexToRemove = -1;

            if (elves.Length % 2 == 0)
            {
                indexToRemove = ((elves.Length/ 2) + index) % elves.Length;
            }
            else
            {
                indexToRemove = ((elves.Length / 2) + index) % elves.Length;
            }

            return indexToRemove;
        }

        private static void SetupElves(ref int[] elves, int numberOfElves)
        {
            for (int e = 0; e < numberOfElves; e++)
            {
                elves[e] = e + 1;
            }
        }
    }
}
