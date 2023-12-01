using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2017
{
    public static class D_24_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day24_full.txt");

            List<Component> components = ParseInputs(inputs);

            List<Bridge> bridges = new List<Bridge>();
            bridges = GenerateBridges(components, 0, null, bridges);
            int maxBridgeLength = bridges.Max(x => x.Ports.Count);
            List<Bridge> longBridges = bridges.Where(x => x.Ports.Count == maxBridgeLength).ToList();

            Console.WriteLine(longBridges.Max(x => x.Ports.Sum()));
        }

        private static List<Bridge> GenerateBridges(List<Component> components, int port, Bridge bridge, List<Bridge> bridges)
        {
            if (components.Any(x => x.Ports.Contains(port)))
            {
                foreach (var component in components.Where(x => x.Ports.Contains(port)))
                {
                    Bridge temp = new Bridge();
                    if (bridge != null)
                    {
                        temp = bridge.DeepCopy();
                    }

                    int indexOfMatching = -1;

                    if (temp == null)
                    {
                        temp = new Bridge();
                        temp.Ports.Add(component.Ports.First(x => x.Equals(port)));
                        temp.Ports.Add(component.Ports.First(x => !x.Equals(port)));
                        indexOfMatching = component.Ports.IndexOf(port);
                    }
                    else
                    {
                        indexOfMatching = component.Ports.IndexOf(port);

                        temp.Ports.Add(component.Ports[indexOfMatching]);
                        temp.Ports.Add(component.Ports[indexOfMatching == 0 ? 1 : 0]);
                    }

                    bridges.Add(temp);

                    GenerateBridges(components.Except(new List<Component> { component }).ToList(), component.Ports[indexOfMatching == 0 ? 1 : 0], temp.DeepCopy(), bridges);
                }
            }

            return bridges;
        }

        private static List<Component> ParseInputs(string[] inputs)
        {
            List<Component> components = new List<Component>();

            foreach (string input in inputs)
            {
                Component component = new Component();
                string[] split = input.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

                component.Ports.Add(int.Parse(split[0]));
                component.Ports.Add(int.Parse(split[1]));

                components.Add(component);
            }

            return components;
        }
    }
}
