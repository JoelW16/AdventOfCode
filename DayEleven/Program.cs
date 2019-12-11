using System;
using System.IO;

namespace DayEleven
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");

            var spacePoliceBot = new SpacePoliceBot(dataDirectory);
//            var numberOfVisitedTiles = spacePoliceBot.Run();
//
//            Console.WriteLine($"OUTPUT:{numberOfVisitedTiles}");

            spacePoliceBot.RunOnWhite();
        }
    }
}
