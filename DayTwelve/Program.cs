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
//            var energy = orbitSimulation.Run(1000);

            orbitSimulation.RunFindPeriod();

            //Console.WriteLine(energy);

        }
    }
}
