using System;
using System.Collections.Generic;
using System.Text;

namespace DayEleven
{
    public interface IIntCodeComputerBot
    {
        long Input { get; set; }

        long Output { get; set; }

        bool IsConcurrent { get; set; }

        long GetInput();

        void SetOutput(long output);

    }
}
