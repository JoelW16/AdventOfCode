using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DaySix
{
    public class CelestialBody
    {
        public string Name;
        public CelestialBody Orbits { get; set; }
        public List<CelestialBody> Satellites { get; set; }

        public List<CelestialBody> Neighborhood
        {
            get
            {
                var neighborhood = Satellites.Select(s => s).ToList();
                if(Orbits != null) neighborhood.Add(Orbits);
                return neighborhood;
            }
        }

        public CelestialBody(string name)
        {
            Name = name;
            Satellites = new List<CelestialBody>();
        }
    }
}
