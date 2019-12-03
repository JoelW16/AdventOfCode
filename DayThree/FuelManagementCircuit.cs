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
            Console.WriteLine($"Nearest intersection by manhattan distance is: {nearestIntersect}");

            var nearestIntersectOnWire = GetNearestIntersectOnWire(intersects, wires[0], wires[1]);
            Console.WriteLine($"Nearest intersection on the wire is: {nearestIntersectOnWire}");
        }

        private static List<Dictionary<(int, int), int>> ReadWires(string path)
        {
            var lines = File.ReadLines(path).ToList();
            var wires = lines.Select(ParseWire).ToList();
            return wires;
        }

        private static Dictionary<(int, int), int> ParseWire(string wireInstructions)
        {
            var location = (x: 0,y: 0);
            var distance = 0;
            var wire = new Dictionary<(int, int), int>();
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
                    distance++;
                    wire.TryAdd(location, distance);
                }
            }
            return wire;
        }

        private static List<(int, int)> GetIntersects(Dictionary<(int, int), int> wire1, Dictionary<(int, int), int> wire2)
        {
            return wire1.Keys.Intersect(wire2.Keys).ToList();
        }

        private static int GetNearestIntersectManhattanDistance(IEnumerable<(int x, int y)> intersects)
        {
            return intersects.Min(i => Math.Abs(i.x) + Math.Abs(i.y));
        }

        private static int GetNearestIntersectOnWire(IEnumerable<(int x, int y)> intersects, IReadOnlyDictionary<(int, int), int> wire1, IReadOnlyDictionary<(int, int), int> wire2)
        {
            return intersects.Min(i => wire1[i] + wire2[i]);
        }
    }




   
}