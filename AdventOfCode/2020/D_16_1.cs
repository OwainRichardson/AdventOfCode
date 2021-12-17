using AdventOfCode._2020.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public static class D_16_1
    {
        public static void Execute()
        {
            string[] inputs = File.ReadAllLines(@"2020\Data\day16.txt");

            List<TicketField> ticketFields = ParseTicketFields(inputs);

            Ticket yourTicket = ParseYourTicket(inputs);

            List<Ticket> nearbyTickets = ParseNearbyTickets(inputs);

            CheckTicketValidity(nearbyTickets, ticketFields);
        }

        private static void CheckTicketValidity(List<Ticket> nearbyTickets, List<TicketField> ticketFields)
        {
            int ticketScanningErrorRate = 0;

            foreach (Ticket ticket in nearbyTickets)
            {
                foreach (int value in ticket.Values)
                {
                    if (ticketFields.All(x => !x.AcceptableNumbers.Contains(value)))
                    {
                        ticketScanningErrorRate += value;
                    }
                }
            }

            Console.WriteLine(ticketScanningErrorRate);
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