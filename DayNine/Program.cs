using System;
using System.IO;

namespace DayNine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");

            var intCodeComputer = new IntCodeComputer(dataDirectory);
            intCodeComputer.AddMemory(32000);
            intCodeComputer.Run();
        }
    }
}
