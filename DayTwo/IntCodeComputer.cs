using System;
using System.IO;
using System.Linq;

namespace DayTwo
{
    public class IntCodeComputer
    {


        public static void Main(string[] args)
        {
            var intCodeProgram = ReadInIntCodeProgram();
            var programPointer = 0;
            var program = (intCodeProgram, programPointer);
            var returnValue = Run(program);

            Console.WriteLine(returnValue);

        }

        private static int[] ReadInIntCodeProgram(){
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");
            var lines = File.ReadLines(dataDirectory).ToList();

            var intCodeProgram = lines?[0].Split(',').Select(i => Convert.ToInt32(i)).ToArray();
            return intCodeProgram;
        }

        private static int Run((int[] intCodeProgram, int pointer) program)
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

        private static (int[], int) Add(int[] intCodeProgram, int pointer)
        {
            intCodeProgram[intCodeProgram[pointer + 3]] = intCodeProgram[intCodeProgram[pointer + 1]] + intCodeProgram[intCodeProgram[pointer + 2]];
            pointer += 4;
            return (intCodeProgram, pointer);
        }

        private static (int[], int) Multiply(int[] intCodeProgram, int pointer)
        {
            intCodeProgram[intCodeProgram[pointer + 3]] = intCodeProgram[intCodeProgram[pointer + 1]] * intCodeProgram[intCodeProgram[pointer + 2]];
            pointer += 4;
            return (intCodeProgram, pointer);
        }

    }
}
