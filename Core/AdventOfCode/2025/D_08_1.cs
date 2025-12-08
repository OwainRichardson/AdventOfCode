

using AdventOfCode._2025.Models;

namespace AdventOfCode._2025;

public static class D_08_1
{
    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day08.txt").ToArray();

        List<JunctionBox> junctionBoxes = ParseInputs(inputs);

        Dictionary<int, List<int>> circuits = new();
        double lastMinDistance = 0;

        for (int iteration = 1; iteration <= 1000; iteration++)
        {
            double minDistance = junctionBoxes.SelectMany(jb => jb.ConnectedTo.Values).Select(jb => jb).Where(jb => jb > lastMinDistance).Min();

            JunctionBox boxToConnect1 = junctionBoxes.First(jb => jb.ConnectedTo.Values.Contains(minDistance));

            int box2Id = boxToConnect1.ConnectedTo.First(ct => ct.Value.Equals(minDistance)).Key;
            JunctionBox boxToConnect2 = junctionBoxes.First(jb => jb.Id == box2Id);
            

            CombineCircuits(boxToConnect1, boxToConnect2, circuits);

            lastMinDistance = minDistance;
        }

        List<int> values = circuits.Select(c => c.Value.Count).OrderByDescending(c => c).ToList();

        int numberOfBoxesToMultiply = 3;

        long total = 1;

        for (int index = 0; index < numberOfBoxesToMultiply; index++)
        {
            total *= values[index];
        }

        Console.WriteLine(total);
    }

    private static bool BoxesAreConnected(JunctionBox box1, JunctionBox box2, Dictionary<int, List<int>> circuits)
    {
        var box1Circuit = circuits.SingleOrDefault(c => c.Value.Contains(box1.Id));

        if (box1Circuit.Value == null)
        {
            return false;
        }

        return box1Circuit.Value.Contains(box2.Id);
    }

    private static void CombineCircuits(JunctionBox boxToConnect1, JunctionBox boxToConnect2, Dictionary<int, List<int>> circuits)
    {
        if (BoxesAreConnected(boxToConnect1, boxToConnect2, circuits)) return;

        var box1Circuit = circuits.SingleOrDefault(c => c.Value.Contains(boxToConnect1.Id));
        var box2Circuit = circuits.SingleOrDefault(c => c.Value.Contains(boxToConnect2.Id));

        int maxCircuitId = circuits.Any() ? circuits.Keys.Max() : 0;

        if (box1Circuit.Value == null && box2Circuit.Value == null)
        {
            circuits.Add(maxCircuitId + 1, [boxToConnect1.Id, boxToConnect2.Id]);
        }

        if (box1Circuit.Value != null && box2Circuit.Value == null)
        {
            box1Circuit.Value.Add(boxToConnect2.Id);
        }

        if (box1Circuit.Value == null && box2Circuit.Value != null)
        {
            box2Circuit.Value.Add(boxToConnect1.Id);
        }

        if (box1Circuit.Value != null && box2Circuit.Value != null)
        {
            box1Circuit.Value.AddRange(box2Circuit.Value);

            circuits.Remove(box2Circuit.Key);
        }
    }

    private static List<JunctionBox> ParseInputs(string[] inputs)
    {
        List<JunctionBox> junctionBoxes = new();
        int currentId = 1;

        foreach (string input in inputs)
        {
            string[] inputSplit = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

            JunctionBox junctionBox = new()
            {
                Id = currentId,
                X = int.Parse(inputSplit[0]),
                Y = int.Parse(inputSplit[1]),
                Z = int.Parse(inputSplit[2])
            };

            foreach (JunctionBox box in junctionBoxes)
            {
                junctionBox.ConnectedTo.Add(box.Id, CalculateDistance(junctionBox, box));
            }

            junctionBoxes.Add(junctionBox);

            currentId++;
        }

        return junctionBoxes;
    }

    private static double CalculateDistance(JunctionBox junctionBox1, JunctionBox junctionBox2)
    {
        return Math.Sqrt(Math.Pow(junctionBox1.X - junctionBox2.X, 2) + Math.Pow(junctionBox1.Y - junctionBox2.Y, 2) + Math.Pow(junctionBox1.Z - junctionBox2.Z, 2));
    }
}