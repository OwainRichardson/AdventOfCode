

using AdventOfCode._2025.Models;

namespace AdventOfCode._2025;

public static class D_11_2
{
    static List<string> Paths = new();

    public static void Execute()
    {
        string[] inputs = File.ReadAllLines(@"2025\Data\day11.txt").ToArray();

        List<Device> devices = ParseInputs(inputs);
        List<Device> startDevices = devices.Where(d => d.Id == "fft" || d.Id == "dac").ToList();

        foreach (Device startDevice in startDevices)
        {
            CalculatePaths(devices, startDevice, startDevice.Id);
        }

        Console.WriteLine(Paths.Count(p => p.Contains("fft") && p.Contains("dac")));
    }

    private static void CalculatePaths(List<Device> devices, Device currentDevice, string path = "svr")
    {
        foreach (string output in currentDevice.Outputs)
        {
            if (output == "out")
            {
                if (path.Contains("fft") && path.Contains("dac"))
                {
                    Paths.Add($"{path}-{output}");
                }
                return;
            }

            if (path.Contains(output))
            {
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