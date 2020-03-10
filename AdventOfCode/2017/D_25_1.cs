using AdventOfCode._2017.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2017
{
    public static class D_25_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2017\Data\day25_full.txt");

            string startState = string.Empty;
            long steps = 0;
            List<State> states = ParseInputs(inputs, out startState, out steps);

            RunTuringMachine(states, steps, startState);
        }

        private static void RunTuringMachine(List<State> states, long steps, string startState)
        {
            int position = 0;
            string currentState = startState;
            Dictionary<int, int> tape = new Dictionary<int, int>();
            tape.Add(0, 0);

            for (int step = 1; step <= steps; step++)
            {
                if (!tape.ContainsKey(position))
                {
                    tape.Add(position, 0);
                }

                State state = states.First(x => x.Id == currentState);
                Instruction instruction = state.Instructions.First(x => x.Value == tape[position]);

                tape[position] = instruction.Write;
                position += instruction.Move == ScannerDirections.Left ? -1 : 1;
                currentState = instruction.NewState;
            }

            Console.WriteLine(tape.Sum(x => x.Value));
        }

        private static List<State> ParseInputs(string[] inputs, out string startState, out long steps)
        {
            List<State> states = new List<State>();
            string beginStatePattern = @"Begin in state (\w+).";
            string stepsPattern = @"Perform a diagnostic checksum after (\d+) steps.";
            string statePattern = @"In state (\w+):";
            string currentValuePattern = @"If the current value is (\d+):";
            string writeValuePattern = @"- Write the value (\d+).";
            string moveDirectionPattern = @"- Move one slot to the (\w+).";
            string continueStatePattern = @"- Continue with state (\w+).";
            startState = "";
            steps = 0;

            for (int index = 0; index < inputs.Length;)
            {
                if (Regex.Match(inputs[index], beginStatePattern).Success)
                {
                    Match match = Regex.Match(inputs[index], beginStatePattern);
                    startState = match.Groups[1].Value;
                    index++;
                }
                else if (Regex.Match(inputs[index], stepsPattern).Success)
                {
                    Match match = Regex.Match(inputs[index], stepsPattern);
                    steps = long.Parse(match.Groups[1].Value);
                    index++;
                }
                else if (Regex.Match(inputs[index], statePattern).Success)
                {
                    State state = new State();

                    Match match = Regex.Match(inputs[index], statePattern);
                    state.Id = match.Groups[1].Value;

                    index++;

                    for (int i = 1; i <= 2; i++)
                    {
                        match = Regex.Match(inputs[index], currentValuePattern);
                        Instruction instruction = new Instruction();
                        instruction.Value = int.Parse(match.Groups[1].Value);

                        index++;

                        match = Regex.Match(inputs[index], writeValuePattern);
                        instruction.Write = int.Parse(match.Groups[1].Value);

                        index++;

                        match = Regex.Match(inputs[index], moveDirectionPattern);
                        instruction.Move = match.Groups[1].Value == "left" ? ScannerDirections.Left : ScannerDirections.Right;

                        index++;

                        match = Regex.Match(inputs[index], continueStatePattern);
                        instruction.NewState = match.Groups[1].Value;

                        index++;

                        state.Instructions.Add(instruction);
                    }

                    states.Add(state);

                }
                else
                {
                    index++;
                }
            }

            return states;
        }
    }
}
