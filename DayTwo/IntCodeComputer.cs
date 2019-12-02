using System;
using System.IO;
using System.Linq;

namespace DayTwo
{
    public class IntCodeComputer
    {
        public int[] ReadInIntCodeProgram(string path){
            var lines = File.ReadLines(path).ToList();
            var intCodeProgram = lines?[0].Split(',').Select(i => Convert.ToInt32(i)).ToArray();
            return intCodeProgram;
        }

        public int Run((int[] intCodeProgram, int pointer) program)
        {
            var intCodeProgram = program.intCodeProgram;
            var programPointer = program.pointer;
            var opcode = (Opcode) intCodeProgram[programPointer];
            switch (opcode)
            {
                case Opcode.Add:
                    program = Add(intCodeProgram, programPointer);
                    break;
                case Opcode.Multiply:
                    program = Multiply(intCodeProgram, programPointer);
                    break;
                case Opcode.Halt:
                    return intCodeProgram[0];
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return Run(program);
        }

        private (int[], int) Add(int[] intCodeProgram, int pointer)
        {
            intCodeProgram[intCodeProgram[pointer + 3]] = intCodeProgram[intCodeProgram[pointer + 1]] + intCodeProgram[intCodeProgram[pointer + 2]];
            pointer += 4;
            return (intCodeProgram, pointer);
        }

        private (int[], int) Multiply(int[] intCodeProgram, int pointer)
        {
            intCodeProgram[intCodeProgram[pointer + 3]] = intCodeProgram[intCodeProgram[pointer + 1]] * intCodeProgram[intCodeProgram[pointer + 2]];
            pointer += 4;
            return (intCodeProgram, pointer);
        }
    }
}
