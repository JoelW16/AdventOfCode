using System;
using System.IO;
using System.Linq;

namespace DayTwo
{
    public class IntCodeComputer
    {
        private int[] _intCodeProgram;
        private int _instructionPointer;

        public int[] ReadInIntCodeProgram(string path){
            var lines = File.ReadLines(path).ToList();
            var intCodeProgram = lines?[0].Split(',').Select(i => Convert.ToInt32(i)).ToArray();
            return intCodeProgram;
        }

        public int Run(int[] intCodeProgram, int instructionPointer)
        {
            _intCodeProgram = intCodeProgram;
            _instructionPointer = instructionPointer;

            while (true)
            {
                var opcode = (Opcode) _intCodeProgram[_instructionPointer];
                switch (opcode)
                {
                    case Opcode.Add:
                        Add();
                        break;
                    case Opcode.Multiply:
                        Multiply();
                        break;
                    case Opcode.Halt:
                        return _intCodeProgram[0];
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void Add()
        {
            _intCodeProgram[GetPointer(_instructionPointer + 3)] = GetPointerValue(_instructionPointer + 1) + GetPointerValue(_instructionPointer + 2);
            _instructionPointer += 4;
        }

        private void Multiply()
        {
            _intCodeProgram[GetPointer(_instructionPointer + 3)] = GetPointerValue(_instructionPointer + 1) * GetPointerValue(_instructionPointer + 2);
            _instructionPointer += 4;
        }

        private int GetPointer(int instructionPointer)
        {
            return _intCodeProgram[instructionPointer];
        }

        private int GetPointerValue(int instructionPointer){
            return _intCodeProgram[_intCodeProgram[instructionPointer]];
        }
    }
}
