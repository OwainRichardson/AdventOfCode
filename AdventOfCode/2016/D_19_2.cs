using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_19_2
    {
        public static void Execute()
        {
            int numberOfElves = 3014603;
            LinkedList<int> elves = new LinkedList<int>();

            SetupElves(ref elves, numberOfElves);

            PlayGame(elves);
        }

        private static void PlayGame(LinkedList<int> elves)
        {
            int index = 0;

            while (elves.Count > 1)
            {
                int numberOfElves = elves.Count;

                var node = elves.First;

                while (node != null)
                {
                    var nextNode = node.Next;

                    index = GetIndexOfNode(node.Value, elves);

                    var nodeToRemove = CalculateNodeToRemove(elves, index);
                    elves.Remove(nodeToRemove);

                    if (elves.Count == 1)
                    {
                        break;
                    }

                    node = nextNode;
                }
            }

            Console.WriteLine($"Elf with all the presents: {elves.Single()}");
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

        private static int CalculateNodeToRemove(LinkedList<int> elves, int index)
        {
            int indexToRemove = -1;

            if (elves.Count % 2 == 0)
            {
                indexToRemove = ((elves.Count / 2) + index) % elves.Count;
            }
            else
            {
                 indexToRemove = ((elves.Count / 2) + index) % elves.Count;
            }

            return elves.ElementAt(indexToRemove);
        }

        private static void SetupElves(ref LinkedList<int> elves, int numberOfElves)
        {
            for (int e = 1; e <= numberOfElves; e++)
            {
                elves.AddLast(e);
            }
        }
    }
}
