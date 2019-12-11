using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayEleven
{
    public class SpacePoliceBot : IIntCodeComputerBot
    {
        public long Input { get; set; }
        public long Output { get; set; }
        public bool IsConcurrent { get; set; } = false;
        public bool IsDebug { get; set; } = false;


        private readonly IntCodeComputer _intCodeComputer;
        private (int x, int y) _location;
        private int _heading;
        private Dictionary<(int x, int y), int> _visited;
        private int _instructionCounter;

        public SpacePoliceBot(string path)
        {
            _intCodeComputer = new IntCodeComputer(path, false,this);
            _intCodeComputer.AddMemory(10000);
            _location = (0, 0);
            _heading = 0;
            _visited = new Dictionary<(int, int), int>();
        }

        public int Run()
        {
            _intCodeComputer.Run();
            return _visited.Count;
        }

        public int RunOnWhite()
        {
            _visited.Add(_location, 1);
            _intCodeComputer.Run();
            PrintReg();
            return _visited.Count;
        }

        public long GetInput()
        {
             if(_visited.ContainsKey(_location))
             {
                 return _visited[_location];
             }else
             {
                 return 0;
             }
        }

        public void SetOutput(long output)
        {
            switch (_instructionCounter)
            {
                case 0:
                    Paint(output);
                    _instructionCounter = 1;
                    break;
                case 1:
                    MoveLocation(output);
                    _instructionCounter = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Paint(long output)
        {
            if (_visited.ContainsKey(_location))
            {
                _visited[_location] = (int) output;
            }
            else
            {
                _visited.Add(_location, (int) output);
            }
        }

        private void MoveLocation(long output)
        {
            var direction = (output == 0)? 1 : -1;

            _heading += (90 * direction);
            if (_heading == 360) _heading = 0;
            if (_heading == -90) _heading = 270;

            _location = _heading switch
            {
                0 => (_location.x, _location.y + 1),
                90 => (_location.x + 1, _location.y),
                180 => (_location.x, _location.y - 1),
                270 => (_location.x - 1, _location.y),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void PrintReg()
        {
            var maxX =_visited.Keys.Select(i => i.x).Max();
            var minX = _visited.Keys.Select(i => i.x).Min();

            var maxY = _visited.Keys.Select(i => i.y).Max();
            var minY = _visited.Keys.Select(i => i.y).Min();

            Console.WriteLine($"Reg Resolution: {maxX - minX} x {maxY - minY}");
            Console.WriteLine("--Start--");
            for (var y = maxY; y >= minY; y--)
            {
                Console.WriteLine();
                for (var x = maxX; x >= minX; x--)
                {
                    if (_visited.ContainsKey((x, y)))
                    {
                        if (_visited[(x, y)] == 1)
                        {
                            PrintPrimary();
                        }
                        else
                        {
                            PrintSecondary();
                        }
                    }
                    else
                    {
                        PrintSecondary();
                    }
                }
            }
            ResetConsoleColours();
            Console.WriteLine("---End---");
        }

        public void PrintPrimary()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#");
        }

        public void PrintSecondary()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("#");
        }

        public void ResetConsoleColours()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }
}
