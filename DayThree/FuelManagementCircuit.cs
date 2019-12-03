using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DayThree
{
    public class FuelManagementCircuit
    {

        public static void Main(string[] args)
        {
            var projectDirectory =Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"));
            var dataDirectory = Path.Combine(projectDirectory, @"data", "WirePaths.txt");

            var wires = ReadWires(dataDirectory);
            var intersects = GetIntersects(wires[0], wires[1]);

            var nearestIntersect = GetNearestIntersectManhattanDistance(intersects);
            Console.WriteLine(nearestIntersect);

        }

        private static List<IEnumerable<(int, int)>> ReadWires(string path)
        {
            var lines = File.ReadLines(path).ToList();
            var wires = lines.Select(ParseWire).ToList();
            return wires;
        }

        private static IEnumerable<(int, int)> ParseWire(string wireInstructions)
        {
            var location = (x: 0,y: 0);
            var wire = new List<(int, int)>();
            var instructions = wireInstructions.Split(',');
            foreach (var instruction in instructions)
            {
                var heading = instruction[0];
                var length = int.Parse(instruction[1..]);
                var (x, y) = (0, 0);

                switch(heading)
                {
                    case 'R':
                        x = 1;
                        break;
                    case 'L':
                        x = -1;
                        break;
                    case 'U':
                        y = 1;
                        break;
                    case 'D':
                        y = -1;
                        break;
                };

                for (var i = 0; i < length; i++)
                {
                    location = (location.x + x, location.y + y);
                    wire.Add(location);
                }
            }
            return wire;
        }

        private static IEnumerable<(int, int)> GetIntersects(IEnumerable<(int, int)> wire1, IEnumerable<(int, int)> wire2)
        {
            return wire1.Intersect(wire2);
        }

        private static int GetNearestIntersectManhattanDistance(IEnumerable<(int x, int y)> intersects)
        {
            return intersects.Min(GetManhattanDistance);
        }

        private static int GetManhattanDistance((int x,int y) coordinates)
        {
            return Math.Abs(coordinates.x) + Math.Abs(coordinates.y);
        }
    }




   
}