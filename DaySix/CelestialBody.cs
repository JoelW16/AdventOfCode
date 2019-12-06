using System;
using System.Collections.Generic;
using System.Text;

namespace DaySix
{
    public class CelestialBody
    {
        public string Name;
        public CelestialBody Orbits { get; set; }
        public List<CelestialBody> Satellites { get; set; }

        public CelestialBody(string name)
        {
            Name = name;
            Satellites = new List<CelestialBody>();
        }
    }
}
