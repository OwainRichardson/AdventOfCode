

using AdventOfCode._2025.Models;

namespace AdventOfCode._2025;

public static class D_11_1
{
    static List<string> Paths = new();

    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day11.txt").ToArray();

        List<Device> devices = ParseInputs(inputs);
        Device startDevice = devices.Single(d => d.Id == "you");

        CalculatePaths(devices, startDevice);

        Console.WriteLine(Paths.Count);
    }

    private static void CalculatePaths(List<Device> devices, Device currentDevice, string path = "you")
    {
        foreach (string output in currentDevice.Outputs)
        {
            if (output == "out")
            {
                Paths.Add($"{path}-{output}");
                return;
            }

            Device nextDevice = devices.Single(d => d.Id == output);
            CalculatePaths(devices, nextDevice, $"{path}-{output}");
        }
    }

    private static List<Device> ParseInputs(string[] inputs)
    {
        List<Device> devices = new();

        foreach (string input in inputs)
        {
            string[] idSplit = input.Split(':', StringSplitOptions.RemoveEmptyEntries);

            Device device = new()
            {
                Id = idSplit[0],
                Outputs = idSplit[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()
            };

            devices.Add(device);
        }

        return devices;
    }
}