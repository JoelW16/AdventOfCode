using System;
using System.IO;

namespace DayTen
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "test.txt");

            var monitoringStation = new MonitoringStation(dataDirectory);
            var (key, value) = monitoringStation.FindBestViewPoint();
            Console.WriteLine($"The best asteroid is located at ({key.Item1}, {key.Item2}, viewing {value} asteroids.)");
           
            var manifest = monitoringStation.GetVaporiseAsteroidManifest((key.Item1, key.Item2));
            var result = (manifest[199].Coordinates.x * 100) + manifest[199].Coordinates.y;
            Console.WriteLine(result);
        }
    }
}
