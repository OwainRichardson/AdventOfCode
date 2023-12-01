using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2016
{
    public class D_21_2_Scrambler
    {
        public string Execute(string input, string instruction)
        {
            if (instruction.StartsWith("swap position"))
            {
                return SwapPositions(input, instruction);
            }
            else if (instruction.StartsWith("swap letter"))
            {
                return SwapLetters(input, instruction);
            }
            else if (instruction.StartsWith("rotate left"))
            {
                return RotateRight(input, instruction);
            }
            else if (instruction.StartsWith("rotate right"))
            {
                return RotateLeft(input, instruction);
            }
            else if (instruction.StartsWith("rotate based"))
            {
                string tempInput = input;
                bool found = false;
                while (!found)
                {
                    tempInput = RotateAnswer(tempInput);

                    string answer = RotateBased(tempInput, instruction);
                    if (answer.Equals(input))
                    {
                        found = true;
                    }
                }

                return tempInput;
            }
            else if (instruction.StartsWith("reverse positions"))
            {
                return ReversePosition(input, instruction);
            }
            else if (instruction.StartsWith("move position"))
            {
                return MovePosition(input, instruction);
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private string RotateAnswer(string input)
        {
            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < input.Length; index++)
            {
                sb.Append(input[(index + 1) % input.Length]);
            }

            return sb.ToString();
        }

        private string ReversePosition(string input, string instruction)
        {
            string pattern = @"reverse positions (\d+) through (\d+)";
            Match match = Regex.Match(instruction, pattern);

            int firstPosition = int.Parse(match.Groups[1].Value);
            int secondPosition = int.Parse(match.Groups[2].Value);

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < input.Length; index++)
            {
                if (index < firstPosition || index > secondPosition)
                {
                    sb.Append(input[index]);
                }
                else
                {
                    sb.Append(input[secondPosition - (index - firstPosition)]);
                }
            }

            return sb.ToString();
        }

        private string MovePosition(string input, string instruction)
        {
            string pattern = @"move position (\d+) to position (\d+)";
            Match match = Regex.Match(instruction, pattern);

            int positionToMoveTo = int.Parse(match.Groups[1].Value);
            int positionToMove = int.Parse(match.Groups[2].Value);

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < input.Length; index++)
            {
                if (index == positionToMove)
                {
                    continue;
                }
                else
                {
                    sb.Append(input[index]);
                }
            }


            if (positionToMoveTo > sb.Length)
            {
                sb.Append(input[positionToMove]);
            }
            else
            {
                sb.Insert(positionToMoveTo, input[positionToMove]);
            }

            return sb.ToString();
        }

        private string RotateBased(string input, string instruction)
        {
            string pattern = @"rotate based on position of letter (\w+)";
            Match match = Regex.Match(instruction, pattern);

            string rotateCharacter = match.Groups[1].Value;
            int rotateBy = input.IndexOf(rotateCharacter);

            if (rotateBy >= 4)
            {
                rotateBy += 1;
            }

            rotateBy += 1;

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < input.Length; index++)
            {
                int rotateValue = index - rotateBy;
                while (rotateValue < 0)
                {
                    rotateValue += input.Length;
                }

                sb.Append(input[rotateValue % input.Length]);
            }

            return sb.ToString();
        }

        private string RotateLeft(string input, string instruction)
        {
            string pattern = @"rotate right (\d+) steps?";
            Match match = Regex.Match(instruction, pattern);

            int rotateBy = int.Parse(match.Groups[1].Value);

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < input.Length; index++)
            {
                sb.Append(input[(index + rotateBy) % input.Length]);
            }

            return sb.ToString();
        }

        private string RotateRight(string input, string instruction)
        {
            string pattern = @"rotate left (\d+) steps?";
            Match match = Regex.Match(instruction, pattern);

            int rotateBy = int.Parse(match.Groups[1].Value);

            StringBuilder sb = new StringBuilder();

            for (int index = 0; index < input.Length; index++)
            {
                int rotateValue = index - rotateBy;
                while (rotateValue < 0)
                {
                    rotateValue += input.Length;
                }

                sb.Append(input[rotateValue % input.Length]);
            }

            return sb.ToString();
        }

        private string SwapLetters(string input, string instruction)
        {
            string pattern = @"swap letter (\w+) with letter (\w+)";
            Match match = Regex.Match(instruction, pattern);

            int firstPosition = input.IndexOf(match.Groups[1].Value);
            int secondPosition = input.IndexOf(match.Groups[2].Value);

            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < input.Length; index++)
            {
                if (index == firstPosition)
                {
                    sb.Append(input[secondPosition]);
                }
                else if (index == secondPosition)
                {
                    sb.Append(input[firstPosition]);
                }
                else
                {
                    sb.Append(input[index]);
                }
            }

            return sb.ToString();
        }

        private string SwapPositions(string input, string instruction)
        {
            string pattern = @"swap position (\d+) with position (\d+)";
            Match match = Regex.Match(instruction, pattern);

            int firstPosition = int.Parse(match.Groups[1].Value);
            int secondPosition = int.Parse(match.Groups[2].Value);

            StringBuilder sb = new StringBuilder();
            for (int index = 0; index < input.Length; index++)
            {
                if (index == firstPosition)
                {
                    sb.Append(input[secondPosition]);
                }
                else if (index == secondPosition)
                {
                    sb.Append(input[firstPosition]);
                }
                else
                {
                    sb.Append(input[index]);
                }
            }

            return sb.ToString();
        }
    }
}
