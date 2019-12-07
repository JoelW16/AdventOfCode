using System;
using System.Collections.Generic;
using System.Text;

namespace DaySeven
{
    public class ThrustBot : IIntCodeComputerBot
    {
        public int Input { get; set; }
        public int Output { get; set; }
        private int thrustOutput = 0;
        private int inputNumber = 0;
        private int _currentPhase;
        private Dictionary<int, (int, int)> bestPhase;


        public void Run(IntCodeComputer computer, int[] program)
        {
            bestPhase = new Dictionary<int, (int, int)>();
            for (var i = 0; i < 6; i--)
            {
                for (var j = 0; j < 4; j++)
                {
                    _currentPhase = j;
                    computer.Run(program, 0, this);
                }
            }
        }


        public int GetInput()
        {
            switch (inputNumber)
            {
                case 0:
                    return _currentPhase;
                case 1:
                    return thrustOutput;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetOutput(int output)
        {
            thrustOutput = output;
        }

    }
}
