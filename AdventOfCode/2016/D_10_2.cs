using AdventOfCode._2016.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode._2016
{
    public static class D_10_2
    {
        public static void Execute()
        {
            List<Bot> bots = new List<Bot>();
            var inputs = File.ReadAllLines(@"C:\Work\Misc Projects\AdventOfCode\AdventOfCode\AdventOfCode\2016\Data\day10_full.txt");

            foreach (var input in inputs.Where(x => x.StartsWith("value")))
            {
                AssignValuesToBots(input, ref bots);
            }

            while (bots.Any(x => x.Chip1 != 0 && x.Chip2 != 0))
            {
                Bot botWith2Chips = bots.First(x => x.Chip1 > 0 && x.Chip2 != 0);
                string instruction = inputs.First(x => x.StartsWith($"bot {botWith2Chips.Id} "));

                PerformActions(instruction, ref bots);
            }

            var outputs = bots.Where(x => x.Type == "output" && (x.Id == 0 || x.Id == 1 || x.Id == 2));

            int answer = 1;

            foreach (Bot output in outputs)
            {
                answer *= (output.Chip1 + output.Chip2);
            }

            Console.WriteLine(answer);
        }

        private static void PerformActions(string input, ref List<Bot> bots)
        {
            string botIdPattern = @"bot (\d+)";
            Regex botIdRegex = new Regex(botIdPattern);
            int botId = int.Parse(botIdRegex.Match(input, 0).Groups[1].Value);

            Bot bot = bots.FirstOrDefault(x => x.Id == botId && x.Type == "bot");
            if (bot != null)
            {
                string actionPattern = @".* gives (\w+) to (bot|output) (\d+) and (\w+) to (bot|output) (\d+)";
                Regex actionRegex = new Regex(actionPattern);
                Match match = actionRegex.Match(input);

                string firstAction = match.Groups[1].Value;
                string firstBotOrOutput = match.Groups[2].Value;
                int firstBot = int.Parse(match.Groups[3].Value);

                string secondAction = match.Groups[4].Value;
                string secondBotOrOutput = match.Groups[5].Value;
                int secondBot = int.Parse(match.Groups[6].Value);

                PerformSwap(firstAction, firstBot, firstBotOrOutput, secondBot, secondBotOrOutput, secondBot, botId, ref bots);
            }
            else
            {
                throw new ArgumentNullException($"Bot {botId} not found");
            }
        }

        private static void PerformSwap(string firstAction, int firstReceivingBotId, string firstBotOrOutput, int secondReceivingBotId, string secondBotOrOutput, int secondBot, int botId, ref List<Bot> bots)
        {
            Bot givingBot = bots.First(x => x.Id == botId && x.Type == "bot");
            Bot firstReceivingBot = bots.FirstOrDefault(x => x.Id == firstReceivingBotId && x.Type == firstBotOrOutput);
            if (firstReceivingBot == null)
            {
                firstReceivingBot = new Bot
                {
                    Id = firstReceivingBotId,
                    Type = firstBotOrOutput
                };
                bots.Add(firstReceivingBot);
            }
            Bot secondReceivingBot = bots.FirstOrDefault(x => x.Id == secondReceivingBotId && x.Type == secondBotOrOutput);
            if (secondReceivingBot == null)
            {
                secondReceivingBot = new Bot
                {
                    Id = secondReceivingBotId,
                    Type = secondBotOrOutput
                };
                bots.Add(secondReceivingBot);
            }

            if (firstAction == "high")
            {
                if (givingBot.Chip1 > givingBot.Chip2)
                {
                    if (firstReceivingBot.Chip1 == 0)
                    {
                        firstReceivingBot.Chip1 = givingBot.Chip1;
                        givingBot.Chip1 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip2;
                            givingBot.Chip2 = 0;
                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip2;
                            givingBot.Chip2 = 0;
                        }
                    }
                    else if (firstReceivingBot.Chip2 == 0)
                    {
                        firstReceivingBot.Chip2 = givingBot.Chip1;
                        givingBot.Chip1 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip2;
                            givingBot.Chip2 = 0;

                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip2;
                            givingBot.Chip2 = 0;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Bot {firstReceivingBotId} already has 2 chips");
                    }
                }
                else
                {
                    if (firstReceivingBot.Chip1 == 0)
                    {
                        firstReceivingBot.Chip1 = givingBot.Chip2;
                        givingBot.Chip2 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip1;
                            givingBot.Chip1 = 0;

                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip1;
                            givingBot.Chip1 = 0;
                        }
                    }
                    else if (firstReceivingBot.Chip2 == 0)
                    {
                        firstReceivingBot.Chip2 = givingBot.Chip2;
                        givingBot.Chip2 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip1;
                            givingBot.Chip1 = 0;

                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip1;
                            givingBot.Chip1 = 0;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Bot already has 2 chips");
                    }
                }
            }
            else if (firstAction == "low")
            {
                if (givingBot.Chip1 > givingBot.Chip2)
                {
                    if (firstReceivingBot.Chip1 == 0)
                    {
                        firstReceivingBot.Chip1 = givingBot.Chip2;
                        givingBot.Chip2 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip1;
                            givingBot.Chip1 = 0;

                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip1;
                            givingBot.Chip1 = 0;
                        }
                    }
                    else if (firstReceivingBot.Chip2 == 0)
                    {
                        firstReceivingBot.Chip2 = givingBot.Chip2;
                        givingBot.Chip2 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip1;
                            givingBot.Chip1 = 0;

                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip1;
                            givingBot.Chip1 = 0;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Bot already has 2 chips");
                    }
                }
                else
                {
                    if (firstReceivingBot.Chip1 == 0)
                    {
                        firstReceivingBot.Chip1 = givingBot.Chip1;
                        givingBot.Chip1 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip2;
                            givingBot.Chip2 = 0;
                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip2;
                            givingBot.Chip2 = 0;
                        }
                    }
                    else if (firstReceivingBot.Chip2 == 0)
                    {
                        firstReceivingBot.Chip2 = givingBot.Chip1;
                        givingBot.Chip1 = 0;
                        if (secondReceivingBot.Chip1 == 0)
                        {
                            secondReceivingBot.Chip1 = givingBot.Chip2;
                            givingBot.Chip2 = 0;

                        }
                        else if (secondReceivingBot.Chip2 == 0)
                        {
                            secondReceivingBot.Chip2 = givingBot.Chip2;
                            givingBot.Chip2 = 0;
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Bot already has 2 chips");
                    }
                }
            }
            else
            {
                throw new ArgumentException($"action {firstAction} is an unknown action");
            }
        }

        private static void AssignValuesToBots(string input, ref List<Bot> bots)
        {
            string pattern = @"value (\d+) goes to bot (\d+)";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            int value = int.Parse(match.Groups[1].Value);
            int botId = int.Parse(match.Groups[2].Value);

            Bot bot = bots.FirstOrDefault(x => x.Id == botId);
            if (bot != null)
            {
                if (bot.Chip1 == 0)
                {
                    bot.Chip1 = value;
                }
                else if (bot.Chip2 == 0)
                {
                    bot.Chip2 = value;
                }
                else
                {
                    throw new ArgumentException($"Bot {botId} already has 2 chips");
                }
            }
            else
            {
                bot = new Bot
                {
                    Id = botId,
                    Chip1 = value,
                    Type = "bot"
                };
                bots.Add(bot);
            }
        }
    }
}
