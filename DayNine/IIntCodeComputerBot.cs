using System;
using System.Collections.Generic;
using System.Text;

namespace DayNine
{
    public interface IIntCodeComputerBot
    {
        long Input { get; set; }

        long Output { get; set; }

        long GetInput();

        void SetOutput(long output);

    }
}
