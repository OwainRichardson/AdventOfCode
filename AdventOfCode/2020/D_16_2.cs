using AdventOfCode._2020.Models;
using AdventOfCode.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_16_2
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day16.txt");

            List<TicketField> ticketFields = ParseTicketFields(inputs);

            Ticket yourTicket = ParseYourTicket(inputs);

            List<Ticket> nearbyTickets = ParseNearbyTickets(inputs);

            List<Ticket> validTickets = CheckTicketValidity(nearbyTickets, ticketFields);

            WorkOutColumns(validTickets, ticketFields, yourTicket);
        }

        private static void WorkOutColumns(List<Ticket> validTickets, List<TicketField> ticketFields, Ticket yourTicket)
        {
            int numberOfFields = ticketFields.Count;
            Dictionary<int, List<string>> columns = new Dictionary<int, List<string>>();

            for (int index = 0; index < numberOfFields; index++)
            {
                var valuesForIndex = validTickets.Select(x => x.Values[index]).ToList();

                foreach (TicketField ticketField in ticketFields)
                {
                    bool containsAll = true;

                    foreach (int value in valuesForIndex)
                    {
                        if (!ticketField.AcceptableNumbers.Contains(value))
                        {
                            containsAll = false;
                        }
                    }

                    if (containsAll)
                    {
                        columns.AddOrUpdateTickets(index, ticketField.Name);
                    }
                }
            }

            while (columns.Any(x => x.Value.Count > 1))
            {
                var singleValueColumns = columns.Where(x => x.Value.Count == 1);

                foreach (var singleValueColumn in singleValueColumns)
                {
                    string value = singleValueColumn.Value.Single();

                    foreach (var column in columns.Where(x => x.Value.Count > 1))
                    {
                        if (column.Value.Contains(value))
                        {
                            column.Value.Remove(value);
                        }
                    }
                }
            }

            var departureColumns = columns.Where(x => x.Value.Single().StartsWith("departure")).ToList();

            long product = 1;

            foreach (var col in departureColumns)
            {
                product *= yourTicket.Values[col.Key];
            }

            Console.WriteLine(product);
        }

        private static List<Ticket> CheckTicketValidity(List<Ticket> nearbyTickets, List<TicketField> ticketFields)
        {
            List<Ticket> validTickets = new List<Ticket>();

            foreach (Ticket ticket in nearbyTickets)
            {
                bool valid = true;

                foreach (int value in ticket.Values)
                {
                    if (ticketFields.All(x => !x.AcceptableNumbers.Contains(value)))
                    {
                        valid = false;
                    }
                }

                if (valid) validTickets.Add(ticket);
            }

            return validTickets;
        }

        private static List<Ticket> ParseNearbyTickets(string[] inputs)
        {
            List<Ticket> nearbyTickets = new List<Ticket>();
            int indexOfStart = -1;

            for (int index = 0; index < inputs.Length; index++)
            {
                if (inputs[index] == "nearby tickets:")
                {
                    indexOfStart = index;

                }
                if (indexOfStart != -1 && index > indexOfStart)
                {
                    Ticket ticket = new Ticket();

                    ticket.Values = inputs[index].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

                    nearbyTickets.Add(ticket);
                }
            }

            return nearbyTickets;
        }

        private static Ticket ParseYourTicket(string[] inputs)
        {
            Ticket yourTicket = new Ticket();

            for (int index = 0; index < inputs.Length; index++)
            {
                if (inputs[index] == "your ticket:")
                {
                    string ticketInputs = inputs[index + 1];

                    yourTicket.Values = ticketInputs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
                }
            }

            return yourTicket;
        }

        private static List<TicketField> ParseTicketFields(string[] inputs)
        {
            List<TicketField> ticketFields = new List<TicketField>();

            string pattern = @"^(.+):\s(\d+)-(\d+)\s[o][r]\s(\d+)-(\d+)$";
            Regex regex = new Regex(pattern);

            foreach (string input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input)) break;

                TicketField ticketField = new TicketField();

                Match match = regex.Match(input);

                ticketField.Name = match.Groups[1].Value;

                int firstRangeStart = int.Parse(match.Groups[2].Value);
                int firstRangeEnd = int.Parse(match.Groups[3].Value);
                int secondRangeStart = int.Parse(match.Groups[4].Value);
                int secondRangeEnd = int.Parse(match.Groups[5].Value);

                for (int i = firstRangeStart; i <= firstRangeEnd; i++)
                {
                    ticketField.AcceptableNumbers.Add(i);
                }
                for (int i = secondRangeStart; i <= secondRangeEnd; i++)
                {
                    ticketField.AcceptableNumbers.Add(i);
                }

                ticketFields.Add(ticketField);
            }

            return ticketFields;
        }
    }
}