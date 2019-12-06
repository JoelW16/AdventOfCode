using System;
using System.IO;

namespace DaySix
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "OrbitData.txt");

            var orbitMap = new OrbitMap();

            var numberOfBodies = orbitMap.BuildOrbitMap(dataDirectory);

            Console.WriteLine(numberOfBodies);
        }
    }
}
