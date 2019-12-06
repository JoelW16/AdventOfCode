using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaySix
{
    public class OrbitMap
    {
        private List<CelestialBody> Orbits;

        public List<CelestialBody> BuildOrbitMap(string mapFile)
        {
            var orbits = ReadInOrbits(mapFile);
            Orbits = GetOrbitMap(orbits);
            return Orbits;
        }

        private IEnumerable<string[]> ReadInOrbits(string path)
        {
            var lines = File.ReadLines(path).ToList();
            var obits = lines.Select(o => o.Split(")"));
            return obits;
        }

        private List<CelestialBody> GetOrbitMap(IEnumerable<string[]>  orbits)
        {
            var orbitMap = new List<CelestialBody>();

            foreach (var orbit in orbits)
            {
                var mappedCelestialBody = orbitMap.FirstOrDefault(o => o.Name == orbit[0]);
                if (mappedCelestialBody == null)
                {
                    mappedCelestialBody = new CelestialBody(orbit[0]);
                    orbitMap.Add(mappedCelestialBody);
                }

                var mappedSatellite = orbitMap.FirstOrDefault(o => o.Name == orbit[1]);
                if (mappedSatellite == null)
                {
                    mappedSatellite = new CelestialBody(orbit[1]);
                    orbitMap.Add(mappedSatellite);
                }

                mappedCelestialBody.Satellites.Add(mappedSatellite);
                mappedSatellite.Orbits = mappedCelestialBody;
            }


            return orbitMap;
        }

        public int GetTotalNumberOfOrbits()
        {
            return Orbits.Sum(GetNumberOfOrbits);
        }

        private static int GetNumberOfOrbits(CelestialBody celestialBody)
        {
            return celestialBody.Orbits == null ? 0 : GetNumberOfOrbits(celestialBody.Orbits) + 1;
        }

        public int? NumberOfDijkstraHops(string sourceName, string destinationName)
        {
            var celestialBodies = Orbits.Select(i => i).ToList();
            var source = celestialBodies.FirstOrDefault(o => o.Name == sourceName)?.Orbits;
            var destination = celestialBodies.FirstOrDefault(o => o.Name == destinationName)?.Orbits;
            
            var shortestPathLength = celestialBodies.ToDictionary<CelestialBody, CelestialBody, int?>(celestialBody => celestialBody, celestialBody => null);
            if (source != null) shortestPathLength[source] = 0;

            while (celestialBodies.Count > 0)
            {
                var (key, _) = shortestPathLength.Where(sp => celestialBodies.Contains(sp.Key))
                    .OrderByDescending(e => e.Value.HasValue).ThenBy(e => e.Value).First();
                
                celestialBodies.Remove(key);

                foreach (var neighbor in key.Neighborhood)
                {
                    var distance = shortestPathLength[key] + 1;
                    if (shortestPathLength[neighbor] != null && !(distance < shortestPathLength[neighbor])) continue;
                    shortestPathLength[neighbor] = distance;
                }
            }
            return destination != null ? shortestPathLength[destination] : null;
        }
    }
}
