using System;
using System.IO;

namespace DaySeven
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");

            var thrustBot = new ThrustBot();
            
            var resultPart1 = thrustBot.Run(dataDirectory);
            var resultPart2 = thrustBot.Run(dataDirectory, true);

            Console.WriteLine($"Part 1:{resultPart1} \nPart 2:{resultPart2}");
        }
    }
}
