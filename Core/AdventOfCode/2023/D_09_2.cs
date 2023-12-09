using AdventOfCode._2023.Models;
using AdventOfCode._2023.Models.Enums;
using System.Data.Common;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_09_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day09.txt").ToArray();

            List<Sequence> sequences = ParseInputsToSequences(inputs);
            long total = 0;

            foreach (Sequence sequence in sequences)
            {
                FindPattern(sequence, sequences);
                total += FindPreviousValue(sequence);
            }

            Console.WriteLine(total);
        }

        private static long FindPreviousValue(Sequence sequence)
        {
            int lengthOfParent = sequence.SequenceValues.Count;

            Sequence currentSequence = sequence;

            while (sequence.SequenceValues.Count == lengthOfParent)
            {
                if (currentSequence.ChildSequence != null)
                {
                    if (currentSequence.SequenceValues.Count == currentSequence.ChildSequence.SequenceValues.Count)
                    {
                        currentSequence.SequenceValues = currentSequence.SequenceValues.Prepend(currentSequence.SequenceValues.First() - currentSequence.ChildSequence.SequenceValues.First()).ToList();
                        currentSequence = sequence;
                    }
                    else
                    {
                        currentSequence = currentSequence.ChildSequence;
                        continue;
                    }
                }

                if (currentSequence.ChildSequence == null)
                {
                    currentSequence.SequenceValues = currentSequence.SequenceValues.Prepend(0).ToList();
                    currentSequence = sequence;
                }
            }

            return sequence.SequenceValues.First();
        }

        private static void FindPattern(Sequence sequence, List<Sequence> sequences)
        {
            if (sequence.SequenceValues.All(s => s == 0))
            {
                return;
            }

            sequence.ChildSequence = new Sequence
            {
                Id = sequences.Max(s => s.Id) + 1,
                SequenceValues = new List<int>()
            };

            for (int index = 1; index < sequence.SequenceValues.Count; index++)
            {
                sequence.ChildSequence.SequenceValues.Add(sequence.SequenceValues[index] - sequence.SequenceValues[index - 1]);
            }

            FindPattern(sequence.ChildSequence, sequences);
        }

        private static List<Sequence> ParseInputsToSequences(string[] inputs)
        {
            List<Sequence> sequences = new List<Sequence>();
            int index = 1;

            foreach (string input in inputs)
            {
                List<int> values = input.Split(' ').Select(i => int.Parse(i)).ToList();

                Sequence sequence = new Sequence
                {
                    Id = index,
                    SequenceValues = values
                };

                index++;

                sequences.Add(sequence);
            }

            return sequences;
        }
    }
}