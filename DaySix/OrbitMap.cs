using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaySix
{
    public class OrbitMap
    {
        public int BuildOrbitMap(string mapFile)
        {
            var orbits = ReadInOrbits(mapFile);
            var orbitMap = GetOrbitMap(orbits);
            return GetTotalNumberOfOrbits(orbitMap);
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

        private static int GetTotalNumberOfOrbits(IEnumerable<CelestialBody> orbitMap)
        {
            return orbitMap.Sum(GetNumberOfOrbits);
        }

        private static int GetNumberOfOrbits(CelestialBody celestialBody)
        {
            return celestialBody.Orbits == null ? 0 : GetNumberOfOrbits(celestialBody.Orbits) + 1;
        }
        
    }
}
