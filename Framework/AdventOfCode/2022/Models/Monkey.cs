using System;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode._2022.Models
{
    public class Monkey
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public string OperationValue { get; set; }
        public Queue<BigInteger> Items { get; set; } = new Queue<BigInteger>();
        public int Divisible { get; set; }
        public int TrueMonkey { get; set; }
        public int FalseMonkey { get; set; }
        public int ItemsInspected { get; set; }
    }
}
