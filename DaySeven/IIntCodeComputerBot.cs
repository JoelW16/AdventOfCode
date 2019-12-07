using System;
using System.Collections.Generic;
using System.Text;

namespace DaySeven
{
    public interface IIntCodeComputerBot
    {
        int Input { get; set; }

        int Output { get; set; }

        void Run(IntCodeComputer computer, int[] program);

        int GetInput();

        void SetOutput(int output);

    }
}
