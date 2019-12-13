using System;
using System.IO;

namespace DayThirteen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "IntCodeProgram.txt");

            var intGame = new IntCodeGame(dataDirectory);
            var blockTileCount = intGame.Run();

            Console.WriteLine(blockTileCount);
        }
    }
}
