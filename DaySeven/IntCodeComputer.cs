using System;
using System.IO;
using System.Linq;

namespace DaySeven
{
    public class IntCodeComputer
    {
        private int[] _intCodeProgram;
        private int _instructionPointer = 0;
        private readonly Instruction _instruction = new Instruction();
        private readonly IIntCodeComputerBot _bot;
        private int _inputValue;

        public IntCodeComputer()
        {
        }

        public IntCodeComputer(string path, IIntCodeComputerBot bot = null)
        {
            ReadInIntCodeProgram(path);
            _bot = bot;
        }

        public int[] ReadInIntCodeProgram(string path){
            var lines = File.ReadLines(path).ToList();
            _intCodeProgram = lines?[0].Split(',').Select(i => Convert.ToInt32(i)).ToArray();
            return _intCodeProgram;
        }

        public int Run()
        {
            while (true)
            {
                SetInstruction();
                switch (_instruction.Opcode)
                {
                    case Opcode.Add:
                        Add();
                        break;
                    case Opcode.Multiply:
                        Multiply();
                        break;
                    case Opcode.Input:
                        Input();
                        break;
                    case Opcode.Output:
                        Output();
                        break;
                    case Opcode.JumpTrue:
                        JumpTrue();
                        break;
                    case Opcode.JumpFalse:
                        JumpFalse();
                        break;
                    case Opcode.LessThan:
                        LessThan();
                        break;
                    case Opcode.Equals:
                        Equals();
                        break;
                    case Opcode.Halt:
                        return Halt();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SetInstruction()
        {
            var instruction = GetPointer(_instructionPointer);
            _instruction.Set(instruction);
        }

        private void Add()
        {
            SetValue(3, GetValue(1) + GetValue(2));
            _instructionPointer += 4;
        }

        private void Multiply()
        {
            SetValue(3, GetValue( 1) * GetValue(2));
            _instructionPointer += 4;
        }

        private void Input()
        {
            ReadInput();
            SetValue(1, _inputValue);
            _instructionPointer += 2;
        }

        private void Output()
        {
            Console.WriteLine(GetValue(1));
            _bot?.SetOutput(GetValue(1));
            _instructionPointer += 2;
        }

        private void JumpTrue()
        {
            if (GetValue(1) != 0)
            {
                _instructionPointer = GetValue(2);
            }
            else
            {
                _instructionPointer += 3;
            }
                
        }

        private void JumpFalse()
        {
            if (GetValue(1) == 0)
            {
                _instructionPointer = GetValue(2);
            }
            else
            {
                _instructionPointer += 3;
            }
                
        }


        private void LessThan()
        {
            SetValue(3, GetValue(1) < GetValue(2) ? 1 : 0);
            _instructionPointer += 4;
        }

        private void Equals()
        {
            SetValue(3, GetValue(1) == GetValue(2) ? 1 : 0);
            _instructionPointer += 4;
        }


        private int Halt()
        {
            return _intCodeProgram[0];
        }

        private void ReadInput(bool invalidInput = false)
        {
            if (_bot == null)
            {
                Console.WriteLine(!invalidInput ? "Input data:" : "Invalid Data! input data:");
                var input = Console.ReadLine();
                if (input == null)
                {
                    ReadInput(true);
                }
                else
                {
                    _inputValue = int.Parse(input);
                }
            }
            else
            {
                _inputValue = _bot.GetInput();
            }
        }

        private int GetValue(int paramNumber)
        {
            var mode = _instruction.GetMode(paramNumber);

            switch (mode)
            {
                case 0:
                    return GetPointerValue(_instructionPointer + paramNumber);
                case 1:
                    return GetPointer(_instructionPointer + paramNumber);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetValue(int paramNumber, int value)
        {
            var mode = _instruction.GetMode(paramNumber);

            switch (mode)
            {
                case 0:
                    SetPointerValue(_instructionPointer + paramNumber, value);
                    break;
                case 1:
                    SetPointer(_instructionPointer + paramNumber, value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private int GetPointer(int instructionPointer)
        {
            return _intCodeProgram[instructionPointer];
        }

        private int GetPointerValue(int instructionPointer){
            return _intCodeProgram[GetPointer(instructionPointer)];
        }

        private int SetPointer(int instructionPointer, int value)
        {
            return _intCodeProgram[instructionPointer];
        }

        private void SetPointerValue(int instructionPointer, int value)
        {
            _intCodeProgram[GetPointer(instructionPointer)] = value;
        }
    }
}
