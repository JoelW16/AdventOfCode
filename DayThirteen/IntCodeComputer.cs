using System;
using System.IO;
using System.Linq;

namespace DayThirteen
{
    public class IntCodeComputer
    {
        private long[] _intCodeProgram;
        private long _instructionPointer = 0; 
        private long _relativeBase = 0;
        private readonly Instruction _instruction = new Instruction();
        private readonly IIntCodeComputerBot _bot;
        private long _inputValue;
        private bool _isRunning;
        private readonly bool _isDebug;

        public IntCodeComputer()
        {
        }

        public IntCodeComputer(string path, bool isDebug = false, IIntCodeComputerBot bot = null )
        {
            ReadInIntCodeProgram(path);
            _bot = bot;
            _isDebug = isDebug;
        }

        public long[] ReadInIntCodeProgram(string path){
            var lines = File.ReadLines(path).ToList();
            _intCodeProgram = lines?[0].Split(',').Select(long.Parse).ToArray();
            return _intCodeProgram;
        }

        public void AddMemory(int size)
        {
            Array.Resize<long>(ref _intCodeProgram, _intCodeProgram.Length + size);
        }

        public long Run()
        {
            _isRunning = true;
            while (_isRunning)
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
                    case Opcode.RelativeBaseOffset:
                        RelativeBaseOffset();
                        break;
                    case Opcode.Halt:
                        return Halt();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return 0;
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
            if(_isDebug)Console.WriteLine(GetValue(1));
            _bot?.SetOutput(GetValue(1));
            _instructionPointer += 2;
            if (_bot != null && _bot.IsConcurrent ) _isRunning = false;
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

        private void RelativeBaseOffset()
        {
            _relativeBase += GetValue(1);
            _instructionPointer += 2;
        }


        private long Halt()
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
                if (_isDebug) Console.WriteLine(_inputValue);
            }
        }

        private long GetValue(long paramNumber)
        {
            var mode = _instruction.GetMode(paramNumber);

            switch (mode)
            {
                case 0:
                    return GetPointerValue(_instructionPointer + paramNumber);
                case 1:
                    return GetPointer(_instructionPointer + paramNumber);
                case 2:
                    return GetPointerValue(_instructionPointer + paramNumber, _relativeBase);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetValue(long paramNumber, long value)
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
                case 2:
                    SetPointerValue(_instructionPointer + paramNumber, value, _relativeBase);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private long GetPointer(long instructionPointer)
        {
            return _intCodeProgram[instructionPointer];
        }

        private long GetPointerValue(long instructionPointer, long relativeBase = 0){
            return _intCodeProgram[GetPointer(instructionPointer) + relativeBase];
        }


        private void SetPointer(long instructionPointer, long value)
        {
            _intCodeProgram[instructionPointer] = value;
        }

        private void SetPointerValue(long instructionPointer, long value, long relativeBase = 0)
        {
            _intCodeProgram[GetPointer(instructionPointer) + relativeBase] = value;
        }
    }
}
