﻿using System;

namespace DaySeven
{

    public class Instruction
    {
        public Opcode Opcode { get; set; }

        public int[] ParamModes { get; set; }

        public void Set(int instruction)
        {
            Opcode = (Opcode)(instruction % 100);
            var paramModes = instruction / 100;
            var numberOfParams = paramModes.ToString().Length;
            ParamModes = new int[numberOfParams];

            for (var i = 0; i < paramModes.ToString().Length; i++)
            {
                if (i == 0)
                {
                    ParamModes[i] = (int)(paramModes / 1) % 10;
                }
                else
                {
                    ParamModes[i] = (int)(paramModes / Math.Pow(10, i)) % 10;
                }
            }
        }

        public int GetMode(int paramNumber)
        {
            return (paramNumber -1) < ParamModes.Length ? ParamModes[paramNumber - 1] : 0;
        }

    }
}
