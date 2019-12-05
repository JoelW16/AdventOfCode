using System;
using System.IO;

namespace DayFive
{
    public class ThermalEnvironmentSupervisionTerminal
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");

            var computer = new IntCodeComputer();
            var intCodeProgram = computer.ReadInIntCodeProgram(dataDirectory);

            computer.Run(intCodeProgram, 0);
        }
    }
}