using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DayThirteen
{
    public class IntCodeGame : IIntCodeComputerBot  
    {
        public long Input { get; set; }
        public long Output { get; set; }
        public bool IsConcurrent { get; set; } = false;
        public long Score { get; private set; }

        private readonly IntCodeComputer _intCodeComputer;
        private readonly Dictionary<(long x, long y), long> _monitor;
        private int _outputCounter;
        private (long x, long y) _outputCoordinates;
        private bool _autoMode = true;


        public IntCodeGame(string path)
        {
            _intCodeComputer = new IntCodeComputer(path, false, this);
            _intCodeComputer.AddMemory(10000);
            _monitor = new Dictionary<(long, long), long>();

            Console.CursorVisible = false;
        }

        public int Run(bool autoMode = false)
        {
            _autoMode = autoMode;
            _intCodeComputer.Run();
            Console.SetCursorPosition(100, 0);
            return 0;
        }

        public long GetInput()
        {
            if (_autoMode)
            {
                var ball = _monitor.FirstOrDefault(i => i.Value == 4);
                var paddle = _monitor.FirstOrDefault(i => i.Value == 3);

                if (ball.Key.x > paddle.Key.x)
                {
                    return 1;
                }
                if (ball.Key.x == paddle.Key.x)
                {
                    return 0;
                }
                if (ball.Key.x < paddle.Key.x)
                {
                    return -1;
                }
            }

            //User Mode 
            ClearKeyBuffer();
            Thread.Sleep(100);
            if (!Console.KeyAvailable)
            {
                Console.SetCursorPosition(5, 0);
                Console.Write("no key pressed");
                return 0;
            }
            var pressed = Console.ReadKey().Key;
            Console.SetCursorPosition(50, 0);
            Console.Write(pressed);
            return pressed switch
            {
                ConsoleKey.LeftArrow => -1,
                ConsoleKey.RightArrow => 1,
                _ => 0
            };
        }


        public static void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(false);
        }

        public void SetOutput(long output)
        {
            switch (_outputCounter)
            {
                case 0:
                    _outputCoordinates.x = output;
                    _outputCounter++;
                    break;
                case 1:
                    _outputCoordinates.y = output;
                    _outputCounter++;
                    break;
                case 2:
                    if (_outputCoordinates == (-1, 0))
                    {
                        Score = output;
                        _outputCounter = 0;
                        PrintScore();
                        break;
                    }
                    else if (_monitor.ContainsKey(_outputCoordinates))
                    {
                        _monitor[_outputCoordinates] = output;
                    }
                    else
                    {
                        _monitor.Add(_outputCoordinates, output);
                    }
                    _outputCounter = 0;
                    UpdateDisplay(_outputCoordinates, output);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateDisplay((long x, long y) position, long type)
        {
            var x = (int) position.x + 1;
            var y = (int) position.y + 1;
            
            Console.SetCursorPosition(x, y);
            switch (type)
            {
                case 0:
                    PrintEmpty();
                    break;
                case 1:
                    PrintWall();
                    break;
                case 2:
                    PrintBlock();
                    break;
                case 3:
                    PrintPaddle();
                    break;
                case 4:
                    PrintBall();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ResetConsoleColours();

        }

        public void PrintScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Score: {Score}");
        }

        public void PrintEmpty()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("#");
        }

        public void PrintWall()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("#");
        }

        public void PrintBlock()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("#");
        }


        public void PrintPaddle()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("#");
        }

        public void PrintBall()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("#");
        }

        public void ResetConsoleColours()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
        }
    }
}
