using System;
using System.Collections.Generic;
using System.Text;

namespace DayThirteen
{
    public enum Opcode
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpTrue = 5,
        JumpFalse = 6,
        LessThan = 7,
        Equals = 8,
        RelativeBaseOffset = 9,
        Halt = 99
    }
}
