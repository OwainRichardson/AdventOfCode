using AdventOfCode._2023.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023
{
    public static class D_19_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2023\Data\day19.txt").ToArray();

            (List<Workflow> workflows, List<Part> parts) = ParseInputs(inputs);

            Workflow firstWorkflow = workflows.Single(w => w.Id == "in");

            foreach (Part part in parts)
            {
                Workflow workflow = firstWorkflow;

                while (part.Status != "A" && part.Status != "R")
                {
                    foreach (string rule in workflow.Rules)
                    {
                        string[] split = rule.Split(':').ToArray();

                        string rulePart = split[0].Contains("<") || split[0].Contains(">") ? split[0].Replace("x", part.X.ToString()).Replace("m", part.M.ToString()).Replace("a", part.A.ToString()).Replace("s", part.S.ToString()) : split[0];
                        if (rulePart.Contains("<"))
                        {
                            int[] valueSplit = rulePart.Split('<').Select(x => int.Parse(x)).ToArray();
                            if (valueSplit[0] < valueSplit[1])
                            {
                                if (split[1] == "A" || split[1] == "R")
                                {
                                    part.Status = split[1];
                                    break;
                                }

                                workflow = workflows.Single(w => w.Id == split[1]);
                                break;
                            }
                        }
                        else if (rulePart.Contains(">"))
                        {
                            int[] valueSplit = rulePart.Split('>').Select(x => int.Parse(x)).ToArray();
                            if (valueSplit[0] > valueSplit[1])
                            {
                                if (split[1] == "A" || split[1] == "R")
                                {
                                    part.Status = split[1];
                                    break;
                                }

                                workflow = workflows.Single(w => w.Id == split[1]);
                                break;
                            }
                        }
                        else
                        {
                            if (rulePart == "A" || rulePart == "R")
                            {
                                part.Status = rulePart;
                                break;
                            }

                            workflow = workflows.Single(w => w.Id == rulePart);
                            break;
                        }
                    }
                }
            }

            var acceptedParts = parts.Where(p => p.Status == "A").ToList();

            Console.WriteLine(acceptedParts.Sum(a => a.Rating));
        }

        private static (List<Workflow> workflows, List<Part> parts) ParseInputs(string[] inputs)
        {
            List<Workflow> workflows = new List<Workflow>();
            List<Part> parts = new List<Part>();

            bool isParts = false;
            for (int index = 0; index < inputs.Length; index++)
            {
                if (string.IsNullOrWhiteSpace(inputs[index]))
                {
                    isParts = true;
                    continue;
                }

                if (!isParts)
                {
                    Workflow workflow = new Workflow
                    {
                        Id = inputs[index].Substring(0, inputs[index].IndexOf("{")),
                        Rules = inputs[index].Substring(inputs[index].IndexOf("{") + 1, inputs[index].IndexOf("}") - inputs[index].IndexOf("{") - 1).Split(',').ToList()
                    };

                    workflows.Add(workflow);
                    continue;
                }

                string pattern = @"^\{x=(\d+),m=(\d+),a=(\d+),s=(\d+)\}$";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(inputs[index]);

                Part part = new Part
                {
                    X = int.Parse(match.Groups[1].Value),
                    M = int.Parse(match.Groups[2].Value),
                    A = int.Parse(match.Groups[3].Value),
                    S = int.Parse(match.Groups[4].Value)
                };
                parts.Add(part);
            }

            return (workflows, parts);
        }
    }
}