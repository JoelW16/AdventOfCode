using System;
using System.IO;

namespace DayTwelve
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "moons.txt");

            var orbitSimulation = new OrbitSimulation(dataDirectory);
            var energy = orbitSimulation.Run(1000);
            Console.WriteLine($"Energy in system after 1000 ticks: {energy}");


            orbitSimulation.Reset();
            var period = orbitSimulation.RunFindPeriod();
            Console.WriteLine($"The number of ticks in 1 period of all moons: {period}");


        }
    }
}
