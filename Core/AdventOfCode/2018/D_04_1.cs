using AdventOfCode._2018.Models;
using AdventOfCode._2018.Models.Enums;
using System.Text.RegularExpressions;

namespace AdventOfCode._2018
{
    public static class D_04_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2018\Data\day04.txt").ToArray();

            List<GuardActionLog> actionLogs = ParseInputsToSleepLogs(inputs);

            Dictionary<int, int> guardSleepLogs = new Dictionary<int, int>();

            int currentGuardId = -1;
            int wentToSleep = -1;
            

            foreach (var log in actionLogs)
            {
                if (log.GuardId != null)
                {
                    currentGuardId = log.GuardId.Value;
                    continue;
                }

                if (log.Action == GuardSleepAction.FallsAsleep)
                {
                    wentToSleep = log.Minute;
                    continue;
                }

                if (log.Action == GuardSleepAction.WakesUp)
                {
                    if (guardSleepLogs.ContainsKey(currentGuardId))
                    {
                        guardSleepLogs[currentGuardId] += ((log.Minute) - wentToSleep);
                    }
                    else
                    {
                        guardSleepLogs.Add(currentGuardId, (log.Minute) - wentToSleep);
                    }
                }
            }

            int sleepiestGuard = guardSleepLogs.OrderByDescending(x => x.Value).First().Key;
            int[] minutes = new int[60];

            foreach (var log in actionLogs)
            {
                if (log.GuardId != null)
                {
                    currentGuardId = log.GuardId.Value;
                    continue;
                }

                if (log.Action == GuardSleepAction.FallsAsleep && currentGuardId == sleepiestGuard)
                {
                    wentToSleep = log.Minute;
                    continue;
                }

                if (log.Action == GuardSleepAction.WakesUp && currentGuardId == sleepiestGuard)
                {
                    for (int min = wentToSleep; min < log.Minute; min++)
                    {
                        minutes[min] += 1;
                    }
                }
            }

            int maxSleep = minutes.Max();

            var minuteMostAsleep = Array.IndexOf(minutes, maxSleep);

            Console.WriteLine(sleepiestGuard * minuteMostAsleep);
        }

        private static List<GuardActionLog> ParseInputsToSleepLogs(string[] inputs)
        {
            List<GuardActionLog> actionLogs = new List<GuardActionLog>();

            foreach (string input in inputs)
            {
                GuardActionLog actionLog = new GuardActionLog();

                string pattern = @"^\[(\S+)\W(\d{2}):(\d{2})\]\W(?:Guard\W\#(\d+))?(.+)$";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(input);

                actionLog.GuardId = ParseGuardId(match.Groups[4].Value);
                actionLog.Date = match.Groups[1].Value;
                actionLog.Hour = int.Parse(match.Groups[2].Value);
                actionLog.Minute = int.Parse(match.Groups[3].Value);
                actionLog.Action = ConvertToAction(match.Groups[5].Value);

                actionLogs.Add(actionLog);
            }

            return actionLogs.OrderBy(al => al.Date).ThenBy(al => al.Hour).ThenBy(al => al.Minute).ToList();
        }

        private static int? ParseGuardId(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            return int.Parse(value);
        }

        private static GuardSleepAction ConvertToAction(string value)
        {
            switch (value.Trim())
            {
                case "begins shift":
                    return GuardSleepAction.BeginsShift;
                case "falls asleep":
                    return GuardSleepAction.FallsAsleep;
                case "wakes up":
                    return GuardSleepAction.WakesUp;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
