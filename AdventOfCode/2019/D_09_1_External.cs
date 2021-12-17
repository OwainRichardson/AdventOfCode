using System;
using System.Collections.Generic;

namespace AdventOfCode._2019
{
    public static class D_09_1_External
    {
        public static void Execute()
        {
            long[] data = new long[] { 1102, 34463338, 34463338, 63, 1007, 63, 34463338, 63, 1005, 63, 53, 1101, 0, 3, 1000, 109, 988, 209, 12, 9, 1000, 209, 6, 209, 3, 203, 0, 1008, 1000, 1, 63, 1005, 63, 65, 1008, 1000, 2, 63, 1005, 63, 904, 1008, 1000, 0, 63, 1005, 63, 58, 4, 25, 104, 0, 99, 4, 0, 104, 0, 99, 4, 17, 104, 0, 99, 0, 0, 1102, 32, 1, 1016, 1101, 38, 0, 1012, 1102, 1, 693, 1022, 1102, 1, 27, 1007, 1101, 0, 190, 1025, 1102, 20, 1, 1019, 1102, 1, 33, 1008, 1102, 28, 1, 1013, 1101, 0, 1, 1021, 1102, 1, 851, 1026, 1102, 22, 1, 1018, 1101, 29, 0, 1005, 1101, 0, 21, 1004, 1101, 36, 0, 1009, 1101, 0, 195, 1024, 1101, 0, 39, 1002, 1101, 0, 848, 1027, 1102, 1, 34, 1003, 1102, 23, 1, 1015, 1102, 1, 30, 1010, 1101, 0, 26, 1017, 1102, 1, 35, 1001, 1102, 1, 489, 1028, 1102, 1, 484, 1029, 1102, 1, 686, 1023, 1102, 0, 1, 1020, 1102, 25, 1, 1006, 1102, 1, 31, 1014, 1101, 37, 0, 1000, 1102, 1, 24, 1011, 109, 23, 2105, 1, 1, 4, 187, 1105, 1, 199, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -18, 2102, 1, -3, 63, 1008, 63, 41, 63, 1005, 63, 223, 1001, 64, 1, 64, 1106, 0, 225, 4, 205, 1002, 64, 2, 64, 109, 2, 2101, 0, 1, 63, 1008, 63, 33, 63, 1005, 63, 251, 4, 231, 1001, 64, 1, 64, 1105, 1, 251, 1002, 64, 2, 64, 109, 13, 21107, 40, 41, -8, 1005, 1012, 269, 4, 257, 1106, 0, 273, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 9, 1205, -8, 287, 4, 279, 1105, 1, 291, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -38, 2101, 0, 9, 63, 1008, 63, 34, 63, 1005, 63, 315, 1001, 64, 1, 64, 1106, 0, 317, 4, 297, 1002, 64, 2, 64, 109, 18, 21108, 41, 38, 1, 1005, 1010, 337, 1001, 64, 1, 64, 1106, 0, 339, 4, 323, 1002, 64, 2, 64, 109, -5, 2107, 30, 1, 63, 1005, 63, 359, 1001, 64, 1, 64, 1106, 0, 361, 4, 345, 1002, 64, 2, 64, 109, 14, 21101, 42, 0, -7, 1008, 1011, 42, 63, 1005, 63, 387, 4, 367, 1001, 64, 1, 64, 1106, 0, 387, 1002, 64, 2, 64, 109, -18, 1208, 0, 39, 63, 1005, 63, 403, 1106, 0, 409, 4, 393, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 3, 1207, -1, 38, 63, 1005, 63, 425, 1106, 0, 431, 4, 415, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -8, 1201, 5, 0, 63, 1008, 63, 35, 63, 1005, 63, 455, 1001, 64, 1, 64, 1106, 0, 457, 4, 437, 1002, 64, 2, 64, 109, 30, 1206, -4, 469, 1106, 0, 475, 4, 463, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 10, 2106, 0, -7, 4, 481, 1106, 0, 493, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -24, 21102, 43, 1, 3, 1008, 1014, 40, 63, 1005, 63, 517, 1001, 64, 1, 64, 1105, 1, 519, 4, 499, 1002, 64, 2, 64, 109, -4, 2108, 41, -5, 63, 1005, 63, 539, 1001, 64, 1, 64, 1106, 0, 541, 4, 525, 1002, 64, 2, 64, 109, 18, 21101, 44, 0, -8, 1008, 1017, 47, 63, 1005, 63, 561, 1105, 1, 567, 4, 547, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -24, 1202, 6, 1, 63, 1008, 63, 27, 63, 1005, 63, 589, 4, 573, 1106, 0, 593, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 7, 1208, -5, 34, 63, 1005, 63, 611, 4, 599, 1106, 0, 615, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -5, 1207, 6, 37, 63, 1005, 63, 637, 4, 621, 1001, 64, 1, 64, 1106, 0, 637, 1002, 64, 2, 64, 109, 23, 1206, -6, 655, 4, 643, 1001, 64, 1, 64, 1105, 1, 655, 1002, 64, 2, 64, 109, -10, 2107, 32, -8, 63, 1005, 63, 673, 4, 661, 1105, 1, 677, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 5, 2105, 1, 2, 1001, 64, 1, 64, 1106, 0, 695, 4, 683, 1002, 64, 2, 64, 109, -17, 1202, 0, 1, 63, 1008, 63, 20, 63, 1005, 63, 715, 1106, 0, 721, 4, 701, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -4, 1201, 4, 0, 63, 1008, 63, 21, 63, 1005, 63, 743, 4, 727, 1106, 0, 747, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 10, 1205, 10, 763, 1001, 64, 1, 64, 1105, 1, 765, 4, 753, 1002, 64, 2, 64, 109, 1, 21102, 45, 1, 1, 1008, 1012, 45, 63, 1005, 63, 787, 4, 771, 1105, 1, 791, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -4, 2102, 1, -2, 63, 1008, 63, 29, 63, 1005, 63, 813, 4, 797, 1105, 1, 817, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -4, 2108, 33, 5, 63, 1005, 63, 835, 4, 823, 1105, 1, 839, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, 23, 2106, 0, 1, 1106, 0, 857, 4, 845, 1001, 64, 1, 64, 1002, 64, 2, 64, 109, -12, 21108, 46, 46, 1, 1005, 1015, 879, 4, 863, 1001, 64, 1, 64, 1106, 0, 879, 1002, 64, 2, 64, 109, 10, 21107, 47, 46, -5, 1005, 1019, 899, 1001, 64, 1, 64, 1105, 1, 901, 4, 885, 4, 64, 99, 21101, 27, 0, 1, 21101, 915, 0, 0, 1105, 1, 922, 21201, 1, 52134, 1, 204, 1, 99, 109, 3, 1207, -2, 3, 63, 1005, 63, 964, 21201, -2, -1, 1, 21101, 0, 942, 0, 1105, 1, 922, 22101, 0, 1, -1, 21201, -2, -3, 1, 21101, 0, 957, 0, 1105, 1, 922, 22201, 1, -1, -2, 1106, 0, 968, 21201, -2, 0, -2, 109, -3, 2106, 0, 0 };

            // Part 1
            // long[] quine = new long[] {109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99};
            // long[] bignum = new long[] {1102,34915192,34915192,7,4,7,99,0};
            // long[] bignum2 = new long[] {104,1125899906842624,99};
            IntCodeMachine icm = new IntCodeMachine("ICM", data);
            long output;
            icm.Reset();
            while (icm.Next(new long[] { 1 }, out output))
            {
                System.Console.WriteLine($"1. {output}");
            }

            // Part 2
            icm.Reset();
            while (icm.Next(new long[] { 2 }, out output))
            {
                System.Console.WriteLine($"2. {output}");
            }
        }
    }

    class IntCodeTape
    {
        private long[] initialCode;
        Dictionary<long, long> code;

        public IntCodeTape(long[] data)
        {
            initialCode = new long[data.Length];
            code = new Dictionary<long, long>(data.Length);
            Array.Copy(data, initialCode, data.Length);
            Reset();
        }

        public long this[long index]
        {
            get
            {
                return (code.ContainsKey(index)) ? code[index] : 0;
            }
            set
            {
                code[index] = value;
            }
        }

        public void Reset()
        {
            code.Clear();
            for (int i = 0; i < initialCode.Length; i++) code.Add(i, initialCode[i]);
        }
    }

    class IntCodeMachine
    {
        public const int OP_ADD = 1;    // Add
        public const int OP_MUL = 2;    // Multiply
        public const int OP_IN = 3;     // Input value
        public const int OP_OUT = 4;    // Output value
        public const int OP_JMP_T = 5;  // Jump if true
        public const int OP_JMP_F = 6;  // Jump if false
        public const int OP_LT = 7;     // Less than
        public const int OP_EQ = 8;     // Equals
        public const int OP_SRB = 9;    // Set Relative Base
        public const int OP_END = 99;

        public const int MOD_POS = 0;
        public const int MOD_VAL = 1;
        public const int MOD_REL = 2;   // Relative Base for addressing

        public string name { get; }     // Identifier for the machine instance
        private IntCodeTape code;       // Current state
        private long idx;                // Execution pointer
        private long relBase;            // Relative Base, used for relative addressing

        public bool active { get; private set; }   // Is the machine currently running

        public IntCodeMachine(string name, long[] data)
        {
            this.name = name;
            this.code = new IntCodeTape(data);
        }

        public long Run(long[] input, bool force = false)
        {
            if (active && !force) throw new Exception($"Machine [{name}] already active");

            long output;
            Start(input, out output);   // Assume the machine has no intermediate output!
            return output;
        }

        public void Reset()
        {
            code.Reset();
            idx = 0;
            relBase = 0;
            active = true;
        }

        public void Stop() => active = false;

        public bool Start(long[] input, out long output)
        {
            Reset();
            return Next(input, out output);
        }

        private long Read(long index, long mode)
        {
            switch (mode)
            {
                case MOD_POS:
                    return code[code[index]];
                case MOD_VAL:
                    return code[index];
                case MOD_REL:
                    return code[relBase + code[index]];
                default:
                    throw new Exception($"Unknown mode: {mode}");
            }
        }

        private void Write(long index, long mode, long value)
        {
            switch (mode)
            {
                case MOD_POS:
                    code[code[index]] = value;
                    break;
                case MOD_VAL:
                    throw new Exception("MOD_VAL is invalid for writing");
                case MOD_REL:
                    code[relBase + code[index]] = value;
                    break;
                default:
                    throw new Exception($"Unknown mode: {mode}");
            }
        }

        private void WriteAction(long line, string msg)
        {
            System.Console.WriteLine($"[{line,2}] {msg}");
        }

        public bool Next(long[] inputs, out long output)
        {
            // Machine must have been initialised
            if (!active) throw new Exception("Machine not active");

            int in_ptr = 0;     // current position in the input array

            // System.Console.WriteLine($"[{name}][IN] idx = {idx}");

            // Guarantee that we have output
            output = Int64.MaxValue;

            bool running = true;
            long a, b;
            while (running)
            {
                long cmd = code[idx++];
                long opcode = cmd % 100;
                long mode1 = (cmd / 100) % 10;
                long mode2 = (cmd / 1000) % 10;
                long mode3 = (cmd / 10000) % 10;

                switch (opcode)
                {
                    case OP_ADD:
                        // Console.WriteLine("OP_ADD");
                        a = Read(idx++, mode1);
                        b = Read(idx++, mode2);
                        Write(idx++, mode3, a + b);
                        // WriteAction(idx - 4, $"[{cmd,4}] ADD {a} {b} to {c}");
                        break;
                    case OP_MUL:
                        // Console.WriteLine("OP_MUL");
                        a = Read(idx++, mode1);
                        b = Read(idx++, mode2);
                        Write(idx++, mode3, a * b);
                        // WriteAction(idx - 4, $"[{cmd,4}] MUL {a} {b} to {c}");
                        break;
                    case OP_IN:
                        // Console.WriteLine("OP_IN");
                        a = inputs[in_ptr++];   // Assume enough inputs
                        Write(idx++, mode1, a);
                        // WriteAction(idx - 3, $"[{cmd,4}] IN {a} to {b}");
                        break;
                    case OP_OUT:
                        // Console.WriteLine("OP_OUT");
                        output = Read(idx++, mode1);
                        // WriteAction(idx - 2, $"[{cmd,4}] OUT {output}");
                        return true;
                    case OP_JMP_T:
                        // Console.WriteLine("OP_JMP_T");
                        a = Read(idx++, mode1);
                        b = Read(idx++, mode2);
                        if (a != 0)
                        {
                            idx = b;
                        }
                        // WriteAction(idx - 3, $"[{cmd,4}] JMP TRUE {a} to {b}");
                        break;
                    case OP_JMP_F:
                        // Console.WriteLine("OP_JMP_F");
                        a = Read(idx++, mode1);
                        b = Read(idx++, mode2);
                        if (a == 0)
                        {
                            idx = b;
                        }
                        // WriteAction(idx - 3, $"[{cmd,4}] JMP FALSE {a} to {b}");
                        break;
                    case OP_LT:
                        // Console.WriteLine("OP_LT");
                        a = Read(idx++, mode1);
                        b = Read(idx++, mode2);
                        Write(idx++, mode3, (a < b) ? 1 : 0);
                        // WriteAction(idx - 4, $"[{cmd,4}] LT {a} {b} to {c}");
                        break;
                    case OP_EQ:
                        // Console.WriteLine("OP_EQ");
                        a = Read(idx++, mode1);
                        b = Read(idx++, mode2);
                        Write(idx++, mode3, (a == b) ? 1 : 0);
                        // WriteAction(idx - 4, $"[{cmd,4}] EQ {a} {b} to {c}");
                        break;
                    case OP_SRB:
                        // Console.WriteLine("OP_SRB");
                        a = Read(idx++, mode1);
                        relBase += a;
                        // WriteAction(idx - 2, $"[{cmd,4}] RELBASE {a} from {relBase - a} to {relBase}");
                        break;
                    case OP_END:
                        // Console.WriteLine("OP_END");
                        // WriteAction($"[{name}][END] idx = {idx}");
                        active = false;
                        return false;
                    default:
                        active = false;
                        throw new Exception($"Unknown OpCode: {opcode}");
                }
            }

            // End of run, but no output set (apparently)
            active = false;
            return false;
        }
    }
}